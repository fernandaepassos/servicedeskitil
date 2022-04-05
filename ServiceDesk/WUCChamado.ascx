<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCChamado.ascx.cs" Inherits="WUCChamado" %>
<%@ Register Src="WUCUsuario.ascx" TagName="WUCUsuario" TagPrefix="uc8" %>
<%@ Register Src="WUCKBPesquisa.ascx" TagName="WUCKBPesquisa" TagPrefix="uc7" %>
<%@ Register Src="WUCEscalacaoHorizontal.ascx" TagName="WUCEscalacaoHorizontal" TagPrefix="uc6" %>
<%@ Register Src="WUCSolucaoFiltro.ascx" TagName="WUCSolucaoFiltro" TagPrefix="uc5" %>
<%@ Register Src="WUCPriorizacao.ascx" TagName="WUCPriorizacao" TagPrefix="uc4" %>
<%@ Register Src="WUCLog.ascx" TagName="WUCLog" TagPrefix="uc3" %>
<%@ Register Src="WUCStatusWorkFlow.ascx" TagName="WUCStatusWorkFlow" TagPrefix="uc2" %>
<%@ Register Src="WUCAnexo.ascx" TagName="WUCAnexo" TagPrefix="uc2" %>
<%@ Register Src="WUCItemConfiguracaoTreeView.ascx" TagName="WUCItemConfiguracaoTreeView" TagPrefix="uc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<style>
    fieldset {
        border: 1px solid #6c6c6c;
        border-radius: 5px;
    }

    fieldset legend {
        color: darkgray;
        font-size: 11px;
        font-weight: bold;
    }
</style>

