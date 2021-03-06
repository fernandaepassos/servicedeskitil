<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCIncidente.ascx.cs" Inherits="WUCIncidente" %>
<%@ Register Src="WUCAprovador.ascx" TagName="WUCAprovador" TagPrefix="uc10" %>
<%@ Register Src="WUCUsuario.ascx" TagName="WUCUsuario" TagPrefix="uc8" %>
<%@ Register Src="WUCEscalacaoHorizontal.ascx" TagName="WUCEscalacaoHorizontal" TagPrefix="uc9" %>
<%@ Register Src="WUCKBPesquisa.ascx" TagName="WUCKBPesquisa" TagPrefix="uc8" %>
<%@ Register Src="WUCBaseConhecimento.ascx" TagName="WUCBaseConhecimento" TagPrefix="uc7" %>
<%@ Register Src="WUCSolucaoFiltro.ascx" TagName="WUCSolucaoFiltro" TagPrefix="uc6" %>
<%@ Register Src="WUCLog.ascx" TagName="WUCLog" TagPrefix="uc4" %>
<%@ Register Src="WUCAnexo.ascx" TagName="WUCAnexo" TagPrefix="uc5" %>
<%@ Register Src="WUCPriorizacao.ascx" TagName="WUCPriorizacao" TagPrefix="uc3" %>
<%@ Register Src="WUCStatusWorkFlow.ascx" TagName="WUCStatusWorkFlow" TagPrefix="uc2" %>
<%@ Register Src="WUCItemConfiguracaoTreeView.ascx" TagName="WUCItemConfiguracaoTreeView" TagPrefix="uc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<table width="776" height="520" border="0" cellpadding="0" cellspacing="0">
    <script language="javascript">
        function verifica() {
	        if (confirm("Deseja mesmo excluir este item?")) {
		        return true;
	        }
	        else {
		        return false;
	        }
        }
    </script> 
  <tr>
    <td align="left" valign="top">
	<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" >
  <tr>
    <td align="center" valign="top">
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
        </div></td>
      </tr>
      <tr align="left" valign="middle">
        <td colspan="2"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="73">ID:</td>
            <td width="243"><asp:Label ID="lblPrefixo" runat=server BackColor=transparent></asp:Label><asp:Label ID="lblIDIncidente" runat="server" Width="100%"></asp:Label></td>
            <td width="172">Utilizar Incidente Template:</td>
            <td width="233"><asp:DropDownList ID="ddlIncidentePreDefinido" runat="server" Width="100%"> </asp:DropDownList></td>
            <td width="55" align="center" valign="middle"><asp:Button ID="btnAplicaIncidentePreDefinido" runat="server" CssClass="botao" OnClick="btnAplicaIncidentePreDefinido_Click" Text="Herdar" Width="50px" /></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td colspan="2" align="left" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="73">Descri&ccedil;&atilde;o</td>
              <td><asp:TextBox ID="txtDescricao" runat="server" CssClass="campo_texto" MaxLength="1000"
              Width="694px" Height="20px" TextMode="MultiLine"></asp:TextBox></td>
              </tr>
        </table></td>
      </tr>
      <tr>
        <td width="50%" align="left" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td><uc3:WUCPriorizacao ID="WUCPriorizacao1" runat="server" /></td>
            </tr>
            <tr>
              <td><uc9:WUCEscalacaoHorizontal ID="WUCEscalacaoHorizontal1" runat="server" /></td>
            </tr>
        </table></td>
        <td width="50%" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="100" align="left">&nbsp;Propriet&aacute;rio:</td>
                  <td align="left"><asp:TextBox ID="txtAtendente" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                </tr>
              </table></td>
            </tr>
            <tr>
              <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="100" align="left">&nbsp;Origem:</td>
                  <td align="left"><asp:DropDownList ID="ddlOrigemIncidente" runat="server" CssClass="campo_texto" Width="284px"> </asp:DropDownList></td>
                </tr>
              </table></td>
            </tr>
            <tr>
              <td><uc2:WUCStatusWorkFlow ID="wucIncidenteStatus" runat="server"/></td>
            </tr>
            <tr>
              <td valign="top"></td>
            </tr>
            <tr>
              <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr align="left">
                    <td width="96">&nbsp;Escalado:</td>
                    <td><asp:CheckBox ID="ckEscalado" runat="server" /></td>
                    <td width="50">Trat.Vip:</td>
                    <td><asp:CheckBox ID="ckVip" runat="server" /></td>
                    <td width="50">Template:</td>
                    <td><asp:CheckBox ID="ckModelo" runat="server" /></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td height="38" align="center" valign="middle">
                <asp:Button ID="btnSalvar" runat="server" CssClass="botao" OnClick="btnSalvar_Click"
              Text="  Salvar  " />&nbsp;&nbsp;
                <input class="botao" name="btnFechar" type="button" value="  Fechar  " title="  Fechar  " OnClick="javascript:window.close();" />				                        
                &nbsp;&nbsp;
            <asp:Button ID="btnEnviaParaBaseConhecimento" runat="server" Text="Envia KB" OnClick="btnEnviaParaBaseConhecimento_Click" ToolTip="Envia o registro do RequisicaoServico atual para base de conhecimento" CssClass="botao" />              </td>
            </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
  <!-- abas -->
  <tr>
    <td valign="top" align="left">
      <div>
        <table border="0" cellpadding="0" cellspacing="1" runat=server id="tblAbas">
          <tr>
            <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="aba_esq" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="aba" runat="server" class="aba_centro_off">
                    <asp:LinkButton ID="lkbSolicitante" runat="server" CommandArgument="0" OnClick="mudaAba">Dados do Solicitante</asp:LinkButton></td>
                  <td id="aba_dir" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>
            </td>
            <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="aba_esq1" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="aba1" runat="server" class="aba_centro_off">
                    <asp:LinkButton ID="lkbChamados" runat="server" CommandArgument="1" OnClick="mudaAba">Chamado</asp:LinkButton></td>
                  <td id="aba_dir1" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>
            </td>
            <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="aba_esq2" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="aba2" runat="server" class="aba_centro_off">
                    <asp:LinkButton ID="lkbItensConfirguracao" runat="server" CommandArgument="2" OnClick="mudaAba">ICs</asp:LinkButton></td>
                  <td id="aba_dir2" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>

            </td>
            <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="Td10" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="Td11" runat="server" class="aba_centro_off" >
                    <asp:LinkButton ID="lkbSolucao" runat="server" CommandArgument="3" OnClick="mudaAba">Solu??o</asp:LinkButton></td>
                  <td id="Td12" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>
            </td>
            <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="Td2" runat="server" class="aba_centro_off">
                    <asp:LinkButton ID="lbkAnexos" runat="server" CommandArgument="5" OnClick="mudaAba">Anexo</asp:LinkButton></td>
                  <td id="Td3" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>
            </td>
            <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="Td4" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="Td5" runat="server" class="aba_centro_off">
                    <asp:LinkButton ID="lbkLog" runat="server" CommandArgument="6" OnClick="mudaAba">Log</asp:LinkButton></td>
                  <td id="Td6" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>
            </td>
            <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="Td7" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="Td8" runat="server" class="aba_centro_off" style="width: 32px">
                    <asp:LinkButton ID="lkbHistorico" runat="server" CommandArgument="7" OnClick="mudaAba">Nota</asp:LinkButton></td>
                  <td id="Td9" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>
            </td>
             <td>
              <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="Td13" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="Td14" runat="server" class="aba_centro_off" style="width: 32px">
                    <asp:LinkButton ID="lkbAprovador" runat="server" CommandArgument="8" OnClick="mudaAba">Aprovador</asp:LinkButton></td>
                  <td id="Td15" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
              </table>
            </td>           
          </tr>
        </table>
      </div>
    </td>
  </tr>
  <!-- fim abas -->
  <tr>
    <td align="left" valign="top" class="tabela_abas">
      <asp:MultiView ID="mvwAbas" runat="server">
        <asp:View ID="vwSolicitante" runat="server">
        <uc8:WUCUsuario ID="WUCUsuario1" runat="server" />          
      </asp:View>
        <asp:View ID="vwChamados" runat="server">
            <asp:Panel ID="pnlChamados" runat="server" GroupingText="Chamados Vinculados:" HorizontalAlign="Left" Width="100%" CssClass="dataGrid">
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnVinculaChamado" runat="server" CssClass="botao" OnClick="btnVincularChamado_Click" Text="Pesquisar Chamado" />        
                        </td>
                    </tr>
              </table>
                
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Panel ID="pnlFiltros" runat="server" GroupingText="Op??es de Filtro" Height="50px" Visible="False" Width="100%" CssClass="dataGrid">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td width="73" align="left">C?digo:</td>
                                            <td align="left" valign="middle"><asp:TextBox ID="txtCodigoChamadoFiltro" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                            <td width="95" align="left" valign="middle">
                                                &nbsp; Servi?o:</td>
                                            <td align="left" valign="middle"><asp:DropDownList ID="ddlServicoFiltro" runat="server" Width="280px" CssClass="campo_texto"></asp:DropDownList></td>
                                            <td width="60" rowspan="2" align="center" valign="middle">
											<asp:Button ID="btnFiltrarChamados" Width="40px" runat="server" CssClass="botao" OnClick="btnFiltrarChamados_Click" Text="Filtrar" />											</td>
                                        </tr>
                                        <tr>
                                            <td align="left">Descri??o:</td>
                                            <td align="left" valign="middle"><asp:TextBox ID="txtDescricaoFiltro" runat="server" CssClass="campo_texto" Width="280px"></asp:TextBox></td>
                                            <td align="left" valign="middle">
                                                &nbsp; Solicitante:</td>
                                            <td align="left" valign="middle">
                                                <asp:DropDownList ID="ddlSolicitanteFiltro" runat="server" CssClass="campo_texto" Width="280px"></asp:DropDownList>                                                                                          </td>
                                        </tr>
                                        <tr align="center" valign="middle">
                                          <td colspan="5">
										  </td>
                                        </tr>
                                  </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlResultadoPesquisaChamado" GroupingText="Resultados da Pesquisa:" runat="server" BorderWidth="0px" Height="50px" Visible="True" CssClass="dataGrid">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td align="left" colspan="4">
                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <div id="divHeaderResultadoPesquisaChamado" align="left">
                                                                <table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="left" class="topoGrid" width="23"></td>
                                                                        <td class="topoGrid" width="30">Chamado#</td>
                                                                        <td class="topoGrid">Descri??o</td>
                                                                    </tr>
                                                              </table>
                                                            </div>
                                                        </td>
                                                  </tr>
                                                     <tr>
                                                        <td>
                                                            <asp:Panel ID="pnlGridPesquisa" runat="server" Height="50px" HorizontalAlign="Left"
                                                                ScrollBars="Vertical" Width="100%">
                                                                <asp:GridView ID="gvResultadoFiltroChamados" runat="server" AutoGenerateColumns="False"
                                                                    CellPadding="4" GridLines="None"
                                                                    ShowHeader="False" Width="98%">
                                                                    <FooterStyle CssClass="footerGrid" />
                                                                    <RowStyle BackColor="#F4FBFA" />
                                                                    <Columns>
                                                                    <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                                        <ItemTemplate>
                                                                        <asp:Label ID="lblCodigoChamado" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "chamado_codigo") %>'
                                                                            Visible="false"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                        <asp:CheckBox ID="ck_CodigoChamado" runat="server" Visible="true" />
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Chamado#">
                                                                        <ItemTemplate>
                                                                        <%#ClsParametro.ChamadoPrefixo%><%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="50px" />
                                                                        <HeaderStyle Width="50px" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Descri??o">
                                                                        <ItemTemplate>
                                                                        <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="650px" />
                                                                        <HeaderStyle HorizontalAlign="Left" Width="650px" />
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
                                                    <tr>
                                                        <td align="right">&nbsp;
                                                            <asp:Button ID="btnGravarVinculos" runat="server" CssClass="botao" OnClick="btnGravarVinculos_Click"
                                                            Text="Vincular ao Incidente" />
                                                        </td>
                                                    </tr>
                                              </table>
                                            </td>
                                        </tr>
                                  </table>
                                </asp:Panel>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
										<asp:Panel ID="Panel1" runat="server" Height="60px" HorizontalAlign="Left" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                                            <asp:GridView ID="gvChamadosVinculados" runat="server" AutoGenerateColumns="False"
                                            CellPadding="4" GridLines="None" HorizontalAlign="Center" OnRowCommand="gvChamadosVinculados_RowCommand" Width="98%" Height="80px" OnRowDataBound="gvChamadosVinculados_RowDataBound">
                                            <FooterStyle CssClass="footerGrid" />
                                            <RowStyle BackColor="#F4FBFA" />
                                            <HeaderStyle CssClass="topoGrid" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigoChamado" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "chamado_codigo") %>'
                                                    Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" Width="50px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Chamado#">
                                                <ItemTemplate>
                                                    <%#ClsParametro.ChamadoPrefixo%><%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%>
                                                </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                                                <ItemTemplate>
                                                    <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                                                </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Width="600px" />
                                                    <ItemStyle HorizontalAlign="Left" Width="600px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                <ItemTemplate> 
                                                  <a href="Javascript:VisualizaChamado(<%# DataBinder.Eval(Container.DataItem, "chamado_codigo")%>)">
                                                  <asp:Image ID="Image1" runat=server AlternateText="Exibir Detalhes" ImageUrl="~/images/exibir.gif" />
                                                  </a> </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                </asp:TemplateField>
                                                <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                                                Text="Excluir" >
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
          <asp:Panel ID="pnlCIs" runat="server" GroupingText="Itens de Configura??o Relacionados" Height="50px" HorizontalAlign="Left" Width="100%" CssClass="dataGrid">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <uc1:WUCItemConfiguracaoTreeView ID="WUCItemConfiguracaoTreeView1" runat="server" Legenda="Itens de Configura??o:"
                    TabelaRelacionada="Incidente" />
                </td>
              </tr>
            </table>
          </asp:Panel>
        </asp:View>
          <asp:View ID="vwSolucao" runat="server">
              <asp:Panel ID="Panel3" runat="server" Height="100px" Width="100%">
              <uc6:WUCSolucaoFiltro ID="WUCSolucaoFiltro1" runat="server" />
              </asp:Panel>
          </asp:View>
        <asp:View ID="vwProblemas" runat="server">
        <asp:Panel ID="Panel2" runat="server" Height="40px" Width="100%" CssClass="dataGrid">
		<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td>
	</td>
	<td width="70"><asp:Label ID="Label3" runat="server" Text="Nome: "></asp:Label></td>
    <td><asp:TextBox ID="txtNomeProblema" runat="server" Width="150px" CssClass="campo_texto"></asp:TextBox></td>
    <td><asp:Label ID="Label1" runat="server" Text="Descri??o: "></asp:Label></td>
    <td><asp:TextBox ID="txtDescricaoProblema" runat="server" Width="200px" CssClass="campo_texto"></asp:TextBox></td>
    <td>Tipo:</td>
    <td><asp:DropDownList ID="ddlProblemaTipo" runat="server" Width="150px">
            </asp:DropDownList></td>
	<td> <asp:Button ID="btnSalvarProblema" runat="server" CssClass="botao" OnClick="btnSalvarProblema_Click"
              Text="Salvar" Width="50px" />
            <asp:Button ID="btnNovoProblema" runat="server" CssClass="botao" OnClick="btnNovoProblema_Click"
              Text="Novo" Width="40px" />
            <asp:TextBox ID="txtProblemaCodigo" runat="server" Visible="False" Width="18px"></asp:TextBox></td>		
  </tr>
