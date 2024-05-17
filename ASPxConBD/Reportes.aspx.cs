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
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Verificar que el usuario haya hecho login valido
            //Confirmar que las variables de sesion existen
            //if (Session["claveAdmin"] == null || Session["nombreAdmin"] == null)
            //{
                //Las variables de sesion no existen... terminar sesion y 
                //redireccionar
            //    Session.Abandon();
            //    Response.Redirect("loginAdmin.aspx");
            //}

            if (DropDownList1.Items.Count == 0)
            {
                String query = "select claveU,nombre from usuario";
                //Abrir conexion
                OdbcConnection conexion = new ConexionBD().con;
                //Instanciar el comando
                OdbcCommand comando = new OdbcCommand(query, conexion);
                //No lleva parametros...
                //Ejecutamos el comando con un lector, porque es un select
                OdbcDataReader lector = comando.ExecuteReader();
                //Configuramos dropdownlist
                DropDownList1.DataSource = lector;
                //Configurar el texto del dropdownlist
                DropDownList1.DataTextField = "nombre";//como viene en el select
                //Configurar la llave primaria en el dropdownlist
                DropDownList1.DataValueField = "claveU";//como viene en el select
                //Ligar la configuración al dropdownlist
                DropDownList1.DataBind();
                //Cerrar lector y cerrar la conexion
                lector.Close();
                conexion.Close();
            }
            if (DropDownList2.Items.Count == 0)
            {
                String query = "select claveU,nombre from usuario";
                //Abrir conexion
                OdbcConnection conexion = new ConexionBD().con;
                //Instanciar el comando
                OdbcCommand comando = new OdbcCommand(query, conexion);
                //No lleva parametros...
                //Ejecutamos el comando con un lector, porque es un select
                OdbcDataReader lector = comando.ExecuteReader();
                //Configuramos dropdownlist
                DropDownList2.DataSource = lector;
                //Configurar el texto del dropdownlist
                DropDownList2.DataTextField = "nombre";//como viene en el select
                //Configurar la llave primaria en el dropdownlist
                DropDownList2.DataValueField = "claveU";//como viene en el select
                //Ligar la configuración al dropdownlist
                DropDownList2.DataBind();
                //Cerrar lector y cerrar la conexion
                lector.Close();
                conexion.Close();
            }
            if (DropDownList3.Items.Count == 0)
            {
                String query = "select claveCon,nombre from consola";
                //Abrir conexion
                OdbcConnection conexion = new ConexionBD().con;
                //Instanciar el comando
                OdbcCommand comando = new OdbcCommand(query, conexion);
                //No lleva parametros...
                //Ejecutamos el comando con un lector, porque es un select
                OdbcDataReader lector = comando.ExecuteReader();
                //Configuramos dropdownlist
                DropDownList3.DataSource = lector;
                //Configurar el texto del dropdownlist
                DropDownList3.DataTextField = "nombre";//como viene en el select
                //Configurar la llave primaria en el dropdownlist
                DropDownList3.DataValueField = "claveCon";//como viene en el select
                //Ligar la configuración al dropdownlist
                DropDownList3.DataBind();
                //Cerrar lector y cerrar la conexion
                lector.Close();
                conexion.Close();
            }
            if (DropDownList4.Items.Count == 0)
            {//Dropdown calificaciones
                for(int i = 1; i < 11; i++)
                {
                    DropDownList4.Items.Add(i.ToString());
                }
            }
            //Llenar el checkboxlist de campos para el reporte flexible
            if (CheckBoxList1.Items.Count == 0)
            {//El value va a quedar como el nombre de la columna en la bd!!!
                CheckBoxList1.Items.Add(new ListItem("Titulo", "titulo as 'Titulo'"));
                CheckBoxList1.Items.Add(new ListItem("Contenido", "contentido"));
                CheckBoxList1.Items.Add(new ListItem("Calificación", "calif as 'Calificación'"));
                CheckBoxList1.Items.Add(new ListItem("Fecha de publicación", "critica.fecha as 'fecha publicación'"));
                CheckBoxList1.Items.Add(new ListItem("Nombre del juego", "juego.nombre as 'Juego'"));
                CheckBoxList1.Items.Add(new ListItem("Nombre de la consola", "consola.nombre as 'Consola'"));
                CheckBoxList1.Items.Add(new ListItem("Nombre del usuario", "usuario.nombre as 'Usuario'"));
                CheckBoxList1.Items.Add(new ListItem("Correo del usuario", "usuario.email as 'Correo'"));
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("loginAdmin.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioAdministrador.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            String query = "select titulo,contentido,calif,critica.fecha,juego.nombre as 'Juego',consola.nombre as 'Consola',usuario.nombre,usuario.email "
            + "from consola inner join juego on consola.claveCon = juego.claveCon "
            + "inner join escriben on juego.claveJ = escriben.claveJ "
            + "inner join critica on escriben.claveC = critica.claveC "
            + "inner join usuario on escriben.claveU = usuario.claveU "
            + "where usuario.claveU = ?";//El parametro es la clave del usuario del dropdown
            //Abrir la conexion
            OdbcConnection conexion = new ConexionBD().con;
            //Instanciar el comando
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Configuramos el parámetro
            comando.Parameters.AddWithValue("claveU", DropDownList1.SelectedValue);
            //Ejecutamos el comando y guardamos el resultado en un lector
            OdbcDataReader lector = comando.ExecuteReader();
            //Configuramos el gridview para mostrar el reporte
            GridView1.DataSource = lector;
            //Guardamos la configuracion del gridview
            GridView1.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String select = "select ";
            String from = " from consola inner join juego on consola.claveCon=juego.claveCon "
                +"inner join escriben on juego.claveJ = escriben.claveJ "
                +"inner join critica on escriben.claveC = critica.claveC "
                +"inner join usuario on escriben.claveU = usuario.claveU ";
            //and usuario.claveU = 1
            //and consola.claveCon = 10
            //and calif = 7
            String where = "where 1=1 ";

            //Abrir conexion
            OdbcConnection conexion = new ConexionBD().con;
            //Ver qué campos están seleccionados
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {
                if (CheckBoxList1.Items[i].Selected == true)
                {//Si está seleccionado, lo pegamos al select
                    select = select + CheckBoxList1.Items[i].Value + ",";
                }
            }
            if(select=="select ")
            {//El select está vacío... mostramos todos los campos
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    select = select + CheckBoxList1.Items[i].Value + ",";
                }
            }
            //Hay que quitar la coma del final, de todas formas
            select = select.Substring(0, select.Length - 1);
            select = select + " ";//<--para asegurar que no quede pegado con from
            
            //Ver qué filtros están seleccionados
            if (CheckBox1.Checked == true)
            {
                where = where + " and usuario.claveU = ? ";
            }
            if (CheckBox2.Checked == true)
            {
                where = where + " and consola.claveCon = ? ";
            }
            if (CheckBox3.Checked == true)
            {
                where = where + " and calif= ? ";
            }
            String query = select + from + where;
            OdbcCommand comando = new OdbcCommand(query, conexion);
            //Configurar los parametros
            if (CheckBox1.Checked == true)
            {
                comando.Parameters.AddWithValue("claveU",DropDownList2.SelectedValue);
            }
            if (CheckBox2.Checked == true)
            {
                comando.Parameters.AddWithValue("claveCon",DropDownList3.SelectedValue);
            }
            if (CheckBox3.Checked == true)
            {
                comando.Parameters.AddWithValue("calif", DropDownList4.SelectedValue);
            }
            //Ejecutamos el comando en un lector porque es un select
            OdbcDataReader lector = comando.ExecuteReader();
            //Configuramos el gridview
            GridView2.DataSource = lector;
            //Guardamos la configuracion en el gridview
            GridView2.DataBind();
            //Cerramos lector y conexion
            lector.Close();
            conexion.Close();
        }
    }
}