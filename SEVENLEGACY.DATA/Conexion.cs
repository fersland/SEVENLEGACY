using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SEVENLEGACY.DATA
{
    public class Conexion
    {
        public static SqlConnection conectar()
        {
            string strConexion = ConfigurationManager.ConnectionStrings["dbconexion"].ConnectionString;
            SqlConnection cn = new SqlConnection(strConexion);
            return cn;
        }
    }
}
