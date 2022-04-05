<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuncaoAplicacao.aspx.cs" Inherits="FuncaoAplicacao" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Direitos do Perfil</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<script language="javascript">
<!--
function verifica() {
	if (confirm("Deseja mesmo excluir a Função?")) {
		return true;
	}
	else {
		return false;
	}
}
-->
</script>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
        <div class="Conteudo">
            <div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
			    <table width="776" border="0" cellspacing="5" cellpadding="0">
                    <tr>
                        <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
                        <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                    </tr>
                </table>    
            </div>
            <table width="100%"  border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td>&nbsp;</td>
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
                                      <td align="left" valign="middle" class="tituloFont">Fun&ccedil;&otilde;es da Aplica&ccedil;&atilde;o </td>
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
                              <td>
                                <asp:Panel ID="PanelFuncaoAplicacao" runat="server" Height="340px" Width="100%" ScrollBars="both" CssClass="dataGrid">
								<asp:GridView ID="gvFuncaoAplicacao" CssClass="dataFont" runat="server" Width="100%" CellPadding="4" OnRowCommand="gvFuncaoAplicacao_RowCommand" AllowPaging="True" AutoGenerateColumns="False" OnRowEditing="gvFuncaoAplicacao_RowEditing" OnRowDataBound="gvFuncaoAplicacao_RowDataBound" OnRowCancelingEdit="gvFuncaoAplicacao_RowCancelingEdit" OnRowUpdating="gvFuncaoAplicacao_RowUpdating" OnPageIndexChanging="gvFuncaoAplicacao_PageIndexChanging" ForeColor="#333333" GridLines="None">
                                  <FooterStyle CssClass="footerGrid" />                          
                                  <PagerStyle CssClass="menu" HorizontalAlign="Center" />                          
                                  <RowStyle BackColor="#F4FBFA" />
                                  <HeaderStyle CssClass="topoGrid" />                          
                                  <Columns>
                                  <asp:TemplateField HeaderText="C&#243;digo">
                                  <ItemTemplate>
                                    <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "funcao_codigo") %>' runat="server"></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" Width="60px" />                          
                                  <HeaderStyle HorizontalAlign="Center" Width="60px" />                          
        </asp:TemplateField> <asp:TemplateField HeaderText="Fun&#231;&#227;o">
        <ItemTemplate>
          <asp:Label ID="lblFuncao" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>' runat="server"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtFuncao" CssClass="campo_texto" MaxLength="255" Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>' runat="server"></asp:TextBox>
        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left" Width="80px" />
        <ItemStyle HorizontalAlign="Left" Width="80px" />
        </asp:TemplateField> <asp:TemplateField HeaderText="Aplica&#231;&#227;o">
        <ItemTemplate>
          <asp:Label ID="lblAplicacaoCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "aplicacao_codigo")%>' runat="server"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:Label ID="lblAplicacaoCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "aplicacao_codigo")%>' Runat="server" Visible="False"></asp:Label>
          <asp:DropDownList ID="ddlAplicacaoCodigo" Width="50" Runat="server"></asp:DropDownList>
        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left" Width="250px" />
        <ItemStyle HorizontalAlign="Left" Width="250px" />
        </asp:TemplateField> <asp:TemplateField HeaderText="Fun&#231;&#227;o Superior">
        <ItemTemplate>
          <asp:Label ID="lblCodigoFuncaoSuperior" Text='<%# DataBinder.Eval(Container.DataItem, "funcao_superior_codigo")%>' runat="server"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:Label ID="lblCodigoFuncaoSuperior" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "funcao_superior_codigo") %>' Visible="False"></asp:Label>
          <asp:DropDownList ID="ddlFuncaoSuperior" Width="80" Runat="server"></asp:DropDownList>
        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left" Width="250px" />
        <ItemStyle HorizontalAlign="Left" Width="250px" />
        </asp:TemplateField> <asp:TemplateField HeaderText="Url">
        <ItemTemplate>
          <asp:TextBox CssClass="campo_descricao400" ID="lblUrl" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "url") %>' ></asp:TextBox>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtUrl" CssClass="campo_texto" MaxLength="200" Width="400" Text='<%# DataBinder.Eval(Container.DataItem, "url")%>' runat="server"></asp:TextBox>
        </EditItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField> <asp:CommandField ButtonType="Image" CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar"
                  EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True"
                  UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" >
        <ItemStyle Width="40px" HorizontalAlign="Center" />
        <HeaderStyle HorizontalAlign="Center" Width="40px" />
        </asp:CommandField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="images/icones/excluir.gif"
                  Text="Excluir" >
        <ItemStyle HorizontalAlign="Center" Width="20px" />
        <HeaderStyle HorizontalAlign="Center" Width="20px" />
        </asp:ButtonField>
                                  </Columns>
                                  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#C8E4E6" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>
                              </td>
                            </tr>
                            <tr>
                              <td align="left" class="footerGrid">
                                <asp:ImageButton ID="imgNovaFuncaoAplicacao" runat="server" AlternateText="Novo Fun&ccedil;&atilde;o" ImageUrl="images/icones/inserir.gif" OnClick="imgNovaFuncaoAplicacao_Click"/>                          
