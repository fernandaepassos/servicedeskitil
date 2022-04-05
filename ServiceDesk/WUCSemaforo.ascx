<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCSemaforo.ascx.cs" Inherits="WUCSemaforo" %>

<asp:Label ID="lblTotalAtendido" Text="0" Visible="false" runat="server"></asp:Label>
<asp:Label ID="lblTotalTMA" Text="0" Visible="false" runat="server"></asp:Label>
<asp:Label ID="lblTotalFila" Text="0" Visible="false" runat="server"></asp:Label>
<asp:Label ID="lblTotalTMF" Text="0" Visible="false" runat="server"></asp:Label>

<script type="text/javascript">
    function atualizaPagina() {
        window.location.reload();
    }
</script>

<div class="semaforoWrap">
    <asp:Repeater ID="rptSemaforo" runat="server" OnItemDataBound="rptSemaforo_OnItemDataBound">
        <HeaderTemplate>
            <table class="tabela_semaforo">
                <thead>
                    <tr>
                        <th style="font-size: 15px"><asp:Label ID="lblMes" runat="server"></asp:Label></th>
                        <th>Atendidos</th>
                        <th>TMA</th>
                        <th>Fila</th>
                        <th>TMF</th>
                        <th>Tempo Restante(T&eacute;cnico/Chamado)</th>
                    </tr>
                </thead>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td id="tdPrioridade" runat="server">
                    <asp:Label ID="lblPrioridadeCodigo" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "prioridade_codigo") %>'></asp:Label>
                    <asp:Label ID="lblPrioridadeDescricao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>'></asp:Label>
                </td>
                <td id="tdAtendido" runat="server"><asp:Label ID="lblAtendido" runat="server"></asp:Label></td>
                <td id="tdTMA" runat="server"><asp:Label ID="lblTMA" runat="server"></asp:Label></td>
                <td id="tdFila" runat="server"><asp:Label ID="lblFila" runat="server"></asp:Label></td>
                <td id="tdTMF" runat="server"><asp:Label ID="lblTMF" runat="server"></asp:Label></td>
                <td class="last">
                    <div style="width:350px;height:125px;overflow:auto">
                        <asp:GridView ID="gvAtendimentoTecnico" CssClass="menu" ShowHeader="true" Width="100%" GridLines="None" HorizontalAlign="Right" AutoGenerateColumns="False" runat="server" BorderColor="#CCCCCC" CellPadding="3" OnRowDataBound="gvAtendimentoTecnico_RowDataBound">
                            <RowStyle BackColor="#F4FBFA" />
                            <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                            <AlternatingRowStyle BackColor="White" />
                            <HeaderStyle BackColor="#d7ecec" />
                            <Columns>
                                <asp:TemplateField HeaderText="Tmp rest" HeaderStyle-Height="17px" ItemStyle-Width="60" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTempoVida" Text='<%# DataBinder.Eval(Container.DataItem, "tempo_vida")%>' Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblTempoSla" Text='<%# DataBinder.Eval(Container.DataItem, "tempo_sla_fim")%>' Visible="false" runat="server"></asp:Label>
                                        <asp:HyperLink ID="hlTempoRestante" Text="0" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Técnico Alocado" HeaderStyle-Height="17px" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTecnicoCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo_alocacao")%>' Visible="false" runat="server"></asp:Label>
                                        <asp:HyperLink ID="hlTecnico" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CHM" HeaderStyle-Height="17px" ItemStyle-Width="20" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "chamado_codigo") %>' runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="DT. Inclusao" HeaderStyle-Height="17px"  ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblData" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </ItemTemplate>

        <FooterTemplate>
            <tfoot>
                <tr>
                    <th>Total</th>
                    <th><asp:Label ID="lblAtendido" runat="server"></asp:Label></th>
                    <th><asp:Label ID="lblTMA" runat="server"></asp:Label></th>
                    <th><asp:Label ID="lblFila" Text="0" runat="server"></asp:Label></th>
                    <th><asp:Label ID="lblTMF" Text="0" runat="server"></asp:Label></th>
                    <th><asp:Label ID="lblTotal" runat="server"></asp:Label></th>
                </tr>
            <tfoot>
        </FooterTemplate>
    </asp:Repeater>
</div>
