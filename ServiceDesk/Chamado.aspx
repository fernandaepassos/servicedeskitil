<%@ Page Language="C#" AutoEventWireup="true" CodeFileBaseClass="BasePage" CodeFile="Chamado.aspx.cs" Inherits="Chamado" %>
<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Chamado</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
    <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center" valign="top" style="width: 80%;" colspan="2">
                        <div id="divMensagem" style="width: 79%;" runat="server" class="Mensagem" visible="true">
                            <asp:Image ID="imgIcone" runat="server" ImageAlign="Left" />
                            <asp:Label ID="lblMensagem" runat="server"></asp:Label>
                        </div>
                        <table width="776" align="center" cellspacing="0" border="0">
                            <tr>
                                <td height="30" align="left" valign="bottom">
                                    <div class="abas">
                                        <table border="0" align="left" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="aba_esquerda_off">&nbsp;
                                              </td>
                                                <td class="aba_centro_off">
                                                    <strong>Dados do Chamado</strong></td>
                                                <td class="aba_direita_off">&nbsp;
                                              </td>
                                            </tr>
                                        </table>
                              </div>                              </td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="tabela_padrao" width="100%" > 
                                        <tr>
                                            <td align="left" valign="top" style="width: 100%;">
                                                <table width="100%">
                                                  
                                                    <tr>
                                                        <td style="text-align: left" align="center">
                                                            Solicitante:<br />
                                                            <asp:DropDownList ID="ddlSolicitante" runat="server" Width="685px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left" align="center">
                                                            Descrição:<br />
                                                            <asp:TextBox ID="txtDescricao" runat="server" Height="80px" 
                                                                TextMode="MultiLine" Width="680px"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                    <td style="text-align: left">
                                                        Use no máximo<b> 1000</b> caracteres. Nº de
                                                        caracteres: <asp:TextBox ID="TextBox2" runat="server" Width="50px"></asp:TextBox> restantes.
                                                        <script language="JavaScript1.2">
 		                                                // Início da função para o contador de caracteres
 		                                                <!--
 		                                                    function contar(form)
 		                                                    {
 		                                                    var tamanho = document.forms[0].descricao.value.length;
 		                                                    var tex=document.forms[0].descricao.value;
 		                                                    if (tamanho >= 1000) {
 		                                                    document.forms[0].descricao.value=tex.substring(0,2999);
 		                                                    }
 		                                                    return true;
 		                                                    }
                                                    							
 		                                                    function countChars(form) {
                                                    							
 				                                                    document.forms[0].lentxt.value =  1000-document.forms[0].descricao.value.length;
 		                                                    }
 		                                                //-->
 		                                                </script>
                                                        <script language="JavaScript1.2">
                                                        <!--
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:CheckBox ID="chkNotificar" runat="server" />Desejo receber notificação quando
                                                        minha solicitação for alterada.<table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td abbr="lblData" style="text-align: left" align="left">
                                                        <asp:CheckBox ID="chkAgendar" runat="server" AutoPostBack="True" OnCheckedChanged="cbkAgendar_CheckedChanged" />
                                                        <asp:Label ID="Label1" runat="server" Text="Agendar Atendimento."></asp:Label>&nbsp;<asp:Label
                                                            ID="lblDataAgendamento" runat="server" Text="Data:" Visible="False"></asp:Label>
                                                           <%-- <ew:calendarpopup id="dpkDataAgendamento" runat="server" width="68px" Enabled="False" Visible="False">
                                                                <SelectedDateStyle BackColor="Yellow" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></SelectedDateStyle>
                                                                <MonthHeaderStyle BackColor="Yellow" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></MonthHeaderStyle>
                                                                <WeekdayStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></WeekdayStyle>
                                                                <HolidayStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></HolidayStyle>
                                                                <GoToTodayStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></GoToTodayStyle>
                                                                <OffMonthStyle BackColor="AntiqueWhite" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Gray"></OffMonthStyle>
                                                                <WeekendStyle BackColor="LightGray" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></WeekendStyle>
                                                                <ClearDateStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></ClearDateStyle>
                                                                <DayHeaderStyle BackColor="Orange" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></DayHeaderStyle>
                                                                <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></TodayDayStyle>
                                                             </ew:calendarpopup>--%>
                                                                &nbsp;&nbsp;<asp:Label ID="lblHoraAgendamento" runat="server" Text="Hora:" Visible="False"></asp:Label>
                                                                <ew:timepicker id="tpHoraAgendamento" runat="server" Width="38px" Enabled="False" NullableLabelText="Selecione um horário" PopupWidth="93px" Visible="False">
                                                                    <SelectedTimeStyle BackColor="White" />
                                                                    <TimeStyle BackColor="White" />
                                                                    <ClearTimeStyle BackColor="White" />
                                                                </ew:timepicker>
                                                        </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td abbr="lblData" style="text-align: left">
                                                    </td>
                                                  </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>    
                            <tr>
                            <!-- abas -->
                            <tr>
                                <td height="15" align="left" valign="bottom">
                                    <div class="abas">
			                            <table cellpadding="0" cellspacing="1" border="0">
				                            <tr>
					                            <td>
						                            <table cellpadding="0" cellspacing="0" border="0">
							                            <tr>
								                            <td id="aba_esq" runat="server" class="aba_esquerda_off">&nbsp;</td>
								                            <td id="aba" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbHistorico" runat="server" OnClick="lkbHistorico_Click">Histórico</asp:LinkButton></td>
								                            <td id="aba_dir" runat="server" class="aba_direita_off">&nbsp;</td>
							                            </tr>		
						                            </table>
					                            </td>
					                            <td>
						                            <table cellpadding="0" cellspacing="0" border="0">
							                            <tr>
								                            <td id="aba_esq1" runat="server" class="aba_esquerda_off">&nbsp;</td>
								                            <td id="aba1" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbAnexos" runat="server" OnClick="lkbAnexos_Click">Anexos</asp:LinkButton></td>
								                            <td id="aba_dir1" runat="server" class="aba_direita_off">&nbsp;</td>
							                            </tr>		
						                            </table>
					                            </td>
					                            
				                            </tr>
			                            </table>
                              </div>                              </td>
                            </tr>
                            <tr>
                              <td height="15" align="left" valign="bottom">
							  <table width="776" border="0" cellspacing="0" cellpadding="0" class="tabela_padrao">
