/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Formulário para atualização e cadastro de soluções para qualquer processo.
  
  	Data: 19/11/2005
  	Autor: Fernanda Passos
  	Descrição: Este WebForm apresenta funcionalidade como inserir, excluir, alterar, filtrar
    soluções.
    
  • Alterações
  	Data:
  	Autor:
  	Descrição:
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class SolucaoProjeto : BasePage
{
    #region Declarações
    ServiceDesk.Negocio.ClsSolucaoProjeto objSolucaoProjeto = new ServiceDesk.Negocio.ClsSolucaoProjeto();
    ServiceDesk.Projeto.ClsProjeto objProjeto = new ServiceDesk.Projeto.ClsProjeto();
    #endregion

    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(0);

            divMensagem.Visible = false;
            if (!Page.IsPostBack)
            {
                mtwAbas.ActiveViewIndex = 0;

                ClsSolucaoProjeto.geraDropDownList(dlProcesso);
                ClsSolucaoProjetoTipo.geraDropDownList(dlTipoSolucao, "Selecione o tipo");

                //>>> Se foi informado a solução que deseja visualizar o detalhe <<<
                if (Request.QueryString["tabela"] != null && Request.QueryString["identificador"] != null && Request.QueryString["codigo"] != null)
                {
                    txtTabela.Text = Request.QueryString["tabela"].ToString();
                    txtTabelaIdentificador.Text = Request.QueryString["identificador"];
                    txtCodigoSolucao.Text = Request.QueryString["codigo"].ToString().Trim();

                    //Preenche dados da solução encontrada para a tabela.
                    PreecheCampos(objSolucaoProjeto.GetDataSolucaoProjeto(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim())), "Solucao");

                    //Preenche dados do processo que originou o problema ou que originará.
                    PreecheCampos(objSolucaoProjeto.GetDadosProcessoOrigemSolucao(Convert.ToInt32(txtTabelaIdentificador.Text.Trim()), txtTabela.Text.Trim()), txtTabela.Text.Trim());
                }
                //>>> Se foi informando o processo (tabela) e o registro do processo (indentificado tabela) <<<
                else if (Request.QueryString["tabela"] != null && Request.QueryString["identificador"] != null)
                {
                    txtTabela.Text = Request.QueryString["tabela"].ToString();
                    txtTabelaIdentificador.Text = Request.QueryString["identificador"].ToString();

                    //Preenche dados da solução encontrada para a tabela.
                    PreecheCampos(objSolucaoProjeto.GetDataSolucaoProjeto(Convert.ToInt32(txtTabelaIdentificador.Text.Trim()), txtTabela.Text.Trim()), "Solucao");

                    //Preenche dados do processo que originou o problema ou que originará.
                    PreecheCampos(objSolucaoProjeto.GetDadosProcessoOrigemSolucao(Convert.ToInt32(txtTabelaIdentificador.Text.Trim()), txtTabela.Text.Trim()), txtTabela.Text);
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Métodos


    /// <summary>
    /// Projeto - Limpa os campos de dados do projeto.
    /// </summary>
    private void NovoProjeto()
    {
        try
        {
            txtCodigoProjetoSuperior.Text = "";
            txtCodigoProjeto.Text = "";
            txtAtividadeNome.Text = "";
            dlAtividadeResponsavel.SelectedIndex = -1;
            dpkAtividadeDtInicioPrevista.Clear();
            dpkAtividadeDtFimPrevista.Clear();
            txtAtividadeObservacao.Text = "";

        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Limpa os campos na tela.
    /// </summary>
    private void Novo()
    {
        try
        {
            txtDescricaoSolucao.Text = "";
            dlTipoSolucao.SelectedIndex = -1;
            ckbSeraImplementado.Checked = false;
            txtDescricaoNaoImplementar.Text = "";
            txtCodigoSolucao.Text = "";
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Salva projeto.
    /// </summary>
    private void SalvarProjeto()
    {
        try
        {
            string strMensagem = string.Empty;

            if (dpkAtividadeDtInicioPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00" && dpkAtividadeDtFimPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00")
            {
                if (dpkAtividadeDtInicioPrevista.SelectedDate > dpkAtividadeDtFimPrevista.SelectedDate)
                {
                    lblMensagem.Text = "A data de inicio prevista não pode ser maior que a data fim prevista.";
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                    return;
                }
            }

            int intCodigoProjeto = 0;

            if (objSolucaoProjeto.VerificaSeExisteProjetoParaSolucao(Convert.ToInt32(txtCodigoSolucao.Text.Trim()), out intCodigoProjeto) == false)
            {
                if (txtCodigoProjetoSuperior.Text.Trim() == string.Empty && txtCodigoProjeto.Text.Trim() == string.Empty)
                {
                    objProjeto.Codigo.Valor = objProjeto.GetMaxId().ToString();
                    objProjeto.CodigoSuperior.Valor = "0";
                    objProjeto.Nome.Valor = txtAtividadeNome.Text.Trim();
                    objProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                    objProjeto.CodigoPessoaInclusor.Valor = ClsUsuario.getCargoUsuario(ClsUsuario.getCodigoRede());
                    if (dpkAtividadeDtInicioPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00") objProjeto.DataInicioPrevista.Valor = dpkAtividadeDtInicioPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);
                    if (dpkAtividadeDtFimPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00") objProjeto.DataFimPrevista.Valor = dpkAtividadeDtFimPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);

                    if (objProjeto.insere(out strMensagem) == false)
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                    else
                    {
                        //Atualiza tabela SolucaoProjeto com o código do projeto.
                        objProjeto.UpdateCodigoProjetoSolucao(Convert.ToInt32(objProjeto.Codigo.Valor), Convert.ToInt32(txtCodigoSolucao.Text.Trim()));

                        //Se foi informado o responsável atualiza responsável.
                        if (dlAtividadeResponsavel.SelectedValue.ToString() != string.Empty)
                        {
                            ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ServiceDesk.Projeto.ClsProjetoPessoa();

                            objProjetoPessoa.CodigoPessoa.Valor = dlAtividadeResponsavel.SelectedValue.ToString();
                            objProjetoPessoa.CodigoProjeto.Valor = objProjeto.Codigo.Valor;

                            if (objProjetoPessoa.insere(out strMensagem) == false)
                            {
                                lblMensagem.Text = strMensagem;
                                imgIcone.ImageUrl = "images/icones/aviso.gif";
                                divMensagem.Visible = true;
                            }
                            else
                            {
                                divMensagem.Visible = false;
                                int intCodPai = 0;
                                if (objProjeto.VerificaSeFilho(Convert.ToInt32(objProjeto.Codigo.Valor), out intCodPai) == true)
                                    txtCodigoProjetoSuperior.Text = intCodPai.ToString();
                                txtCodigoProjeto.Text = objProjeto.Codigo.Valor;
                            }
                        }
                        //<<<< Fim insere responsavel
                    }
                }
            }
            else if (txtCodigoProjetoSuperior.Text.Trim() == string.Empty && txtCodigoProjeto.Text.Trim() == string.Empty)
            {
                if (tvAtividadeProjeto.SelectedNode == null)
                {
                    lblMensagem.Text = "Por favor, selecione o item no qual deseja inserir uma atividade.";
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                    return;
                }

                objProjeto.Codigo.Valor = objProjeto.GetMaxId().ToString();
                objProjeto.CodigoSuperior.Valor = tvAtividadeProjeto.SelectedNode.Value;
                objProjeto.Nome.Valor = txtAtividadeNome.Text.Trim();
                objProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objProjeto.CodigoPessoaInclusor.Valor = user.IDusuario.ToString();
                if (dpkAtividadeDtInicioPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00") objProjeto.DataInicioPrevista.Valor = dpkAtividadeDtInicioPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);
                if (dpkAtividadeDtFimPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00") objProjeto.DataFimPrevista.Valor = dpkAtividadeDtFimPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);

                if (objProjeto.insere(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
                else
                {
                    //Se foi informado o responsável atualiza responsável.
                    if (dlAtividadeResponsavel.SelectedValue.ToString() != string.Empty)
                    {
                        ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ServiceDesk.Projeto.ClsProjetoPessoa();

                        objProjetoPessoa.CodigoPessoa.Valor = dlAtividadeResponsavel.SelectedValue.ToString();
                        objProjetoPessoa.CodigoProjeto.Valor = objProjeto.Codigo.Valor;

                        if (objProjetoPessoa.insere(out strMensagem) == false)
                        {
                            lblMensagem.Text = strMensagem;
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                        else
                        {
                            divMensagem.Visible = false;
                            int intCodPai = 0;
                            if (objProjeto.VerificaSeFilho(Convert.ToInt32(objProjeto.Codigo.Valor), out intCodPai) == true)
                                txtCodigoProjetoSuperior.Text = intCodPai.ToString();
                            txtCodigoProjeto.Text = objProjeto.Codigo.Valor;
                        }
                    }
                    //<<<< Fim insere responsavel
                }
            }
            else if (txtCodigoProjeto.Text.Trim() != string.Empty)
            {
                objProjeto.Codigo.Valor = txtCodigoProjeto.Text.Trim();
                if (txtCodigoProjetoSuperior.Text.Trim() == string.Empty)
                    objProjeto.CodigoSuperior.Valor = "0";
                else objProjeto.CodigoSuperior.Valor = txtCodigoProjetoSuperior.Text.Trim();

                objProjeto.Nome.Valor = txtAtividadeNome.Text.Trim();
                objProjeto.DataAlteracao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objProjeto.CodigoPessoaAlterador.Valor = user.IDusuario.ToString();
                if (dpkAtividadeDtInicioPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00") objProjeto.DataInicioPrevista.Valor = dpkAtividadeDtInicioPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);
                if (dpkAtividadeDtFimPrevista.SelectedDate.ToString() != "1/1/0001 00:00:00") objProjeto.DataFimPrevista.Valor = dpkAtividadeDtFimPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);

                if (objProjeto.altera(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
                else
                {
                    //Altera o responsável.
                    ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ServiceDesk.Projeto.ClsProjetoPessoa();
                    objProjetoPessoa.CodigoPessoa.Valor = dlAtividadeResponsavel.SelectedValue.ToString();
                    objProjetoPessoa.CodigoProjeto.Valor = objProjeto.Codigo.Valor;
                    //Verifica se já existe resp.
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.retornaValorCampo("ProjetoPessoa", "projeto_codigo", "projeto_codigo = " + Convert.ToInt32(objProjeto.Codigo.Valor.Trim()) + "") != string.Empty)
                        objProjetoPessoa.altera();
                    else
                        objProjetoPessoa.insere(out strMensagem);

                    divMensagem.Visible = false;
                    int intCodPai = 0;
                    if (objProjeto.VerificaSeFilho(Convert.ToInt32(objProjeto.Codigo.Valor), out intCodPai) == true)
                        txtCodigoProjetoSuperior.Text = intCodPai.ToString();
                    txtCodigoProjeto.Text = objProjeto.Codigo.Valor;

                }
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Preenche campos com o resultado da pesquisa de solução.
    /// </summary>
    private void PreecheCampos(System.Data.SqlClient.SqlDataReader objReader, string txtTabelaLocal)
    {
        try
        {
            if (objReader == null) return;

            if (objReader.Read())
            {
                if (txtTabelaLocal == "Solucao")
                {
                    txtCodigoSolucao.Text = objReader["solucao_projeto_codigo"].ToString();
                    txtDescricaoSolucao.Text = objReader["descricao"].ToString();
                    dlTipoSolucao.SelectedValue = objReader["solucao_projeto_tipo_codigo"].ToString();
                    if (objReader["flag_implementacao"].ToString() == "S") ckbSeraImplementado.Checked = true;
                    if (objReader["flag_implementacao"].ToString() == "N") ckbSeraImplementado.Checked = false;
                    txtDescricaoNaoImplementar.Text = objReader["descricao_nao_implementacao"].ToString();
                }
                else if (txtTabelaLocal == "Problema")
                {
                    txtNomeProblema.Text = objReader["nome"].ToString();
                    txtEquipeAlocadaProblema.Text = objReader["equipe_alocada"].ToString();
                    txtDescricaoProblema.Text = objReader["descricao"].ToString();
                    txtPessoaAlocadaProblema.Text = objReader["pessoa_alocada"].ToString();
                    txtStatusProblema.Text = objReader["status_codigo"].ToString();
                    mtwDadosProcessos.ActiveViewIndex = 0;
                }
                else if (txtTabelaLocal == "Projeto")
                {
                    txtCodigoProjetoSuperior.Text = objReader["projeto_codigo_superior"].ToString();
                    txtCodigoProjeto.Text = objReader["projeto_codigo"].ToString();
                    txtAtividadeNome.Text = objReader["nome"].ToString();
                    dlAtividadeResponsavel.SelectedValue = objReader["pessoa_codigo"].ToString();
                    if (objReader["data_inicio_prevista"].ToString() != string.Empty)
                        dpkAtividadeDtInicioPrevista.SelectedDate = (DateTime)objReader["data_inicio_prevista"];
                    else
                        dpkAtividadeDtInicioPrevista.Clear();

                    if (objReader["data_fim_prevista"].ToString() != string.Empty)
                        dpkAtividadeDtFimPrevista.SelectedDate = (DateTime)objReader["data_fim_prevista"];
                    else
                        dpkAtividadeDtFimPrevista.Clear();

                    txtAtividadeObservacao.Text = objReader["observacao"].ToString();
                }
                else if (txtTabelaLocal == "Chamado")
                {
                    lblChamadoCodigo.Text = objReader["chamado_codigo"].ToString();
                    txtChamadoSolicitante.Text = objReader["solicitante"].ToString();
                    txtChamadoProprietario.Text = objReader["proprietario"].ToString();
                    txtChamadoStatus.Text = objReader["status_codigo"].ToString();
                    txtChamadoEquipe.Text = objReader["equipe"].ToString();
                    txtChamadoPessoaAlicada.Text = objReader["tecnico_pessoa_alocada"].ToString();
                    txtChamadoOrigem.Text = objReader["origem_chamado"].ToString();
                    txtChamadoVip.Text = objReader["vip"].ToString();
                    txtChamadoImpacto.Text = objReader["impacto"].ToString();
                    txtChamadoUrgencia.Text = objReader["urgencia"].ToString();
                    txtChamadoPrioridade.Text = objReader["proprietario"].ToString();
                    txtChamadoEscalado.Text = objReader["escalado"].ToString();
                    txtChamadoTipo.Text = objReader["tipo_chamado"].ToString();

                    mtwDadosProcessos.ActiveViewIndex = 1;
                }
                else if (txtTabelaLocal == "Incidente")
                {
                    txtIncidenteDescricao.Text = objReader["descricao"].ToString();
                    lblIncidenteCodigo.Text = objReader["incidente_codigo"].ToString();
                    txtIncidenteStatus.Text = objReader["status_codigo"].ToString();
                    txtIncidenteSolicitante.Text = objReader["solicitante"].ToString();
                    txtIncidenteImpacto.Text = objReader["impacto"].ToString();
                    txtIncidenteEquipe.Text = objReader["equipe"].ToString();
                    txtIncidenteUrgencia.Text = objReader["urgencia"].ToString();
                    txtIncidenteTecnico.Text = objReader["pessoa_alocacao"].ToString();
                    txtIncidentePrioridade.Text = objReader["prioridade"].ToString();
                    txtIncidenteProprietario.Text = objReader["proprietario"].ToString();
                    txtIncidenteOrigem.Text = objReader["origem"].ToString();

                    mtwDadosProcessos.ActiveViewIndex = 2;
                }


            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    /// <summary>
    /// Limpa os campos do filtro de pesquisa da solução.
    /// </summary>
    private void NovoFiltro()
    {
        try
        {
            dlProcesso.SelectedIndex = -1;
            dpkDataIncioPesquisa.Clear();
            dpkDataFimPesquisa.Clear();
            txtDescricaoPesquisa.Text = "";
            gdPesquisaSolucao.DataSource = null;
            gdPesquisaSolucao.DataBind();
        }
        catch (Exception ex)
        {

            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;


            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Salvar solução
    /// </summary>
    private void SalvarSolucao()
    {
        try
        {
            ///Verifica se foi selecionado alguma solução.
            if (txtTabela.Text.Trim() == string.Empty)
            {
                divMensagem.Visible = true;
                lblMensagem.Text = "Selecione uma solução.";
                imgIcone.ImageUrl = "images/icones/info.gif";
                return;
            }
            else
                divMensagem.Visible = false;

            //Verifica se foi informado que não será implantado a solução.
            //Se informado que não implantado verifica se foi informado a descrição da não implantação.
            if (ckbSeraImplementado.Checked == false && txtDescricaoNaoImplementar.Text.Trim() == string.Empty)
            {
                divMensagem.Visible = true;
                lblMensagem.Text = "Informe o motivo de não implantar a solução.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                return;
            }
            else
                divMensagem.Visible = false;

            objSolucaoProjeto.Tabela.Valor = txtTabela.Text.Trim();
            objSolucaoProjeto.CodigoIdentificadorTabela.Valor = txtTabelaIdentificador.Text.Trim();
            objSolucaoProjeto.CodigoSolucaoTipo.Valor = dlTipoSolucao.SelectedValue.ToString();
            objSolucaoProjeto.Descricao.Valor = txtDescricaoSolucao.Text.Trim();
            objSolucaoProjeto.DescricaoNaoImplementacao.Valor = txtDescricaoNaoImplementar.Text.Trim();
            //Verifica se não será implementado a solução.
            if (ckbSeraImplementado.Checked == false) objSolucaoProjeto.FlagImplementacao.Valor = "N";
            if (ckbSeraImplementado.Checked == true) objSolucaoProjeto.FlagImplementacao.Valor = "S";

            string strMensagem;

            if (txtCodigoSolucao.Text.Trim() == string.Empty)//Insere nova solução
            {
                objSolucaoProjeto.Codigo.Valor = objSolucaoProjeto.GetMaxId().ToString();
                objSolucaoProjeto.PessoaCodigoInclusao.Valor = user.IDusuario.ToString();
                objSolucaoProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);

                if (objSolucaoProjeto.insere(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    divMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                }
                else
                {
                    SalvarProjeto();

                    divMensagem.Visible = false;
                }

                txtCodigoSolucao.Text = objSolucaoProjeto.Codigo.Valor;
            }
            else//Altera solução existente
            {
                objSolucaoProjeto.Codigo.Valor = txtCodigoSolucao.Text.Trim();
                objSolucaoProjeto.PessoaCodigoAlteracao.Valor = user.IDusuario.ToString();
                objSolucaoProjeto.DataAlteracao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);

                if (objSolucaoProjeto.altera(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    divMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                }
                else
                    divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Eventos
    /// <summary>
    /// Evento do Link Button Solução
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbSolucao_Click(object sender, EventArgs e)
    {
        try
        {

            mtwAbas.ActiveViewIndex = 0;
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Evento do Link Button atividade.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbAtividade_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCodigoSolucao.Text.Trim() != string.Empty)
            {
                int intCodigoProjetoRaiz = 0;
                if (objSolucaoProjeto.VerificaSeExisteProjetoParaSolucao(Convert.ToInt32(txtCodigoSolucao.Text.Trim()), out intCodigoProjetoRaiz) == true)
                {
                    ServiceDesk.Projeto.ClsProjeto.populaNoRaiz(intCodigoProjetoRaiz, tvAtividadeProjeto);
                    tvAtividadeProjeto.ExpandAll();
                    NovoProjeto();
                }
                else
                {
                    NovoProjeto();
                    tvAtividadeProjeto.Nodes.Clear();
                }
            }
            else
            {
                lblMensagem.Text = "Selecione a solução para qual deseja atribuir atividades.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
                return;
            }
            //Preenche combo dos usuários.
            ServiceDesk.Negocio.ClsUsuario.geraDropDownList(dlAtividadeResponsavel, "Selecione o responsável");

            mtwAbas.ActiveViewIndex = 1;
            divMensagem.Visible = false;
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    /// <summary>
    /// Evento do botão salvar solução.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarSolucao();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Evento de exclusão da solução.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            string strMensagem;

            //Verifica se foi selecionado a solução a ser excluída.
            if (txtCodigoSolucao.Text.Trim() == string.Empty)
            {
                lblMensagem.Text = "Informe a solução que deseja excluir.";
                divMensagem.Visible = true;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                return;
            }

            objSolucaoProjeto.Codigo.Valor = txtCodigoSolucao.Text.Trim();

            if (objSolucaoProjeto.exclui(out strMensagem) == false)
            {
                lblMensagem.Text = strMensagem;
                divMensagem.Visible = true;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
            else
            {
                divMensagem.Visible = false;
                Novo();
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;
            imgIcone.ImageUrl = "images/icones/aviso.gif";

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    /// <summary>
    /// Enviar RDM
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmRdm_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarSolucao();

        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;
            imgIcone.ImageUrl = "images/icones/aviso.gif";

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Pesquisa soluções existentes.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            //Verifica se datas.
            if (dpkDataIncioPesquisa.SelectedDate.ToString() != "1/1/0001 00:00:00" && dpkDataFimPesquisa.SelectedDate.ToString() != "1/1/0001 00:00:00")
            {
                if (dpkDataIncioPesquisa.SelectedDate > dpkDataFimPesquisa.SelectedDate)
                {
                    lblMensagem.Text = "A data de inicio da pesquisa não pode ser maior que a data fim.";
                    divMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    return;
                }
                else
                    divMensagem.Visible = false;
            }

            ServiceDesk.Negocio.ClsSolucaoProjeto.geraGridView(gdPesquisaSolucao, dlProcesso.SelectedValue.ToString(), dpkDataIncioPesquisa.SelectedDate, dpkDataFimPesquisa.SelectedDate, txtDescricaoPesquisa.Text);
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;
            imgIcone.ImageUrl = "images/icones/aviso.gif";

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Evento RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdPesquisaSolucao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Exibir")
            {
                GridViewRow objRow = gdPesquisaSolucao.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    string strMsg = string.Empty;

                    Label lblTabela = (Label)objRow.FindControl("lblTabela");
                    txtTabela.Text = lblTabela.Text.Trim();
                    Label lblTabelaIdentificador = (Label)objRow.FindControl("lblTabelaIdentificador");
                    txtTabelaIdentificador.Text = lblTabelaIdentificador.Text.Trim();
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoSolucaoProjeto");

                    //Preenche dados da solução encontrada.
                    PreecheCampos(objSolucaoProjeto.GetDataSolucaoProjeto(Convert.ToInt32(lblCodigo.Text.Trim())), "Solucao");

                    //Preenche dados do processo para qual a solução foi cadastrada.
                    PreecheCampos(objSolucaoProjeto.GetDadosProcessoOrigemSolucao(Convert.ToInt32(txtTabelaIdentificador.Text.Trim()), txtTabela.Text.Trim()), txtTabela.Text.Trim());
                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Limpa os campos que contém dados da solução.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            Novo();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Evento que chama método novo() para limpar os campos da pesquisa de solução
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovaPesquisaSolucao_Click(object sender, EventArgs e)
    {
        NovoFiltro();
    }


    /// <summary>
    /// Projeto - Evento salvar projeto.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAtividadeSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarProjeto();
            int intCodigoProjetoRaiz = 0;
            if (objSolucaoProjeto.VerificaSeExisteProjetoParaSolucao(Convert.ToInt32(txtCodigoSolucao.Text.Trim()), out intCodigoProjetoRaiz) == true)
            {
                ServiceDesk.Projeto.ClsProjeto.populaNoRaiz(intCodigoProjetoRaiz, tvAtividadeProjeto);
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Evento que popula o nó.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvAtividadeProjeto_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            //ServiceDesk.Negocio.ClsItemConfiguracaoTipo.populaNoRaiz(Convert.ToInt32(e.Node.Value), null, e.Node, "itemconfiguracao.aspx?");

            //ServiceDesk.Projeto.ClsProjeto.PopulaNoz(Convert.ToInt32(e.Node.Value), tvAtividadeProjeto, e.Node, false);

            ServiceDesk.Projeto.ClsProjeto.PopulaNoz(Convert.ToInt32(e.Node.Value), tvAtividadeProjeto, e.Node, false);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Projeto - Evento que carrega os dados do projeto selecionado.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvAtividadeProjeto_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            PreecheCampos(objProjeto.GetDadosProjetoPorParamatro(Convert.ToInt32(tvAtividadeProjeto.SelectedValue)), "Projeto");
            tvAtividadeProjeto.SelectedNodeStyle.BackColor = System.Drawing.Color.LightGray;
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Remover
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvAtividadeProjeto_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {

    }

    /// <summary>
    /// Projeto - Evento que chama a exclusão do projeto.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAtividadeExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            ///Verifica se foi informado um projeto ou atividade.
            if (txtCodigoProjeto.Text.Trim() == string.Empty)
            {
                lblMensagem.Text = "Selecione uma atividade/projeto para excluir.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
                return;
            }

            string strMensagem = string.Empty;

            objProjeto.Codigo.Valor = txtCodigoProjeto.Text.Trim();

            if (objProjeto.exclui(out strMensagem) == false)
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else
            {
                //Atualiza SolucaoProjeto com null se o projeto foi excluido.
                int intCodigoProjeto = 0;
                objSolucaoProjeto.VerificaSeExisteProjetoParaSolucao(Convert.ToInt32(txtCodigoSolucao.Text.Trim()), out intCodigoProjeto);
                if (intCodigoProjeto == Convert.ToInt32(objProjeto.Codigo.Valor))
                {
                    objProjeto.UpdateCodigoProjetoSolucao(0, Convert.ToInt32(txtCodigoSolucao.Text.Trim()));
                }

                //Atualiza treeviev
                int intCodigoProjetoRaiz = 0;
                if (objSolucaoProjeto.VerificaSeExisteProjetoParaSolucao(Convert.ToInt32(txtCodigoSolucao.Text.Trim()), out intCodigoProjetoRaiz) == true)
                {
                    ServiceDesk.Projeto.ClsProjeto.populaNoRaiz(intCodigoProjetoRaiz, tvAtividadeProjeto);
                }


                NovoProjeto();
                //ServiceDesk .Projeto .ClsProjeto.populaNos(   
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    /// <summary>
    /// Projeto - Limpa tela para inserção de novos registros.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAtividadeNova_Click(object sender, EventArgs e)
    {
        try
        {
            NovoProjeto();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #endregion
}
