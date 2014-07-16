#region Acerca de...
/////////////////////////////////////////////////////////////////////////////
// Instituto Tecnológico de Costa Rica
// Proyecto: Framework Matricula
// Descripción: Clase de acceso a datos de tablas.
// Fecha: Viernes, 20 de Junio de 2014, 02:40:00 p.m.
// Dado que esta clase implementa IDispose, las clases derivadas no 
// deben hacerlo.
////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;




namespace ITCR.Residencias.Datos
{
    public class cConnectionDatos
    {

        private static string _sStringConnection = Datos.Properties.Settings.Default.ITCRMatriculaConnectionString;
        private static SqlCommand _sqlcommandQuery;
        private static SqlDataReader _readerR;
        private static SqlConnection _sqlConnection;
        private static string _sComando;
        


        public static List<string> CrearUsuario(string pCarne, string pPin)
        {
            List<string> lRetorno = new List<string>();
            string sComando = "";
            

            using (SqlConnection connection = new SqlConnection(_sStringConnection))
            {
                lRetorno.Add("false");                
                
                sComando = "select * from Estudiante where num_carne = " + pCarne;
                connection.Open();
                
                SqlCommand sqlcommandQuery = new SqlCommand(sComando, connection);

                using (SqlDataReader reader = sqlcommandQuery.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //Verifica que se agregue solo una vez.
                        if (reader["txt_contrasenna"].ToString() == "") { lRetorno[0] = "true"; }
                        else { lRetorno[0] = "no"; }
                    }
                }
                connection.Close();

                if (lRetorno[0] == "true")
                {
                    sComando = "update Estudiante set txt_contrasenna = " + pPin + " where num_carne = " + pCarne;

                    connection.Open();

                    sqlcommandQuery = new SqlCommand(sComando, connection);
                    sqlcommandQuery.ExecuteNonQuery();

                    connection.Close();
                }
                
            }

            return lRetorno;
        }


       /* Retorna una lista con solo 3 valores: 
        *   1- un boolean para verificar al usuario (existe=true)
        *   2- nombre
        *   3- carnet
        */
        public static List<string> Autentificacion(string pUsuario, string pPassword)
        {
            List<string> listUsuario = new List<string>();
            

            // Usuario newUsuario = new Usuario();

            string sComando = "";
            
            using (SqlConnection connection = new SqlConnection(_sStringConnection))
            {
                    sComando = "select * from Estudiante where num_carne = " + pUsuario + " and txt_contrasenna =  " + pPassword;
                    listUsuario.Add("false");

                    connection.Open();
                    SqlCommand sqlcommandQuery = new SqlCommand(sComando, connection);

                    using (SqlDataReader reader = sqlcommandQuery.ExecuteReader())
                    {
                        while (reader.Read())
                        {                            

                            listUsuario[0] = "true";

                            listUsuario.Add(reader["num_carne"].ToString()); // 1
                            listUsuario.Add(reader["nom_estudiante"].ToString()); // 2
                            listUsuario.Add(reader["txt_edificio"].ToString()); // 3
                            listUsuario.Add(reader["txt_cuarto"].ToString()); // 4
                            listUsuario.Add(reader["txt_correo"].ToString()); // 5
                            listUsuario.Add(reader["txt_apellidos"].ToString()); // 6
                        }
                    }
                    connection.Close();

            }
            return listUsuario;
        }

        public static List<string> SeleccionarEstudiantesEdificio(string p_edificio)
        {
            List<string> listEstudiantes = new List<string>();

            try
            {
                using(_sqlConnection = new SqlConnection(_sStringConnection))
                {
                    _sqlConnection.Open();
                    _sComando = "select * from estudiante where txt_edificio = '" + p_edificio + "'  Order by txt_cuarto ASC";
                    _sqlcommandQuery = new SqlCommand(_sComando, _sqlConnection);

                    using(_readerR = _sqlcommandQuery.ExecuteReader())
                    {
                        while (_readerR.Read())
                        {
                            listEstudiantes.Add(_readerR["num_carne"].ToString()); // 0
                            listEstudiantes.Add(_readerR["nom_estudiante"].ToString()); // 1
                            listEstudiantes.Add(_readerR["txt_apellidos"].ToString()); // 2
                            listEstudiantes.Add(_readerR["txt_cuarto"].ToString()); // 3
                        }
                        
                    }
                }
                
            }
            catch(Exception e)
            {
                listEstudiantes.Add("False");
                listEstudiantes.Add(e.ToString());
                listEstudiantes.Add("ERROR");
            }

            return listEstudiantes;
        }

        public static List<string> SeleccionarUnEstudiante(string p_carne)
        {
            List<string> listEstudiantes = new List<string>();

            try
            {
                using (_sqlConnection = new SqlConnection(_sStringConnection))
                {
                    _sqlConnection.Open();
                    _sComando = "select * from estudiante where num_carne = '" + p_carne + "'";
                    _sqlcommandQuery = new SqlCommand(_sComando, _sqlConnection);

                    using (_readerR = _sqlcommandQuery.ExecuteReader())
                    {
                        while (_readerR.Read())
                        {
                            listEstudiantes.Add(_readerR["num_carne"].ToString()); // 0
                            listEstudiantes.Add(_readerR["nom_estudiante"].ToString()); // 1
                            listEstudiantes.Add(_readerR["txt_apellidos"].ToString()); // 2
                            listEstudiantes.Add(_readerR["txt_cuarto"].ToString()); // 3
                            listEstudiantes.Add(_readerR["txt_correo"].ToString()); // 4
                        }

                    }
                }

            }
            catch (Exception e)
            {
                listEstudiantes.Add("False");
                listEstudiantes.Add(e.ToString());
                listEstudiantes.Add("ERROR");
            }

            return listEstudiantes;
        }

