<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCMeusProblemas.ascx.cs" Inherits="WUCMeusProblemas" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="center" colspan="2" style="width: 100%" valign="middle">
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
            <table align="center" border="0" cellpadding="0" cellspacing="0" width="776">
                <tr>
                    <td align="center" height="35" valign="bottom">
                        <table align="left" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="aba_esquerda_off">
                                    &nbsp;</td>
                                <td class="aba_centro_off">
                                    Meus Problemas</td>
                                <td class="aba_direita_off">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0" class="tabela_padrao" width="100%">
                            <tr>
                                <td align="left" valign="top">
                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Vertical" Width="776px" CssClass="dataGrid">
                                                    <asp:GridView ID="gvProblema" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnRowCommand="gvProblema_RowCommand"
                                                        OnRowDataBound="gvProblema_RowDataBound" Width="776px">
                                                        <FooterStyle CssClass="footerGrid" />
                                                        <RowStyle BackColor="#F4FBFA" />
                                                        <HeaderStyle CssClass="topoGrid" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "problema_codigo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nome">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "nome")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tipo">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "tipo")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Propriet&#225;rio">
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "proprietario")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inclus&#227;o">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDataInclusao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Exibir" ImageUrl="~/images/icones/editar.gif"
                                                                Text="Exibir">
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                                            </asp:ButtonField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                                Text="Excluir">
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
