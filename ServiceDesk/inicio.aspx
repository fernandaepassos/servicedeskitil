<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inicio.aspx.cs" Inherits="inicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Bem-Vindo</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
    
<script language=javascript>
<!--
self.moveTo(0,0);
self.resizeTo(screen.availWidth,screen.availHeight);
self.focus();
//-->
</script>

<style type="text/css">
<!--
.style1 {
	color: #2BA7A7;
	font-weight: bold;
}
-->
</style>
</head>
<body leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0">
    <div>      
      <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td height="30" align="center" valign="bottom">&nbsp;</td>
        </tr>
        <tr>
          <td align="center" valign="top"><table width="95%" height="100%"  border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="216" align="center" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabela">
                          <!--DWLayoutTable-->
                          <tr>
                            <td height="22" colspan="3">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaCabecalho">
                                <!--DWLayoutTable-->
                                <tr>
                                  <td width="8" height="22" class="esq_top">&nbsp;</td>
                                  <td align="left" valign="top" class="centro_top"><table width="100%" height="22"  border="0" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="center" valign="middle"><img src="images/imgPerfil.gif" width="14" height="13" /></td>
                                        <td width="90%" align="left" valign="middle">Meu Perfil</td>
                                      </tr>
                                  </table></td>
                                  <td width="8" class="dir_top"></td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td colspan="3" align="left" valign="top" class="fundo_tabela"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td align="left" valign="top">
                                    <asp:PlaceHolder ID="phMenu" runat="server"></asp:PlaceHolder>
                                  </td>
                                </tr>
                                <tr>
                                  <td height="18">&nbsp;</td>
                                </tr>
                            </table></td>
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
                <td align="right" valign="top"><table width="97%"  border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1">
                          <!--DWLayoutTable-->
                          <tr>
                            <td height="22" colspan="3">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table2">
                                <!--DWLayoutTable-->
                                <tr>
                                  <td width="8" height="22" class="esq_top">&nbsp;</td>
                                  <td align="left" valign="top" class="centro_top"><table width="100%" height="22"  border="0" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td width="20" align="center" valign="middle"><img src="images/imgGerenciamento.gif" width="14" height="13" /></td>
                                        <td align="left">M&oacute;dulos ITIL </td>
                                      </tr>
                                  </table></td>
                                  <td width="8" class="dir_top"></td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td colspan="3" align="left" valign="top" class="fundo_tabela"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td><table width="100%" height="26"  border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td width="10"><img src="images/serv_esq.gif" width="10" height="27" /></td>
                                      <td align="center" valign="middle" background="images/fundo.gif"><span class="style1">service desk </span></td>
                                    </tr>
                                </table></td>
                                <td width="10%"><table width="100%" height="26"  border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td width="50%" align="right" background="images/fundo.gif"><img src="images/serv_direita.gif" width="10" height="27" /></td>
                                      <td width="1">&nbsp;</td>
                                      <td width="50%" bgcolor="#C0E2D1">&nbsp;</td>
                                    </tr>
                                </table></td>
                                <td bgcolor="#C0E2D1">&nbsp;</td>
                              </tr>
                              <tr>
                                <td width="45%" height="210" align="right" valign="middle" bgcolor="#C4E5F9"><img src="images/lado_azul.gif" width="283" height="210" /></td>
                                <td width="10%" rowspan="2" valign="top"><table width="100%" height="246"  border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td background="images/fundo_azul.gif">&nbsp;</td>
                                      <td width="2">&nbsp;</td>
                                      <td background="images/fundo_verde.gif">&nbsp;</td>
                                    </tr>
                                </table></td>
                                <td width="45%" align="left" bgcolor="#C0E2D1"><img src="images/lado_verde.gif" width="243" height="210" /></td>
                              </tr>
                              <tr>
                                <td height="36" align="left" valign="bottom" bgcolor="#C4E5F9"><img src="images/txt_azul.gif" width="207" height="23" /></td>
                                <td height="36" align="right" valign="bottom" bgcolor="#C0E2D1"><img src="images/txt_verde.gif" width="208" height="22" /></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td colspan="3" align="left" valign="top">
                              <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table3">
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
                  </table>
                    <br />
                </td>
              </tr>
          </table></td>
        </tr>
      </table>
    </div> 
    </form>
</body>
</html>