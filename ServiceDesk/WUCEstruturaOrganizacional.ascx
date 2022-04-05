<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCEstruturaOrganizacional.ascx.cs" Inherits="WUCEstruturaOrganizacional" %>
<table width="776"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="2">
                        <div id="divMensagem" style="width: 100%; height: 20px;" runat="server" class="Mensagem" visible="true">
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="60" align="center" valign="middle"><asp:Image ID="imgIcone" runat="server"  /></td>
                                    <td align="center" valign="middle" style="height: 20px"><asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="776"  border="0" cellspacing="1" cellpadding="0">
  <tr align="left" valign="middle">
    <td width="90">ID:</td>
    <td><asp:Label ID="lblIDEstrutura" runat="server"></asp:Label></td>
    <td width="73">Endere&ccedil;o:</td>
    <td><asp:TextBox ID="txtEndereco" MaxLength="200" runat="server" Width="296px" CssClass="campo_texto"></asp:TextBox></td>
  </tr>
  <tr align="left" valign="middle">
    <td>Descri&ccedil;&atilde;o:</td>
    <td align="left"><asp:TextBox ID="txtEstruturaOrganizacional" MaxLength="100" Width="296px" runat="server" CssClass="campo_texto"></asp:TextBox></td>
    <td>Telefone:</td>
    <td><asp:TextBox ID="txtTelefone" onKeyPress="FormataTel(this)" MaxLength="12" runat="server" Width="296px" CssClass="campo_texto"></asp:TextBox></td>
  </tr>
  <tr align="left" valign="middle">
    <td>N&iacute;vel Superior: </td>
    <td><asp:DropDownList ID="ddlEstruturaSuperior" runat="server" Width="300px"> </asp:DropDownList></td>
    <td>Fax:</td>
    <td><asp:TextBox ID="txtFax" onKeyPress="FormataTel(this)" MaxLength="12" runat="server" Width="296px" CssClass="campo_texto"></asp:TextBox></td>
  </tr>
  <tr align="left" valign="middle">
    <td>Tipo:</td>
    <td><asp:DropDownList ID="ddlTipoEstrutura" runat="server" Width="300px"> </asp:DropDownList></td>
    <td>CNPJ:</td>
    <td>
        <asp:TextBox ID="txtCnpj" MaxLength="14" onKeyPress="Somente_Numeros(this)" runat="server" Width="296px" CssClass="campo_texto"></asp:TextBox>
    </td>
  </tr>
  <tr align="left" valign="middle">
    <td>Sigla:</td>
    <td><asp:TextBox ID="txtSigla" MaxLength="3" runat="server" Width="296px" CssClass="campo_texto"></asp:TextBox></td>
    <td>Respons&aacute;vel:</td>
    <td><asp:DropDownList ID="ddlResponsavel" runat="server" Width="300px"></asp:DropDownList></td>
  </tr>
  <tr align="left" valign="middle">
    <td>Status:</td>
    <td><asp:DropDownList ID="ddlStatus" runat="server" Width="300px"></asp:DropDownList></td>
    <td>&nbsp;</td>
    <td align="center" valign="middle"><asp:Button ID="btnNovo" runat="server" CssClass="botao" Text="Novo" OnClick="btnNovo_Click"/>    
    <asp:Button ID="btnSalvar" runat="server" CssClass="botao" Text="Salvar" OnClick="btnSalvar_Click" /> 
    <asp:Button ID="btnExcluir" runat="server" Visible="false" CssClass="botao" Text="Excluir" OnClick="btnExcluir_Click" />
    </td>
  </tr>
</table>
                    </td>
                </tr>
</table>