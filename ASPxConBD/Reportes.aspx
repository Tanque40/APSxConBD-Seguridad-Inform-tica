<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="ASPxConBD.Reportes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Reportes</title>
       <meta http-equiv="Content-Security-Policy" 
   content="base-uri 'self';
       default-src 'self';
       img-src https://*;
       form-action https://*;
       script-src 'self';
       style-src 'self';
       frame-ancestors 'self';
       upgrade-insecure-requests;"
/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Salir" />
&nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Página principal" />
            <br />
            <br />
            Reporte de críticas por usuario<br />
            Selecciona un usuario:
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Generar reporte" />
            <br />
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <br />
            Reporte flexible<br />
            Selecciona los campos del reporte:<br />
            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
            </asp:CheckBoxList>
            <br />
            <br />
            Selecciona los filtros:<br />
            <asp:CheckBox ID="CheckBox1" runat="server" />
            Usuario:
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox2" runat="server" />
            Consola:
            <asp:DropDownList ID="DropDownList3" runat="server">
            </asp:DropDownList>
&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox3" runat="server" />
            Calificación:
            <asp:DropDownList ID="DropDownList4" runat="server">
            </asp:DropDownList>
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Generar reporte" />
            <br />
            <br />
            <asp:GridView ID="GridView2" runat="server">
            </asp:GridView>
            <br />
            <asp:Label ID="Label2" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
