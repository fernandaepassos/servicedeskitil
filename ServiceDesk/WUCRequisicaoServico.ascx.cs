using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;
using ServiceDesk.Generica;

public partial class WUCRequisicaoServico : System.Web.UI.UserControl
{
    #region Declaracoes Publicas
    public String strFormatoDataInclusao = ClsParametro.DataInclusao;
    #endregion

    #region Propriedades

    private int intCodigoRequisicaoServico;
    private int intCodigoChamado;

    #endregion

    #region Métodos

    #region Bloqueia Campos
    /// <summary>
    /// Bloqueia Campos
    /// </summary>
    public void BloqueiaCampos()
    {
        //=======================================================================//
        // - O que: Bloqueia os campos impacto, urgencia, equipe e origem se o usuário
        // atual for de equipe superior a primeira e se o mesmo for um analista.
        // - Quem: Fernanda Passos.
        // - Quando: 05/03/2006 ás 23:00hs.
        //=======================================================================//

        //Verifica se o sistema esta configurado para bloqueiar os campos.
        if (ClsParametro.BloqueiaCampoParaNivelMaior1.Trim() == "S")
        {
            //Verifica se o usuário é esta com perfil analista e se esta em alguma equipe superior a do primeiro nível.
            if (ClsUsuario.VerificaPerfilNivel(ClsUsuario.getCodigoUsuario()) == true)
            {
                WUCPriorizacao1.BloqueiaCampos(true, true, true);
                ddlOrigemRequisicaoServico.Enabled = false;
            }
            else
            {
                WUCPriorizacao1.BloqueiaCampos(false, false, true);
                ddlOrigemRequisicaoServico.Enabled = true;
            }
        }
    }
    #endregion

