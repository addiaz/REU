using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using ITCR.Residencias.Negocios;

namespace ITCR.Residencias.Interfaz
{
    public partial class frmMantenimiento : System.Web.UI.Page
    {
	private int CodFuncionalidad = 0; //asignar el codigo asignado por el sistema de seguridad 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session == null)
                {
                    Response.Redirect("frmAutenticacion.aspx", true);
                }

                // el siguiente código carga los datos en pantalla
                //CargarDatos();
                //gvDatos.DataBind();
            }
            // agregar el script del lado del cliente para confirmar borrado
            cUtilInterfaz.AgregarScriptBorrar(this);
        }

        /// <summary>
        /// Método de ejemplo que implementa el cambio de página en el gridview
        /// </summary>
        //protected void gvDatos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    if (gvDatos.EditIndex != -1)    //si se está haciendo alguna edición en el grid
        //    {
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        gvDatos.PageIndex = e.NewPageIndex;
        //        CargarDatos();
        //        gvDatos.DataBind();
        //    }
        //}

        /// <summary>
        /// Metodo de ejemplo para cargar los datos del GridView
        /// </summary>
        //protected void CargarDatos()
        //{
        //    if (Session.Keys.Count == 0)
        //    {
        //        Response.Redirect("frmAutenticacion.aspx");
        //    }
        //    cSICFCentrosFuncionalesNegocios cf = new cSICFCentrosFuncionalesNegocios(Global.gCOD_APLICACION, Session["COD_SEDE"].ToString(), 1, Session["ID_USUARIO"].ToString());
        //    DataTable oDt = cf.SeleccionarTodos();
        //    gvDatos.DataSource = oDt;
        //}

        /// <summary>
        /// Metodo de ejemplo para agregar el botón de borrado en cada fila del gridview al hacer el databinding
        /// </summary>
        //protected void gvDatos_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        ImageButton btn = (ImageButton)e.Row.Cells[4].FindControl("btnEliminar");
        //        if (btn != null)
        //        {
        //            btn.Attributes.Add("onclick", "return confirmarBorrado()");
        //        }
        //    }
        //    catch { }
        //}

        /// <summary>
        /// Metodo de ejemplo para implementar el botón de búsqueda
        /// </summary>
        //protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        //{
        //    DataTable oDt = new DataTable();
        //    cSICFCentrosFuncionalesNegocios cf = new cSICFCentrosFuncionalesNegocios(Global.gCOD_APLICACION, Session["COD_SEDE"].ToString(), 1, Session["ID_USUARIO"].ToString());
        //    //es la columna llave, se puede utilizar SeleccionarUno()
        //    if (txtCol1.Text != "")
        //        cf.UNICOD = int.Parse(txtCol1.Text);
        //    // Columnas de busqueda adicionales, si es una columna de algún tipo texto debe asegurarse que la clase de datos 
        //    // le incluya los símbolos % a la cadena para que se haga la busqueda parcial (el procedimiento almacenado generado ya lo hace)
        //    if (txtCol2.Text != "")
        //        cf.DescripcionCF = txtCol2.Text;
        //    if (txtCol3.Text != "")
        //        cf.CCOSTO = Double.Parse(txtCol3.Text);
        //    oDt = cf.Buscar();

        //    gvDatos.DataSource = oDt;
        //    gvDatos.DataBind();
        //}
    }
}