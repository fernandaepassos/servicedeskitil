<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BaseConhecimento.aspx.cs" Inherits="BaseConhecimento2" %>

<%@ Register Src="WUCKBPesquisa.ascx" TagName="WUCKBPesquisa" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

    <SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Base de conhecimento</title>        
    <link rel="stylesheet" href="css/estilo.css" type="text/css">         
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0">
      <div>      
        <table width="100%"  border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td align="center" valign="top"><img src="" width="1" height="6" alt="" /></td>
          </tr>
          <tr>
            <td align="center" valign="top"><table width="97%"  border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaPadrao">
                    <!--DWLayoutTable-->
                    <tr>
                      <td height="22" colspan="3">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaCabecalho">
                          <!--DWLayoutTable-->
                          <tr>
                            <td width="8" height="22" class="esq_top">&nbsp;</td>
                            <td align="left" valign="top" class="centro_top"><table width="100%" height="22"  border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="20" align="center" valign="middle">&nbsp;</td>
                                  <td align="left" valign="middle" class="tituloFont">Base de Conhecimento </td>
                                </tr>
                            </table></td>
                            <td width="8" class="dir_top"></td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td colspan="3" align="center" valign="top" class="fundo_tabela"> <uc1:WUCKBPesquisa ID="WUCKBPesquisa1" runat="server" /> </td>
                    </tr>
                    <tr>
                      <td colspan="3" align="left" valign="top">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaRodape">
                          <!--DWLayoutTable-->
                          <tr>
                            <td width="8" height="7" class="esq_down"></td>
                            <td valign="top" class="centro_down"></td>
                            <td width="8" class="dir_down"></td>
                          </tr>
                      </table></td>
                    </tr>
                </table></td>
              </tr>
            </table></td>
          </tr>
        </table>
      </div>
    </form>
</body>
</html>