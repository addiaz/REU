using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ITCR.Residencias.Interfaz
{
    public enum eModo : short { Insertar = 1, Modificar = 2, Consultar = 3 }

    public class Global : System.Web.HttpApplication
    {

        //Encabezado principal
        public static string gTituloPagina = "Tecnológico de Costa Rica";

        //Pie de pagina global
        public static string gPiePagina = "Tecnológico de Costa Rica, 2014";
        
        // al variar esto se ve afectado el encabezado de cada página y el título de las páginas en el navegador
        public static string gSubTituloPagina = "Residencias";

        public static int gCOD_APLICACION = 0; //modificar en web.config, no acá


        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            gCOD_APLICACION = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Main.gCOD_APLICACION"].ToString());
            gTituloPagina = System.Configuration.ConfigurationManager.AppSettings["Main.gTituloPagina"].ToString();
            gSubTituloPagina = System.Configuration.ConfigurationManager.AppSettings["Main.gSubTituloPagina"].ToString();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started


        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
