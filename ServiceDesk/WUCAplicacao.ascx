<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCAplicacao.ascx.cs" Inherits="WUCAplicacao" %>
<script language="javascript">
	function Somente_Numeros(codigo)
	{ 
		var Tecla = event.which;
		if(Tecla == null)
		{
			Tecla = event.keyCode;
			if ( Tecla < 48 ||  Tecla > 57)
			{
				event.returnValue = false;
	    		return false
			}
			event.returnValue = true;
			return true
	    }  
	}

    function verifica() {
	    if (confirm("Deseja mesmo excluir este item?")) {
		    return true;
	    }
	    else {
		    return false;
	    }
    }
</script> 
<table width="100%"  border="0" cellspacing="0" cellpadding="0">
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
        <td colspan="2" align="left" valign="top">      <table width="100%"  border="0" cellspacing="2" cellpadding="0">
          <tr align="left" valign="middle">
            <td width="130">ID:</td>
            <td colspan="5"><asp:Label ID="lblIDAplicacao" runat="server"></asp:Label>			</td>
          </tr>
          <tr align="left" valign="middle">
            <td>Descri&ccedil;&atilde;o:</td>
            <td colspan="5"><asp:TextBox ID="txtDescricao" CssClass="campo_texto" MaxLength="200" runat="server" Width="650px"></asp:TextBox>			</td>
          </tr>
          <tr align="left" valign="middle">
            <td>Aplica&ccedil;&atilde;o Superior:</td>
            <td colspan="3"><asp:DropDownList ID="ddlAplicacaoSuperior" runat="server" Width="98%"> </asp:DropDownList></td>
            <td colspan="2" valign="top">
			  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="120"><asp:CheckBox ID="ckInstalacaoPadrao" Text="Instala&ccedil;&atilde;o Padr&atilde;o" runat="server" /></td>
                  <td width="140"><asp:CheckBox ID="ckPermissaoAcesso" Text="Permiss&atilde;o de Acesso" runat="server" /></td>
                  <td><asp:CheckBox ID="ckAgendar" Text="Agendar" runat="server" /></td>
                </tr>
              </table>            
			</td>
          </tr>
          <tr align="left" valign="middle">
            <td>Pre&ccedil;o R$:</td>
            <td width="115"><asp:TextBox ID="txtPrecoReal" onKeyPress="Somente_Numeros(this)" MaxLength="5" CssClass="campo_texto" Width="75px" runat="server"></asp:TextBox></td>
            <td width="70">Pre&ccedil;o U$: </td>
            <td width="80" align="left"><asp:TextBox ID="txtPrecoDolar" onKeyPress="Somente_Numeros(this)" MaxLength="5" CssClass="campo_texto" runat="server" Width="71px"></asp:TextBox></td>
            <td colspan="2">
			  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="120"><asp:CheckBox ID="ckVisibilidade" Text="Visibilidade" runat="server" /></td>
                  <td width="140"><asp:CheckBox ID="ckAprovacao" Text="Aprova&ccedil;&atilde;o" runat="server" /></td>
                  <td>&nbsp;</td>
                </tr>
              </table>            
			</td>
          </tr>
          <tr align="left" valign="middle">
            <td>Tipo de Licen&ccedil;a: </td>
            <td colspan="3">
			<asp:RadioButtonList ID="rdbTipoLicenca" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="USR" Text="Por Usu&#225;rio"></asp:ListItem>
        <asp:ListItem Value="INS" Text="Por Instala&#231;&#227;o"></asp:ListItem>
      </asp:RadioButtonList>
			</td>
            <td width="128">&nbsp;Localiza&ccedil;&atilde;o Interna: </td>
            <td>
			<asp:TextBox ID="txtLocalizacaoInterna" MaxLength="1000" CssClass="campo_texto" runat="server" Width="250px"></asp:TextBox>
			</td>
          </tr>
          <tr align="left" valign="middle">
            <td>N&deg; de Licen&ccedil;as:</td>
            <td colspan="3">
			<asp:TextBox ID="txtNumeroLicencas" runat="server" CssClass="campo_texto" Width="260px" onKeyPress="Somente_Numeros(this)" MaxLength="5" ></asp:TextBox>
			</td>
            <td>&nbsp;Localiza&ccedil;&atilde;o Externa: </td>
            <td>
			<asp:TextBox ID="txtLocalizacaoExterna" MaxLength="1000" CssClass="campo_texto" runat="server" Width="250px"></asp:TextBox>
			</td>
          </tr>
          <tr align="left" valign="middle">
            <td>Sigla:</td>
            <td colspan="3"><asp:TextBox ID="txtSigla" MaxLength="100" CssClass="campo_texto" runat="server" Width="60px"></asp:TextBox></td>
            <td>&nbsp;Vers&atilde;o:</td>
            <td><asp:TextBox ID="txtVersao" MaxLength="255" runat="server" CssClass="campo_texto" Width="250px"></asp:TextBox></td>
          </tr>
          <tr align="left" valign="middle">
            <td colspan="6" align="center">
			<asp:Button ID="btnNovo" runat="server" Width="60px" CssClass="botao" Text="Novo" OnClick="btnNovo_Click" />
      <asp:Button ID="btnSalvar" runat="server" Width="60px" CssClass="botao" Text="Salvar" OnClick="btnSalvar_Click"/>
      <asp:Button ID="btnExcluir" runat="server" Width="60px" Visible="false" CssClass="botao" Text="Excluir" OnClick="btnExcluir_Click"/>
			</td>
          </tr>
        </table></td>
    </tr>
</table>