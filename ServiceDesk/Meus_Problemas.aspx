<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Meus_Problemas.aspx.cs" Inherits="Meus_Problemas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Meus Problemas</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
     <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">           
            <tr>
                <td align="center" valign="middle" style="width: 100%;" colspan="2">
                  <div id="divMensagem" style="width: 100%;" runat="server" class="Mensagem" visible="true">
					<table width="776" border="0" cellspacing="5" cellpadding="0">
  <tr>
    <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
    <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
  </tr>
</table>    
</div> 
                    <table width="776" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr> 
                            <td height="35" align="center" valign="bottom">
                                    <table border="0" align="left"  cellpadding="0" cellspacing="0">
                                        <tr> 
                                            <td class="aba_esquerda_off">&nbsp;</td>
                                            <td class="aba_centro_off">Meus Problemas</td>
                                            <td class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                          </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <table width="100%"  border="0" cellspacing="0" cellpadding="0" class="tabela_padrao">
  <tr>
    <td align="left" valign="top">
	<table width="100%" border="0" cellpadding="0" cellspacing="2">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Panel ID="Panel1" runat="server" Height="380px" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                                           <asp:GridView ID="gvProblema" width="98%" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server" 
                                                CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvProblema_RowCommand" OnRowDataBound="gvProblema_RowDataBound">
                                                <FooterStyle CssClass="footerGrid" />
                                                <RowStyle BackColor="#F4FBFA" />
                                                <HeaderStyle CssClass="topoGrid" />
                                                    <Columns>
                                                      <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "problema_codigo")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField  HeaderText="Nome" >
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "nome")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tipo" >
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "tipo")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Propriet&#225;rio">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "proprietario")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inclus&#227;o">
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblDataInclusao" Text ='<%# DataBinder.Eval(Container.DataItem, "data_inclusao")%>' runat ="server"></asp:Label> 
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                        <asp:ButtonField CommandName="Exibir" Text="Exibir" ButtonType="Image" ImageUrl="~/images/icones/editar.gif" >
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                        </asp:ButtonField>
                                                        <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                            Text="Excluir" >
                                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width="30px" />
                                                        </asp:ButtonField>
                                                </Columns>
                                                <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                          </asp:GridView>
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
                </td>
            </tr>
        </table>
    </div>  
    </form>
</body>
</html>