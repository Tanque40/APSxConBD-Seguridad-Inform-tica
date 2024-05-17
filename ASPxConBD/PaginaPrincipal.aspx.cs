using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Importar biblioteca para base de datos
using System.Data.Odbc;

namespace ASPxConBD
{
    public partial class PaginaPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Acceder a las variables de sesion
            //Si las variables de sesion no existen, quiere decir
            //que el usuario no pudo hacer login
            if(Session["nombreUsuario"]==null || Session["claveU"]==null)
            {//Cerramos sesion y redireccionamos al login
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            //Si llego aquí es que existen las variables de sesion
            String cUsuario = Session["claveU"].ToString();
            String nombreU = Session["nombreUsuario"].ToString();
            Label1.Text = "Bienvenid@ " + nombreU;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {//boton salir - abandonamos la sesion y regresamos al login
            Session.Abandon();
            Response.Redirect("login.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            String query = "select claveJ as 'Clave',juego.nombre as 'Juego',consola.nombre as 'Consola' from "
                + " juego inner join consola on juego.claveCon = consola.claveCon "
                + " where juego.nombre like ? ";
            //Crear conexion usando la clase ConexionBD
            OdbcConnection conexion = new ConexionBD().con;
            //Crear comando para leer los datos
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Pasarle los parametros
            comando.Parameters.AddWithValue("buscar", "%" + TextBox1.Text + "%");
            //Ejecutamos el comando y como es select, lo cachamos con datareader
            OdbcDataReader lector = comando.ExecuteReader();
            //Voy a verificar que tenga datos, si no le avisamos al usuario
            if (lector.HasRows == false)
            {//No tiene datos
                Label2.Text = "No se encontraron juegos con esas palabras";
                GridView1.Visible = false;
            }
            else
            {//Si tiene datos, cargar el gridview
                GridView1.Visible = true;
                //Para que el gridview nos permita seleccionar cosas
                GridView1.AutoGenerateSelectButton = true;
                GridView1.DataSource = lector; //Configuramos la fuente
                GridView1.DataBind(); //Conectarse al lector
                Label2.Text = "";
            }
            lector.Close();
            conexion.Close();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cual es el renglon seleccionado
            int renglon = GridView1.SelectedIndex;
            //Sacar la llave primaria del juego seleccionado
            //En la columna 1 porque la columna 0 es Seleccionar
            //Variable de sesion para guardar el juego seleccionado y poder
            //cargar su información en la pagina NuevaCritica
            Session.Add("llaveJuego", GridView1.Rows[renglon].Cells[1].Text);
            //Mandamos al usuario a la pagina de NuevaCritica.aspx
            Response.Redirect("NuevaCritica.aspx");
        }
    }
}