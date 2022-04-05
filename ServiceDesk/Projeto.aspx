<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Projeto.aspx.cs" Inherits="Projeto" %>
<%@ Register Src="WUCSolucao.ascx" TagName="WUCSolucao" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Solução</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
      <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td align="center" height="1" valign="bottom">
              <table border="0" cellpadding="0" cellspacing="0" height="35" width="776">
                <tr>
                  <td align="left" valign="bottom">
                      <table align="left" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td class="aba_esquerda_off">&nbsp;
                          </td>
                          <td class="aba_centro_off">Solução</td>
                          <td class="aba_direita_off">&nbsp;
                          </td>
                        </tr>
                      </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <tr>
            <td align="center" colspan="2" valign="top">  
              <table width="690" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="left" valign="top">
	<uc1:WUCSolucao ID="WUCSolucao1" runat="server" />
	</td>
  </tr>
</table>

            </td>
          </tr>
          <tr>
            <td align="center" colspan="2" valign="top">
            </td>
          </tr>
        </table>
      </div>
    </form>
</body>
</html>