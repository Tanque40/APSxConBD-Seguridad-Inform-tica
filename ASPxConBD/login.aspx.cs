using System;
//Importar la biblioteca de ODBC
using System.Data.Odbc;


namespace ASPxConBD
{
	public partial class login : System.Web.UI.Page
	{
		protected void Page_Load( object sender, EventArgs e )
		{

		}

		protected void Button1_Click( object sender, EventArgs e )
		{
			//11. Crear query
			String query = "select claveU,nombre from usuario where email=? and password=?";
			//12. Declarar objeto tipo conexion
			OdbcConnection conexion;
			//13. Instanciar un objeto de tipo ConexionBD
			//Conexion BD es la clase que hicimos para conectarnos
			ConexionBD objetoDeAyuda;
			//14. Llamar al constructor de ConexionBD
			objetoDeAyuda = new ConexionBD();
			//15. Sacar la conexion del objeto de ayuda y lo guardamos
			//en nuestro objeto conexion
			conexion = objetoDeAyuda.con;
			//16. Crear un comando de odbc
			//El comando permite ejecutar queries en la base de datos
			//El comando necesita una conexion abierta a la bd y una consulta
			OdbcCommand comando = new OdbcCommand( query, conexion );
			//17. Sacar los parámetros del usuario de la interfaz gráfica
			String correo;
			String contrasena;
			correo = TextBox1.Text;
			contrasena = TextBox2.Text;
			//18. Pasarle los parametros al comando
			//Los parametros van en el mismo orden en el que aparecen en la
			//consulta
			comando.Parameters.AddWithValue( "correo", correo );
			comando.Parameters.AddWithValue( "contrasena", contrasena );
			//19. Correr el comando (todo lo de arriba es configuracion
			//del comando)
			OdbcDataReader lector = comando.ExecuteReader();
			//20. Verificar si el lector tiene renglones
			if(lector.HasRows)
			{
				//21. Avanzar el lector al primer renglon
				lector.Read(); //<-- pasa al siguiente renglon que se puede leer
							   //22. Sacar el dato en la segunda columna del lector
				String nombre = lector.GetString( 1 );
				int claveUsuario = lector.GetInt32( 0 );
				//Variables de sesion
				Session.Add( "nombreUsuario", nombre );
				Session.Add( "claveU", claveUsuario );
				//Timeout de la sesion
				Session.Timeout = 10; //minutos
									  //Redirigir al usuario a la paginaprincipal
				Response.Redirect( "PaginaPrincipal.aspx" );
			} else
			{//El lector no tiene renglones, por lo tanto el usuario puso mal
			 //su correo o su contraseña
				Label1.Text = "Correo o contraseña incorrectos";
				//Borrar por seguridad los valores de la sesion
				Session.Clear(); //Borra todas las cookies de sesion
				Session.Abandon(); //Cierra la sesion y la borra
			}
			//23. Cerrar los objetos de la bd para liberar recursos
			TextBox1.Text = "";
			TextBox2.Text = "";
			lector.Close();
			conexion.Close();
		}
	}
}