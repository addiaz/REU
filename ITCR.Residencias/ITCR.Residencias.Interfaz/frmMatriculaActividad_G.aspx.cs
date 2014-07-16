using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ITCR.Residencias.Interfaz
{
    public partial class frmMatriculaActividad_G : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarTablaActividades();

            txtCapitan.Text = "Yo: " + Session["ID_USUARIO"].ToString();
            //string sUsuario = Session["CARNE_USUARIO"].ToString();
            // if ( EquipoBuscarEstudiante(sUsuario) ) // Devuelve un bool.
            
            //{PanelCentral.Visible = false;}
        }

        protected void btnMostrarEstudiantes_Click(object sender, EventArgs e)
        {
            //btnMostrarEstudiantes.Text = selectActividades.SelectedIndex.ToString();

            if (selectActividades.SelectedIndex.ToString() == "1")
            {
                if (panelEstudiante.Visible == false)
                {
                    panelEstudiante.Visible = true;
                    MostrarEstudiantesEdificio();
                }

            }

        }

        protected void MostrarEstudiantesEdificio()
        {
            List<string> listEstudiantes = Datos.cConnectionDatos.SeleccionarEstudiantesEdificio(Session["EDIFICIO"].ToString());

            int i = 0;

            if (listEstudiantes[0] != "False")
            {
                while (i < listEstudiantes.Count)
                {
                    ListItem listItem = new ListItem(listEstudiantes[i + 1] + "  " + listEstudiantes[i + 2], listEstudiantes[i], true); // Nombre, Carné

                    chklEstudiantes.Items.Add(listItem);

                    i += 4;
                }
            }
        }
        

        protected void btnMatricular_Click(object sender, EventArgs e)
        {
            int i = 0;

            while (i < chklEstudiantes.Items.Count)
            {
                if (chklEstudiantes.Items[i].Selected == true)
                {
                    chklEstudiantes.Items[i].Selected = false;

                    ListItem listItemNuevo = new ListItem(chklEstudiantes.Items[i].Text, chklEstudiantes.Items[i].Value, true);
                    chklEstudiantesEquipo.Items.Add(listItemNuevo);

                    chklEstudiantes.Items[i].Selected = true;
                }

                i += 1;
            }
            
            i = 0;
            while (i < chklEstudiantes.Items.Count)
            {
                if (chklEstudiantes.Items[i].Selected == true)
                {
                    chklEstudiantes.Items[i].Selected = false;
                    chklEstudiantes.Items[i].Enabled = false;
                }

                i += 1;
            }

            btnDesmatricular.Enabled = true;
        }

        protected void btn_Desmatricular_Click(object sender, EventArgs e)
        {
            int i = 0;

            while (-1 < i && i < chklEstudiantesEquipo.Items.Count)
            {
                string sValor = chklEstudiantesEquipo.Items[i].Text;
                Boolean bBorrado = false; // Se usa para saber si se elimino algun check del equipo

                if (chklEstudiantesEquipo.Items[i].Selected == true)
                {
                    int j = 0;
                    while (j < chklEstudiantes.Items.Count)
                    {
                        if (sValor == chklEstudiantes.Items[j].Text)
                        {
                            //se restaura en la lista de residentes en el edificio
                            chklEstudiantes.Items[j].Selected = false;
                            chklEstudiantes.Items[j].Enabled = true;

                            //Se elimina del equipo
                            chklEstudiantesEquipo.Items.RemoveAt(i);

                            i -= 1;
                        }

                        j += 1;
                    }
                    
                }
                i += 1;

            }

            if (chklEstudiantesEquipo.Items.Count == 0)
            {
                btnDesmatricular.Enabled = false;
                chklEstudiantes.Items.Clear();
                panelEstudiante.Visible = false;
            }
        }

        protected void LlenarTablaActividades()
        {
            List<string> listActividades = Datos.cConnectionDatos.ObtenerActividades();

            


            HtmlTableRow rowNew;
            HtmlTableCell cellNuevaCelda;


            tablaActividades.Rows.Clear();
            ddlActividades.Items.Clear();

            /* Se inicalizan las variables HTML */
            rowNew = new HtmlTableRow();
            cellNuevaCelda = new HtmlTableCell();


            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "#"; // Numero de Actividad
            rowNew.Cells.Add(cellNuevaCelda);

            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "Nombre"; // Nombre de Actividad
            rowNew.Cells.Add(cellNuevaCelda);

            /*****************************************************/
            cellNuevaCelda = new HtmlTableCell();
            cellNuevaCelda.InnerText = "Descripción"; // Nombre de Descripción
            rowNew.Cells.Add(cellNuevaCelda);


            int i = 0;

            while (i < listActividades.Count)
            {
                ListItem listItem = new ListItem(listActividades[i + 1], listActividades[i], true);

                
                //HtmlSelect selector = new HtmlSelect();

                selectActividades.Items.Add(listItem);

                ddlActividades.Items.Add(listItem);

                /* Se vuelve a inicializar las variables, esto es para obtener un nuevo elemento HTML */
                rowNew = new HtmlTableRow();
                cellNuevaCelda = new HtmlTableCell();

                /* Se agrega una fila nueva a la tabla */
                tablaActividades.Rows.Add(rowNew);

                /*****************************************************/
                /* Numero de Actividad */
                cellNuevaCelda.InnerText = listActividades[i];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/

                /*****************************************************/
                /* Nombre de Actividad*/
                cellNuevaCelda = new HtmlTableCell();
                cellNuevaCelda.InnerText = listActividades[i + 1];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/

                /*****************************************************/
                /* Descripcion de Actividad*/
                cellNuevaCelda = new HtmlTableCell();
                cellNuevaCelda.InnerText = listActividades[i + 2];
                rowNew.Cells.Add(cellNuevaCelda);
                /*****************************************************/

                i += 3;
            }
        }
    }
}