<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkFlow.aspx.cs" Inherits="WorkFlow" %>

<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="javascript">
function atualiza() {
  var o = window.event.srcElement;
  if (o.tagName == "INPUT" && o.type == "checkbox") {
    __doPostBack("","");
  }
}
function verifica() {
	if (confirm("Deseja mesmo excluir o Item?")) {
		return true;
	}
	else {
		return false;
	}
}
</script>

<head id="Head1" runat="server">
  <title>Help Desk  ITIL Compliance :: Atributos do Item de Configuração</title>
  <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body leftmargin="0" rightmargin="0" topmargin="0" class="body">
  <form id="form1" runat="server" style="margin:0">
    <div>
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr id="Tr1" runat="server">
          <td height="0" align="center" valign="middle">
            <div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem"
              visible="false">
              <table width="776" border="0" cellspacing="5" cellpadding="0">
                <tr>
                  <td width="60" align="center" valign="bottom">
                    <asp:Image ID="imgIcone" runat="server" /></td>
                  <td align="center" valign="bottom">
                    <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                </tr>
              </table>
            </div>
          </td>
        </tr>
        <tr id="Tr2" runat="server">
          <td height="20" align="center" valign="middle">&nbsp;		  </td>
        </tr>
        <tr id="Tr3" runat="server">
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
                                <td align="left" valign="middle" class="tituloFont">Workflow</td>
                                <td width="120" align="left" valign="middle" class="tituloFont">
								
                          <asp:ImageButton ID="imgNovoWorkFlowTipo" ImageUrl="images/icones/inserir.gif" AlternateText="Novo Tipo de WorkFlow" runat="server" OnClick="novoWorkFlowTipo" />                    
