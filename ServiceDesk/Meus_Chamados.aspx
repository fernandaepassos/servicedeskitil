<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Meus_Chamados.aspx.cs" Inherits="Meus_Chamados" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Help Desk  ITIL Compliance :: Meus Chamados</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css" />
    <link rel="Stylesheet" href="css/bootstrap-grid.css" type="text/css" />

    <script src="js/PopUps.js" type="text/javascript"></script>
    <script src="js/jquery-1.12.0.min.js" type="text/javascript"></script>
    <script src="js/jQuery.GridResize.js" type="text/javascript"></script>

        <script type="text/javascript">
        //script para fazer resize do gridview junto com o resize da janela
        var timer;

        $(document).ready(function () {
            resizeGrid();
        });

        $(window).resize(function () {
            clearTimeout(timer);
            timer = setTimeout(function () { resizeGrid(); }, 200);
        });

        function resizeGrid() {
            $('#pnlGridChamados').GridResize({ heightBase: 90 });
        }
    </script>
</head>
<body class="body">
    <div class="container-fluid">
        <form id="form1" runat="server" style="margin: 0 auto;">
            <div class="row col-md-12">
                <div id="divMensagem" style="padding: 4px 0 0 15px;" runat="server" class="Mensagem" visible="true">
                    <asp:Image ID="imgIcone" runat="server" />
                    <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label>
                </div>
            </div>
            <!--Div que pega toda a table-->
            <div style="padding-top:10px">
                <div class="col-md-12 grid-bar grid-bar-top" style="border-top-left-radius: 1em; border-top-right-radius: 1em; display: flex; justify-content: space-between">
                    <span class="tituloFont" style="display: table-cell; padding: 4px 0 0 30px;">Chamados Encontrados </span>
                </div>
                <div class="col-md-12 fundo_tabela-r">
                    <asp:Panel ID="pnlFiltro" runat="server" CssClass="form-inline" Height="25px" Style="padding: 3px 0px 0px 45px;">
                        <!--Filtros-->
                        <div class="form-group">
                            <label for="txtCodigo">Código: </label>
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="campo_texto" type="number" min="0" placeholder="Somente números"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="txtDescricao">Descri&ccedil;&atilde;o: </label>
                            <asp:TextBox ID="txtDescricao" runat="server" Width="320px" CssClass="campo_texto" MaxLength="50" placeholder="Máximo 50 caracteres"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label for="ddlStatus">Status: </label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlStatus">Prioridade: </label>
                            <asp:DropDownList ID="ddlPrioridade" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlStatus">Tipo: </label>
                            <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnFiltra" runat="server" CssClass="botao" Text="Filtrar" OnClick="btnFiltra_Click" />
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlGridChamados" runat="server" Height="600px" ScrollBars="Vertical" Width="100%">
                        <asp:GridView ID="gvChamados" runat="server" ClientIDMode="Static" GridLines="None" AutoGenerateColumns="False" CellPadding="4" ShowHeader="true"
                            HorizontalAlign="Center" OnRowDataBound="gvChamados_RowDataBound" ShowFooter="False" CssClass="table table-bordered" Style="text-align: center">
                            <FooterStyle CssClass="footerGrid" />
                            <RowStyle BackColor="#F4FBFA" />
                            <HeaderStyle CssClass="topoGrid" />
                            <Columns>
                                <asp:TemplateField HeaderText="Exibir">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChamadoCodigo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%>' runat="server"></asp:Label>
                                        <a href='<%=strLinkPagina%><%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%><%=strFimLinkPagina%>'>
                                            <asp:Image ID="imgDetalhes1" runat="server" alt="Detalhes" ImageUrl="~/images/icones/detalhe.gif" /></a>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID#">
                                    <ItemTemplate>
                                        <%#ClsParametro.ChamadoPrefixo%><%# DataBinder.Eval(Container.DataItem, "chamado_codigo") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                    <ItemTemplate>
                                        <asp:TextBox CssClass="campo_descricao400" ID="lblDescricao" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="410px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="410px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Incluído em">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataInclusao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Codigostatus" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescricaoStatus" runat="server" Text='' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="80px" />
                                    <ItemStyle Width="80px" />
                                </asp:TemplateField>
                                <%--Adicionado Tipo--%>
                                <asp:TemplateField HeaderText="TipoCodigo" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipoCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "chamado_tipo_codigo") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tipo" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTipo" runat="server" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <%--Adicionado Tipo--%>
                                <asp:TemplateField HeaderText="CodigoPrioridade" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoPrioridade" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "prioridade_codigo") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prioridade">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrioridade" runat="server" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dt.Altera&#231;&#227;o">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataAlteracao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_alteracao") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CodigoAlterador" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoAlterador" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo_alterador") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Alterador">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAlterador" runat="server" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CodigoSolicitante" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoSolicitante" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo_solicitante") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Solicitante">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSolicitante" runat="server" Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dt.Finaliza&#231;&#227;o">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataFinalizacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_finalizacao") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CodigoFinalizador" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCodigoFinalizador" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo_finalizador") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Finalizador">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinalizador" runat="server" Text='' Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dt.Avalia&#231;&#227;o">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataAvaliacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_avaliacao") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="VIP">
                                    <ItemTemplate>
                                        <asp:Label ID="lblVip" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "vip") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Escalado">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEscalado" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "escalado") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dt.Agendamento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDataAgendamento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_agendamento") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tempo de Vida">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTempoDeVida" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tempo_vida") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tempo de Atendimento">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTempoDeAtendimento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tempo_atendimento") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="150px" />
                                    <ItemStyle HorizontalAlign="Center" Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLA In&#237;cio">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSlaInicio" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tempo_sla_inicio") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SLA Fim">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSlaFim" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tempo_sla_fim") %>'
                                            Visible="true"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Exibir">
                                    <ItemTemplate>
                                        <a href='<%=strLinkPagina%><%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%><%=strFimLinkPagina%>'>
                                            <asp:Image ID="imgDetalhes2" runat="server" ImageUrl="~/images/icones/detalhe.gif" /></a>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                    <ItemStyle Width="20px" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />

                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div class="col-md-12 grid-bar grid-bar-bottom" style="border-bottom-left-radius: 1em; border-bottom-right-radius: 1em; display: flex; justify-content: space-between">
                    <span class="tituloFont" style="display: table-cell; padding: 4px 20px 0 0;">Total de Chamados:
                        <asp:Literal ID="litCountChamados" runat="server" />
                    </span>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
