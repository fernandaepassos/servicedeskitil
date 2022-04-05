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

public partial class WUCRequisicaoMudanca : System.Web.UI.UserControl
{
    #region Declaracoes Publicas
    public string strFormatoDataInclusao = ClsParametro.DataInclusao;
    #endregion

    #region Propriedades

    private int intCodigoRequisicaoMudanca;
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
        // - O que: Bloqueia os campos impacto, urgencia, origem se o usuário
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
                ddlOrigemRequisicaoMudanca.Enabled = false;
            }
            else
            {
                WUCPriorizacao1.BloqueiaCampos(false, false, true);
                ddlOrigemRequisicaoMudanca.Enabled = true;
            }
        }
    }
    #endregion

    /// <summary>
    /// Abre um RequisicaoMudanca existente para edição
    /// </summary>
    /// <param name="CodigoRequisicaoMudanca">Código do RequisicaoMudanca desejado</param>
    public void EditaRequisicaoMudanca(int CodigoRequisicaoMudanca)
    {
        try
        {
            if (CodigoRequisicaoMudanca != 0)
            {
                //busca dados do RequisicaoMudanca e preenche as abas
                tblAbas.Visible = true;
                mvwAbas.Visible = true;
                lblIDRequisicaoMudanca.Text = CodigoRequisicaoMudanca.ToString();
                wucRequisicaoMudancaStatus.montaDados(Convert.ToInt32(lblIDRequisicaoMudanca.Text), "REQUISICAOMUDANCA");
            }
        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Cria um novo RequisicaoMudanca herdando as informações do chamado informado
    /// </summary>
    /// <param name="CodigoChamado">Codigo do Chamado</param>
    public void CriaRequisicaoMudancaHerdandoChamado(int CodigoChamado)
    {
        try
        {
            intCodigoChamado = CodigoChamado;
            if (intCodigoChamado != 0)
            {
                //Cria o novo RequisicaoMudanca
                string strCodigoRequisicaoMudancaCriado = string.Empty;
                if (ClsRequisicaoMudanca.criaRequisicaoMudancaBaseadoChamado(intCodigoChamado.ToString(), out strCodigoRequisicaoMudancaCriado))
                {
                    ExibeMensagem("Operação efetuada com sucesso.", "images/icones/info.gif", true);
                    intCodigoRequisicaoMudanca = Convert.ToInt32(strCodigoRequisicaoMudancaCriado);

                    //busca dados do RequisicaoMudanca criado e preenche as abas
                    tblAbas.Visible = true;
                    mvwAbas.Visible = true;
                    PreencheDadosRequisicaoMudanca(intCodigoRequisicaoMudanca);
                }
                else
                {
                    ExibeMensagem("Não foi possível criar uma Requisicao Mudanca vinculado ao chamado.", "images/icones/aviso.gif", true);
                }
            }
        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Fornece um meio de acesso ao painel de mensagem
    /// </summary>
    /// <param name="Mensagem">Mensagem a ser exibida na tela</param>
    /// <param name="Imagem">Nome da imagem do ícone do painel</param>
    /// <param name="Ativo">true para Exibir, false para Ocultar</param>
    /// <example>ExibeMensagem("teste","images/icones/aviso.gif",true)</example>
    private void ExibeMensagem(string Mensagem, string Imagem, bool Ativo)
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
    /// Preenche os dados do RequisicaoMudanca informado
    /// </summary>
    /// <param name="CodigoRequisicaoMudanca">Codigo do RequisicaoMudanca</param>
    private void PreencheDadosRequisicaoMudanca(int CodigoRequisicaoMudanca)
    {
        try
        {
            string strDataMinimaSistema = ClsParametro.DataMinimaSistema;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;
            ClsRequisicaoMudanca objRequisicaoMudanca = new ClsRequisicaoMudanca(CodigoRequisicaoMudanca);
            if (objRequisicaoMudanca != null)
            {
                //prenche os dados do RequisicaoMudanca, marca os itens nas caixa e listas, etc.
                #region Remove o RequisicaoMudanca atual da lista de modelos se ele for modelo
                if (lblIDRequisicaoMudanca.Text != string.Empty)
                {
                    ListItem itemAtual = ddlRequisicaoMudancaPreDefinido.Items.FindByValue(lblIDRequisicaoMudanca.Text);
                    ddlRequisicaoMudancaPreDefinido.Items.Remove(itemAtual);
                    itemAtual = null;
                }
                #endregion Remove o RequisicaoMudanca atual da lista de modelos se ele for modelo

                #region Preenche os dados do RequisicaoMudanca
                try
                {
                    lblIDRequisicaoMudanca.Text = objRequisicaoMudanca.Codigo.Valor;
                    txtDescricao.Text = ClsTexto.trocaHtmlPorAspa(objRequisicaoMudanca.Descricao.Valor);
                    if (objRequisicaoMudanca.PessoaCodigoSolicitante.Valor != string.Empty)
                    {
                        WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(objRequisicaoMudanca.PessoaCodigoSolicitante.Valor));
                    }
                    if (objRequisicaoMudanca.PessoaCodigoInclusor.Valor != string.Empty)
                    {
                        txtAtendente.Text = ClsUsuario.getNomeUsuario(objRequisicaoMudanca.PessoaCodigoInclusor.Valor);
                    }

                    WUCPriorizacao1.setImpacto(objRequisicaoMudanca.Impacto.Valor);
                    WUCPriorizacao1.setUrgencia(objRequisicaoMudanca.Urgencia.Valor);

                    if (objRequisicaoMudanca.OrigemRequisicaoMudanca.Valor != string.Empty)
                    {
                        ddlOrigemRequisicaoMudanca.SelectedValue = objRequisicaoMudanca.OrigemRequisicaoMudanca.Valor;
                    }
                    if ((objRequisicaoMudanca.Escalado.Valor != string.Empty) && (objRequisicaoMudanca.Escalado.Valor == "S"))
                    {
                        ckEscalado.Checked = true;
                    }

                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoMudanca.NivelAtendimento.Valor, objRequisicaoMudanca.Equipe.Valor, objRequisicaoMudanca.Tecnico.Valor);
                    WUCEscalacaoHorizontal1.strTabela = "RequisicaoMudanca";
                    if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty) WUCEscalacaoHorizontal1.intIdentificadorTabela = Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim());


                    if ((objRequisicaoMudanca.Vip.Valor != string.Empty) && (objRequisicaoMudanca.Vip.Valor == "S"))
                    { ckVip.Checked = true; }
                    else
                    { ckVip.Checked = false; }

                    if ((objRequisicaoMudanca.Modelo.Valor != string.Empty) && (objRequisicaoMudanca.Modelo.Valor.ToUpper() == "S"))
                    { ckModelo.Checked = true; }
                    else
                    { ckModelo.Checked = false; }

                }
                catch
                {
                    ExibeMensagem("Ocorreu um erro ao preencher os dados da Requisicao Mudanca.", "images/icones/aviso.gif", true);
                }
                #endregion Preenche Dados RequisicaoMudanca

            }
            objRequisicaoMudanca = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }


    //Page Load  
    protected void Page_Load(object sender, EventArgs e)
    {
        int intRequisicaoCodigo = 0;

        try
        {

            if (Request.QueryString["REQUISICAOMUDANCA"] != null)
            {
                intRequisicaoCodigo = Convert.ToInt32(Request.QueryString["REQUISICAOMUDANCA"].ToString());
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
                if (ClsRequisicaoMudanca.GetCodigoProprietario(intRequisicaoCodigo) != string.Empty) btnSalvar.Enabled = ClsUsuario.verificaAcessoPapel(ClsUsuario.getCodigoUsuario(), intRequisicaoCodigo, 2, "REQUISICAOMUDANCA");
                //=====================================================================//
            }

            pnlResultadoPesquisaChamado.Visible = false;

            //Esconde a mensagem de erro
            ExibeMensagem("", "", false);

       //     String strCodigoUsuario = ClsUsuario.getCodigoUsuario(strPessoaCodigo);

            if (!Page.IsPostBack)
            {
                mvwAbas.ActiveViewIndex = 0;
                string strMensagem = string.Empty;

                #region Preenche combos, listas etc

                //Coloca o nome do Atendente
                txtAtendente.Text = ClsUsuario.getNomeUsuario();
                //dropdown lists
                ClsTipoOrigemChamado.geraDropDownList(ddlOrigemRequisicaoMudanca);
                WUCPriorizacao1.geraDropDownListImpactoUrgencia();

                if (lblIDRequisicaoMudanca.Text.Trim() == string.Empty)
                {
                    //Se está incluindo chamado, so exibe o 1º nivel para selecao
                    WUCEscalacaoHorizontal1.geraDropDownListNivel("0");
                }
                else
                {
                    //gera os niveis possiveis baseado no nivel do chamado
                    ClsRequisicaoMudanca objRequisicaoNivel = new ClsRequisicaoMudanca(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                    WUCEscalacaoHorizontal1.geraDropDownListNivel(objRequisicaoNivel.NivelAtendimento.Valor);
                    objRequisicaoNivel = null;
                }

                ClsRequisicaoMudanca.geraDropDownListRequisicaoMudancaPreDefinido(ddlRequisicaoMudancaPreDefinido);

                //Campos de filtro da aba de chamados vinculados
                //Combo Solicitantes
                ClsUsuario.geraDropDownList(ddlSolicitanteFiltro, "--");
                //Combo Serviços
                ClsItemConfiguracao.geraDropDownList(ddlMudancaFiltro);

                #endregion

                if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                {
                    PreencheDadosRequisicaoMudanca(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
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
            if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty) AbilitaDesabilitaAbas(true); else AbilitaDesabilitaAbas(false);
            //====================================================//

            if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
            {
                lblPrefixo.Text = ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoRequisicaoMudanca);

                //Log
                WUCLog1.MontaDados(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

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
                        //if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        //{
                        //  ClsRequisicaoMudanca objRequisicaoMudanca = new ClsRequisicaoMudanca(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                        //  WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(objRequisicaoMudanca.PessoaCodigoSolicitante.Valor));
                        //  objRequisicaoMudanca = null;
                        //}
                        break;
                    }
                case 1:
                    {
                        //Chamados  
                        if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        {
                            ClsRequisicaoMudanca.geraGridViewChamadosVinculados(gvChamadosVinculados, lblIDRequisicaoMudanca.Text.Trim());
                        }
                        break;
                    }
                case 2:
                    {
                        //ICS
                        if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        {
                            WUCItemConfiguracaoTreeView1.CarregaIC(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                        }
                        break;
                    }
                case 3:
                    {
                        //Solucao
                        if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        {
                            WUCSolucaoFiltro1.PreencheCampo("REQUISICAOMUDANCA", Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                            WUCSolucaoFiltro1.Filtrar();
                        }
                        break;
                    }

                case 4:
                    {
                        //Anexos
                        if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        {
                            WUCAnexo1.CarregaAnexos(lblIDRequisicaoMudanca.Text.Trim(), "REQUISICAOMUDANCA");
                        }
                        break;
                    }
                case 5:
                    {
                        //Log
                        if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        {
                            WUCLog1.MontaDados(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                        }
                        break;
                    }
                case 6:
                    {
                        //Notas de Atendimento
                        if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        {
                            ClsRequisicaoMudanca.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDRequisicaoMudanca.Text.Trim());
                        }
                        break;
                    }
                case 7:
                    {
                        //Aprovadores
                        if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                        {
                            WUCAprovador1.PreencheCampos("RequisicaoMudanca", Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                        }
                        break;
                    }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    #endregion

    //Eventos dos Elementos da Tela
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            //Salva as informações do RequisicaoMudanca
            bool bOcorreuErro = false;
            string strCodigoChamado = string.Empty;
            string strCodigoRequisicaoMudanca = string.Empty;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;

            #region Validações
            if (WUCUsuario1.PessoaCodigo() == 0)
            {
                ExibeMensagem("Por favor selecione o solicitante.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }

            if (WUCPriorizacao1.getImpacto().ToString() == "")
            {
                ExibeMensagem("Por favor, informe o impacto.", "images/icones/aviso.gif", true);
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
                ExibeMensagem("Por favor informe a descrição.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }
            #endregion

            if (bOcorreuErro == false)
            {
                string strMensagem = string.Empty;
                //cria o objeto RequisicaoMudanca
                ClsRequisicaoMudanca objRequisicaoMudanca = new ClsRequisicaoMudanca();

                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = objRequisicaoMudanca.Atributos.NomeTabela;

                //atribui os valores
                if (lblIDRequisicaoMudanca.Text == "") //novo
                {
                    objRequisicaoMudanca.PessoaCodigoInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();
                    objRequisicaoMudanca.PessoaCodigoSolicitante.Valor = WUCUsuario1.PessoaCodigo().ToString();
                    objRequisicaoMudanca.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                    objRequisicaoMudanca.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
                    objRequisicaoMudanca.Status.Valor = wucRequisicaoMudancaStatus.primeiroStatus("REQUISICAOMUDANCA").ToString();

                    if (ckEscalado.Checked)
                    { objRequisicaoMudanca.Escalado.Valor = "S"; }
                    else
                    { objRequisicaoMudanca.Escalado.Valor = "N"; }
                    objRequisicaoMudanca.OrigemRequisicaoMudanca.Valor = ddlOrigemRequisicaoMudanca.SelectedValue;

                    // ************************************************************************
                    // Alteração Sylvio- 16-02-2006
                    // ************************************************************************
                    if (WUCEscalacaoHorizontal1.getNivel().ToString().Trim() != string.Empty)
                    {
                        objRequisicaoMudanca.Equipe.Valor = WUCEscalacaoHorizontal1.getEquipe().ToString();
                        //objIncidente.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                        objRequisicaoMudanca.NivelAtendimento.Valor = WUCEscalacaoHorizontal1.getNivel().ToString();
                        //Se não foi selecionado tecnico. O lider de equipe vira técnico
                        if ((WUCEscalacaoHorizontal1.getNivel().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getEquipe().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getTecnico().ToString() == string.Empty))
                        {
                            objRequisicaoMudanca.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(WUCEscalacaoHorizontal1.getEquipe().ToString());
                        }
                        else
                        {
                            objRequisicaoMudanca.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                        }
                    }
                    else
                    {
                        //Nivel e Equipe de Atendimento padrão (escalacao horizontal)
                        objRequisicaoMudanca.NivelAtendimento.Valor = ClsParametro.NivelAtendimentoPadrao;
                        objRequisicaoMudanca.Equipe.Valor = ClsParametro.EquipeAtendimentoPadrao;
                        objRequisicaoMudanca.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(ClsParametro.EquipeAtendimentoPadrao);
                    }
                    // ************************************************************************

                    objRequisicaoMudanca.Impacto.Valor = WUCPriorizacao1.getImpacto().ToString();
                    objRequisicaoMudanca.Urgencia.Valor = WUCPriorizacao1.getUrgencia().ToString();
                    objRequisicaoMudanca.Prioridade.Valor = WUCPriorizacao1.getPrioridade().ToString();

                    if (ckVip.Checked)
                    { objRequisicaoMudanca.Vip.Valor = "S"; }
                    else
                    { objRequisicaoMudanca.Vip.Valor = "N"; }

                    if (ckModelo.Checked)
                    { objRequisicaoMudanca.Modelo.Valor = "S"; }
                    else
                    { objRequisicaoMudanca.Modelo.Valor = "N"; }

                    objRequisicaoMudanca.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                    //salva novo
                    if (objRequisicaoMudanca.insere(out strMensagem))
                    {
                        objIdentificador.atualizaValor();

                        #region Grava Escalacao Horizontal e envia e-mail.

                        ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal();
                        string strCodigoEscalacao = objEscalacaoHorizontal.insereEscalacao(objRequisicaoMudanca.Atributos.NomeTabela, objRequisicaoMudanca.Codigo.Valor, objRequisicaoMudanca.NivelAtendimento.Valor, objRequisicaoMudanca.Equipe.Valor, objRequisicaoMudanca.Tecnico.Valor, objRequisicaoMudanca.PessoaCodigoAlterador.Valor).ToString();
                        if (strCodigoEscalacao != string.Empty)
                        {

                            #region Envia notificacao Equipe ou Tecnico
                            if ((objRequisicaoMudanca.Equipe.Valor != string.Empty) || (objRequisicaoMudanca.Tecnico.Valor != string.Empty))
                            {
                                ClsNotificacao objNotificacao = new ClsNotificacao();
                                objNotificacao.Tabela.Valor = "EscalacaoHorizontal";
                                objNotificacao.DtInclusao.Valor = System.DateTime.Now.ToString(ClsParametro.DataInclusao);
                                objNotificacao.IdentificadorTabela.Valor = objEscalacaoHorizontal.Codigo.Valor;
                                objNotificacao.CodigoUsuarioEmissor.Valor = objRequisicaoMudanca.PessoaCodigoInclusor.Valor;

                                if ((objRequisicaoMudanca.Equipe.Valor != string.Empty) && (objRequisicaoMudanca.Tecnico.Valor == string.Empty))
                                {
                                    #region Equipe

                                    //lider
                                    SqlDataReader objReaderLider = ClsEquipe.getlLiderEquipe(objRequisicaoMudanca.Equipe.Valor.ToString());
                                    if (objReaderLider.Read())
                                    {
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objReaderLider["pessoa_codigo"].ToString();
                                    }
                                    objReaderLider = null;

                                    ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objRequisicaoMudanca.Equipe.Valor));
                                    objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("A Requisição de Mudança " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoRequisicaoMudanca) + objRequisicaoMudanca.Codigo.Valor + " com a descricao '" + objRequisicaoMudanca.Descricao.Valor + "' foi escalado para o grupo '" + objEquipe.Descricao.Valor + "' pela central de serviços.");
                                    objEquipe = null;

                                    #endregion Equipe
                                }
                                else
                                {
                                    #region Tecnico

                                    if ((objRequisicaoMudanca.Equipe.Valor != string.Empty) && (objRequisicaoMudanca.Tecnico.Valor != string.Empty))
                                    {
                                        //Tecnico                
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objRequisicaoMudanca.Tecnico.Valor;
                                        objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("A Requisição de Mudança " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoRequisicaoMudanca) + objRequisicaoMudanca.Codigo.Valor + " com a descricao '" + objRequisicaoMudanca.Descricao.Valor + "' foi escalado para o técnico '" + ClsUsuario.getNomeUsuario(objRequisicaoMudanca.Tecnico.Valor) + "' pela central de serviços.");
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
                        SServiceDesk.Negocio.ClsWorkFlow.gravaLog(Convert.ToInt32(objRequisicaoMudanca.Codigo.Valor), objRequisicaoMudanca.Atributos.NomeTabela, "0", objRequisicaoMudanca.Status.Valor);
                        #endregion

                        //gera niveis da escalacao horizontal novamente
                        WUCEscalacaoHorizontal1.geraDropDownListNivel(objRequisicaoMudanca.NivelAtendimento.Valor);

                        ExibeMensagem(strMensagem, "images/icones/info.gif", true);

                        lblIDRequisicaoMudanca.Text = objRequisicaoMudanca.Codigo.Valor.ToString();
                        WUCAnexo1.salvaDocumento(true, lblIDRequisicaoMudanca.Text, "REQUISICAOMUDANCA");

                        PreencheDadosRequisicaoMudanca(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));

                        //====================================================//
                        // - O que: Abilta e desabilita as abas da tela.
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
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoMudanca.NivelAtendimento.Valor, objRequisicaoMudanca.Equipe.Valor, objRequisicaoMudanca.Tecnico.Valor);

                }
                else //alterando
                {
                    objRequisicaoMudanca.Codigo.Valor = lblIDRequisicaoMudanca.Text;
                    objRequisicaoMudanca.DataAlteracao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                    objRequisicaoMudanca.PessoaCodigoAlterador.Valor = ClsUsuario.getCodigoUsuario().ToString();
                    objRequisicaoMudanca.PessoaCodigoSolicitante.Valor = WUCUsuario1.PessoaCodigo().ToString();
                    objRequisicaoMudanca.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

                    if (ckEscalado.Checked)
                    { objRequisicaoMudanca.Escalado.Valor = "S"; }
                    else
                    { objRequisicaoMudanca.Escalado.Valor = "N"; }

                    objRequisicaoMudanca.OrigemRequisicaoMudanca.Valor = ddlOrigemRequisicaoMudanca.SelectedValue;

                    // ************************************************************************
                    // Alteração Gleison - 14-01-2006
                    // ************************************************************************
                    objRequisicaoMudanca.Equipe.Valor = WUCEscalacaoHorizontal1.getEquipe().ToString();
                    //objRequisicaoMudanca.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                    objRequisicaoMudanca.NivelAtendimento.Valor = WUCEscalacaoHorizontal1.getNivel().ToString();
                    //Se não foi selecionado tecnico. O lider de equipe vira técnico
                    if ((WUCEscalacaoHorizontal1.getNivel().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getEquipe().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getTecnico().ToString() == string.Empty))
                    {
                        objRequisicaoMudanca.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(WUCEscalacaoHorizontal1.getEquipe().ToString());
                    }
                    else
                    {
                        objRequisicaoMudanca.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                    }
                    // ************************************************************************

                    objRequisicaoMudanca.Impacto.Valor = WUCPriorizacao1.getImpacto().ToString();
                    objRequisicaoMudanca.Urgencia.Valor = WUCPriorizacao1.getUrgencia().ToString();
                    objRequisicaoMudanca.Prioridade.Valor = WUCPriorizacao1.getPrioridade().ToString();

                    if (ckVip.Checked)
                    { objRequisicaoMudanca.Vip.Valor = "S"; }
                    else
                    { objRequisicaoMudanca.Vip.Valor = "N"; }

                    if (ckModelo.Checked)
                    { objRequisicaoMudanca.Modelo.Valor = "S"; }
                    else
                    { objRequisicaoMudanca.Modelo.Valor = "N"; }


                    //Instancia um obj antes da alteração
                    //para utilizar na gravacao de log.
                    ClsRequisicaoMudanca objRequisicaoMudancaAntigo = new ClsRequisicaoMudanca(Convert.ToInt32(objRequisicaoMudanca.Codigo.Valor));

                    //salva alteracao
                    if (objRequisicaoMudanca.altera(out strMensagem))
                    {
                        string strMensagemAviso = "Operação realizada com sucesso.";
                        string strImagemAviso = "images/icones/info.gif";

                        wucRequisicaoMudancaStatus.salvaStatus();
                        WUCAnexo1.salvaDocumento(true, lblIDRequisicaoMudanca.Text, "REQUISICAOMUDANCA");

                        //Instancia um obj APÓS da alteração
                        //para utilizar na gravacao de log.
                        ClsRequisicaoMudanca objRequisicaoMudancaAtualizado = new ClsRequisicaoMudanca(Convert.ToInt32(objRequisicaoMudanca.Codigo.Valor));
                        //Marca o campo de data de alteracao para nao ser verificado
                        objRequisicaoMudancaAtualizado.DataAlteracao.VerificaAlteracao = false;

                        //grava log de alteração
                        ClsLog.insereLog(ClsLog.enumTipoLog.UPDATE, objRequisicaoMudanca.PessoaCodigoAlterador.Valor, "RequisicaoMudanca", objRequisicaoMudanca.Codigo.Valor, objRequisicaoMudancaAtualizado.Atributos, objRequisicaoMudancaAntigo.Atributos);

                        PreencheDadosRequisicaoMudanca(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));

                        //Se houve mudança na escalação horizontal. grava escalacao e 
                        //manda mail
                        if ((objRequisicaoMudancaAntigo.NivelAtendimento.Valor != objRequisicaoMudancaAtualizado.NivelAtendimento.Valor) || (objRequisicaoMudancaAntigo.Equipe.Valor != objRequisicaoMudancaAtualizado.Equipe.Valor) || (objRequisicaoMudancaAntigo.Tecnico.Valor != objRequisicaoMudancaAtualizado.Tecnico.Valor))
                        {
                            #region Grava Escalacao Horizontal e envia e-mail.
                            ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal();
                            string strCodigoEscalacao = objEscalacaoHorizontal.insereEscalacao(objRequisicaoMudanca.Atributos.NomeTabela, objRequisicaoMudanca.Codigo.Valor, objRequisicaoMudanca.NivelAtendimento.Valor, objRequisicaoMudanca.Equipe.Valor, objRequisicaoMudanca.Tecnico.Valor, objRequisicaoMudanca.PessoaCodigoAlterador.Valor).ToString();
                            if (strCodigoEscalacao != string.Empty)
                            {

                                #region Envia notificacao Equipe ou Tecnico
                                if ((objRequisicaoMudanca.Equipe.Valor != string.Empty) || (objRequisicaoMudanca.Tecnico.Valor != string.Empty))
                                {
                                    ClsNotificacao objNotificacao = new ClsNotificacao();
                                    objNotificacao.Tabela.Valor = "EscalacaoHorizontal";
                                    objNotificacao.DtInclusao.Valor = System.DateTime.Now.ToString(ClsParametro.DataInclusao);
                                    objNotificacao.IdentificadorTabela.Valor = objEscalacaoHorizontal.Codigo.Valor;
                                    objNotificacao.CodigoUsuarioEmissor.Valor = objRequisicaoMudancaAntigo.PessoaCodigoInclusor.Valor;

                                    if ((objRequisicaoMudanca.Equipe.Valor != string.Empty) && (objRequisicaoMudanca.Tecnico.Valor == string.Empty))
                                    {
                                        #region Equipe

                                        //lider
                                        SqlDataReader objReaderLider = ClsEquipe.getlLiderEquipe(objRequisicaoMudanca.Equipe.Valor.ToString());
                                        if (objReaderLider.Read())
                                        {
                                            objNotificacao.CodigoUsuarioReceptor.Valor = objReaderLider["pessoa_codigo"].ToString();
                                        }
                                        objReaderLider = null;

                                        ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objRequisicaoMudanca.Equipe.Valor));
                                        objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("A Requisição de Mudança " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoRequisicaoMudanca) + objRequisicaoMudanca.Codigo.Valor + " com a descricao '" + objRequisicaoMudanca.Descricao.Valor + "' foi escalado para o grupo '" + objEquipe.Descricao.Valor + "' pela central de serviços.");
                                        objEquipe = null;

                                        #endregion Equipe
                                    }
                                    else
                                    {
                                        #region Tecnico

                                        if ((objRequisicaoMudanca.Equipe.Valor != string.Empty) && (objRequisicaoMudanca.Tecnico.Valor != string.Empty))
                                        {
                                            //Tecnico                
                                            objNotificacao.CodigoUsuarioReceptor.Valor = objRequisicaoMudanca.Tecnico.Valor;
                                            objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("A Requisição de Mudança " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoRequisicaoMudanca) + objRequisicaoMudanca.Codigo.Valor + " com a descricao '" + objRequisicaoMudanca.Descricao.Valor + "' foi escalado para o técnico '" + ClsUsuario.getNomeUsuario(objRequisicaoMudanca.Tecnico.Valor) + "' pela central de serviços.");
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
                        objRequisicaoMudancaAtualizado = null;

                        ExibeMensagem(strMensagemAviso, strImagemAviso, true);
                    }
                    else
                    {
                        ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                    }

                    //gera niveis da escalacao horizontal novamente
                    WUCEscalacaoHorizontal1.geraDropDownListNivel(objRequisicaoMudanca.NivelAtendimento.Valor);
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoMudanca.NivelAtendimento.Valor, objRequisicaoMudanca.Equipe.Valor, objRequisicaoMudanca.Tecnico.Valor);

                    objRequisicaoMudanca = null;
                    objRequisicaoMudancaAntigo = null;
                    objIdentificador = null;
                }

                if (lblIDRequisicaoMudanca.Text.Trim() != "")
                    wucRequisicaoMudancaStatus.montaDados(Convert.ToInt32(lblIDRequisicaoMudanca.Text), "REQUISICAOMUDANCA");

                if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
                {
                    WUCLog1.MontaDados(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                }

                //======================================================//
                // - O que: Bloqueia campos de acordo com regra definida.
                // - Quem: Fernanda Passos.
                // - Quando: 06/03/2006 ás 13:12hs.
                //======================================================//
                BloqueiaCampos();
                //======================================================//

                PreencheDadosRequisicaoMudanca(Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }

    protected void btnAplicaRequisicaoMudancaPreDefinido_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRequisicaoMudancaPreDefinido.SelectedValue != string.Empty)
            {
                try
                {
                    //busca os dados do RequisicaoMudanca selecionado.
                    ClsRequisicaoMudanca objRequisicaoMudancaModelo = new ClsRequisicaoMudanca(Convert.ToInt32(ddlRequisicaoMudancaPreDefinido.SelectedValue));

                    //preenche os campos de classificaçãso e cadastro da tela.
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objRequisicaoMudancaModelo.NivelAtendimento.Valor, objRequisicaoMudancaModelo.Equipe.Valor, objRequisicaoMudancaModelo.Tecnico.Valor);
                    ddlOrigemRequisicaoMudanca.SelectedValue = objRequisicaoMudancaModelo.OrigemRequisicaoMudanca.Valor;
                    WUCPriorizacao1.setImpacto(objRequisicaoMudancaModelo.Impacto.Valor);
                    WUCPriorizacao1.setUrgencia(objRequisicaoMudancaModelo.Urgencia.Valor);

                    if (objRequisicaoMudancaModelo.Escalado.Valor == "S")
                    { ckEscalado.Checked = true; }
                    else
                    { ckEscalado.Checked = false; }

                    if (objRequisicaoMudancaModelo.Vip.Valor == "S")
                    { ckVip.Checked = true; }
                    else
                    { ckVip.Checked = false; }

                    objRequisicaoMudancaModelo = null;

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
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
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
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void btnGravarVinculos_Click(object sender, EventArgs e)
    {
        string strCodigoRequisicaoMudanca = lblIDRequisicaoMudanca.Text;
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
                        string strCodigoChamadoVinculado = lblCodigoChamadoVinculado.Text;
                        if ((strCodigoChamadoVinculado != string.Empty) && (strCodigoRequisicaoMudanca != string.Empty))
                        {
                            ClsRequisicaoMudanca.AdicionaRelacaoRequisicaoMudancaChamado(strCodigoChamadoVinculado, strCodigoRequisicaoMudanca);
                        }
                    }
                }
            }

            ExibeMensagem("Operação efetuada com sucesso.", "images/icones/info.gif", true);
            ClsRequisicaoMudanca.geraGridViewChamadosVinculados(gvChamadosVinculados, strCodigoRequisicaoMudanca);
            btnVinculaChamado.Text = "Pesquisar Chamado";
            pnlFiltros.Visible = false;
            pnlResultadoPesquisaChamado.Visible = false;

        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
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
                objNotaAtendimento.Tabela.Valor = "REQUISICAOMUDANCA";
                objNotaAtendimento.DescricaoNota.Valor = ClsTexto.trocaAspaPorHtml(txtDescricaoNotaAtendimento.Text);
                objNotaAtendimento.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                objNotaAtendimento.IdentificadorTabela.Valor = lblIDRequisicaoMudanca.Text;
                objNotaAtendimento.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();

                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = objNotaAtendimento.Atributos.NomeTabela;
                objNotaAtendimento.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objNotaAtendimento.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                    ExibeMensagem("Operação realizada com sucesso.", "images/icones/info.gif", true);
                    txtDescricaoNotaAtendimento.Text = "";
                    ClsRequisicaoMudanca.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDRequisicaoMudanca.Text);
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

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }

    protected void gvChamadosVinculados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoChamado = string.Empty;
                string strCodigoRequisicaoMudanca = string.Empty;
                GridViewRow objRow = gvChamadosVinculados.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoChamado");
                    strCodigoChamado = lblCodigo.Text;
                    strCodigoRequisicaoMudanca = lblIDRequisicaoMudanca.Text;

                    if ((strCodigoChamado != string.Empty) && (strCodigoRequisicaoMudanca != string.Empty))
                    {
                        try
                        {
                            ClsRequisicaoMudanca.RemoveRelacaoRequisicaoMudancaChamado(strCodigoRequisicaoMudanca, strCodigoChamado);
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsRequisicaoMudanca.geraGridViewChamadosVinculados(gvChamadosVinculados, strCodigoRequisicaoMudanca);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }

    protected void gvNotaAtendimento_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoNotaAtendimento = string.Empty;
                string strCodigoRequisicaoMudanca = string.Empty;
                GridViewRow objRow = gvNotaAtendimento.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoNotaAtendimento");
                    strCodigoNotaAtendimento = lblCodigo.Text;
                    strCodigoRequisicaoMudanca = lblIDRequisicaoMudanca.Text;

                    if ((strCodigoNotaAtendimento != string.Empty) && (strCodigoRequisicaoMudanca != string.Empty))
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
                    ClsRequisicaoMudanca.geraGridViewNotaAtendimento(gvNotaAtendimento, strCodigoRequisicaoMudanca);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

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

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void btnFiltrarChamados_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = string.Empty;
            string strMensagem = string.Empty;
            bool bPrimeiroCampo = true;

            pnlResultadoPesquisaChamado.Visible = true;

            //monta a query de acordo com os filtros.
            strSql = "SELECT C.chamado_codigo, C.descricao, C.data_inclusao, C.pessoa_codigo_proprietario, C.pessoa_codigo_solicitante ";
            strSql += "FROM chamado C ";
            if (ddlMudancaFiltro.SelectedValue != string.Empty)
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

            if (ddlMudancaFiltro.SelectedValue != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += " C.chamado_codigo = CIC.chamado_codigo AND CIC.ic_codigo = " + ddlMudancaFiltro.SelectedValue;
            }

            if (lblIDRequisicaoMudanca.Text != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += " C.chamado_codigo NOT IN (SELECT chamado_codigo from RequisicaoMudancaChamado WHERE RequisicaoMudanca_codigo =" + lblIDRequisicaoMudanca.Text + ") ";
            }
            strSql += " ORDER BY C.chamado_codigo";

            if (!ClsChamado.geraGridViewQuery(gvResultadoFiltroChamados, strSql, out strMensagem))
            {
                ExibeMensagem(strMensagem, "images/icones/aviso.gif", true);
            }

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }


    #region Envia para base de conhecimento.
    /// <summary>
    /// Envia registro de RequisicaoMudanca para base de conhecimento
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
            if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
            {
                string codUsuario = ClsUsuario.getCodigoUsuario().ToString();
                ClsConhecimentoProcesso.EnviaParaBaseConhecimento("REQUISICAOMUDANCA", lblIDRequisicaoMudanca.Text.Trim(), codUsuario, codUsuario, out strMensagem);
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    protected void lbkMudancas_Click(object sender, EventArgs e)
    {
        try
        {
            mvwAbas.ActiveViewIndex = 4;

            if (lblIDRequisicaoMudanca.Text.Trim() != string.Empty)
            {
                WUCSolucaoFiltro1.PreencheCampo("REQUISICAOMUDANCA", Convert.ToInt32(lblIDRequisicaoMudanca.Text.Trim()));
                WUCSolucaoFiltro1.Filtrar();
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

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
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
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
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #endregion

    #region Eventos

    #endregion
}
