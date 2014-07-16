<%@ Control Language="C#" ClassName="Navigation"  %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="GSSI.Web" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="System.Reflection" %>


<script runat="server">
/// <summary>
/// AutoNav.NET
/// Version 1.9.11
/// Designed at: http://www.graphicallyspeaking.ca/
/// Primary dev: Matthew Leichty - http://www.matthewleichty.com/
/// </summary>
/// <remarks>
/// CHANGELOG
/// 2011-08-03	1.9.11	Added auto-detection of PathPrefix localization for Sitefinity
/// 2011-05-30	1.9.10	Trim on MainNavCollection string
/// 2011-05-04	1.9.9	Fixed deprecated MainNav rendering, fixed highlighting for Sitefinity 4
/// 2011-04-27  1.9.8   Fixed external links bug in multi-language mode
/// 2011-04-05	1.9.7	Fixed MainNav TopLevelHTMLTemplate bug with ASP.NET postbacks
///						Fixed reverted feature - main navs can again render sub-navs with jQuery
/// 2011-02-08	1.9.6	Main nav now supports TopLevelHTMLTemplate
/// 2011-02-08	1.9.5	Bug fixes for main navigation / language support
/// 2011-02-01  1.9.4   Added Sitefinity 4.0 Navigation hiding
/// 2011-01-28	1.9.3	Updated Main Nav so that it will update link text based on the SiteMap title.
/// 2010-12-22	1.9.2	Better auto-highlighting
/// 2010-12-02	1.9.1	Complete re-write of Main Navigation selection.  Main Nav can appear anywhere in site hierarchy, not just in the root.
///						Rewrite URL matching portion to not require tildes.
/// 2010-11-03	1.8		Added LinkHTMLTemplate
/// 2010-11-01	1.7		Added full path prefix option
/// 2010-10-29	1.6.1	External link option
///						Assignment of multiple SiteMap providers
///						Bug fixes: Sitefinity localization and 1.6 fixes
/// 2010-10-27	1.6		web.config-less Configuration!
/// 2010-10-18	1.5.2	Fixing multi-language bug with auto-highlighting
/// 2010-10-06	1.5.1	Multi-language fix
/// 2010-08-19	1.5		Added Sitefinity localization / language support (only for pathprefix)
/// 2010-08-17	1.4.1	Fixed breadcrumb span HTML rendering 
/// 2010-08-03	1.4		Added select box rendering
///						Changed _topLevelHTMLTemplate
/// 2010-07-08	1.3		Fixed grouping error for top navigation
///						Moved jQuery rendering to support multiple MainNavigation controls
///						Allowed rendering of navigation below a particular numbered node, anywhere in the node tree
/// 2010-06-29	1.2		Implemented list grouping (for L2 lists only)
/// 2010-04-14  1.1.3   Fixed weird path error in Sitefinity (appending "~/" to some links)
/// 2010-04-14  1.1.2   Added support for external pages in Sitefinity
/// 2010-04-06	1.1.1	Added level and span tag to Home link for mobile nav
/// 2010-03-04	1.1		Added ShowAllChildrenUnderNode to render all children nodes under a certain level in TreeView, 
///						regardless of active state of parent (for Jim Pattison)
/// 2010-03-02	1.0.3	Tooltip testing removed
/// 2010-02-23	1.0.2	Fixed apostrophe in nav bug
/// 2010-02-05	1.0.1	Fixed Sitefinity null error for node cloning
/// 					Pushed down jQuery-generated nav for Opera, other Opera fixes
/// 2010-02-03	1.0		Documented release
/// 2010-02-02	0.6		Ditched DMenu, updated MainNav
/// 2010-01-20	0.5.1	Small changes to paged navigation
/// 2010-01-06	0.5		Main navigation menus (superfish)
///						Paged navigation	
/// 2010-01-05	0.4.5	Small fixes
/// 2009-12-30	0.4.4	Fixed Sitefinity-highlighting with two identical nodes
/// 2009-12-16	0.4.3	Sitefinity-specific highlighting fix
/// 2009-12-15	0.4.2	Fixed auto-highlighting for deep sub-folders
///						Made node-finding case-insensitive
/// 2009-12-15	0.4.1	Fixed bug of Sitefinity rendering more items than needed
/// 2009-12-09	0.4		DMenu revisions for Sitefinity
/// 2009-11-26	0.3		Breadcrumbs updates
/// 2009-11-26	0.2.1	Added first-child, last-child class to last children of nodes
/// 2009-11-24	0.2		Changed sitemap / top as links properties
/// 2009-11-05	0.1		Alpha Release
/// </remarks>
	protected void Page_Load(object sender, EventArgs e)
	{
		//if (!Page.IsPostBack)
		// Later we can do some caching.  For now, generates nav on each page load.
			GenerateNavigation();
	}
	public HtmlGenericControl navigation;

	private SiteMapNode localSiteMap;

	private List<HyperLink> breadcrumbs;

	Dictionary<string, object> MainNavs = new Dictionary<string, object>();
	MainNavConfiguration MainNavSettings = MainNavConfiguration.GetConfig();
		
	private string _activeClass = "active";
	private string _inactiveClass = "inactive";
	private string _moreClass = "more";
	private string _navListClass = "nav";
	private bool _autoActivate = true;
	private bool _showTopLevelNav = false;
	private string _autoActivateTitleID = "";
	private string _breadcrumbSeparator = ">";
	private bool _showBreadcrumbHomeLink = true;
	private string _homeLinkTitle = null;
	private bool _renderTopLevelAsLinks = true;
	private string _topLevelHTMLTemplate = String.Empty;  // <span class=\"subnav-title\">{1}</span>
	private string _linkTemplate = "<span>{1}</span>";
	private string _containerCssClass = null;
		
	private int _maxDepth = 0;
	private bool _loadNavWithJQuery = true;
	private bool _pagedNavigation = false;
	private string _navCode = "";
	private int _showChildrenAt = 0;
	private string _goToTree = "";
	
	private bool _foundInNav = false;
	private bool autoHighlight = false;
	private int _startNavDepth = 1;
	private int _navDepth = 1;
	
	private RenderingModeOptions _renderingMode = RenderingModeOptions.SubNavigation;
	// private AutoActivateOptions _autoActivateTitle = AutoActivateOptions.Header1;
	private bool _findByID = false;
	private bool _useMainNav = false;

	private int _siteMapColumns = 1;  // Not implemented yet
	private int _groupSize = 0;
	
	private string ApplicationPath;
	private string CurrentPath;
	
	private SiteMapNode MostSpecificNode;
	private int MostSpecificNodeLength = 0;

	private string _defaultText = String.Empty;
	private bool _landingPageLink = false;
	private bool _debugMode = false;

	private bool _overwriteMainTitle = true;
	
	private string _mainNavCol = String.Empty;
	private Control mainNavControl;
	
	private List<Control> mainNavCollection = new List<Control>();
	private Dictionary<SiteMapNode, int> mainNavsInSitemap = new Dictionary<SiteMapNode, int>();
	private List<string> mainNavPathMatches = new List<string>();

	private string _siteMapProvider = String.Empty;
	private bool _openExternal = true;
	private bool? _useSitefinityLocalization = null;
	private bool _useFullPath = false;
	private string pathPrefix = "";
	bool usePaged = false;
	
	
	// "Treeview" now "SubNavigation".  Remove TreeView later.
	public enum RenderingModeOptions {
		MainNavigation, SubNavigation, TreeView, BreadCrumbs, Sitemap, SelectBox
	}
	
	// Configuration
	private System.Web.SiteMapProvider siteMapProvider {
		get {
			if (String.IsNullOrEmpty(SiteMapProviderName))
				return SiteMap.Provider;
			else {
				try {
					return SiteMap.Providers[SiteMapProviderName];
				}
				catch {
					return SiteMap.Provider;			
				}
			}
		}
	}
	
	public RenderingModeOptions RenderingMode { get { return _renderingMode; } set { _renderingMode = value; } }
	public int MaximumTreeDepth { get { return _maxDepth; } set { _maxDepth = value; } }
	public string StartingNode { get { return _goToTree; } set { _goToTree = value; } }
	
	public string SiteMapProviderName { get { return _siteMapProvider; } set { _siteMapProvider = value; } }
	// Turns on Path Prefix conversions
	//public bool UseSitefinityLocalization { get { return _useSitefinityLocalization; } set { _useSitefinityLocalization = value; } }
	public bool UseSitefinityLocalization {
		get {
			if (_useSitefinityLocalization != null)
				return (bool)_useSitefinityLocalization;
			else {
				try {
					Type t = Type.GetType("Telerik.Localization.Configuration.ConfigHelper, Telerik.Localization", true);

					object obj = GetNewObject(t);
					object mode = FollowPropertyPath(obj, "Handler.PersistenceMode");

					if (mode == null) {
						return false;
					}
					else {
						_useSitefinityLocalization = (mode.ToString() == "PathPrefix");
						return (bool)_useSitefinityLocalization;
					}
				}
				catch {
					_useSitefinityLocalization = false;
					return false;
				}
			}
		}
	}
	
			
	public bool UseFullPath { get { return _useFullPath; } set { _useFullPath = value; } }
	public bool DebugMode { get { return _debugMode; } set { _debugMode = value; } }
	
	[Category("Appearance")]
	public string ActiveCssClass { get { return _activeClass; } set { _activeClass = value; } }
	[Category("Appearance")]
	public string InactiveCssClass { get { return _inactiveClass; } set { _inactiveClass = value; } }
	[Category("Appearance")]
	public string MoreCssClass { get { return _moreClass; } set { _moreClass = value; } }
	[Category("Appearance")]
	public string NavigationCssClass { get { return _navListClass; } set { _navListClass = value; } }
	[Category("Appearance")]
	public string TopLevelHTMLTemplate { get { return _topLevelHTMLTemplate; } set { _topLevelHTMLTemplate = value; } }
	[Category("Appearance")]
	public string LinkHTMLTemplate { get { return _linkTemplate; } set { _linkTemplate = value; } }
