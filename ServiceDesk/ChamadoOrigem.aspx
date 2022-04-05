<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChamadoOrigem.aspx.cs" Inherits="ChamadoOrigem" %>
<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Origem do Chamado</title>
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
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">           
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
                <td height="20" align="center" valign="middle">&nbsp;              </td>
            </tr>            
            <tr>
              <td align="center" valign="top" colspan="2"><table width="97%"  border="0" cellspacing="0" cellpadding="0">
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
                                    <td align="left" valign="middle" class="tituloFont">Origem do Chamado</td>
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
                                <td width="91" align="left">Descri&ccedil;&atilde;o:</td>
                                <td align="left">
                                  <asp:TextBox ID="txtDescricao" CssClass="campo_texto" MaxLength="100" runat="server" Width="520px"></asp:TextBox>                                </td>
                                <td align="center" valign="middle">
                                  <asp:Button ID="btnLimpar" Width="80px" CssClass="botao" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />                            
    </td>
                                <td align="center" valign="middle">
                                  <asp:Button ID="btnSalvar" Width="80px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />                            
    </td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td align="left" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td><table height="19" border="0" align="left"  cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="aba_esquerda_off">&nbsp;</td>
                                    <td class="aba_centro_off">Origem do chamado</td>
                                    <td class="aba_direita_off">&nbsp;</td>
                                  </tr>
                                </table></td>
                              </tr>
                              <tr>
                                <td><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td align="center" valign="top">
                                        <asp:Panel ID="pnlGridAtributo"  runat="server" Height="400px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid"> <asp:GridView ID="gvChamadoOrigem" GridLines="None" Width="98%" HorizontalAlign="Center" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="false" OnRowCommand="gvChamadoOrigem_OnRowCommand" OnRowDataBound="gvChamadoOrigem_RowDataBound">
                                          <FooterStyle CssClass="footerGrid" />                            
                                          <HeaderStyle CssClass="topoGrid" />                            
                                          <RowStyle BackColor="#F4FBFA" />
                                          <Columns>
                                          <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                                          <ItemTemplate>
                                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "chamado_origem_codigo")%>' runat="server"></asp:Label>
                                          </ItemTemplate>
                                          </asp:TemplateField> <asp:TemplateField  HeaderText="Descri&#231;&#227;o">
                                          <ItemTemplate>
                                            <asp:TextBox CssClass="campo_descricao650" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="600px" />                            
                                          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="600px" />                            
              </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="images/icones/editar.gif">
              <ItemStyle HorizontalAlign="Center" Width="40px" />
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
              </asp:ButtonField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif">
              <ItemStyle HorizontalAlign="Center" Width="20px" />
              <HeaderStyle HorizontalAlign="Center" Width="20px" />
              </asp:ButtonField>
                                          </Columns>
                                          <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />                            
                                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#2461BF" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>
                                      </td>
                                    </tr>
                                </table></td>
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
    </div>
    </form>
</body>
</html>