        public static void InsertarTaller(string p_nombre, string p_cupo, string p_horario, string p_descripcion)
        {
            _sComando = "insert into Taller values(" + p_nombre + "," + p_cupo + "," + p_horario + "," + p_descripcion + ")";

            using (_sqlConnection = new SqlConnection(_sStringConnection))
            {
                _sqlConnection.Open();
                _sqlcommandQuery = new SqlCommand(_sComando, _sqlConnection);
                _sqlcommandQuery.ExecuteNonQuery();
                { 

                }
            }
        }

        public static bool ActualizarTaller(string p_columna, string p_nombre)
        {
            try
            {
                using (_sqlConnection = new SqlConnection(_sStringConnection))
                {
                    _sqlConnection.Open();
                    _sComando = "update Taller set " + p_columna + " where id_taller = " + p_nombre;
                    _sqlcommandQuery = new SqlCommand(_sComando, _sqlConnection);
                    _sqlcommandQuery.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e)
            {
                string sMensajeError = e.ToString();
                return false;
            }
            
        }

        public static List<String> SeleccionarTalleresDisponibles()
        {
            List<string> listTalleres = new List<string>();
            try
            {
                using (_sqlConnection = new SqlConnection(_sStringConnection))
                {
                    _sqlConnection.Open();
                    _sComando = "select * from Taller where num_cupo > 0";
                    _sqlcommandQuery = new SqlCommand(_sComando, _sqlConnection);

                    using (_readerR = _sqlcommandQuery.ExecuteReader())
                    {
                        while (_readerR.Read())
                        {
                            listTalleres.Add(_readerR["id_taller"].ToString()); //0
                            listTalleres.Add(_readerR["nom_taller"].ToString()); //1
                            listTalleres.Add(_readerR["num_cupo"].ToString()); //2
                            listTalleres.Add(_readerR["txt_horario"].ToString()); //3
                            listTalleres.Add(_readerR["nom_instructor"].ToString()); //4
                        }
                    }
                }

            }
            catch (Exception e)
            {
                listTalleres.Add(e.ToString());
            }

            return listTalleres;
        }

        /* Retorna una lista con los nombres y cupo de los talleres, que tiene cupo disponible,
         * es decir, mayor que cero. Esto para mostrarse en la interfaz de estudiante. 
         */
        public static List<string> gestionarTalleres(string pModalidad, string pColumna, string pNombre)
        {
            List<string> listTaller = new List<string>();

            try
            {
                using (_sqlConnection = new SqlConnection(_sStringConnection))
                {
                    _sqlConnection.Open();
                    _sComando = "select * from Taller where id_taller = " + pNombre;

                    _sqlcommandQuery = new SqlCommand(_sComando, _sqlConnection);
                    using (_readerR = _sqlcommandQuery.ExecuteReader())
                    {
                        while (_readerR.Read())
                        {
                            listTaller.Add(_readerR["id_taller"].ToString()); //0
                            listTaller.Add(_readerR["nom_taller"].ToString()); //1
                            listTaller.Add(_readerR["num_cupo"].ToString()); //2
                            //listTaller.Add(_readerR["txt_horario"].ToString()); //3
                            listTaller.Add(_readerR["txt_instructor"].ToString()); //3
                        }
                    }

                }
            }
            catch(Exception e)
            {
                listTaller.Add(e.ToString());
            }

            return listTaller;
        }

        public static List<string> matricularTaller(string pCarne ,string pTaller, string pModalidad)
        {
            List<string> listTaller = new List<string>();
            try
            {
                using (_sqlConnection = new SqlConnection(_sStringConnection))
                {
                    if (pModalidad == "insertar")
                    {
                        _sComando = "insert into TallerResidente values(" + pCarne + ", " + pTaller + ")";
                    }
                    else if (pModalidad == "borrar") 
                    { 
                        _sComando = "delete from TallerResidente where num_carne = " + pCarne; 
                    }
                    else if (pModalidad == "seleccionar todos")
                    {
                        _sComando = "select * from TallerResidente";
                    }
                    else if (pModalidad == "seleccionar uno")
                    {
                        _sComando = "select * from TallerResidente where num_carne = " + pCarne;
                    }

                    _sqlConnection.Open();
                    _sqlcommandQuery = new SqlCommand(_sComando, _sqlConnection);

                    using (_readerR = _sqlcommandQuery.ExecuteReader())
                    {
                        while (_readerR.Read())
                        {
                            listTaller.Add(_readerR["num_carne"].ToString()); //0
                            listTaller.Add(_readerR["id_taller"].ToString()); //1
                        }
                    }

                }
            }
            catch (Exception e)
            {
                listTaller.Add(e.ToString());
            }

            return listTaller;
        }



        /* Retorna una lista con los nombres de las actividades. 
         * Esto para mostrarse en la interfaz de estudiante. 
         */
        public static List<string> ObtenerActividades()
        {

            List<string> lRetorno = new List<string>();

            using (_sqlConnection = new SqlConnection(_sStringConnection))
            {
                _sComando = "select * from actividad";

                _sqlConnection.Open();

                SqlCommand command = new SqlCommand(_sComando, _sqlConnection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lRetorno.Add(reader["id_actividad"].ToString()); //0
                        lRetorno.Add(reader["nom_actividad"].ToString()); //1
                        lRetorno.Add(reader["txt_horario"].ToString()); //2
                    }
                }

            }
            return lRetorno;
        }

    }
}