    /// <summary>
    /// Abre um RequisicaoServico existente para edição
    /// </summary>
    /// <param name="CodigoRequisicaoServico">Código do RequisicaoServico desejado</param>
    public void EditaRequisicaoServico(int CodigoRequisicaoServico)
    {
        try
        {
            if (CodigoRequisicaoServico != 0)
            {
                //busca dados do RequisicaoServico e preenche as abas
                tblAbas.Visible = true;
                mvwAbas.Visible = true;
                lblIDRequisicaoServico.Text = CodigoRequisicaoServico.ToString();
                wucRequisicaoServicoStatus.montaDados(Convert.ToInt32(lblIDRequisicaoServico.Text), "RequisicaoServico");
            }
        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Cria um novo RequisicaoServico herdando as informações do chamado informado
    /// </summary>
    /// <param name="CodigoChamado">Codigo do Chamado</param>
    public void CriaRequisicaoServicoHerdandoChamado(int CodigoChamado)
    {
        try
        {
            intCodigoChamado = CodigoChamado;
            if (intCodigoChamado != 0)
            {
                //Cria o novo RequisicaoServico
                String strCodigoRequisicaoServicoCriado = string.Empty;
                if (ClsRequisicaoServico.criaRequisicaoServicoBaseadoChamado(intCodigoChamado.ToString(), out strCodigoRequisicaoServicoCriado))
                {
                    ExibeMensagem("Operação efetuada com sucesso.", "images/icones/info.gif", true);
                    intCodigoRequisicaoServico = Convert.ToInt32(strCodigoRequisicaoServicoCriado);

                    //busca dados do RequisicaoServico criado e preenche as abas
                    tblAbas.Visible = true;
                    mvwAbas.Visible = true;
                    PreencheDadosRequisicaoServico(intCodigoRequisicaoServico);
                }
                else
                {
                    ExibeMensagem("Não foi possível criar um RequisicaoServico vinculado ao chamado.", "images/icones/aviso.gif", true);
                }
            }
        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Fornece um meio de acesso ao painel de mensagem
    /// </summary>
    /// <param name="Mensagem">Mensagem a ser exibida na tela</param>
    /// <param name="Imagem">Nome da imagem do ícone do painel</param>
    /// <param name="Ativo">true para Exibir, false para Ocultar</param>
    /// <example>ExibeMensagem("teste","images/icones/aviso.gif",true)</example>
    private void ExibeMensagem(String Mensagem, String Imagem, bool Ativo)
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
        else //nao foi especidifcao, assume true
        {
            lblMensagem.Visible = true;
            divMensagem.Visible = true;
        }
    }

    /// <summary>
    /// Preenche os dados do RequisicaoServico informado
    /// </summary>
    /// <param name="CodigoRequisicaoServico">Codigo do RequisicaoServico</param>
    private void PreencheDadosRequisicaoServico(int CodigoRequisicaoServico)
    {
        try
        {
            string strDataMinimaSistema = ClsParametro.DataMinimaSistema;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;
            ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico(CodigoRequisicaoServico);
            if (objRequisicaoServico != null)
            {
                //prenche os dados do RequisicaoServico, marca os itens nas caixa e listas, etc.
                #region Remove o RequisicaoServico atual da lista de modelos se ele for modelo
                if (lblIDRequisicaoServico.Text != string.Empty)
                {
                    ListItem itemAtual = ddlRequisicaoServicoPreDefinido.Items.FindByValue(lblIDRequisicaoServico.Text);
                    ddlRequisicaoServicoPreDefinido.Items.Remove(itemAtual);
                    itemAtual = null;
                }
                #endregion Remove o RequisicaoServico atual da lista de modelos se ele for modelo

                #region Preenche os dados do RequisicaoServico
                try
                {
                    this.lblIDRequisicaoServico.Text = objRequisicaoServico.Codigo.Valor;
                    this.txtDescricao.Text = ClsTexto.trocaHtmlPorAspa(objRequisicaoServico.Descricao.Valor);
                    if (objRequisicaoServico.PessoaCodigoSolicitante.Valor != string.Empty)
                    {
                        WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(objRequisicaoServico.PessoaCodigoSolicitante.Valor));
                    }
                    if (objRequisicaoServico.PessoaCodigoInclusor.Valor != string.Empty)
                    {
                        this.txtAtendente.Text = ClsUsuario.getNomeUsuario(objRequisicaoServico.PessoaCodigoInclusor.Valor);
                    }

                    WUCPriorizacao1.setImpacto(objRequisicaoServico.Impacto.Valor);
                    WUCPriorizacao1.setUrgencia(objRequisicaoServico.Urgencia.Valor);

                    if (objRequisicaoServico.OrigemRequisicaoServico.Valor != string.Empty)
                    {
                        this.ddlOrigemRequisicaoServico.SelectedValue = objRequisicaoServico.OrigemRequisicaoServico.Valor;
                    }
                    if ((objRequisicaoServico.Escalado.Valor != string.Empty) && (objRequisicaoServico.Escalado.Valor == "S"))
                    {
                        ckEscalado.Checked = true;
                    }

                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoServico.NivelAtendimento.Valor, objRequisicaoServico.Equipe.Valor, objRequisicaoServico.Tecnico.Valor);
                    WUCEscalacaoHorizontal1.strTabela = "RequisicaoServico";
                    if (lblIDRequisicaoServico.Text.Trim() != string.Empty) WUCEscalacaoHorizontal1.intIdentificadorTabela = Convert.ToInt32(lblIDRequisicaoServico.Text.Trim());

                    if ((objRequisicaoServico.Vip.Valor != string.Empty) && (objRequisicaoServico.Vip.Valor == "S"))
                    { ckVip.Checked = true; }
                    else
                    { ckVip.Checked = false; }

                    if ((objRequisicaoServico.Modelo.Valor != string.Empty) && (objRequisicaoServico.Modelo.Valor.ToUpper() == "S"))
                    { ckModelo.Checked = true; }
                    else
                    { ckModelo.Checked = false; }

                }
                catch
                {
                    ExibeMensagem("Ocorreu um erro ao preencher os dados do RequisicaoServico.", "images/icones/aviso.gif", true);
                }
                #endregion Preenche Dados RequisicaoServico

            }
            objRequisicaoServico = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }


    //Page Load  
    protected void Page_Load(object sender, EventArgs e)
    {
        int intRequisicaoCodigo = 0;

        try
        {

            if (Request.QueryString["RequisicaoServico"] != null)
            {
                intRequisicaoCodigo = Convert.ToInt32(Request.QueryString["RequisicaoServico"].ToString());
            }

            //Verifica a permissao do papel do usuario no chamado
            if (intRequisicaoCodigo > 0)
            {
                //=====================================================================//
                // - O que: Libera o bloqueio do botão salvar depois de ter definido 
                // um proprietário para o chamado. Se o chamado verifica a segurança dos 
                // papéis sem ter um proprietário o sistema irá blquear o botão salvar 
                // deixando o chamado impossibilitado de receber um proprietário.
                // - Quem: Fernanda Passos
                // - Quando: 31/03/2006 ás 14:49hs
                //=====================================================================//
                if (ClsRequisicaoServico.GetCodigoProprietario(intRequisicaoCodigo) != string.Empty) btnSalvar.Enabled = ClsUsuario.verificaAcessoPapel(ClsUsuario.getCodigoUsuario(), intRequisicaoCodigo, 2, "requisicaoservico");
                //=====================================================================//
            }

            pnlResultadoPesquisaChamado.Visible = false;

            //Esconde a mensagem de erro
            ExibeMensagem("", "", false);
            if (!Page.IsPostBack)
            {
                mvwAbas.ActiveViewIndex = 0;
                string strMensagem = string.Empty;

                #region Preenche combos, listas etc

                //Coloca o nome do Atendente
                txtAtendente.Text = ClsUsuario.getNomeUsuario();
                //dropdown lists
                ClsTipoOrigemChamado.geraDropDownList(ddlOrigemRequisicaoServico);
                WUCPriorizacao1.geraDropDownListImpactoUrgencia();

                if (lblIDRequisicaoServico.Text.Trim() == string.Empty)
                {
                    //Se está incluindo chamado, so exibe o 1º nivel para selecao
                    WUCEscalacaoHorizontal1.geraDropDownListNivel("0");
                }
                else
                {
                    //gera os niveis possiveis baseado no nivel do chamado
                    ClsRequisicaoServico objRequisicaoNivel = new ClsRequisicaoServico(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                    WUCEscalacaoHorizontal1.geraDropDownListNivel(objRequisicaoNivel.NivelAtendimento.Valor);
                    objRequisicaoNivel = null;
                }

                ClsRequisicaoServico.geraDropDownListRequisicaoServicoPreDefinido(ddlRequisicaoServicoPreDefinido);

                //Campos de filtro da aba de chamados vinculados
                //Combo Solicitantes
                ClsUsuario.geraDropDownList(ddlSolicitanteFiltro, "--");
                //Combo Serviços
                ClsItemConfiguracao.geraDropDownList(ddlServicoFiltro);

                #endregion

                if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                {
                    PreencheDadosRequisicaoServico(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                }

                //======================================================//
                // - O que: Bloqueia campos de acordo com regra definida.
                // - Quem: Fernanda Passos.
                // - Quando: 06/03/2006 ás 13:12hs.
                //======================================================//
                BloqueiaCampos();
                //======================================================//
            }

            //====================================================//
            // - O que: Abilta e desabilita as abas da tela.
            // - Quem: Fernanda Passos.
            // - Quando: 0704/2006
            //====================================================//
            if (lblIDRequisicaoServico.Text.Trim() != string.Empty) AbilitaDesabilitaAbas(true); else AbilitaDesabilitaAbas(false);
            //====================================================//

            if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
            {
                lblPrefixo.Text = ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico);

                //Log
                WUCLog1.MontaDados(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion


    //Abas
    #region Evento mudaAba
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
                        //Solicitante
                        //if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        //{
                        //  ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                        //  WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(objRequisicaoServico.PessoaCodigoSolicitante.Valor));
                        //  objRequisicaoServico = null;
                        //}
                        break;
                    }
                case 1:
                    {
                        //Chamados  
                        if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        {
                            ClsRequisicaoServico.geraGridViewChamadosVinculados(gvChamadosVinculados, lblIDRequisicaoServico.Text.Trim());
                        }
                        break;
                    }
                case 2:
                    {
                        //ICS
                        if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        {
                            WUCItemConfiguracaoTreeView1.CarregaIC(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                        }
                        break;
                    }
                case 3:
                    {
                        //Solucao
                        if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        {
                            WUCSolucaoFiltro1.PreencheCampo("RequisicaoServico", Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                            WUCSolucaoFiltro1.Filtrar();
                        }
                        break;
                    }

                case 4:
                    {
                        //Anexos
                        if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        {
                            WUCAnexo1.CarregaAnexos(lblIDRequisicaoServico.Text.Trim(), "RequisicaoServico");
                        }
                        break;
                    }
                case 5:
                    {
                        //Log
                        if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        {
                            WUCLog1.MontaDados(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                        }
                        break;
                    }
                case 6:
                    {
                        //Notas de Atendimento
                        if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        {
                            ClsRequisicaoServico.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDRequisicaoServico.Text.Trim());
                        }
                        break;
                    }
                case 7:
                    {
                        //Aprovadores
                        if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
                        {
                            WUCAprovador1.PreencheCampos("RequisicaoServico", Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                        }
                        break;
                    }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #endregion


    //Eventos dos Elementos da Tela
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            //Salva as informações do RequisicaoServico
            bool bOcorreuErro = false;
            string strCodigoChamado = string.Empty;
            string strCodigoRequisicaoServico = string.Empty;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;
            string strPessoaCodigo = ClsUsuario.getCodigoUsuario().ToString();

            #region Validações
            if (WUCUsuario1.PessoaCodigo() == 0)
            {
                ExibeMensagem("Por favor selecione o solicitante.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if (this.WUCPriorizacao1.getImpacto().ToString() == "")
            {
                ExibeMensagem("Por favor, informe o impacto.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if (this.WUCPriorizacao1.getUrgencia().ToString() == "")
            {
                lblMensagem.Text = "Por favor, informe a urgência.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                ExibeMensagem("Por favor, informe a urgência.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if ((this.txtDescricao.Text == "") && (bOcorreuErro == false))
            {
                ExibeMensagem("Por favor informe a descrição.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }
            #endregion

            if (bOcorreuErro == false)
            {
                String strMensagem = String.Empty;
                //cria o objeto RequisicaoServico
                ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico();

                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = objRequisicaoServico.Atributos.NomeTabela;

                //atribui os valores
                if (lblIDRequisicaoServico.Text == "") //novo
                {
                    objRequisicaoServico.PessoaCodigoInclusor.Valor = strPessoaCodigo;
                    objRequisicaoServico.PessoaCodigoSolicitante.Valor = WUCUsuario1.PessoaCodigo().ToString();
                    objRequisicaoServico.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                    objRequisicaoServico.Descricao.Valor = ClsTexto.trocaAspaPorHtml(this.txtDescricao.Text);
                    objRequisicaoServico.Status.Valor = wucRequisicaoServicoStatus.primeiroStatus("RequisicaoServico").ToString();

                    if (ckEscalado.Checked)
                    { objRequisicaoServico.Escalado.Valor = "S"; }
                    else
                    { objRequisicaoServico.Escalado.Valor = "N"; }
                    objRequisicaoServico.OrigemRequisicaoServico.Valor = ddlOrigemRequisicaoServico.SelectedValue;

                    // ************************************************************************
                    // Alteração Sylvio- 16-02-2006
                    // ************************************************************************
                    if (WUCEscalacaoHorizontal1.getNivel().ToString().Trim() != string.Empty)
                    {
                        objRequisicaoServico.Equipe.Valor = WUCEscalacaoHorizontal1.getEquipe().ToString();
                        //objIncidente.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                        objRequisicaoServico.NivelAtendimento.Valor = WUCEscalacaoHorizontal1.getNivel().ToString();
                        //Se não foi selecionado tecnico. O lider de equipe vira técnico
                        if ((WUCEscalacaoHorizontal1.getNivel().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getEquipe().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getTecnico().ToString() == string.Empty))
                        {
                            objRequisicaoServico.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(WUCEscalacaoHorizontal1.getEquipe().ToString());
                        }
                        else
                        {
                            objRequisicaoServico.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                        }
                    }
                    else
                    {
                        //Nivel e Equipe de Atendimento padrão (escalacao horizontal)
                        objRequisicaoServico.NivelAtendimento.Valor = ClsParametro.NivelAtendimentoPadrao;
                        objRequisicaoServico.Equipe.Valor = ClsParametro.EquipeAtendimentoPadrao;
                        objRequisicaoServico.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(ClsParametro.EquipeAtendimentoPadrao);
                    }
                    // ************************************************************************
                    
                    objRequisicaoServico.Impacto.Valor = WUCPriorizacao1.getImpacto().ToString();
                    objRequisicaoServico.Urgencia.Valor = WUCPriorizacao1.getUrgencia().ToString();
                    objRequisicaoServico.Prioridade.Valor = WUCPriorizacao1.getPrioridade().ToString();

                    if (ckVip.Checked)
                    { objRequisicaoServico.Vip.Valor = "S"; }
                    else
                    { objRequisicaoServico.Vip.Valor = "N"; }

                    if (ckModelo.Checked)
                    { objRequisicaoServico.Modelo.Valor = "S"; }
                    else
                    { objRequisicaoServico.Modelo.Valor = "N"; }

                    objRequisicaoServico.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                    //salva novo
                    if (objRequisicaoServico.insere(out strMensagem))
                    {
                        objIdentificador.atualizaValor();

                        #region Grava Escalacao Horizontal e envia e-mail.
                        ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal();
                        string strCodigoEscalacao = objEscalacaoHorizontal.insereEscalacao(objRequisicaoServico.Atributos.NomeTabela, objRequisicaoServico.Codigo.Valor, objRequisicaoServico.NivelAtendimento.Valor, objRequisicaoServico.Equipe.Valor, objRequisicaoServico.Tecnico.Valor, objRequisicaoServico.PessoaCodigoAlterador.Valor).ToString();
                        if (strCodigoEscalacao != string.Empty)
                        {

                            #region Envia notificacao Equipe ou Tecnico
                            if ((objRequisicaoServico.Equipe.Valor != string.Empty) || (objRequisicaoServico.Tecnico.Valor != string.Empty))
                            {
                                ClsNotificacao objNotificacao = new ClsNotificacao();
                                objNotificacao.Tabela.Valor = "EscalacaoHorizontal";
                                objNotificacao.DtInclusao.Valor = System.DateTime.Now.ToString(ClsParametro.DataInclusao);
                                objNotificacao.IdentificadorTabela.Valor = objEscalacaoHorizontal.Codigo.Valor;
                                objNotificacao.CodigoUsuarioEmissor.Valor = objRequisicaoServico.PessoaCodigoInclusor.Valor;

                                if ((objRequisicaoServico.Equipe.Valor != string.Empty) && (objRequisicaoServico.Tecnico.Valor == string.Empty))
                                {
                                    #region Equipe

                                    //lider
                                    SqlDataReader objReaderLider = ClsEquipe.getlLiderEquipe(objRequisicaoServico.Equipe.Valor.ToString());
                                    if (objReaderLider.Read())
                                    {
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objReaderLider["pessoa_codigo"].ToString();
                                    }
                                    objReaderLider = null;

                                    ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objRequisicaoServico.Equipe.Valor));
                                    objNotificacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml("A Requisição de Serviço " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico) + objRequisicaoServico.Codigo.Valor + " com a descricao '" + objRequisicaoServico.Descricao.Valor + "' foi escalado para o grupo '" + objEquipe.Descricao.Valor + "' pela central de serviços.");
                                    objEquipe = null;

                                    #endregion Equipe
                                }
                                else
                                {
                                    #region Tecnico

                                    if ((objRequisicaoServico.Equipe.Valor != string.Empty) && (objRequisicaoServico.Tecnico.Valor != string.Empty))
                                    {
                                        //Tecnico                
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objRequisicaoServico.Tecnico.Valor;
                                        objNotificacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml("A Requisição de Serviço " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico) + objRequisicaoServico.Codigo.Valor + " com a descricao '" + objRequisicaoServico.Descricao.Valor + "' foi escalado para o técnico '" + ClsUsuario.getNomeUsuario(objRequisicaoServico.Tecnico.Valor) + "' pela central de serviços.");
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
                                        strMensagem = "Operação realizada com sucesso. Foi enviada uma mensagem de notificação.";
                                    }
                                    else
                                    {
                                        strMensagem = " Não foi possível enviar uma mensagem notificação.";
                                    }
                                }
                                else
                                {
                                    strMensagem = " Não foi possível enviar uma mensagem de notificação.";
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

                        #region Cria o Log de Status Inicial
                        SServiceDesk.Negocio.ClsWorkFlow.gravaLog(Convert.ToInt32(objRequisicaoServico.Codigo.Valor), objRequisicaoServico.Atributos.NomeTabela, "0", objRequisicaoServico.Status.Valor);
                        #endregion

                        //gera niveis da escalacao horizontal novamente
                        WUCEscalacaoHorizontal1.geraDropDownListNivel(objRequisicaoServico.NivelAtendimento.Valor);

                        ExibeMensagem(strMensagem, "images/icones/info.gif", true);

                        this.lblIDRequisicaoServico.Text = objRequisicaoServico.Codigo.Valor.ToString();
                        WUCAnexo1.salvaDocumento(true, lblIDRequisicaoServico.Text, "REQUISICAOSERVICO");

                        PreencheDadosRequisicaoServico(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));

                        //====================================================//
                        // - O que: Abilita e desabilita as abas da tela.
                        // - Quem: Fernanda Passos.
                        // - Quando: 0704/2006
                        //====================================================//
                        AbilitaDesabilitaAbas(true);
                        //====================================================//
                    }
                    else
                    {
                        ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                    }

                    //gera niveis da escalacao horizontal novamente
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoServico.NivelAtendimento.Valor, objRequisicaoServico.Equipe.Valor, objRequisicaoServico.Tecnico.Valor);


                }
                else //alterando
                {
                    objRequisicaoServico.Codigo.Valor = lblIDRequisicaoServico.Text;
                    objRequisicaoServico.DataAlteracao.Valor = System.DateTime.Now.ToString(strFormatoDataInclusao);

                    objRequisicaoServico.PessoaCodigoAlterador.Valor = strPessoaCodigo;
                    objRequisicaoServico.PessoaCodigoSolicitante.Valor = WUCUsuario1.PessoaCodigo().ToString();
                    objRequisicaoServico.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(this.txtDescricao.Text);

                    if (ckEscalado.Checked)
                    { objRequisicaoServico.Escalado.Valor = "S"; }
                    else
                    { objRequisicaoServico.Escalado.Valor = "N"; }

                    objRequisicaoServico.OrigemRequisicaoServico.Valor = ddlOrigemRequisicaoServico.SelectedValue;

                    // ************************************************************************
                    // Alteração Gleison - 14-01-2006
                    // ************************************************************************
                    objRequisicaoServico.Equipe.Valor = WUCEscalacaoHorizontal1.getEquipe().ToString();
                    //objRequisicaoServico.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                    objRequisicaoServico.NivelAtendimento.Valor = WUCEscalacaoHorizontal1.getNivel().ToString();
                    //Se não foi selecionado tecnico. O lider de equipe vira técnico
                    if ((WUCEscalacaoHorizontal1.getNivel().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getEquipe().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getTecnico().ToString() == string.Empty))
                    {
                        objRequisicaoServico.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(WUCEscalacaoHorizontal1.getEquipe().ToString());
                    }
                    else
                    {
                        objRequisicaoServico.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                    }
                    // ************************************************************************

                    objRequisicaoServico.Impacto.Valor = WUCPriorizacao1.getImpacto().ToString();
                    objRequisicaoServico.Urgencia.Valor = WUCPriorizacao1.getUrgencia().ToString();
                    objRequisicaoServico.Prioridade.Valor = WUCPriorizacao1.getPrioridade().ToString();

                    if (ckVip.Checked)
                    { objRequisicaoServico.Vip.Valor = "S"; }
                    else
                    { objRequisicaoServico.Vip.Valor = "N"; }

                    if (ckModelo.Checked)
                    { objRequisicaoServico.Modelo.Valor = "S"; }
                    else
                    { objRequisicaoServico.Modelo.Valor = "N"; }


                    //Instancia um obj antes da alteração
                    //para utilizar na gravacao de log.
                    ClsRequisicaoServico objRequisicaoServicoAntigo = new ClsRequisicaoServico(Convert.ToInt32(objRequisicaoServico.Codigo.Valor));

                    //salva alteracao
                    if (objRequisicaoServico.altera(out strMensagem))
                    {
                        String strMensagemAviso = "Operação realizada com sucesso.";
                        String strImagemAviso = "images/icones/info.gif";

                        wucRequisicaoServicoStatus.salvaStatus();
                        WUCAnexo1.salvaDocumento(true, lblIDRequisicaoServico.Text, "REQUISICAOSERVICO");

                        //Instancia um obj APÓS da alteração
                        //para utilizar na gravacao de log.
                        ClsRequisicaoServico objRequisicaoServicoAtualizado = new ClsRequisicaoServico(Convert.ToInt32(objRequisicaoServico.Codigo.Valor));
                        //Marca o campo de data de alteracao para nao ser verificado
                        objRequisicaoServicoAtualizado.DataAlteracao.VerificaAlteracao = false;

                        //grava log de alteração
                        ClsLog.insereLog(ClsLog.enumTipoLog.UPDATE, objRequisicaoServico.PessoaCodigoAlterador.Valor, "RequisicaoServico", objRequisicaoServico.Codigo.Valor, objRequisicaoServicoAtualizado.Atributos, objRequisicaoServicoAntigo.Atributos);

                        PreencheDadosRequisicaoServico(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));

                        //Se houve mudança na escalação horizontal. grava escalacao e 
                        //manda mail
                        if ((objRequisicaoServicoAntigo.NivelAtendimento.Valor != objRequisicaoServicoAtualizado.NivelAtendimento.Valor) || (objRequisicaoServicoAntigo.Equipe.Valor != objRequisicaoServicoAtualizado.Equipe.Valor) || (objRequisicaoServicoAntigo.Tecnico.Valor != objRequisicaoServicoAtualizado.Tecnico.Valor))
                        {
                            #region Grava Escalacao Horizontal e envia e-mail.
                            ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal();
                            string strCodigoEscalacao = objEscalacaoHorizontal.insereEscalacao(objRequisicaoServico.Atributos.NomeTabela, objRequisicaoServico.Codigo.Valor, objRequisicaoServico.NivelAtendimento.Valor, objRequisicaoServico.Equipe.Valor, objRequisicaoServico.Tecnico.Valor, objRequisicaoServico.PessoaCodigoAlterador.Valor).ToString();
                            if (strCodigoEscalacao != string.Empty)
                            {

                                #region Envia notificacao Equipe ou Tecnico
                                if ((objRequisicaoServico.Equipe.Valor != string.Empty) || (objRequisicaoServico.Tecnico.Valor != string.Empty))
                                {
                                    ClsNotificacao objNotificacao = new ClsNotificacao();
                                    objNotificacao.Tabela.Valor = "EscalacaoHorizontal";
                                    objNotificacao.DtInclusao.Valor = System.DateTime.Now.ToString(ClsParametro.DataInclusao);
                                    objNotificacao.IdentificadorTabela.Valor = objEscalacaoHorizontal.Codigo.Valor;
                                    objNotificacao.CodigoUsuarioEmissor.Valor = objRequisicaoServicoAntigo.PessoaCodigoInclusor.Valor;

                                    if ((objRequisicaoServico.Equipe.Valor != string.Empty) && (objRequisicaoServico.Tecnico.Valor == string.Empty))
                                    {
                                        #region Equipe

                                        //lider
                                        SqlDataReader objReaderLider = ClsEquipe.getlLiderEquipe(objRequisicaoServico.Equipe.Valor.ToString());
                                        if (objReaderLider.Read())
                                        {
                                            objNotificacao.CodigoUsuarioReceptor.Valor = objReaderLider["pessoa_codigo"].ToString();
                                        }
                                        objReaderLider = null;

                                        ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objRequisicaoServico.Equipe.Valor));
                                        objNotificacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml("A Requisição de Serviço " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico) + objRequisicaoServico.Codigo.Valor + " com a descricao '" + objRequisicaoServico.Descricao.Valor + "' foi escalado para o grupo '" + objEquipe.Descricao.Valor + "' pela central de serviços.");
                                        objEquipe = null;

                                        #endregion Equipe
                                    }
                                    else
                                    {
                                        #region Tecnico

                                        if ((objRequisicaoServico.Equipe.Valor != string.Empty) && (objRequisicaoServico.Tecnico.Valor != string.Empty))
                                        {
                                            //Tecnico                
                                            objNotificacao.CodigoUsuarioReceptor.Valor = objRequisicaoServico.Tecnico.Valor;
                                            objNotificacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml("A Requisição de Serviço " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoServico) + objRequisicaoServico.Codigo.Valor + " com a descricao '" + objRequisicaoServico.Descricao.Valor + "' foi escalado para o técnico '" + ClsUsuario.getNomeUsuario(objRequisicaoServico.Tecnico.Valor) + "' pela central de serviços.");
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
                        objRequisicaoServicoAtualizado = null;

                        ExibeMensagem(strMensagemAviso, strImagemAviso, true);
                    }
                    else
                    {
                        ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                    }

                    //gera niveis da escalacao horizontal novamente
                    WUCEscalacaoHorizontal1.geraDropDownListNivel(objRequisicaoServico.NivelAtendimento.Valor);
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoServico.NivelAtendimento.Valor, objRequisicaoServico.Equipe.Valor, objRequisicaoServico.Tecnico.Valor);

                    objRequisicaoServicoAntigo = null;
                    objRequisicaoServico = null;
                    objIdentificador = null;
                }

                if (lblIDRequisicaoServico.Text.Trim() != "")
                    wucRequisicaoServicoStatus.montaDados(Convert.ToInt32(lblIDRequisicaoServico.Text), "requisicaoservico");

                if (this.lblIDRequisicaoServico.Text.Trim() != string.Empty)
                {
                    WUCLog1.MontaDados(Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                }

                //======================================================//
                // - O que: Bloqueia campos de acordo com regra definida.
                // - Quem: Fernanda Passos.
                // - Quando: 06/03/2006 ás 13:12hs.
                //======================================================//
                BloqueiaCampos();
                //======================================================//



            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    protected void btnAplicaRequisicaoServicoPreDefinido_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRequisicaoServicoPreDefinido.SelectedValue != string.Empty)
            {
                try
                {
                    //busca os dados do RequisicaoServico selecionado.
                    ClsRequisicaoServico objRequisicaoServicoModelo = new ClsRequisicaoServico(Convert.ToInt32(ddlRequisicaoServicoPreDefinido.SelectedValue));

                    //preenche os campos de classificaçãso e cadastro da tela.
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoServicoModelo.NivelAtendimento.Valor, objRequisicaoServicoModelo.Equipe.Valor, objRequisicaoServicoModelo.Tecnico.Valor);
                    ddlOrigemRequisicaoServico.SelectedValue = objRequisicaoServicoModelo.OrigemRequisicaoServico.Valor;
                    WUCPriorizacao1.setImpacto(objRequisicaoServicoModelo.Impacto.Valor);
                    WUCPriorizacao1.setUrgencia(objRequisicaoServicoModelo.Urgencia.Valor);

                    if (objRequisicaoServicoModelo.Escalado.Valor == "S")
                    { ckEscalado.Checked = true; }
                    else
                    { ckEscalado.Checked = false; }

                    if (objRequisicaoServicoModelo.Vip.Valor == "S")
                    { ckVip.Checked = true; }
                    else
                    { ckVip.Checked = false; }

                    objRequisicaoServicoModelo = null;

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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnVincularChamado_Click(object sender, EventArgs e)
    {
        try
        {
            if (pnlFiltros.Visible)
            {
                btnVinculaChamado.Text = "Pesquisar Chamado";
                pnlFiltros.Visible = false;
                pnlResultadoPesquisaChamado.Visible = false;
            }
            else
            {
                btnVinculaChamado.Text = "Ocultar Área de Pesquisa";
                pnlFiltros.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnGravarVinculos_Click(object sender, EventArgs e)
    {
        String strCodigoRequisicaoServico = lblIDRequisicaoServico.Text;
        try
        {
            for (int i = 0; i < gvResultadoFiltroChamados.Rows.Count; i++)
            {
                GridViewRow objRow = gvResultadoFiltroChamados.Rows[i];

                if (objRow != null)
                {
                    CheckBox ckCodigoChamado = (CheckBox)objRow.FindControl("ck_CodigoChamado");
                    if (ckCodigoChamado.Checked)
                    {
                        Label lblCodigoChamadoVinculado = (Label)objRow.FindControl("lblCodigoChamado");
                        String strCodigoChamadoVinculado = lblCodigoChamadoVinculado.Text;
                        if ((strCodigoChamadoVinculado != string.Empty) && (strCodigoRequisicaoServico != String.Empty))
                        {
                            ClsRequisicaoServico.AdicionaRelacaoRequisicaoServicoChamado(strCodigoChamadoVinculado, strCodigoRequisicaoServico);
                        }
                    }
                }
            }

            ExibeMensagem("Operação efetuada com sucesso.", "images/icones/info.gif", true);
            ClsRequisicaoServico.geraGridViewChamadosVinculados(gvChamadosVinculados, strCodigoRequisicaoServico);
            btnVinculaChamado.Text = "Pesquisar Chamado";
            pnlFiltros.Visible = false;
            pnlResultadoPesquisaChamado.Visible = false;

        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    protected void btnGravaNota_Click(object sender, EventArgs e)
    {
        try
        {
            //Salva as informações do chamado
            bool bOcorreuErro = false;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;

            //Validações    
            if ((this.txtDescricaoNotaAtendimento.Text == "") && (bOcorreuErro == false))
            {
                ExibeMensagem("Por favor informe a descrição do registro.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if (bOcorreuErro == false)
            {
                String strMensagem = String.Empty;
                //cria o objeto Chamado
                ServiceDesk.Negocio.ClsNotaAtendimento objNotaAtendimento = new ServiceDesk.Negocio.ClsNotaAtendimento();

                //atribui os valores
                objNotaAtendimento.Tabela.Valor = "RequisicaoServico";
                objNotaAtendimento.DescricaoNota.Valor = ClsTexto.trocaAspaPorHtml(this.txtDescricaoNotaAtendimento.Text);
                objNotaAtendimento.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                objNotaAtendimento.IdentificadorTabela.Valor = lblIDRequisicaoServico.Text;
                objNotaAtendimento.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();

                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = objNotaAtendimento.Atributos.NomeTabela;
                objNotaAtendimento.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objNotaAtendimento.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                    ExibeMensagem("Operação realizada com sucesso.", "images/icones/info.gif", true);
                    txtDescricaoNotaAtendimento.Text = "";
                    ClsRequisicaoServico.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDRequisicaoServico.Text);
                }
                else
                {
                    ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                }

                objNotaAtendimento = null;
                objIdentificador = null;
            }

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    protected void gvChamadosVinculados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                String strCodigoChamado = string.Empty;
                String strCodigoRequisicaoServico = string.Empty;
                GridViewRow objRow = gvChamadosVinculados.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoChamado");
                    strCodigoChamado = lblCodigo.Text;
                    strCodigoRequisicaoServico = lblIDRequisicaoServico.Text;

                    if ((strCodigoChamado != string.Empty) && (strCodigoRequisicaoServico != String.Empty))
                    {
                        try
                        {
                            ClsRequisicaoServico.RemoveRelacaoRequisicaoServicoChamado(strCodigoRequisicaoServico, strCodigoChamado);
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsRequisicaoServico.geraGridViewChamadosVinculados(gvChamadosVinculados, strCodigoRequisicaoServico);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    protected void gvNotaAtendimento_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                String strCodigoNotaAtendimento = string.Empty;
                String strCodigoRequisicaoServico = string.Empty;
                GridViewRow objRow = gvNotaAtendimento.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoNotaAtendimento");
                    strCodigoNotaAtendimento = lblCodigo.Text;
                    strCodigoRequisicaoServico = lblIDRequisicaoServico.Text;

                    if ((strCodigoNotaAtendimento != string.Empty) && (strCodigoRequisicaoServico != String.Empty))
                    {
                        try
                        {
                            ClsNotaAtendimento objNotaAtendimento = new ClsNotaAtendimento();
                            objNotaAtendimento.Codigo.Valor = strCodigoNotaAtendimento;
                            objNotaAtendimento.exclui();
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                            objNotaAtendimento = null;
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsRequisicaoServico.geraGridViewNotaAtendimento(gvNotaAtendimento, strCodigoRequisicaoServico);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    protected void gvNotaAtendimento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
                    btnExcluir.Attributes.Add("onclick", "return verifica();");
                }
            }

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnFiltrarChamados_Click(object sender, EventArgs e)
    {
        try
        {
            String strSql = string.Empty;
            String strMensagem = string.Empty;
            bool bPrimeiroCampo = true;

            pnlResultadoPesquisaChamado.Visible = true;

            //monta a query de acordo com os filtros.
            strSql = "SELECT C.chamado_codigo, C.descricao, C.data_inclusao, C.pessoa_codigo_proprietario, C.pessoa_codigo_solicitante ";
            strSql += "FROM chamado C ";
            if (ddlServicoFiltro.SelectedValue != string.Empty)
            {
                strSql += ", ChamadoIC CIC ";
            }
            strSql += "WHERE ";

            if (txtCodigoChamadoFiltro.Text.Trim() != string.Empty)
            {
                strSql += "C.chamado_codigo =" + ClsTexto.trocaAspaPorHtml(txtCodigoChamadoFiltro.Text.Trim());
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

                strSql += " C.chamado_codigo = CIC.chamado_codigo AND CIC.ic_codigo = " + ddlServicoFiltro.SelectedValue;
            }

            if (lblIDRequisicaoServico.Text != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += " C.chamado_codigo NOT IN (SELECT chamado_codigo from RequisicaoServicoChamado WHERE RequisicaoServico_codigo =" + lblIDRequisicaoServico.Text + ") ";
            }
            strSql += " ORDER BY C.chamado_codigo";

            if (!ClsChamado.geraGridViewQuery(gvResultadoFiltroChamados, strSql, out strMensagem))
            {
                ExibeMensagem(strMensagem, "images/icones/aviso.gif", true);
            }

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }


    #region Envia para base de conhecimento.
    /// <summary>
    /// Envia registro de RequisicaoServico para base de conhecimento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnviaParaBaseConhecimento_Click(object sender, EventArgs e)
    {
        try
        {
            string strMensagem = string.Empty;
            string strCodigoRede = string.Empty;
            divMensagem.Visible = false;
            if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
            {
                string codUsuario = ClsUsuario.getCodigoUsuario().ToString();
                ClsConhecimentoProcesso.EnviaParaBaseConhecimento("RequisicaoServico", lblIDRequisicaoServico.Text.Trim(), codUsuario, codUsuario, out strMensagem);
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    protected void lbkMudancas_Click(object sender, EventArgs e)
    {
        try
        {
            mvwAbas.ActiveViewIndex = 4;

            if (lblIDRequisicaoServico.Text.Trim() != string.Empty)
            {
                WUCSolucaoFiltro1.PreencheCampo("RequisicaoServico", Convert.ToInt32(lblIDRequisicaoServico.Text.Trim()));
                WUCSolucaoFiltro1.Filtrar();
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    protected void gvChamadosVinculados_RowDataBound(object sender, GridViewRowEventArgs e)
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Métodos
    #region Abilita e Dasabilita as abas (LinkButton)
    /// <summary>
    /// Abilita e Dasabilita as abas (LinkButton)
    /// </summary>
    /// <param name="bolAbilitar">Recebe true ou false. Se é para desabilitar ou não.</param>
    public void AbilitaDesabilitaAbas(bool bolAbilitar)
    {
        try
        {
            //====================================================//
            // - O que: Abilta e desabilita as abas da tela.
            // - Quem: Fernanda Passos.
            // - Quando: 0704/2006
            //====================================================//
            tblAbas.Visible = true;
            mvwAbas.Visible = true;
            lkbSolicitante.Enabled = true;

            if (bolAbilitar == true)
            {
                //todas as abas ativas 
                lkbChamados.Enabled = true;
                lbkAnexos.Enabled = true;
                lkbItensConfirguracao.Enabled = true;
                lbkLog.Enabled = true;
                lkbHistorico.Enabled = true;
                lkbSolucao.Enabled = true;
                lkbAprovador.Enabled = true;
            }
            else
            {
                //Só aba solicitante ativa
                lkbChamados.Enabled = false;
                lbkAnexos.Enabled = false;
                lkbItensConfirguracao.Enabled = false;
                lbkLog.Enabled = false;
                lkbHistorico.Enabled = false;
                lkbSolucao.Enabled = false;
                lkbAprovador.Enabled = false;
            }
            //====================================================//

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
    #endregion

    #region Eventos

    #endregion
}