<table width="800" height="550" border="0" cellpadding="0" cellspacing="0">
    <script language="javascript">
        function verifica() {
            var result = confirm("Deseja mesmo excluir este item?");
            return result;
        }
    </script>
    <tr>
        <td align="left" valign="top">
            <table width="800" align="center" cellspacing="0" border="0">
                <tr>
                    <td align="center" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td colspan="2">
                                    <div id="divMensagem" style="width: 100%; height: 20px;" runat="server" class="Mensagem" visible="true">
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="60" align="center" valign="middle">
                                                    <asp:Image ID="imgIcone" runat="server" /></td>
                                                <td align="center" valign="middle" style="height: 20px">
                                                    <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="padding: 4px 15px 4px 1px;">
                                        <span style="margin-right: 55px">ID:</span>
                                        <asp:Label ID="lblPrefixo" runat="server" BackColor="transparent"></asp:Label>
                                        <asp:Label ID="lblIDChamado" runat="server" Width="60px" BackColor="Transparent"></asp:Label>
                                        <asp:Button ID="btnPrint" runat="server" CssClass="botao" OnClick="btnPrint_Click" Text="Print OS" Width="75px" style="float:right" />
                                          
                                        <%--Utilizar Chamado Template: 
                                        <asp:DropDownList ID="ddlChamadoPreDefinido" runat="server" CssClass="combo" Width="100%"></asp:DropDownList></td>
                                        <asp:Button ID="btnAplicaChamadoPreDefinido" runat="server" CssClass="botao" OnClick="btnAplicaChamadoPreDefinido_Click"Text="Herdar" Width="50px" /></td>--%>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="left" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="73">Descri&ccedil;&atilde;o</td>
                                            <td>
                                                <asp:TextBox ID="txtDescricao" CssClass="campo_texto" runat="server" Width="704px" MaxLength="1000" Height="100px" TextMode="MultiLine" style="max-width: 704px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="50%" align="left" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <uc4:WUCPriorizacao ID="WUCPriorizacao1" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="73">Origem:</td>
                                                        <td align="left" valign="middle" width="120">
                                                            <asp:DropDownList ID="ddlOrigemChamado" runat="server" CssClass="combo"></asp:DropDownList></td>
                                                        <td align="left" valign="middle">
                                                            <table border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td width="40">&nbsp;<asp:Label ID="lblAcaoLbl" runat="server" Text="Ação: "></asp:Label></td>
                                                                    <td>
                                                                        <asp:Label ID="lblAcao" runat="server"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc6:WUCEscalacaoHorizontal ID="WUCEscalacaoHorizontal1" runat="server"></uc6:WUCEscalacaoHorizontal>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <%--                                       <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="100" align="left">&nbsp;Propriet&aacute;rio:</td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAtendente" runat="server" Width="280px" CssClass="campo_texto"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td width="100" align="left">&nbsp;Tipo:</td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlTipoChamado" runat="server" CssClass="combo" Width="284px"></asp:DropDownList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <uc2:WUCStatusWorkFlow ID="wucIncidenteStatus" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr align="left">
                                                        <td width="97">&nbsp;Escalado:</td>
                                                        <td>
                                                            <asp:CheckBox ID="ckEscalado" runat="server" /></td>
                                                        <td width="50">Trat.Vip:</td>
                                                        <td>
                                                            <asp:CheckBox ID="ckVip" runat="server" /></td>
                                                        <td width="50">Template:</td>
                                                        <td align="left">
                                                            <asp:CheckBox ID="ckModelo" runat="server" /></td>
                                                        <td width="80" align="left">Res.1&deg; Cont: </td>
                                                        <td align="left">
                                                            <asp:CheckBox ID="chkResolvido" runat="server" AutoPostBack="True" /></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr align="left">
                                                        <td width="95">&nbsp;Agendar:</td>
                                                        <td>&nbsp;<asp:CheckBox ID="chkAgendar" runat="server" AutoPostBack="True" OnCheckedChanged="cbkAgendar_CheckedChanged" />
                                                            <asp:Label
                                                                ID="lblDataAgendamento" runat="server" Text="Data:" Visible="False"></asp:Label>
                                                          <%--  <ew:CalendarPopup ID="dpkDataAgendamento" runat="server" Width="68px" Enabled="False" Visible="False">
                                                                <SelectedDateStyle BackColor="Yellow" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></SelectedDateStyle>
                                                                <MonthHeaderStyle BackColor="Yellow" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></MonthHeaderStyle>
                                                                <WeekdayStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></WeekdayStyle>
                                                                <HolidayStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></HolidayStyle>
                                                                <GoToTodayStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></GoToTodayStyle>
                                                                <OffMonthStyle BackColor="AntiqueWhite" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Gray"></OffMonthStyle>
                                                                <WeekendStyle BackColor="LightGray" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,
                    Arial"
                                                                    ForeColor="Black"></WeekendStyle>
                                                                <ClearDateStyle BackColor="White" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></ClearDateStyle>
                                                                <DayHeaderStyle BackColor="Orange" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></DayHeaderStyle>
                                                                <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Size="XX-Small" Font-Names="Verdana,Helvetica,Tahoma,Arial" ForeColor="Black"></TodayDayStyle>
                                                                <ButtonStyle CssClass="botao" />
                                                                <TextboxLabelStyle CssClass="campo_texto" />
                                                            </ew:CalendarPopup>--%>
                                                            &nbsp;&nbsp;
      <asp:Label ID="lblHoraAgendamento" runat="server" Text="Hora:" Visible="False"></asp:Label>
                                                            <ew:TimePicker ID="tpHoraAgendamento" runat="server" Width="38px" Enabled="False" NullableLabelText="Selecione um hor&aacute;rio" PopupWidth="93px" Visible="False">
                                                                <SelectedTimeStyle BackColor="White" />
                                                                <TimeStyle BackColor="White" />
                                                                <ClearTimeStyle BackColor="White" />
                                                                <ButtonStyle CssClass="botao" />
                                                                <TextboxLabelStyle CssClass="campo_texto" />
                                                            </ew:TimePicker>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td align="center" valign="middle">
                                                            <asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                                                            <input class="botao" name="btnFechar" type="button" value="Fechar" title="Fechar" onclick="javascript: window.close();" />
                                                            <asp:Button ID="btnEnviaParaBaseConhecimento" runat="server" Text="Envia KB" OnClick="btnEnviaParaBaseConhecimento_Click" ToolTip="Envia o registro do chamado atual para base de conhecimento" CssClass="botao" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <!-- abas -->
                <tr>
                    <td align="left" valign="bottom">
                        <table cellpadding="0" cellspacing="0" border="0" runat="server" id="tblAbas">
                            <tr align="left">
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="aba_esq" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="aba" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lkbSolicitante" runat="server" OnClick="mudaAba" CommandArgument="0">Dados do Solicitante</asp:LinkButton></td>
                                            <td id="aba_dir" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="TD_AbaIncidente" runat="server">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="aba_esq1" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="aba1" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lkbIncidentes" runat="server" OnClick="mudaAba" CommandArgument="1">Incidentes</asp:LinkButton></td>
                                            <td id="aba_dir1" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>

                                            <td id="aba_esq2" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="aba2" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lkbItensConfirguracao" runat="server" OnClick="mudaAba" CommandArgument="2">ICs</asp:LinkButton></td>
                                            <td id="aba_dir2" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td runat="server" id="TD_AbaSolucao">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="Td2" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lbkMudancas" runat="server" CommandArgument="3" OnClick="mudaAba">Solu&ccedil;&atilde;o</asp:LinkButton></td>
                                            <td id="Td3" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="Td4" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="Td5" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lbkAnexos" runat="server" CommandArgument="4" OnClick="mudaAba">Anexos</asp:LinkButton></td>
                                            <td id="Td6" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="Td7" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="Td8" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lbkLigacao" runat="server" OnClick="mudaAba" CommandArgument="5">Hist&oacute;rico Liga&ccedil;&otilde;es</asp:LinkButton></td>
                                            <td id="Td9" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="Td10" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="Td11" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lbkLog" runat="server" CommandArgument="6" OnClick="mudaAba">Log</asp:LinkButton></td>
                                            <td id="Td12" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="Td13" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="Td14" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lkbHistorico" runat="server" CommandArgument="7" OnClick="mudaAba">Nota</asp:LinkButton>
                                                &nbsp;</td>
                                            <td id="Td15" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="TD_AbaRequisicaoServico" runat="server">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="Td19" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="Td20" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lkbRequisicaoServico" runat="server" CommandArgument="8" OnClick="mudaAba">Requisição de Servi&ccedil;o</asp:LinkButton>
                                                &nbsp;</td>
                                            <td id="Td21" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td id="TD_AbaRequisicaoMudanca" runat="server">
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td id="Td16" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                            <td id="Td17" runat="server" class="aba_centro_off">
                                                <asp:LinkButton ID="lbkRequisicaoMudanca" runat="server" CommandArgument="9" OnClick="mudaAba">Requisição de Mudança</asp:LinkButton>
                                                &nbsp;</td>
                                            <td id="Td18" runat="server" class="aba_direita_off">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <!-- fim abas -->
                <tr>
                    <td class="tabela_abas">
                        <asp:MultiView ID="mvwAbas" runat="server">
                            <asp:View ID="vwSolicitante" runat="server">
                                <uc8:WUCUsuario ID="WUCUsuario1" runat="server" />
                            </asp:View>
                            <asp:View ID="vwIncidentes" runat="server">
                                <asp:Panel ID="pnlIncidentes" runat="server" HorizontalAlign="Left" Width="100%" CssClass="dataGrid">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnVinculaIncidente" runat="server" CssClass="botao" OnClick="btnVinculaIncidente_Click"
                                                    Text="Pesquisar Incidente" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlFiltros" runat="server" Height="50px" Visible="False" Width="100%" CssClass="dataGrid">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td width="73" align="left">Código:</td>
                                                            <td align="left" valign="middle">
                                                                <asp:TextBox ID="txtCodigoIncidenteFiltro" runat="server" CssClass="campo_texto" Width="210px"></asp:TextBox></td>
                                                            <td width="95" align="left">&nbsp;Serviço:</td>
                                                            <td align="left" valign="middle">
                                                                <asp:DropDownList ID="ddlServicoFiltro" runat="server" Width="260px">
                                                                </asp:DropDownList></td>
                                                            <td width="140" align="center" valign="middle">
                                                                <asp:Button ID="btnFiltrarIncidentes" runat="server" CssClass="botao" OnClick="btnFiltrarIncidentes_Click"
                                                                    Text="Filtrar" Width="120px" />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="left">Descrição:</td>
                                                            <td align="left" valign="middle">
                                                                <asp:TextBox ID="txtDescricaoFiltro" runat="server" CssClass="campo_texto" Width="210px"></asp:TextBox></td>
                                                            <td align="left">&nbsp;Solicitante:</td>
                                                            <td align="left" valign="middle">
                                                                <asp:DropDownList ID="ddlSolicitanteFiltro" runat="server" Width="260px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="150" align="center" valign="middle">
                                                                <asp:Button ID="btnGravarVinculos" runat="server" CssClass="botao" Width="120px" OnClick="btnGravarVinculos_Click" Text="Vincular ao Chamado" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlResultadoPesquisaIncidente" runat="server" BorderWidth="0px" Width="100%"
                                                    Height="80px" Visible="True" CssClass="dataGrid">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <td width="100%">
                                                                            <div id="divHeaderResultadoPesquisaChamado" align="left">
                                                                                <table align="left" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="left" class="topoGrid" width="23"></td>
                                                                                        <td class="topoGrid" width="120" align="center" valign="middle">Incidente#</td>
                                                                                        <td class="topoGrid" align="left">
                                                                                            <p class="MsoNormal" style="margin: 0cm 0cm 0pt">
                                                                                                Descri&ccedil;&atilde;o de Incidentes Pesquisados
                                                                                            </p>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="pnlGridPesquisa" runat="server" Height="100px" HorizontalAlign="Left"
                                                                                ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                                                                                <asp:GridView ID="gvResultadoFiltroIncidentes" runat="server" AutoGenerateColumns="False"
                                                                                    CellPadding="4" GridLines="None" OnRowCommand="gvResultadoFiltroIncidentes_RowCommand"
                                                                                    ShowHeader="False" Width="98%" ShowFooter="False">
                                                                                    <FooterStyle CssClass="footerGrid" />
                                                                                    <RowStyle BackColor="#F4FBFA" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCodigoIncidente" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "incidente_codigo") %>'
                                                                                                    Visible="false"></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="ck_CodigoIncidente" runat="server" Visible="true" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Incidente#">
                                                                                            <ItemTemplate>
                                                                                                <%#ServiceDesk.Negocio.ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente)%><%# DataBinder.Eval(Container.DataItem, "incidente_codigo")%>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Descrição">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="610px" />
                                                                                            <HeaderStyle Width="610px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                                        </asp:TemplateField>
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
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel3" runat="server" Height="100px" HorizontalAlign="Left" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                                                                <asp:GridView ID="gvIncidentesVinculados" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" GridLines="None" HorizontalAlign="Center" OnRowCommand="gvIncidentesVinculados_RowCommand" Width="98%" OnRowDataBound="gvIncidentesVinculados_RowDataBound">
                                                                    <FooterStyle CssClass="footerGrid" />
                                                                    <RowStyle BackColor="#F4FBFA" />
                                                                    <HeaderStyle CssClass="topoGrid" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCodigoIncidente" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "incidente_codigo") %>'
                                                                                    Visible="false"></asp:Label>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Right" Width="23px" />
                                                                            <HeaderStyle Width="23px" />
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Incidente#">
                                                                            <ItemTemplate>
                                                                                <%#ServiceDesk.Negocio.ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente)%><%# DataBinder.Eval(Container.DataItem, "incidente_codigo")%>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Descri&#231;&#227;o de Incidentes Vinculados">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ItemTemplate>
                                                                                <a href="Javascript:VisualizaIncidente(<%# DataBinder.Eval(Container.DataItem, "incidente_codigo")%>)">
                                                                                    <asp:Image ID="Image1" runat="server" AlternateText="Exibir Detalhe" ImageUrl="~/images/icones/detalhe.gif" /></a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="20px" />
                                                                            <HeaderStyle Width="20px" />
                                                                        </asp:TemplateField>
                                                                        <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                                            Text="Excluir">
                                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                            <HeaderStyle HorizontalAlign="Center" Width="20px" />
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
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="vwCIs" runat="server">
                                <asp:Panel ID="pnlCIs" runat="server" GroupingText="Itens de Configuração Relacionados" Height="50px" HorizontalAlign="Left"
                                    Width="100%" CssClass="dataGrid">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="2">
                                        <tr>
                                            <td align="center" valign="top">
                                                <uc1:WUCItemConfiguracaoTreeView ID="WUCItemConfiguracaoTreeView1" runat="server" Legenda="Itens de Configuração:"
                                                    TabelaRelacionada="Chamado" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="center" valign="top">
                                        <asp:View ID="vwSolucao" runat="server">
                                            <uc5:WUCSolucaoFiltro ID="WUCSolucaoFiltro1" runat="server" />
                                        </asp:View>
                                    </td>
                                </tr>
                            </table>
                            <asp:View ID="vwAnexos" runat="server">
                                <uc2:WUCAnexo ID="WUCAnexo1" runat="server" Altura="100px" Largura="100%" TabelaRelacionada="Chamado" />
                            </asp:View>
                            <asp:View ID="vwHistoricoLigacoes" runat="server">
                                <asp:Panel ID="pnlLigacao" runat="server"
                                    GroupingText="Histórico de Contatos com o Usuário:" Width="100%" CssClass="dataGrid">
                                    <table width="100%" cellpadding="0" cellspacing="2">
                                        <tr>
                                            <td width="130">
                                                <asp:Label ID="lblCampoDescricaoLigacao" runat="server" Text="Observação:"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDescricaoLigacao" runat="server" CssClass="campo_texto" Height="20px" MaxLength="1000"
                                                    TextMode="MultiLine" Width="550px"></asp:TextBox></td>
                                            <td align="left" width="8%">
                                                <asp:Button ID="btnGravaHistoricoLigacao" runat="server" CssClass="botao" Text="Gravar" OnClick="btnGravaHistoricoLigacao_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" valign="top">
                                                <asp:Panel ID="pnlGridHistorico" runat="server" ScrollBars="Vertical" Height="190px" Width="100%" CssClass="dataGrid">
                                                    <asp:GridView ID="gvHistoricoLigacoes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        ForeColor="#333333" GridLines="None" Width="98%" OnRowCommand="gvHistoricoLigacoes_RowCommand" OnRowDataBound="gvHistoricoLigacoes_RowDataBound">
                                                        <FooterStyle CssClass="footerGrid" />
                                                        <RowStyle BackColor="#F4FBFA" />
                                                        <HeaderStyle CssClass="topoGrid" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="C&#243;digo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoLigacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ligacao_codigo") %>'
                                                                        Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDataHistorico" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao")%> ' Visible="true" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CodigoAtendente" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoAtendente" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo_proprietario") %>' Visible="false" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Justify" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Atendente">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNomeAtendente" Text='' Visible="true" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                                                <ItemTemplate>
                                                                    <asp:TextBox CssClass="campo_descricao400" ID="txtDescricaoHistorico" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="400px" />
                                                                <HeaderStyle HorizontalAlign="Left" Width="400px" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                                Text="Excluir">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
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
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="vwLog" runat="server">
                                <uc3:WUCLog ID="WUCLog1" runat="server" Altura="100px" Largura="100%" OrigemRelacionada="Chamado" ExibirLogStatus="true" />
                            </asp:View>
                            <asp:View ID="vwNota" runat="server">
                                <asp:Panel ID="nlNota" runat="server"
                                    GroupingText="Notas de Atendimento:" Height="50px" Width="100%" CssClass="dataGrid">
                                    <table width="100%">
                                        <tr>
                                            <td width="130">
                                                <asp:Label ID="lblLegendaNota" runat="server" Text="Observação:"></asp:Label></td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDescricaoNotaAtendimento" runat="server" CssClass="campo_texto" Height="20px" MaxLength="1000"
                                                    TextMode="MultiLine" Width="550px"></asp:TextBox></td>
                                            <td align="left" width="8%">
                                                <asp:Button ID="btnGravaNota" runat="server" CssClass="botao" Text="Gravar" OnClick="btnGravaNota_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top" colspan="3">
                                                <asp:Panel ID="pnlGridNota" runat="server" ScrollBars="Vertical" Width="100%" EnableViewState="true"
                                                    CssClass="dataGrid" style="max-height: 185px;">
                                                    <asp:GridView ID="gvNotaAtendimento" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        ForeColor="#333333" GridLines="None" Width="98%" OnRowDataBound="gvNotaAtendimento_RowDataBound" OnRowCommand="gvNotaAtendimento_RowCommand">
                                                        <FooterStyle CssClass="footerGrid" />
                                                        <RowStyle BackColor="#F4FBFA" />
                                                        <HeaderStyle CssClass="topoGrid" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="C&#243;digo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoNotaAtendimento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nota_codigo") %>'
                                                                        Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Data">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDataNota" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao")%>'
                                                                        Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CodigoAtendente" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoAtendenteNotaAtendimento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Justify" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Atendente">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNomeAtendenteNotaAtendimento" runat="server" Text='' Visible="true"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Width="180px" />
                                                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                                                <ItemTemplate>
                                                                    <asp:TextBox CssClass="campo_descricao400" ID="lblNota" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "nota") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="400px" />
                                                                <HeaderStyle HorizontalAlign="Left" Width="400px" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                                Text="Excluir">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
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
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="vwRequisicao" runat="server">
                                <asp:Panel ID="pnlServico" runat="server" GroupingText="Requisições de Serviço" Height="50px" HorizontalAlign="Left" Width="100%" CssClass="dataGrid">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel4" runat="server" Height="80px" HorizontalAlign="Left" ScrollBars="Vertical"
                                                    Width="100%" CssClass="dataGrid">
                                                    <asp:GridView ID="gvRequisicaoServico" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        ForeColor="#333333" GridLines="None" Width="98%" OnRowCommand="gvRequisicaoServico_RowCommand">
                                                        <FooterStyle CssClass="footerGrid" />
                                                        <RowStyle BackColor="#F4FBFA" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoServico" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "requisicaoservico_codigo") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" Width="23px" />
                                                                <HeaderStyle Width="23px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Requisi&#231;&#227;o#">
                                                                <ItemTemplate>
                                                                    <%#ServiceDesk.Negocio.ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico)%><%# DataBinder.Eval(Container.DataItem, "requisicaoservico_codigo") %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                                                <ItemTemplate>
                                                                    <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="600px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <a href="Javascript:VisualizaRequisicao(<%# DataBinder.Eval(Container.DataItem, "requisicaoservico_codigo")%>)">
                                                                        <asp:Image ID="Image1" runat="server" AlternateText="Exibir Detalhes" ImageUrl="~/images/exibir.gif" />
                                                                    </a>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                                Text="Excluir">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle CssClass="topoGrid" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="vwMudanca" runat="server">
                                <asp:Panel ID="pnlMudanca" runat="server" GroupingText="Requisições de Mudança" Height="50px" HorizontalAlign="Left" Width="100%" CssClass="dataGrid">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel5" runat="server" Height="80px" HorizontalAlign="Left" ScrollBars="Vertical"
                                                    Width="100%" CssClass="dataGrid">
                                                    <asp:GridView ID="gvRequisicaoMudanca" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                        ForeColor="#333333" GridLines="None" Width="98%" OnRowCommand="gvRequisicaoMudanca_RowCommand">
                                                        <FooterStyle CssClass="footerGrid" />
                                                        <RowStyle BackColor="#F4FBFA" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCodigoMudanca" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "requisicaomudanca_codigo") %>'
                                                                        Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" Width="23px" />
                                                                <HeaderStyle Width="23px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Requisi&#231;&#227;o#">
                                                                <ItemTemplate>
                                                                    <%#ServiceDesk.Negocio.ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoRequisicaoMudanca)%><%# DataBinder.Eval(Container.DataItem, "requisicaomudanca_codigo") %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                                                <ItemTemplate>
                                                                    <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" Width="600px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <a href="Javascript:VisualizaRequisicaoMudanca(<%# DataBinder.Eval(Container.DataItem, "requisicaomudanca_codigo")%>)">
                                                                        <asp:Image ID="Image1" runat="server" AlternateText="Exibir Detalhes" ImageUrl="~/images/exibir.gif" />
                                                                    </a>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:TemplateField>
                                                            <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                                Text="Excluir">
                                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                            </asp:ButtonField>
                                                        </Columns>
                                                        <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle CssClass="topoGrid" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