&nbsp;Novo WorkFlow								</td>
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
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td align="left">
                          <asp:Panel ID="pnlGridChamados"  runat="server" ScrollBars="Vertical" Height="100px" Width="100%" CssClass="dataGrid">
						  <asp:GridView ID="gvWorkFlowTipo" GridLines="None" width="98%" AllowPaging="True" PageSize="5" AutoGenerateColumns="False" runat="server" CellPadding="3" CssClass="dataGrid" ShowFooter="True" OnRowCommand="gvWorkFlowTipo_OnRowCommand" OnRowDataBound="gvWorkFlowTipo_RowDataBound" OnPageIndexChanging="gvWorkFlowTipo_PageIndexChanging" OnRowEditing="gvWorkFlowTipo_OnRowEditing" OnRowCancelingEdit="gvWorkFlowTipo_OnRowCancelingEdit" OnRowUpdating="gvWorkFlowTipo_OnRowUpdating" OnLoad="gvWorkFlowTipo_Load">
                            <FooterStyle />                    
                            <HeaderStyle CssClass="topoGrid" />                    
                            <RowStyle BackColor="#F4FBFA" /> <EditRowStyle BackColor="#C8E4E6" />
                            <PagerStyle HorizontalAlign="Center" />                    
                            <SelectedRowStyle BackColor="#A3D1D6" Font-Bold="True" ForeColor="#333333" /> <AlternatingRowStyle BackColor="White" />
                            <Columns>
                            <asp:CommandField ButtonType="Image" CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" ShowEditButton="true" EditImageUrl="images/icones/editar.gif" EditText="Editar" UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" ItemStyle-Width="40" /> <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                            <ItemTemplate>
                              <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "workflow_tipo_codigo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                              <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "workflow_tipo_codigo")%>' runat="server"></asp:Label>
                            </EditItemTemplate>
                            </asp:TemplateField> <asp:TemplateField HeaderText="Descri&ccedil;&atilde;o" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Left" ItemStyle-Width="55%">
                            <ItemTemplate>
                              <asp:TextBox CssClass="campo_descricao450" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                            </ItemTemplate>
                            <EditItemTemplate>
                              <asp:TextBox CssClass="campo_texto" Width="450" ID="txtDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />                    
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />                    
        </asp:TemplateField> <asp:TemplateField HeaderText="Tabela" HeaderStyle-HorizontalAlign="Center">
        <ItemTemplate>
          <asp:Label ID="lblTabela" Text='<%# DataBinder.Eval(Container.DataItem, "tabela")%>' runat="server"></asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="txtTabela" CssClass="campo_texto" Width="120" Text='<%# DataBinder.Eval(Container.DataItem, "tabela")%>' runat="server"></asp:TextBox>
        </EditItemTemplate>
        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120"/>
        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="120"/>
        </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Fluxo" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center" ImageUrl="images/icones/detalhe.gif" Text="Inserir Fluxos" /> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center" ImageUrl="images/icones/excluir.gif" Text="Excluir" />
                            </Columns>
                          </asp:GridView> </asp:Panel>                        </td>
                      </tr>
                      <asp:Panel ID="pnNovoWorkFlowTipo" runat="server">
                        <tr>
                          <td height="30" align="left" valign="bottom">
                            <table height="19" border="0" align="left" cellpadding="0" cellspacing="0">
                              <tr>
                                <td class="aba_esquerda_off">&nbsp; </td>
                                <td class="aba_centro_off">Novo WorkFlow</td>
                                <td class="aba_direita_off">&nbsp; </td>
                              </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td align="center" valign="top">                            <table width="92%" border="0" cellpadding="0" cellspacing="2">
                            <tr>
                              <td width="100" align="left">Descri&ccedil;&atilde;o:</td>
                              <td align="left" valign="middle">
                                <asp:Label ID="lblCodigoWorkFlow" Visible="false" runat="server"></asp:Label>
                                <asp:TextBox ID="txtDescricao" MaxLength="255" Width="250" runat="server" CssClass="campo_texto"></asp:TextBox>
                                <img src="images/icones/info.gif" onmouseover="this.style.cursor='help'" alt="Campo obrigat&oacute;rio." /> </td>
                              <td width="100" align="left" valign="middle"> Tabela: </td>
                              <td align="left" valign="middle">
                                <asp:TextBox ID="txtTabela" MaxLength="50" Width="250" runat="server" CssClass="campo_texto"></asp:TextBox>
                                <img src="images/icones/info.gif" onmouseover="this.style.cursor='help'" alt="Campo obrigat&oacute;rio." /> </td>
                            </tr>
                            <tr>
                              <td colspan="4" align="center">
                                <asp:Button ID="btnSalvaWorkFlowTipo" CssClass="botao" runat="server" Text="Salvar" OnClick="insereWorkFlowTipo" />                          
    </td>
                            </tr>
                          </table></td>
                        </tr>
                      </asp:Panel>
                      <asp:Panel ID="pnNovoWorkFlow" runat="server">
                        <tr>
                          <td align="left" valign="bottom"> </td>
                        </tr>
                        <tr>
                          <td align="left" valign="top">
                            <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td width="265" align="left" valign="top" class="dataTree">
                                  <asp:Panel ID="pnTreeViewWorkFlow" CssClass="dataGrid" ScrollBars="Both" Height="330px" runat="server">
								  <asp:TreeView ID="trvWorkFlow" onclick="atualiza()" Height="100%" ShowCheckBoxes="All" PopulateNodesFromClient="true" OnSelectedNodeChanged="selecionaNo" ShowLines="true" OnTreeNodePopulate="populaNos" OnTreeNodeCheckChanged="marcaNo" runat="server"></asp:TreeView> </asp:Panel>
                                </td>
                                <td rowspan="3" align="center" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                  <tr>
                                    <td align="center" valign="top"><table width="500" border="0" cellpadding="0" cellspacing="2">
                                      <tr align="left">
                                        <td width="115"> Status: </td>
                                        <td>
                                          <asp:DropDownList ID="ddlWorkFlowStatus" runat="server" Width="360px"></asp:DropDownList>
                                          <img src="images/icones/info.gif" onmouseover="this.style.cursor='help'" alt="Campo obrigat&oacute;rio." /> </td>
                                      </tr>
                                      <tr align="left">
                                        <td>Superior:</td>
                                        <td>
                                          <asp:DropDownList ID="ddlWorkFlowSuperior" runat="server" Width="360px"></asp:DropDownList>
                                        </td>
                                      </tr>
                                      <tr align="left">
                                        <td>Descri&ccedil;&atilde;o:</td>
                                        <td><asp:TextBox ID="txtWorkFlowDescricao" CssClass="campo_texto" MaxLength="50" Width="356px" runat="server"></asp:TextBox></td>
                                      </tr>
                                      <tr>
                                        <td align="left">Condi&ccedil;&atilde;o:</td>
                                        <td align="left" colspan="2">
                                          <asp:TextBox ID="txtWorkFlowPreCondicao" CssClass="campo_texto" TextMode="MultiLine" MaxLength="3000" Height="50px" Width="356px" runat="server"></asp:TextBox>
                                        </td>
                                      </tr>
                                      <tr align="left">
                                        <td>Semi-autom&aacute;tico: </td>
                                        <td><asp:DropDownList ID="ddlSemiAutomatico" runat="server" Width="360px">
                                            <asp:ListItem Text="N&atilde;o" Value="N"></asp:ListItem>
                                            <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                                          </asp:DropDownList>
                                        </td>
                                      </tr>
                                      <tr align="left">
                                        <td>Tabela Repercus&atilde;o: </td>
                                        <td><asp:DropDownList ID="ddlRepercusaoTabela" runat="server" Width="360px"></asp:DropDownList>
                                        </td>
                                      </tr>
                                      <tr align="left">
                                        <td>Status Repercus&atilde;o:</td>
                                        <td><asp:DropDownList ID="ddlRepercusaoStatus" runat="server" Width="360px"></asp:DropDownList></td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                                      <tr align="center" valign="middle">
                                        <td height="30"> 
                                          <asp:Button ID="btnSalvaWorkFlow" CssClass="botao" runat="server" Text="Salvar" Width="60px" OnClick="salvaWorkFlow" />                                                                              
                                          <asp:Button ID="btnNovoWorkFlow" CssClass="botao" runat="server" Text="Novo" Width="60px" OnClick="novoWorkFlow" />                                              </td>
                                        </tr>
                                      <tr align="center" valign="middle">
                                        <td height="40" align="left" valign="top">
                                          <asp:Panel ID="Panel1"  runat="server" ScrollBars="Both" Height="150" Width="100%" CssClass="dataGrid"> <asp:GridView ID="gvWorkFlowRepercusao" width="98%" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server" BorderColor="#CCCCCC" CellPadding="3" CssClass="dataGrid" OnRowCommand="gvWorkFlowRepercusao_OnRowCommand" OnRowDataBound="gvWorkFlowRepercusao_RowDataBound">
                                            <FooterStyle CssClass="footerGrid" />                                    
                                            <HeaderStyle CssClass="topoGrid" />                                    
                                            <RowStyle BackColor="#F4FBFA" />
                                            <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />                                    
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                            <asp:TemplateField HeaderText="C&oacute;digo" Visible="False">
                                            <ItemTemplate>
                                              <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "workflow_repercusao_codigo")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField> <asp:TemplateField HeaderText="Tabela Repercus&atilde;o">
                                            <ItemTemplate>
                                              <asp:Label ID="lblTabela" Text='<%# DataBinder.Eval(Container.DataItem, "tabela_destino")%>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField> <asp:TemplateField HeaderText="Status Repercus&atilde;o">
                                            <ItemTemplate>
                                              <asp:Label ID="lblStatusCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo_destino")%>' runat="server" Visible="false"></asp:Label>
                                              <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center" ImageUrl="images/icones/excluir.gif" Text="Excluir Repercus&atilde;o" />
                                            </Columns>
                                          </asp:GridView> </asp:Panel>
                                        </td>
                                      </tr>
                                    </table></td>
                                  </tr>
                                </table></td>
                              </tr>
                              <tr>
                                <td height="30" align="center" valign="middle">
                                  <asp:Button ID="btExcluiWorkFlow" CssClass="botao" Text="Exclui Selecionados" OnClick="excluiWorkFlow" runat="server" />                    
            </td>
                              </tr>
                          </table></td>
                        </tr>
                      </asp:Panel>
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