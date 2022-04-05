<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AuditoriaLista.aspx.cs" Inherits="AuditoriaLista" %>
<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk ITIL Compliance :: Lista de Auditorias</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>

<script language="javascript">
<!--
function verifica() {
	if (confirm("Deseja mesmo excluir a Auditoria?")) {
		return true;
	}
	else {
		return false;
	}
}
-->
</script>

<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">           
            <tr>
                <td align="center" valign="top" style="width: 100%;" colspan="2">
                    <div id="divMensagem" style="width: 100%;" runat="server" class="Mensagem" visible="true">
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
              <td align="center" valign="top" style="width: 80%;" colspan="2"><table width="776" height="35" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td align="left" valign="bottom">
                    <table border="0" align="left"  cellpadding="0" cellspacing="0">
                      <tr>
                        <td class="aba_esquerda_off">&nbsp;</td>
                        <td class="aba_centro_off">Lista de Auditorias</td>
                        <td class="aba_direita_off">&nbsp;</td>
                      </tr>
                    </table>
                    </td>
                </tr>
              </table></td>
            </tr>
            <tr>
              <td align="center" valign="top" style="width: 80%;" colspan="2">
			  <table width="776" align="center" cellspacing="0" border="0" class="tabela_padrao">
                                  
                                  <tr>
                                    <td>
									  <asp:Panel ID="pnlGridChamados"  runat="server" ScrollBars="Vertical" Height="425px" Width="100%" CssClass="dataGrid">
                                      <asp:GridView ID="gvAuditoria" width="98%" HorizontalAlign="Center" GridLines="None" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="4" ShowFooter="True" OnRowCommand="gvAuditoria_OnRowCommand" OnRowDataBound="gvAuditoria_RowDataBound" OnLoad="gvAuditoria_Load">
                                        <RowStyle BackColor="#F4FBFA" />
                                        <HeaderStyle CssClass="topoGrid" />
                                          <Columns>
                                            <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                                              <ItemTemplate>
                                                <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "auditoria_codigo")%>' runat="server"></asp:Label>
                                              </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Nome">
                                              <ItemTemplate>
                                                <asp:TextBox CssClass="campo_descricao400" ID="lblNome" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "nome") %>' ></asp:TextBox>
                                              </ItemTemplate>
                                              <FooterTemplate>
                                              </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="400px" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="400px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Situa&#231;&#227;o">
                                              <ItemTemplate>
                                                <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo")%>' runat="server"></asp:Label>
                                              </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Data da Inclus&#227;o">
                                              <ItemTemplate>
                                                <asp:Label ID="lblDataInclusao" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao")%>' runat="server"></asp:Label>
                                              </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="images/icones/editar.gif" >
                                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                            </asp:ButtonField>
                                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif" >
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                            </asp:ButtonField>
                                          </Columns>
                                       <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                                       <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                       <EditRowStyle BackColor="#C8E4E6" />
                                       <AlternatingRowStyle BackColor="White" />
                                      </asp:GridView>
									  </asp:Panel>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td height="30" align="left">
									<asp:ImageButton ID="imgNovaAuditoria" ImageUrl="images/icones/inserir.gif" AlternateText="Nova Auditoria" runat="server" OnClick="novaAuditoria" />&nbsp;Nova Auditoria									</td>
                                  </tr>
                </table>
			  </td>
            </tr>
      </table>
    </div>
    </form>
</body>
</html>