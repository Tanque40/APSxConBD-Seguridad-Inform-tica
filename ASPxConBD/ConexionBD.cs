using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//2. Importar la biblioteca de ODBC
using System.Data.Odbc;

//El namespace debe ser el nombre de su proyecto
namespace ASPxConBD
{
    public class ConexionBD
    {
        //3. Declarar el objeto conexion
        public OdbcConnection con { get; set; }

        //4. Programar el constructor
        public ConexionBD()
        {
            //Constructor
            //5. Declarar objeto para acceder al web.config
            System.Configuration.Configuration webConfig;
            //6. Decirle al objeto de configuracion cual web.config
            //debe abrir --> Debe ser el nombre del proyecto
            webConfig = System.Web.Configuration
                .WebConfigurationManager
                .OpenWebConfiguration("/ASPxConBD");
            //7. Declarar objeto para el string de conexion
            System.Configuration.ConnectionStringSettings stringDeConexion;
            //8. Extraer el string de conexion con ayuda del objeto
            //para acceder al web.config
            stringDeConexion = webConfig.ConnectionStrings
                .ConnectionStrings["BDGameSpot"];
            //9. Instanciar la conexion usando el string de conexion
            con = new OdbcConnection(stringDeConexion.ToString());
            //10. Abrir la conexion
            con.Open();
        }
    }
}