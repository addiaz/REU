using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCR.Residencias.Interfaz
{
    public partial class MasterAdministrador : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Page.Title = "TEC - " + Global.gSubTituloPagina;

                if (Session == null)
                {
                    Response.Redirect("frmAutenticacion.aspx", true);
                }

                //si la sesión es válida crea el menu
                if (Session["ID_USUARIO"] != null)
                {
                    lblCuentaUsuario.Text = Session["ID_USUARIO"].ToString();
                }
                else
                {
                    Response.Redirect("frmAutenticacion.aspx");
                }
            }
        }
    }
}