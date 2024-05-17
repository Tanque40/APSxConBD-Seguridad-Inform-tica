<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioAdministrador.aspx.cs" Inherits="ASPxConBD.InicioAdministrador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta http-equiv="Content-Security-Policy" 
    content="base-uri 'self';
        default-src 'self';
        img-src data: https:;
        object-src 'none';
        script-src 'self';
        style-src 'self';
        upgrade-insecure-requests;"
 />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Salir" />
            &nbsp;<asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Reportes" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <br />
            Gestión de catálogo juegos<br />
            <br />
            Nuevo juego<br />
            <br />
            Nombre:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            Resumen:
            <br />
            <asp:TextBox ID="TextBox2" runat="server" Height="161px" Rows="10" TextMode="MultiLine" Width="237px"></asp:TextBox>
            <br />
            Consola:
            <asp:DropDownList ID="DropDownList1" runat="server" >
            </asp:DropDownList>
            <br />
            Fecha de lanzamiento: <asp:TextBox ID="TextBox3" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <asp:Button ID="Button2" runat="server" Text="Agregar juego" OnClick="Button2_Click" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <hr />
            Editar/Borrar juego<br />
            <br />
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
            <asp:Button ID="Button3" runat="server" Text="Buscar Juego" OnClick="Button3_Click" />
            <br />
            <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" >
            </asp:GridView>
            <br />
            Clave:
            <asp:Label ID="Label4" runat="server"></asp:Label>
            <br />
            Nombre:
            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
            <br />
            Resumen:
            <br />
            <asp:TextBox ID="TextBox6" runat="server" Height="161px" Rows="10" TextMode="MultiLine" Width="237px"></asp:TextBox>
            <br />
            Consola:
            <asp:DropDownList ID="DropDownList2" runat="server" >
            </asp:DropDownList>
            <br />
            Fecha de lanzamiento: <asp:TextBox ID="TextBox7" runat="server" TextMode="Date"></asp:TextBox>
            <br />
            <asp:Button ID="Button4" runat="server" Text="Editar juego" OnClick="Button4_Click" />
&nbsp;<asp:Button ID="Button5" runat="server" Text="Borrar juego" OnClick="Button5_Click" />
            <br />
            <asp:Label ID="Label3" runat="server"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
