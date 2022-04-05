<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCLog.ascx.cs" Inherits="WUCLog" %>


<asp:Panel ID="pnlGrupoLog" runat="server"  Width="100%" GroupingText="Log de Operações:" CssClass="dataGrid" style="max-height:200px">
    <asp:Panel ID="pnlLog" runat="server" Width="100%" ScrollBars="Vertical" CssClass="dataGrid" style="max-height:170px">
        <asp:GridView ID="gvLog" runat="server" AutoGenerateColumns="False"
            BackColor="White" GridLines="None" CellPadding="4"
            OnRowDataBound="gvLog_RowDataBound" PageSize="5" Width="100%">
            <FooterStyle BackColor="#FDF9DC" Font-Bold="True" ForeColor="#47451F" />
            <RowStyle BackColor="White" ForeColor="#1D164C" />
            <Columns>
                <asp:TemplateField HeaderText="Quem Alterou">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "log_evento_codigo") %>'
                            Visible="false"></asp:Label>
                        <asp:Label ID="lblCodigoPessoa" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>'
                            Visible="false"></asp:Label>
                    <asp:Label ID="lblNomePessoa" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Data">
                    <ItemTemplate>
                        <asp:Label ID="lblData" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                    <ItemTemplate>
                        <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="550px" />
                    <HeaderStyle HorizontalAlign="Left" Width="550px" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#FDF9DC" ForeColor="#47451F" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle CssClass="topoGrid" />
            <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" />
        </asp:GridView>
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlGrupoLogStatus" runat="server" Width="100%" Visible="False" GroupingText="Log de Status:" CssClass="dataGrid" style="max-height:200px">
    <asp:Panel ID="pnlLogStatus" runat="server" Width="100%" ScrollBars="Vertical" Visible="False" CssClass="dataGrid" style="max-height:170px">
        <asp:GridView ID="gvLogStatus" runat="server" AutoGenerateColumns="False"
            BackColor="White" GridLines="None" CellPadding="4"
            OnRowDataBound="gvLogStatus_RowDataBound" PageSize="5" Width="98%">
            <FooterStyle BackColor="#FDF9DC" Font-Bold="True" ForeColor="#47451F" />
            <RowStyle BackColor="White" ForeColor="#1D164C" />
            <Columns>
                <asp:TemplateField HeaderText="Quem Alterou">
                    <ItemTemplate>
                        <asp:Label ID="lblCodigoStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_log_codigo") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblCodigoPessoaStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>' Visible="false"></asp:Label>
                        <asp:Label ID="lblNomePessoaStatus" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                    <HeaderStyle HorizontalAlign="Left" Width="200px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Data">
                    <ItemTemplate>
                        <asp:Label ID="lblDataStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status Origem">
                    <ItemTemplate>
                        <asp:Label ID="lblStatusOrigem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo_origem") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status Destino">
                    <ItemTemplate>
                        <asp:Label ID="lblStatusDestino" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo_destino") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="#FDF9DC" ForeColor="#47451F" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle CssClass="topoGrid" />
            <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" />
        </asp:GridView>
    </asp:Panel>
</asp:Panel>
