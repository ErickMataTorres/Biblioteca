using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Clases
{
    public class Conexion
    {
        public static SqlConnection Conectar()
        {
            string conx = "DATA SOURCE = A; INITIAL CATALOG = Proyecto; INTEGRATED SECURITY = YES;";
            SqlConnection s = new SqlConnection(conx);
            return s;
        }
    }
}
