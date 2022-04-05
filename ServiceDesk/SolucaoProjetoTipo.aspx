<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SolucaoProjetoTipo.aspx.cs" Inherits="SolucaoProjetoTipo" %>
<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Tipo de Solução</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center" valign="middle">
      
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
                                              <td align="left" valign="middle" class="tituloFont">Tipo de Solu&ccedil;&atilde;o </td>
                                              <td width="150" align="left" valign="middle" class="tituloFont">
											  <asp:ImageButton ID="imgNovoTipo" runat="server" AlternateText="Novo Tipo de Urg&ecirc;ncia"
                                    ImageUrl="images/icones/inserir.gif" OnClick="imgNovoTipoDia_Click" />                                    
      Novo Tipo de Solu&ccedil;&atilde;o
											  </td>
                                              <td align="left" valign="middle" class="tituloFont">&nbsp;</td>
                                              <td align="left" valign="middle" class="tituloFont">&nbsp;</td>
                                            </tr>
                                        </table></td>
                                        <td width="8" class="dir_top"></td>
                                      </tr>
                                  </table></td>
                                </tr>
                                <tr>
                                  <td colspan="3" align="center" valign="top" class="fundo_tabela">
								  <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                      <td align="left" valign="top">
                                        <div id="divNovoTipo" runat="server" class="Novo" visible="true">
                                          <div align="left">
                                            <asp:Panel ID="pnlGridChamados"  runat="server" ScrollBars="Vertical" Width="100%" CssClass="dataGrid"> <asp:GridView ID="gvSolucaoProjetoTipo" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="98%" OnPageIndexChanging="gvSolucaoProjetoTipo_PageIndexChanging" OnRowCancelingEdit="gvSolucaoProjetoTipo_RowCancelingEdit" OnRowCommand="gvSolucaoProjetoTipo_RowCommand" OnRowEditing="gvSolucaoProjetoTipo_RowEditing" OnRowUpdating="gvSolucaoProjetoTipo_RowUpdating">
                                              <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />                                  
                                              <RowStyle BackColor="#F4FBFA" />
                                              <Columns>
                                              <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                              <ItemTemplate>
                                                <asp:Label ID="lblCodigo" runat="server" visible=false BorderStyle=None Text='<%# DataBinder.Eval(Container.DataItem, "solucao_projeto_tipo_codigo")%>'></asp:Label>
                                              </ItemTemplate>
                                              <EditItemTemplate>
                                                <asp:TextBox ID="txtCodigo" MaxLength="20" visible=false Width="100" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "solucao_projeto_tipo_codigo")%>' ></asp:TextBox>
                                              </EditItemTemplate>
                                              </asp:TemplateField> <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                              <ItemTemplate>
                                                <asp:TextBox CssClass="campo_descricao650" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                                              </ItemTemplate>
                                              <EditItemTemplate>
                                                <asp:TextBox ID="txtDescricao" CssClass="campo_texto" MaxLength="50" runat="server" Width="650" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>'></asp:TextBox>
                                              </EditItemTemplate>
                                              <HeaderStyle HorizontalAlign="Left" />                                  
                                              <ItemStyle HorizontalAlign="Left" />                                  
            </asp:TemplateField> <asp:CommandField ButtonType="Image" CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True" UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar">
            <ItemStyle Width="38px" />
            <HeaderStyle Width="38px" />
            </asp:CommandField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                Text="Excluir">
            <ItemStyle HorizontalAlign="Right" Width="20px" />
            <HeaderStyle Width="20px" />
            </asp:ButtonField>
                                              </Columns>
                                              <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />                                  
                                              <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                                              <HeaderStyle CssClass="topoGrid" />                                  
                                              <EditRowStyle BackColor="White" /> <AlternatingRowStyle BackColor="White" /> <EditRowStyle BackColor="#C8E4E6" /> </asp:GridView> </asp:Panel>
                                          </div>
                                      </div></td>
                                    </tr>
                                    <tr>
                                      <td align="center" valign="top">									  <asp:Panel ID="pnlNovoTipo" runat="server" BackColor="Transparent" GroupingText="Novo Tipo:"
                                    Height="20px" Visible="False" Width="100%" CssClass="dataGrid">
                                        <table border="0" cellpadding="0" cellspacing="2" width="92%">
                                            <tr>
                                              <td width="70" align="left">Descri&ccedil;&atilde;o:</td>
                                              <td align="left"><asp:TextBox ID="txtDescricaoTipo" CssClass="campo_texto" runat="server" Width="570px" MaxLength="100"></asp:TextBox>
                                                  <asp:CheckBox
                        ID="ckPadrao" runat="server" Text="Padr&atilde;o" /></td>
                                              <td align="left" valign="middle">
                                                <asp:Button ID="btnSalvar" runat="server" CssClass="botao" OnClick="btnSalvar_Click"
                                                    Text="Salvar" />                                  
            </td>
                                            </tr>
                                          </table>
                                        </asp:Panel>                                      </td>
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
                    </table></td>
  </tr>
</table>

    </form>
</body>
</html>