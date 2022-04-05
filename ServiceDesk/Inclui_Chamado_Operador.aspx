<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeFile="Inclui_Chamado_Operador.aspx.cs" Inherits="Inclui_Chamado_Operador" %>
<%@ Register Src="WUCAnexo.ascx" TagName="WUCAnexo" TagPrefix="uc1" %>
<%@ Register Src="WUCUsuario.ascx" TagName="WUCUsuario" TagPrefix="uc2" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<script language="javascript">
function atualiza() {
  var o = window.event.srcElement;
  if (o.tagName == "INPUT" && o.type == "checkbox") {
    __doPostBack("","");
  }
}

</script>
<head id="Head1" runat="server">
  <title>Help Desk  ITIL Compliance :: Inclui Chamado Operador</title>
  <link href="css/estilo.css" rel="stylesheet" type="text/css" />
  <script language="javascript" src="js/funcoes.js"></script>
</head>

<script language=javascript>
// Início da função para o contador de caracteres
<!--
function contar()
{
var tamanho = document.forms[0].txtDescricao.value.length;
var tex=document.forms[0].txtDescricao.value;
if (tamanho >= 1000) {
 	document.forms[0].txtDescricao.value=tex.substring(0,999);
}
return true;
}
					
function countChars() {
					
 		document.forms[0].txtContador.value =  1000-document.forms[0].txtDescricao.value.length;
}

if( (navigator.appName == "Netscape")
 		&& (parseInt(navigator.appVersion) >= 4) ) {
 		document.captureEvents( Event.KEYUP )
 		document.onkeyup = countChars;
}
			
if( (navigator.appName == "Microsoft Internet Explorer")
 		&& (parseInt(navigator.appVersion) >= 4) ) {
 		document.onkeyup = countChars;
}
//-->
// Fim da função para contar caracteres.
</script>



<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center" valign="top"><img name="" src="" width="1" height="6" alt="" /></td>
  </tr>
  <tr>
    <td align="center" valign="top">
	<div id="divMensagem" style="width: 100%;" runat="server" class="Mensagem" visible="true">
					<table width="776" height="19" border="0" cellpadding="0" cellspacing="0">
  						<tr>
   						  <td width="60" align="center" valign="middle"><asp:Image ID="imgIcone" runat="server"  /></td>
    						<td align="center" valign="middle"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
  						</tr>
		  </table>    
</div>
	</td>
  </tr>
  <tr>
    <td align="center" valign="top"><table width="97%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaPadrao">
            <!--DWLayoutTable-->
            <tr>
              <td height="22" colspan="3">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaCabecalho">
                  <!--DWLayoutTable-->
                  <tr>
                    <td width="8" height="22" class="esq_top">&nbsp;</td>
                    <td align="left" valign="top" class="centro_top"><table width="100%" height="22"  border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td width="20" align="center" valign="middle">&nbsp;</td>
                          <td align="left" valign="middle" class="tituloFont">
                            <asp:Label ID="lblDadosChamado" runat="server" Text="Dados do Chamado"></asp:Label>
                          </td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela">
                <table width="100%" height="520px" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td align="left" valign="top">
                      <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr width="300px">
                          <td width="40%" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="3">
                              <tr>
                                <td align="left">
                                  <asp:Label ID="lblIDChamado" runat="server" Width="60px" BackColor="Transparent"></asp:Label>
                                  <asp:Label ID="lblDescricao" runat="server" Text="Descri&ccedil;&atilde;o:"></asp:Label>
                                  <br />
                                  <asp:TextBox ID="txtDescricao" runat=server Height="130px" TextMode="MultiLine" Width="370px"  onKeyPress="Javascript:contar();" MaxLength="1000" CssClass="campo_texto"></asp:TextBox>
                                </td>
                              </tr>
                              <tr>
                                <td>
                                  <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td align="left">
                                        <asp:Label ID="lblNumeroCaracteres" runat="server" Text="N&ordm; de caracteres:"></asp:Label>
                                        <asp:TextBox ID="txtContador" runat="server" Columns="5" CssClass="campo_texto" Width="60px"></asp:TextBox>
                                        <asp:Label ID="lblRestantes" runat="server" Text="&nbsp;restantes.&nbsp;"></asp:Label>
                                      </td>
                                      <td align="left">&nbsp; </td>
                                    </tr>
                                </table></td>
                              </tr>
                              <tr>
                                <td>
                                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td width="102" align="left" valign="middle"><asp:Label ID="Label1" runat="server" Text="A&ccedil;&atilde;o: "></asp:Label></td>
                                      <td align="left" valign="middle"><asp:DropDownList ID="ddlAcao" runat="server" Width="272px"> </asp:DropDownList>
                                      </td>
                                    </tr>
                                </table></td>
                              </tr>
                          </table></td>
                          <td width="40%" align="left" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="3">
                              <tr>
                                <td colspan="2" align="left">
                                  <asp:Label ID="lblServicosDesejados" runat="server" Text="Servi&ccedil;o(s) desejado(s):"></asp:Label>
