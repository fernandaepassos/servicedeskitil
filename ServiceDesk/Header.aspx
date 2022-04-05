<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Header.aspx.cs" Inherits="Header" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bem-Vindo</title>

    <script type="text/javascript" src="js/PopUps.js"></script>

    <script type="text/JavaScript">
        function MM_preloadImages() { 
            var d = document; if (d.images) {
                if (!d.MM_p) d.MM_p = new Array();
                var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                    if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
            }
        }

        function doClick(index, numTabs, id) {
            for (var j = 1; j <= numTabs; j++) {
                if (j != id)
                    document.all("code" + j).style.display = "none";

            }
            document.all("code" + id).style.display = "";

            for (var i = 1; i <= numTabs; i++) {
                if (i != id)
                    document.all("tab" + i).className = "backtab";
            }

            document.all("tab" + id).className = "tab";
        }

        window.onload = function () {
            doClick(1, 10, 2);
        };
    </script>

    <link rel="stylesheet" href="css/estilo.css" type="text/css" />
    <style type="text/css">
        td.tab {
            text-align: center;
            background: #A8DBDC;
            border-bottom: none;
        }

        td.backtab {
            text-align: center;
            background: #ECEFF4;
            border-bottom: 1px solid #2BA7A7;
        }
    </style>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" onclick="MM_preloadImages('images/abas/aba_direita2_off.gif','images/abas/aba_esquerda2_off.gif')">
    <form id="form1" runat="server">
        <div>
            <!-- LOGOTIPO / LOGIN -->
            <div class="headerTop">
                <div style="width: 40%;min-width: 340px;">
                    <!-- Imagens à direita-->
                    <img src="images/logotipo.png" style="padding-left:30px" />
                    <img src="images/logotipo.gif" style="margin-bottom: 15px;" />
                </div>         
                <div style="display:flex;flex-direction:column">
                    <!-- Botões e mensagem boas vindas -->
                    <div class="headerLinks">
                        <div><img src="images/bordaMenuEsq.jpg" width="42" height="28" /></div>
                        <div class="fundoMenu">
                            <div>
                                <img src="images/imgPrincipal.gif"/>
                                <a href="inicio.aspx" target="showframe" class="menu">PRINCIPAL</a>
                            </div>
                            <div>
                                <img src="images/imgSobre.gif" />
                                <a href="javascript:window.parent.location = 'logout.aspx'" class="menu">LOGOUT</a>
                            </div>
                        </div>
                        <div><img src="images/bordaMenuDir.jpg" width="42" height="28" /></div>
                    </div>
                    <div class="fonte_data" style="padding-bottom: 5px;">
                        <span>Seja bem vindo (a)</span>
                        <asp:Label id="lblNomUsu" runat="server"></asp:Label>
                    </div>
                    <div class="fonte_data">
                        <asp:Label ID="lblData" Runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <!-- MENU -->
            <div class="menuItens">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td bgcolor="#2BA7A8">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tab" id="tab1" onclick="doClick(0, 10, 1)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp;
                                                
                                            </td>
                                            <td class="aba_centro2_off">Chamado</td>
                                            <td class="aba_direita2_off">&nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab2" onclick="doClick(1, 10, 2)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp;
                                                
                                            </td>
                                            <td class="aba_centro2_off">Service Desk</td>
                                            <td class="aba_direita2_off">&nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab3" onclick="doClick(2, 10, 3)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp;
                                                
                                            </td>
                                            <td class="aba_centro2_off">Incidente</td>
                                            <td class="aba_direita2_off">&nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab4" onclick="doClick(3, 10, 4)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp;
                                                
                                            </td>
                                            <td class="aba_centro2_off">Problema</td>
                                            <td class="aba_direita2_off">&nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab5" onclick="doClick(4, 10, 5)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp;
                                                
                                            </td>
                                            <td class="aba_centro2_off">Mudan&ccedil;a</td>
                                            <td class="aba_direita2_off">&nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab6" onclick="doClick(5, 10, 6)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp;
                                                
                                            </td>
                                            <td class="aba_centro2_off">Base de Conhecimento</td>
                                            <td class="aba_direita2_off">&nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab7" onclick="doClick(6, 10, 7)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp;
                                                
                                            </td>
                                            <td class="aba_centro2_off">Configura&ccedil;&atilde;o</td>
                                            <td class="aba_direita2_off">&nbsp;
                                                
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab8" onclick="doClick(7, 10, 8)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp; </td>
                                            <td class="aba_centro2_off">N&iacute;vel de Servi&ccedil;o </td>
                                            <td class="aba_direita2_off">&nbsp; </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab9" onclick="doClick(8, 10, 9)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp; </td>
                                            <td class="aba_centro2_off">Sistema</td>
                                            <td class="aba_direita2_off">&nbsp; </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="backtab" id="tab10" onclick="doClick(9, 10, 10)">
                                    <table border="0" align="left" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td class="aba_esquerda2_off">&nbsp; </td>
                                            <td class="aba_centro2_off">Seguran&ccedil;a</td>
                                            <td class="aba_direita2_off">&nbsp; </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="menuInferior">
                    <td colspan="11" align="left" valign="top" style="/*position: absolute; top: 97px; width: 100%; */">
                        <pre id="code1" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
	<tr align="center" valign="middle">
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
          <tr align="center" valign="middle">
            <td width="20">&nbsp;</td>
            <td><a href="javascript:NovaJanela('Inclui_Chamado_Operador.aspx','width=784,height=562');" class="menu">Novo Chamado</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="meus_chamados.aspx" target="showframe" class="menu">Meus Chamados</a></td>
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>

                        <pre id="code2" style="display: inline"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
	<tr align="center" valign="middle">
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
          <tr align="center" valign="middle">
            <td width="20">&nbsp;</td>
            <td><a href="Cockpit2.aspx" target="showframe" class="menu">Cockpit ITIL</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="javascript:NovaJanela('Semaforo.aspx','width=600,height=605');" class="menu">Sem&aacute;foro</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="javascript:NovaJanelaDashBoard('Indisponivel.aspx','width=600,height=605');" class="menu">DashBoard</a></td>
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>

                        <%--  <pre id="code2" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC" class="menu">
	                        <tr align="center" valign="middle">
		                        <td align="left" valign="top">
		                        <table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#a8dbdc">
          <tr align="center" valign="middle">
            <td width="20">&nbsp;</td>
            <td><a href="javascript:NoveJanela('Cockpit.aspx');" class="menu">Cockpit ITIL</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="javascript:NovaJanela('Semaforo.aspx','width=600,height=605');" class="menu">Sem&aacute;foro</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="javascript:NovaJanelaDashBoard('Indisponivel.aspx','width=600,height=605');" class="menu">DashBoard</a></td>
          </tr>
        </table></td>
	                        </tr>		
                        </table>
		            </pre>--%>
                        <pre id="code3" style="display: inline"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
	<tr align="center" valign="middle">
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
          <tr align="center" valign="middle">
            <td width="20">&nbsp;</td>
            <td><a href="javascript:NovaJanela('incidente.aspx','width=784,height=562');" class="menu">Novo Incidente</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Meus_Incidentes.aspx" target="showframe" class="menu">Meus Incidentes</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="javascript:NovaJanela('RequisicaoServico.aspx','width=784,height=562');" class="menu">Nova Requisição de Serviço</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="MinhasRequisicoesServico.aspx" target="showframe" class="menu">Minhas Requisições de Serviço</a></td>
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>
                        <pre id="code4" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#a8dbdc" class="menu">
	<tr align="left" valign="middle">
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#a8dbdc">
          <tr align="left" valign="middle">
            <td width="20">&nbsp;</td>
            <td align="center" valign="middle"><a href="Indisponivel.aspx" target="showframe" class="menu">Novo problema</a></td>
            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td align="center" valign="middle"><a href="Indisponivel.aspx" target="showframe" class="menu">Problemas</a></td>
            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td align="center" valign="middle"><a href="Indisponivel.aspx" target="showframe" class="menu">Tipo de problema</a></td>
            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td align="center" valign="middle"><a href="Indisponivel.aspx" target="showframe" class="menu">Gerenciar Erros</a></td>
            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td align="center" valign="middle"><a href="Indisponivel.aspx" target="showframe" class="menu">Erros Conhecidos</a></td>
           
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>
                        <pre id="code5" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC" class="menu">
	<tr align="center" valign="middle">
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
          <tr align="center" valign="middle">
            <td width="20">&nbsp;</td>
            <td id="Td1" align="center" valign="middle" visible="false" runat=server><a href="Indisponivel.aspx" target="showframe" class="menu">Incluir Mudan&#231;a</a></td>
            <td id="Td2" width="20" visible="false" runat=server><img src="images/defesa.gif" width="3" height="15"></td>
            <td id="Td3" align="center" valign="middle" visible="false" runat=server><a href="Indisponivel.aspx" target="showframe" class="menu">Gerenciar Mudan&#231;as</a></td>
            <td id="Td4" width="20" visible="false" runat=server><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="javascript:NovaJanela('RequisicaoMudanca.aspx','width=784,height=562');" class="menu">Nova Requisição de Mudança</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="MinhasRequisicoesMudanca.aspx" target="showframe" class="menu">Minhas Requisições de Mudança</a></td>            
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>
                        <pre id="code6" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#a8dbdc" class="menu">
	<tr align="center" valign="middle">
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#a8dbdc">
          <tr align="center" valign="middle">
            <td width="20">&nbsp;</td>
            <td><a href="BaseConhecimento.aspx" target="showframe" class="menu">Base de Conhecimento</a></td>
            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Notificacao.aspx" target="showframe" class="menu">Notificação</a></td>
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>
                        <pre id="code7" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC" class="menu">
	<tr>
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
          <tr>
            <td width="20">&nbsp;</td>
            <td align="center" valign="middle"><a href="itemconfiguracaoatributo.aspx" target="showframe" class="menu">Atributos do Item de Configura&#231;&#227;o</a></td>
            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td align="center" valign="middle"><a href="itemconfiguracaotipo.aspx" target="showframe" class="menu">Tipos de Item de Configura&#231;&#227;o</a></td>

            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td align="center" valign="middle"><a href="itemconfiguracao.aspx" target="showframe" class="menu">Itens de Configura&#231;&#227;o</a></td>
            <td width="20" align="center" valign="middle"><img src="images/defesa.gif" width="3" height="15"></td>
            <td align="center" valign="middle"><a href="auditorialista.aspx" target="showframe" class="menu">Auditorias no CMDB</a></td>
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>
                        <pre id="code8" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC" class="menu">
	<tr>
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
          <tr align="center" valign="middle">
            <td width="20">&nbsp;</td>
            <td><a href="<%=ClsParametro.URLRelatorios %>" target="showframe" class="menu">Relat&oacute;rios</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="JavaScript:AbrirIndicadores('<%=ClsParametro.URLIndicadores %>');" class="menu">Indicadores</a></td>
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>
                        <pre id="code9" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC" class="menu">
	<tr>
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
          <tr align="center" valign="middle">
            <td width="5">&nbsp;</td>
            <td><a href="Parametro.aspx" target="showframe" class="menu">Par&acirc;metros</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Nomenclatura.aspx" target="showframe" class="menu">Nomenclaturas</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="ChamadoOrigem.aspx" target="showframe" class="menu">Origem</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Equipe.aspx" target="showframe" class="menu">Equipes</a></td>
             <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="NivelEquipe.aspx" target="showframe" class="menu">Nivel</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="WorkFlow.aspx" target="showframe" class="menu">WorkFlow</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Pessoa.aspx" target="showframe" class="menu">Pessoas</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="TipoImpacto.aspx" target="showframe" class="menu">Impacto</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="TipoUrgencia.aspx" target="showframe" class="menu">Urg&#234;ncia</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Prioridade.aspx" target="showframe" class="menu">Prioridade</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Status.aspx" target="showframe" class="menu">Status</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="StatusTabela.aspx" target="showframe" class="menu">Status por Tabela</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="Minha_Estrutura_Organizacional.aspx" target="showframe" class="menu">Estrutura</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="ChamadoTipo.aspx" target="showframe" class="menu">Tipo Chamado</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="TipoAcao.aspx" target="showframe" class="menu">Ação</a></td>
            <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
            <td><a href="SolucaoProjetoTipo.aspx" target="showframe" class="menu">Tipo de solu&#231;&#227;o</a></td>
          </tr>
        </table></td>
	</tr>		
