using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
//using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace ITCR.Residencias.Interfaz
{

    public partial class frmAutenticacion : System.Web.UI.Page
    {
        #region Declaraciones de propiedades publicas de la clase
        //código de funcionalidad de login
        public int CodFuncionalidad = 1;

        
        

        /*
        public int TipoUsuarioSeleccionado
        {
            get
            {
                return System.Int32.Parse(this.ddlTipoUsuario.SelectedValue.ToString());
            }
            set
            {
                if ((value < 3) && (value > 0))
                    this.ddlTipoUsuario.SelectedIndex = value -1;   //selecciona el value - 1, pues los items en el ddl son base 0
                else
                {
                    Exception ex = new Exception("Valor incorrecto para el tipo de usuario. Utilice 1=Funcionario, 2=Estudiante.");
                    Session.Add("ObjetoError", ex);
                }
            }
        }
        */
        public string SedeSeleccionada
        {
            get
            {
                return "CA";
            }
            set
            {
                //eliminado el campo de sede a solicitud de Kattia 22/3/2012
            }
        }
        /*
        public string imagen
        {
            get
            {
                return imgAplicacion.ImageUrl;
            }
            set
            {
                imgAplicacion.ImageUrl = imagen;
            }
        }
        */
        public string PaginaRedireccionar;

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //cerrar sesion si ya existe una
                if (Session != null)
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                }

                // Inicializar los valores en el combo de sede
                //Titulo.Text = Global.gSubTituloPagina;
                //LlenarComboSedes(DropSede);           //eliminado a solicitud de Kattia 22/3/2012
                // Seleccionar el primer elemento en el combolist.
                //ddlTipoUsuario.SelectedIndex = 0;
                // Colocar el focus inicial en el textos del nombre de usuario
                string s = "<SCRIPT language='javascript'>document.getElementById('MainContent_txtUsuario').focus() </SCRIPT>";
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "onload", s);

                //colocar datos del usuario loggeado actualmente por default
                try
                {
                    string nombreUsuarioActual = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
                    //quitar el nombre de dominioy el \
                    int tamanoNombre = nombreUsuarioActual.Length - nombreUsuarioActual.IndexOf("\\") + 1;  //tamano completo dominio\\usuario
                    tamanoNombre -= 2; //quitarle el tamaño de \\
                    nombreUsuarioActual = nombreUsuarioActual.Substring(nombreUsuarioActual.IndexOf("\\") + 1, tamanoNombre);
                    txtUsuario.Text = nombreUsuarioActual;
                }
                catch   // no es importante si da algún error al poner el usuario actual como valor default
                {  }
            }
        }

        public static void LlenarComboSedes(DropDownList sSedes)
        {
            try
            {
                DataTable oDt;
                DataSet oDs;
                wsSeguridad.SeguridadSoapClient wsseg = new wsSeguridad.SeguridadSoapClient();
                oDs = wsseg.ObtenerListaSedes();
                oDt = oDs.Tables[0];
                sSedes.DataSource = new DataView(oDt);
                sSedes.DataTextField = "NOM_SEDE";
                sSedes.DataValueField = "COD_SEDE";
                sSedes.DataBind();
                oDt.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnEntrar_Click(object sender, ImageClickEventArgs e)
        {
            Datos.cConnectionDatos conexion = new Datos.cConnectionDatos();

            lblMensajeError.Text = "";
            if (txtUsuario.Text == "")
            {
                lblMensajeError.Text = "El nombre de usuario no puede ser vacío";
            }
            else
            {
                if (txtPassword.Text == "")
                {
                    lblMensajeError.Text = "La clave de acceso no puede ser vacía";
                }
                else
                {
                    try
                    {
                        string IdUsuario = txtUsuario.Text;
                        string Password = txtPassword.Text;
                        Session["COD_SEDE"] = "CA"; // DropSede.SelectedItem.Value;      //modificado a solicitud de Kattia 22/3/2012
                        int CodAplicacion = Global.gCOD_APLICACION;


                        if (txtUsuario.Text.CompareTo("admin") == 0)
                        {
                            if (txtPassword.Text == "123")
                            {
                                /* En caso de ser usuario administrador, redirije la pagina hacia el master de administrador */

                                /* Agrega todas las caracteristicas necesarias para la sesion */
                                
                                Session.Add("ID_USUARIO", "Administrador"); // ID de usuario
                                Session.Add("NOM_USUARIO", "admin"); // Nombre de usuario

                                FormsAuthentication.RedirectFromLoginPage(IdUsuario, true);
                                Response.Redirect("~/frmAdministrador.aspx");
                            }
                            

                        }
                        else{

                            // Usuario obtiene, en caso de existir, los datos necesarios de un usuario
                            // Para este caso es solo para estudiantes, el primer valor del vector es un bool que indica si es usuario o no.

                            List<string> Usuario = Datos.cConnectionDatos.Autentificacion(IdUsuario, Password);

                            if (Usuario[0].CompareTo("true") == 0)
                            {
                                Session.Add("CARNE_USUARIO", Usuario[1]); // carne de usuario
                                Session.Add("ID_USUARIO", Usuario[2]); // nombre de Usuario
                                Session.Add("EDIFICIO", Usuario[3]);
                                Session.Add("CUARTO", Usuario[4]);
                                Session.Add("CORREO", Usuario[5]);
                                Session.Add("APELLIDO", Usuario[6]);

                                FormsAuthentication.RedirectFromLoginPage(IdUsuario, true);
                            }
                            else if (Usuario[0] == "false")
                            {
                                lblMensajeError.ForeColor = Color.Red;
                                lblMensajeError.Font.Bold = true;
                                lblMensajeError.Text = "La contraseña o el usuario no es válido.";
                                txtUsuario.Text = "";
                                txtPassword.Text = "";
                            }
                        }   
                    }
                    catch (COMException ex)// captura y manejo de errores
                    {
                        if (ex.ErrorCode == -2147024810)
                        {
                            lblMensajeError.ForeColor = Color.Red;
                            lblMensajeError.Font.Bold = true;
                            lblMensajeError.Text = "La contraseña o el usuario no es válido.";
                            txtPassword.Text = "";
                        }
                        else if (ex.ErrorCode == -2147023570)
                        {
                            lblMensajeError.ForeColor = Color.Red;
                            lblMensajeError.Font.Bold = true;
                            lblMensajeError.Text = "Error de inicio de sesión: nombre de usuario desconocido o contraseña incorrecta";
                            txtPassword.Text = "";
                        }
                        else
                        {
                            lblMensajeError.Text = ex.Message;
                            txtPassword.Text = "";
                        }
                    }
                    catch (Exception ex) // captura y manejo de errores
                    {
                        lblMensajeError.ForeColor = Color.Red;
                        lblMensajeError.Font.Bold = true;
                        lblMensajeError.Text = "Contraseña o usuario incorrecto."; //ex.Message;
                        txtPassword.Text = "";
                    }

                }
            }
        }

        protected void btnCrearContra_Click(object sender, EventArgs e)
        {
            panelSignIn.Visible = false;
            panelCrearUsuario.Visible = true;
        }
        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            lblMensajeError2.Text = "";
            if (txtCarne.Text == "")
            {
                lblMensajeError2.Text = "El campo de Carné no puede ser vacío.";
            }
            else if (txtPin.Text == "")
            {
                lblMensajeError2.Text = "El campo de Pin no puede ser vacío.";
            }
            else if (txtPin.Text.Length < 6)
            {
                lblMensajeError2.Text = "La contraseña debe ser no menor de 6 carácteres.";
            }
            else
            {

                //Guarda en la BD el Pin del estudiante relacionado con el carné dado.
                //string sRetorno = Datos.cConnectionDatos.CrearUsuario(txtCarne.Text, txtPin.Text)[0];

                string sRetorno = "true";

                if (sRetorno == "true")
                {
                    alertExito.Visible = true;
                    panelSignIn.Visible = true;
                    panelCrearUsuario.Visible = false;
                }
                else if (sRetorno == "no") lblMensajeError2.Text = "Ya tiene un Pin asociado. Este es único y no se puede cambiar.";
                else lblMensajeError2.Text = "El carné es incorrecto.";

                
            }
            
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            panelSignIn.Visible = true;
            panelCrearUsuario.Visible = false;
        }
        
    }
}