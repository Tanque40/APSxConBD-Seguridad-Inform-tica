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
    public partial class InicioAdministrador : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificar que el usuario haya hecho login valido
            //Confirmar que las variables de sesion existen
            if(Session["claveAdmin"]==null || Session["nombreAdmin"] == null)
            {
                //Las variables de sesion no existen... terminar sesion y 
                //redireccionar
                Session.Abandon();
                Response.Redirect("loginAdmin.aspx");
            }
            //Si hizo login valido, le damos la bienvenida
            Label1.Text = "Bienvenido administador(a) " + Session["nombreAdmin"].ToString();

            //***** Cargar dropdowns de consolas *****
            //DropDownList1
            if (DropDownList1.Items.Count == 0)
            {
                //Query para traer llaves primarias y nombre
                String queryConsolas = "select claveCon,nombre from consola";
                //Abrir conexion a bd
                OdbcConnection conexion = new ConexionBD().con;
                //Crear comando con query y conexion
                OdbcCommand comando = new OdbcCommand(queryConsolas, conexion);
                //Configurar parametros
                //No... este query no lleva parametros
                //Corremos el comando con executeReader porque es un select
                OdbcDataReader lector = comando.ExecuteReader();
                //Configurar el dropdownlist con el lector
                DropDownList1.DataSource = lector;
                //Configurar valor oculto como la llave primaria de las consolas
                //Viene del nombre de la columna en el select queryConsolas
                DropDownList1.DataValueField = "claveCon";
                //Configurar texto que ve el usuario de forma amigable
                //Viene del nombre de la columna en el select queryConsolas
                DropDownList1.DataTextField = "nombre";
                //Ligamos la configuracion al lector
                DropDownList1.DataBind();
                conexion.Close();
            }
            //DropDownList2
            if (DropDownList2.Items.Count == 0)
            {
                //Query para traer llaves primarias y nombre
                String queryConsolas = "select claveCon,nombre from consola";
                //Abrir conexion a bd
                OdbcConnection conexion = new ConexionBD().con;
                //Crear comando con query y conexion
                OdbcCommand comando = new OdbcCommand(queryConsolas, conexion);
                //Configurar parametros
                //No... este query no lleva parametros
                //Corremos el comando con executeReader porque es un select
                OdbcDataReader lector = comando.ExecuteReader();
                //Configurar el dropdownlist con el lector
                DropDownList2.DataSource = lector;
                //Configurar valor oculto como la llave primaria de las consolas
                //Viene del nombre de la columna en el select queryConsolas
                DropDownList2.DataValueField = "claveCon";
                //Configurar texto que ve el usuario de forma amigable
                //Viene del nombre de la columna en el select queryConsolas
                DropDownList2.DataTextField = "nombre";
                //Ligamos la configuracion al lector
                DropDownList2.DataBind();
                conexion.Close();
            }

            //***** Cargar dropdowns de consolas *****
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("loginAdmin.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String queryLlave = "select max(claveJ) from juego";
            //claveJ --> ahorita la generamos la guardamos en llave,
            //nombre --> TextBox1,
            //resumen --> TextBox2,
            //claveCon --> Dropdownlist1,
            //fechaLanzamiento --> TextBox3
            String queryInsert = "insert into juego values(?,?,?,?,?)";
            //***** Generar llave primaria *****
            int llave = 1;//Inicializamos en 1 por si la tabla esta vacia
            //Abrir conexion
            OdbcConnection conexion = new ConexionBD().con;
            //Crear comando
            OdbcCommand comando = new OdbcCommand(queryLlave, conexion);
            //Este query no necesita parametros, por lo tanto lo corro
            OdbcDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                lector.Read();
                //Sacamos la maxima llave primaria
                llave = lector.GetInt32(0);
                llave++;
            }
            lector.Close(); //Cerramos el lector
            //***** Generar llave primaria *****

            //***** Insert del juego *****
            //Crear comando
            comando = new OdbcCommand(queryInsert, conexion);
            //Configurar los parametros
            comando.Parameters.AddWithValue("llavePrimaria", llave);
            comando.Parameters.AddWithValue("nombre", TextBox1.Text);
            comando.Parameters.AddWithValue("resumen", TextBox2.Text);
            comando.Parameters.AddWithValue("llaveConsola", DropDownList1.SelectedValue.ToString());
            comando.Parameters.AddWithValue("fechaLanz", TextBox3.Text);
            //Correr el comando en un try-catch
            try
            {
                comando.ExecuteNonQuery();
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                DropDownList1.SelectedIndex = 0;
                Label2.Text = "Juego registrado exitosamente";
            }
            catch(Exception ex)
            {
                Label2.Text = "No pudimos dar de alta el juego " + ex.ToString();
            }
            conexion.Close();
            //***** Insert del juego *****
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            String query = "select claveJ as 'Clave',juego.nombre as 'Juego',consola.nombre as 'Consola' from "
    + " juego inner join consola on juego.claveCon = consola.claveCon "
    + " where juego.nombre like ? ";
            //Crear conexion usando la clase ConexionBD
            OdbcConnection conexion = new ConexionBD().con;
            //Crear comando para leer los datos
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Pasarle los parametros
            comando.Parameters.AddWithValue("buscar", "%" + TextBox4.Text + "%");
            //Ejecutamos el comando y como es select, lo cachamos con datareader
            OdbcDataReader lector = comando.ExecuteReader();
            //Voy a verificar que tenga datos, si no le avisamos al usuario
            if (lector.HasRows == false)
            {//No tiene datos
                Label3.Text = "No se encontraron juegos con esas palabras";
                GridView1.Visible = false;
            }
            else
            {//Si tiene datos, cargar el gridview
                GridView1.Visible = true;
                //Para que el gridview nos permita seleccionar cosas
                GridView1.AutoGenerateSelectButton = true;
                GridView1.DataSource = lector; //Configuramos la fuente
                GridView1.DataBind(); //Conectarse al lector
                Label3.Text = "";
            }
            lector.Close();
            conexion.Close();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String query = "select juego.nombre,resumen,consola.claveCon,fechaLanzamiento "
    + " from juego inner join consola on juego.claveCon = consola.claveCon "
    + " where juego.claveJ = ?";
            //Abrir conexion a la bd
            OdbcConnection conexion = new ConexionBD().con;
            //Crear el comando
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Pasarle el parametro juego.ClaveJ al comando
            //esta en el gridview, en el renglon seleccionado en la columna 1
            //La columna 0 del gridview es "Seleccionar"
            comando.Parameters.AddWithValue("claveJuego", GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text);
            //Ejecutar el comando, como es un select, guardo el resultado en un reader
            OdbcDataReader lector = comando.ExecuteReader();
            //Verificar si el lector trae datos
            if (lector.HasRows)
            {
                //Si tiene datos, pasar al primer renglon
                lector.Read();
                //Sacar los datos del lector y los cargamos en los labels correspondientes
                //Llave de juego
                Label4.Text = GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text;
                TextBox5.Text = lector.GetString(0); //Columna cero, ahi viene el nombre del juego
                TextBox6.Text = lector.GetString(1); //Columna 1, ahi viene el resumen
                DropDownList2.SelectedValue = lector.GetInt32(2).ToString(); //Columna 2, ahi viene la consola
                TextBox7.Text = lector.GetDateTime(3).ToString(); //Columna 3, ahi viene la fecha
            }
            lector.Close();
            conexion.Close();

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            //Aquí hacemos el update
            //nombre --> TextBox5
            //resumen --> TextBox6
            //claveCon --> DropDownList2
            //fechaLanzamiento --> TextBox7.Text
            //ClaveJ --> GridView SelectedIndex Celda 1
            String query = "update juego set nombre=?,resumen=?,claveCon=?,fechaLanzamiento=? "
                 +" where claveJ = ?";
            //Abrir conexion
            OdbcConnection conexion = new ConexionBD().con;
            //Crear comando
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Configurar parametros
            comando.Parameters.AddWithValue("nombre", TextBox5.Text);
            comando.Parameters.AddWithValue("resumen", TextBox6.Text);
            comando.Parameters.AddWithValue("claveCon", DropDownList2.SelectedValue);
            comando.Parameters.AddWithValue("fechaLanzamiento", TextBox7.Text);
            comando.Parameters.AddWithValue("claveJ", GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text);
            //Ejecutar comando, en un try-catch
            try
            {
                comando.ExecuteNonQuery();
                Label3.Text = "El juego se actualizó exitosamente";
            }
            catch(Exception ex)
            {
                Label3.Text = "No pudimos actualizar el juego " + ex.ToString();
            }
            conexion.Close();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //claveJ --> GridView SelectedIndex Celda 1
            String queryJuego = "delete from juego where claveJ=?";
            String queryEscriben = "delete from escriben where claveJ=?";
            String queryCritica = "delete from critica where claveC not in (select claveC from escriben)";

            //Abrir la conexion
            OdbcConnection conexion = new ConexionBD().con;
            //Crear comando
            OdbcCommand comando = new OdbcCommand(queryEscriben, conexion);
            //Configurar parametros
            comando.Parameters.AddWithValue("claveJ", GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text);
            //Correr el comando en un try-catch
            try
            {
                comando.ExecuteNonQuery();
                //Ejecutar el comando para juego
                comando = new OdbcCommand(queryJuego, conexion);
                //Configurar parametro
                comando.Parameters.AddWithValue("claveJ", GridView1.Rows[GridView1.SelectedIndex].Cells[1].Text);
                //Ejecutar el comando
                comando.ExecuteNonQuery();
                //Ejecutar el siguiente comando, para critica
                comando = new OdbcCommand(queryCritica, conexion);
                //Este no necesita parametros... solo lo corremos
                comando.ExecuteNonQuery();
                Label3.Text = "El juego se borró exitosamente";
                GridView1.Visible = false;
                Label4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
                TextBox7.Text = "";
                DropDownList2.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Label3.Text = "No pudimos eliminar el juego " + ex.ToString();
            }
            conexion.Close();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reportes.aspx");
        }
    }
}