<tr>
                            <td style="text-align: left">
                                <asp:MultiView ID="mvwAbas" runat="server">
                                    <asp:View ID="vwHistorico" runat="server">
                                        <asp:Panel ID="pnlHistorico" runat="server" 
                                             GroupingText="Histórico da Solicitação" Height="50px" Width="100%" CssClass="dataGrid">
                                                <asp:GridView ID="GridView1" HorizontalAlign="Center" AutoGenerateColumns="false" runat="server" BorderColor="#CCCCCC" 
                                                CellPadding="3" CssClass="dataGrid" ShowFooter="true" >
                                                <FooterStyle CssClass="footerGrid" />
                                                <HeaderStyle CssClass="topoGrid" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Data" />
                                                        <asp:BoundField HeaderText="Descricao" />
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Right" ForeColor="#4A3C8C" BackColor="#E7E7FF"  />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </asp:View>
                                    <asp:View ID="vwAnexos" runat="server">
                                        <asp:Panel ID="pnlAnexo" runat="server" GroupingText="Inserir Anexo"
                                            Height="50px" HorizontalAlign="Left" Width="100%" CssClass="dataGrid" >
                                                    Imagem Associada ("Print Screen" da tela com erro):<br />
                                            <asp:FileUpload ID="FileUpload1" runat="server" Width="450px" />
                                                    <asp:Button ID="Button1" CssClass="botao" runat="server" Text="Incluir" /></asp:Panel>
                                    </asp:View>
                                </asp:MultiView></td>
                        </tr>
                        <tr>
                            <td align="center" style="text-align: center; height: 3px;">
                                <asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" />
                                <asp:Button ID="btnCancelar" CssClass="botao" runat="server" Text="Button" /></td>
                        </tr>
</table>

							  </td>
                            </tr>
                            <!-- fim abas -->

                            
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>