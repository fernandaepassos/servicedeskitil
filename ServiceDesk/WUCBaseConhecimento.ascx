<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCBaseConhecimento.ascx.cs" Inherits="BaseConhecimento" %>
<%@ Register Src="WUCSolucaoFiltro.ascx" TagName="WUCSolucaoFiltro" TagPrefix="uc1" %>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td height="0" align="center" valign="middle" colspan="2">
            <div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
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
    <tr>
        <td>
            <table width="100%" border="0" cellpadding="0" cellspacing="2">
                <tr>
                    <td width="71" align="left">Nome do conhecimento:</td>
                    <td align="left">
                        <asp:TextBox ID="txtNomeConhecimento" runat="server" Width="390px" CssClass="campo_texto" MaxLength="100"></asp:TextBox></td>
                    <td align="left" width="51">Status:</td>
                    <td width="30%" align="left">
                        <asp:DropDownList ID="dlStatusConhecimento" runat="server" Width="227px" CssClass="campo_texto"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td align="left" colspan="4">Descrição:</td>
                </tr>

                <tr>
                    <td colspan="4" align="center" valign="middle">
                        <asp:TextBox ID="txtDescricaoConhecimento" runat="server" Width="762px" CssClass="campo_texto" Height="100px" MaxLength="7500" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" valign="middle">&nbsp;<asp:TextBox ID="txtCodConhecimento" runat="server" Visible="False" Width="27px"></asp:TextBox>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" CssClass="botao" />
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" OnClick="btnExcluir_Click" CssClass="botao" />
                        <asp:Button ID="btnNovo" runat="server" Text="Novo" Width="52px" OnClick="btnNovo_Click" CssClass="botao" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" align="left">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
                            <tr>
                                <td id="Td10" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                <td id="Td11" runat="server" class="aba_centro_off">
                                    <asp:LinkButton ID="lkbProjeto" runat="server" OnClick="lkbProjeto_Click">Solução</asp:LinkButton></td>
                                <td id="Td12" runat="server" class="aba_direita_off">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td id="Td7" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                <td id="Td8" runat="server" class="aba_centro_off">
                                    <asp:LinkButton ID="lkbAnexo" runat="server" OnClick="lkbAnexo_Click">Anexo</asp:LinkButton></td>
                                <td id="Td9" runat="server" class="aba_direita_off">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td id="Td13" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                <td id="Td14" runat="server" class="aba_centro_off">
                                    <asp:LinkButton ID="lkbPerfil" runat="server" OnClick="lkbPerfil_Click">Visualização</asp:LinkButton></td>
                                <td id="Td15" runat="server" class="aba_direita_off">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td id="Td16" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                <td id="Td17" runat="server" class="aba_centro_off">
                                    <asp:LinkButton ID="lkbConhecimento" runat="server" OnClick="lkbConhecimento_Click">Conhecimentos Relacionados x Item configuração</asp:LinkButton></td>
                                <td id="Td18" runat="server" class="aba_direita_off">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="tabela_abas">
            <asp:MultiView ID="mvAbas" runat="server">
                <asp:View ID="vwProjeto" runat="server">
                    <uc1:WUCSolucaoFiltro ID="WUCSolucaoFiltro1" runat="server" />
                </asp:View>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:View ID="vwAnexo" runat="server">
                                <table width="100%" border="0" cellpadding="0" cellspacing="2">
                                    <tr>
                                        <td align="left" width="71">Arquivo:&nbsp;
                                        </td>
                                        <td colspan="2" align="left">
                                            <%--<input id="flDocumento" runat="server" title="Documento" type="file" style="width: 100%; height: 23px;" class="campo_texto" size="20" />--%>
                                            <asp:FileUpload ID="flDocumento" runat="server" style="width: 100%; height: 23px;"  class="campo_texto" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="left">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="73" align="left">Nome:</td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtNomeAnexo" runat="server" Width="99%" CssClass="campo_texto"></asp:TextBox></td>
                                                    <td width="80" align="right">
                                                        <asp:Button ID="btnSalvarAnexo" runat="server" Text="Salvar" Width="90px" OnClick="btnSalvarAnexo_Click" CssClass="botao" Height="23px" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>

                                <asp:Panel ID="plAnexo" runat="server" Height="127px" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                                    <asp:GridView ID="gvAnexo" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        GridLines="None" HorizontalAlign="Left" ShowFooter="True" Width="749px" OnRowCommand="gvAnexo_RowCommand" OnRowDataBound="gvAnexo_RowDataBound" BorderColor="Gray" CssClass="dataGrid">
                                        <FooterStyle CssClass="footerGrid" />
                                        <RowStyle BackColor="#F4FBFA" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigoAnexo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_codigo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Caminho" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCaminho" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_caminho")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nome" Visible="False">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_nome")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Nome">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArquivo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                Text="Excluir">
                                                <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Middle" />
                                            </asp:ButtonField>
                                        </Columns>
                                        <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle CssClass="topoGrid" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </asp:View>
                        </td>
                    </tr>
                </table>

                <asp:View ID="vwPerfil" runat="server">
                    <table width="100%">
                        <tr>
                            <td>Disponível visualização para:</td>
                            <td>
                                <asp:DropDownList ID="dlPerfil" runat="server" Width="530px" CssClass="campo_texto"></asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnSalvarPerfil" runat="server" Text="Salvar" Width="61px" OnClick="btnSalvarPerfil_Click" CssClass="botao" />
                            </td>
                        </tr>
                    </table>
                    <asp:Panel ID="plPerfil" runat="server" BorderColor="Gray" Height="127px" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                        <asp:GridView ID="gvPerfil" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" HorizontalAlign="Left" ShowFooter="True" Width="749px" OnRowCommand="gvPerfil_RowCommand" BorderColor="Gray" CssClass="dataGrid">
                            <FooterStyle CssClass="footerGrid" />
                            <RowStyle BackColor="#F4FBFA" />
                            <Columns>
                                <asp:TemplateField HeaderText="CodigoConhecimento" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoConhecimento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "conhecimento_codigo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CodigoPerfil" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoPerfil" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "perfil_codigo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Visualizadores">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescricao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grupo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAplicaca" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "aplicacao")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="250px" />
                                    <ItemStyle HorizontalAlign="Center" Width="250px" />
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Image" ImageUrl="~/images/icones/excluir.gif" Text="Excluir" CommandName="Excluir">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:ButtonField>
                            </Columns>
                            <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle CssClass="topoGrid" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </asp:View>
                <asp:View ID="vwConhecimento" runat="server">
                    <table width="100%" border="0" cellpadding="0" cellspacing="2">
                        <tr>
                            <td width="252" align="left" valign="top">
                                <table width="252" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Panel ID="pnlConhecimentoAssociado" runat="server" Height="93px" ScrollBars="Auto" Width="252px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="dataGrid">
                                                <asp:TreeView ID="tvTipoIC" PopulateNodesFromClient="False" ShowLines="True" runat="server" Width="252px" NodeIndent="25" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small" ForeColor="Black" Height="384px" OnTreeNodePopulate="tvTipoIC_TreeNodePopulate" OnSelectedNodeChanged="tvTipoIC_SelectedNodeChanged" NodeWrap="True">
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="252" align="center" valign="top">
                                <table width="252" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Panel ID="pnItemConfiguracao" runat="server" Height="93px" ScrollBars="Auto" Width="252px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="dataGrid">
                                                <asp:TreeView ID="tvIC" PopulateNodesFromClient="False" ShowLines="True" runat="server" ShowCheckBoxes="All" Width="252px" NodeIndent="25" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small" ForeColor="Black" Height="383px" OnTreeNodePopulate="tvIC_TreeNodePopulate" OnSelectedNodeChanged="tvIC_SelectedNodeChanged" NodeWrap="True">
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnSalvarConhecimentoAssociado" runat="server" Text="Associar" OnClick="btnSalvarConhecimentoAssociado_Click" CssClass="botao" /></td>
                            <td width="252" align="center" valign="top">
                                <table width="252" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" valign="top">
                                            <asp:Panel ID="plConhecimento" runat="server" Height="93px" ScrollBars="Auto" Width="252px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CssClass="dataGrid">
                                                <asp:TreeView ID="tvConhecimento" PopulateNodesFromClient="False" ShowLines="True" runat="server" ShowCheckBoxes="All" Width="252px" NodeIndent="25" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="Small" ForeColor="Black" Height="383px" NodeWrap="True">
                                                </asp:TreeView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Button ID="btnAssociarConhecimento" runat="server" Text="Associar" OnClick="btnAssociarConhecimento_Click" CssClass="botao" /></td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </td>
    </tr>
</table>
