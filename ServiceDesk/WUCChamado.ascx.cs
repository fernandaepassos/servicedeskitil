using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceDesk.Negocio;
using ServiceDesk.Generica;

public partial class WUCChamado : System.Web.UI.UserControl
{
    #region Declarações
    #region Declaracoes Publicas

    public string strFormatoDataInclusao = ClsParametro.DataInclusao;

    #endregion

    #region Declaracoes Privadas
    #endregion

    #endregion

    #region Métodos

    #region Método Bloqueia Campos
    /// <summary>
    /// Bloqueia Campos
    /// </summary>
    /// <param name="intCodigoChamado">Código do chamado</param>
    public void BloqueiaCampos(int intCodigoChamado)
    {
        try
        {
            //=======================================================================//
            // - O que: Verifica se o tipo do chamado é do tipo que gera processo, se 
            // sim ocorrerá o bloqueio das campos equipe, nível e técnico, porém se for
            // definido em parametro que a varialve BloqueiaEscalacaoHorizontalChamado
            // tem o valor N (Não bloquear) o sistema não fará nenhum bloqueio.
            // - Quem: Fernanda Passos.
            // - Quando: 05/03/2006 ás 23:00hs.
            //=======================================================================//
            if (ClsParametro.BloqueiaEscalacaoHorizontalChamado == "N") return;

            if (ClsChamado.VerificaSeClassificadoComoTipoGeraProcesso(intCodigoChamado) == true)
                WUCEscalacaoHorizontal1.BloqueiaCampos(true, true, true);
            else
                WUCEscalacaoHorizontal1.BloqueiaCampos(false, false, false);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Mensagem do sistema
    /// <summary>
    /// Fornece um meio de acesso ao painel de mensagem
    /// </summary>
    /// <param name="Mensagem">Mensagem a ser exibida na tela</param>
    /// <param name="Imagem">Nome da imagem do ícone do painel</param>
    /// <param name="Ativo">true para Exibir, false para Ocultar</param>
    /// <example>ExibeMensagem("teste","images/icones/aviso.gif",true)</example>
    public void ExibeMensagem(string Mensagem, string Imagem, bool Ativo)
    {
        try
        {
            lblMensagem.Text = Mensagem;
            imgIcone.ImageUrl = Imagem;

            if (Ativo == true)
            {
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else if (Ativo == false)
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
            else //nao foi especificado, assume true
            {
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Abre um Chamado existente para edição
    /// </summary>
    /// <param name="CodigoChamado">Código do Chamado desejado</param>
    public void EditaChamado(int CodigoChamado)
    {
        try
        {
            if (CodigoChamado != 0)
            {
                tblAbas.Visible = true;
                mvwAbas.Visible = true;
                lblIDChamado.Text = CodigoChamado.ToString();
                wucIncidenteStatus.montaDados(Convert.ToInt32(lblIDChamado.Text), "CHAMADO");
            }
        }
        catch
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);
        }
    }

    //Dados do chamado    
    public void PreencheDadosChamado(int CodigoChamado)
    {
        string strDataMinimaSistema = ClsParametro.DataMinimaSistema;
        ClsChamado objChamado = new ClsChamado(CodigoChamado);

        if (objChamado != null)
        {
            //preenche os dados do chamado.

            #region tela principal

            lblIDChamado.Text = objChamado.Codigo.Valor;
            txtDescricao.Text = ClsTexto.trocaHtmlPorAspa(objChamado.Descricao.Valor);

            if (objChamado.MatriculaSolicitante.Valor != string.Empty)
            {
                WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(objChamado.MatriculaSolicitante.Valor));
            }

            //proprietário
            //if (objChamado.MatriculaInclusor.Valor != string.Empty)
            //    txtAtendente.Text = ClsUsuario.getNomeUsuario(objChamado.MatriculaInclusor.Valor);

            #region Data de Agendamento
            if ((objChamado.DataAgendamento.Valor != string.Empty) && (objChamado.DataAgendamento.Valor != strDataMinimaSistema))
            {
                DateTime dtAgendamento = Convert.ToDateTime(objChamado.DataAgendamento.Valor);
                //dpkDataAgendamento.SelectedDate = dtAgendamento.Date;
                tpHoraAgendamento.SelectedTime = dtAgendamento;
                chkAgendar.Checked = true;
                //dpkDataAgendamento.Visible = true;
                tpHoraAgendamento.Visible = true;
                //dpkDataAgendamento.Enabled = true;
                tpHoraAgendamento.Enabled = true;
                //dpkDataAgendamento.Text = "...";
                tpHoraAgendamento.Text = "...";
                lblDataAgendamento.Visible = true;
                lblHoraAgendamento.Visible = true;
            }
            #endregion

            //nao permite alteracao de data de agendamento
            chkAgendar.Enabled = false;
            //dpkDataAgendamento.Enabled = false;
            tpHoraAgendamento.Enabled = false;

            if (objChamado.SolucionadoPrimeiroContato.Valor.ToUpper() == "S")
            { chkResolvido.Checked = true; }
            else
            { chkResolvido.Checked = false; }

            WUCPriorizacao1.setImpacto(objChamado.Impacto.Valor);
            WUCPriorizacao1.setUrgencia(objChamado.Urgencia.Valor);

            //Tratamento de Status - Flávio
            wucIncidenteStatus.salvaStatus();

            if (objChamado.TipoChamado.Valor != string.Empty)
            {
                try
                {
                    ddlTipoChamado.ClearSelection();
                    ddlTipoChamado.SelectedValue = objChamado.TipoChamado.Valor;
                }
                catch { }
            }

            if (objChamado.OrigemChamado.Valor != string.Empty)
            {
                ddlOrigemChamado.ClearSelection();
                try
                {
                    ddlOrigemChamado.SelectedValue = objChamado.OrigemChamado.Valor;
                }
                catch { }
            }
            if (objChamado.Escalado.Valor.ToUpper() == "S")
            {
                ckEscalado.Checked = true;
            }

            WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objChamado.NivelAtendimento.Valor, objChamado.Equipe.Valor, objChamado.Tecnico.Valor);

            if (objChamado.Vip.Valor.ToUpper() == "S")
            {
                ckVip.Checked = true;
            }

            if (objChamado.ChamadoModelo.Valor.ToUpper() == "S")
            {
                ckModelo.Checked = true;
            }

            //==================================================================//
            // - O que: Tratamento da exibição da ação selecionada na abertura
            //   do chamado.
            // - Quem: Fernanda Passos
            // - Quando: 03/03/2006 ás 15:18hs
            //==================================================================//
            if (objChamado.Acao.Valor != string.Empty)
            {
                lblAcao.Text = ClsAcao.getDescricaoAcao(objChamado.Acao.Valor);
                lblAcao.Visible = true;
                lblAcaoLbl.Visible = true;
            }
            else
            {
                lblAcao.Visible = false;
                lblAcaoLbl.Visible = false;
            }
            //==================================================================//

            #region Remove o chamado atual da lista de modelos se ele for modelo
            /*if (lblIDChamado.Text != string.Empty)
            {
                try
                {
                    ListItem itemAtual = ddlChamadoPreDefinido.Items.FindByValue(lblIDChamado.Text);
                    ddlChamadoPreDefinido.Items.Remove(itemAtual);
                    itemAtual = null;
                }
                catch { }
            }*/
            #endregion
            #endregion
        }
        objChamado = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int intChamado = 0;

        if (Request.QueryString["chamado"] != null)
        { intChamado = Convert.ToInt32(Request.QueryString["chamado"].ToString()); }

        //Esconde a mensagem de erro
        ExibeMensagem("", "", false);
        if (!Page.IsPostBack)
        {
            pnlResultadoPesquisaIncidente.Visible = false;

            if (Request.QueryString["aba"] != null)
            { mvwAbas.ActiveViewIndex = Convert.ToInt32(Request.QueryString["aba"]); }
            else
            { mvwAbas.ActiveViewIndex = 0; }

            #region Preenche combos etc

            WUCPriorizacao1.geraDropDownListImpactoUrgencia();
            ClsTipoOrigemChamado.geraDropDownList(ddlOrigemChamado);
            ClsTipoChamado.geraDropDownList(ddlTipoChamado);
            //ClsChamado.geraDropDownListChamadoPreDefinido(ddlChamadoPreDefinido);
            //Campos de filtro da aba de chamados vinculados
            //Combo Solicitantes
            ClsUsuario.geraDropDownList(ddlSolicitanteFiltro, "--");
            //Combo Serviços
            ClsItemConfiguracao.geraDropDownList(ddlServicoFiltro);
            //Combo de status
            ClsStatus.geraDropDownListStatus(wucIncidenteStatus.DdlStatusFuturo);

            #endregion

            if (lblIDChamado.Text.Trim() != string.Empty)
            {
                //nivel
                ClsChamado objChamadoNivel = new ClsChamado(Convert.ToInt32(lblIDChamado.Text.Trim()));
                WUCEscalacaoHorizontal1.geraDropDownListNivel(objChamadoNivel.NivelAtendimento.Valor);
                objChamadoNivel = null;
                //dados do chamado
                PreencheDadosChamado(Convert.ToInt32(lblIDChamado.Text.Trim()));
            }
            else
            {
                //nivel 0
                WUCEscalacaoHorizontal1.geraDropDownListNivel("0");
            }
        }

        // Libera o bloqueio do botão salvar depois de ter definido um proprietário para o chamado. Se o chamado verifica a segurança dos 
        // papéis sem ter um proprietário o sistema irá bloquear o botão salvar  deixando o chamado impossibilitado de receber um proprietário.
        // Quem: Fernanda Passos - Quando: 31/03/2006 ás 14:49hs

        /*if (!string.IsNullOrEmpty(ClsChamado.GetCodigoProprietario(intChamado)))
            btnSalvar.Enabled = ClsUsuario.verificaAcessoPapel(ClsUsuario.getCodigoUsuario(), intChamado, 2, "chamado");*/

        TD_AbaIncidente.Visible = false;
        TD_AbaRequisicaoMudanca.Visible = false;
        TD_AbaRequisicaoServico.Visible = false;

        if (ddlTipoChamado.SelectedValue != string.Empty)
        {
            try
            {
                if (ddlTipoChamado.SelectedValue == ClsParametro.CodigoTipoChamadoIncidente)
                    TD_AbaIncidente.Visible = true;
                else if (ddlTipoChamado.SelectedValue == ClsParametro.CodigoTipoChamadoRequisicaoMudanca)
                    TD_AbaRequisicaoMudanca.Visible = true;
                else if (ddlTipoChamado.SelectedValue == ClsParametro.CodigoTipoChamadoServico)
                    TD_AbaRequisicaoServico.Visible = true;
            }
            catch { }
        }

        //só exibe as abas quando o incidente tiver sido salvo ou ja existir
        if (lblIDChamado.Text.Trim() != string.Empty)
        {
            tblAbas.Visible = true;
            mvwAbas.Visible = true;
        }
        else
        {
            tblAbas.Visible = false;
            mvwAbas.Visible = false;
        }

        if (lblIDChamado.Text.Trim() != string.Empty)
        {
            lblPrefixo.Text = ClsParametro.ChamadoPrefixo;
            //Log
            WUCLog1.MontaDados(Convert.ToInt32(lblIDChamado.Text.Trim()));
        }

        //Para o tipo de chamado informado, habilita/desabilita a aba solução
        // dependendo se o tipo escolhido gera ou não um processo.
        /*if (ClsTipoChamado.geraProcesso(ddlTipoChamado.SelectedValue.ToString()))
        {
            //desabilita o link
            lbkMudancas.Visible = true;
            //habilita das tds da aba
            TD_AbaSolucao.Visible = true;
        }
        else*/
        {
            lbkMudancas.Visible = false;
            //deshabilita das tds da aba
            TD_AbaSolucao.Visible = false;
        }

        //============================================================//
        // - O que: Bloqueia campos.
        // - Quem: Fernanda Passos.
        // - Quando: 05/03/2006 ás 22:26hs.
        //============================================================//
        BloqueiaCampos(intChamado);
        //============================================================//

        //Esconde botão salvar e envia KB quando o chamado está finalizado
        btnSalvar.Enabled = !wucIncidenteStatus.StatusAtual.Equals("Finalizado");
        btnEnviaParaBaseConhecimento.Enabled = !wucIncidenteStatus.StatusAtual.Equals("Finalizado");
    }

    #endregion

    #endregion

    #region Eventos

    #region Abas
    /// <summary>
    /// Evento do clique do Botao da aba 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void mudaAba(object sender, EventArgs e)
    {
        try
        {
            int intAbaSelecionada = 0;
            int intCodigoItem = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            //Coloquei o switch pelo motivo que nao tinha certeza qual controle ficara (button/link)
            switch (sender.GetType().ToString())
            {
                case "System.Web.UI.WebControls.Button":
                    {
                        Button btnAba = (Button)sender;
                        intAbaSelecionada = Convert.ToInt32(btnAba.CommandArgument);
                        mvwAbas.ActiveViewIndex = intAbaSelecionada;
                        btnAba = null;
                        break;
                    }
                case "System.Web.UI.WebControls.LinkButton":
                    {
                        LinkButton lkbAba = (LinkButton)sender;
                        intAbaSelecionada = Convert.ToInt32(lkbAba.CommandArgument);
                        mvwAbas.ActiveViewIndex = intAbaSelecionada;
                        lkbAba = null;
                        break;
                    }
            }

            //Verificando qual aba foi escolhida
            //Esse switch pode deixar de existir, colocando o carregamento de todas as grids no momento de carregamento
            //mas o carregamento poderá ser desnecessário, uma vez nem sempre todas as abas serão carregadas
            switch (intAbaSelecionada)
            {
                case 0:
                    {
                        //if (this.lblIDChamado.Text.Trim() != string.Empty)
                        //{
                        //    WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(this.lblIDChamado.Text.Trim()));
                        //}
                        break;
                    }
                case 1:
                    {
                        if (lblIDChamado.Text.Trim() != "")
                        {
                            ClsChamado.geraGridViewIncidentes(gvIncidentesVinculados, lblIDChamado.Text.Trim());
                        }
                        break;
                    }
                case 2:
                    {
                        //CIs
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            WUCItemConfiguracaoTreeView1.CarregaIC(Convert.ToInt32(lblIDChamado.Text.Trim()));
                        }
                        break;
                    }
                case 3:
                    {
                        //Solucao
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            WUCSolucaoFiltro1.PreencheCampo("Chamado", Convert.ToInt32(lblIDChamado.Text.Trim()));
                            WUCSolucaoFiltro1.Filtrar();
                        }
                        break;
                    }
                case 4:
                    {
                        //anexos
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            WUCAnexo1.CarregaAnexos(lblIDChamado.Text.Trim(), "Chamado");
                        }
                        break;
                    }
                case 5:
                    {
                        //Historico ligacoes
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            ClsChamado.geraGridViewLigacoes(gvHistoricoLigacoes, lblIDChamado.Text.Trim());
                        }
                        break;
                    }
                case 6:
                    {
                        //Log
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            WUCLog1.MontaDados(Convert.ToInt32(lblIDChamado.Text.Trim()));
                        }
                        break;
                    }
                case 7:
                    {
                        //Notas Atendimento
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            ClsChamado.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDChamado.Text.Trim());
                        }
                        break;
                    }
                case 8:
                    {
                        //requisicao servico
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            ClsChamado.geraGridViewRequisicaoServico(gvRequisicaoServico, lblIDChamado.Text.Trim());
                        }
                        break;
                    }
                case 9:
                    {
                        //requisicao servico
                        if (lblIDChamado.Text.Trim() != string.Empty)
                        {
                            ClsChamado.geraGridViewRequisicaoMudanca(gvRequisicaoMudanca, lblIDChamado.Text.Trim());
                        }
                        break;
                    }

            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    #endregion

    #region Evento cbkAgendar_CheckedChanged
    /// <summary>
    /// Evento cbkAgendar_CheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbkAgendar_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (!chkAgendar.Checked)
            {
                //dpkDataAgendamento.Enabled = false;
                tpHoraAgendamento.Enabled = false;
                //dpkDataAgendamento.Visible = false;
                tpHoraAgendamento.Visible = false;
                lblDataAgendamento.Visible = false;
                lblHoraAgendamento.Visible = false;
            }
            else
            {
                //dpkDataAgendamento.Enabled = true;
                tpHoraAgendamento.Enabled = true;
                //dpkDataAgendamento.Visible = true;
                tpHoraAgendamento.Visible = true;
                lblDataAgendamento.Visible = true;
                lblHoraAgendamento.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento Salva Chamado
    /// <summary>
    /// Salva um chamado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            //Salva as informações do chamado
            bool bOcorreuErro = false;
            string strFormatoData = ClsParametro.DataInclusao;
            string codigoSolicitante = WUCUsuario1.PessoaCodigo().ToString();

            //Validações
            if (codigoSolicitante == "0")
            {
                ExibeMensagem("Por favor, informe o solicitante.", "images/icones/info.gif", true);
                bOcorreuErro = true;
            }
            if ((chkAgendar.Checked) && (bOcorreuErro == false))
            {
                //if ((dpkDataAgendamento.SelectedDate.Date.ToString() == string.Empty) || (tpHoraAgendamento.SelectedTime.TimeOfDay.ToString() == string.Empty))
                //{
                //    ExibeMensagem("Por favor, informe a data e horário de agendamento.", "images/icones/info.gif", true);
                //    bOcorreuErro = true;
                //}
            }

            if (WUCPriorizacao1.getImpacto().ToString() == "")
            {
                ExibeMensagem("Por favor, informe o impacto.", "images/icones/info.gif", true);
                bOcorreuErro = true;
            }

            if (WUCPriorizacao1.getUrgencia().ToString() == "")
            {
                lblMensagem.Text = "Por favor, informe a urgência.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                ExibeMensagem("Por favor, informe a urgência.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if ((txtDescricao.Text == "") && (bOcorreuErro == false))
            {
                ExibeMensagem("Por favor, informe a descrição do chamado.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if (bOcorreuErro == false)
            {
                string strMensagem = string.Empty;

                //cria o objeto Chamado
                ServiceDesk.Negocio.ClsChamado objChamado = new ServiceDesk.Negocio.ClsChamado();

                //atribui os valores
                objChamado.Codigo.Valor = lblIDChamado.Text;
                objChamado.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
                objChamado.SolucionadoPrimeiroContato.Valor = chkResolvido.Checked ? "S" : "N";

                //Tratamento de Status Flávio 05/01/2006
                //objChamado.Status.Valor = wucIncidenteStatus.primeiroStatus("CHAMADO").ToString();
                wucIncidenteStatus.salvaStatus();

                objChamado.DataAlteracao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objChamado.Escalado.Valor = ckEscalado.Checked ? "S" : "N";
                objChamado.Impacto.Valor = WUCPriorizacao1.getImpacto().ToString();
                objChamado.Urgencia.Valor = WUCPriorizacao1.getUrgencia().ToString();
                objChamado.Prioridade.Valor = WUCPriorizacao1.getPrioridade().ToString();
                objChamado.TipoChamado.Valor = ddlTipoChamado.SelectedValue;

                //Busca os SLAs da prioridade e grava no chamado 
                if (WUCPriorizacao1.getPrioridade().ToString().Trim() != string.Empty)
                {
                    ClsPrioridade objPrioridade = new ClsPrioridade(Convert.ToInt32(WUCPriorizacao1.getPrioridade()));
                    objChamado.SLAInicio.Valor = objPrioridade.TempoInicioAtendimento.Valor;
                    objChamado.SLASolucao.Valor = objPrioridade.TempoSolucao.Valor;
                    objPrioridade = null;
                }

                string codUsuario = ClsUsuario.getCodigoUsuario().ToString();
                objChamado.MatriculaInclusor.Valor = codUsuario;
                objChamado.MatriculaAlterador.Valor = codUsuario;
                objChamado.OrigemChamado.Valor = ddlOrigemChamado.SelectedValue;
                objChamado.NivelAtendimento.Valor = WUCEscalacaoHorizontal1.getNivel();
                objChamado.Equipe.Valor = WUCEscalacaoHorizontal1.getEquipe();

                //Se não foi selecionado tecnico. O lider de equipe vira técnico
                if ((WUCEscalacaoHorizontal1.getNivel() != string.Empty) && (WUCEscalacaoHorizontal1.getEquipe() != string.Empty) && (WUCEscalacaoHorizontal1.getTecnico() == string.Empty))
                    objChamado.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(WUCEscalacaoHorizontal1.getEquipe());
                else
                    objChamado.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico();

                objChamado.Vip.Valor = ckVip.Checked ? "S" :"N";
                objChamado.ChamadoModelo.Valor = ckModelo.Checked ? "S" : "N";
                objChamado.MatriculaSolicitante.Valor = codigoSolicitante;
                    
                //Grava a data e o usuário ao finalizar
                if (wucIncidenteStatus.StatusAtual.Equals("Finalizado"))
                {
                    objChamado.DataFinalizacao.Valor = DateTime.Now.ToString();
                    objChamado.MatriculaFinalizador.Valor = codUsuario; 
                }

                //Instancia um obj antes da alteração para utilizar na gravacao de log.
                ClsChamado objChamadoAntigo = new ClsChamado(Convert.ToInt32(objChamado.Codigo.Valor));

                if (objChamado.altera(out strMensagem))
                {
                    string strMensagemAviso = "Operação realizada com sucesso.";
                    string strImagemAviso = "images/icones/info.gif";

                    WUCAnexo1.salvaDocumento(true, lblIDChamado.Text, "CHAMADO");
                    //propaga o SLA
                    ClsChamado.AlteraSLAItensRelacionados(Convert.ToInt32(objChamado.Codigo.Valor), Convert.ToInt32(WUCPriorizacao1.getPrioridade().Trim()), Convert.ToInt32(WUCPriorizacao1.getImpacto()), Convert.ToInt32(WUCPriorizacao1.getUrgencia()));

                    if (!string.IsNullOrEmpty(lblIDChamado.Text.Trim()))
                    { wucIncidenteStatus.montaDados(Convert.ToInt32(lblIDChamado.Text), "CHAMADO"); }

                    //Instancia um obj APÓS da alteração para utilizar na gravacao de log.
                    ClsChamado objChamadoAtualizado = new ClsChamado(Convert.ToInt32(objChamado.Codigo.Valor));

                    //Marca o campo de data de alteracao para nao ser verificado
                    objChamadoAtualizado.DataAlteracao.VerificaAlteracao = false;

                    //grava log de alteração
                    ClsLog.insereLog(ClsLog.enumTipoLog.UPDATE, objChamado.MatriculaAlterador.Valor, "Chamado", objChamado.Codigo.Valor, objChamadoAtualizado.Atributos, objChamadoAntigo.Atributos);

                    //Se houve mudança na escalação horizontal. grava escalacao e manda mail
                    if ((objChamadoAntigo.NivelAtendimento.Valor != objChamadoAtualizado.NivelAtendimento.Valor) || (objChamadoAntigo.Equipe.Valor != objChamadoAtualizado.Equipe.Valor) || (objChamadoAntigo.Tecnico.Valor != objChamadoAtualizado.Tecnico.Valor))
                    {
                        #region Grava Escalacao Horizontal e envia e-mail.
                        ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal();
                        string strCodigoEscalacao = objEscalacaoHorizontal.insereEscalacao(objChamado.Atributos.NomeTabela, objChamado.Codigo.Valor, objChamado.NivelAtendimento.Valor, objChamado.Equipe.Valor, objChamado.Tecnico.Valor, objChamado.MatriculaAlterador.Valor).ToString();
                        if (strCodigoEscalacao != string.Empty)
                        {

                            #region Envia notificacao Equipe ou Tecnico
                            if ((objChamado.Equipe.Valor != string.Empty) || (objChamado.Tecnico.Valor != string.Empty))
                            {
                                ClsNotificacao objNotificacao = new ClsNotificacao();
                                objNotificacao.Tabela.Valor = "EscalacaoHorizontal";
                                objNotificacao.DtInclusao.Valor = System.DateTime.Now.ToString(ClsParametro.DataInclusao);
                                objNotificacao.IdentificadorTabela.Valor = objEscalacaoHorizontal.Codigo.Valor;
                                objNotificacao.CodigoUsuarioEmissor.Valor = objChamadoAntigo.MatriculaInclusor.Valor;

                                if ((objChamado.Equipe.Valor != string.Empty) && (objChamado.Tecnico.Valor == string.Empty))
                                {
                                    #region Equipe

                                    //lider
                                    SqlDataReader objReaderLider = ClsEquipe.getlLiderEquipe(objChamado.Equipe.Valor.ToString());
                                    if (objReaderLider.Read())
                                    {
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objReaderLider["pessoa_codigo"].ToString();
                                    }
                                    objReaderLider = null;

                                    ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objChamado.Equipe.Valor));
                                    objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("A Requisição de Serviço " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico) + objChamado.Codigo.Valor + " com a descricao '" + objChamado.Descricao.Valor + "' foi escalado para o grupo '" + objEquipe.Descricao.Valor + "' pela central de serviços.");
                                    objEquipe = null;

                                    #endregion Equipe
                                }
                                else
                                {
                                    #region Tecnico

                                    if ((objChamado.Equipe.Valor != string.Empty) && (objChamado.Tecnico.Valor != string.Empty))
                                    {
                                        //Tecnico                
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objChamado.Tecnico.Valor;
                                        objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("A Requisição de Serviço " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico) + objChamado.Codigo.Valor + " com a descricao '" + objChamado.Descricao.Valor + "' foi escalado para o técnico '" + ClsUsuario.getNomeUsuario(objChamado.Tecnico.Valor) + "' pela central de serviços.");
                                    }
                                    #endregion Tecnico
                                }

                                ClsIdentificador objIdentificadorNotificacao = new ClsIdentificador();
                                objIdentificadorNotificacao.Tabela.Valor = "Notificacao";
                                objNotificacao.Codigo.Valor = objIdentificadorNotificacao.getProximoValor().ToString();

                                //grava a notificacao
                                if (objNotificacao.enviar(out strMensagem))
                                {
                                    objIdentificadorNotificacao.atualizaValor();
                                    //envia o mensagem de notificacao;
                                    if (ClsNotificacao.EnviaMensagemNotificacao(objNotificacao))
                                    {
                                        strMensagemAviso = " Foi enviada uma mensagem de notificação.";
                                        strImagemAviso = "images/icones/info.gif";
                                    }
                                    else
                                    {
                                        strMensagemAviso = " Não foi possível enviar uma mensagem notificação.";
                                        strImagemAviso = "images/icones/aviso.gif";
                                    }
                                }
                                else
                                {
                                    strMensagemAviso = " Não foi possível enviar uma mensagem notificação.";
                                    strImagemAviso = "images/icones/aviso.gif";
                                }

                                objNotificacao = null;
                                objIdentificadorNotificacao = null;
                            }
                            #endregion

                            ClsEscalacaoHorizontal objEscalacao = new ClsEscalacaoHorizontal(Convert.ToInt32(strCodigoEscalacao));
                            ClsEscalacaoHorizontal.propagaEscalacaoHorizontal(objEscalacao);
                            objEscalacao = null;
                        }
                        objEscalacaoHorizontal = null;

                        #endregion
                    }
                    objChamadoAtualizado = null;

                    ExibeMensagem(strMensagemAviso, strImagemAviso, true);
                }
                else
                {
                    ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                }

                if (lblIDChamado.Text.Trim() != string.Empty)
                {
                    WUCLog1.MontaDados(Convert.ToInt32(lblIDChamado.Text.Trim()));
                }

                //Se foi classificado como INCIDENTE / MUDANÇA / Req. Servico
                //cria um INCIDENTE ou MUDANÇA conforme o caso.
                #region Incidente
                if (ddlTipoChamado.SelectedValue == ClsTipoChamado.getCodigoTipoChamadoIncidente())
                {
                    if (!ClsChamado.PossuiIncidenteVinculado(objChamado.Codigo.Valor))
                    {
                        string strCodigoIncidenteCriado = string.Empty;
                        if (ClsIncidente.criaIncidenteBaseadoChamado(objChamado.Codigo.Valor, out strCodigoIncidenteCriado))
                        {
                            ExibeMensagem("Foi criado um incidente vinculado ao chamado.", "images/icones/info.gif", true);
                            ClsChamado.geraGridViewIncidentes(gvIncidentesVinculados, objChamado.Codigo.Valor);
                        }
                        else
                        {
                            ExibeMensagem("Não foi possível criar um incidente vinculado ao chamado.", "images/icones/aviso.gif", true);
                        }
                    }

                    //Apaga todos os itens de outros tipos vinculados ao chamado
                    //menos os do tipo requisicao de serviço.
                    ClsChamado.apagaItensVinculados("Incidente", objChamado.Codigo.Valor);
                }
                #endregion

                #region Requisicao Servico
                if (ddlTipoChamado.SelectedValue == ClsTipoChamado.getCodigoTipoChamadoRequisicaoServico())
                {
                    if (!ClsChamado.PossuiRequisicaoServicoVinculado(objChamado.Codigo.Valor))
                    {
                        string strCodigoCriado = string.Empty;
                        if (ClsRequisicaoServico.criaRequisicaoServicoBaseadoChamado(objChamado.Codigo.Valor, out strCodigoCriado))
                        {
                            ExibeMensagem("Foi criado uma requisição de serviço vinculada ao chamado.", "images/icones/info.gif", true);
                            ClsChamado.geraGridViewRequisicaoServico(gvRequisicaoServico, objChamado.Codigo.Valor);
                        }
                        else
                        {
                            ExibeMensagem("Não foi possível criar uma Requisição de Serviço vinculado ao chamado.", "images/icones/aviso.gif", true);
                        }
                    }

                    //Apaga todos os itens de outros tipos vinculados ao chamado
                    //menos os do tipo requisicao de serviço.
                    ClsChamado.apagaItensVinculados("RequisicaoServico", objChamado.Codigo.Valor);
                }
                #endregion

                #region Requisição de Mudança
                if (ddlTipoChamado.SelectedValue == ClsTipoChamado.getCodigoTipoChamadoRequisicaoMudanca())
                {
                    if (!ClsChamado.PossuiRequisicaoMudancaVinculado(objChamado.Codigo.Valor))
                    {
                        string strCodigoCriado = string.Empty;
                        if (ClsRequisicaoMudanca.criaRequisicaoMudancaBaseadoChamado(objChamado.Codigo.Valor, out strCodigoCriado))
                        {
                            ExibeMensagem("Foi criado uma Requisição de Mudanca vinculada ao chamado.", "images/icones/info.gif", true);
                            ClsChamado.geraGridViewRequisicaoMudanca(gvRequisicaoMudanca, objChamado.Codigo.Valor);
                        }
                        else
                        {
                            ExibeMensagem("Não foi possível criar uma Requisição de Mudança vinculado ao chamado.", "images/icones/aviso.gif", true);
                        }
                    }

                    //Apaga todos os itens de outros tipos vinculados ao chamado
                    //menos os do tipo requisicao de Mudanca.
                    ClsChamado.apagaItensVinculados("RequisicaoMudanca", objChamado.Codigo.Valor);
                }
                #endregion

                #region Outros Tipos que nao geram itens automaticos
                if ((ddlTipoChamado.SelectedValue != ClsTipoChamado.getCodigoTipoChamadoRequisicaoMudanca()) && (ddlTipoChamado.SelectedValue != ClsTipoChamado.getCodigoTipoChamadoRequisicaoServico()) && (ddlTipoChamado.SelectedValue != ClsTipoChamado.getCodigoTipoChamadoIncidente()))
                {
                    //Apaga todos os itens de outros tipos vinculados ao chamado
                    //menos os do tipo requisicao de serviço.
                    ClsChamado.apagaItensVinculados("Outros", objChamado.Codigo.Valor);
                }
                #endregion

                if (!string.IsNullOrEmpty(lblIDChamado.Text.Trim()))
                {
                    wucIncidenteStatus.montaDados(Convert.ToInt32(lblIDChamado.Text), "CHAMADO");
                }

                //Gera as grids novamente
                //RS
                ClsChamado.geraGridViewRequisicaoServico(gvRequisicaoServico, objChamado.Codigo.Valor);
                //Incidente
                ClsChamado.geraGridViewIncidentes(gvIncidentesVinculados, objChamado.Codigo.Valor);
                //mudanca

                //gera niveis da escalacao horizontal novamente
                WUCEscalacaoHorizontal1.geraDropDownListNivel(objChamado.NivelAtendimento.Valor);
                WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objChamado.NivelAtendimento.Valor, objChamado.Equipe.Valor, objChamado.Tecnico.Valor);

                //============================================================//
                // - O que: Bloqueia campos.
                // - Quem: Fernanda Passos.
                // - Quando: 05/03/2006 ás 22:26hs.
                //============================================================//
                if (lblIDChamado.Text != string.Empty) BloqueiaCampos(Convert.ToInt32(lblIDChamado.Text.Trim()));
                //============================================================//

                objChamado = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento Grava Histórico de Ligações
    /// <summary>
    /// Evento Grava Histórico de Ligações
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGravaHistoricoLigacao_Click(object sender, EventArgs e)
    {
        //Salva as informações do chamado
        bool bOcorreuErro = false;
        string strFormatoDataInclusao = ClsParametro.DataInclusao;

        //Validações    
        if ((txtDescricaoLigacao.Text == "") && (bOcorreuErro == false))
        {
            ExibeMensagem("Por favor informe a descrição do registro de ligação.", "images/icones/aviso.gif", true);
            bOcorreuErro = true;
        }

        if (bOcorreuErro == false)
        {
            string strMensagem = string.Empty;
            //cria o objeto Chamado
            ClsLigacao objLigacao = new ClsLigacao();

            //atribui os valores
            objLigacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricaoLigacao.Text);
            objLigacao.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
            objLigacao.CodigoChamado.Valor = lblIDChamado.Text;
            objLigacao.CodigoInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString().ToString();

            ClsIdentificador objIdentificador = new ClsIdentificador();
            objIdentificador.Tabela.Valor = objLigacao.Atributos.NomeTabela;
            objLigacao.Codigo.Valor = objIdentificador.getProximoValor().ToString();

            if (objLigacao.insere(out strMensagem))
            {
                objIdentificador.atualizaValor();
                ExibeMensagem("Operação realizada com sucesso.", "images/icones/info.gif", true);
                txtDescricaoLigacao.Text = "";
                ClsChamado.geraGridViewLigacoes(gvHistoricoLigacoes, lblIDChamado.Text);
            }
            else
            {
                ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
            }
        }
    }
    #endregion

    #region Evento RowCommand gvHistoricoLigacoes_RowCommand
    /// <summary>
    /// Evento RowCommand gvHistoricoLigacoes_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHistoricoLigacoes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Excluir")
        {
            string strCodigoLigacao = string.Empty;
            string strCodigoChamado = string.Empty;
            GridViewRow objRow = gvHistoricoLigacoes.Rows[Convert.ToInt32(e.CommandArgument)];

            if (objRow != null)
            {
                Label lblCodigo = (Label)objRow.FindControl("lblCodigoLigacao");
                strCodigoLigacao = lblCodigo.Text;
                strCodigoChamado = lblIDChamado.Text;

                if ((strCodigoLigacao != string.Empty) && (strCodigoChamado != string.Empty))
                {
                    try
                    {
                        ClsLigacao objLigacao = new ClsLigacao();
                        objLigacao.Codigo.Valor = strCodigoLigacao;
                        objLigacao.exclui();
                        ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                    }
                    catch
                    {
                        ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                    }
                }
                ClsChamado.geraGridViewLigacoes(gvHistoricoLigacoes, strCodigoChamado);
            }
        }
    }
    #endregion

    #region Evento RowDataBound gvHistoricoLigacoes_RowDataBound
    /// <summary>
    /// Evento RowDataBound gvHistoricoLigacoes_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHistoricoLigacoes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //condicao IF que exibe os dados no GridView (estado: não-editável)
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lblCodigoAtendente = (Label)e.Row.FindControl("lblCodigoAtendente");
                Label lblNomeAtendente = (Label)e.Row.FindControl("lblNomeAtendente");
                lblNomeAtendente.Text = ClsUsuario.getNomeUsuario(lblCodigoAtendente.Text);

                string strFormatoDataExibicao = ClsParametro.DataCompletaExibicao;
                Label lblData = (Label)e.Row.FindControl("lblDataHistorico");
                lblData.Text = Convert.ToDateTime(lblData.Text.Trim()).ToString(strFormatoDataExibicao);
                TextBox txtDescricaoHistorico = (TextBox)e.Row.FindControl("txtDescricaoHistorico");
                txtDescricaoHistorico.Text = ClsTexto.trocaHtmlPorAspa(txtDescricaoHistorico.Text);

                // Adiciona um evento javascript no botão Excluir
                ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                btnExcluir.Attributes.Add("onclick", "return verifica();");
            }
        }
    }
    #endregion

    #region Evento RowCommand gvNotaAtendimento_RowCommand
    /// <summary>
    /// Evento RowCommand gvNotaAtendimento_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvNotaAtendimento_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Excluir")
        {
            string strCodigoNotaAtendimento = string.Empty;
            string strCodigoChamado = string.Empty;
            GridViewRow objRow = gvNotaAtendimento.Rows[Convert.ToInt32(e.CommandArgument)];

            if (objRow != null)
            {
                Label lblCodigo = (Label)objRow.FindControl("lblCodigoNotaAtendimento");
                strCodigoNotaAtendimento = lblCodigo.Text;
                strCodigoChamado = lblIDChamado.Text;

                if ((strCodigoNotaAtendimento != string.Empty) && (strCodigoChamado != string.Empty))
                {
                    try
                    {
                        ClsNotaAtendimento objNotaAtendimento = new ClsNotaAtendimento();
                        objNotaAtendimento.Codigo.Valor = strCodigoNotaAtendimento;
                        objNotaAtendimento.exclui();
                        ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                    }
                    catch
                    {
                        ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                    }
                }
                ClsChamado.geraGridViewNotaAtendimento(gvNotaAtendimento, strCodigoChamado);
            }
        }
    }
    #endregion

    #region Evento RowDataBound gvNotaAtendimento_RowDataBound
    /// <summary>
    /// Evento Evento RowDataBound gvNotaAtendimento_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvNotaAtendimento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lblCodigoAtendente = (Label)e.Row.FindControl("lblCodigoAtendenteNotaAtendimento");
                Label lblNomeAtendente = (Label)e.Row.FindControl("lblNomeAtendenteNotaAtendimento");
                lblNomeAtendente.Text = ClsUsuario.getNomeUsuario(lblCodigoAtendente.Text);

                string strFormatoDataExibicao = ClsParametro.DataCompletaExibicao;
                Label lblData = (Label)e.Row.FindControl("lblDataNota");
                lblData.Text = Convert.ToDateTime(lblData.Text.Trim()).ToString(strFormatoDataExibicao);

                // Adiciona um evento javascript no botão Excluir
                ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                //btnExcluir.Attributes.Add("onclick", "return verifica();");
            }
        }
    }
    #endregion

    #region Evento Grava Nota
    /// <summary>
    /// Evento Grava Nota
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGravaNota_Click(object sender, EventArgs e)
    {
        try
        {
            //Salva as informações do chamado
            bool bOcorreuErro = false;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;

            //Validações    
            if ((txtDescricaoNotaAtendimento.Text == "") && (bOcorreuErro == false))
            {
                ExibeMensagem("Por favor informe a descrição do registro.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if (bOcorreuErro == false)
            {
                string strMensagem = string.Empty;
                //cria o objeto Chamado
                ClsNotaAtendimento objNotaAtendimento = new ClsNotaAtendimento();

                //atribui os valores
                objNotaAtendimento.Tabela.Valor = "Chamado";
                objNotaAtendimento.DescricaoNota.Valor = ClsTexto.trocaAspaPorHtml(txtDescricaoNotaAtendimento.Text);
                objNotaAtendimento.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                objNotaAtendimento.IdentificadorTabela.Valor = lblIDChamado.Text;
                objNotaAtendimento.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString().ToString();

                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = objNotaAtendimento.Atributos.NomeTabela;
                objNotaAtendimento.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objNotaAtendimento.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                    ExibeMensagem("Operação realizada com sucesso.", "images/icones/info.gif", true);
                    txtDescricaoNotaAtendimento.Text = "";
                    ClsChamado.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDChamado.Text);
                }
                else
                {
                    ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento btnAplicaChamadoPreDefinido_Click
    /// <summary>
    /// Evento btnAplicaChamadoPreDefinido_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /*protected void btnAplicaChamadoPreDefinido_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlChamadoPreDefinido.SelectedValue != string.Empty)
            {
                try
                {
                    //busca os dados do incidente selecionado.
                    ClsChamado objChamadoModelo = new ClsChamado(Convert.ToInt32(ddlChamadoPreDefinido.SelectedValue));

                    //preenche os campos de classificaçãso e cadastro da tela.
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objChamadoModelo.NivelAtendimento.Valor, objChamadoModelo.Equipe.Valor, objChamadoModelo.Tecnico.Valor);

                    ddlOrigemChamado.SelectedValue = objChamadoModelo.OrigemChamado.Valor;

                    WUCPriorizacao1.setImpacto(objChamadoModelo.Impacto.Valor);
                    WUCPriorizacao1.setUrgencia(objChamadoModelo.Urgencia.Valor);

                    if (objChamadoModelo.Escalado.Valor == "S")
                    { ckEscalado.Checked = true; }
                    else
                    { ckEscalado.Checked = false; }

                    if (objChamadoModelo.Vip.Valor == "S")
                    { ckVip.Checked = true; }
                    else
                    { ckVip.Checked = false; }

                    objChamadoModelo = null;

                    //mensagem confirmacao
                    ExibeMensagem("Operação realizada com sucesso.", "images/icones/info.gif", true);

                }
                catch
                {
                    //mensagem erro
                    ExibeMensagem("Não foi possível aplicar os dados do modelo selecionado.", "images/icones/aviso.gif", true);
                }
            }
            else
            {
                //mensagem erro
                ExibeMensagem("Selecione um modelo.", "images/icones/aviso.gif", true);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }*/
    #endregion

    #region Evento lkbSolucao_Click
    /// <summary>
    /// Evento lkbSolucao_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbSolucao_Click(object sender, EventArgs e)
    {
        try
        {
            mvwAbas.ActiveViewIndex = 8;

            if (lblIDChamado.Text.Trim() != string.Empty)
            {
                WUCSolucaoFiltro1.PreencheCampo("Chamado", Convert.ToInt32(lblIDChamado.Text.Trim()));
                WUCSolucaoFiltro1.Filtrar();
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento Envia para base de conhecimento
    /// <summary>
    /// Envia para base de conhecimento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnviaParaBaseConhecimento_Click(object sender, EventArgs e)
    {
        string strMensagem = string.Empty;

        divMensagem.Visible = false;
        if (lblIDChamado.Text.Trim() != string.Empty)
        {
            string codUsuario = ClsUsuario.getCodigoUsuario().ToString().ToString();
            ClsConhecimentoProcesso.EnviaParaBaseConhecimento("Chamado", lblIDChamado.Text.Trim(), codUsuario,codUsuario, out strMensagem);
            lblMensagem.Text = strMensagem;
            imgIcone.ImageUrl = "images/icones/info.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;
        }
    }
    #endregion

    #region Evento btnVinculaIncidente_Click
    /// <summary>
    /// Evento btnVinculaIncidente_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVinculaIncidente_Click(object sender, EventArgs e)
    {
        try
        {
            if (pnlFiltros.Visible)
            {
                btnVinculaIncidente.Text = "Pesquisar Incidente";
                pnlFiltros.Visible = false;
                pnlResultadoPesquisaIncidente.Visible = false;
            }
            else
            {
                btnVinculaIncidente.Text = "Ocultar Área de Pesquisa";
                pnlFiltros.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Evento btnFiltrarIncidentes_Click
    /// <summary>
    /// Evento btnFiltrarIncidentes_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltrarIncidentes_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = string.Empty;
            string strMensagem = string.Empty;
            bool bPrimeiroCampo = true;

            //monta a query de acordo com os filtros.
            strSql = "SELECT C.incidente_codigo, C.descricao, C.data_inclusao, C.pessoa_codigo_proprietario, C.pessoa_codigo_solicitante ";
            strSql += "FROM Incidente C ";
            if (ddlServicoFiltro.SelectedValue != string.Empty)
            {
                strSql += ", IncidenteIC CIC ";
            }
            strSql += "WHERE ";

            if (txtCodigoIncidenteFiltro.Text.Trim() != string.Empty)
            {
                strSql += "C.incidente_codigo =" + ClsTexto.trocaAspaPorHtml(txtCodigoIncidenteFiltro.Text.Trim());
                bPrimeiroCampo = false;
            }

            if (txtDescricaoFiltro.Text.Trim() != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += " C.descricao Like '%" + ClsTexto.trocaAspaPorHtml(txtDescricaoFiltro.Text.Trim()) + "%' ";
            }

            if (ddlSolicitanteFiltro.SelectedValue != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += "C.pessoa_codigo_solicitante =" + ddlSolicitanteFiltro.SelectedValue;
            }

            if (ddlServicoFiltro.SelectedValue != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += " C.incidente_codigo = CIC.incidente_codigo AND CIC.ic_codigo = " + ddlServicoFiltro.SelectedValue;
            }

            if (lblIDChamado.Text != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += " C.incidente_codigo NOT IN (SELECT incidente_codigo from IncidenteChamado WHERE chamado_codigo =" + lblIDChamado.Text + ") ";
            }
            strSql += " ORDER BY C.incidente_codigo";

            if (!ClsChamado.geraGridViewQuery(gvResultadoFiltroIncidentes, strSql, out strMensagem))
            {
                ExibeMensagem(strMensagem, "images/icones/aviso.gif", true);
                pnlResultadoPesquisaIncidente.Visible = false;
            }
            else
            {
                pnlResultadoPesquisaIncidente.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvResultadoFiltroIncidentes_RowCommand
    /// <summary>
    /// Evento gvResultadoFiltroIncidentes_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvResultadoFiltroIncidentes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoChamado = string.Empty;
                string strCodigoIncidente = string.Empty;
                GridViewRow objRow = gvIncidentesVinculados.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoIncidente");
                    strCodigoIncidente = lblCodigo.Text;
                    strCodigoChamado = lblIDChamado.Text;

                    if ((strCodigoChamado != string.Empty) && (strCodigoIncidente != string.Empty))
                    {
                        try
                        {
                            ServiceDesk.Negocio.ClsIncidente.RemoveRelacaoIncidenteChamado(strCodigoIncidente, strCodigoChamado);
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsChamado.geraGridViewIncidentes(gvIncidentesVinculados, strCodigoChamado);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento btnGravarVinculos_Click
    /// <summary>
    /// Evento btnGravarVinculos_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGravarVinculos_Click(object sender, EventArgs e)
    {
        string strCodigoChamado = lblIDChamado.Text;
        try
        {
            for (int i = 0; i < gvResultadoFiltroIncidentes.Rows.Count; i++)
            {
                GridViewRow objRow = gvResultadoFiltroIncidentes.Rows[i];

                if (objRow != null)
                {
                    CheckBox ckCodigoIncidente = (CheckBox)objRow.FindControl("ck_CodigoIncidente");
                    if (ckCodigoIncidente.Checked)
                    {
                        Label lblCodigoIncidenteVinculado = (Label)objRow.FindControl("lblCodigoIncidente");
                        string strCodigoIncidenteVinculado = lblCodigoIncidenteVinculado.Text;
                        if ((strCodigoIncidenteVinculado != string.Empty) && (strCodigoChamado != string.Empty))
                        {
                            ServiceDesk.Negocio.ClsIncidente.AdicionaRelacaoIncidenteChamado(strCodigoChamado, strCodigoIncidenteVinculado);
                        }
                    }
                }
            }

            ExibeMensagem("Operação efetuada com sucesso.", "images/icones/info.gif", true);
            ClsChamado.geraGridViewIncidentes(gvIncidentesVinculados, strCodigoChamado);
            btnVinculaIncidente.Text = "Pesquisar Incidente";
            pnlFiltros.Visible = false;
            pnlResultadoPesquisaIncidente.Visible = false;

        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Evento gvIncidentesVinculados_RowCommand
    /// <summary>
    /// Evento gvIncidentesVinculados_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvIncidentesVinculados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoChamado = string.Empty;
                string strCodigoIncidente = string.Empty;
                GridViewRow objRow = gvIncidentesVinculados.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoIncidente");
                    strCodigoIncidente = lblCodigo.Text;
                    strCodigoChamado = lblIDChamado.Text;

                    if ((strCodigoChamado != string.Empty) && (strCodigoIncidente != string.Empty))
                    {
                        try
                        {
                            ServiceDesk.Negocio.ClsIncidente.RemoveRelacaoIncidenteChamado(strCodigoIncidente, strCodigoChamado);
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsChamado.geraGridViewIncidentes(gvIncidentesVinculados, strCodigoChamado);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvRequisicaoServico_RowCommand
    /// <summary>
    /// Evento gvRequisicaoServico_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRequisicaoServico_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoChamado = string.Empty;
                string strCodigoServico = string.Empty;
                GridViewRow objRow = gvRequisicaoServico.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoServico");
                    strCodigoServico = lblCodigo.Text;
                    strCodigoChamado = lblIDChamado.Text;

                    if ((strCodigoChamado != string.Empty) && (strCodigoServico != string.Empty))
                    {
                        try
                        {
                            ServiceDesk.Negocio.ClsRequisicaoServico.RemoveRelacaoRequisicaoServicoChamado(strCodigoServico, strCodigoChamado);
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsChamado.geraGridViewRequisicaoServico(gvRequisicaoServico, strCodigoChamado);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvRequisicaoMudanca_RowCommand
    /// <summary>
    /// Evento gvRequisicaoMudanca_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRequisicaoMudanca_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoChamado = string.Empty;
                string strCodigoMudanca = string.Empty;
                GridViewRow objRow = gvRequisicaoMudanca.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoMudanca");
                    strCodigoMudanca = lblCodigo.Text;
                    strCodigoChamado = lblIDChamado.Text;

                    if ((strCodigoChamado != string.Empty) && (strCodigoMudanca != string.Empty))
                    {
                        try
                        {
                            ServiceDesk.Negocio.ClsRequisicaoMudanca.RemoveRelacaoRequisicaoMudancaChamado(strCodigoMudanca, strCodigoChamado);
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsChamado.geraGridViewRequisicaoMudanca(gvRequisicaoMudanca, strCodigoChamado);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvIncidentesVinculados_RowDataBound
    /// <summary>
    /// Evento gvIncidentesVinculados_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvIncidentesVinculados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion
    #endregion
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
}
