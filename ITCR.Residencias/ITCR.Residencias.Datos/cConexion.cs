using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ITCR.Residencias.Datos
{
    public class cConexion{

    private static readonly cConexion _DBInstance = new cConexion();
    private readonly SqlConnection _con = new SqlConnection("data source=SQLMASTER; initial catalog=REU;user id=sa;password=Solaris2012");
   // private readonly SqlConnection con = new SqlConnection("data source=loengry; initial catalog=REU;");


    static cConexion() { }
    public static cConexion Instance
    {
        get
        {
            return _DBInstance;
        }
    }

    public SqlConnection GetDBConnection()
    {
        //if(con.State = ConnectionState.Open())
        return _con;
    }
    }
}
