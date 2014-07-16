using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ITCR.Residencias.Interfaz
{
    public partial class frmMatricula : System.Web.UI.Page
    {

        /* En el Page Load actualiza todas las tablas
         * de talleres y actividades, de acuerdo con
         * sus cupos.
         */

        

        protected void Page_Load(object sender, EventArgs e)
        {
            mostrarTalleres();

            List<string> lET = Datos.cConnectionDatos.matricularTaller(Session["CARNE_USUARIO"].ToString(), "", "seleccionar uno");
            if (lET.Count != 0)
            {
                ddlListaTalleres.Enabled = false;
                btnMatricular.Enabled = false;

                btnDesmatricular.Enabled = true;
                ddlListaTalleresMatriculados.Enabled = true;

                List<string> lCurso = Datos.cConnectionDatos.gestionarTalleres("seleccionar uno", "", lET[1]);

                ListItem listItem = new ListItem(lCurso[1], lCurso[0], true);
                ddlListaTalleresMatriculados.Items.Add(listItem);
            }
        }

        protected void btnMatricular_Click(object sender, EventArgs e)
        {
            
            /* Actualiza el cupo del taller consultado */
            Datos.cConnectionDatos.ActualizarTaller("num_cupo = num_cupo - 1", ddlListaTalleres.SelectedValue);
            //Datos.cConnectionDatos.gestionarTalleres("actualizar","Cupo = Cupo - 1", ddlListaTalleres.SelectedValue);

            Datos.cConnectionDatos.matricularTaller(Session["CARNE_USUARIO"].ToString(), ddlListaTalleres.SelectedValue, "insertar");

            btnDesmatricular.Enabled = true; // se activa el boton de desmatricular.
            ddlListaTalleresMatriculados.Enabled = true; // se activa la lista de taller para desmatricular


            ListItem listItem = new ListItem(ddlListaTalleres.SelectedItem.Text, ddlListaTalleres.SelectedValue, true);
            //"#" + ddlListaTalleres.SelectedItem.Value+"-" + ddlListaTalleres.SelectedItem.Text
            ddlListaTalleresMatriculados.Items.Add(listItem);

            
            /* Desactiva la lista, puesto que tiene solo derecho a matricular un taller. */
            ddlListaTalleres.Enabled = false;
            btnMatricular.Enabled = false;

            
            mostrarTalleres();
            
        }

        protected void btnDesmatricular_Click(object sender, EventArgs e)
        {
            
            Datos.cConnectionDatos.ActualizarTaller("num_cupo = num_cupo + 1", ddlListaTalleres.SelectedValue);
            //Datos.cConnectionDatos.gestionarTalleres("actualizar", "Cupo = Cupo + 1", ddlListaTalleres.SelectedValue);
            
            Datos.cConnectionDatos.matricularTaller(Session["CARNE_USUARIO"].ToString(),"", "borrar");

            ddlListaTalleresMatriculados.Items.Clear();

            ddlListaTalleres.Enabled = true;
            btnMatricular.Enabled = true;

            btnDesmatricular.Enabled = false;
            ddlListaTalleresMatriculados.Enabled = false;

            ddlListaTalleres.Items.Clear();
            mostrarTalleres();
            // pasa lo que tenga que pasar con la base de datos...
        }

        
        public void mostrarTalleres()
        {
            List<string> Curso = Datos.cConnectionDatos.SeleccionarTalleresDisponibles();
                //Datos.cConnectionDatos.gestionarTalleres("consultar disponibles","" ,"todos"); // (metodo, columna, nombre)

            /* Se declaran las variables */
            HtmlTableRow rowNew;
            HtmlTableCell cellNuevaCelda;

            /* Se limpia la tabla */
            tableTalleres.Rows.Clear();

            /* Se inicalizan las variables HTML */
            rowNew = new HtmlTableRow();
            cellNuevaCelda = new HtmlTableCell();

            /* Agrega los encabezados de las columas de la tabla */

            tableTalleres.Rows.Add(rowNew);

            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "#"; // Numero de Taller
            //cellNuevaCelda.BgColor = ConsoleColor.Gray.ToString();
            rowNew.Cells.Add(cellNuevaCelda);

            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "Nombre"; // Nombre de Taller
            //cellNuevaCelda.BgColor = ConsoleColor.Black.ToString();
            rowNew.Cells.Add(cellNuevaCelda);

            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "Cupo"; // Cupo de Taller
            //cellNuevaCelda.BgColor = ConsoleColor.Black.ToString();
            rowNew.Cells.Add(cellNuevaCelda);

            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "Horario"; // Horario de Taller
            //cellNuevaCelda.BgColor = ConsoleColor.Black.ToString();
            rowNew.Cells.Add(cellNuevaCelda);

            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "Descripción"; // Descripcion de Taller
            //cellNuevaCelda.BgColor = ConsoleColor.Black.ToString();
            rowNew.Cells.Add(cellNuevaCelda);


            /* Inicia a llenar los valores de cada celda de la tabla */

            int iIndiceTaller = 1;
            int i = 0;

           

            while (i < Curso.Count)
            {
                ListItem listItem = new ListItem(Curso[i + 1], Curso[i], true); // text, value, bool.  value = id

                ddlListaTalleres.Items.Add(listItem);


                /* Se vuelve a inicializar las variables, esto es para obtener un nuevo elemento HTML */
                rowNew = new HtmlTableRow();
                cellNuevaCelda = new HtmlTableCell();

                /* Se agrega una fila nueva a la tabla */
                tableTalleres.Rows.Add(rowNew);

                /*****************************************************/
                /* Numero de Taller */
                cellNuevaCelda.InnerText = Curso[i];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/


                /*****************************************************/
                /* Nombre de Taller*/
                cellNuevaCelda = new HtmlTableCell();
                cellNuevaCelda.InnerText = Curso[i + 1];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/

                /*****************************************************/
                /* Cupo de Taller*/
                cellNuevaCelda = new HtmlTableCell();
                cellNuevaCelda.InnerText = Curso[i + 2];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/

                /*****************************************************/
                /* Horario de Taller*/
                cellNuevaCelda = new HtmlTableCell();
                cellNuevaCelda.InnerText = Curso[i + 3];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/

                /*****************************************************/
                /* Descripcion de Taller*/
                cellNuevaCelda = new HtmlTableCell();
                cellNuevaCelda.InnerText = Curso[i + 4];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/

                i = i + 5;
                iIndiceTaller++;
                                                                        
               
            } 
        }
    }
}