</table>

            <asp:Panel ID="Panel4" runat="server" Height="60px" ScrollBars="Vertical"
              Width="765px" CssClass="dataGrid">
              <asp:GridView ID="gvProblemaIncidente" runat="server" AutoGenerateColumns="False" CellPadding="3" CssClass="dataGrid" HorizontalAlign="Left"
                OnRowCommand="gvProblemaIncidente_RowCommand" Width="755px" GridLines="None">
                <FooterStyle CssClass="footerGrid" />
				<RowStyle BackColor="#F4FBFA" />
				<HeaderStyle CssClass="topoGrid" />
                <Columns>
                  <asp:TemplateField HeaderText="Codigo" Visible="False">
                    <ItemTemplate>
                      <asp:Label ID="lblCodigoProblema" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "problema_codigo")%>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Nome">
                    <ItemTemplate>
                      <asp:Label ID="lblNome" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" Width="400px" />
                      <HeaderStyle HorizontalAlign="Left" Width="400px" />
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="Data cadastro">
                    <ItemTemplate>
                      <asp:Label ID="lblDataCadastro" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao")%>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                      <HeaderStyle HorizontalAlign="Center" />
                  </asp:TemplateField>
                  <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Image" ImageUrl="~/images/icones/editar.gif">
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                      <HeaderStyle HorizontalAlign="Center" Width="40px" />
                  </asp:ButtonField>
                  <asp:ButtonField CommandName="Detalhe" Text="Detalhe" ButtonType="Image" ImageUrl="~/images/icones/detalhe.gif">
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                      <HeaderStyle HorizontalAlign="Center" Width="20px" />
                  </asp:ButtonField>
                </Columns>
                <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
				<SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
				<EditRowStyle BackColor="#C8E4E6" />
				<AlternatingRowStyle BackColor="White" />
              </asp:GridView>
            </asp:Panel>
          </asp:Panel>
        </asp:View>
        <asp:View ID="vwAnexos" runat="server">
          <uc5:WUCAnexo ID="WUCAnexo1" runat="server" Altura="100" Largura="100%" TabelaRelacionada="Incidente" />
        </asp:View>
        <asp:View ID="vwLog" runat="server">
          <uc4:WUCLog ID="WUCLog1" runat="server" Altura="100px" Largura="100%" OrigemRelacionada="Incidente" />
        </asp:View>
        <asp:View ID="vwNota" runat="server">
          <asp:Panel ID="nlNota" runat="server" GroupingText="Notas de Atendimento:" Height="100px" Width="100%" CssClass="dataGrid">
            <table width="100%" border="0" cellpadding="0" cellspacing="2">
              <tr>
                <td width="130"><asp:Label ID="lblLegendaNota" runat="server" Text="Observa??o:"></asp:Label></td>
                <td><asp:TextBox ID="txtDescricaoNotaAtendimento" runat="server" CssClass="campo_texto"
                    Height="20px" MaxLength="1000" TextMode="MultiLine" Width="550px"></asp:TextBox></td>
                <td align="right"><asp:Button ID="btnGravaNota" Width="50px" runat="server" CssClass="botao" OnClick="btnGravaNota_Click"
                    Text="Gravar" /></td>
              </tr>
              <tr>
                <td colspan="3" align="left">
                  <asp:Panel ID="pnlGridNotas" runat="server" Height="215px" ScrollBars="Vertical"
                    Width="100%" CssClass="dataGrid">
                    <asp:GridView ID="gvNotaAtendimento" runat="server" AutoGenerateColumns="False" CellPadding="4"
                      ForeColor="#333333" GridLines="None" OnRowCommand="gvNotaAtendimento_RowCommand"
                      OnRowDataBound="gvNotaAtendimento_RowDataBound" Width="98%">
                      <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                      <RowStyle BackColor="#F4FBFA" />
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
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CodigoAtendente" Visible="False">
                          <ItemTemplate>
                            <asp:Label ID="lblCodigoAtendenteNotaAtendimento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>'
                              Visible="false"></asp:Label>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Justify" Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Atendente">
                          <ItemTemplate>
                            <asp:Label ID="lblNomeAtendenteNotaAtendimento" runat="server" Text='' Visible="true"></asp:Label>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" Width="140px" />
                          <ItemStyle HorizontalAlign="Left" Width="140px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                          <ItemTemplate>
                            <asp:TextBox CssClass="campo_descricao400" ID="lblNota" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "nota") %>' ></asp:TextBox>
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
                      <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                      <HeaderStyle CssClass="topoGrid" />
                      <EditRowStyle BackColor="#2461BF" />
                      <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                  </asp:Panel>                </td>
              </tr>
            </table>
          </asp:Panel>
    </asp:View>
          <asp:View ID="vwAprovador" runat="server">
              <uc10:WUCAprovador ID="WUCAprovador1" runat="server" />
          </asp:View>
	</asp:MultiView></td>
  </tr>
</table>
</td>
  </tr>
</table>
