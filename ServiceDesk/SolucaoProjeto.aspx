<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SolucaoProjeto.aspx.cs" Inherits="SolucaoProjeto" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Solução Projeto</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
<script language="javascript" type="text/javascript">
<!--

function TABLE1_onclick() {

}

// -->
</script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0">
    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center" valign="middle">
	<div id="divMensagem" style="width: 100%;" runat="server" class="Mensagem" visible="false">
					<table width="776" border="0" cellspacing="5" cellpadding="0">
  <tr>
    <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
    <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
  </tr>
</table>    
</div>
	</td>
  </tr>
</table>

	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center" valign="middle"><table width="776" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="35" align="left" valign="bottom">
			                            <table cellpadding="0" cellspacing="1" border="0">
				                            <tr>
					                            <td>
						                            <table cellpadding="0" cellspacing="0" border="0">
							                            <tr>
								                            <td id="aba_esq" runat="server" class="aba_esquerda_off">&nbsp;</td>
								                            <td id="aba" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbSolucao" runat="server" OnClick="lkbSolucao_Click">Solução</asp:LinkButton></td>
								                            <td id="aba_dir" runat="server" class="aba_direita_off">&nbsp;</td>
							                            </tr>		
						                            </table>
					                            </td>
					                            <td>
						                            <table cellpadding="0" cellspacing="0" border="0">
							                            <tr>
								                            <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp;</td>
								                            <td id="Td2" runat="server" class="aba_centro_off"><asp:LinkButton ID="lkbAtividade" runat="server" OnClick="lkbAtividade_Click">Atividade</asp:LinkButton></td>
								                            <td id="Td3" runat="server" class="aba_direita_off">&nbsp;</td>
							                            </tr>		
						                            </table>
					                            </td>
					                        </tr>  
					                    </table>                </td>
              </tr>
            </table>
                </td>
        </tr>

        <tr>
            <td align="center" valign="top">
               
            
            <table width="776" border="0" cellspacing="0" cellpadding="0" class="tabela_padrao">
            <tr>
                <td> <asp:MultiView ID="mtwAbas" runat="server"> <asp:View ID="vwSolucao" runat="server">
                <table id="tbPesquisaSolucao" width="776">
                    <tr>
                    <td colspan="3" align="left">
                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="12px" Text="Filtra solução"
                                        Width="129px" Font-Names="Trebuchet MS"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                    <td align="left" width="90">
                        <asp:Label ID="Label8" runat="server" Text="Processo: "></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="dlProcesso" runat="server" Width="364px" CssClass="campo_texto"> </asp:DropDownList></td>
                    <td> &nbsp; Entre <ew:CalendarPopup ID="dpkDataIncioPesquisa" runat="server" Width="100px" Nullable="True" NullableLabelText="Selecione uma data" Text="..."> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Black" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> 
                        <TextboxLabelStyle CssClass="campo_texto" />
                    </ew:CalendarPopup> &nbsp;e &nbsp;<ew:CalendarPopup ID="dpkDataFimPesquisa" runat="server" Width="100px" Nullable="True" NullableLabelText="Selecione uma data" Text="..."> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Black" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> 
                                                <TextboxLabelStyle CssClass="campo_texto" />
                                            </ew:CalendarPopup> </td>
                    </tr>
                    <tr>
                    <td align="left">
                        <asp:Label ID="Label9" runat="server" Text="Descrição:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtDescricaoPesquisa" runat="server" Width="360px" CssClass="campo_texto"></asp:TextBox>
                    </td>
                    <td> &nbsp;
                        <asp:Button ID="btnFiltrar" CssClass="botao" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />      
        &nbsp;
                    <asp:Button ID="btnNovaPesquisaSolucao" runat="server" CssClass="botao" OnClick="btnNovaPesquisaSolucao_Click"
                                        Text="Novo filtro" /></td>
                    </tr>
                </table>
                <table width="776" border="0" cellpadding="0" cellspacing="3">
                    <tr>
                    <td> </td>
                    </tr>
                    <tr>
                    <td> 
                    <asp:Panel ID="pnlGridPesquisaSolucao"  runat="server" ScrollBars="Vertical" Width="760px" CssClass="dataGrid">
                    <asp:GridView ID="gdPesquisaSolucao" runat="server" Width="100%" AutoGenerateColumns="False" BorderColor="#CCCCCC" GridLines="None"
                                        CellPadding="3" CssClass="dataGrid" HorizontalAlign="Center" ShowFooter="True" OnRowCommand="gdPesquisaSolucao_RowCommand">
                        <FooterStyle CssClass="footerGrid" />
                        <RowStyle BackColor="#F4FBFA" />   
                        <HeaderStyle CssClass="topoGrid" />    
                        <Columns>
                        <asp:TemplateField HeaderText="Codigo" Visible="False">
                        <ItemTemplate>
                        <asp:Label ID="lblCodigoSolucaoProjeto" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "solucao_projeto_codigo")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="tabela" Visible="False">
                        <ItemTemplate>
                        <asp:Label ID="lblTabela" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tabela")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="tabela_identificador" Visible="False">
                        <ItemTemplate>
                        <asp:Label ID="lblTabelaIdentificador" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "tabela_identificador")%>'></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField> <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                        <ItemTemplate> 
                        <asp:TextBox CssClass="campo_descricao400" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="410px" />    
                        <ItemStyle HorizontalAlign="Left" Width="410px" />    
                    </asp:TemplateField> <asp:TemplateField HeaderText="Tipo de solu&#231;&#227;o">
                    <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "tipo_solucao")%> </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField> <asp:ButtonField Text="Exibir" ButtonType="Image" ImageUrl="~/images/icones/editar.gif" CommandName="Exibir">
                    <HeaderStyle HorizontalAlign="Center" Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:ButtonField>
                        </Columns>
                        <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />    
                </asp:GridView>
                </asp:Panel>
                 </td>
                    </tr>
                </table>
                    <asp:MultiView ID="mtwDadosProcessos" runat="server">
                        <asp:View ID="vwDadosProcessoProblema" runat="server">
                            
                <table width="776" border="0" cellpadding="0" cellspacing="3" id="tbDadosProblema">
				<tr>
                    <td colspan="4" align="left" valign="middle">
					<asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="12px" Text="Informações sobre o problema"
                                 Font-Names="Trebuchet MS"></asp:Label>                    </td>
                  </tr>
                    <tr>
                    <td width="90" align="left">
                        <asp:Label ID="Label1" runat="server" Text="Nome:"></asp:Label></td>
                    <td align="left">
                      <asp:TextBox ID="txtNomeProblema" runat="server" Width="280px" Enabled="False" CssClass="campo_texto"></asp:TextBox></td>
                    <td width="90" align="left" >
                      <asp:Label ID="Label4" runat="server" Text="Equipe alocada: "></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtEquipeAlocadaProblema" runat="server" Width="280px" Enabled="False" CssClass="campo_texto"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td  align="left">
                        <asp:Label ID="Label2" runat="server" Text="Descrição:"></asp:Label></td>
                    <td align="left" >
                        <asp:TextBox ID="txtDescricaoProblema" runat="server" Width="280px" TextMode="MultiLine" Height="23px" Enabled="False" CssClass="campo_texto"></asp:TextBox></td>
                    <td  align="left" >
                        <asp:Label ID="Label5" runat="server" Text="Pessoa alocada :"></asp:Label></td>
                    <td align="left" >
                        <asp:TextBox ID="txtPessoaAlocadaProblema" runat="server" Width="280px" Enabled="False" CssClass="campo_texto"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td align="left" >
                        <asp:Label ID="Label3" runat="server" Text="Status: "></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStatusProblema" runat="server" Width="280px" Enabled="False" CssClass="campo_texto"></asp:TextBox>
                    </td>
                    <td  align="right">&nbsp; </td>
                    <td >&nbsp; </td>
                    </tr>
                </table>
                        </asp:View>
                        <asp:View ID="vwDadosProcessoChamado" runat="server">
                            <table width="776" >
							<tr align="left">
                                    <td colspan="6">
									<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="30%"><asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" Text="Informações sobre o chamado"
                                Width="129px" Font-Names="Trebuchet MS">
									  
									</asp:Label></td>
    <td width="5%">ID #:</td>
    <td width="65%"><asp:Label ID="lblChamadoCodigo" runat="server" Enabled="False" ForeColor="Black"
                                            Width="234px"></asp:Label></td>
  </tr>
