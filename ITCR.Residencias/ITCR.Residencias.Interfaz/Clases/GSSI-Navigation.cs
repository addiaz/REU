using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.IO;
using System.Web.Hosting;
using System.Collections.Generic;
using System.Web.Caching;

/// <summary>
/// AutoNav.NET
/// Version 1.0.1 Beta
/// </summary>
/// <remarks>
/// CHANGELOG
/// 2010-06-29	1.0.1	Small XML capitalization change
/// 2010-02-02	1.0		Added versioning
/// </remarks>

namespace GSSI.Web {
	public class SiteMapProvider : StaticSiteMapProvider {
		public SiteMapProvider()
		{
		}

		internal object _lock = new object();

		private bool _initialized = false;
		private SiteMapNode _root = null;
		private string _siteMapFilePath = "Web.sitemap";
		private Dictionary<string, SiteMapNode> _siteMapTable; 

		private Dictionary<string, SiteMapNode> siteMapTable  {
			get {
				if (_siteMapTable == null) {
					lock (_lock) {
						if (_siteMapTable == null) {
							_siteMapTable = new Dictionary<string, SiteMapNode>();
						}
					}
				}

				return _siteMapTable;
			}
		}


		public override SiteMapNode BuildSiteMap() {
			lock (_lock) {
				// If siteMap is already constructed, simply returns it.
				if (_siteMapTable != null && _root != null) {
					return _root;
				}

				XmlDocument siteMapFile = GetConfigDocument();

				if (siteMapFile.HasChildNodes && siteMapFile.ChildNodes[1].HasChildNodes) {
					AddXmlNodes(siteMapFile.ChildNodes[1].ChildNodes, _root);
				}
				else {
					_root = new SiteMapNode(this, "empty", "~/", "Home");
				}
				return _root;
			}
		}
		private XmlDocument GetConfigDocument() {
			//if (_document != null)
			//	return _document;

			XmlDocument siteMapFile = new XmlDocument();
			string fullSiteMapPath = HostingEnvironment.MapPath(AppPathWithSlash() + _siteMapFilePath);

			try {				
				siteMapFile.Load(fullSiteMapPath);
			}
			catch {
				throw new Exception("Sitemap file could not be loaded.  Did you remember to convert special characters to html codes (ex: & to &amp;)?");
			}

			//_document = siteMapFile;
			return siteMapFile;
		}
		protected override void AddNode(SiteMapNode node) {
			siteMapTable.Add(node.Key, node);
		}
		protected override void AddNode(SiteMapNode node, SiteMapNode parentNode) {
			node.ParentNode = parentNode;
			siteMapTable.Add(node.Key, node);
		}
		public override SiteMapNodeCollection GetChildNodes(SiteMapNode node) {
			SiteMapNodeCollection nodeCollection = new SiteMapNodeCollection();
			
			foreach (KeyValuePair<string,SiteMapNode> smNode in siteMapTable) {
				// Exclude root node
				if (smNode.Value.ParentNode != null) {
					if (smNode.Value.ParentNode == node) {
						nodeCollection.Add(smNode.Value);
					}
				}
			}
			return nodeCollection;
		}

		private void AddXmlNodes(XmlNodeList xmlNodes, SiteMapNode parentNode) {
			SiteMapNode smNode = null;
			string title = null, url = null, description = null;

			foreach (XmlNode node in xmlNodes) {
				GetXmlValue(node, "title", ref title);
				GetXmlValue(node, "url", ref url);
				GetXmlValue(node, "description", ref description);
				
				smNode = new SiteMapNode(this, Guid.NewGuid().ToString(), url, title, description);

				foreach (XmlAttribute attribute in node.Attributes) {
					smNode[attribute.Name] = attribute.Value;
				}
				if (parentNode == null) {
					AddNode(smNode, parentNode);
					_root = smNode;
				}
				else
					AddNode(smNode, parentNode);
				
				if (node.HasChildNodes) {
					AddXmlNodes(node.ChildNodes, smNode);
				}
			}
		}
		private void GetXmlValue(XmlNode node, string attribute, ref string assignTo) {
			if (node.Attributes != null && node.Attributes[attribute] != null) {
				assignTo = node.Attributes[attribute].Value;
				node.Attributes.Remove(node.Attributes[attribute]);
			}
		}
		private string AppPathWithSlash() {
			if (HostingEnvironment.ApplicationVirtualPath == "/") {
				return "/";
			}
			else {
				return HostingEnvironment.ApplicationVirtualPath + "/";
			}
		}
		protected override SiteMapNode GetRootNodeCore() {
			BuildSiteMap();
			return _root;
		}
		protected override void Clear() {
			lock (_lock) {
				_root = null;
				_siteMapTable.Clear();
				_siteMapTable = null;
				base.Clear();
			}
		}

		private void OnConfigFileChange(string key, object value, CacheItemRemovedReason reason) {

			// Notifiy the parent for the change.
			//StaticSiteMapProvider parentProvider = ParentProvider as StaticSiteMapProvider;
			//if (parentProvider != null) {
			//	parentProvider.OnConfigFileChange(key, value, reason);
			//}
			Clear();
			BuildSiteMap();

		} 
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes) {
			if (_initialized) {
				throw new InvalidOperationException(
					"SiteMap provider cannot be initialized twice.");
			}

			if (attributes != null) {
				if (!String.IsNullOrEmpty(attributes["siteMapFile"])) {
					_siteMapFilePath = attributes["siteMapFile"];
				}
			}

			base.Initialize(name, attributes);

			string fullSiteMapPath = HostingEnvironment.MapPath(AppPathWithSlash() + _siteMapFilePath);
			if (!String.IsNullOrEmpty(fullSiteMapPath)) {
				CacheItemRemovedCallback _handler = new CacheItemRemovedCallback(OnConfigFileChange);
				CacheDependency _dep = new CacheDependency(fullSiteMapPath);
				HttpContext.Current.Cache.Add(GetHashCode().ToString(), "", _dep,
					Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration,
					CacheItemPriority.Normal, _handler);
				ResourceKey = (new FileInfo(fullSiteMapPath)).Name;
			}


			_initialized = true;
		}
	}
	public class MainNavConfiguration  : ConfigurationSection {

		public static MainNavConfiguration GetConfig() {
            return System.Configuration.ConfigurationManager.GetSection("mainNav") as MainNavConfiguration;
		}
		[ConfigurationProperty("menus")]
		public MainNavConfigurationStateCollection Menus { get { return this["menus"] as MainNavConfigurationStateCollection; } }
	}
	public class MainNavConfigurationState : ConfigurationElement {
		
		[ConfigurationProperty("menuID", IsRequired = true)]
		public string MenuID { get { return this["menuID"] as string; } }

		[ConfigurationProperty("url", IsRequired = true)]
		public string URL { get { return this["url"] as string; } }

	}
	public class MainNavConfigurationStateCollection : ConfigurationElementCollection {
		public MainNavConfigurationState this[int index] {
			get {
				return base.BaseGet(index) as MainNavConfigurationState;
			}
			set {
				if (base.BaseGet(index) != null) {
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		protected override ConfigurationElement CreateNewElement() {
			return new MainNavConfigurationState();
		}

		protected override object GetElementKey(ConfigurationElement element) {
			return ((MainNavConfigurationState)element).MenuID;
		} 
	}
}
