<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCChamadoTipo.ascx.cs" Inherits="WUCChamadoTipo" %>
<script language="javascript">
    function verifica() {
	    if (confirm("Deseja mesmo excluir este item?")) {
		    return true;
	    }
	    else {
		    return false;
	    }
    }
</script> 
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
                          <td align="left" valign="middle" class="tituloFont">Tipo Chamado </td>
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
                    <table width="92%" border="0" cellspacing="2" cellpadding="0">
                      <tr>
                        <td width="80" align="left" valign="middle">Prefixo:</td>
                        <td align="left" valign="middle">
                          <asp:TextBox ID="txtPrefixo" Width="80px" MaxLength="10" CssClass="campo_texto" runat="server"></asp:TextBox>                        </td>
                        <td width="80" align="left" valign="middle">Descri&ccedil;&atilde;o:</td>
                        <td align="left" valign="middle">
                          <asp:TextBox ID="txtDescricao" runat="server" MaxLength="100" Width="250px" CssClass="campo_texto"></asp:TextBox>                        </td>
                        <td width="110" align="left" valign="middle">Gera Processo?:</td>
                        <td align="left" valign="middle">
                          <asp:DropDownList ID="ddlGeraProcesso" Width="60px" runat="server"></asp:DropDownList>                        </td>
                        <td colspan="2" align="center" valign="middle">
                          <asp:Button ID="btnNovo" Width="60px" CssClass="botao" runat="server" Text="Novo" OnClick="btnNovo_Click" />              
              &nbsp;&nbsp;
                          <asp:Button ID="btnSalvar" Width="60px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />          </td>
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
                            <td class="aba_centro_off">Tipo Chamado</td>
                            <td class="aba_direita_off">&nbsp;</td>
                          </tr>
                        </table></td>
                        </tr>
                      <tr align="left">
                        <td valign="top">
                          <asp:Panel ID="pnlGridTipoChamado"  runat="server" Height="400px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
						  <asp:GridView ID="gvTipoChamado" GridLines="None" Width="98%" HorizontalAlign="Center" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="False" 
									                OnRowCommand="gvTipoChamado_OnRowCommand" 
									                OnRowDataBound="gvTipoChamado_RowDataBound"
									                OnRowEditing="gvTipoChamado_RowEditing" 
									                OnRowCancelingEdit="gvTipoChamado_RowCancelingEdit" 
									                OnRowUpdating="gvTipoChamado_RowUpdating">
                            <FooterStyle CssClass="footerGrid" />              
                            <HeaderStyle CssClass="topoGrid" />              
                            <RowStyle BackColor="#F4FBFA" />
                            <Columns>
                            <asp:TemplateField HeaderText="C&#243;digo" Visible = "False">
                            <ItemTemplate>
                              <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "chamado_tipo_codigo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField> <asp:TemplateField HeaderText="Prefixo">
                            <ItemTemplate>
                              <asp:Label ID="lblPrefixo" Text='<%# DataBinder.Eval(Container.DataItem, "prefixo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                              <asp:TextBox ID="txtPrefixo" CssClass="campo_texto" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "prefixo")%>' MaxLength="10" Width="50px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />              
                            <ItemStyle HorizontalAlign="Center" Width="50px" />              
              </asp:TemplateField> <asp:TemplateField HeaderText="Descri&#231;&#227;o">
              <ItemTemplate>
                <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
              </ItemTemplate>
              <EditItemTemplate>
                <asp:TextBox ID="txtDescricao" CssClass="campo_texto" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>' MaxLength="100" Width="550px"></asp:TextBox>
              </EditItemTemplate>
              <HeaderStyle HorizontalAlign="Left" Width="550px" />
              <ItemStyle HorizontalAlign="Left" Width="550px" />
              </asp:TemplateField> <asp:TemplateField HeaderText="Gera Processo?">
              <ItemTemplate>
                <asp:Label ID="lblGeraProcesso" Text='<%# DataBinder.Eval(Container.DataItem, "flag_gera_processo")%>' runat="server"></asp:Label>
              </ItemTemplate>
              <EditItemTemplate>
                <asp:Label ID="lblGeraProcesso" Text='<%# DataBinder.Eval(Container.DataItem, "flag_gera_processo")%>' Runat="server" Visible="False"></asp:Label>
                <asp:DropDownList ID="ddlGeraProcesso" runat="server"></asp:DropDownList>
              </EditItemTemplate>
              <HeaderStyle HorizontalAlign="Center" Width="80px" />
              <ItemStyle HorizontalAlign="Center" Width="80px" />
              </asp:TemplateField> <asp:CommandField ButtonType="Image" 
                                                            CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" 
                                                            EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True"
                                                            UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" >
              <ItemStyle HorizontalAlign="Center" Width="40px" />
              <HeaderStyle Width="40px" />
              </asp:CommandField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif" >
              <ItemStyle HorizontalAlign="Center" Width="20px" />
              <HeaderStyle Width="20px" />
              </asp:ButtonField>
                            </Columns>
                            <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />              
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