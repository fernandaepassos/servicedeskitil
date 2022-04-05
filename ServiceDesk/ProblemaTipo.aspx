<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProblemaTipo.aspx.cs" Inherits="ProblemaTipo" %>
<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Tipo de Problema</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
    <div>
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
    <td align="center" valign="top">
	<table border="0" cellpadding="0" cellspacing="0" width="776">
            <tr>
                <td height="35" align="left" valign="bottom"><table align="left" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td class="aba_esquerda_off">&nbsp; </td>
                    <td class="aba_centro_off">Tipos de Problema</td>
                    <td class="aba_direita_off">&nbsp; </td>
                  </tr>
                </table></td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <table border="0" cellpadding="0" cellspacing="3" class="tabela_padrao" width="776">
                        <tr>
                            <td>
                                <div id="divNovoTipo" runat="server" class="Novo" visible="true">
                                    <div>
                        <asp:Panel ID="pnlGridProblemaTipo"  runat="server" ScrollBars="Vertical" Height="350px" Width="100%">
	                    <asp:GridView ID="gvProblemaTipo" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="98%" OnPageIndexChanging="gvProblemaTipo_PageIndexChanging" OnRowCancelingEdit="gvProblemaTipo_RowCancelingEdit" OnRowCommand="gvProblemaTipo_RowCommand" OnRowEditing="gvProblemaTipo_RowEditing" OnRowUpdating="gvProblemaTipo_RowUpdating">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F4FBFA" />
                        <Columns>
                            <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblCodigo" CssClass="tabela_padrao" runat="server" BorderStyle="None" Text='<%# DataBinder.Eval(Container.DataItem, "problema_tipo_codigo")%>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCodigo" runat="server" MaxLength="20" Text='<%# DataBinder.Eval(Container.DataItem, "problema_tipo_codigo")%>' Visible="false" Width="100"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                <ItemTemplate>
                                    <asp:TextBox CssClass="campo_descricao650" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "nome") %>' ></asp:TextBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox CssClass="campo_texto" Width="650px" ID="txtDescricao" runat="server" MaxLength="50" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <FooterStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                    <asp:CommandField ButtonType="Image" CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" 
                    EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True" 
                    UpdateImageUrl="images/icones/salvar.gif" UpdateText="Atualizar" >
                        <ItemStyle Width="40px" />
                            </asp:CommandField>                        
                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                Text="Excluir" >
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:ButtonField>
                        </Columns>
                        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle CssClass="topoGrid" />
                        <EditRowStyle BackColor="#C8E4E6" />
              <AlternatingRowStyle BackColor="White" />
              </asp:GridView>	
              </asp:Panel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;<asp:ImageButton ID="imgNovoTipo" runat="server" AlternateText="Novo Tipo de Urgência"
                                    ImageUrl="images/icones/inserir.gif" OnClick="imgNovoTipo_Click" />
                                Novo Tipo</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlNovoTipo" runat="server" BackColor="Transparent" GroupingText="Novo Tipo:"
                                    Height="20px" Visible="False" Width="100%">
                                    <table border="0" cellpadding="0" cellspacing="4" width="100%">
                                        <tr>
                                            <td width="78" align="left" >Descrição: </td>
                                            <td width="597" align="left"><asp:TextBox ID="txtDescricaoTipo" runat="server" CssClass="campo_texto" Width="597px" MaxLength="50"></asp:TextBox></td>
                                          <td width="83" align="center" valign="middle"><asp:Button ID="btnSalvar" runat="server" CssClass="botao" OnClick="btnSalvar_Click"
                                                    Text="Gravar" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
	</td>
  </tr>
</table>

      </div>
    </form>
</body>
</html>