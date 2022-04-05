<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCItemConfiguracaoRelatorio.ascx.cs" Inherits="WUCItemConfiguracaoRelatorio" %>
<div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
  <table width="776" border="0" cellspacing="5" cellpadding="0">
    <tr>
      <td width="60" align="center" valign="bottom">
        <asp:Image ID="imgIcone" runat="server" /></td>
      <td align="center" valign="bottom">
        <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
    </tr>
  </table>
</div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center" valign="top">
      <table width="776" border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td height="35" align="left" valign="bottom">
            <table border="0" align="left" cellpadding="0" cellspacing="0">
              <tr>
                <td class="aba_esquerda_off">&nbsp;</td>
                <td class="aba_centro_off">Item de Configuração</td>
                <td class="aba_direita_off">&nbsp;</td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td align="center" valign="top">
      <table width="776" class="tabela_padrao" border="0" cellspacing="0" cellpadding="5">
        <asp:Repeater ID="rptStatus" runat="server" OnItemDataBound="rptStatus_DataBound">
          <ItemTemplate>
            <asp:Label ID="lblCodigo" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo")%>'></asp:Label>
            <tr align="left">
              <td><%# DataBinder.Eval(Container.DataItem, "descricao")%></td>
              <td><asp:Label ID="lblStatusQuantidade" runat="server"></asp:Label></td>
            </tr>
          </ItemTemplate>
        </asp:Repeater>
      </table>
    </td>
  </tr>
</table>