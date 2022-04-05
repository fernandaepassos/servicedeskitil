<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="teste_webparts" %>

<%@ Register Assembly="InfoSupport.Demos.WebParts" Namespace="InfoSupport.Demos.WebParts"
    TagPrefix="cc1" %>

<!--
    Data: 06/03/2006
    Autor: Marcos Paulo Dalles Monteiro
    Descrição: Dashboard para o sistema HDItil
  -->

  <!--
    Páginas (controles) que poderão ser instanciados
    no DashBoard.  
    -->  
<%@ Register Src="AtendimentoProcessos.ascx" TagName="AtendimentoProcessos" TagPrefix="uc" %>
<%@ Register Src="WUCChamadoTipo.ascx" TagName="WUCChamadoTipo" TagPrefix="uc5" %>
<%@ Register Src="WUCEquipe.ascx" TagName="WUCEquipe" TagPrefix="uc6" %>
<%@ Register Src="WUCItemConfiguracao.ascx" TagName="WUCItemConfiguracao" TagPrefix="uc10" %>
<%@ Register Src="WUCItemConfiguracaoAtributo.ascx" TagName="WUCItemConfiguracaoAtributo" TagPrefix="uc11" %>
<%@ Register Src="WUCItemConfiguracaoTipo.ascx" TagName="WUCItemConfiguracaoTipo" TagPrefix="uc13" %>
<%@ Register Src="WUCKBPesquisa.ascx" TagName="WUCKBPesquisa" TagPrefix="uc14" %>
<%@ Register Src="WUCMeusChamados.ascx" TagName="WUCMeusChamados" TagPrefix="uc16" %>
<%@ Register Src="WUCMeusIncidentes.ascx" TagName="WUCMeusIncidentes" TagPrefix="uc17" %>
<%@ Register Src="WUCMinhasRequisicoesServico.ascx" TagName="WUCMinhasRequisicoesServico" TagPrefix="uc19" %>
<%@ Register Src="WUCNivelEquipe.ascx" TagName="WUCNivelEquipe" TagPrefix="uc20" %>
<%@ Register Src="WUCParametro.ascx" TagName="WUCParametro" TagPrefix="uc22" %>
<%@ Register Src="WUCPerfil.ascx" TagName="WUCPerfil" TagPrefix="uc23" %>
<%@ Register Src="WUCPerfilEstrutura.ascx" TagName="WUCPerfilEstrutura" TagPrefix="uc24" %>
<%@ Register Src="WUCPessoa.ascx" TagName="WUCPessoa" TagPrefix="uc25" %>
<%@ Register Src="WUCPessoaPerfilEstrutura.ascx" TagName="WUCPessoaPerfilEstrutura" TagPrefix="uc26" %>
<%@ Register Src="WUCPrioridade.ascx" TagName="WUCPrioridade" TagPrefix="uc27" %>
<%@ Register Src="WUCSegurancaDireito.ascx" TagName="WUCSegurancaDireito" TagPrefix="uc31" %>
<%@ Register Src="WUCSegurancaDireitoPapel.ascx" TagName="WUCSegurancaDireitoPapel" TagPrefix="uc32" %>
<%@ Register Src="WUCSegurancaPapel.ascx" TagName="WUCSegurancaPapel" TagPrefix="uc33" %>
<%@ Register Src="WUCSemaforo.ascx" TagName="WUCSemaforo" TagPrefix="uc34" %>
<%@ Register Src="WUCStatus.ascx" TagName="WUCStatus" TagPrefix="uc38" %>
<%@ Register Src="WUCStatusTabela.ascx" TagName="WUCStatusTabela" TagPrefix="uc39" %>
<%@ Register Src="WUCTipoUsuario.ascx" TagName="WUCTipoUsuario" TagPrefix="uc41" %>


<script runat="server">
    
    void CustomizePage(Object s, EventArgs e)
    {
        wpm1.DisplayMode = 
          WebPartManager.CatalogDisplayMode;
    }
        
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: DashBoard</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>

<script language="javascript">

//self.moveTo(0,0);
self.resizeTo(screen.availWidth,screen.availHeight);
self.focus();