&nbsp;
                              <asp:Label ID="lblServicosDesejadosQuantidade" ForeColor="red" runat="server" Text=""></asp:Label>
                                </td>
                              </tr>
                              <tr>
                                <td colspan="2" align="left">
                                  <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Both" Width="380" CssClass="dataGrid" BorderColor="#C6E7E7" BorderStyle="Solid" BorderWidth="1px"> <asp:TreeView ID="trv_servico" onclick="atualiza()" Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Black" PopulateNodesFromClient="False" ShowLines="true" ShowExpandCollapse="true" runat="server" OnTreeNodePopulate="trvTipo_TreeNodePopulate" ShowCheckBoxes="All" NodeIndent="25" OnTreeNodeCheckChanged="trv_servico_TreeNodeCheckChanged"> <ParentNodeStyle CssClass="menu" /> <SelectedNodeStyle CssClass="menu" /> <RootNodeStyle CssClass="menu" /> <LeafNodeStyle CssClass="menu" /> <HoverNodeStyle CssClass="menu" /> </asp:TreeView> </asp:Panel>
                                </td>
                              </tr>
                              <tr>
                                <td colspan="2" align="left" valign="top"><table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
                                    <tr valign="bottom">
                                      <td align="left">
                                        <asp:CheckBox ID="chkAgendar" runat="server" Text="Agendar" AutoPostBack="True" OnCheckedChanged="cbkAgendar_CheckedChanged" CssClass="checkbox_branco" />    
                                  </td>
                                      <td align="left">
                                        <asp:Label ID="lblDataAgendamento" runat="server" Text="Data:" Visible="False"></asp:Label>
                                        <%--<ew:CalendarPopup ID="dpkDataAgendamento" runat="server" Enabled="False" Visible="False"
                                                                                    Width="68px">--%> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                                                        Font-Size="XX-Small" ForeColor="Black" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                                                        Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                                                        ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                                                        ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                                                        ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                                                        Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                                                        ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                                                        ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                                                        ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                                                        Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> </ew:CalendarPopup> &nbsp;&nbsp;
                                        <asp:Label ID="lblHoraAgendamento" runat="server" Text="Hora:" Visible="False"></asp:Label>
                                        <ew:TimePicker ID="tpHoraAgendamento" runat="server" Enabled="False" NullableLabelText="Selecione um hor&aacute;rio"
                                                                                    PopupWidth="93px" Visible="False" Width="40px"> <SelectedTimeStyle BackColor="White" /> <TimeStyle BackColor="White" /> <ClearTimeStyle BackColor="White" /> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> </ew:TimePicker> </td>
                                    </tr>
                                </table></td>
                              </tr>
                          </table></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td align="center" valign="middle">
                      <asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" Width="65px" />    
&nbsp;&nbsp;
                  <input class="botao" name="btnFechar" type="button" value="  Fechar  " title="  Fechar  " onclick="javascript:window.close();" />
                    </td>
                  </tr>
                  <tr>
                    <td align="left" height="30" valign="bottom">
                      <table cellpadding="0" cellspacing="0" border="0" runat="server" id="tblAbas">
                        <tr align="left">
                          <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                              <tr>
                                <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                <td id="Td2" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbSolicitante" runat="server" OnClick="mudaAba" CommandArgument="0" >Solicitante</asp:LinkButton></td>
                                <td id="Td3" runat="server" class="aba_direita_off">&nbsp;</td>
                              </tr>
                          </table></td>
                          <td>
                            <table cellpadding="0" cellspacing="0" border="0">
                              <tr>
                                <td id="Td4" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                <td id="Td5" runat="server" class="aba_centro_off"><asp:LinkButton ID="lbkAnexos" runat="server" OnClick="mudaAba" CommandArgument="1" >Anexos</asp:LinkButton></td>
                                <td id="Td6" runat="server" class="aba_direita_off">&nbsp;</td>
                              </tr>
                          </table></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="left">
                    <td class="tabela_abas"> <asp:MultiView ID="mvwAbas" runat="server" Visible=true> <asp:View ID="vwSolicitante" runat="server"> <uc2:WUCUsuario ID="WUCUsuario1" runat="server" /> </asp:View> <asp:View ID="vwAnexos" runat="server"> <uc1:WUCAnexo ID="WUCAnexo1" runat="server" Altura="50px" TabelaRelacionada="Chamado" Largura="98%" /> </asp:View> </asp:MultiView> </td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="left" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaRodape">
                  <!--DWLayoutTable-->
                  <tr>
                    <td width="8" height="7" class="esq_down"></td>
                    <td valign="top" class="centro_down"></td>
                    <td width="8" class="dir_down"></td>
                  </tr>
              </table></td>
            </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
</table>

	</form>
</body>
</html>