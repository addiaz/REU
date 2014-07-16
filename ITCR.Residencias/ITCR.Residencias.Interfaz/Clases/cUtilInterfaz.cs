using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//estos los usa el metodo de serializarJSON
using System.Collections;
using System.Text;
using System.IO;
//using DataJsonConverter;
using System.Collections.Generic;
using ITCR.Residencias.Interfaz.wsSeguridad;

public enum eModo : short {Insertar = 1, Modificar = 2, Consultar = 3, Revisar = 4, Anular = 5, EditarAnular = 6, Eliminar = 7, VerHistorial = 8}

/// <summary>
/// Summary description for cUtilInterfaz.
/// </summary>
public class cUtilInterfaz
{
    /// <summary>
    /// Propósito: Agrega al placeholder en la interfaz el codigo para mostrar la ventana de error
    /// </summary>
    /// <returns>Nada </returns>
    /// <remarks>
    /// Parametros
    /// <UL>
    ///		 <LI>aspxPage. El objeto que representa la página actual</LI>
    /// </UL>
    /// </remarks>
    public static void AgregarCodError(Page aspxPage)
    {

        String strScript = "<script language=\"javascript\" id=\"clientError\">" + Environment.NewLine
            + "<!-- " + Environment.NewLine + "mostrarError();" + Environment.NewLine + "//--></script>";

        //p_oPHScript.Controls.Add(new LiteralControl(sTextoError));

        if (!aspxPage.ClientScript.IsStartupScriptRegistered("strKey2"))
        {
            aspxPage.ClientScript.RegisterStartupScript(aspxPage.GetType(), "strKey2", strScript);
        }
    }


    /// <summary>
    /// Propósito: Agrega al placeholder en la interfaz el codigo para mostrar la ventana de busqueda
    /// </summary>
    /// <returns>Nada </returns>
    /// <remarks>
    /// Parametros
    /// <UL>
    ///		 <LI>aspxPage. Objeto que representa a la ventana padre actual</LI>
    ///		 <LI>p_sURL. El URL de la ventana de busqueda</LI>
    ///		 <LI>p_sTitulo. El título de la ventana</LI>
    /// </UL>
    /// </remarks>
    public static void AgregarCodBusqueda(Page aspxPage, string p_sURL, string p_sTitulo, int p_nCodigoBusqueda)
    {
        string strScript;

        strScript = "<script language=\"javascript\" id=\"clientError\">" + Environment.NewLine;
        strScript += "<!-- " + Environment.NewLine + "mostrarBusqueda('" + p_sURL + "', '" + p_sTitulo + "', '" + p_nCodigoBusqueda + "');" + Environment.NewLine;
        strScript += "//--></script>";

        if (!aspxPage.ClientScript.IsStartupScriptRegistered("strKey3"))
        {
            aspxPage.ClientScript.RegisterStartupScript(aspxPage.GetType(), "strKey3", strScript);
        }
    }

    /// <summary>
    /// Propósito: Agrega al placeholder en la interfaz el codigo para mostrar una ventana de alert
    /// </summary>
    /// <returns>Nada </returns>
    /// <remarks>
    /// Parametros
    /// <UL>
    ///		 <LI>p_oPHScript. El objeto que representa la página actual</LI>
    ///		 <LI>p_sMensaje. Mensaje de error a mostrar en la ventana</LI>
    /// </UL>
    /// </remarks>
    public static void AgregarMensaje(Page aspxPage, String strMessage)
    {
        String strScript = "<script language=JavaScript>alert('"
            + strMessage + "')</script>";

        if (!aspxPage.ClientScript.IsStartupScriptRegistered("strKey1"))
        {
            aspxPage.ClientScript.RegisterStartupScript(aspxPage.GetType(), "strKey1", strScript);
        }
    }

    /// <summary>
    /// Propósito: Agrega al placeholder en la interfaz el codigo para mostrar una ventana de alert y luego hacer redirect a otra página
    /// </summary>
    /// <returns>Nada </returns>
    /// <remarks>
    /// Parametros
    /// <UL>
    ///		 <LI>aspxPage. El objeto que representa la página actual</LI>
    ///		 <LI>strMensaje. Mensaje de error a mostrar en la ventana</LI>
    ///		 <LI>strMensaje. Mensaje de error a mostrar en la ventana</LI>
    /// </UL>
    /// </remarks>
    public static void AgregarMensajeRedireccc(Page aspxPage, String strMessage, String strPaginaRedirect)
    {
        String strScript = "<script language=JavaScript>alert('"
            + strMessage + "');window.location.href = '" + strPaginaRedirect + "'; </script>";

        if (!aspxPage.ClientScript.IsStartupScriptRegistered("strKey1"))
        {
            aspxPage.ClientScript.RegisterStartupScript(aspxPage.GetType(), "strKey1", strScript);
        }
    }

    ///	<summary>
    ///	This method	setups the java	client script confirmation.	Note. You 
    ///	can	put	the	javascript in the aspx page. 
    ///	</summary>
    public static void AgregarScriptBorrar(Page aspxPage)
    {
        string js = @"
				<script	language=JavaScript>
						function confirmarBorrado()
						{
							return confirm('¿Desea eliminar el registro indicado?');
						}
					</script>";
        //Register the script
        if (!aspxPage.ClientScript.IsClientScriptBlockRegistered("ConfirmarBorrado"))
        {
            aspxPage.ClientScript.RegisterClientScriptBlock(aspxPage.GetType(), "ConfirmarBorrado", js);
        }
    }

    /// <summary>
    /// Propósito: Obtiene y retorna un objeto de negocios con los permisos del perfil para una funcionalidad
    /// </summary>
    /// <returns>DataTable con el registro </returns>
    /// <remarks>
    /// Parametros
    /// <UL>
    ///		 <LI>phScipt. El PlaceHolder de la ventana</LI>
    /// </UL>
    /// Propiedades actualizadas luego de una llamada exitosa a este método: 
    /// <UL>
    ///		 <LI>Todas</LI>
    /// </UL>
    /// </remarks>
    public static bool TienePermiso(string pCOD_SEDE, int pCOD_APLICACION, int pCOD_FUNCIONALIDAD, string pID_USUARIO)
    {
        try
        {

            SeguridadSoapClient wsseg = new SeguridadSoapClient();
            bool retVal = wsseg.TienePermisoFuncionalidad(pCOD_APLICACION, pCOD_FUNCIONALIDAD, pID_USUARIO, pCOD_SEDE);
            return retVal;
        }
        catch (Exception ex) // captura y manejo de errores
        {
            throw ex;
        }
    }

    public static void LlenarComboSedes(DropDownList sSedes)
    {
        try
        {
            SeguridadSoapClient wsseg = new SeguridadSoapClient();
            DataSet oDs = wsseg.ObtenerListaSedes();
            DataTable oDt = oDs.Tables[0];
            sSedes.DataSource = new DataView(oDt);
            sSedes.DataTextField = "NOM_SEDE";
            sSedes.DataValueField = "COD_SEDE";
            sSedes.DataBind();
            oDt.Dispose();
            oDs.Dispose();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }

    public static string LeerParametro(string pNombreParam)
    {
        string Resultado;

        //obtener la ruta del archivo de configuracion
        AppSettingsReader _configReader = new AppSettingsReader();

        // Leer el valor de configuracion
        Resultado = _configReader.GetValue(pNombreParam, typeof(string)).ToString();
        if (Resultado == null)
        {
            Resultado = "";
        }
        return Resultado;
    }

}

