<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Notificacao.aspx.cs" Inherits="Notificacao" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>
<SCRIPT LANGUAGE="JavaScript" SRC="js/funcoes.js"></SCRIPT>


<head id="Head1" runat="server">
  <title>Help Desk  ITIL Compliance :: Notificação</title>
  <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
  <form id="form1" runat="server" style="margin:0">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td height="20" align="center" valign="top">&nbsp;        </td>
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
                              <td align="left" valign="middle" class="tituloFont">Notifica&ccedil;&otilde;es</td>
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
    <td align="center" valign="top">
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
  <tr>
    <td align="left" valign="top">
	<asp:Panel ID="pnlGridNotificacao" runat="server" ScrollBars="Vertical" Height="400px" Width="100%" CssClass="dataGrid">
                        <asp:GridView ID="gvNotificacao" runat="server" AutoGenerateColumns="False" CellPadding="4"
                          EnableTheming="True" Font-Names="Arial" Font-Size="Smaller" ForeColor="#333333"
                          GridLines="None" Width="98%" OnRowCommand="gvNotificacao_RowCommand" OnRowDataBound="gvNotificacao_RowDataBound">
                          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                          <RowStyle BackColor="#F4FBFA" />
                          <Columns>
                            <asp:TemplateField HeaderText="Codigo" Visible="False">
                              <ItemTemplate>
                                <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "notificacao_codigo")%>'></asp:Label>
                                <asp:Label ID="lblIdentificadorTabela" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "identificador_tabela")%>'></asp:Label>
                              </ItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Detalhe">
                              <ItemTemplate>
                                <asp:Label ID="lblTipo" runat=server Text=<%# DataBinder.Eval(Container.DataItem, "tipo")%>></asp:Label>
                              </ItemTemplate>
                              <ItemStyle HorizontalAlign="Center" Width="100px" />
                              <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                              <ItemTemplate>
                                <asp:TextBox CssClass="campo_descricao350" ID="txtDescricao" runat="server"
                                  TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                              </ItemTemplate>
                              <ItemStyle HorizontalAlign="Left" Width="350px" Wrap="False" />
                              <HeaderStyle Width="350px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Emissor">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "emissor")%>
                              </ItemTemplate>
                              <ItemStyle HorizontalAlign="Left" />
                              <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data emiss&#227;o">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "data_inclusao")%>
                              </ItemTemplate>
                              <ItemStyle HorizontalAlign="Center" />
                              <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Justificatica">
                              <ItemTemplate>
                                <asp:TextBox CssClass="campo_texto" ID="txtJustificativa" Width="190" runat="server"
                                  Text='<%# DataBinder.Eval(Container.DataItem, "justificativa")%>'></asp:TextBox>
                              </ItemTemplate>
                              <ItemStyle HorizontalAlign="Left" Width="200px" />
                              <HeaderStyle Width="200px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Image" CommandName="Aprovar" ImageUrl="~/images/icones/validar.gif"
                              Text="Aprovar">
                              <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Image" CommandName="Reprovar" ImageUrl="~/images/icones/ReprovarTransparente.ico"
                              Text="Reprovar">
                              <ItemStyle Width="10px" />
                              <HeaderStyle Width="10px" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="FlagAprovacao" Visible="False">
                              <ItemTemplate>
                                <asp:TextBox ID="txtFlgAprov" Width="300" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "flag_aprovado")%>'></asp:TextBox>
                              </ItemTemplate>
                            </asp:TemplateField>
                          </Columns>
                          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                          <HeaderStyle CssClass="topoGrid" />
                          <EditRowStyle BackColor="#2461BF" />
                          <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                      </asp:Panel>
	</td>
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
  </form>
</body>
</html>