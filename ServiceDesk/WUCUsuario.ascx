<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCUsuario.ascx.cs" Inherits="WUCUsuario" %>
<script language="javascript" src="js/funcoes.js"></script>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <div id="formItens" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td width="120" style="height: 22px">
                            <asp:Label ID="lblPessoaCodigo" runat="server" Visible="false"></asp:Label><asp:Label ID="lblNomeUsuario" runat="server">Usuário:</asp:Label></td>
                        <td colspan="3" style="height: 22px">
                            <asp:TextBox ID="txtNomeUsuario" runat="server" CssClass="campo_texto" Width="586px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMatricula" runat="server">Matricula:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtMatricula" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                        <td width="120">
                            <asp:Label ID="lblEmpresa" runat="server">&nbsp;&nbsp;&nbsp;Empresa:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtEmpresa" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCodigoRede" runat="server" Width="92px">Chave de rede:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtCodigoRede" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblArea" runat="server">&nbsp;&nbsp;&nbsp;Área:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtArea" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTelefone" runat="server">Telefone/Ramal:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtTelefoneRamal" onKeyPress="FormataTel(this)" MaxLength="12" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblCargo" runat="server">&nbsp;&nbsp;&nbsp;Cargo:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtCargo" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVip" runat="server">Usuário VIP:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtUsuarioVIP" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblEMail" runat="server">&nbsp;&nbsp;&nbsp;e-Mail:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblEquipamento" runat="server">Equipamento:</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtEquipamento" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="lblLocalizacao" runat="server">&nbsp;&nbsp;&nbsp;Localização:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocalizacao" CssClass="campo_texto" runat="server" Width="232px"></asp:TextBox></td>
                    </tr>
                </table>
            </div>
        </td>
        <td width="100px">
            <div class="ItensFlex" style="align-items:center">
                <asp:Button ID="btnPesquisa" CssClass="botao" runat="server" Text="Pesquisar" OnClick="btnPesquisa_Click" />
                <asp:Button ID="btnProprioSolicitante" runat="server" CssClass="botao" OnClick="btnProprioSolicitante_Click" Text="Sou solicitante" />
                <asp:Button ID="btnlimpar" runat="server" CssClass="botao" OnClick="btnlimpar_Click" Text="Limpar" />
                <img src="images/Pessoafoto.jpg" border="0" />
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlSolicitante" runat="server" Width="100%" ScrollBars="Auto" Height="100px" CssClass="dataGrid">
    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" HorizontalAlign="Left" Width="98%" BorderColor="Gray" CssClass="dataGrid" Height="100%" OnRowCommand="gvUsuarios_RowCommand">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F4FBFA" />
        <Columns>
            <asp:ButtonField ButtonType="Image" CommandName="Selecionar" Text="Selecionar" ImageUrl="images/icones/selecionado.gif">
                <ItemStyle HorizontalAlign="Center" Width="20px" />
                <HeaderStyle HorizontalAlign="Center" Width="20px" />
            </asp:ButtonField>
            <asp:TemplateField HeaderText="C&#243;digo">
                <ItemTemplate>
                    <asp:Label ID="lblPessoaCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Matr&#237;cula">
                <ItemTemplate>
                    <asp:Label ID="lblPessoaMatricula" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "matricula")%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                <ItemStyle HorizontalAlign="Center" Width="120px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Nome do Usu&#225;rio">
                <ItemTemplate>
                    <asp:Label ID="lblPessoaNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle BackColor="White" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle CssClass="topoGrid" />
        <EditRowStyle BackColor="White" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</asp:Panel>