</table>
		</pre>
                        <pre id="code10" style="display: none"><table width="100%" height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC" class="menu">
	<tr>
		<td align="left" valign="top"><table height="26" border="0" cellpadding="0" cellspacing="0" bgcolor="#A8DBDC">
            <tr align="center" valign="middle">
                <td width="20">&nbsp;</td>
                <td><a href="Perfil.aspx" target="showframe" class="menu">Perfil</a></td>
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="PerfilEstrutura.aspx" target="showframe" class="menu">Perfil Estrutura</a></td>
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="DireitoPerfil.aspx" target="showframe" class="menu">Direito Perfil</a></td>
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="PessoaPerfilEstrutura.aspx" target="showframe" class="menu">Pessoa Perfil</a></td>
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="TipoUsuario.aspx" target="showframe" class="menu">Tipos de Usuário</a></td>
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="Aplicacao.aspx" target="showframe" class="menu">Nova Aplicação</a></td>
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="Minhas_Aplicacoes.aspx" target="showframe" class="menu">Filtrar Aplicação</a></td>
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="FuncaoAplicacao.aspx" target="showframe" class="menu">Função Aplicação</a></td> 
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="SegurancaPapel.aspx" target="showframe" class="menu">Papeis da Segurança</a></td> 
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="SegurancaDireitoPapel.aspx" target="showframe" class="menu">Direitos dos Papéis</a></td> 
                <td width="20"><img src="images/defesa.gif" width="3" height="15"></td>
                <td><a href="SegurancaDireito.aspx" target="showframe" class="menu">Direitos da Segurança</a></td> 
            </tr>
            </table></td>
	</tr>		
</table>
		</pre>
                    </td>
                </tr>
            </table>
            </div>
        </div>
    </form>
</body>
</html>
