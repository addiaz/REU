using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITCR.Residencias.Datos
{
   public  class cAbtractDAL
    {
        protected SqlCommand CrearComando()
        {
            var cadenaConexion = cConexion.Instance.GetDBConnection();
            SqlCommand comando = new SqlCommand();
            comando = cadenaConexion.CreateCommand();
            comando.CommandType = CommandType.Text;
            return comando;
        }

        protected SqlCommand CrearComandoProcedure()
        {
            var cadenaConexion = cConexion.Instance.GetDBConnection();
            SqlCommand comando = new SqlCommand();
            comando = cadenaConexion.CreateCommand();
            comando.CommandType = CommandType.StoredProcedure;
            return comando;
        }


        protected DataTable EjecutarComandoSelect(SqlCommand comando)
        {

            DataTable tabla = new DataTable();
            var con = cConexion.Instance.GetDBConnection();
            // SqlCommand comando1 = new SqlCommand(comando, con);
            try
            {
                con.Open();

                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                adaptador.Fill(tabla);

            }
            catch (Exception ex)
            {
                con.Close();
            }
            con.Close();
            return tabla;

        }

        protected int EjecutarComando(SqlCommand comando)
        {
            var con = cConexion.Instance.GetDBConnection();
            int resultado;

            con.Open();
            resultado = comando.ExecuteNonQuery();
            con.Close();


            return resultado;
        }
    }
}
