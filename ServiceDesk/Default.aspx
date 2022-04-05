<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Help Desk ITIL Compliance</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css" />
    <script type="text/javascript" src="js/jquery-1.12.0.min.js"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

    <script type="text/javascript">
        //Centrar botão login
        $(function () {
            var td = $('input[value="Entrar"]').parent();
            $(td).attr("align", "center");
        });
    </script>
</head>
<body style="background-color: #D7ECED;">
    <form id="form1" runat="server" style="margin: 0px">
        <div style="align-items: center; display: flex; flex-direction: column;">
            <div class="header">
                <img alt="logo" src="images/default/logo.gif" />
                <span>HELP DESK ITIL</span>
            </div>
            <div class="loginBox">
                <asp:TextBox ID="txtMessage" runat="server" ForeColor="red" Width="700px" />  
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="LoginUser" CssClass="validator" />
                <div>
                    <asp:Login ID="LoginUser" runat="server"
                        BorderPadding="4"
                        Font-Names="Verdana" Font-Size="1em"
                        ForeColor="#333333"
                        Height="150px" Width="400px"
                        TitleText=""
                        Orientation="Vertical"
                        LoginButtonType="Button"
                        UserNameLabelText="Usuário: " DisplayRememberMe="false"
                        PasswordLabelText="Senha: " PasswordRequiredErrorMessage="Informe a senha" UserNameRequiredErrorMessage="Informe o login"
                        LoginButtonText="Entrar" OnAuthenticate="LoginUser_Authenticate" OnLoggedIn="Login1_LoggedIn">
                        <InstructionTextStyle Font-Italic="True" ForeColor="Red" />
                        <LoginButtonStyle CssClass="login-center" />
                        <TextBoxStyle Font-Size="1em" Width="200px" />
                        <TitleTextStyle BackColor="white" Font-Bold="True" Font-Size="1.5em" ForeColor="#49adb" Height="40px" />
                        <CheckBoxStyle CssClass="cbRemember" />
                    </asp:Login>
                </div>
            </div>
            <div class="footer">
                <span class="default footer">Sistema de Help Desk ITIL Compliance 2017 ©</span>
            </div>
        </div>
    </form>
</body>
</html>
