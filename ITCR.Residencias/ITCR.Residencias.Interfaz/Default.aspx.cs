using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace ITCR.Residencias.Interfaz
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "TEC - " + Global.gSubTituloPagina;

   
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://www.estudiantec.cr", true);
        }

        protected void lnkError_Click(object sender, EventArgs e)
        {
            Session.Add("ObjetoError", new Exception("Mensaje de error"));
            cUtilInterfaz.AgregarCodError(this);
        }
    }
}
