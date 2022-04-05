<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCNivelEquipe.ascx.cs" Inherits="WUCNivelEquipe" %>
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
                          <td align="left" valign="middle" class="tituloFont">N&iacute;vel das Equipes </td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td align="center" valign="top"><table width="92%"  border="0" cellspacing="2" cellpadding="0">
                    <tr>
                      <td width="110" align="left">Descri&ccedil;&atilde;o do N&iacute;vel:</td>
                      <td colspan="4" align="left" valign="middle">
                        <asp:TextBox ID="txtDescricao" MaxLength="100" CssClass="campo_texto" runat="server" Width="712px"></asp:TextBox>                      </td>
                    </tr>
                    <tr>
                      <td align="left">Empresa:</td>
                      <td width="250" align="left" valign="middle">
                        <asp:DropDownList ID="ddlEmpresa" runat="server" CssClass="campo_texto" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged" AutoPostBack="True" Width="250px"></asp:DropDownList>                      </td>
                      <td width="95" align="right">Lider do N&iacute;vel:</td>
                      <td width="200" align="left">
                        <asp:DropDownList ID="ddlPessoa" runat="server" CssClass="campo_texto" Width="200px"></asp:DropDownList>                      </td>
                      <td width="180" align="left">
                        <asp:Button ID="btnLimpar" Width="60px" CssClass="botao" runat="server" Text="Novo" OnClick="btnLimpar_Click" />                  
                        <asp:Button ID="btnSalvar" Width="60px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />    </td>
                    </tr>
                  </table></td>
                </tr>
                <tr>
                  <td align="center" valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td align="left"><table border="0" align="left"  cellpadding="0" cellspacing="0">
                        <tr>
                          <td class="aba_esquerda_off">&nbsp;</td>
                          <td class="aba_centro_off">Nivel</td>
                          <td class="aba_direita_off">&nbsp;</td>
                        </tr>
                      </table></td>
                      </tr>
                    <tr>
                      <td align="center" valign="top">
                        <asp:Panel ID="pnlGridNivelEquipe"  runat="server" Height="350px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
						<asp:GridView ID="gvNivelEquipe" GridLines="None" Width="98%" HorizontalAlign="Center" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="False" OnRowCommand="gvNivelEquipe_OnRowCommand" OnRowDataBound="gvNivelEquipe_RowDataBound">
                          <FooterStyle CssClass="footerGrid" />                  
                          <HeaderStyle CssClass="topoGrid" />                  
                          <RowStyle BackColor="#F4FBFA" />
                          <Columns>
                          <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                          <ItemTemplate>
                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "equipe_nivel_codigo")%>' runat="server"></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField> <asp:TemplateField  HeaderText="Nivel">
                          <ItemTemplate>
                            <asp:TextBox CssClass="campo_descricao200" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />                  
                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />                  
        </asp:TemplateField> <asp:TemplateField  HeaderText="Lider do N&#237;vel">
        <ItemTemplate>
          <asp:Label ID="lblPessoaCodigoGerente" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo_gerente")%> ' runat="server"></asp:Label>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
        </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="images/icones/editar.gif" >
        <ItemStyle HorizontalAlign="Center" Width="40px" />
        <HeaderStyle Width="40px" />
        </asp:ButtonField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif" >
        <ItemStyle HorizontalAlign="Center" Width="20px" />
        <HeaderStyle Width="20px" />
        </asp:ButtonField>
                          </Columns>
                          <PagerStyle HorizontalAlign="Center" CssClass="menu" />                  
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

	</td>
</tr>            