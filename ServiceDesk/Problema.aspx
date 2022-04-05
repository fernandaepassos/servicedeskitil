<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Problema.aspx.cs" Inherits="Problema" %>

<%@ Register Src="WUCSolucao.ascx" TagName="WUCSolucao" TagPrefix="uc1" %>
<%@ Register Src="WUCSolucaoFiltro.ascx" TagName="WUCSolucaoFiltro" TagPrefix="uc2" %>
<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Dados do Problema</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
    <script language="javascript" type="text/javascript">
        <!--
        function TABLE1_onclick() {

        }
        function TABLE2_onclick() {

        }
        function TABLE3_onclick() {

        }
        // -->
    </script>
</head>

<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td height="0" align="center" valign="middle" colspan="2">
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
                  <td align="center" valign="top"  colspan="2"><table width="776" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td height="35" align="left" valign="bottom">
                        <table border="0" align="left" cellpadding="0" cellspacing="0">
                          <tr>
                            <td class="aba_esquerda_off">&nbsp; </td>
                            <td class="aba_centro_off">Dados do Problema</td>
                            <td class="aba_direita_off">&nbsp; </td>
                          </tr>
                      </table></td>
                    </tr>
                  </table></td>
                </tr>
                <tr>
                  <td align="center" valign="top"  colspan="2"><table width="776" border="0" align="center" cellpadding="0" cellspacing="0" class="tabela_padrao">
                    <tr>
                      <td align="center" valign="top"><table width="776" border="0" cellpadding="0" cellspacing="2">
                          <tr align="left" valign="middle">
                            <td width="300"> Nome:</td>
                            <td colspan="4" ><asp:TextBox ID="txtNome" runat="server"  CssClass="campo_texto" Width="660px"></asp:TextBox></td>
                          </tr>
                          <tr align="left" valign="middle">
                            <td>Tipo do Problema: </td>
                            <td width="247" align="left">
                              <asp:DropDownList ID="dlTipoProblema" runat="server"  CssClass="campo_texto" Width="260px"></asp:DropDownList>
                            </td>
                            <td width="7" >&nbsp;</td>
                            <td width="150" >Problema Fechado: </td>
                            <td width="257" ><asp:DropDownList ID="dlFlgProbFechado" runat="server"  CssClass="campo_texto" Width="254px">
                                  <asp:ListItem Selected="True" Value="N">N&#227;o</asp:ListItem>
                                  <asp:ListItem Value="S">Sim</asp:ListItem>
                            </asp:DropDownList>                              </td>
                          </tr>
                          <tr align="left" valign="middle">
                            <td>Equip. Alocada:</td>
                            <td align="left"><asp:DropDownList ID="cboNomeEquipe" runat="server"  CssClass="campo_texto" Width="260px" ></asp:DropDownList></td>
                            <td >&nbsp;</td>
                            <td >Pessoa alocada:</td>
                            <td ><asp:DropDownList ID="dlPessoaAlocada" runat="server" CssClass="campo_texto" Width="254px"
                                                                ></asp:DropDownList></td>
                          </tr>
                          <tr align="left" valign="middle">
                            <td> Status Problema:</td>
                            <td>
                            <asp:DropDownList ID="cboStatusProblema" runat="server"  CssClass="campo_texto" Width="260px"></asp:DropDownList></td>
                            <td>&nbsp; </td>
                            <td>Propriet&aacute;rio:</td>
                            <td> 
                                <asp:DropDownList ID="cboPessoaCadastro" runat="server"  CssClass="campo_texto" Width="254px"></asp:DropDownList>
                              <asp:TextBox ID="txtCdProblema" runat="server" Visible="False"  CssClass="campo_texto"></asp:TextBox>                            </td>
                          </tr>
                          <tr align="left" valign="middle">
                            <td> Descri&ccedil;&atilde;o</td>
                            <td colspan="4">
                              <asp:TextBox ID="txtDescricao" runat="server"  MaxLength="255" TextMode="MultiLine" Width="660px" CssClass="campo_texto"></asp:TextBox></td>
                          </tr>
                          <tr>
                            <td colspan="5" align="center" valign="middle">
                              <asp:Button ID="btnSalvar" CssClass="botao" Width="69px" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />                  
                              <asp:Button ID="btnExcluir" CssClass="botao" Width="69px" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />                  
                              <asp:Button ID="btnNovo" CssClass="botao" Width="69px" runat="server" OnClick="btnNovo_Click" Text="Novo" />                  
                              <asp:Button ID="btnEnviaEquipe" CssClass="botao" Width="139px" runat="server" Text="Enviar para equipe" OnClick="btnEnviaEquipe_Click" /></td>
                          </tr>
                      </table></td>
                    </tr>
                    <!-- abas -->
                    <tr>
                      <td align="left" valign="bottom">
                          <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                    <td id="aba_esq" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                    <td id="aba" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbIncidente" runat="server" OnClick="lkbIncidente_Click">Incidente</asp:LinkButton></td>
                                    <td id="aba_dir" runat="server" class="aba_direita_off">&nbsp;</td>
                                  </tr>
                              </table></td>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                    <td id="aba_esq1" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                    <td id="aba1" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbItemConfig" runat="server" OnClick="lkbItemConfig_Click">Item de configura&ccedil;&atilde;o</asp:LinkButton></td>
                                    <td id="aba_dir1" runat="server" class="aba_direita_off">&nbsp;</td>
                                  </tr>
                              </table></td>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                    <td id="aba_esq2" runat="server" class="aba_esquerda_off" >&nbsp;</td>
                                    <td id="aba2" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbMudanca" runat="server" OnClick="lkbMudanca_Click">Mudan&ccedil;a</asp:LinkButton></td>
                                    <td id="aba_dir2" runat="server" class="aba_direita_off">&nbsp;&nbsp; </td>
                                  </tr>
                              </table></td>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                    <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                    <td id="Td2" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbRecorrencia" runat="server" OnClick="lkbRecorrencia_Click">Recorr&ecirc;ncia</asp:LinkButton></td>
                                    <td id="Td3" runat="server" class="aba_direita_off">&nbsp;&nbsp; </td>
                                  </tr>
                              </table></td>
                              <td>
                                <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                    <td id="Td4" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                    <td id="Td5" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbSolucao" runat="server" OnClick="lkbSolucao_Click">Solu&ccedil;&atilde;o</asp:LinkButton></td>
                                    <td id="Td6" runat="server" class="aba_direita_off">&nbsp;&nbsp; </td>
                                  </tr>
                              </table></td>
                            </tr>
                        </table>
					  </td>
                    </tr>
                    <!-- fim abas -->
                    <tr>
                      <td class="tabela_abas"> 
					  <asp:MultiView ID="mtwAbas" runat="server"> <asp:View ID="vwIncidente" runat="server">
                        <asp:Panel ID="Panel1" runat="server"  Width="100%" HorizontalAlign="Center" CssClass="dataGrid">
                          <table width="100%"  border="0" cellspacing="2" cellpadding="0">
  							<tr>
    						  <td width="115" align="left" valign="middle"><asp:Label ID="Label13" runat="server" Text="Incidentes:"></asp:Label></td>
 						      <td align="left" valign="middle"><asp:DropDownList ID="dlNomeIncidente" runat="server" Width="520px" CssClass="campo_texto"> </asp:DropDownList></td>
 						      <td width="110" align="right"><asp:Button ID="btnAddIncidente" runat="server" OnClick="btnAddIncidente_Click1"
                                                    Text="Adicionar" CssClass="botao" />                  
                          <asp:Button ID="btnRemIncidente" runat="server" Text="Remover" Visible="False" CssClass="botao" /> </td>
  							</tr>
						  </table>
                          <asp:GridView ID="gdIncidente" Width="100%" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server"  CellPadding="4" ShowFooter="True"   OnRowCommand="gdIncidente_RowCommand">
                          <FooterStyle CssClass="footerGrid" /> 
                          <RowStyle BackColor="#F4FBFA" />                 
                          <HeaderStyle CssClass="topoGrid" />                  
                          <Columns>
                          <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                          <ItemTemplate>
                            <asp:Label ID="lblCodigoIncidente" Text='<%# DataBinder.Eval(Container.DataItem, "incidente_codigo")%>' runat="server"></asp:Label>
                          </ItemTemplate>
                          </asp:TemplateField> <asp:TemplateField HeaderText="Incidentes relacionados">
                          <ItemTemplate>
                          <asp:TextBox CssClass="campo_descricao200" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                          </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" />
                              <ItemStyle HorizontalAlign="Left" Width="200px" />
                          </asp:TemplateField> <asp:TemplateField HeaderText="Impacto">
                          <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "impacto")%> </ItemTemplate>
                          </asp:TemplateField> <asp:TemplateField HeaderText="Urg&#234;ncia">
                          <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "UrgenciaDescricao")%> </ItemTemplate>
                          </asp:TemplateField> <asp:TemplateField HeaderText="Prioridade">
                          <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "PrioridadeDescricao")%> </ItemTemplate>
                          </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                            Text="Excluir" >
                              <ItemStyle HorizontalAlign="Left" Width="20px" />
                          </asp:ButtonField>
                          </Columns>
                          <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                          <EditRowStyle BackColor="#2461BF" />
                          <AlternatingRowStyle BackColor="White" />         
      </asp:GridView> </asp:Panel>
                        </asp:View> <asp:View ID="vwItemConfig" runat="server">
                        <asp:Panel ID="Panel2" runat="server" Width="100%" CssClass="dataGrid">
                   <table width="100%"  border="0" cellspacing="2" cellpadding="0">
  							<tr>
    						  <td width="115" align="left" valign="middle"><asp:Label ID="Label14" runat="server" Text="Item de config.:"></asp:Label></td>
 						      <td align="left" valign="middle"><asp:DropDownList ID="dlItemConfiguracao" runat="server" Width="520px" CssClass="campo_texto"> </asp:DropDownList></td>
 						      <td width="110" align="right"><asp:Button ID="btnAddItemConfiguracao" runat="server" OnClick="btnAddItemConfiguracao_Click1" Text="Adicionar" CssClass="botao" />
							  <asp:Button ID="btnRemItemConfiguracao" runat="server" Text="Remover" Visible="False" CssClass="botao" /></td>
  							</tr>
						  </table>
        <asp:GridView ID="grItemConfiguracao" Width="100%" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server"  CellPadding="4"  ShowFooter="True"  OnRowCommand="grItemConfiguracao_RowCommand">
        <FooterStyle CssClass="footerGrid" /> 
        <RowStyle BackColor="#F4FBFA" />                 
        <HeaderStyle CssClass="topoGrid" />
        <Columns>
        <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
        <ItemTemplate>
          <asp:Label ID="lblCodigoItemConfig" Text='<%# DataBinder.Eval(Container.DataItem, "ic_codigo")%>' runat="server"></asp:Label>
        </ItemTemplate>
        </asp:TemplateField> <asp:TemplateField HeaderText="Itens de configura&#231;&#227;o relacionados">
        <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "nome")%> </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                                Text="Excluir" >
            <ItemStyle HorizontalAlign="Left" Width="20px" />
        </asp:ButtonField>
        </Columns>
        <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
        </asp:GridView>
                        </asp:Panel>
                        </asp:View> <asp:View ID="vwMudanca" runat="server">
                        <asp:Panel ID="Panel3" runat="server" Width="100%" CssClass="dataGrid">
                <table width="100%"  border="0" cellspacing="2" cellpadding="0">
  							<tr>
    						  <td width="115" align="left" valign="middle"><asp:Label ID="Label15" runat="server" Text="Mudança: "></asp:Label></td>
 						      <td align="left" valign="middle"><asp:DropDownList ID="dlMudanca" runat="server" Width="520px" CssClass="campo_texto"> </asp:DropDownList></td>
 						      <td width="110" align="right"><asp:Button ID="btnAddMudanca" runat="server" OnClick="btnAddMudanca_Click1" Text="Adicionar" CssClass="botao" />
							  <asp:Button ID="btnRemMudanca" runat="server" OnClick="btnRemMudanca_Click1" Text="Remover" Visible="False" CssClass="botao" /></td>
  							</tr>
						  </table>
        <asp:GridView ID="gdMudanca" Width="100%" GridLines="None" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server" 
                                                    CellPadding="4"  ShowFooter="True"   OnRowCommand="gdMudanca_RowCommand">
        <FooterStyle CssClass="footerGrid" /> 
        <RowStyle BackColor="#F4FBFA" />                 
        <HeaderStyle CssClass="topoGrid" />
        <Columns>
        <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
        <ItemTemplate>
          <asp:Label ID="lblCodigoMudanca" Text='<%# DataBinder.Eval(Container.DataItem, "mudanca_codigo")%>' runat="server"></asp:Label>
        </ItemTemplate>
        </asp:TemplateField> <asp:TemplateField HeaderText="Mudan&#231;as relacionadas">
        <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "descricao")%> </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                            Text="Excluir" >
            <ItemStyle HorizontalAlign="Left" Width="20px" />
        </asp:ButtonField>
        </Columns>
        <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" /> 
      </asp:GridView> </asp:Panel>
                        </asp:View> <asp:View ID="vwRecorrencia" runat="server"><asp:GridView ID="gdRecorrencia" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server"  CellPadding="4" GridLines="None" ShowFooter="True" OnRowCommand="gdRecorrencia_RowCommand" OnRowDataBound="gdRecorrencia_RowDataBound">
                        <FooterStyle CssClass="footerGrid" /> 
                        <RowStyle BackColor="#F4FBFA" />                 
                        <HeaderStyle CssClass="topoGrid" />                  
                        <Columns>
                        <asp:TemplateField HeaderText="CodigoRecorrencia" Visible="False">
                        <ItemTemplate>
                          <asp:Label ID="lblCodigoRecorrencia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "recorrencia_log_codigo")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="CodigoIdentificadorRegistro" Visible="False">
                        <ItemTemplate>
                          <asp:Label ID="lblCodigoIdentificadorRegistro" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "identificador_registro")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="CodigoOrigem" Visible="False">
                        <ItemTemplate>
                          <asp:Label ID="lblCodigoOrigem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "identificador_origem")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="Item de configura&#231;&#227;o  recorrido">
                        <ItemTemplate>
                          <asp:Label ID="lblNomeIC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>'></asp:Label>
                        </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField> <asp:TemplateField HeaderText="Data da recorr&#234;ncia">
                        <ItemTemplate>
                          <asp:Label ID="lblDataRecorrencia" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> <asp:ButtonField CommandName="Exibir" Text="Exibir" ImageUrl="~/images/icones/detalhe.gif" ButtonType="Image" >
                            <ItemStyle HorizontalAlign="Left" Width="20px" />
                        </asp:ButtonField>
                        </Columns>
                        <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />                  
      </asp:GridView> </asp:View> <asp:View ID="vwSolucao" runat="server">
      <uc2:WUCSolucaoFiltro ID="WUCSolucaoFiltro1" runat="server" />
      </asp:View> </asp:MultiView></td>
                    </tr>
                  </table></td>
                </tr>
            </table>
    </form>
</body>
</html>