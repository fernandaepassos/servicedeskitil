<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cockpit2.aspx.cs" Inherits="Cockpit2" %>

<!DOCTYPE html>

<html>

<head>
    <title>Help Desk  ITIL Compliance :: Cockpit</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
    <link rel="Stylesheet" href="css/bootstrap-grid.css" type="text/css" />

    <script src="js/PopUps.js" type="text/javascript"></script>
    <script src="js/jquery-1.12.0.min.js" type="text/javascript"></script>
    <%--<script src="js/bootstrap.min.js" type="text/javascript"></script>--%>
    <script src="js/jQuery.gridResize.js" type="text/javascript"></script>

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
            $('#pnlGridChamados').GridResize({ heightBase: 70 });
        }
    </script>
    <style>
        #wrapper {
            padding-left: 250px;
        }

        #sidebar {
            z-index: 1000;
            position: fixed;
            left: 250px;
            width: 250px;
            height: 100%;
            margin-left: -250px;
            overflow-y: auto;
            border-right: 1px solid gray;
        }

        #page-content {
            width: 100%;
            position: absolute;
            margin-right: -250px;
        }

        @media(min-width:768px) {
            #wrapper {
                padding-left: 250px;
            }

            #sidebar {
                width: 250px;
            }

            #page-content {
                position: relative;
                margin-right: 0;
            }
        }
    </style>
</head>

<body class="body">
    <form id="form1" runat="server" style="margin: 0 auto;">
        <div id="wrapper">
            <!-- Sidebar -->
            <div id="sidebar" class="tabela_tree" style="width: 250px;">
                <div id="filtro" style="border-bottom: 1px solid gray; padding: 2px 4px 5px 4px;">
                    <div class="ItensFlex start">
                        <span>Agrupar:</span>
                        <asp:DropDownList ID="ddlChamado" runat="server" Width="100%">
                            <asp:ListItem Value="chamado">Chamados</asp:ListItem>
                            <asp:ListItem Value="incidente">Incidentes</asp:ListItem>
                            <asp:ListItem Value="problema">Problemas</asp:ListItem>
                            <asp:ListItem Value="requisicaoservico">Requisição de Serviço</asp:ListItem>
                        </asp:DropDownList>

                        <span>Número:</span>
                        <asp:TextBox ID="txtCodigo" runat="server" Width="100%" CssClass="campo_texto"></asp:TextBox>

                        <span>Descrição:</span>
                        <asp:TextBox ID="txtDescricao" runat="server" Width="100%" CssClass="campo_texto" TextMode="MultiLine" Height="55px"></asp:TextBox>

                        <span>Proprietário:</span>
                        <asp:DropDownList ID="ddlProprietario" runat="server" Width="100%" />
                        <div class="ItensFlex itensCenter">
                            <asp:Button ID="Pesquisar" runat="server" Text="Pesquisar" OnClick="Pesquisar_Click" CssClass="botao round" Width="50%" />
                        </div>
                    </div>
                </div>
                <div id="arvore">
                    <div class="tabela_tree">
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" CssClass="dataGrid">
                            <asp:TreeView ID="trv_tabelas" runat="server" Width="98%" ExpandDepth="FullyExpand"
                                BorderColor="Transparent" BorderWidth="0px" OnSelectedNodeChanged="trv_tabelas_SelectedNodeChanged"
                                ShowLines="True" LeafNodeStyle-ChildNodesPadding="40px" NodeIndent="15"  >
                                <ParentNodeStyle CssClass="menu" />
                                <SelectedNodeStyle CssClass="menu" />
                                <RootNodeStyle CssClass="menu" />
                                <NodeStyle CssClass="menu" />
                            </asp:TreeView>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <!-- /Sidebar -->

            <!-- Content -->
            <div id="page-content">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12" style="padding-top:10px">
                            <div class="col-md-12 grid-bar grid-bar-top" style="border-top-left-radius: 1em; border-top-right-radius: 1em; display: flex; justify-content: space-between">
                                <span class="tituloFont" style="display: table-cell; padding: 4px 0 0 30px;"><asp:Literal ID="lblChamado" runat="server" Text="Chamados"></asp:Literal> Encontrados </span>
                            </div>
                            <div class="col-md-12 fundo_tabela-r">
                                <asp:Panel ID="pnlGridChamados" runat="server" Height="600px" ScrollBars="Auto" Width="100%">
                                    <asp:GridView ID="gvChamados" runat="server" Width="100%" GridLines="None" AutoGenerateColumns="False" CellPadding="4" HorizontalAlign="Center" OnLoad="gvChamados_Load" OnRowDataBound="gvChamados_RowDataBound" ShowFooter="True" CssClass="dataGrid">
                                       <%-- <FooterStyle CssClass="footerGrid" />--%>
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
                                                    <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, hfLabel.Value + "_codigo") %>'></asp:Label>
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
                            </div>
                            <div class="col-md-12 grid-bar grid-bar-bottom" style="border-bottom-left-radius: 1em; border-bottom-right-radius: 1em; display: flex; justify-content: space-between">
                                <span class="tituloFont" style="display: table-cell; padding: 4px 20px 0 0;">Total:<asp:Literal ID="litCountChamados" runat="server" /></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /Content -->

        </div>
        <asp:HiddenField ID="hfLabel" runat="server" />    
    </form>
</body>
</html>
