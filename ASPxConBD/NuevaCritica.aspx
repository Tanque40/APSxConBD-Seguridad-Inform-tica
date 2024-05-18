<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NuevaCritica.aspx.cs" Inherits="ASPxConBD.NuevaCritica" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<meta http-equiv="Content-Security-Policy" 
    content="base-uri 'self';
        default-src 'self';
        img-src https://*;
        form-action https://*;
        script-src 'self';
        style-src 'self';
        upgrade-insecure-requests;"
 />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Salir" />
            <br />
            <br />
            Clave juego:
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            Nombre juego:
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            Resumen:
            <asp:Label ID="Label3" runat="server"></asp:Label>
            <br />
            Consola:
            <asp:Label ID="Label4" runat="server"></asp:Label>
            <br />
            Fecha de lanzamiento:
            <asp:Label ID="Label5" runat="server"></asp:Label>
            <br />
            <br />
            Ingresa tu reseña:<br />
            Título:
            <asp:TextBox ID="TextTitulo" runat="server" Width="335px"></asp:TextBox>
            <br />
            Contenido:<br />
            <asp:TextBox ID="TextContenido" runat="server" Height="174px" Rows="10" TextMode="MultiLine" Width="398px"></asp:TextBox>
            <br />
            Calificación:
            <asp:DropDownList ID="DropDownCalif" runat="server">
            </asp:DropDownList>
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Salvar reseña" />
            <br />
            <asp:Label ID="LabelResultado" runat="server"></asp:Label>
            <br />
        </div>
    </form>
</body>
</html>
