<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCPriorizacao.ascx.cs" Inherits="WUCPriorizacao" %>
<table width="100%"  border="0" cellspacing="1" cellpadding="0">
  <tr>
    <td width="71">Impacto:</td>
    <td><asp:DropDownList ID="ddlImpacto" runat="server" CssClass="combo" Width="305px"></asp:DropDownList></td>
  </tr>
  <tr>
    <td>Urg&ecirc;ncia:</td>
    <td><asp:DropDownList ID="ddlUrgencia" runat="server" CssClass="combo" Width="305px"></asp:DropDownList></td>
  </tr>
  <tr>
    <td>Prioridade:</td>
    <td><asp:TextBox ID="txtPrioridade" runat="server" CssClass="combo" Width="303px" BackColor="#FFFFC0" Enabled="false" Font-Bold="True" Font-Names="Times New Roman"></asp:TextBox></td>
  </tr>
</table>