</script>
<body leftmargin="0" rightmargin="0" topmargin="0" bottommargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
    <asp:WebPartManager ID="wpm1" runat="server" />
    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
	<tr>
	<td height="20" valign="bottom">
	  <table border="0" align="left"  cellpadding="0" cellspacing="0">
        <tr>
          <td class="aba_esquerda_off">&nbsp;</td>
          <td class="aba_centro_off">Dashboard</td>
          <td class="aba_direita_off">&nbsp;</td>
        </tr>
      </table></td>
	</tr>
      <tr>
        <td align="center" valign="top">
		<table width="100%" height="99%" border="0" cellpadding="0" cellspacing="0" class="tabela_padrao">
          <tr>
            <td width="30%" align="left" valign="top" height="18" class="dataFont">
			<cc1:dropdowncatalogzone id="DropDownCatalogZone1"                           
             runat="server" Height="25px">           
                  <AddVerb Description="Adiciona uma tela &#224; uma zona" Text="Adicionar" />                   
                  <HeaderCloseVerb Description="Fechar" Text="" Visible="False" />                   
                  <CloseVerb Description="Fecha cat&#225;logo" Text="Fechar" /> 
                  <VerbStyle CssClass="botao" />                   
                  <HeaderVerbStyle /> 
                  <PartTitleStyle />
                  <FooterStyle />            
                  <PartChromeStyle /> 
                  <InstructionTextStyle /> 
                  <LabelStyle /> 
                  <PartLinkStyle /> 
                  <SelectedPartLinkStyle />
                  <HeaderStyle />            
                  <EmptyZoneTextStyle /> 
                  <EditUIStyle /> 
                  <ErrorStyle /> 
                  <PartStyle />
                <ZoneTemplate>
                <asp:DeclarativeCatalogPart ID="dcp1" runat="server">                
                <WebPartsTemplate>
                <uc11:WUCItemConfiguracaoAtributo id="WUCItemConfiguracaoAtributo" title="Atributos de Itens de Configura&ccedil;&atilde;o" runat="server"  /> 
                <uc14:WUCKBPesquisa id="WUCKBPesquisa" title="Base de Conhecimento" runat="server"  /> 
                <uc:AtendimentoProcessos ID="AtendimentoProcessos" title="Cockpit" runat="server"  /> 
                <uc6:WUCEquipe ID="WUCEquipe" title="Equipe" runat="server"  /> 
                <uc10:WUCItemConfiguracao id="WUCItemConfiguracao" title="Item de Configura&ccedil;&atilde;o" runat="server"  /> 
                <uc16:WUCMeusChamados id="WUCMeusChamados" title="Meus Chamados" runat="server"  /> 
                <uc17:WUCMeusIncidentes id="WUCMeusIncidentes" title="Meus Incidentes" runat="server"  />
                <uc19:WUCMinhasRequisicoesServico id="WUCMinhasRequisicoesServico" title="Minhas Requisi&ccedil;&otilde;es de Servi&ccedil;o" runat="server"  /> 
                <uc20:WUCNivelEquipe id="WUCNivelEquipe" title="N&iacute;vel da Equipe" runat="server"  /> 
                <uc22:WUCParametro id="WUCParametro" title="Par&acirc;metros" runat="server"  /> 
                <uc23:WUCPerfil id="WUCPerfil" title="Perfil" runat="server"  /> 
                <uc24:WUCPerfilEstrutura id="WUCPerfilEstrutura" title="Perfil Estrutura" runat="server"  /> 
                <uc25:WUCPessoa id="WUCPessoa" title="Pessoa" runat="server"  /> 
                <uc26:WUCPessoaPerfilEstrutura id="WUCPessoaPerfilEstrutura" title="Pessoa Perfil Estrutura" runat="server"  /> 
                <uc27:WUCPrioridade id="WUCPrioridade" title="Prioridade" runat="server"  /> 
                <uc31:WUCSegurancaDireito id="WUCSegurancaDireito" title="Seguran&ccedil;a Direito" runat="server"  /> 
                <uc32:WUCSegurancaDireitoPapel id="WUCSegurancaDireitoPapel" title="Seguran&ccedil;a Direito Papel" runat="server"  /> 
                <uc33:WUCSegurancaPapel id="WUCSegurancaPapel" title="Seguran&ccedil;a Papel" runat="server"  /> 
                <uc34:WUCSemaforo id="WUCSemaforo" title="Sem&aacute;foro" runat="server"  /> 
                <uc38:WUCStatus id="WUCStatus" title="Status" runat="server"  /> 
                <uc39:WUCStatusTabela id="WUCStatusTabela" title="Status Tabela" runat="server"  /> 
                <uc5:WUCChamadoTipo ID="WUCChamadoTipo" title="Tipo de Chamado" runat="server"  /> 
                <uc13:WUCItemConfiguracaoTipo id="WUCItemConfiguracaoTipo" title="Tipo de Item de Configura&ccedil;&atilde;o" runat="server"  /> 
                <uc41:WUCTipoUsuario id="WUCTipoUsuario" title="Tipo Usu&aacute;rio" runat="server"  />                 
                </WebPartsTemplate>
                </asp:DeclarativeCatalogPart>
            </ZoneTemplate>            </cc1:dropdowncatalogzone>            
				<asp:Button ID="lnkbtnCustomizar" runat="server" Text="Customizar" CssClass="botao" OnClick="CustomizePage" /></td>
            <td align="left" valign="top"><table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td height="30">&nbsp;</td>
              </tr>
              <tr>
                <td align="left" valign="middle" style="height: 22px">
                    &nbsp;</td>
              </tr>
            </table>
			</td>
          </tr>
          <tr>
            <td colspan="2" align="center" valign="top">
              <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#7EC9CB">
                <tr align="center" valign="middle" bgcolor="#F0F9F9">
                  <td valign="top"> <asp:WebPartZone
             ID="Zona1" 
             runat="server" BorderColor="#F0F9F9" EmptyZoneText="Insira uma tela selecionando-a acima." Font-Names="Verdana" Padding="0" TitleBarVerbButtonType="Button"> <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" /> <CloseVerb Description="Fecha '{0}'" Text="Fechar" /> <MinimizeVerb Description="Minimiza '{0}'" Text="Minimizar" /> <MenuLabelHoverStyle ForeColor="#E2DED6" /> <EmptyZoneTextStyle Font-Size="0.8em" /> <MenuLabelStyle ForeColor="White" /> <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" ForeColor="#333333" />
                        <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />            
                  <RestoreVerb Description="Restaura '{0}'" Text="Restaurar" /> <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" /> <PartStyle Font-Size="0.8em" ForeColor="#333333" /> <TitleBarVerbStyle BackColor="#00C0C0" Font-Size="0.6em" Font-Underline="False" ForeColor="White" /> <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                    Font-Size="0.6em" /> <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" /> <DeleteVerb Enabled="False" Visible="False" /> </asp:WebPartZone></td>
                  <td valign="top"> <asp:WebPartZone
             ID="Zona2" 
             runat="server" BorderColor="#F0F9F9" EmptyZoneText="Insira uma tela selecionando-a acima." Font-Names="Verdana" Padding="0" TitleBarVerbButtonType="Button"> <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" /> <CloseVerb Description="Fecha '{0}'" Text="Fechar" /> <MinimizeVerb Description="Minimiza '{0}'" Text="Minimizar" /> <MenuLabelHoverStyle ForeColor="#E2DED6" /> <EmptyZoneTextStyle Font-Size="0.8em" /> <MenuLabelStyle ForeColor="White" /> <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" ForeColor="#333333" />
                        <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />            
                  <RestoreVerb Description="Restaura '{0}'" Text="Restaurar" /> <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" /> <PartStyle Font-Size="0.8em" ForeColor="#333333" /> <TitleBarVerbStyle BackColor="#00C0C0" Font-Size="0.6em" Font-Underline="False" ForeColor="White" /> <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                    Font-Size="0.6em" /> <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" /> <DeleteVerb Enabled="False" Visible="False" /> </asp:WebPartZone></td>
                </tr>
                <tr align="center" valign="middle" bgcolor="#F0F9F9">
                  <td valign="top"> <asp:WebPartZone
             ID="Zona3" 
             runat="server" BorderColor="#F0F9F9" EmptyZoneText="Insira uma tela selecionando-a acima." Font-Names="Verdana" Padding="0" TitleBarVerbButtonType="Button"> <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" /> <CloseVerb Description="Fecha '{0}'" Text="Fechar" /> <MinimizeVerb Description="Minimiza '{0}'" Text="Minimizar" /> <MenuLabelHoverStyle ForeColor="#E2DED6" /> <EmptyZoneTextStyle Font-Size="0.8em" /> <MenuLabelStyle ForeColor="White" /> <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" ForeColor="#333333" />
                        <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />            
                  <RestoreVerb Description="Restaura '{0}'" Text="Restaurar" /> <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" /> <PartStyle Font-Size="0.8em" ForeColor="#333333" /> <TitleBarVerbStyle BackColor="#00C0C0" Font-Size="0.6em" Font-Underline="False" ForeColor="White" /> <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                    Font-Size="0.6em" /> <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" /> <DeleteVerb Enabled="False" Visible="False" /> </asp:WebPartZone> </td>
                  <td valign="top"> <asp:WebPartZone
             ID="Zona4" 
             runat="server" BorderColor="#F0F9F9" EmptyZoneText="Insira uma tela selecionando-a acima." Font-Names="Verdana" Padding="0" TitleBarVerbButtonType="Button"> <PartChromeStyle BackColor="#F7F6F3" BorderColor="#E2DED6" Font-Names="Verdana" ForeColor="White" /> <CloseVerb Description="Fecha '{0}'" Text="Fechar" /> <MinimizeVerb Description="Minimiza '{0}'" Text="Minimizar" /> <MenuLabelHoverStyle ForeColor="#E2DED6" /> <EmptyZoneTextStyle Font-Size="0.8em" /> <MenuLabelStyle ForeColor="White" /> <MenuVerbHoverStyle BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid"
                    BorderWidth="1px" ForeColor="#333333" />
                        <HeaderStyle Font-Size="0.7em" ForeColor="#CCCCCC" HorizontalAlign="Center" />            
                  <RestoreVerb Description="Restaura '{0}'" Text="Restaurar" /> <MenuVerbStyle BorderColor="#5D7B9D" BorderStyle="Solid" BorderWidth="1px" ForeColor="White" /> <PartStyle Font-Size="0.8em" ForeColor="#333333" /> <TitleBarVerbStyle BackColor="#00C0C0" Font-Size="0.6em" Font-Underline="False" ForeColor="White" /> <MenuPopupStyle BackColor="#5D7B9D" BorderColor="#CCCCCC" BorderWidth="1px" Font-Names="Verdana"
                    Font-Size="0.6em" /> <PartTitleStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.8em" ForeColor="White" /> <DeleteVerb Enabled="False" Visible="False" /> </asp:WebPartZone></td>
                </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
    </table>
    </form>
</body>
</html>