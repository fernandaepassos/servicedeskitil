<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCItemConfiguracaoTreeView.ascx.cs" Inherits="WUCItemConfiguracaoTreeView" %>

<table width="100%">
  <tr>
    <td>
      <asp:Panel ID="pnlCIs" runat="server" Height="150px" HorizontalAlign="Left"
        ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
        <asp:TreeView ID="tvw_CI" runat="server" OnTreeNodePopulate="tvwCI_TreeNodePopulate" ShowCheckBoxes="All" ShowLines="True">
        </asp:TreeView>
      </asp:Panel>
    </td>
  </tr>
  <tr>
    <td align="right">
      <asp:Label ID="lblCodigoIdentificador" runat="server" Visible=false></asp:Label>
      <asp:Button ID="btnSalvar" runat="server" CssClass="botao" Text="Salvar" OnClick="btnSalvar_Click" /></td>
  </tr>
</table>
