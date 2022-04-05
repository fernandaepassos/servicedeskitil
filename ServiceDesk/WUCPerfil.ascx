<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCPerfil.ascx.cs" Inherits="WUCPerfil" %>
<%@ Register Assembly="MetaBuilders.WebControls.GlobalRadioButton" Namespace="MetaBuilders.WebControls"
    TagPrefix="cc1" %>


<tr id="Tr1" runat=server>
    <td height="0" align="center" valign="middle">
		<div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
			<table width="776" border="0" cellspacing="5" cellpadding="0">
                <tr>
                    <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
                    <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                </tr>
            </table>    
        </div>
	</td>
</tr>
<tr id="Tr2" runat=server>
    <td align="center" valign="middle">
	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="20">&nbsp;</td>
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
                          <td align="left" valign="middle" class="tituloFont">Perfil</td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                  <td align="center" valign="top">
                    <table width="92%"  border="0" cellspacing="2" cellpadding="0">
                      <tr>
                        <td align="left" width="71">Aplica&ccedil;&atilde;o:</td>
                        <td align="left" valign="middle">
                          <asp:DropDownList ID="ddlAplicacao" runat="server" Width="620" CssClass="campo_texto" OnSelectedIndexChanged="ddlAplicacao_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>                        </td>
                        <td align="center" valign="middle">
                          <asp:Button ID="btnSalvar" Width="60px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click"  />          </td>
                      </tr>
                  </table></td>
                </tr>
                <tr align="left" valign="top">
                  <td colspan="7">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="left"><table height="19" border="0" align="left"  cellpadding="0" cellspacing="0">
                          <tr>
                            <td class="aba_esquerda_off">&nbsp;</td>
                            <td class="aba_centro_off">Lista</td>
                            <td class="aba_direita_off">&nbsp;</td>
                          </tr>
                        </table></td>
                        </tr>
                      <tr align="left">
                        <td valign="top">
                          <asp:Panel ID="pnlGridPerfil"  runat="server" Height="400px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
						  <asp:GridView ID="gvTipoUsuario" GridLines="None" Width="98%" HorizontalAlign="Center" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="False" OnSelectedIndexChanged="ddlAplicacao_SelectedIndexChanged">
                            <FooterStyle CssClass="footerGrid" />              
                            <HeaderStyle CssClass="topoGrid" />              
                            <RowStyle BackColor="#F4FBFA" />
                            <Columns>
                            <asp:TemplateField>
                            <ItemTemplate>
                              <asp:Label ID="lblCodigo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_usuario_codigo") %>' runat="server"></asp:Label>
                              <asp:CheckBox ID="cbCodigo" runat="server" />              
              </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="20px" />              
                            <HeaderStyle HorizontalAlign="Center" Width="20px" />              
              </asp:TemplateField> <asp:TemplateField HeaderText="Tipo de Usu&#225;rio">
              <ItemTemplate>
                <asp:TextBox CssClass="campo_descricao650" ID="lblTipoUsuario" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
              </ItemTemplate>
              <HeaderStyle HorizontalAlign="Left" Width="650px" />
              <ItemStyle HorizontalAlign="Left" Width="650px" />
              </asp:TemplateField> <asp:TemplateField HeaderText="Padr&#227;o">
              <ItemTemplate> <cc1:GlobalRadioButton ID="rbTipoUsuario" runat="server" GroupName="opcPadrao"/> </ItemTemplate>
              <HeaderStyle HorizontalAlign="Center" Width="50px" />
              <ItemStyle HorizontalAlign="Center" Width="50px" />
              </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="menu" />              
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#C8E4E6" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>                        </td>
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
</table>  </td>
</tr> 