<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCMeusIncidentes.ascx.cs" Inherits="WUCMeusIncidentes" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr id="Tr1" runat="server">
        <td align="center" height="0" valign="middle">
            <div id="divMensagem" runat="server" class="Mensagem" style="width: 100%" visible="true">
                <table border="0" cellpadding="0" cellspacing="5" width="776">
                    <tr>
                        <td align="center" valign="bottom" width="60">
                            <asp:Image ID="imgIcone" runat="server" /></td>
                        <td align="center" valign="bottom">
                            <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2" valign="top">
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="776">
                <tr>
                    <td align="left" height="35" valign="bottom">
                        <table align="left" border="0" cellpadding="0" cellspacing="0" height="19">
                            <tr>
                                <td class="aba_esquerda_off">
                                    &nbsp;</td>
                                <td class="aba_centro_off">
                                    Meus Incidentes</td>
                                <td class="aba_direita_off">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td align="left" valign="top">
                                    <asp:Panel ID="pnlFiltro" runat="server" Width="100%" CssClass="dataGrid">
                                        <table border="0" cellpadding="0" cellspacing="5" class="tabela_padrao" width="776">
                                            <tr>
                                                <td align="left" height="20" width="55">
                                                    Código:</td>
                                                <td align="left" width="291">
                                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="campo_texto" Height="18px" Width="98%"></asp:TextBox></td>
                                                <td align="left" width="69">
                                                    Descrição:
                                                </td>
                                                <td align="left" width="290">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtDescricao" runat="server" CssClass="campo_texto" Width="100%"></asp:TextBox></td>
                                                            <td align="right" valign="middle" width="9%">
                                                                <asp:Button ID="Button1" runat="server" CssClass="botao" Text="..." />&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center" rowspan="2" valign="middle" width="39">
                                                    <asp:Button ID="btnFiltra" runat="server" CssClass="botao" OnClick="btnFiltra_Click"
                                                        Text="Filtrar" /></td>
                                            </tr>
                                            <tr align="center" valign="middle">
                                                <td align="left" height="20">
                                                    Status:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="campo_texto" Width="120px">
                                                    </asp:DropDownList></td>
                                                <td align="left">
                                                    Solicitante:
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="ddlSolicitante" runat="server" CssClass="campo_texto" Width="100%">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr align="center" valign="top">
                                                <td colspan="5">
                                                    <asp:Panel ID="Panel1" runat="server" Height="350px" ScrollBars="Vertical" Width="760px" CssClass="dataGrid">
                                                        <asp:GridView ID="gvIncidentes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                            GridLines="None" HorizontalAlign="Center" OnRowDataBound="gvIncidente_RowDataBound"
                                                            ShowFooter="True" Width="740px">
                                                            <FooterStyle CssClass="footerGrid" />
                                                            <HeaderStyle CssClass="topoGrid" />
                                                            <RowStyle BackColor="#F4FBFA" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ID#">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "incidente_codigo") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Data">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "data_inclusao") %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "descricao") %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Codigostatus" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo") %>'
                                                                            Visible="false"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Status">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDescricaoStatus" runat="server" Text='' Visible="true"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <a href='<%=strLinkPagina%><%# DataBinder.Eval(Container.DataItem, "incidente_codigo")%><%=strFimLinkPagina%>'>
                                                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/icones/detalhe.gif" /></a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
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
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
