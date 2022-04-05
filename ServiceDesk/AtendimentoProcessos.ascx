<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AtendimentoProcessos.ascx.cs"
    Inherits="AtendimentoProcessos" %>
<%@ Register Src="WUCSemaforo.ascx" TagName="WUCSemaforo" TagPrefix="uc3" %>

<table width="99%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td align="left" valign="top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="170" valign="bottom">&nbsp;</td>
                    <td height="35" align="left" valign="bottom">
                        <table cellpadding="0" cellspacing="1" border="0">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td id="Td7" runat="server" class="aba_esquerda_off">&nbsp;
                                            </td>
                                            <td class="aba_centro_off" id="Td11" runat="server">
                                                <asp:LinkButton ID="lkbResultado" runat="server" OnClick="mudaAba" CommandArgument="2">Resultado</asp:LinkButton>
                                            </td>
                                            <td id="Td9" runat="server" class="aba_direita_off">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td id="Td8" runat="server" class="aba_esquerda_off">&nbsp;
                                            </td>
                                            <td class="aba_centro_off" id="Td12" runat="server">
                                                <asp:LinkButton ID="lkbSemaforo" runat="server" OnClick="mudaAba" CommandArgument="3">Semaf&oacute;ro</asp:LinkButton>
                                            </td>
                                            <td id="Td10" runat="server" class="aba_direita_off">&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="250" align="center" valign="top" style="border: 1px solid gray;padding: 0 4px 0 4px">
                        <table width="98%" height="98%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    <table cellpadding="0" cellspacing="1" border="0">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp;
                                                        </td>
                                                        <td class="aba_centro_off" id="Td2" runat="server">
                                                            <asp:LinkButton ID="lkbFiltro" runat="server" OnClick="mudaAba" CommandArgument="0">Filtro</asp:LinkButton></td>
                                                        <td id="Td3" runat="server" class="aba_direita_off">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td id="Td4" runat="server" class="aba_esquerda_off">&nbsp;
                                                        </td>
                                                        <td class="aba_centro_off" id="Td5" runat="server">
                                                            <asp:LinkButton ID="lkbAgrupamento" runat="server" OnClick="mudaAba" CommandArgument="1">Agrupamento</asp:LinkButton></td>
                                                        <td id="Td6" runat="server" class="aba_direita_off">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <asp:Label ID="lblChamado" Visible="false" runat="server"></asp:Label>
                            <tr>
                                <td align="center" valign="middle">
                                    <asp:MultiView ID="mvLateral" runat="server">
                                        <asp:View ID="vwFiltro" runat="server">
                                            <table width="250" height="98%" border="0" cellspacing="0" cellpadding="0" class="tabela_semaforo">
                                                <tr>
                                                    <td align="left">Agrupar:</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlChamado" runat="server" Width="100%">
                                                            <asp:ListItem Value="chamado">Chamados</asp:ListItem>
                                                            <asp:ListItem Value="incidente">Incidentes</asp:ListItem>
                                                            <asp:ListItem Value="problema">Problemas</asp:ListItem>
                                                            <asp:ListItem Value="requisicaoservico">Requisição de Serviço</asp:ListItem>
                                                        </asp:DropDownList></td>
                                                </tr>
                                                <tr>
                                                    <td align="left">N&uacute;mero:</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCodigo" runat="server" Width="245px" CssClass="campo_texto"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left">Descri&ccedil;&atilde;o:</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtDescricao" runat="server" Width="245px" CssClass="campo_texto"
                                                            TextMode="MultiLine" Height="60px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td align="left">Propriet&aacute;rio:</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:DropDownList ID="ddlProprietario" runat="server" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="30" align="center" valign="middle">
                                                        <asp:Button ID="Pesquisar" runat="server" Text="Pesquisar" OnClick="Pesquisar_Click"
                                                            CssClass="botao" Width="50%" /></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                        <asp:View ID="vwArvore" runat="server">
                                            <table width="250" height="479" class="tabela_semaforo">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <asp:Panel ID="Panel1" runat="server" Height="479" Width="250px" BorderColor="White"
                                                            BorderStyle="None" BorderWidth="0px" ScrollBars="Both" CssClass="dataGrid">
                                                            <asp:TreeView ID="trv_tabelas" runat="server" Width="98%" ExpandDepth="FullyExpand"
                                                                BorderColor="Transparent" BorderWidth="0px" OnSelectedNodeChanged="trv_tabelas_SelectedNodeChanged"
                                                                ShowLines="True">
                                                                <ParentNodeStyle CssClass="menu" />
                                                                <SelectedNodeStyle CssClass="menu" />
                                                                <RootNodeStyle CssClass="menu" />
                                                                <NodeStyle CssClass="menu" />
                                                            </asp:TreeView>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:View>
                                    </asp:MultiView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="left" valign="top" style="border: 1px solid gray">
                        <table width="98%" height="100%" border="0" cellspacing="0" cellpadding="0" class="tabela_semaforo">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Panel ID="pnlGridResultado" runat="server" Height="500px" ScrollBars="Auto" HorizontalAlign="Center" CssClass="dataGrid">
                                        <asp:GridView ID="gvChamados" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Center" OnLoad="gvChamados_Load" OnRowDataBound="gvChamados_RowDataBound" ShowFooter="True" CssClass="dataGrid">
                                            <FooterStyle CssClass="footerGrid" />
                                            <RowStyle BackColor="#F4FBFA" />
                                            <HeaderStyle CssClass="topoGrid" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hplLink1" ImageUrl="~/images/icones/editar.gif" Text="Editar"
                                                            runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrefixo" runat="server" Text=''></asp:Label>
                                                        <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, lblChamado.Text + "_codigo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Data">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "data_inclusao") %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                                    <ItemTemplate>
                                                        <asp:TextBox CssClass="campo_descricao400" ID="lblDescricao" runat="server" TextMode="MultiLine"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" Wrap="False" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Propriet&#225;rio">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoProprietario" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo_proprietario") %>'></asp:Label>
                                                        <asp:Label ID="lblDescricaoProprietario" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblDescricaoStatus" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prioridade">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoPrioridade" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "prioridade_codigo") %>'
                                                            Visible="false"></asp:Label>
                                                        <asp:Label ID="lblDescricaoPrioridade" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hplLink" ImageUrl="~/images/icones/editar.gif" Text="Editar" runat="server"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle Width="30px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMensagemGrid" Text="Selecione na árvore de Agrupamento um item." runat="server"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlSemaforo" runat="server" Height="450px" Width="99%" ScrollBars="Vertical"
                                        HorizontalAlign="Left" Visible="false" CssClass="dataGrid">
                                        <uc3:WUCSemaforo ID="WUCSemaforo1" runat="server" />
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
