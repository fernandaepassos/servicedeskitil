<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="RequisicaoServico.aspx.cs" Inherits="RequisicaoChamado" %>

<%@ Register Src="WUCRequisicaoServico.ascx" TagName="WUCRequisicaoServico" TagPrefix="uc1" %>

<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="js/funcoes.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Requisição de Serviço</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0">
 <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr id="Tr1" runat="server">
                  <td height="0" align="center" valign="middle">
				   <div id="divMensagem" style="width: 80%;" runat="server" class="Mensagem" visible="false">
					<table width="776" border="0" cellspacing="5" cellpadding="0">
  <tr>
    <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
    <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
  </tr>
</table>    
</div>				  </td>
                </tr>
                <tr id="Tr2" runat="server">
                  <td height="1" align="center" valign="middle"><img src="" width="1" height="6" alt="" /></td>
                </tr>
                <tr>
                    <td align="center" valign="top"  colspan="2">                  <table width="97%"  border="0" cellspacing="0" cellpadding="0">
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
                                          <td align="left" valign="middle" class="tituloFont">Dados de Requisi&ccedil;&atilde;o de Servi&ccedil;o </td>
                                        </tr>
                                    </table></td>
                                    <td width="8" class="dir_top"></td>
                                  </tr>
                              </table></td>
                            </tr>
                            <tr>
                              <td colspan="3" align="center" valign="top" class="fundo_tabela">
							  <uc1:WUCRequisicaoServico id="WUCRequisicaoServico1" runat="server"></uc1:WUCRequisicaoServico>
							  </td>
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
