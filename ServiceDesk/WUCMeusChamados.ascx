<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCMeusChamados.ascx.cs" Inherits="WUCMeusChamados" %>
<table border="0" cellpadding="0" cellspacing="0" height="400" width="100%">
    <tr>
        <td align="center" colspan="2" style="width: 100%" valign="top">
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
            <table align="center" border="0" cellpadding="0" cellspacing="0" height="100%" width="776">
                <tr>
                    <td align="left" height="35" valign="bottom" width="776">
                        <table align="left" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="aba_esquerda_off">
                                    &nbsp;</td>
                                <td class="aba_centro_off">
                                    Chamados Encontrados</td>
                                <td class="aba_direita_off">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" height="400" valign="top">
                        <asp:Panel ID="pnlFiltro" runat="server" Height="350" Width="100%" CssClass="dataGrid">
                            <table border="0" cellpadding="0" cellspacing="5" class="tabela_padrao" height="350"
                                width="100%">
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
                                                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="campo_texto" Height="18px"
                                                        Width="98%"></asp:TextBox></td>
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
                                        <asp:DropDownList ID="ddlStatus" runat="server" Height="15px" Width="120px">
                                        </asp:DropDownList></td>
                                    <td align="left">
                                        Solicitante:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlSolicitante" runat="server" Height="15px" Width="99%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr align="center" valign="top">
                                    <td colspan="5">
                                        <asp:Panel ID="pnlGridChamados" runat="server" Height="350px" ScrollBars="Vertical"
                                            Width="760px">
                                            <asp:GridView ID="gvChamados" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                GridLines="None" HorizontalAlign="Center" OnRowDataBound="gvChamados_RowDataBound"
                                                ShowFooter="True" Width="740px">
                                                <FooterStyle CssClass="footerGrid" />
                                                <RowStyle BackColor="#F4FBFA" />
                                                <HeaderStyle CssClass="topoGrid" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID#">
                                                        <ItemTemplate>
                                                            <%# DataBinder.Eval(Container.DataItem, "chamado_codigo") %>
                                                        </ItemTemplate>
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
                                                            <asp:TextBox CssClass="campo_descricao" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="370px" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="370px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Codigostatus" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigoStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo") %>'
                                                                Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescricaoStatus" runat="server" Text='' Visible="true"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <a href='<%=strLinkPagina%><%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%><%=strFimLinkPagina%>'>
                                                                <asp:Image ID="Image1" runat="server" alt="Detalhes" ImageUrl="~/images/icones/detalhe.gif" /></a>
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
</table>
