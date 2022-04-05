<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCSegurancaDireito.ascx.cs" Inherits="WUCSegurancaDireito" %>
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
                          <td align="left" valign="middle" class="tituloFont">Direitos da Seguran&ccedil;a </td>
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
                        <td width="70" align="left" valign="middle"><asp:Label ID="lblTitulo" Text="Descri&ccedil;&atilde;o:" runat="server"></asp:Label></td>
                        <td align="left" valign="middle">
                          <asp:TextBox ID="txtDescricao" runat="server" MaxLength="50" Width="550px" CssClass="campo_texto"></asp:TextBox>                        </td>
                        <td colspan="2" align="center">
                          <asp:Button ID="btnLimpar" Width="60px" CssClass="botao" runat="server" Text="Novo" OnClick="btnLimpar_Click" />              
              &nbsp;&nbsp;
                          <asp:Button ID="btnSalvar" Width="60px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />              
          </td>
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
                            <td class="aba_centro_off">Direitos</td>
                            <td class="aba_direita_off">&nbsp;</td>
                          </tr>
                        </table></td>
                        </tr>
                      <tr>
                        <td align="center" valign="top">
                          <asp:Panel ID="pnlGridSegurancaDireito"  runat="server" Height="250px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
						  <asp:GridView ID="gvSegurancaDireito" GridLines="None" Width="98%" HorizontalAlign="Center" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="True" 
									                OnRowCommand="gvSegurancaDireito_OnRowCommand" 
									                OnRowDataBound="gvSegurancaDireito_RowDataBound"
									                OnRowEditing="gvSegurancaDireito_RowEditing" 
									                OnRowCancelingEdit="gvSegurancaDireito_RowCancelingEdit" 
									                OnRowUpdating="gvSegurancaDireito_RowUpdating">
                            <HeaderStyle CssClass="topoGrid" />              
                            <RowStyle BackColor="#F4FBFA" />
                            <Columns>
                            <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                            <ItemTemplate>
                              <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "seguranca_direito_codigo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField> <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                            <ItemTemplate>
                              <asp:TextBox CssClass="campo_descricao700" ID="lblDescricaoSegurancaDireito" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                              <asp:Label ID="lblDescricaoSegurancaDireito" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>' Runat="server" Visible="False"></asp:Label>
                              <asp:TextBox ID="txtDescricaoSegurancaDireito" runat="server" MaxLength="30"
				                                                 Width="200px"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />              
                            <ItemStyle HorizontalAlign="Left" />              
              </asp:TemplateField> <asp:CommandField ButtonType="Image"
                                                            CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" 
                                                            EditImageUrl="images/icones/editar.gif" EditText="Editar"
                                                            UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" >
              <ItemStyle HorizontalAlign="Center" Width="40px" />
              </asp:CommandField> <asp:ButtonField ButtonType="Image" Visible="False" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif" >
              <ItemStyle HorizontalAlign="Center" Width="20px" />
              <HeaderStyle Width="20px" />
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
</table>  </td>
</tr> 