&nbsp;Nova Fun&ccedil;&atilde;o&nbsp;
      <asp:ImageButton ID="imgPesquisar" ImageUrl="~/images/icones/exibir.gif" AlternateText="Pesquisar" runat="server" OnClick="imgPesquisar_Click" />
&nbsp; Filtrar</td>
                            </tr>
                            <tr>
                              <td align="center" valign="top">                                <asp:Panel ID="pnlNovaFuncao" runat="server" BackColor="Transparent" GroupingText="Nova Fun&ccedil;&atilde;o"
                            Height="20px" Visible="False" Width="100%" CssClass="dataGrid">
                                <table border="0" cellpadding="0" cellspacing="2" width="770" class="dataFont">
                                    <tr>
                                      <td align="left" width="125"> Descri&ccedil;&atilde;o: </td>
                                      <td colspan="3" align="left">
                                        <asp:TextBox ID="txtDescricao" MaxLength="255" Width="596px" runat="server" CssClass="campo_texto"></asp:TextBox></td>
                                      <td width="60" rowspan="3" align="center" valign="middle">
                                        <asp:Button ID="btnSalvar" width="60" runat="server" CssClass="botao" OnClick="imgSalvar_Click"
                                            Text="Gravar" />                          
            </td>
                                    </tr>
                                    <tr>
                                      <td align="left"> Fun&ccedil;&atilde;o Superior:</td>
                                      <td width="227" align="left">
                                        <asp:DropDownList ID="ddlFuncaoSuperior" Width="243px" runat="server"></asp:DropDownList></td>
                                      <td width="110" align="left">Aplica&ccedil;&atilde;o:</td>
                                      <td width="227" align="left"><asp:DropDownList ID="ddlAplicacao" Width="241px" runat="server"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                      <td align="left"> Url:</td>
                                      <td colspan="3" align="left">
                                        <asp:TextBox ID="txtUrl" MaxLength="255" Width="596px" runat="server" CssClass="campo_texto"></asp:TextBox></td>
                                    </tr>
                                  </table>
                                </asp:Panel>
                                <asp:Panel ID="plPesquisar" runat="server" BackColor="Transparent" GroupingText="Pesquisa"
                            Height="20px" Visible="False" Width="100%" CssClass="dataGrid">
                                  <table border="0" cellpadding="0" cellspacing="2" width="770" class="dataFont">
                                    <tr>
                                      <td align="left" width="125"> Descri&ccedil;&atilde;o: </td>
                                      <td colspan="3" align="left">
                                        <asp:TextBox ID="txtDescricaoFuncao" MaxLength="255" Width="596px" runat="server" CssClass="campo_texto"></asp:TextBox></td>
                                      <td width="60" rowspan="2" align="center" valign="middle">
                                        <asp:Button ID="btnFiltrar" width="60" runat="server" CssClass="botao" OnClick="btnFiltrar_Click"
                                            Text="Filtrar" />                          
            </td>
                                    </tr>
                                    <tr>
                                      <td align="left"> Fun&ccedil;&atilde;o Superior:</td>
                                      <td width="227" align="left">
                                        <asp:DropDownList ID="ddlFuncaoSuperiorCodigo" Width="243px" runat="server"></asp:DropDownList></td>
                                      <td width="110" align="left">Aplica&ccedil;&atilde;o:</td>
                                      <td width="227" align="left"><asp:DropDownList ID="ddlAplicacaoCodigo" Width="241px" runat="server"></asp:DropDownList></td>
                                    </tr>
                                  </table>
                              </asp:Panel>                              </td>
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
            <h1>
            <span class="TextoValor"></span>&nbsp;</h1>
            <h1>
          <br>
                &nbsp;</h1>
        
        <div id="divPesquisar" class="Novo" visible="false" runat="server">
          <div align="center">
              &nbsp;</div>       
        </div>   
        
        <div id="divNovaFuncaoAplicacao" visible="false" class="Novo" runat="server">
          <div>
            <span class="TextoValor"></span>
          </div>
          
          <br>
          <div align="center">
              &nbsp;</div>
        </div>
	  </div>
    </form>
</body>
</html>