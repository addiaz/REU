using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCR.Residencias.Interfaz
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //lblTitulo.Text = Global.gTituloPagina;
                //lblTitulo.Text = Global.gSubTituloPagina;
                Page.Title = "TEC - " + Global.gSubTituloPagina;

                //TODO: esto es temporal, en etapa de desarrollo
                //wsSeguridad.SeguridadSoapClient wsseg = new wsSeguridad.SeguridadSoapClient();
                //Session.Add("ID_USUARIO", "csanabria");
                //Session.Add("NUM_CEDULA", wsseg.ObtenerCedula("csanabria")); //obtener número de cédula si tiene.
                //Session.Add("NOM_USUARIO", wsseg.ObtenerNombreUsuario("csanabria")); //obtener nombre completo del usuario.
                //Session.Add("COD_SEDE", "CA");
                //hasta aqui...

                if (Session == null)
                {
                    Response.Redirect("frmAutenticacion.aspx", true);
                }

                //si la sesión es válida crea el menu
                if (Session["ID_USUARIO"] != null)
                {
                    CrearMenu(Session["COD_SEDE"].ToString(), Global.gCOD_APLICACION, Session["ID_USUARIO"].ToString());
                    lblCuentaUsuario.Text = Session["ID_USUARIO"].ToString();
                    //CrearMenu("CA", 28, "csanabria");
                }
                else
                {
                    Response.Redirect("frmAutenticacion.aspx");
                }
            }
            else{
                CrearMenu(Session["COD_SEDE"].ToString(), Global.gCOD_APLICACION, Session["ID_USUARIO"].ToString());
            }
        }

        private void CrearMenu(string pSede, int pCodAplicacion, string pLogin)
        {
            try
            {
                if (Session["ID_USUARIO"] != null)
                {
                    if (Session["ID_USUARIO"].ToString() != "[Ingreso]")
                    {
                        DataTable dt;
                        DataSet ds;
                        wsSeguridad.SeguridadSoapClient wsseg = new wsSeguridad.SeguridadSoapClient();
                        ds = wsseg.ObtenerMenuUsuario(Global.gCOD_APLICACION, Session["ID_USUARIO"].ToString(), Session["COD_SEDE"].ToString());
                        dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            Crea_menu(dt);
                        }
                        
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("ObjetoError", ex);
                cUtilInterfaz.AgregarCodError(this.Page);
            }
        }

        private void Crea_menu(DataTable dt)
        {
            string _Arreglo = "[";
            object[] Row;
            if (dt.Rows.Count > 0)
            {
                int _filas = dt.Rows.Count;
                for (int _a = 0; _a < _filas; _a++)
                {
                    Row = dt.Rows[_a].ItemArray;
                    _Arreglo += "[";
                    for (int _b = 0; _b < Row.Length; _b++)
                    {
                        _Arreglo += "'" + Row[_b].ToString() + "',";
                    }
                    _Arreglo = _Arreglo.Remove(_Arreglo.Length - 1);
                    _Arreglo += " ],";
                }
            }
            _Arreglo = _Arreglo.Remove(_Arreglo.Length - 1);
            _Arreglo += "]";

            if (_Arreglo != "")
            {

                if (!Page.ClientScript.IsStartupScriptRegistered("Menu"))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Menu", "<script type=\"text/javascript\"> CreaMenu(" + _Arreglo + ");</script>");
                }
            }
            //else
            //{
            //    Response.Redirect("frmAutenticacion.aspx");
            //}
        }

        //private void AgregarNodo(DataRow FilaActual, DataTable TablaCompleta, MenuItem NodoPadre)
        //{
        //    try
        //    {
        //        // Función recursiva que agrega un nodo y sus hijos al arbol
        //        DataRow[] FilasHijas;
        //        //DataRow r;
        //        MenuItem nodo;

        //        // Agregar el nodo
        //        if (NodoPadre == null)
        //        {
        //            // Es uno de los nodos raíz del arbol, agregar el nodo directamente al árbol
        //            nodo = new MenuItem(FilaActual["NOM_FUNCIONALIDAD"].ToString(), FilaActual["DES_FUNCIONALIDAD"].ToString(), null, FilaActual["URL_PAGINA"].ToString());
        //            Menu.Items.Add(nodo);
        //        }
        //        else
        //        {
        //            // Es un nodo hijo
        //            nodo = new MenuItem(FilaActual["NOM_FUNCIONALIDAD"].ToString(), FilaActual["DES_FUNCIONALIDAD"].ToString(), null, FilaActual["URL_PAGINA"].ToString());
        //            //nodo = new MenuItem(FilaActual["NOM_FUNCIONALIDAD"].ToString(), FilaActual["URL_PAGINA"].ToString(), FilaActual["DES_FUNCIONALIDAD"].ToString());
        //            NodoPadre.ChildItems.Add(nodo);
        //        }

        //        // Obtener los hijos para el nodo actual
        //        FilasHijas = TablaCompleta.Select("COD_FUNCIONALIDAD_PADRE=" + FilaActual["COD_FUNCIONALIDAD"].ToString());
        //        if (FilasHijas.Length > 0)
        //        {
        //            foreach (DataRow r in FilasHijas)
        //            {
        //                AgregarNodo(r, TablaCompleta, nodo);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Session.Add("ObjetoError", ex);
        //        cUtilInterfaz.AgregarCodError(this.Page);
        //    }
        //}

    }
}
