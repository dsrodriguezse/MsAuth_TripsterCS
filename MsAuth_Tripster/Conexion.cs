//using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace MsAuth_Tripster
{
    public class Conexion
    {
        private MySqlConnection conexion;
        private string server = "localhost";
        private string database = "dbrueba";
        private string user = "root";
        private string password = "12345678";
        private string cadenaConexion;
        public Conexion()
        {
            cadenaConexion = "Database="+database+
                "; DataSource="+server+
                "; User Id="+user+
                "; Password=" + password;
        }
        public MySqlConnection getConexion()
        {
            if(conexion == null)
            {
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
            }
            return conexion;
        }
    }
}