//	[Category("Appearance")]
	private bool RenderTopLevelAsLinks { get { return _topLevelHTMLTemplate == String.Empty; } }
	[Category("Appearance")]
	public bool PagedNavigation { get { return _pagedNavigation; } set { _pagedNavigation = value; } }
	[Category("Appearance")]
	public string HomeLinkTitle { get { return _homeLinkTitle; } set { _homeLinkTitle = value; } }
	[Category("Appearance")]
	public string ContainerCssClass { get { 
		if(String.IsNullOrEmpty(_containerCssClass))
			return null;
		else
			return _containerCssClass;
	} set { _containerCssClass = value; } }
			
	[Category("Columns and Grouping")]
	public int GroupSize { get { return _groupSize; } set { _groupSize = value; } }
	
	// Behavior
	[Category("Behavior")]
	public bool AutoActivate { get { return _autoActivate; } set { _autoActivate = value; } }
	[Category("Behavior")]
	public string AutoActivateTitleID { get { return _autoActivateTitleID; } set { _autoActivateTitleID = value; } }
	[Category("Behavior")]
	public int ShowAllChildrenUnderNode { get { return _showChildrenAt; } set { _showChildrenAt = value; } }
	[Category("Behavior")]
	public bool OpenExternalLinksInNewWindow { get { return _openExternal; } set { _openExternal = value; } }
	
	// Breadcrumbs
	[Category("Breadcrumbs")]
	public string BreadcrumbSeparator { get { return _breadcrumbSeparator; } set { _breadcrumbSeparator = value; } }
	[Category("Breadcrumbs")]
	public bool ShowBreadcrumbHomeLink { get { return _showBreadcrumbHomeLink; } set { _showBreadcrumbHomeLink = value; } }
	
	// Sub Navigation
	[Category("Sub Navigation")]
	public bool ShowTopLevelNavigation { get { return _showTopLevelNav; } set { _showTopLevelNav = value; } }
	[Category("Sub Navigation")]
	public int StartingTreeDepth { get { return _startNavDepth; } set { _startNavDepth = value; } }
	
	// Main Navigation
	[Category("Main Navigation")]
	public bool LoadNavWithJQuery { get { return _loadNavWithJQuery; } set { _loadNavWithJQuery = value; } }
	[Category("Main Navigation")]
	public string MainNavCollectionID { get { return _mainNavCol; } set { _mainNavCol = value; } }

	[Category("Main Navigation")]
	public bool UpdateLinkControl { get { return _overwriteMainTitle; } set { _overwriteMainTitle = value; } }
	
	// Select Box
	[Category("Select Box")]
	public string DefaultText { get { return _defaultText; } set { _defaultText = value; } }
	[Category("Select Box")]
	public bool ShowLandingPageLink { get { return _landingPageLink; } set { _landingPageLink = value; } }

	
	void GenerateNavigation() {
		
		if (siteMapProvider.RootNode.HasChildNodes) {

			string tempUrl = Request.ApplicationPath.ToLower();		
			// Remove language path from Sitefinity path prefix
			if (UseSitefinityLocalization) {
				ApplicationPath = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() + "/" + tempUrl.ToLower();
				CurrentPath = Request.Path.ToLower().Replace(System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() + "/", "");
			}
			else {
				ApplicationPath = tempUrl.ToLower();
				
				// I put this code in for one project, not sure about making it permanent, hence the logical skip.
				if (1==0 && ApplicationPath != "/") {   
					CurrentPath = Request.Path.ToLower().Replace(ApplicationPath, "");
					ApplicationPath = "/";
				}
				else
					CurrentPath = Request.Path.ToLower();
			}
			if (UseFullPath)
				pathPrefix = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host;
						
			// ApplicationPath = Request.ApplicationPath.ToLower();
			
			
			if (this.Page.Items[siteMapProvider + "_AutoNav"] != null) {
				localSiteMap = (SiteMapNode)(this.Page.Items[siteMapProvider + "_AutoNav"]);
			}
			else {
				localSiteMap = siteMapProvider.RootNode.Clone();
				CopySiteMap(localSiteMap, siteMapProvider.RootNode.ChildNodes);

				MarkCurrentNodes(localSiteMap.ChildNodes);
				if (!_foundInNav && AutoActivate) {
					TryAutoHighlight(localSiteMap.ChildNodes);
				}
				ActivateParentNodes(localSiteMap.ChildNodes);
				this.Page.Items.Add(siteMapProvider + "_AutoNav", localSiteMap);
			}
			if(RenderingMode == RenderingModeOptions.SubNavigation) RenderingMode = RenderingModeOptions.TreeView;
			
			switch (RenderingMode) {
				case RenderingModeOptions.TreeView:
					navigation = RenderTree(localSiteMap, _startNavDepth);
					break;
				case RenderingModeOptions.BreadCrumbs:
					MakeBreadCrumbs();
					break;
				case RenderingModeOptions.Sitemap:
					navigation = RenderTree(localSiteMap, _startNavDepth);
					break;
				case RenderingModeOptions.MainNavigation:
					_useMainNav = true;
					GetMainNavSettings();
					navigation = RenderTree(localSiteMap, _startNavDepth);
					break;
				case RenderingModeOptions.SelectBox:
					MakeSelectControl();
					break;
			}
			if (!(RenderingMode == RenderingModeOptions.MainNavigation && !String.IsNullOrEmpty(_mainNavCol) && !_pagedNavigation))
				output.Controls.Add(navigation);

			if (DebugMode) {
				HtmlGenericControl navContainer = new HtmlGenericControl("div");
				if (DebugMode) {
					navContainer.Attributes["style"] = "display: none;";
					navContainer.Attributes["class"] = "debug-fullnav";
				}
				
				RenderingMode = RenderingModeOptions.Sitemap;
				_startNavDepth = 1;
				navigation = RenderTree(localSiteMap, _startNavDepth);
				navigation.Attributes["data-source"] = "custom-sitemap";
				navContainer.Controls.Add(navigation);
				navigation = RenderTree(siteMapProvider.RootNode, 1);
				navigation.Attributes["data-source"] = "orig-sitemap";
				navContainer.Controls.Add(navigation);
				output.Controls.Add(navContainer);
				
			}
		}
		
	}

	public static object GetNewObject(Type t) {
		try {
			return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
		}
		catch {
			return null;
		}
	}
	
	public static object FollowPropertyPath(object value, string path) {
		Type currentType = value.GetType();

		foreach (string propertyName in path.Split('.')) {
			PropertyInfo property = currentType.GetProperty(propertyName);
			value = property.GetValue(value, null);
			currentType = property.PropertyType;
		}
		return value;
	}
	private void GetMainNavSettings() {
		MainNavs = new Dictionary<string, object>();
		if (String.IsNullOrEmpty(_mainNavCol)) {
			MainNavSettings = MainNavConfiguration.GetConfig();
			foreach (MainNavConfigurationState menu in MainNavSettings.Menus) {
				MainNavs.Add(AbsolutePath(menu.URL), menu);
			}
		}
		else {
			mainNavControl = FindControlRecursively(this.Page.Form.Controls, _mainNavCol.Trim());
			if (mainNavControl != null)
			{
				GetMainLinks(mainNavControl.Controls);
				
			}
		}
		FindMainNavs(localSiteMap, 1);
		 //output.Controls.Add(new LiteralControl("MainNavs: " + mainNavsInSitemap.Count + " found: <br />"));
	}
	
	private void FindMainNavs(SiteMapNode parentNode, int navDepth) {
		foreach (SiteMapNode node in parentNode.ChildNodes) {
			//output.Controls.Add(new LiteralControl("Node URL: " + node["absoluteURL"] + " <br />"));
			if (MainNavs.ContainsKey(node["absoluteURL"]) && !mainNavPathMatches.Contains(node["absoluteURL"])) {
				mainNavsInSitemap.Add(node, navDepth);
				mainNavPathMatches.Add(node["absoluteURL"]);
				if(MainNavs[node["absoluteURL"]] is MainNavConfigurationState)
					node["id"] = ((MainNavConfigurationState)MainNavs[node["absoluteURL"]]).MenuID;
			}
			if (node.HasChildNodes)
				FindMainNavs(node, navDepth + 1);
		}
	}
	
	public static Control FindControlRecursively(ControlCollection controls, string controlID) {
		if (controlID == null || controls == null)
			return null;

		foreach (Control c in controls) {
			if (c.ID == controlID)
				return c;

			if (c.HasControls()) {
				Control inner = FindControlRecursively(c.Controls, controlID);
				if (inner != null)
					return inner;
			}
		}
		return null;
	}
	
	private void GetMainLinks(ControlCollection ctrls) {
		foreach (Control ctl in ctrls) {
			if (ctl is HyperLink) {
				if (!MainNavs.ContainsKey(AbsolutePath(((HyperLink)ctl).NavigateUrl))) {
					MainNavs.Add(AbsolutePath(((HyperLink)ctl).NavigateUrl), ctl);
					  //output.Controls.Add(new LiteralControl("Found a link: " +AbsolutePath(((HyperLink)ctl).NavigateUrl) + " <br />"));
				}
			}
			if (ctl is HtmlAnchor) {
				if (!MainNavs.ContainsKey(AbsolutePath(((HtmlAnchor)ctl).HRef))) {
					MainNavs.Add(AbsolutePath(((HtmlAnchor)ctl).HRef), ctl);
					  //output.Controls.Add(new LiteralControl("Found a link: " + AbsolutePath(((HtmlAnchor)ctl).HRef) + " <br />"));
				}
			}
			if (ctl.HasControls())
				GetMainLinks(ctl.Controls);
		}
	}
	private string AbsolutePath(string url) {
		if (url.IndexOf("://", 0, (url.Length < 9 ? url.Length : 9)) > -1)
		{
			return url;
		}
		else
		{
			string nodeUrl;
			int queryPos = url.IndexOf("?");
			
			if (queryPos > -1) {
				nodeUrl = System.Web.VirtualPathUtility.ToAbsolute(url.Substring(0, queryPos)) + url.Substring(queryPos);
			}
			else {
				nodeUrl = System.Web.VirtualPathUtility.ToAbsolute(url);
			}

			// Remove language path from Sitefinity path prefix
			if (UseSitefinityLocalization)
				nodeUrl = nodeUrl.ToLower().Replace(System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() + "/", "");
			else
				nodeUrl = nodeUrl.ToLower();

			return nodeUrl;
		}
	}
	
	private void CopySiteMap(SiteMapNode parentNode, SiteMapNodeCollection nodes) {
		int currentNode;
		parentNode.ChildNodes = new SiteMapNodeCollection();
		SiteMapNode newNode;
		bool showNode = true;
		foreach (SiteMapNode node in nodes) {
			// Problem probably with object locking.
			if (String.IsNullOrEmpty(node["absoluteURL"]) || node["absoluteURL"] == null)
			{
				try //cambio
				{
					node["absoluteURL"] = AbsolutePath(node.Url);
				}
				catch { }
			}
			// Use Sitefinity's navigation hiding without requiring Sitefinity for this component
			try
			{
				object testNode = node.Clone(false);

				Type t = testNode.GetType();
				
				PropertyInfo p = t.GetProperty("ShowInNavigation");
				if (p == null)
				{
					showNode = true;
				}
				else
				{
					object source = p.GetValue(testNode, null);
					showNode = (bool)source;
				}
			}
			catch
			{
				showNode = true;
			}
			if (showNode && node != null) {
				newNode = new SiteMapNode(node.Provider, node.Key, node.Url, node.Title, node.Description);
				newNode.Roles = node.Roles;
				newNode["absoluteURL"] = AbsolutePath(node.Url);
				// newNode = node.Clone();
				newNode.ParentNode = parentNode;
				currentNode = parentNode.ChildNodes.Add(newNode);
				if (node.HasChildNodes) {
					CopySiteMap(parentNode.ChildNodes[currentNode], node.ChildNodes);
				}
			}
		}

	}
	private void MakeSelectControl() {
		navigation = new HtmlGenericControl("div");
		navigation.Attributes["class"] = ContainerCssClass ?? "select-navigation";
		DropDownList ddl = new DropDownList();
		ListItem item;
		ddl.Attributes["onchange"] = "window.location.href=this.options[this.selectedIndex].value;";
		if (_defaultText != String.Empty)
			ddl.Items.Add(new ListItem(_defaultText, ""));
		foreach (SiteMapNode node in localSiteMap.ChildNodes) {
			if (node["active"] == "1" && node.HasChildNodes) {
				foreach (SiteMapNode innernode in node.ChildNodes) {
					if (!(innernode.ParentNode.ChildNodes.IndexOf(innernode) == 0 && !ShowLandingPageLink)) {
						item = new ListItem(innernode.Title, innernode["absoluteURL"].ToString());
						if (innernode["active"] == "1") item.Selected = true;
						ddl.Items.Add(item);
					}
				}
			}
		}
		navigation.Controls.Add(ddl);
	}
	private void MakeBreadCrumbs() {
		navigation = new HtmlGenericControl("div");
		navigation.Attributes["class"] = ContainerCssClass ?? "breadcrumbs";
		HyperLink link;
		
		breadcrumbs = new List<HyperLink>();
		if (ShowBreadcrumbHomeLink) {
			link = new HyperLink();
			link.NavigateUrl = "~/";
			if(HomeLinkTitle != null)
				link.Text = HomeLinkTitle;
			else
				link.Text = localSiteMap.Title;
			breadcrumbs.Add(link);
		}
		
		BuildBreadCrumbs(localSiteMap.ChildNodes);
		if (!_foundInNav) {
			link = new HyperLink();
			link.NavigateUrl = CurrentPath;
			link.Text = GetPageTitle(this.Page.Controls);
			breadcrumbs.Add(link);
		}
		 // output.Controls.Add(new LiteralControl("PageTitle: " + GetPageTitle(this.Page.Controls)));
		
		for (int i = 0; i < breadcrumbs.Count; i++) {
			link = breadcrumbs[i];
			HtmlGenericControl span;
			if (i != breadcrumbs.Count - 1) {
				navigation.Controls.Add(link);
				span = new HtmlGenericControl("span");
				span.Attributes["class"] = "separator";
				span.InnerHtml = _breadcrumbSeparator;
				navigation.Controls.Add(span);
			}
			else {
				span = new HtmlGenericControl("span");
				span.InnerHtml = link.Text;
				navigation.Controls.Add(span);
			}
		}
		
	}
	private void BuildBreadCrumbs(SiteMapNodeCollection nodes) {
		HyperLink link;
		foreach (SiteMapNode node in nodes) {
			if (node["active"] == "1") {
				link = new HyperLink();
				link.NavigateUrl = node.Url;
				link.Text = HttpUtility.HtmlEncode(node.Title);
				breadcrumbs.Add(link);
				if (node["absoluteURL"] == CurrentPath) {
					_foundInNav = true;
				}
			}
			if(node.HasChildNodes)
				BuildBreadCrumbs(node.ChildNodes);			
		}
	}
	private string GetPageTitle(ControlCollection ctrls) {
		string result;

		foreach (Control ctl in ctrls) {
		
			if(ctl is HtmlContainerControl) {
				if (ctl.ID == _autoActivateTitleID) {
					return ((HtmlContainerControl)ctl).InnerText;
				}
			}
			
			if (ctl.HasControls()) {
				result = GetPageTitle(ctl.Controls);
				if (result != "")
					return result;
			}
		}
		return "";
	}

	
	private void TryAutoHighlight(SiteMapNodeCollection nodes) {
		// Treats top pages as possible directory names and performs a text compare a la Sitefinity
		AutoHighlight(nodes, ".", "/");
		
		if (!_foundInNav) {
			AutoHighlight(nodes, "/", "");
		}
		if(MostSpecificNode != null)
			MostSpecificNode["active"] = "1";
	}

	private void AutoHighlight(SiteMapNodeCollection nodes, string findChar, string replaceChar) {
		string compareUrl;
		foreach (SiteMapNode node in nodes) {
			if (node["absoluteURL"].IndexOf(findChar) > -1) {
				if(replaceChar == "")
					compareUrl = node["absoluteURL"].Substring(0, node["absoluteURL"].LastIndexOf(findChar) + 1);
				else
					compareUrl = node["absoluteURL"].Substring(0, node["absoluteURL"].LastIndexOf(findChar)) + replaceChar;
			}
			else
				compareUrl = node["absoluteURL"];
			// output.Controls.Add(new LiteralControl("Char '" + findChar + "' in: " + compareUrl + "<br />CurrentPath: " + CurrentPath));
			
			if (compareUrl != ApplicationPath &&
				CurrentPath.StartsWith(compareUrl)) {
				_foundInNav = true;
				autoHighlight = true;

				int matchLength = CurrentPath.Length - CurrentPath.Replace(compareUrl, "").Length;
				 // output.Controls.Add(new LiteralControl("MatchLength: " + matchLength));
				
				// change in 1.9.2 -- based on number of characters matched rather than just URL length, which is more logical.
				if (matchLength > MostSpecificNodeLength) {
					MostSpecificNode = node;
					MostSpecificNodeLength = matchLength;
					 // output.Controls.Add(new LiteralControl("Char '" + findChar + "' found: " + compareUrl + "<br />CurrentPath: " + CurrentPath ));
				}
				
			}
			if (node.HasChildNodes)
				AutoHighlight(node.ChildNodes, findChar, replaceChar);
		}
	}

	private void ActivateParentNodes(SiteMapNodeCollection nodes) {
		SiteMapNode currentNode;
		foreach (SiteMapNode node in nodes) {
			if (node["active"] == "1") {
				currentNode = node.ParentNode;
				while (currentNode != localSiteMap) {
					currentNode["active"] = "1";
					currentNode = currentNode.ParentNode;
				}
			}
			if (node.HasChildNodes) {
				ActivateParentNodes(node.ChildNodes);
			}
		}
	}
	
	
	private void MarkCurrentNodes(SiteMapNodeCollection nodes) {
		foreach (SiteMapNode node in nodes) {
			if (node["absoluteURL"] == CurrentPath) {
				node["active"] = "1";
				_foundInNav = true;
			}
			else {
				node["active"] = "0";
			}
			if (node.HasChildNodes) {
				MarkCurrentNodes(node.ChildNodes);
			}			
		}
	}
	private SiteMapNode GetNode(SiteMapNode node, string goToTree, int charPos) {
		SiteMapNode tmpNode;
	
		if (node.HasChildNodes) {
			try {
				tmpNode = node.ChildNodes[Int32.Parse(goToTree.Substring(charPos, 2))];
				_navDepth++;
				if (goToTree.Length != charPos - 2)
					tmpNode = GetNode(tmpNode, goToTree, charPos + 2);
				
				return tmpNode;
			}
			catch {
				return node;
			}
		}
		else {
			return node;
		}
		
	}
	private HtmlGenericControl RenderTree(SiteMapNode parentNode, int navDepth) {
		if (_startNavDepth > navDepth) {
			foreach (SiteMapNode node in parentNode.ChildNodes) {
				if (node["active"] == "1")
					return RenderTree(node, navDepth + 1);	
			}
		}
			
		HtmlGenericControl navContainer = new HtmlGenericControl("div");

		string goToTree = "";
	   
		if ((_pagedNavigation && RenderingMode == RenderingModeOptions.MainNavigation && !String.IsNullOrEmpty(Request.QueryString["gototree"]))
			|| (_goToTree != "")) {
		
			goToTree = _goToTree;
			if (!String.IsNullOrEmpty(Request.QueryString["gototree"])) {
				goToTree = (string)Request.QueryString["gototree"];
			}
			// Will break if home page is not first item - should fix
			if (goToTree == "00") goToTree = "";
			if (goToTree != "") {
				SiteMapNode tmpNode = GetNode(parentNode, goToTree, 0);
				if (tmpNode != parentNode) {
					HyperLink link = new HyperLink();
					string backNav = goToTree.Substring(0,goToTree.Length-2);
					if(backNav == "") backNav = "00";
					if (_pagedNavigation) {
						link.NavigateUrl = Request.Path + "?gototree=" + backNav;
						link.Text = HttpUtility.HtmlEncode(tmpNode.Title);
						link.CssClass = "btn-general back-button";
						navContainer.Controls.Add(link);
						_maxDepth = navDepth = _navDepth;						
					}
				}
				parentNode = tmpNode;
				usePaged = true;
				
			}
		}
		
		
		if(this.RenderingMode == RenderingModeOptions.Sitemap)
			navContainer.Attributes["class"] = ContainerCssClass ?? "sitemap";
		
		if (RenderingMode == RenderingModeOptions.MainNavigation) {
			navContainer.Attributes["class"] = ContainerCssClass ?? "main-nav";
			if (usePaged)
			{
				navContainer.Controls.Add(RenderChildNodes(parentNode, navDepth, goToTree));
				if (mainNavControl != null) {
					//mainNavControl.Controls.Add(RenderChildNodes(parentNode, navDepth, goToTree));
					mainNavControl.Visible = false;
				}
			  
				
			}
			else
			{
				
				HtmlGenericControl ul = new HtmlGenericControl("ul");
				HtmlGenericControl li;

				string activeClass = "";
				foreach (KeyValuePair<SiteMapNode, int> kvp in mainNavsInSitemap)
				{

					if (String.IsNullOrEmpty(_mainNavCol))
					{
						// Should be refactored with RenderTree to share the same rendering code.			

						if (kvp.Key["active"] == "1" || (kvp.Key.ParentNode != localSiteMap && kvp.Key.ParentNode["active"] == "1"))
							activeClass = "active ";
						else
							activeClass = "";
						li = new HtmlGenericControl("li");
						li.Attributes["class"] = activeClass + "L1";
						li.Attributes["id"] = kvp.Key["id"];

						HyperLink link = new HyperLink();
						link.NavigateUrl = kvp.Key["absoluteURL"];
						link.CssClass = activeClass + "L1";
						link.Text = LinkHTMLTemplate.Replace("{1}", kvp.Key.Title);
						li.Controls.Add(link);
						if (kvp.Key.HasChildNodes && (kvp.Value + 1 < _maxDepth || _maxDepth == 0))
							li.Controls.Add(RenderChildNodes(kvp.Key, kvp.Value + 1, goToTree));
						ul.Controls.Add(li);
					}
					else
					{
						object mainNav = MainNavs[kvp.Key["absoluteURL"]];

						// This if statement should be compressed / abstracted into one.
						string thisNodeCode = goToTree + kvp.Key.ParentNode.ChildNodes.IndexOf(kvp.Key).ToString("D2");
						
						if (mainNav is HtmlAnchor)
						{
							HtmlAnchor anc = (HtmlAnchor)mainNav;
							if (kvp.Key["active"] == "1")
								anc.Attributes["class"] = (string)(anc.Attributes["class"] + " active").Trim();

							anc.HRef = LocalPath(AbsolutePath(anc.HRef), kvp.Key, kvp.Key.ParentNode.ChildNodes.IndexOf(kvp.Key).ToString("D2"));
							
							if (kvp.Key.HasChildNodes && (kvp.Value < _maxDepth || _maxDepth == 0))
								anc.Parent.Controls.Add(RenderChildNodes(kvp.Key, kvp.Value + 1, thisNodeCode));
							if (_overwriteMainTitle)
								anc.InnerHtml = LinkHTMLTemplate.Replace("{1}", kvp.Key.Title);

							// Mainnav TopLevelHtmlTemplate
							if (_topLevelHTMLTemplate != string.Empty)
							{
								Literal litTitle = new Literal();
								litTitle.Text = _topLevelHTMLTemplate.Replace("{1}", anc.InnerText);
								anc.Parent.Controls.AddAt(anc.Parent.Controls.IndexOf(anc)+1, litTitle);
								anc.Attributes["style"] = "display:none";
							}
							// End Mainnav TopLevelHtmlTemplate
						}
						if (mainNav is HyperLink)
						{
							HyperLink anc = (HyperLink)mainNav;
							if (kvp.Key["active"] == "1")
								anc.CssClass = (string)(anc.CssClass + " active").Trim();

							anc.NavigateUrl = LocalPath(AbsolutePath(anc.NavigateUrl), kvp.Key, kvp.Key.ParentNode.ChildNodes.IndexOf(kvp.Key).ToString("D2"));

							if (kvp.Key.HasChildNodes && (kvp.Value < _maxDepth || _maxDepth == 0))
								anc.Parent.Controls.Add(RenderChildNodes(kvp.Key, kvp.Value + 1, thisNodeCode));
							if (_overwriteMainTitle)
								anc.Text = LinkHTMLTemplate.Replace("{1}", kvp.Key.Title);

							// Mainnav TopLevelHtmlTemplate
							if (_topLevelHTMLTemplate != string.Empty)
							{
								Literal litTitle = new Literal();
								litTitle.Text = _topLevelHTMLTemplate.Replace("{1}", anc.Text);
								anc.Parent.Controls.AddAt(anc.Parent.Controls.IndexOf(anc)+1, litTitle);
								anc.Attributes["style"] = "display:none";
							}
							// End Mainnav TopLevelHtmlTemplate
						}
					}
				}
				if (String.IsNullOrEmpty(_mainNavCol))
				{
					navContainer.Controls.Add(ul);
				}
			}
		}
		else {
				
			if (RenderTopLevelAsLinks)
				navContainer.Controls.Add(RenderChildNodes(parentNode, navDepth, goToTree));
			else {

				foreach (SiteMapNode node in parentNode.ChildNodes) {
					string thisNodeCode = goToTree + parentNode.ChildNodes.IndexOf(node).ToString("D2");
					if (RenderingMode == RenderingModeOptions.Sitemap ||
						(RenderingMode == RenderingModeOptions.TreeView &&
						(ShowTopLevelNavigation || node["active"] == "1"))) {
						if(TopLevelHTMLTemplate.IndexOf("{}") == -1) 
							navContainer.Controls.Add(new LiteralControl(TopLevelHTMLTemplate.Replace("{1}", HttpUtility.HtmlEncode(node.Title))));
						navContainer.Controls.Add(RenderChildNodes(node, navDepth + 1, thisNodeCode));
					}
				}
			}
		}
		return navContainer;

	}
	private HtmlGenericControl RenderChildNodes(SiteMapNode parentNode, int navDepth) {
		return RenderChildNodes(parentNode, navDepth, _navCode);
	}
	private string LocalPath(string url, SiteMapNode node, string thisNodeCode) {
		if (_pagedNavigation && node.HasChildNodes) {
			return Request.Path + "?gototree=" + thisNodeCode;
		}
		else {
			// Add language path from Sitefinity path prefix
			if (!node.Url.StartsWith("http"))
			{
				if (UseSitefinityLocalization)
					return pathPrefix + "/" + System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() + url;
				else
					return pathPrefix + url;
			}
			else
			{
				return url;
			}
		}
	}
	private HtmlGenericControl RenderChildNodes(SiteMapNode parentNode, int navDepth, string navCode) {
		int loops = 1;
		bool groupLists = (navDepth == 2 && GroupSize != 0);
		if (groupLists) {
			loops = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(parentNode.ChildNodes.Count)/Convert.ToDouble(GroupSize)));
			
			
		}
		string levelClass = "L" + navDepth.ToString() + " ";

		HtmlGenericControl outerContainer = new HtmlGenericControl("ul");
		outerContainer.Attributes["class"] = levelClass + "nav-outer";
		
		for (int i = 0; i < loops; i++) {
			HtmlGenericControl container = new HtmlGenericControl("li");
			if (groupLists) {
				container.Attributes["class"] = levelClass + "nav-inner " + "group-" + (i + 1);
			}
			
			HtmlGenericControl subnav;
			if (loops == 1)
				subnav = outerContainer;
			else
				subnav = new HtmlGenericControl("ul");
			
			StringBuilder liClassName;

			StringBuilder ulClassName = new StringBuilder(_navListClass + " " + levelClass);
			if (parentNode["active"] == "1") {
				ulClassName.Append(_activeClass);
			}
			subnav.Attributes.Add("class", ulClassName.ToString().Trim());
			
			HtmlGenericControl li;
			HyperLink link;
			bool isFirstChild = false;
			string thisNodeCode = "";
			int nodeCount = -1;

			int lowerLimit = groupLists ? GroupSize * i : 0;
			int upperLimit = groupLists ? GroupSize * (i + 1) : parentNode.ChildNodes.Count;
			if (upperLimit > parentNode.ChildNodes.Count || upperLimit == 0) upperLimit = parentNode.ChildNodes.Count;
			
			for (int n = lowerLimit; n < upperLimit; n++) {
					SiteMapNode node = parentNode.ChildNodes[n];
					string nodeURL = node["absoluteURL"];
					//if (UseSitefinityLocalization)
					//	nodeURL = node.Url.ToLower().Replace(System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower() + "/", "");
					//else
					//	nodeURL = node.Url.ToLower();

					// Remove this later?  (_useMainNav && (navDepth != 1 || MainNavs.ContainsKey(nodeURL))) ||
					if ((RenderingMode == RenderingModeOptions.TreeView &&
						(ShowTopLevelNavigation || navDepth != _startNavDepth ||
						(navDepth == _startNavDepth && node["active"] == "1"))) ||
						(_useMainNav && (navDepth != 1 || MainNavs.ContainsKey(nodeURL))) ||
						RenderingMode == RenderingModeOptions.Sitemap) {

						thisNodeCode = navCode + parentNode.ChildNodes.IndexOf(node).ToString("D2");
						nodeCount++;

						if (nodeCount == 0)
							isFirstChild = true;
						else
							isFirstChild = false;

						if (_pagedNavigation && isFirstChild) {
							if (navDepth != 1 && node["absoluteURL"] != parentNode["absoluteURL"]) {
								li = new HtmlGenericControl("li");
								link = new HyperLink();
								link.NavigateUrl = parentNode.Url;
								link.Text = LinkHTMLTemplate.Replace("{1}",HttpUtility.HtmlEncode(parentNode.Title));
								
								if (parentNode["active"] == "1") {
									li.Attributes.Add("class", "first-child " + levelClass + _activeClass);
									link.CssClass = levelClass + _activeClass;
								}
								else {
									li.Attributes.Add("class", "first-child " + levelClass + _inactiveClass);
									link.CssClass = levelClass + _inactiveClass;
								}
								li.Controls.Add(link);
								subnav.Controls.Add(li);
								isFirstChild = false;
							}
							if (navDepth == 1) {
								li = new HtmlGenericControl("li");
								link = new HyperLink();
								link.NavigateUrl = "~/";
								li.Attributes.Add("class", "first-child " + levelClass + _inactiveClass);
								if (HomeLinkTitle != null)
									link.Text = LinkHTMLTemplate.Replace("{1}",HomeLinkTitle);
								else
									link.Text = LinkHTMLTemplate.Replace("{1}", localSiteMap.Title);
								link.CssClass = levelClass.Trim();
								li.Controls.Add(link);
								subnav.Controls.Add(li);
								isFirstChild = false;
							}
						}
						liClassName = new StringBuilder();

						li = new HtmlGenericControl("li");
						link = new HyperLink();

						if (_useMainNav && MainNavs.ContainsKey(nodeURL) && navDepth == 1) {
							if(MainNavs[nodeURL] is MainNavConfigurationState)
							li.Attributes["id"] = ((MainNavConfigurationState)MainNavs[nodeURL]).MenuID;

						//	if (MainNavs.Count == nodeCount + 1)
						//		liClassName.Append("last-child ");
						}
						else {
						//	if (parentNode.ChildNodes.IndexOf(node) == parentNode.ChildNodes.Count - 1)
						//		liClassName.Append("last-child ");
						}
						if(n == upperLimit-1)
							liClassName.Append("last-child ");
						if (isFirstChild)
							liClassName.Append("first-child ");


						if (node.HasChildNodes) {
							liClassName.Append(_moreClass + " ");
							link.CssClass = _moreClass;
						}
						liClassName.Append(levelClass);

						link.NavigateUrl = LocalPath(node["absoluteURL"], node, thisNodeCode);
						
						link.Text = LinkHTMLTemplate.Replace("{1}", HttpUtility.HtmlEncode(node.Title));
						
						// link.ToolTip = node["absoluteURL"].Substring(0, node["absoluteURL"].LastIndexOf("."));

						if (RenderingMode != RenderingModeOptions.Sitemap) {
							if (node["active"] == "1") {
								liClassName.Append(_activeClass);
								link.CssClass = ((string)(link.CssClass + " " + levelClass + _activeClass)).Trim();
							}
							else {
								liClassName.Append(_inactiveClass);
								link.CssClass = ((string)(link.CssClass + " " + levelClass)).Trim();
							}
						}

						if (OpenExternalLinksInNewWindow && node.Url.StartsWith("http")) {
							link.Target = "_blank";
						}
						li.Attributes.Add("class", liClassName.ToString().Trim());
						
						li.Controls.Add(link);

						if (node.HasChildNodes
							&& (_maxDepth == 0 || navDepth < _maxDepth)
							&& (RenderingMode == RenderingModeOptions.Sitemap ||
							RenderingMode == RenderingModeOptions.MainNavigation ||
							(RenderingMode == RenderingModeOptions.TreeView &&
							(node["active"] == "1" || (_showChildrenAt != 0 && navDepth >= _showChildrenAt))))) {
							
								li.Controls.Add(RenderChildNodes(node, navDepth + 1, thisNodeCode));
							
						}
						subnav.Controls.Add(li);
					}

				}
			if (groupLists) {
				container.Controls.Add(subnav);
				outerContainer.Controls.Add(container);
								
			}
			else {
				// outerContainer.Controls.Add(subnav);
			}
		}
		
		if (_loadNavWithJQuery && !usePaged && RenderingMode == RenderingModeOptions.MainNavigation && navDepth == 2) {
			StringBuilder sb = new StringBuilder();
			StringWriter tw = new StringWriter(sb);
			HtmlTextWriter hw = new HtmlTextWriter(tw);
			outerContainer.RenderControl(hw);
			string controlText = sb.ToString();
			string nodeID = "nodeID-" + navCode;
			string customScript = "jQuery('." + nodeID + "').replaceWith('" + controlText.Replace("'", "\\'") +
				"'); ";

			Page.ClientScript.RegisterStartupScript(this.GetType(), "script_" + nodeID, customScript, true);
			
			HtmlGenericControl lateNav = new HtmlGenericControl("div");
			lateNav.Attributes["class"] = nodeID;
				
			return lateNav;
		}
		else {
			return outerContainer;
		}
	}
</script>

<asp:PlaceHolder ID="output" runat="server" />
