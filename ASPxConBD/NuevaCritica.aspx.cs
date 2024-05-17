using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Importar biblioteca de ODBC
using System.Data.Odbc;

namespace ASPxConBD
{
    public partial class NuevaCritica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Acceder a las variables de sesion
            //Si las variables de sesion no existen, quiere decir
            //que el usuario no pudo hacer login
            if (Session["nombreUsuario"] == null || Session["claveU"] == null)
            {//Cerramos sesion y redireccionamos al login
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            //Cargar la informacion
            String query = "select juego.nombre,resumen,consola.nombre,fechaLanzamiento "
                + " from juego inner join consola on juego.claveCon = consola.claveCon "
                + " where juego.claveJ = ?";
            //Abrir conexion a la bd
            OdbcConnection conexion = new ConexionBD().con;
            //Crear el comando
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Pasarle el parametro juego.ClaveJ al comando (esta en la variable
            //de sesion que viene de PaginaPrincipal.aspx
            comando.Parameters.AddWithValue("claveJuego", Session["llaveJuego"].ToString());
            //Ejecutar el comando, como es un select, guardo el resultado en un reader
            OdbcDataReader lector = comando.ExecuteReader();
            //Verificar si el lector trae datos
            if (lector.HasRows)
            {
                //Si tiene datos, pasar al primer renglon
                lector.Read();
                //Sacar los datos del lector y los cargamos en los labels correspondientes
                //Llave de juego
                Label1.Text = Session["llaveJuego"].ToString();
                Label2.Text = lector.GetString(0); //Columna cero, ahi viene el nombre del juego
                Label3.Text = lector.GetString(1); //Columna 1, ahi viene el resumen
                Label4.Text = lector.GetString(2); //Columna 2, ahi viene la consola
                Label5.Text = lector.GetDateTime(3).ToString(); //Columna 3, ahi viene la fecha
            }
            lector.Close();
            conexion.Close();
            //Llenar el dropdownlist de calificaciones
            if (DropDownCalif.Items.Count == 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    DropDownCalif.Items.Add(i.ToString());
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Variable para la llave primaria de la tabla critica
            int llave = 1;
            String queryMaximo = "select max(claveC) from critica";
            //Llave primaria, titulo, contenido, calificacion
            String insertCritica = "insert into critica values(?,'"+TextTitulo.Text+"',CURRENT_TIMESTAMP,?,?)";
            //ClaveUsuario --> sesion claveU, ClaveJuego --> sesion llaveJuego, ClaveCritica --> llave
            String insertEscriben = "insert into escriben values(?,?,?)";

            //Abrir conexion a bd
            OdbcConnection conexion = new ConexionBD().con;

            //***** Generar la llave primaria *****
            //Averiguar la llave primaria maxima de la tabla
            OdbcCommand comando = new OdbcCommand(queryMaximo, conexion);
            //No configuro parametros, no los necesita
            //Ejecutamos el comando
            OdbcDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                //Pasamos al primer renglon
                lector.Read();
                try
                {
                    llave = lector.GetInt32(0); //Solo hay columna 0 para este select
                    llave++;
                }
                catch (Exception ex)
                {
                    llave = 1;
                }
            }
            lector.Close();
            //***** Generar la llave primaria *****

            //***** Insertar en la tabla Critica *****
            //Ya existe un objeto comando, ya hay una conexion abierta
            comando = new OdbcCommand(insertCritica, conexion);
            //Configurar los parametros del comando
            comando.Parameters.AddWithValue("llavePrimaria", llave);
            //comando.Parameters.AddWithValue("titulo", TextTitulo.Text);
            comando.Parameters.AddWithValue("contenido", TextContenido.Text);
            comando.Parameters.AddWithValue("calif", DropDownCalif.SelectedItem.Text);
            //Correr el comando, como es un insert, corremos NonQuery
            //Hay que hacer un try-catch
            try
            {
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //No pudimos insertar en la tabla... avisarle al usuario
                LabelResultado.Text = "Hubo un error, no pudimos insertar la crítica"+ex.ToString();
            }
            //***** Insertar en la tabla Critica *****

            //***** Insertar en la tabla escriben *****
            //Ya existe un objeto comando, lo reinstanciamos, la conexion también esta abierta
            comando = new OdbcCommand(insertEscriben, conexion);
            //Configurar los parámetros
            comando.Parameters.AddWithValue("llaveUsuario", Session["claveU"].ToString());
            comando.Parameters.AddWithValue("llaveJuego", Session["llaveJuego"].ToString());
            comando.Parameters.AddWithValue("llaveCritica", llave.ToString());
            //Ejecutar el comando en su try-catch
            try
            {
                //Usamos NonQuery porque es un insert
                comando.ExecuteNonQuery();
                //Si llegamos hasta aqui todo salio bien
                //Limpiar la interfaz
                TextTitulo.Text = "";
                TextContenido.Text = "";
                DropDownCalif.SelectedIndex = 0; //Seleccionamos el primer elemento del dropdown
                //Mensaje amigable al usuario de que todo salió bien
                LabelResultado.Text = "Reseña dada de alta, gracias " + Session["nombreUsuario"];
            }
            catch(Exception ex)
            {
                LabelResultado.Text= "Hubo un error, no pudimos insertar la crítica"+ex.ToString();
            }
            //***** Insertar en la tabla escriben *****
            //Cerramos la conexion
            conexion.Close();
            
        }
    }
}