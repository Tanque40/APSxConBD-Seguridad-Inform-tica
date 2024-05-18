<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginAdmin.aspx.cs" Inherits="ASPxConBD.loginAdmin" %>

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
            Login administrador<br />
            <br />
            Correo:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            Contraseña:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Entrar" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