</table>

									</td>
                              </tr>
							<tr align="left">
							  <td colspan="3" width="90">Solicitante:</td>
							  <td><asp:TextBox ID="txtChamadoSolicitante" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							  <td width="90">Propriet&aacute;rio:</td>
							  <td><asp:TextBox ID="txtChamadoProprietario" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							</tr>
							<tr align="left">
							  <td colspan="3">Status:</td>

							  <td width="287"><asp:TextBox ID="txtChamadoStatus" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							  <td width="60">Impacto:</td>
							  <td width="310"><asp:TextBox ID="txtChamadoImpacto" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							</tr>
							<tr align="left">
							  <td colspan="3">Equipe:</td>
							  <td><asp:TextBox ID="txtChamadoEquipe" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							  <td>Urg&ecirc;ncia:</td>
							  <td><asp:TextBox ID="txtChamadoUrgencia" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							</tr>
							<tr align="left">
							  <td colspan="3">T&eacute;cnico:</td>
							  <td><asp:TextBox ID="txtChamadoPessoaAlicada" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							  <td>Prioridade:</td>
							  <td><asp:TextBox ID="txtChamadoPrioridade" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							</tr>
							<tr align="left">
							  <td colspan="3">Origem:</td>
							  <td><asp:TextBox ID="txtChamadoOrigem" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="280px"></asp:TextBox></td>
							  <td colspan="2"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="90">Escalado:</td>
                                  <td><asp:TextBox ID="txtChamadoEscalado" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="50px"></asp:TextBox></td>
                                  <td>Vip:</td>
                                  <td><asp:TextBox ID="txtChamadoVip" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="50px"></asp:TextBox></td>
                                  <td>Tipo:</td>
                                  <td><asp:TextBox ID="txtChamadoTipo" runat="server" CssClass="campo_texto" Enabled="False"
                                            ForeColor="Black" Width="50px"></asp:TextBox></td>
                                </tr>
                              </table></td>
							  </tr>
                        </table>
                        </asp:View>
                        <asp:View ID="vwDadosProcessoIncidente" runat="server">
                            <table width="776" border="0" cellpadding="0" cellspacing="2">
							<tr align="left" valign="middle">
                                    <td colspan="8" align="left">
									<asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="12px" Text="Informações sobre o incidente"
                                Width="129px" Font-Names="Trebuchet MS"></asp:Label></td>
                                </tr>
                                <tr align="left">
                                    <td align="left" width="90">
                                        Descrição:</td>
                                    <td colspan="7">
                                        <asp:TextBox ID="txtIncidenteDescricao" runat="server" CssClass="campo_texto" MaxLength="1000"
                                            Width="680px"></asp:TextBox>                                  </td>
                                </tr>
                                <tr align="left">
                                    <td align="left">
                                        ID#:
                                    </td>
                                    <td width="290">
                                        <asp:Label ID="lblIncidenteCodigo" runat="server" Enabled="False" ForeColor="Black"
                                            Width="234px"></asp:Label></td>
                                    <td align="left" width="90">
                                        Status:
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtIncidenteStatus" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                </tr>
                                <tr align="left">
                                    <td align="left" >
                                        Solicitante:</td>
                                    <td width="290">
                                        <asp:TextBox ID="txtIncidenteSolicitante" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                    <td align="left" width="90">
                                        Impacto:</td>
                                    <td colspan="5" >
                                        <asp:TextBox ID="txtIncidenteImpacto" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                </tr>
                                <tr align="left">
                                    <td align="left">
                                        Equipe:</td>
                                    <td>
                                        <asp:TextBox ID="txtIncidenteEquipe" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                    <td align="left">
                                        Urgência:</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtIncidenteUrgencia" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                </tr>
                                <tr align="left">
                                    <td align="left">
                                        Técnico:</td>
                                    <td>
                                        <asp:TextBox ID="txtIncidenteTecnico" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                    <td align="left">
                                        Prioridade:</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtIncidentePrioridade" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                </tr>
                                <tr align="left">
                                    <td align="left">
                                        Proprietário:</td>
                                    <td>
                                        <asp:TextBox ID="txtIncidenteProprietario" runat="server" CssClass="campo_texto"
                                            Width="280px"></asp:TextBox></td>
                                    <td align="left">
                                        Origem:</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtIncidenteOrigem" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                </tr>
                        </table>
                        </asp:View>
                    </asp:MultiView>
                <table width="776" border="0" cellpadding="0" cellspacing="3" id="tdDadosSolucaoo">
                    <tr>
                    <td align="left">
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="12px" Text="Solução"
                                        Width="129px" Font-Names="Trebuchet MS"></asp:Label>
                        <asp:TextBox ID="txtCodigoSolucao" runat="server" Visible="False" Width="13px"></asp:TextBox>
                    </td>
                    <td > 
                        <asp:TextBox ID="txtTabelaIdentificador" runat="server" Visible="False" Width="12px"></asp:TextBox>
                        <asp:TextBox ID="txtTabela" runat="server" Visible="False" Width="3px"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td align="left" width="90">
                        <asp:Label ID="Label6" runat="server" Text="Descrição:"></asp:Label>
                    </td>
                    <td align="left" >
                        <asp:TextBox ID="txtDescricaoSolucao" runat="server" Width="670px" CssClass="campo_texto"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                    <td align="left">
                        <asp:Label ID="Label7" runat="server" Text="Tipo solução:"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="dlTipoSolucao" runat="server" Width="246px" CssClass="campo_texto"> </asp:DropDownList>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:CheckBox ID="ckbSeraImplementado" runat="server" Text="Será implementado?" Width="124px" /></td>
                    </tr>
                    <tr>
                    <td align="left" > Motivo de n&atilde;o<br />
                        implementar:</td>
                    <td align="left" >
                        <asp:TextBox ID="txtDescricaoNaoImplementar" runat="server" Width="670px" TextMode="MultiLine" Height="24px" CssClass="campo_texto"></asp:TextBox></td>
                    </tr>
                </table>
                <table width="776">
                    <tr>
                    <td align="center">
                        <asp:Button ID="btnSalvar" CssClass="botao" Width="69px" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />    
        &nbsp;
                    <asp:Button ID="btnExcluir" CssClass="botao" Width="69px" runat="server" Text="Excluir" OnClick="btnExcluir_Click" />
        &nbsp;
                    <asp:Button ID="btnSubmRdm" CssClass="botao" Width="117px" runat="server" Text="Submeter RDM" Enabled="False" OnClick="btnSubmRdm_Click" />
                    <asp:Button ID="btnNovo" runat="server" CssClass="botao" OnClick="btnNovo_Click"
                                        Text="Novo" /></td>
                    </tr>
                </table>
                    
                    
                    
                    

                    
                    
                    
                    </asp:View> <asp:View ID="vwAtividade" runat="server">
                <table width="776" border="0" cellpadding="0" cellspacing="3" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
                    <tr>
                    <td align="left" valign="top"> 
                        <table width="776" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 281px" valign="top">
                            <asp:TreeView ID="tvAtividadeProjeto" runat="server" Height="406px" Width="241px" OnSelectedNodeChanged="tvAtividadeProjeto_SelectedNodeChanged"  OnTreeNodeExpanded="tvAtividadeProjeto_TreeNodeExpanded" OnTreeNodePopulate="tvAtividadeProjeto_TreeNodePopulate" BackColor="Transparent" BorderColor="Transparent" ForeColor="Transparent" ImageSet="Simple" ShowLines="True" ToolTip="Para adicionar nova atividade clique sobre o ícone adição."> 
                        <ParentNodeStyle BackColor="Transparent" />
                        <SelectedNodeStyle BackColor="White" BorderColor="Transparent" />
                        <RootNodeStyle BackColor="Transparent" BorderColor="White" />
                        <NodeStyle BackColor="Transparent" BorderColor="Transparent" />
                        <LeafNodeStyle BackColor="Transparent" />
                        <HoverNodeStyle BackColor="Transparent" BorderColor="Transparent" />
                    </asp:TreeView> 
                                </td>
                                <td align="left" valign="top">
                    <table>
                            <tr>
                                <td align="right">
                        <asp:Label ID="Label13" runat="server" Text="Nome:"></asp:Label></td>
                                <td>
                        <asp:TextBox ID="txtAtividadeNome" runat="server" Width="303px" CssClass="campo_texto"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">
                        <asp:Label
                                            ID="Label14" runat="server" Text="Responsável: "></asp:Label></td>
                                <td>
                    <asp:DropDownList ID="dlAtividadeResponsavel" runat="server" Width="183px" CssClass="campo_texto"> </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td align="right">
                    <asp:Label ID="Label15" runat="server" Text="Inicio Previsto: " Width="99px"></asp:Label></td>
                                <td>
                                    <ew:CalendarPopup ID="dpkAtividadeDtInicioPrevista" runat="server" Nullable="True" Text="..." Width="103px"> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> 
                                        <TextboxLabelStyle CssClass="campo_texto" />
                                    </ew:CalendarPopup>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                    <asp:Label ID="Label16" runat="server" Text="Fim Previsto: "></asp:Label></td>
                                <td>
                                    <ew:CalendarPopup ID="dpkAtividadeDtFimPrevista" runat="server" Nullable="True" Text="..."
                                                        Width="103px"> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> 
                                        <TextboxLabelStyle CssClass="campo_texto" />
                                    </ew:CalendarPopup> 
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                    <asp:Label ID="Label19" runat="server" Text="Observações: "></asp:Label></td>
                                <td>
                    <asp:TextBox ID="txtAtividadeObservacao" runat="server" 
                                                        TextMode="MultiLine" Width="390px" CssClass="campo_texto"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="center">&nbsp;
                                    
                              </td>
                            </tr>
                        <tr>
                            <td>
                        <asp:TextBox ID="txtCodigoProjeto" runat="server" Visible="False" Width="19px"></asp:TextBox>
                        <asp:TextBox ID="txtCodigoProjetoSuperior" runat="server" Visible="False" Width="18px"></asp:TextBox>
                            </td>
                            <td align="center">

                            <asp:Button ID="btnAtividadeSalvar" runat="server" CssClass="botao" Text="Salvar" OnClick="btnAtividadeSalvar_Click" />
                                <asp:Button ID="btnAtividadeExcluir" runat="server" CssClass="botao" Text="Excluir" OnClick="btnAtividadeExcluir_Click" />
                                <asp:Button ID="btnAtividadeNova" runat="server" CssClass="botao" Text="Novo" OnClick="btnAtividadeNova_Click" /></td>
                        </tr>
                        </table>
                                </td>
                            </tr>
                      </table>
                        
                    </td>
                    </tr>
                </table>
                </asp:View> </asp:MultiView> </td>
            </tr>
          </table></td>
        </tr>
    </table>
	</form>
</body>
</html>