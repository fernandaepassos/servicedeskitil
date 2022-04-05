<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCItemConfiguracaoAtributo.ascx.cs" Inherits="WUCItemConfiguracaoAtributo" %>
<script language="javascript">
<!--
function verifica() {
	if (confirm("Deseja mesmo excluir o atributo do Item de Configuração?")) {
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
    <tr id="Tr1" runat="server">
      <td height="0" align="center" valign="middle">
        <div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
          <table width="776" border="0" cellspacing="5" cellpadding="0">
            <tr>
              <td width="60" align="center" valign="bottom">
                <asp:Image ID="imgIcone" runat="server" />
              </td>
              <td align="center" valign="bottom">
                <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label>
              </td>
            </tr>
          </table>
        </div>
      </td>
    </tr>
    <tr id="Tr2" runat="server">
      <td height="20" align="center" valign="middle">&nbsp;      </td>
    </tr>
    <tr>
      <td align="center" valign="top" colspan="2"><table width="97%"  border="0" cellspacing="0" cellpadding="0">
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
                            <td align="left" valign="middle" class="tituloFont">Dados do Atributo do Item de Controle</td>
                          </tr>
                      </table></td>
                      <td width="8" class="dir_top"></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td align="center" valign="top">
                      <table width="92%" border="0" cellspacing="2" cellpadding="0">
                        <tr align="left" valign="middle">
                          <td width="68"> Descri&ccedil;&atilde;o:</td>
                          <td>
                            <asp:TextBox ID="txtDescricao" CssClass="campo_texto" MaxLength="200" runat="server" Width="180px"></asp:TextBox>                          </td>
                          <td width="37"> Tipo: </td>
                          <td>
                            <asp:DropDownList ID="ddlAtributoTipo" Width="200" CssClass="campo_texto" runat="server"></asp:DropDownList>                          </td>
                          <td width="56">Unidade:</td>
                          <td>
                            <asp:DropDownList ID="ddlUnidadeMedida" Width="200" CssClass="campo_texto" runat="server"> </asp:DropDownList></td>
                          <td align="center" valign="middle">
                            <asp:Button ID="btnSalvar" Width="70" CssClass="botao" runat="server" Text="Salvar" OnClick="salvaItemConfiguracaoAtributo" />                
                            <asp:Button ID="btnNovo" Width="70" CssClass="botao" runat="server" Text="Novo" OnClick="novoItemConfiguracaoAtributo" />                          </td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="left" valign="top">
                    <td colspan="7">
                      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td><table border="0" align="left" cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="aba_esquerda_off">&nbsp; </td>
                              <td class="aba_centro_off"> Lista de Atributos Cadastrados</td>
                              <td class="aba_direita_off">&nbsp; </td>
                            </tr>
                          </table>                             </td>
                          </tr>
                        <tr>
                          <td align="center" valign="top">
                            <asp:Panel ID="pnlGridAtributo" runat="server" Height="400px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
							<asp:GridView ID="gvAtributo" GridLines="None" Width="98%" HorizontalAlign="Center"
                              AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3"
                              ShowFooter="False" OnRowCommand="gvAtributo_OnRowCommand" OnRowDataBound="gvAtributo_RowDataBound">
                              <FooterStyle CssClass="footerGrid" />                
                              <HeaderStyle CssClass="topoGrid" />                
                              <RowStyle BackColor="#F4FBFA" />
                              <Columns>
                              <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                              <ItemTemplate>
                                <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "ic_atributo_codigo")%>'
                                      runat="server"></asp:Label>
                              </ItemTemplate>
                              </asp:TemplateField> <asp:TemplateField HeaderText="Nome">
                              <ItemTemplate>
                                <asp:TextBox CssClass="campo_descricao400" ID="lblNone" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                              </ItemTemplate>
                              <ItemStyle HorizontalAlign="Left" Width="410px" />                
                              <HeaderStyle HorizontalAlign="Left" Width="410px" />                
              </asp:TemplateField> <asp:TemplateField HeaderText="Tipo">
              <ItemTemplate>
                <asp:Label ID="lblTipo" Text='<%# DataBinder.Eval(Container.DataItem, "tipo")%>'
                                      runat="server"></asp:Label>
              </ItemTemplate>
              <HeaderStyle HorizontalAlign="Center" />
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField> <asp:TemplateField HeaderText="Unidade de Medida">
              <ItemTemplate>
                <asp:Label ID="lblCodigoMedida" Text='<%# DataBinder.Eval(Container.DataItem, "medida_codigo")%>'
                                      runat="server"></asp:Label>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Center" />
              </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="images/icones/editar.gif">
              <ItemStyle HorizontalAlign="Center" Width="40px" />
              <HeaderStyle HorizontalAlign="Center" Width="40px" />
              </asp:ButtonField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif">
              <ItemStyle HorizontalAlign="Center" Width="20px" />
              <HeaderStyle HorizontalAlign="Center" Width="20px" />
              </asp:ButtonField>

                              </Columns>
                              <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />                
                              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#C8E4E6" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>
                          </td>
                        </tr>
                    </table></td>
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
  </table>
</div>