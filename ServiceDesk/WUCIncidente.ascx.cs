/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • User Control que trata um incidente, permitindo sua ediçao.
      
  	Data: 27/12/2005
  	Autor: Sylvio Neto
  	Descrição: Este User Control encapsula as atividades relacionadas às operações sobre
    incidentes.
  
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
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

public partial class WUCIncidente : System.Web.UI.UserControl
{
    #region Declaracoes Publicas

    public string strFormatoDataInclusao = ClsParametro.DataInclusao;

    #endregion

    #region Propriedades

    private int intCodigoIncidente;
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
                ddlOrigemIncidente.Enabled = false;
            }
            else
            {
                WUCPriorizacao1.BloqueiaCampos(false, false, true);
                ddlOrigemIncidente.Enabled = true;
            }
        }
    }
    #endregion

    /// <summary>
    /// Abre um incidente existente para edição
    /// </summary>
    /// <param name="CodigoIncidente">Código do incidente desejado</param>
    public void EditaIncidente(int CodigoIncidente)
    {
        try
        {
            if (CodigoIncidente != 0)
            {
                //busca dados do incidente e preenche as abas
                tblAbas.Visible = true;
                mvwAbas.Visible = true;
                lblIDIncidente.Text = CodigoIncidente.ToString();
                wucIncidenteStatus.montaDados(Convert.ToInt32(lblIDIncidente.Text), "INCIDENTE");
            }
        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Cria um novo incidente herdando as informações do chamado informado
    /// </summary>
    /// <param name="CodigoChamado">Codigo do Chamado</param>
    public void CriaIncidenteHerdandoChamado(int CodigoChamado)
    {
        try
        {
            intCodigoChamado = CodigoChamado;
            if (intCodigoChamado != 0)
            {
                //Cria o novo incidente
                string strCodigoIncidenteCriado = string.Empty;
                if (ClsIncidente.criaIncidenteBaseadoChamado(intCodigoChamado.ToString(), out strCodigoIncidenteCriado))
                {
                    ExibeMensagem("Operação efetuada com sucesso.", "images/icones/info.gif", true);
                    intCodigoIncidente = Convert.ToInt32(strCodigoIncidenteCriado);

                    //busca dados do incidente criado e preenche as abas
                    tblAbas.Visible = true;
                    mvwAbas.Visible = true;
                    PreencheDadosIncidente(intCodigoIncidente);
                }
                else
                {
                    ExibeMensagem("Não foi possível criar um incidente vinculado ao chamado.", "images/icones/aviso.gif", true);
                }
            }
        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);


            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

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
            else //nao foi especidifcao, assume true
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
    /// Preenche os dados do incidente informado
    /// </summary>
    /// <param name="CodigoIncidente">Codigo do Incidente</param>
    private void PreencheDadosIncidente(int CodigoIncidente)
    {
        try
        {
            string strDataMinimaSistema = ClsParametro.DataMinimaSistema;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;

            ClsIncidente objIncidente = new ClsIncidente(CodigoIncidente);
            if (objIncidente != null)
            {
                //prenche os dados do incidente, marca os itens nas caixa e listas, etc.

                #region Remove o incidente atual da lista de modelos se ele for modelo
                if (lblIDIncidente.Text != string.Empty)
                {
                    ListItem itemAtual = ddlIncidentePreDefinido.Items.FindByValue(lblIDIncidente.Text);
                    ddlIncidentePreDefinido.Items.Remove(itemAtual);
                    itemAtual = null;
                }
                #endregion Remove o incidente atual da lista de modelos se ele for modelo

                #region Preenche os dados do Incidente
                try
                {
                    lblIDIncidente.Text = objIncidente.Codigo.Valor;
                    txtDescricao.Text = ClsTexto.trocaHtmlPorAspa(objIncidente.Descricao.Valor);
                    if (objIncidente.MatriculaSolicitante.Valor != string.Empty)
                    {
                        WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(objIncidente.MatriculaSolicitante.Valor));
                    }
                    if (objIncidente.MatriculaInclusor.Valor != string.Empty)
                    {
                        txtAtendente.Text = ClsUsuario.getNomeUsuario(objIncidente.MatriculaInclusor.Valor);
                    }

                    WUCPriorizacao1.setImpacto(objIncidente.Impacto.Valor);
                    WUCPriorizacao1.setUrgencia(objIncidente.Urgencia.Valor);

                    if (objIncidente.OrigemIncidente.Valor != string.Empty)
                    {
                        ddlOrigemIncidente.SelectedValue = objIncidente.OrigemIncidente.Valor;
                    }
                    if ((objIncidente.Escalado.Valor != string.Empty) && (objIncidente.Escalado.Valor == "S"))
                    {
                        ckEscalado.Checked = true;
                    }

                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objIncidente.NivelAtendimento.Valor, objIncidente.Equipe.Valor, objIncidente.Tecnico.Valor);
                    WUCEscalacaoHorizontal1.strTabela = "Incidente";
                    if (lblIDIncidente.Text.Trim() != string.Empty) WUCEscalacaoHorizontal1.intIdentificadorTabela = Convert.ToInt32(lblIDIncidente.Text.Trim());

                    if ((objIncidente.Vip.Valor != string.Empty) && (objIncidente.Vip.Valor == "S"))
                    { ckVip.Checked = true; }
                    else
                    { ckVip.Checked = false; }

                    if ((objIncidente.Modelo.Valor != string.Empty) && (objIncidente.Modelo.Valor.ToUpper() == "S"))
                    { ckModelo.Checked = true; }
                    else
                    { ckModelo.Checked = false; }

                }
                catch
                {
                    ExibeMensagem("Ocorreu um erro ao preencher os dados do incidente.", "images/icones/aviso.gif", true);
                }
                #endregion Preenche Dados Incidente

            }
            objIncidente = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }


    //Page Load  
    protected void Page_Load(object sender, EventArgs e)
    {
        int intIncidenteCodigo = 0;

        try
        {

            if (Request.QueryString["incidente"] != null)
            {
                intIncidenteCodigo = Convert.ToInt32(Request.QueryString["incidente"].ToString());
            }

            if (intIncidenteCodigo > 0)
            {
                //=====================================================================//
                // - O que: Libera o bloqueio do botão salvar depois de ter definido 
                // um proprietário para o incidente. Se o incidente verifica a segurança dos 
                // papéis sem ter um proprietário o sistema irá blquear o botão salvar 
                // deixando o incidente impossibilitado de receber um proprietário.
                // - Quem: Fernanda Passos
                // - Quando: 31/03/2006 ás 14:49hs
                //=====================================================================//
                if (ClsIncidente.GetCodigoProprietario(intIncidenteCodigo) != string.Empty) btnSalvar.Enabled = ClsUsuario.verificaAcessoPapel(ClsUsuario.getCodigoUsuario(), intIncidenteCodigo, 2, "incidente");
                //=====================================================================//
            }
            else
            {
                //Verificar acessibilidade
                /*int intCodigoFuncao = 47;
                if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
                {
                    Response.Redirect("AcessoNegado.aspx", false);
                    return;
                }*/
            }

            //Esconde a mensagem de erro
            ExibeMensagem("", "", false);

            if (!Page.IsPostBack)
            {
                pnlResultadoPesquisaChamado.Visible = false;
                mvwAbas.ActiveViewIndex = 0;
                string strMensagem = string.Empty;

                #region Preenche combos, listas etc

                //Coloca o nome do Atendente
                txtAtendente.Text = ClsUsuario.getNomeUsuario();
                //dropdonw lists
                ClsTipoOrigemChamado.geraDropDownList(ddlOrigemIncidente);
                WUCPriorizacao1.geraDropDownListImpactoUrgencia();

                //Monta a escalacao Horizontal
                if (lblIDIncidente.Text.Trim() == string.Empty)
                {
                    //Se está incluindo chamado, so exibe o 1º nivel para selecao
                    WUCEscalacaoHorizontal1.geraDropDownListNivel("0");
                }
                else
                {
                    //gera os niveis possiveis baseado no nivel do chamado
                    ClsIncidente objIncidenteNivel = new ClsIncidente(Convert.ToInt32(lblIDIncidente.Text.Trim()));
                    WUCEscalacaoHorizontal1.geraDropDownListNivel(objIncidenteNivel.NivelAtendimento.Valor);
                    objIncidenteNivel = null;
                }

                ClsIncidente.geraDropDownListIncidentePreDefinido(ddlIncidentePreDefinido);
                ServiceDesk.Negocio.ClsProblemaTipo.geraDropDownList(ddlProblemaTipo, "Selecione o tipo");
                //Campos de filtro da aba de chamados vinculados
                //Combo Solicitantes
                ClsUsuario.geraDropDownList(ddlSolicitanteFiltro, "--");
                //Combo Serviços
                ServiceDesk.Negocio.ClsItemConfiguracao.geraDropDownList(ddlServicoFiltro);

                #endregion

                if (lblIDIncidente.Text.Trim() != string.Empty)
                {
                    PreencheDadosIncidente(Convert.ToInt32(lblIDIncidente.Text.Trim()));
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
            if (lblIDIncidente.Text.Trim() != string.Empty) AbilitaDesabilitaAbas(true); else AbilitaDesabilitaAbas(false);
            //====================================================//


            if (lblIDIncidente.Text.Trim() != string.Empty)
            {
                lblPrefixo.Text = ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente);
                //Log
                WUCLog1.MontaDados(Convert.ToInt32(lblIDIncidente.Text.Trim()));
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

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
                        //if (lblIDIncidente.Text.Trim() != string.Empty)
                        //{
                        //    ClsIncidente objIncidente = new ClsIncidente(Convert.ToInt32(lblIDIncidente.Text.Trim()));
                        //    WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(objIncidente.MatriculaSolicitante.Valor));
                        //    objIncidente = null;
                        //}
                        break;
                    }
                case 1:
                    {
                        //Chamados  
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            ClsIncidente.geraGridViewChamadosVinculados(gvChamadosVinculados, lblIDIncidente.Text.Trim());
                        }
                        break;
                    }
                case 2:
                    {
                        //ICS
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            WUCItemConfiguracaoTreeView1.CarregaIC(Convert.ToInt32(lblIDIncidente.Text.Trim()));
                        }
                        break;
                    }
                case 3:
                    {
                        //Solucao
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            WUCSolucaoFiltro1.PreencheCampo("Incidente", Convert.ToInt32(lblIDIncidente.Text.Trim()));
                            WUCSolucaoFiltro1.Filtrar();
                        }
                        break;
                    }
                case 4:
                    {
                        //Problemas
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            ClsIncidente.geraGridViewProblemasVinculados(gvProblemaIncidente, lblIDIncidente.Text.Trim());
                        }
                        break;
                    }
                case 5:
                    {
                        //Anexos
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            WUCAnexo1.CarregaAnexos(lblIDIncidente.Text.Trim(), "Incidente");
                        }
                        break;
                    }
                case 6:
                    {
                        //Log
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            WUCLog1.MontaDados(Convert.ToInt32(lblIDIncidente.Text.Trim()));
                        }
                        break;
                    }
                case 7:
                    {
                        //Notas de Atendimento
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            ClsIncidente.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDIncidente.Text.Trim());
                        }
                        break;
                    }
                case 8:
                    {
                        //Aprovadores
                        if (lblIDIncidente.Text.Trim() != string.Empty)
                        {
                            WUCAprovador1.PreencheCampos("Incidente", Convert.ToInt32(lblIDIncidente.Text.Trim()));
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


    //Eventos dos Elementos da Tela
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            //Salva as informações do Incidente
            bool bOcorreuErro = false;
            string strCodigoChamado = string.Empty;
            string strCodigoIncidente = string.Empty;
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
                ExibeMensagem("Por favor informe a descrição do Incidente.", "images/icones/aviso.gif", true);
                bOcorreuErro = true;
            }
            #endregion


            if (bOcorreuErro == false)
            {
                string strMensagem = string.Empty;
                //cria o objeto Incidente
                ServiceDesk.Negocio.ClsIncidente objIncidente = new ServiceDesk.Negocio.ClsIncidente();

                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = objIncidente.Atributos.NomeTabela;

                //atribui os valores
                if (lblIDIncidente.Text == "") //novo
                {
                    objIncidente.MatriculaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString().ToString();
                    objIncidente.MatriculaSolicitante.Valor = WUCUsuario1.PessoaCodigo().ToString();
                    objIncidente.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);

                    //Verifica se caracteres da descrição execede o limite.
                    if (txtDescricao.Text.Length > 1000)
                    {
                        objIncidente.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Substring(0, 1000));
                        txtDescricao.Text = ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Substring(0, 1000));
                    }
                    else objIncidente.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

                    objIncidente.Status.Valor = wucIncidenteStatus.primeiroStatus("INCIDENTE").ToString();

                    if (ckEscalado.Checked)
                    { objIncidente.Escalado.Valor = "S"; }
                    else
                    { objIncidente.Escalado.Valor = "N"; }
                    objIncidente.OrigemIncidente.Valor = ddlOrigemIncidente.SelectedValue;

                    // ************************************************************************
                    // Alteração Sylvio- 16-02-2006
                    // ************************************************************************
                    if (WUCEscalacaoHorizontal1.getNivel().ToString().Trim() != string.Empty)
                    {
                        objIncidente.Equipe.Valor = WUCEscalacaoHorizontal1.getEquipe().ToString();
                        //objIncidente.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                        objIncidente.NivelAtendimento.Valor = WUCEscalacaoHorizontal1.getNivel().ToString();
                        //Se não foi selecionado tecnico. O lider de equipe vira técnico
                        if ((WUCEscalacaoHorizontal1.getNivel().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getEquipe().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getTecnico().ToString() == string.Empty))
                        {
                            objIncidente.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(WUCEscalacaoHorizontal1.getEquipe().ToString());
                        }
                        else
                        {
                            objIncidente.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                        }
                    }
                    else
                    {
                        //Nivel e Equipe de Atendimento padrão (escalacao horizontal)
                        objIncidente.NivelAtendimento.Valor = ClsParametro.NivelAtendimentoPadrao;
                        objIncidente.Equipe.Valor = ClsParametro.EquipeAtendimentoPadrao;
                        objIncidente.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(ClsParametro.EquipeAtendimentoPadrao);
                    }
                    // ************************************************************************

                    objIncidente.Impacto.Valor = WUCPriorizacao1.getImpacto().ToString();
                    objIncidente.Urgencia.Valor = WUCPriorizacao1.getUrgencia().ToString();
                    objIncidente.Prioridade.Valor = WUCPriorizacao1.getPrioridade().ToString();

                    if (ckVip.Checked)
                    { objIncidente.Vip.Valor = "S"; }
                    else
                    { objIncidente.Vip.Valor = "N"; }

                    if (ckModelo.Checked)
                    { objIncidente.Modelo.Valor = "S"; }
                    else
                    { objIncidente.Modelo.Valor = "N"; }

                    objIncidente.Codigo.Valor = objIdentificador.getProximoValor().ToString();


                    //salva novo
                    if (objIncidente.insere(out strMensagem))
                    {
                        objIdentificador.atualizaValor();

                        #region Grava Escalacao Horizontal e envia e-mail.  

                        ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal();
                        string strCodigoEscalacao = objEscalacaoHorizontal.insereEscalacao(objIncidente.Atributos.NomeTabela, objIncidente.Codigo.Valor, objIncidente.NivelAtendimento.Valor, objIncidente.Equipe.Valor, objIncidente.Tecnico.Valor, objIncidente.MatriculaAlterador.Valor).ToString();
                        if (strCodigoEscalacao != string.Empty)
                        {

                            #region Envia notificacao Equipe ou Tecnico
                            if ((objIncidente.Equipe.Valor != string.Empty) || (objIncidente.Tecnico.Valor != string.Empty))
                            {
                                ClsNotificacao objNotificacao = new ClsNotificacao();
                                objNotificacao.Tabela.Valor = "EscalacaoHorizontal";
                                objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                                objNotificacao.IdentificadorTabela.Valor = objEscalacaoHorizontal.Codigo.Valor;
                                objNotificacao.Tipo.Valor = ClsParametro.CodigoTipoAceitacao.Trim();
                                objNotificacao.CodigoUsuarioEmissor.Valor = objIncidente.MatriculaInclusor.Valor;

                                if ((objIncidente.Equipe.Valor != string.Empty) && (objIncidente.Tecnico.Valor == string.Empty))
                                {
                                    #region Equipe

                                    //lider
                                    SqlDataReader objReaderLider = ClsEquipe.getlLiderEquipe(objIncidente.Equipe.Valor.ToString());
                                    if (objReaderLider.Read())
                                    {
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objReaderLider["pessoa_codigo"].ToString();
                                    }
                                    objReaderLider = null;

                                    ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objIncidente.Equipe.Valor));
                                    objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("O Incidente " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente) + objIncidente.Codigo.Valor + " com a descricao '" + objIncidente.Descricao.Valor + "' foi escalado para o grupo '" + objEquipe.Descricao.Valor + "' pela central de serviços.");
                                    objEquipe = null;

                                    #endregion Equipe
                                }
                                else
                                {
                                    #region Tecnico

                                    if ((objIncidente.Equipe.Valor != string.Empty) && (objIncidente.Tecnico.Valor != string.Empty))
                                    {
                                        //Tecnico                
                                        objNotificacao.CodigoUsuarioReceptor.Valor = objIncidente.Tecnico.Valor;
                                        objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("O Incidente " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente) + objIncidente.Codigo.Valor + " com a descricao '" + objIncidente.Descricao.Valor + "' foi escalado para o técnico '" + ClsUsuario.getNomeUsuario(objIncidente.Tecnico.Valor) + "' pela central de serviços.");
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
                        SServiceDesk.Negocio.ClsWorkFlow.gravaLog(Convert.ToInt32(objIncidente.Codigo.Valor), objIncidente.Atributos.NomeTabela, "0", objIncidente.Status.Valor);
                        //SServiceDesk.Negocio.ClsWorkFlow.AlteraCodigoWorkFlowAtualProcesso("incidente", Convert.ToInt32(objIncidente.Codigo.Valor),);   
                        #endregion

                        //gera niveis da escalacao horizontal novamente
                        WUCEscalacaoHorizontal1.geraDropDownListNivel(objIncidente.NivelAtendimento.Valor);

                        ExibeMensagem("Operação realizada com sucesso.", "images/icones/info.gif", true);
                        lblIDIncidente.Text = objIncidente.Codigo.Valor.ToString();
                        WUCAnexo1.salvaDocumento(true, lblIDIncidente.Text, "INCIDENTE");
                        PreencheDadosIncidente(Convert.ToInt32(lblIDIncidente.Text.Trim()));

                        // Abilita todas as abas por que agora já tem registro salvo.
                        AbilitaDesabilitaAbas(true);
                    }
                    else
                    {
                        ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                    }

                    //gera niveis da escalacao horizontal novamente
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objIncidente.NivelAtendimento.Valor, objIncidente.Equipe.Valor, objIncidente.Tecnico.Valor);


                }
                else 
                {
                    objIncidente.Codigo.Valor = lblIDIncidente.Text;
                    objIncidente.DataAlteracao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                    objIncidente.MatriculaAlterador.Valor = ClsUsuario.getCodigoUsuario().ToString().ToString();
                    objIncidente.MatriculaSolicitante.Valor = WUCUsuario1.PessoaCodigo().ToString();
                    objIncidente.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

                    if (ckEscalado.Checked)
                    { objIncidente.Escalado.Valor = "S"; }
                    else
                    { objIncidente.Escalado.Valor = "N"; }

                    objIncidente.OrigemIncidente.Valor = ddlOrigemIncidente.SelectedValue;

                    // ************************************************************************
                    // Alteração Gleison - 14-01-2006
                    // ************************************************************************
                    objIncidente.Equipe.Valor = WUCEscalacaoHorizontal1.getEquipe().ToString();
                    //objIncidente.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                    objIncidente.NivelAtendimento.Valor = WUCEscalacaoHorizontal1.getNivel().ToString();
                    //Se não foi selecionado tecnico. O lider de equipe vira técnico
                    if ((WUCEscalacaoHorizontal1.getNivel().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getEquipe().ToString() != string.Empty) && (WUCEscalacaoHorizontal1.getTecnico().ToString() == string.Empty))
                    {
                        objIncidente.Tecnico.Valor = ClsEquipe.getCodigoLiderEquipe(WUCEscalacaoHorizontal1.getEquipe().ToString());
                    }
                    else
                    {
                        objIncidente.Tecnico.Valor = WUCEscalacaoHorizontal1.getTecnico().ToString();
                    }
                    // ************************************************************************

                    objIncidente.Impacto.Valor = WUCPriorizacao1.getImpacto().ToString();
                    objIncidente.Urgencia.Valor = WUCPriorizacao1.getUrgencia().ToString();
                    objIncidente.Prioridade.Valor = WUCPriorizacao1.getPrioridade().ToString();

                    if (ckVip.Checked)
                    { objIncidente.Vip.Valor = "S"; }
                    else
                    { objIncidente.Vip.Valor = "N"; }

                    if (ckModelo.Checked)
                    { objIncidente.Modelo.Valor = "S"; }
                    else
                    { objIncidente.Modelo.Valor = "N"; }


                    //Instancia um obj antes da alteração
                    //para utilizar na gravacao de log.
                    ClsIncidente objIncidenteAntigo = new ClsIncidente(Convert.ToInt32(objIncidente.Codigo.Valor));

                    //salva alteracao
                    if (objIncidente.altera(out strMensagem))
                    {
                        string strMensagemAviso = "Operação realizada com sucesso.";
                        string strImagemAviso = "images/icones/info.gif";

                        wucIncidenteStatus.salvaStatus();
                        WUCAnexo1.salvaDocumento(true, lblIDIncidente.Text, "INCIDENTE");

                        //Instancia um obj APÓS da alteração
                        //para utilizar na gravacao de log.
                        ClsIncidente objIncidenteAtualizado = new ClsIncidente(Convert.ToInt32(objIncidente.Codigo.Valor));
                        //Marca o campo de data de alteracao para nao ser verificado
                        objIncidenteAtualizado.DataAlteracao.VerificaAlteracao = false;

                        //grava log de alteração
                        ClsLog.insereLog(ClsLog.enumTipoLog.UPDATE, objIncidente.MatriculaAlterador.Valor, "Incidente", objIncidente.Codigo.Valor, objIncidenteAtualizado.Atributos, objIncidenteAntigo.Atributos);

                        PreencheDadosIncidente(Convert.ToInt32(lblIDIncidente.Text.Trim()));

                        //Se houve mudança na escalação horizontal. grava escalacao e 
                        //manda mail
                        if ((objIncidenteAntigo.NivelAtendimento.Valor != objIncidenteAtualizado.NivelAtendimento.Valor) || (objIncidenteAntigo.Equipe.Valor != objIncidenteAtualizado.Equipe.Valor) || (objIncidenteAntigo.Tecnico.Valor != objIncidenteAtualizado.Tecnico.Valor))
                        {
                            #region Grava Escalacao Horizontal e envia e-mail.
                            ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal();
                            string strCodigoEscalacao = objEscalacaoHorizontal.insereEscalacao(objIncidente.Atributos.NomeTabela, objIncidente.Codigo.Valor, objIncidente.NivelAtendimento.Valor, objIncidente.Equipe.Valor, objIncidente.Tecnico.Valor, objIncidente.MatriculaAlterador.Valor).ToString();
                            if (strCodigoEscalacao != string.Empty)
                            {

                                #region Envia notificacao Equipe ou Tecnico
                                if ((objIncidente.Equipe.Valor != string.Empty) || (objIncidente.Tecnico.Valor != string.Empty))
                                {
                                    ClsNotificacao objNotificacao = new ClsNotificacao();
                                    objNotificacao.Tabela.Valor = "EscalacaoHorizontal";
                                    objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                                    objNotificacao.IdentificadorTabela.Valor = objEscalacaoHorizontal.Codigo.Valor;
                                    objNotificacao.Tipo.Valor = ClsParametro.CodigoTipoAceitacao.Trim();
                                    objNotificacao.CodigoUsuarioEmissor.Valor = objIncidenteAntigo.MatriculaInclusor.Valor;

                                    if ((objIncidente.Equipe.Valor != string.Empty) && (objIncidente.Tecnico.Valor == string.Empty))
                                    {
                                        #region Equipe

                                        //lider
                                        SqlDataReader objReaderLider = ClsEquipe.getlLiderEquipe(objIncidente.Equipe.Valor.ToString());
                                        if (objReaderLider.Read())
                                        {
                                            objNotificacao.CodigoUsuarioReceptor.Valor = objReaderLider["pessoa_codigo"].ToString();
                                        }
                                        objReaderLider = null;

                                        ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objIncidente.Equipe.Valor));
                                        objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("O Incidente " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente) + objIncidente.Codigo.Valor + " com a descricao '" + objIncidente.Descricao.Valor + "' foi escalado para o grupo '" + objEquipe.Descricao.Valor + "' pela central de serviços.");
                                        objEquipe = null;

                                        #endregion Equipe
                                    }
                                    else
                                    {
                                        #region Tecnico

                                        if ((objIncidente.Equipe.Valor != string.Empty) && (objIncidente.Tecnico.Valor != string.Empty))
                                        {
                                            //Tecnico                
                                            objNotificacao.CodigoUsuarioReceptor.Valor = objIncidente.Tecnico.Valor;
                                            objNotificacao.Descricao.Valor = ClsTexto.trocaAspaPorHtml("O Incidente " + ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente) + objIncidente.Codigo.Valor + " com a descricao '" + objIncidente.Descricao.Valor + "' foi escalado para o técnico '" + ClsUsuario.getNomeUsuario(objIncidente.Tecnico.Valor) + "' pela central de serviços.");
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

                        objIncidenteAtualizado = null;



                        ExibeMensagem(strMensagemAviso, strImagemAviso, true);
                    }
                    else
                    {
                        ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
                    }

                    objIncidenteAntigo = null;

                    //gera niveis da escalacao horizontal novamente
                    WUCEscalacaoHorizontal1.geraDropDownListNivel(objIncidente.NivelAtendimento.Valor);
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objIncidente.NivelAtendimento.Valor, objIncidente.Equipe.Valor, objIncidente.Tecnico.Valor);

                    objIncidente = null;
                    objIdentificador = null;
                }

                if (lblIDIncidente.Text.Trim() != "")
                    wucIncidenteStatus.montaDados(Convert.ToInt32(lblIDIncidente.Text), "INCIDENTE");

                if (lblIDIncidente.Text.Trim() != string.Empty)
                {
                    WUCLog1.MontaDados(Convert.ToInt32(lblIDIncidente.Text.Trim()));
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void btnAplicaIncidentePreDefinido_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlIncidentePreDefinido.SelectedValue != string.Empty)
            {
                try
                {
                    //busca os dados do incidente selecionado.
                    ClsIncidente objIncidenteModelo = new ClsIncidente(Convert.ToInt32(ddlIncidentePreDefinido.SelectedValue));

                    //preenche os campos de classificaçãso e cadastro da tela.
                    WUCEscalacaoHorizontal1.setEquipeNivelTecnico(objIncidenteModelo.NivelAtendimento.Valor, objIncidenteModelo.Equipe.Valor, objIncidenteModelo.Tecnico.Valor);

                    ddlOrigemIncidente.SelectedValue = objIncidenteModelo.OrigemIncidente.Valor;
                    WUCPriorizacao1.setImpacto(objIncidenteModelo.Impacto.Valor);
                    WUCPriorizacao1.setUrgencia(objIncidenteModelo.Urgencia.Valor);

                    if (objIncidenteModelo.Escalado.Valor == "S")
                    { ckEscalado.Checked = true; }
                    else
                    { ckEscalado.Checked = false; }

                    if (objIncidenteModelo.Vip.Valor == "S")
                    { ckVip.Checked = true; }
                    else
                    { ckVip.Checked = false; }

                    objIncidenteModelo = null;

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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void btnGravarVinculos_Click(object sender, EventArgs e)
    {
        string strCodigoIncidente = lblIDIncidente.Text;
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
                        if ((strCodigoChamadoVinculado != string.Empty) && (strCodigoIncidente != string.Empty))
                        {
                            ClsIncidente.AdicionaRelacaoIncidenteChamado(strCodigoChamadoVinculado, strCodigoIncidente);
                        }
                    }
                }
            }

            ExibeMensagem("Operação efetuada com sucesso.", "images/icones/info.gif", true);
            ClsIncidente.geraGridViewChamadosVinculados(gvChamadosVinculados, strCodigoIncidente);
            btnVinculaChamado.Text = "Pesquisar Chamado";
            pnlFiltros.Visible = false;
            pnlResultadoPesquisaChamado.Visible = false;

        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

        }
    }

    protected void btnSalvarProblema_Click(object sender, EventArgs e)
    {
        try
        {
            string strMensagem = string.Empty;
            string strFormatoDataInclusao = ClsParametro.DataInclusao;

            //instancia um objeto incidente com o incidente atual para usar as informações.
            ClsIncidente objIncidente = new ClsIncidente(Convert.ToInt32(lblIDIncidente.Text));

            //cria e grava o novo problema
            ClsProblema objProblema = new ClsProblema();
            objProblema.Codigo.Valor = objProblema.GetMaxId().ToString();
            objProblema.Nome.Valor = ClsTexto.trocaAspaPorHtml(txtNomeProblema.Text.Trim());
            objProblema.Descricao.Valor = ClsTexto.trocaAspaPorHtml(txtDescricaoProblema.Text.Trim());
            objProblema.CodigoProblemaTipo.Valor = ddlProblemaTipo.SelectedValue;
            objProblema.DtInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
            objProblema.UsuarioQueCadastrou.Valor = ClsUsuario.getCodigoUsuario().ToString().ToString();
            objProblema.FlagFechado.Valor = "N";
            objProblema.Codigo.Valor = objProblema.GetMaxId().ToString();

            if (objProblema.insere(out strMensagem))
            {
                //Se conseguiu inserir, relaciona o incidente com o problema criado
                //e copia os dados do incidente para o problema criado
                try
                {
                    //Relaciona incidente ao problema
                    ClsProblema.AdicionaRelacaoIncidenteProblema(objIncidente.Codigo.Valor, objProblema.Codigo.Valor);
                    //Copia os ICs do chamado para o incidente criado
                    ClsProblema.herdaICIncidente(objIncidente.Codigo.Valor, objProblema.Codigo.Valor);
                    ExibeMensagem(strMensagem, "images/icones/info.gif", true);
                }
                catch (Exception ex)
                {
                    ExibeMensagem(ex.Message, "images/icones/aviso.gif", true);
                }
            }
            else
            {
                ExibeMensagem(strMensagem, "images/icones/aviso.gif", true);
            }

            ClsIncidente.geraGridViewProblemasVinculados(gvProblemaIncidente, objIncidente.Codigo.Valor);

            objIncidente = null;
            objProblema = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void btnNovoProblema_Click(object sender, EventArgs e)
    {
        ddlProblemaTipo.SelectedValue = string.Empty;
        txtNomeProblema.Text = string.Empty;
        txtDescricaoProblema.Text = string.Empty;
        txtProblemaCodigo.Text = string.Empty;
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
                objNotaAtendimento.Tabela.Valor = "Incidente";
                objNotaAtendimento.DescricaoNota.Valor = ClsTexto.trocaAspaPorHtml(txtDescricaoNotaAtendimento.Text);
                objNotaAtendimento.DataInclusao.Valor = DateTime.Now.ToString(strFormatoDataInclusao);
                objNotaAtendimento.IdentificadorTabela.Valor = lblIDIncidente.Text;
                objNotaAtendimento.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString().ToString();

                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = objNotaAtendimento.Atributos.NomeTabela;
                objNotaAtendimento.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objNotaAtendimento.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                    ExibeMensagem("Operação realizada com sucesso.", "images/icones/info.gif", true);
                    txtDescricaoNotaAtendimento.Text = "";
                    ClsIncidente.geraGridViewNotaAtendimento(gvNotaAtendimento, lblIDIncidente.Text);
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void gvChamadosVinculados_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoChamado = string.Empty;
                string strCodigoIncidente = string.Empty;
                GridViewRow objRow = gvChamadosVinculados.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoChamado");
                    strCodigoChamado = lblCodigo.Text;
                    strCodigoIncidente = lblIDIncidente.Text;

                    if ((strCodigoChamado != string.Empty) && (strCodigoIncidente != string.Empty))
                    {
                        try
                        {
                            ClsIncidente.RemoveRelacaoIncidenteChamado(strCodigoIncidente, strCodigoChamado);
                            ExibeMensagem("Item removido com sucesso.", "images/icones/info.gif", true);
                        }
                        catch
                        {
                            ExibeMensagem("Ocorreu um erro ao remover o item. Operação não realizada.", "images/icones/aviso.gif", true);
                        }
                    }
                    ClsIncidente.geraGridViewChamadosVinculados(gvChamadosVinculados, strCodigoIncidente);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void gvProblemaIncidente_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow objRow = gvProblemaIncidente.Rows[Convert.ToInt32(e.CommandArgument)];

            if (e.CommandName == "Detalhe")
            {
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoProblema");
                    Response.Redirect("Problema.aspx?CodProblema=" + lblCodigo.Text.Trim(), false);
                }
            }
            if (e.CommandName == "Editar")
            {
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoProblema");
                    ServiceDesk.Negocio.ClsProblema objProblema = new ServiceDesk.Negocio.ClsProblema(Convert.ToInt32(lblCodigo.Text.Trim()));
                    txtNomeProblema.Text = objProblema.Nome.Valor;
                    txtDescricaoProblema.Text = objProblema.Descricao.Valor;
                    ddlProblemaTipo.SelectedValue = objProblema.CodigoProblemaTipo.Valor;
                    objProblema = null;
                }
            }
            objRow = null;

        }
        catch (Exception ex)
        {
            ExibeMensagem(ex.Message, "images/icones/aviso.gif", true);
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void gvNotaAtendimento_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoNotaAtendimento = string.Empty;
                string strCodigoIncidente = string.Empty;
                GridViewRow objRow = gvNotaAtendimento.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoNotaAtendimento");
                    strCodigoNotaAtendimento = lblCodigo.Text;
                    strCodigoIncidente = lblIDIncidente.Text;

                    if ((strCodigoNotaAtendimento != string.Empty) && (strCodigoIncidente != string.Empty))
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
                    ClsIncidente.geraGridViewNotaAtendimento(gvNotaAtendimento, strCodigoIncidente);
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());

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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    protected void btnFiltrarChamados_Click(object sender, EventArgs e)
    {
        try
        {
            string strSql = string.Empty;
            string strMensagem = string.Empty;
            bool bPrimeiroCampo = true;

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

            if (lblIDIncidente.Text != string.Empty)
            {
                if (bPrimeiroCampo == false)
                { strSql += " AND "; }
                else
                { bPrimeiroCampo = false; }

                strSql += " C.chamado_codigo NOT IN (SELECT chamado_codigo from IncidenteChamado WHERE incidente_codigo =" + lblIDIncidente.Text + ") ";
            }
            strSql += " ORDER BY C.chamado_codigo";

            if (!ClsChamado.geraGridViewQuery(gvResultadoFiltroChamados, strSql, out strMensagem))
            {
                ExibeMensagem(strMensagem, "images/icones/aviso.gif", true);
                pnlResultadoPesquisaChamado.Visible = false;
            }
            else
            {
                pnlResultadoPesquisaChamado.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }



    #region Envia para base de conhecimento.
    /// <summary>
    /// Envia registro de incidente para base de conhecimento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnviaParaBaseConhecimento_Click(object sender, EventArgs e)
    {
        try
        {
            string strMensagem = string.Empty;
            string codUsuario = ClsUsuario.getCodigoUsuario().ToString().ToString();
            divMensagem.Visible = false;
            if (lblIDIncidente.Text.Trim() != string.Empty)
            {
                ClsConhecimentoProcesso.EnviaParaBaseConhecimento("Incidente", lblIDIncidente.Text.Trim(), codUsuario, codUsuario, out strMensagem);
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion



    protected void gvChamadosVinculados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                try
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    //ImageButton btnExcluir = (ImageButton)e.Row.Cells[3].Controls[0];
                    //btnExcluir.Attributes.Add("onclick", "return verifica();");
                    //for (int j = 0; j < e.Row.Cells.Count; j++)
                    //{
                    //  for (int i = 0; i < e.Row.Cells[j].Controls.Count; i++)
                    //  {
                    //    string tipo = e.Row.Cells[i].Controls.GetType().ToString();
                    //  }
                    //}
                }
                catch (Exception ex)
                {
                    ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
                }
            }
        }
    }

    #region Métodos
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Eventos

    #endregion

}
