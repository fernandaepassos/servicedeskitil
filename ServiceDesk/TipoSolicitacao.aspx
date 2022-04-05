<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TipoSolicitacao.aspx.cs" Inherits="TipoSolicitacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Tipo de Solicitação</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td align="center" valign="top"><table width="776" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td height="35" valign="bottom"><table border="0" align="left" cellpadding="0" cellspacing="0">
                    <tr>
                      <td class="aba_esquerda_off">&nbsp; </td>
                      <td class="aba_centro_off">Solicita&ccedil;&atilde;o</td>
                      <td class="aba_direita_off">&nbsp; </td>
                    </tr>
                  </table></td>
                </tr>
                <tr>
                  <td align="center" valign="top"><table width="776" border="0" cellspacing="0" cellpadding="0" class="tabela_padrao">
                    <tr>
                <td>
                    <table width="776" border="0" cellspacing="2" cellpadding="0">
  <tr>
    <td>
	<asp:GridView ID="gvTipoUrgencia" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333" GridLines="None" 
                        Width="776px">
                        <FooterStyle CssClass="footerGrid" />
                        <RowStyle BackColor="#F4FBFA" />
                        <Columns>
                            <asp:CommandField ButtonType="Image" CancelImageUrl="~/images/icones/voltar.gif"
                                CancelText="Cancelar" EditImageUrl="~/images/icones/editar.gif" EditText="Editar"
                                InsertImageUrl="~/images/show.gif" InsertText="Inserir" NewImageUrl="~/images/show.gif"
                                NewText="Novo" SelectImageUrl="~/images/show.gif" SelectText="Selecionar" ShowEditButton="True"
                                UpdateImageUrl="~/images/show.gif" UpdateText="Salvar" />
                            <asp:TemplateField HeaderText="Descri&#231;&#227;o"></asp:TemplateField>
                            <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icones/excluir.gif" Text="Excluir">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:ButtonField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle CssClass="topoGrid" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
	</td>
  </tr>
  <tr>
    <td align="left">
	<asp:ImageButton ID="imgNovoTipo" ImageUrl="images/icones/inserir.gif" AlternateText="Novo Tipo de Urgência" runat="server" OnClick="imgNovoTipoDia_Click" />&nbsp;Novo Tipo de Solicitação
                    <div id="Div1" runat="server" class="Novo" visible="false">
                        <div>
                            <span class="TextoValor"></span>&nbsp;</div>
                    </div>
	</td>
  </tr>
</table>
<div id="divNovoTipoDia" runat="server" class="Novo" visible="false">
                        <div>
                        </div>
                    </div>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Panel ID="pnlNovoTipo" runat="server" GroupingText="Novo Tipo:" Height="20px"
                        Visible="False" Width="776px" BackColor="Transparent" CssClass="dataGrid">
                        <table width="100%" border="0" cellpadding="0" cellspacing="2" class="tabela_padrao">
                            <tr>
                                <td width="91" align="left"> 
								Descrição:</td>
                                <td align="left"> <asp:TextBox CssClass="campo_texto" ID="txtDescricaoTipo" width="600" runat="server"></asp:TextBox>&nbsp;</td>
                                <td width="60" align="left"><asp:Button ID="btnSalvar" width="60" CssClass="botao" runat="server" Text="Gravar" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
                  </table></td>
                </tr>
              </table></td>
            </tr>
            
        </table>
    
    </div>
    </form>
</body>
</html>