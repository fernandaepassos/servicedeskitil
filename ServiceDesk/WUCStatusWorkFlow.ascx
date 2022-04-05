<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCStatusWorkFlow.ascx.cs" Inherits="WUCStatusWorkFlow" %>
<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td colspan="2">
	    <asp:Label ID="lblTabela" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="lblCodigoItem" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="lblCodigoStatusAtual" runat="server" Visible="false"></asp:Label>
	</td>
  </tr>
  <tr>
    <td width="100" align="left" valign="middle">Status Atual: </td>
    <td align="left"><asp:TextBox ID="txtStatusAtual" CssClass="campo_texto" Enabled="false" runat="server" Width="282px" BackColor="#FFFFC0" ForeColor="Black" Font-Bold="True"></asp:TextBox></td>
  </tr>
  <tr>
    <td width="100" align="left" valign="middle"><asp:Label ID="lblStatusFuturo" runat="server" Text="Próximo:" Width="71px">  </asp:Label></td>
    <td align="left"><asp:DropDownList ID="ddlStatusFuturo" runat="server" Width="284px" CssClass="combo" BackColor="White"></asp:DropDownList></td>
  </tr>
</table>