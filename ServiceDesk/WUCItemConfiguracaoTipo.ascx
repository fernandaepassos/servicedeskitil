<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCItemConfiguracaoTipo.ascx.cs" Inherits="WUCItemConfiguracaoTipo" %>
<script language="javascript">
<!--
function verifica() {
	if (confirm("Deseja mesmo excluir o tipo do Item de Configuração?")) {
		return true;
	}
	else {
		return false;
	}
}
-->
</script>

<div>
  <table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td>
        <div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem"
          visible="false">
          <table width="776" border="0" cellspacing="5" cellpadding="0">
            <tr>
              <td width="60" align="center" valign="bottom">
                <asp:Image ID="imgIcone" runat="server" /></td>
              <td align="center" valign="bottom">
                <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
            </tr>
          </table>
        </div>
      </td>
    </tr>
    <tr id="Tr1" runat="server">
      <td height="20" align="center" valign="top">&nbsp;      </td>
    </tr>
    <tr id="Tr2" runat="server">
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
                            <td align="left" valign="middle" class="tituloFont">Dados de Requisi&ccedil;&atilde;o de Servi&ccedil;o </td>
                          </tr>
                      </table></td>
                      <td width="8" class="dir_top"></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td colspan="3" align="center" valign="top" class="fundo_tabela">
				<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="22%" align="left" valign="top" class="dataTree">
	<asp:Panel ID="pnTreeView" ScrollBars="Both" Height="290" runat="server" CssClass="dataGrid">
                          <asp:TreeView ID="trvTipo"
                            PopulateNodesFromClient="true" ShowLines="true" ShowExpandCollapse="true" runat="server"
                            OnTreeNodePopulate="trvTipo_TreeNodePopulate" CssClass="menu">
                            <ParentNodeStyle CssClass="menu" />
                            <SelectedNodeStyle CssClass="menu" />
                            <RootNodeStyle CssClass="menu" />
                            <NodeStyle CssClass="menu" />
                            <LeafNodeStyle CssClass="menu" />
                            <HoverNodeStyle CssClass="menu" />
                          </asp:TreeView>
                        </asp:Panel>	</td>
    <td align="center" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="center" valign="top"><table width="92%" border="0" cellpadding="0" cellspacing="2">
          <tr>
            <td width="200" align="left"> Tipo Item de Configura&ccedil;&atilde;o sup.: </td>
            <td colspan="3" align="left">
              <asp:DropDownList ID="ddlSuperior" Width="440" runat="server"> </asp:DropDownList></td>
          </tr>
          <tr>
            <td align="left"> Descri&ccedil;&atilde;o:</td>
            <td colspan="3" align="left" valign="top">
              <asp:TextBox ID="txtDescricao" Width="436px" runat="server" MaxLength="200" CssClass="campo_texto"></asp:TextBox>
            </td>
          </tr>
          <tr>
            <td align="left"> Prefixo:</td>
            <td width="150" align="left">
              <asp:TextBox ID="txtPrefixo" Width="150" runat="server" CssClass="campo_texto"></asp:TextBox></td>
            <td width="50" align="left"> Situa&ccedil;&atilde;o:</td>
            <td align="left">
              <asp:DropDownList ID="ddlStatus" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt"
                    Width="100"> </asp:DropDownList></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td align="left" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="2">          
          <tr>
            <td colspan="6" align="left">
              <asp:Panel ID="pnAtributo" ScrollBars="Both" Height="200" Width="100%" runat="server" CssClass="dataGrid">
                <asp:CheckBoxList ID="cblAtributo" RepeatColumns="5" RepeatDirection="Horizontal" runat="server"> </asp:CheckBoxList>
              </asp:Panel>
            </td>
          </tr>
          <tr>
            <td height="19" colspan="6" align="center" valign="middle">
              <asp:Button ID="btnSalva" CssClass="botao" Text="Salvar" runat="server" OnClick="btnSalva_Click" />        
              <asp:Button ID="btnNovo" CssClass="botao" Text="Novo" runat="server" OnClick="btnNovo_Click" />        
              <asp:Button ID="btnExclui" CssClass="botao" Text="Excluir" runat="server" OnClick="btnExclui_Click" />        
    </td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
</table>

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

