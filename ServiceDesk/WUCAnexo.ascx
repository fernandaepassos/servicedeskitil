<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCAnexo.ascx.cs" Inherits="WUCAnexo" %>
<script type="text/javascript">
    function verifica() {
        return confirm("Deseja mesmo excluir este item?");
    }
</script>
<asp:Panel ID="pnlAnexo" runat="server" Width="100%" GroupingText="Arquivos Anexados" CssClass="dataGrid">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="70">Arquivo:</td>
            <td width="315" align="left" valign="middle">
<%--                <input type="file" style="width: 300px; height: 23px;" id="flDocumento" title="Documento" runat="server" enableviewstate="true" class="campo_texto" /></td>--%>
                <asp:FileUpload ID="flDocumento" runat="server" style="width: 300px; height: 23px;"  class="campo_texto"  enableviewstate="true" />
            <td width="95" align="left">Descri&ccedil;&atilde;o:</td>
            <td align="left" valign="middle">
                <asp:TextBox ID="txtDocumentoNome" runat="server" Width="223px" CssClass="campo_texto"></asp:TextBox></td>
            <td width="50" align="center" valign="middle">
                <asp:Button ID="btnSalvaDocumento" Text="Anexar" runat="server" OnClick="salvaDocumento" CssClass="botao" Width="50px" Height="22px" />
                <asp:Label ID="lblCodigoIdentificador" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblArquivoUpload" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lblTabela" runat="server" Visible="False"></asp:Label></td>
        </tr>
    </table>
    <asp:Panel ID="pnlGridAnexos" runat="server" ScrollBars="Auto" Height="180px" Width="100%">
        <asp:GridView ID="gvDocumento" GridLines="None" Width="98%" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="2" OnPageIndexChanging="gvDocumento_PageIndexChanging" OnRowDataBound="gvDocumento_RowDataBound" OnRowCommand="gvDocumento_RowCommand">
            <FooterStyle CssClass="footerGrid" />
            <HeaderStyle CssClass="topoGrid" />
            <RowStyle BackColor="#F4FBFA" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigo" Width="760px" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_codigo") %>' Visible="false" runat="server"></asp:Label>
                        <asp:Label ID="lblCaminho" Width="760px" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_caminho") %>' Visible="false" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Arquivo">
                    <ItemTemplate>
                        <asp:Label CssClass="campo_descricao350" ID="lblArquivo" Visible="false" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_caminho") %>'></asp:Label>
                        <asp:HyperLink ID="hplArquivo" ImageUrl="images/icones/exibir.gif" Text="Visualizar Arquivo" runat="server"></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" CssClass="menu" VerticalAlign="Middle" Width="80px" />
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                    <ItemTemplate>
                        <asp:Label CssClass="campo_descricao650" ID="txtNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_nome") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif">
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                    <HeaderStyle HorizontalAlign="Center" Width="20px" />
                </asp:ButtonField>
            </Columns>
            <PagerStyle CssClass="menu" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <EditRowStyle BackColor="#C8E4E6" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </asp:Panel>
</asp:Panel>
