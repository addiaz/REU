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

            btnMatricular.Text = Select1.SelectedIndex.ToString();

            if (Select1.SelectedIndex == 1)
            {
                if (PanelInicio.Visible == false)
                {
                    PanelInicio.Visible = true;
                    MostrarEstudiantesEdificio();
                }

            }

            else
            {
                if (PanelInicio.Visible == false)
                {
                    PanelInicio.Visible = true;
                    PanelPorEdificio.Visible = true;
                }

            }
        }

        protected void MostrarEstudiantesEdificio()
        {
            PanelResidentes.Visible = true;

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
            HtmlTable tableTEquipo = new HtmlTable();
            HtmlTableRow rowColumna = new HtmlTableRow();
            HtmlTableCell cellFila = new HtmlTableCell();

            List<string> listEstudiantesEquipo = new List<string>();
            int i = 0;
            int iContadorEstudiantes = 0;

            while (i < chklEstudiantes.Items.Count)
            {
                if (chklEstudiantes.Items[i].Selected == true)
                {
                    chklEstudiantes.Items[i].Selected = false;

                    //ListItem listItemNuevo = new ListItem(chklEstudiantes.Items[i].Text, chklEstudiantes.Items[i].Value, true);
                    //chklEstudiantesEquipo.Items.Add(listItemNuevo);

                    listEstudiantesEquipo.Add(chklEstudiantes.Items[i].Text);

                    //chklEstudiantes.Items[i].Selected = true;

                    iContadorEstudiantes++;
                }

                i += 1;
            }
            
            /*
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
            */

            if (Select1.SelectedIndex.ToString() == "1")
            {
                if (iContadorEstudiantes > 5)
                {
                    cellFila.InnerText = chklEstudiantes.Items[i].Text;
                    rowColumna.Cells.Add(cellFila);
                    tableTEquipo.Rows.Add(rowColumna);


                    btnDesmatricular.Enabled = true;
                    chklEstudiantes.Items.Clear();
                    PanelResidentes.Visible = false;
                }
                else // Equipo valido para futbol sala.
                {
                    // Debe ser el equipo mayor igual que 5 personas.
                }
            }

        }

        protected void btn_Desmatricular_Click(object sender, EventArgs e)
        {
            int i = 0;

            /*
            while (-1 < i && i < chklEstudiantesEquipo.Items.Count)
            {
                string sValor = chklEstudiantesEquipo.Items[i].Text;

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
            */

            tableEquipo.Rows.Clear();
            btnDesmatricular.Enabled = false;
                
            chklEstudiantes.Items.Clear();

            PanelInicio.Visible = false;
            PanelResidentes.Visible = false;
        }

        protected void LlenarTablaActividades()
        {
            List<string> listActividades = Datos.cConnectionDatos.ObtenerActividades();


            HtmlTableRow rowNew;
            HtmlTableCell cellNuevaCelda;


            tablaActividades.Rows.Clear();
            Select1.Items.Clear();
            drpActividades.Items.Clear();

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

                //selectActividades.Items.Add(listItem);
                drpActividades.Items.Add(listItem);
                Select1.Items.Add(listItem);

                drpActividades.Visible = false;

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