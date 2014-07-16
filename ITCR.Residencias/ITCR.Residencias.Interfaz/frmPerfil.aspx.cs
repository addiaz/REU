using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCR.Residencias.Interfaz
{
    public partial class frmPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label lblTexto = new Label();
            lblTexto.ForeColor = System.Drawing.Color.LightGray;


            lblNombre.InnerText = lblNombre.InnerText + " " + Session["ID_USUARIO"].ToString();
            lblApellido.InnerText = lblApellido.InnerText + " " + Session["APELLIDO"].ToString();
            lblCarne.InnerText = lblCarne.InnerText + " " + Session["CARNE_USUARIO"].ToString();
            lblCorreo.InnerText = lblCorreo.InnerText + " " + Session["CORREO"].ToString();
            lblCuarto.InnerText = lblCuarto.InnerText + " " + Session["CUARTO"].ToString();
            lblEdificio.InnerText = lblEdificio.InnerText + " " + Session["EDIFICIO"].ToString();
        }
    }
}