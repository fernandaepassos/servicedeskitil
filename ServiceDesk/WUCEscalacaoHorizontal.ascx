<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCEscalacaoHorizontal.ascx.cs" Inherits="WUCEscalacaoHorizontal" %>
<table width="100%"  border="0" cellpadding="0" cellspacing="1">
  <tr>
    <td width="71" align="left">Nível:</td>
    <td align="left"><asp:DropDownList ID="ddlNivel" runat="server" AutoPostBack="True" Width="305px" CssClass="combo" OnSelectedIndexChanged="ddlNivel_SelectedIndexChanged"></asp:DropDownList></td>
  </tr>
  <tr>
    <td align="left">Equipe:</td>
    <td align="left"><asp:DropDownList ID="ddlEquipe" runat="server" AutoPostBack="True" Width="305px" CssClass="combo"
        OnSelectedIndexChanged="ddlEquipe_SelectedIndexChanged"></asp:DropDownList></td>
  </tr>
  <tr>
    <td align="left">T&eacute;cnico:</td>
    <td align="left"><asp:DropDownList ID="ddlTecnico" runat="server" Width="305px" CssClass="combo"></asp:DropDownList></td>
  </tr>
</table>