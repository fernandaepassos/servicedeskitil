<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCPessoaPerfilEstrutura.ascx.cs" Inherits="WUCPessoaPerfilEstrutura" %>
<script language="javascript">
<!--
function verifica() {
	if (confirm("Deseja mesmo excluir o Perfil da Pessoa?")) {
		return true;
	}
	else {
		return false;
	}
}
-->
</script>
<div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
  <table width="776" border="0" cellspacing="5" cellpadding="0">
    <tr>
      <td width="60" align="center" valign="bottom">
        <asp:Image ID="imgIcone" runat="server" /></td>
      <td align="center" valign="bottom">
        <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
    </tr>
  </table>
</div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <asp:Panel ID="pnPerfil" runat="server">
  <tr>
    <td height="20" align="center" valign="top">&nbsp;      </td>
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
                          <td align="left" valign="middle" class="tituloFont">Pessoa Perfil</td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td align="center" valign="top">
				  <table width="92%" border="0" cellspacing="2" cellpadding="0">
                    <tr>
                      <td align="left" width="71">Nome:</td>
                      <td align="left"><asp:DropDownList ID="ddlEstruturaCodigo" Width="300px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlEstruturaCodigo_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                  </table></td>
                </tr>
                <tr>
                  <td align="left" valign="top">
				  <asp:Panel ID="pnlPessoaPerfilEstrutura" runat="server" Height="150px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
            <asp:GridView ID="gvPessoaPerfilEstrutura" runat="server" Width="98%" CellPadding="4" GridLines="None" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" OnPageIndexChanging="gvPessoaPerfilEstrutura_OnPageIndexChanging" OnRowDataBound="gvPessoaPerfilEstrutura_RowDataBound">
              <FooterStyle CssClass="footerGrid" />
              <RowStyle BackColor="#F4FBFA" />
              <HeaderStyle CssClass="topoGrid" />
              <PagerStyle BackColor="#F4FBFA" HorizontalAlign="Center" />
              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
              <EditRowStyle BackColor="#2461BF" />
              <AlternatingRowStyle BackColor="White" />
              <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Right" ItemStyle-Width="40px" HeaderText="C&#243;digo">
                  <ItemTemplate>
                    <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_perfil_estrutura_codigo") %>' runat="server"></asp:Label>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" Width="50px" />
                  <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Aplica&#231;&#227;o">
                  <ItemTemplate>
                    <asp:Label ID="lblAplicacao" Text='<%# DataBinder.Eval(Container.DataItem, "aplicacao_codigo")%>' runat="server"></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Pessoa">
                  <ItemTemplate>
                    <asp:Label ID="lblPessoa" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo")%>' runat="server"></asp:Label>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Left" Width="350px" />
                  <ItemStyle HorizontalAlign="Left" Width="350px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo de Usu&#225;rio">
                  <ItemTemplate>
                    <asp:Label ID="lblTipoUsuario" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_usuario_codigo")%>' runat="server"></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:Label ID="lblTipoUsuario" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_usuario_codigo") %>' Visible="False"></asp:Label>
                    <asp:DropDownList ID="ddlTipoUsuario" Width="150" Runat="server"></asp:DropDownList>
                  </EditItemTemplate>
                </asp:TemplateField>
              </Columns>
            </asp:GridView>
			</asp:Panel>
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
    </table></td>
  </tr>
  </asp:Panel>
  <asp:Panel ID="pnPerfilInclui" runat="server">
  <tr>
    <td height="20" align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="10" align="center" valign="top"><table width="97%"  border="0" cellspacing="0" cellpadding="0">
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
                          <td width="20" align="center" valign="middle">&nbsp;</td>
                          <td align="left" valign="middle" class="tituloFont">Novo Perfil</td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td align="center" valign="top">
                      <table width="92%" border="0" cellspacing="2" cellpadding="0">
                        <tr>
                          <td align="left" width="71">Empresa:</td>
                          <td align="left">
                            <asp:DropDownList ID="ddlEstruturaNova" AutoPostBack="true" Width="300px" runat="server" OnSelectedIndexChanged="ddlEstruturaNova_SelectedIndexChanged"></asp:DropDownList>
                          </td>
                          <td width="71" align="left">Pessoa:</td>
                          <td align="left"><asp:DropDownList ID="ddlPessoaNova" Width="300px" runat="server" OnSelectedIndexChanged="ddlPessoaNova_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td align="center" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="0">
                        <tr>
                          <td colspan="4">
                            <asp:Panel ID="pnlNovoPerfil" runat="server" Height="150px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
							<asp:GridView ID="gvNovoPerfil" runat="server" Width="98%" CellPadding="4" GridLines="None" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" OnRowDataBound="gvNovoPerfil_RowDataBound">
                              <FooterStyle CssClass="footerGrid" />    
                              <RowStyle BackColor="#F4FBFA" />
                              <HeaderStyle CssClass="topoGrid" />    
                              <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />    
                              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#2461BF" /> <AlternatingRowStyle BackColor="White" />
                              <Columns>
                              <asp:TemplateField>
                              <ItemTemplate>
                                <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "perfil_estrutura_codigo") %>' runat="server"></asp:Label>
                                <asp:CheckBox ID="cbCodigo" runat="server" />    
                          </ItemTemplate>
                              <ItemStyle HorizontalAlign="Right" Width="50px" />    
                          </asp:TemplateField> <asp:TemplateField HeaderText="Aplica&#231;&#227;o">
                          <ItemTemplate>
                            <asp:Label ID="lblAplicacao" Text='<%# DataBinder.Eval(Container.DataItem, "aplicacao_codigo")%>' runat="server"></asp:Label>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" Width="300px" />
                          <ItemStyle HorizontalAlign="Left" Width="300px" />
                          </asp:TemplateField> <asp:TemplateField HeaderText="Tipo de Usu&#225;rio">
                          <ItemTemplate>
                            <asp:Label ID="lblTipoUsuario" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_usuario_codigo")%>' runat="server"></asp:Label>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" Width="300px" />
                          <ItemStyle HorizontalAlign="Left" Width="300px" />
                          </asp:TemplateField>
                              </Columns>
                            </asp:GridView> </asp:Panel>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="4">
                            <asp:Button ID="btnSalvar" Text="Salvar" CssClass="botao" OnClick="salvaPerfil" runat="server" />    
                      </td>
                        </tr>
                    </table></td>
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
    </table></td>
  </tr>
  <tr>
    <td align="center" valign="top">
      
    </td>
  </tr>
  </asp:Panel>
</table>
