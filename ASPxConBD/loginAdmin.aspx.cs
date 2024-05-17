using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Importar biblioteca ODBC
using System.Data.Odbc;

namespace ASPxConBD
{
    public partial class loginAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String query = "select claveAdmin,nombre from administrador where email=? and password=?";
            //Viene por pasos en el login.aspx
            OdbcConnection conexion = new ConexionBD().con;
            //Crear comando
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Configurar parametros
            comando.Parameters.AddWithValue("email", TextBox1.Text);
            comando.Parameters.AddWithValue("password", TextBox2.Text);
            //Como es un select, necesito el lector para cachar el resultado
            OdbcDataReader lector = comando.ExecuteReader();
            //Ver si el lector trae renglones
            if (lector.HasRows)
            {
                //El usuario puso bien su correo y su contraseña
                lector.Read(); //<-- estamos en el primer renglon del lector
                //Configuramos las variables de sesion
                Session.Add("claveAdmin", lector.GetInt32(0));
                Session.Add("nombreAdmin", lector.GetString(1));
                Session.Timeout = 10;
                //Redireccionar a la pagina del administrador
                Response.Redirect("InicioAdministrador.aspx");
            }
            else
            {
                //El usuario no puso bien su correo y su contraseña
                Session.Abandon();
                Label1.Text="Credenciales incorrectas";
            }
        }
    }
}