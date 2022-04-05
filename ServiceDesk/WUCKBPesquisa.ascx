<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCKBPesquisa.ascx.cs" Inherits="teste" %>
<%@ Register Src="WUCBaseConhecimento.ascx" TagName="WUCBaseConhecimento" TagPrefix="uc1" %>

<table width="776" height="520" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top">
	<table width="100%"  border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center" valign="top"><table width="776" border="0" cellpadding="0" cellspacing="0" >
      <tr>
        <td align="left" valign="top">
          <table width="100%" border="0" cellpadding="0" cellspacing="2">
            <tr>
              <td align="left" valign="top">
                <asp:Panel ID="pnlConhecimentoAssociado" runat="server" BorderColor="Silver" BorderStyle="Solid"

                            BorderWidth="1px" Height="160px" ScrollBars="Auto" Width="257px" CssClass="dataGrid"> <asp:TreeView ID="tvTipoIC" runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" Height="401px" NodeIndent="25"

                                OnTreeNodePopulate="tvTipoIC_TreeNodePopulate"

                                PopulateNodesFromClient="False" ShowLines="True" OnSelectedNodeChanged="tvTipoIC_SelectedNodeChanged" NodeWrap="True" Width="257px"> </asp:TreeView> </asp:Panel>
              </td>
            </tr>
        </table>		  </td>
        <td align="left" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="2">
          <tr>
            <td align="left" valign="top">
              <asp:Panel ID="pnItemConfiguracao" runat="server" BorderColor="Silver" BorderStyle="Solid"

                            BorderWidth="1px" Height="160px" ScrollBars="Auto" Width="257px" CssClass="dataGrid"> <asp:TreeView ID="tvIC" runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" OnTreeNodePopulate="tvIC_TreeNodePopulate"

                                ShowLines="True" OnSelectedNodeChanged="tvIC_SelectedNodeChanged" Height="401px" NodeWrap="True" PopulateNodesFromClient="False" Width="257px"> </asp:TreeView> </asp:Panel>
            </td>
          </tr>
        </table></td>
        <td align="left" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="2">
          <tr>
            <td align="left" valign="top">
              <asp:Panel ID="plConhecimento" runat="server" BorderColor="Silver" BorderStyle="Solid"

                            BorderWidth="1px" Height="160px" ScrollBars="Auto" Width="245px" CssClass="dataGrid"> <asp:TreeView ID="tvConhecimento" runat="server" Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" Height="383px" NodeIndent="25"

                                PopulateNodesFromClient="False" ShowLines="True" OnSelectedNodeChanged="tvConhecimento_SelectedNodeChanged" NodeWrap="True" Width="245px"> </asp:TreeView> </asp:Panel>
            </td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td colspan="3" align="right" valign="top">
            <asp:HyperLink ID="hplNovaConhecimento" runat="server"><span style="padding:1px 20px 1px 20px; border:1px solid #3C8D8E; text-decoration: none; color: #333333; background-color:  #E9F5F5; font:10px Arial; FONT-WEIGHT:bold;cursor:hand; cursor:pointer;">Novo Conhecimento</span></asp:HyperLink>&nbsp;</td>
      </tr>
      <tr>
        <td colspan="3" align="center" valign="top" Height="200px">
            <asp:Panel ID="pnlGridConhecimento" runat="server" Height="200px" Width="98%" ScrollBars="Vertical" Visible="false" CssClass="dataGrid">
		<asp:GridView ID="gvConhecimento" runat="server" AutoGenerateColumns="False" BorderColor="Gray" CellPadding="4" CssClass="dataGrid" ForeColor="Black" GridLines="None" HorizontalAlign="Left" Width="748px" BackColor="White" Height="188px">
<FooterStyle CssClass="footerGrid" />
<RowStyle BackColor="#F4FBFA" />
<HeaderStyle CssClass="topoGrid" />
    <Columns>
        <asp:TemplateField HeaderText="Codigo" Visible="False">
            <ItemTemplate>
                <asp:Label ID="lblCodigo" visible=false runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "conhecimento_codigo")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Nome do conhecimento">
            <ItemTemplate>
                <asp:Label ID="lblNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="100px" />
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <a ID="VisualizaConhecimento" href="Javascript:VisualizaConhecimento(<%# DataBinder.Eval(Container.DataItem, "conhecimento_codigo")%>)">
                    <asp:Image ID="Image1" runat=server AlternateText="Detalhe" ImageUrl="~/images/exibir.gif" />
                </a>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateField>        
    </Columns>
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
    <EditRowStyle BackColor="White" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>

            </asp:Panel>
            <asp:Panel ID="pnlGridChamado" runat="server" Height="200px" Width="98%" ScrollBars="Vertical" Visible="false" CssClass="dataGrid">
		<asp:GridView ID="gvChamado" runat="server" AutoGenerateColumns="False" BorderColor="Gray" CellPadding="4" CssClass="dataGrid" ForeColor="Black" GridLines="None" HorizontalAlign="Left" Width="748px" BackColor="White" AllowSorting="True" CaptionAlign="Left" EnableTheming="True" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="195px">
