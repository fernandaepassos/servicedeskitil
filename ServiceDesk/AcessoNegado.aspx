<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AcessoNegado.aspx.cs" Inherits="AcessoNegado" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk ITIL Compliance :: Acesso Negado!</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr id="Tr2" runat="server">
              <td height="1">&nbsp;</td>
            </tr>
            <tr id="Tr3" runat="server">
              <td height="1"><table class="tabela_aviso" width="100%" height="200">
                <tr>
                  <td align="right" valign="middle">&nbsp;</td>
                  <td width="79" align="right" valign="middle"><img src="images/icon_erroAcesso.gif" width="79" height="70"></td>
                  <td width="350" align="left">
                  <div align="center">Seu acesso a essa funcionalidade foi negado.<br>
                   Em caso de dúvidas entre em contato com a central de Tecnologia<br>
                  <asp:Label id="lblerro" runat="server"></asp:Label></div>
                  </td>
                  <td align="left">&nbsp;</td>
                </tr>
              </table></td>
            </tr>
        </table>
      </div> 
        <br>
</form>
</body>
</html>