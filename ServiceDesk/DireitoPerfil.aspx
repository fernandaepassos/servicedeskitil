<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DireitoPerfil.aspx.cs" Inherits="DireitoPerfil" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Direitos do Perfil</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
        <table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center" valign="middle">
	<div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
		    <table width="776" border="0" cellspacing="5" cellpadding="0">
                <tr>
                    <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
                    <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F" Width="100%"></asp:Label></td>
                </tr>
            </table>    
        </div>
	</td>
  </tr>
  <tr>
    <td height="20" align="center" valign="middle">&nbsp;</td>
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
                          <td align="left" valign="middle" class="tituloFont">Direitos dos Perfis </td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela">
			  <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="center" valign="top">
                    <table width="770" border="0" cellpadding="0" cellspacing="2">
                      <tr>
                        <td align="right"> </td>
                        <td align="right"> </td>
                      </tr>
                      <tr align="left" valign="middle">
                        <td width="71">
                          <asp:Label ID="lblAplicacao" runat="server" Text="Aplica&ccedil;&atilde;o:" CssClass="dataFont"></asp:Label></td>
                        <td>
                          <asp:DropDownList ID="ddlAplicacao" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAplicacao_SelectedIndexChanged" Width="680px"></asp:DropDownList></td>
                      </tr>
                      <tr align="left" valign="middle">
                        <td>
                          <asp:Label ID="lblTipoUsuario" runat="server" Text="Perfil:" CssClass="dataFont"></asp:Label></td>
                        <td>
                          <asp:DropDownList ID="ddlTipoUsuario" runat="server" Width="680px"></asp:DropDownList>                        </td>
                      </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="center" valign="middle"> &nbsp;
                            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnBusca_Click" CssClass="botao" Width="58px" />                
                            <asp:Button ID="btnGravar" runat="server" Text="Salvar" OnClick="btnGravar_Click" CssClass="botao" Width="58px" /></td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td class="menu"align="left" valign="bottom">
                    <asp:Panel ID="plTreview" runat="server" CssClass="dataGrid" Height="364px" Width="100%" ScrollBars="Vertical" Visible="False">
					<asp:TreeView ID="tvFuncao" runat="server" Height="199px" Width="267px" ShowCheckBoxes="All" CssClass="menu"> <ParentNodeStyle CssClass="menu" /> <SelectedNodeStyle CssClass="menu" /> <RootNodeStyle CssClass="menu" /> <NodeStyle CssClass="menu" /> <LeafNodeStyle CssClass="menu" /> <HoverNodeStyle CssClass="menu" /> </asp:TreeView> </asp:Panel>
                  </td>
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
    </table>	</td>
  </tr>
</table>
</form>
</body>
</html>