<FooterStyle CssClass="footerGrid" />
<RowStyle BackColor="#F4FBFA" />
<HeaderStyle CssClass="topoGrid" />
    <Columns>
        <asp:TemplateField HeaderText="Descri&#231;&#227;o do chamado">
            <ItemTemplate>
                <asp:TextBox CssClass="campo_descricao650" ID="lblNome" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" Width="700px" />
            <ItemStyle HorizontalAlign="Left" Width="700px" Wrap="False" />
            <FooterStyle Width="700px" />
            <ControlStyle Width="700px" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <a href="Javascript:VisualizaChamado(<%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%>)">

                    <asp:Image ID="Image1" runat=server AlternateText="Detalhe" ImageUrl="~/images/exibir.gif" />
                </a>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Right" Wrap="False" />
            <ItemStyle HorizontalAlign="Right" Wrap="False" />
            <FooterStyle Wrap="False" />
        </asp:TemplateField>        
    </Columns>
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" Wrap="False" />
    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
    <EditRowStyle BackColor="White" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>

            </asp:Panel>                        
            <asp:Panel ID="pnlGridIncidente" runat="server" Height="200px" Width="98%" ScrollBars="Vertical" Visible="false" CssClass="dataGrid">
		<asp:GridView ID="gvIncidente" runat="server" AutoGenerateColumns="False" BorderColor="Gray" CellPadding="4" CssClass="dataGrid" ForeColor="Black" GridLines="None" HorizontalAlign="Left" Width="98%" BackColor="White">
<FooterStyle CssClass="footerGrid" />
<RowStyle BackColor="#F4FBFA" />
<HeaderStyle CssClass="topoGrid" />
    <Columns>
        <asp:TemplateField HeaderText="Descri&#231;&#227;o do incidente">
            <ItemTemplate>
                <asp:TextBox CssClass="campo_descricao650" ID="lblNome" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" Width="700px" />
            <ItemStyle HorizontalAlign="Left" Width="700px" />
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <a href="Javascript:VisualizaIncidente(<%# DataBinder.Eval(Container.DataItem, "incidente_codigo")%>)">

                    <asp:Image ID="Image1" runat=server AlternateText="Detalhe" ImageUrl="~/images/exibir.gif" />
                </a>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Right" Wrap="True" />
            <ItemStyle HorizontalAlign="Right" Wrap="True" />
        </asp:TemplateField>        
    </Columns>
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
    <EditRowStyle BackColor="White" />
    <AlternatingRowStyle BackColor="White" />
</asp:GridView>

            </asp:Panel>                        <asp:Panel ID="pnlGridSolicitacaoServico" runat="server" Height="200px" Width="98%" ScrollBars="Vertical" Visible="false" CssClass="dataGrid">
                <asp:GridView ID="gvSolicitacaoServico" runat="server" AutoGenerateColumns="False" BorderColor="Gray" CellPadding="4" CssClass="dataGrid" ForeColor="Black" GridLines="None" HorizontalAlign="Left" Width="98%" BackColor="White">
                    <FooterStyle CssClass="footerGrid" />
                    <RowStyle BackColor="#F4FBFA" />
                    <HeaderStyle CssClass="topoGrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Descri&#231;&#227;o da solicita&#231;&#227;o de servi&#231;o">
                            <ItemTemplate>
                                <asp:TextBox CssClass="campo_descricao650" ID="lblNome" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="700px" />
                            <ItemStyle HorizontalAlign="Left" Width="700px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <a href="Javascript:VisualizaRequisicaoServico(<%# DataBinder.Eval(Container.DataItem, "requisicaoservico_codigo")%>)">
                                    <asp:Image ID="Image1" runat=server AlternateText="Detalhe" ImageUrl="~/images/exibir.gif" />
                                </a>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                    <EditRowStyle BackColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </asp:Panel>
        </td>
      </tr>
    </table></td>
  </tr>
</table>	</td>
  </tr>
</table>