using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe Requisição Serviço
/// </summary>
namespace ServiceDesk.Negocio
{
    public class ClsRequisicaoServico
    {
        #region Construtores
        public ClsRequisicaoServico()
        {
            this.alimentaColecaoCampos();
        }

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsRequisicaoServico(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion

        #endregion

        #region Atributos de uma Requisicao de Servico

        //Colecao de atributos de Requisição de Serviço
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoaCodigoInclusor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objUrgencia = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPrioridade = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objImpacto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objOrigemRequisicaoServico = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoaCodigoAlterador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoaCodigoSolicitante = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAlteracao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataFinalizacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoaCodigoFinalizador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objBaseConhecimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAvaliacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objAvaliacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objObservacaoAvaliacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEnviarNotificacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objVip = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempoDeVida = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempoDeAtendimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEquipe = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTecnico = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEscalado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoRequisicaoServico = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objRequisicaoServicoModelo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSLAInicio = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSLASolucao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNivelAtendimento = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            { return this.objAtributos; }
        }

        /// <summary>
        /// Código da RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descrição da RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// PessoaCodigo do inclusor da RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PessoaCodigoInclusor
        {
            get { return objPessoaCodigoInclusor; }
            set { this.objPessoaCodigoInclusor = value; }
        }

        /// <summary>
        ///Tipo do RequisicaoServico
        ///</summary>
        public ServiceDesk.Banco.ClsAtributo TipoRequisicaoServico
        {
            get { return objTipoRequisicaoServico; }
            set { this.objTipoRequisicaoServico = value; }
        }

        /// <summary>
        /// Urgência do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Urgencia
        {
            get { return objUrgencia; }
            set { this.objUrgencia = value; }
        }

        /// <summary>
        /// Prioridade do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Prioridade
        {
            get { return objPrioridade; }
            set { this.objPrioridade = value; }
        }

        /// <summary>
        /// Impacto do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Impacto
        {
            get { return objImpacto; }
            set { this.objImpacto = value; }
        }

        /// <summary>
        /// Origem do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo OrigemRequisicaoServico
        {
            get { return objOrigemRequisicaoServico; }
            set { this.objOrigemRequisicaoServico = value; }
        }

        /// <summary>
        /// Status do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Status
        {
            get { return objStatus; }
            set { this.objStatus = value; }
        }

        /// <summary>
        /// PessoaCodigo do alterador do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PessoaCodigoAlterador
        {
            get { return objPessoaCodigoAlterador; }
            set { this.objPessoaCodigoAlterador = value; }
        }

        /// <summary>
        /// PessoaCodigo do solicitante do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PessoaCodigoSolicitante
        {
            get { return objPessoaCodigoSolicitante; }
            set { this.objPessoaCodigoSolicitante = value; }
        }

        /// <summary>
        /// Data de inclusão do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInclusao
        {
            get { return objDataInclusao; }
            set { this.objDataInclusao = value; }
        }

        /// <summary>
        /// Data de alteração do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAlteracao
        {
            get { return objDataAlteracao; }
            set { this.objDataAlteracao = value; }
        }

        /// <summary>
        /// Data de finalização do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataFinalizacao
        {
            get { return objDataFinalizacao; }
            set { this.objDataFinalizacao = value; }
        }

        /// <summary>
        /// PessoaCodigo do finalizador do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PessoaCodigoFinalizador
        {
            get { return objPessoaCodigoFinalizador; }
            set { this.objPessoaCodigoFinalizador = value; }
        }

        /// <summary>
        /// Codigo do item na base de conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo BaseConhecimento
        {
            get { return objBaseConhecimento; }
            set { this.objBaseConhecimento = value; }
        }

        /// <summary>
        /// Data da avaliação do atendimento do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAvaliacao
        {
            get { return objDataAvaliacao; }
            set { this.objDataAvaliacao = value; }
        }

        /// <summary>
        /// Nota atribuida ao atendimento do RequisicaoServico pelo solicitante.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Avaliacao
        {
            get { return objAvaliacao; }
            set { this.objAvaliacao = value; }
        }

        /// <summary>
        /// Observação sobre a avaliação do atendimento do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ObservacaoAvaliacao
        {
            get { return objObservacaoAvaliacao; }
            set { this.objObservacaoAvaliacao = value; }
        }

        /// <summary>
        /// Enviar notificação ao solicitante durante o atendimento do RequisicaoServico(S/N)? .
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo EnviarNotificacao
        {
            get { return objEnviarNotificacao; }
            set { this.objEnviarNotificacao = value; }
        }

        /// <summary>
        /// RequisicaoServico VIP(S/N)? .
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Vip
        {
            get { return objVip; }
            set { this.objVip = value; }
        }

        /// <summary>
        /// Tempo de vida do RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TempoDeVida
        {
            get { return objTempoDeVida; }
            set { this.objTempoDeVida = value; }
        }

        /// <summary>
        /// Tempo de atendimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TempoDeAtendimento
        {
            get { return objTempoDeAtendimento; }
            set { this.objTempoDeAtendimento = value; }
        }

        /// <summary>
        /// Equipe atribuida ao RequisicaoServico
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Equipe
        {
            get { return objEquipe; }
            set { this.objEquipe = value; }
        }

        /// <summary>
        /// Técnico atribuido ao RequisicaoServico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tecnico
        {
            get { return objTecnico; }
            set { this.objTecnico = value; }
        }

        /// <summary>
        /// RequisicaoServico Escalado?.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Escalado
        {
            get { return objEscalado; }
            set { this.objEscalado = value; }
        }

        /// <summary>
        /// RequisicaoServico Modelo?.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Modelo
        {
            get { return objRequisicaoServicoModelo; }
            set { this.objRequisicaoServicoModelo = value; }
        }

        /// <summary>
        /// SLA para início de atendimento
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo SLAInicio
        {
            get { return objSLAInicio; }
            set { this.objSLAInicio = value; }
        }

        /// <summary>
        /// SLA para Solucao do Chamado
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo SLASolucao
        {
            get { return objSLASolucao; }
            set { this.objSLASolucao = value; }
        }

        /// <summary>
        /// SLA para Solucao do Chamado
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo NivelAtendimento
        {
            get { return objNivelAtendimento; }
            set { this.objNivelAtendimento = value; }
        }
        #endregion

        #region Métodos

        #region alimentaColecaoCampos

        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "RequisicaoServico";
            objAtributos.DescricaoTabela = "RequisicaoServico";

            objCodigo.Campo = "requisicaoservico_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descrição";
            objDescricao.CampoIdentificador = false;
            objDescricao.CampoObrigatorio = true;
            objDescricao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDescricao);

            objPessoaCodigoInclusor.Campo = "pessoa_codigo_proprietario";
            objPessoaCodigoInclusor.Descricao = "PessoaCodigo Proprietário";
            objPessoaCodigoInclusor.CampoIdentificador = false;
            objPessoaCodigoInclusor.CampoObrigatorio = false;
            objPessoaCodigoInclusor.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPessoaCodigoInclusor);

            objUrgencia.Campo = "urgencia_codigo";
            objUrgencia.Descricao = "Urgência";
            objUrgencia.CampoIdentificador = false;
            objUrgencia.CampoObrigatorio = false;
            objUrgencia.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objUrgencia);

            objPrioridade.Campo = "prioridade_codigo";
            objPrioridade.Descricao = "Prioridade";
            objPrioridade.CampoIdentificador = false;
            objPrioridade.CampoObrigatorio = false;
            objPrioridade.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPrioridade);

            objImpacto.Campo = "impacto_codigo";
            objImpacto.Descricao = "Impacto";
            objImpacto.CampoIdentificador = false;
            objImpacto.CampoObrigatorio = false;
            objImpacto.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objImpacto);

            objOrigemRequisicaoServico.Campo = "requisicao_servico_origem_codigo";
            objOrigemRequisicaoServico.Descricao = "Origem da Requisicao de Serviço";
            objOrigemRequisicaoServico.CampoIdentificador = false;
            objOrigemRequisicaoServico.CampoObrigatorio = false;
            objOrigemRequisicaoServico.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objOrigemRequisicaoServico);

            objTipoRequisicaoServico.Campo = "requisicao_servico_tipo_codigo";
            objTipoRequisicaoServico.Descricao = "Tipo da Requisicao de Servico";
            objTipoRequisicaoServico.CampoIdentificador = false;
            objTipoRequisicaoServico.CampoObrigatorio = false;
            objTipoRequisicaoServico.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTipoRequisicaoServico);

            objStatus.Campo = "status_codigo";
            objStatus.Descricao = "Status";
            objStatus.CampoIdentificador = false;
            objStatus.CampoObrigatorio = false;
            objStatus.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objStatus);

            objPessoaCodigoAlterador.Campo = "pessoa_codigo_alterador";
            objPessoaCodigoAlterador.Descricao = "Código Alterador";
            objPessoaCodigoAlterador.CampoIdentificador = false;
            objPessoaCodigoAlterador.CampoObrigatorio = false;
            objPessoaCodigoAlterador.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPessoaCodigoAlterador);

            objDataInclusao.Campo = "data_inclusao";
            objDataInclusao.Descricao = "Data Inclusão";
            objDataInclusao.CampoIdentificador = false;
            objDataInclusao.CampoObrigatorio = true;
            objDataInclusao.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataInclusao);

            objDataAlteracao.Campo = "data_alteracao";
            objDataAlteracao.Descricao = "Data Alteração";
            objDataAlteracao.CampoIdentificador = false;
            objDataAlteracao.CampoObrigatorio = false;
            objDataAlteracao.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataAlteracao);

            objDataFinalizacao.Campo = "data_finalizacao";
            objDataFinalizacao.Descricao = "Data Finalização";
            objDataFinalizacao.CampoIdentificador = false;
            objDataFinalizacao.CampoObrigatorio = false;
            objDataFinalizacao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDataFinalizacao);

            objPessoaCodigoFinalizador.Campo = "pessoa_codigo_finalizador";
            objPessoaCodigoFinalizador.Descricao = "Código Finalizador";
            objPessoaCodigoFinalizador.CampoIdentificador = false;
            objPessoaCodigoFinalizador.CampoObrigatorio = false;
            objPessoaCodigoFinalizador.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPessoaCodigoFinalizador);

            objPessoaCodigoSolicitante.Campo = "pessoa_codigo_solicitante";
            objPessoaCodigoSolicitante.Descricao = "Código Solicitante";
            objPessoaCodigoSolicitante.CampoIdentificador = false;
            objPessoaCodigoSolicitante.CampoObrigatorio = false;
            objPessoaCodigoSolicitante.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPessoaCodigoSolicitante);

            objBaseConhecimento.Campo = "base_conhecimento";
            objBaseConhecimento.Descricao = "Item da Base Conhecimento";
            objBaseConhecimento.CampoIdentificador = false;
            objBaseConhecimento.CampoObrigatorio = false;
            objBaseConhecimento.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objBaseConhecimento);

            objDataAvaliacao.Campo = "data_avaliacao";
            objDataAvaliacao.Descricao = "Data Avaliação";
            objDataAvaliacao.CampoIdentificador = false;
            objDataAvaliacao.CampoObrigatorio = false;
            objDataAvaliacao.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataAvaliacao);

            objAvaliacao.Campo = "avaliacao";
            objAvaliacao.Descricao = "Avaliação";
            objAvaliacao.CampoIdentificador = false;
            objAvaliacao.CampoObrigatorio = false;
            objAvaliacao.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objAvaliacao);

            objObservacaoAvaliacao.Campo = "observacao_avaliacao";
            objObservacaoAvaliacao.Descricao = "Observações";
            objObservacaoAvaliacao.CampoIdentificador = false;
            objObservacaoAvaliacao.CampoObrigatorio = false;
            objObservacaoAvaliacao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objObservacaoAvaliacao);

            objEnviarNotificacao.Campo = "envia_notificacao";
            objEnviarNotificacao.Descricao = "Notificar?";
            objEnviarNotificacao.CampoIdentificador = false;
            objEnviarNotificacao.CampoObrigatorio = false;
            objEnviarNotificacao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objEnviarNotificacao);

            objVip.Campo = "vip";
            objVip.Descricao = "VIP?";
            objVip.CampoIdentificador = false;
            objVip.CampoObrigatorio = false;
            objVip.Tipo = System.Data.DbType.String;
            objAtributos.Add(objVip);

            objTempoDeVida.Campo = "tempo_vida";
            objTempoDeVida.Descricao = "Tempo de Vida";
            objTempoDeVida.CampoIdentificador = false;
            objTempoDeVida.CampoObrigatorio = false;
            objTempoDeVida.Tipo = System.Data.DbType.Decimal;
            objAtributos.Add(objTempoDeVida);

            objTempoDeAtendimento.Campo = "tempo_atendimento";
            objTempoDeAtendimento.Descricao = "Tempo de Atendimento";
            objTempoDeAtendimento.CampoIdentificador = false;
            objTempoDeAtendimento.CampoObrigatorio = false;
            objTempoDeAtendimento.Tipo = System.Data.DbType.Decimal;
            objAtributos.Add(objTempoDeAtendimento);

            objEquipe.Campo = "equipe_codigo_alocacao";
            objEquipe.Descricao = "Equipe";
            objEquipe.CampoIdentificador = false;
            objEquipe.CampoObrigatorio = false;
            objEquipe.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objEquipe);

            objTecnico.Campo = "pessoa_codigo_alocacao";
            objTecnico.Descricao = "Pessoa alocada para atendimento";
            objTecnico.CampoIdentificador = false;
            objTecnico.CampoObrigatorio = false;
            objTecnico.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTecnico);

            objEscalado.Campo = "escalado";
            objEscalado.Descricao = "Escalado";
            objEscalado.CampoIdentificador = false;
            objEscalado.CampoObrigatorio = false;
            objEscalado.Tipo = System.Data.DbType.String;
            objAtributos.Add(objEscalado);

            objRequisicaoServicoModelo.Campo = "modelo";
            objRequisicaoServicoModelo.Descricao = "Modelo?";
            objRequisicaoServicoModelo.CampoIdentificador = false;
            objRequisicaoServicoModelo.CampoObrigatorio = false;
            objRequisicaoServicoModelo.Tipo = System.Data.DbType.String;
            objAtributos.Add(objRequisicaoServicoModelo);

            objSLASolucao.Campo = "tempo_sla_fim";
            objSLASolucao.Descricao = "modelo";
            objSLASolucao.CampoIdentificador = false;
            objSLASolucao.CampoObrigatorio = false;
            objSLASolucao.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objSLASolucao);

            objSLAInicio.Campo = "tempo_sla_inicio";
            objSLAInicio.Descricao = "modelo";
            objSLAInicio.CampoIdentificador = false;
            objSLAInicio.CampoObrigatorio = false;
            objSLAInicio.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objSLAInicio);

            objNivelAtendimento.Campo = "nivel_atendimento_codigo";
            objNivelAtendimento.Descricao = "Nível de Atendimento";
            objNivelAtendimento.CampoIdentificador = false;
            objNivelAtendimento.CampoObrigatorio = false;
            objNivelAtendimento.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objNivelAtendimento);

        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere uma nova Requisição de Serviço
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objDescricao.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar a Descrição do Item.";
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Item inserido com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objRequisicaoServico.objAtributos);
                objRequisicaoServico = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsRequisicaoServico objRequisicaoServico)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objRequisicaoServico.objAtributos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo altera
        /// <summary>
        /// Método que altera uma Requisição de Serviço
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Item atualizado com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo exclui
        /// <summary>
        /// Método que exclui uma Requisição de serviço
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui()
        {
            try
            {
                string strMsg = string.Empty;

                //Valida a exclusão.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    objBanco = null;
                    return true;
                }
                else
                {
                    objBanco = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region getRequisicaoServico
        /// <summary>
        /// Pega os dados de um determinada Requisicao de Servico
        /// </summary>
        /// <param name="strCodigoRequisicaoServico">Codigo da Requisicao de Serviço</param>
        static public System.Data.SqlClient.SqlDataReader getRequisicaoServico(String strCodigoRequisicaoServico)
        {
            try
            {
                String strSql = String.Empty;
                System.Data.SqlClient.SqlDataReader objSqlDataReader = null;

                strSql = "SELECT * FROM RequisicaoServico";
                strSql += " WHERE requisicaoservico_codigo = '" + strCodigoRequisicaoServico + "'";
                objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                return objSqlDataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region metodo geraGridViewChamadosVinculados
        /// <summary>
        /// Gera uma nova geraGridView de de chamados vinculados a Requisição de Serviço.
        /// </summary>
        /// <param name="objGridView">GridView</param>
        /// <param name="strCodigoRequisicaoServico">Codigo da Requisição de Serviço</param>
        public static void geraGridViewChamadosVinculados(System.Web.UI.WebControls.GridView objGridView, String strCodigoRequisicaoServico)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                String strSql = "Select C.chamado_codigo, C.descricao ";
                strSql += "from chamado C, RequisicaoServicoChamado RSC ";
                strSql += "Where C.chamado_codigo = RSC.chamado_codigo ";
                strSql += "and RSC.requisicaoservico_codigo = '" + strCodigoRequisicaoServico + "' ";
                strSql += "order by RSC.chamado_codigo";

                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region RemoveRelacaoRequisicaoServicoChamado
        /// <summary>
        /// Adiciona um Item de Configuracao a Requisição de Serviço
        /// </summary>
        /// <param name="strCodigoRequisicaoServico">Codigo da Requisição de Serviço</param>
        /// <param name="strCodigoChamado">Codigo do Chamado a ser desvinculado da Requisição de Serviço</param>
        static public void RemoveRelacaoRequisicaoServicoChamado(String strCodigoRequisicaoServico, string strCodigoChamado)
        {
            try
            {
                String strSql = String.Empty;
                String strCodigoRequisicaoItemConfiguracao = string.Empty;

                if ((strCodigoRequisicaoServico != string.Empty) && (strCodigoChamado != String.Empty))
                {
                    strSql = "DELETE FROM RequisicaoServicoChamado ";
                    strSql += "WHERE requisicaoservico_codigo = '" + strCodigoRequisicaoServico + "' and chamado_codigo = '" + strCodigoChamado + "' ";
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    objBanco.executaSQL(strSql);
                    objBanco = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico();
                objDropDownList.DataTextField = objRequisicaoServico.objDescricao.Campo;
                objDropDownList.DataValueField = objRequisicaoServico.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objRequisicaoServico.objAtributos);
                objRequisicaoServico = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "--";
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo geraDropDownListIncidentePreDefinido
        /// <summary>
        /// Gera um novo DropDownList listando apenas as Requisições de Serviço marcadas como modelo, 
        /// de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownListIncidentePreDefinido(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico();
                objDropDownList.DataTextField = objRequisicaoServico.objDescricao.Campo;
                objDropDownList.DataValueField = objRequisicaoServico.objCodigo.Campo;

                objRequisicaoServico.objCodigo.CampoIdentificador = false;
                objRequisicaoServico.objCodigo.CampoObrigatorio = false;
                objRequisicaoServico.objRequisicaoServicoModelo.Valor = "S";
                objRequisicaoServico.objRequisicaoServicoModelo.CampoIdentificador = true;
                objRequisicaoServico.objRequisicaoServicoModelo.CampoObrigatorio = true;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objRequisicaoServico.objAtributos, true);
                objRequisicaoServico = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "Selecione o modelo";
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);

                objRequisicaoServico = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region herdaInformacoesChamado(string strCodigoChamado)
        /// <summary>
        /// Herda informações de um determinado chamado e insere no objeto do tipo Requisição Serviço
        /// </summary>
        /// <param name="strCodigoChamado">Codigo do chamado do qual se deseja herdar os dados</param>
        public static ClsRequisicaoServico herdaInformacoesChamado(string strCodigoChamado)
        {
            ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico();

            //busca dados do chamado
            SqlDataReader objReader = ServiceDesk.Negocio.ClsChamado.getChamado(strCodigoChamado);

            //copia os dados do chamado para o incidente    
            if (objReader.Read())
            {
                //Validação 
                if (objReader["impacto_codigo"].ToString() == string.Empty)
                {
                    Exception myEx = new Exception("Não foi possivel herdar as informações do chamado. Informe o impacto do chamado e tente novamente.");
                    throw myEx;
                }

                //cópia
                try
                {                    
                    string strFormatoDataInclusao = ClsParametro.DataInclusao;
                    string strDataMinimaSistema = ClsParametro.DataMinimaSistema;

                    objRequisicaoServico.PessoaCodigoInclusor.Valor = objReader["pessoa_codigo_proprietario"].ToString();
                    objRequisicaoServico.PessoaCodigoSolicitante.Valor = objReader["pessoa_codigo_solicitante"].ToString();
                    objRequisicaoServico.Descricao.Valor = objReader["descricao"].ToString();

                    if (objReader["data_inclusao"].ToString() != string.Empty)
                    {
                        objRequisicaoServico.DataInclusao.Valor = Convert.ToDateTime(objReader["data_inclusao"]).ToString(strFormatoDataInclusao, null);
                    }
                    else
                    {
                        objRequisicaoServico.DataInclusao.Valor = strDataMinimaSistema;
                    }

                    objRequisicaoServico.Status.Valor = SServiceDesk.Negocio.ClsWorkFlow.primeiroStatus("REQUISICAOSERVICO").ToString();
                    objRequisicaoServico.Urgencia.Valor = objReader["urgencia_codigo"].ToString();
                    objRequisicaoServico.Prioridade.Valor = objReader["prioridade_codigo"].ToString();
                    objRequisicaoServico.Impacto.Valor = objReader["impacto_codigo"].ToString();
                    objRequisicaoServico.TipoRequisicaoServico.Valor = objReader["chamado_tipo_codigo"].ToString();
                    if (objReader["origem_chamado_tipo_codigo"].ToString() != string.Empty)
                    {
                        objRequisicaoServico.OrigemRequisicaoServico.Valor = objReader["origem_chamado_tipo_codigo"].ToString();
                    }
                    objRequisicaoServico.Escalado.Valor = objReader["escalado"].ToString();
                    objRequisicaoServico.Equipe.Valor = objReader["equipe_codigo_alocacao"].ToString();
                    objRequisicaoServico.Tecnico.Valor = objReader["pessoa_codigo_alocacao"].ToString();
                    objRequisicaoServico.NivelAtendimento.Valor = objReader["nivel_atendimento_codigo"].ToString();

                }
                catch
                {
                    Exception myEx = new Exception("Não foi possivel herdar as informações do chamado.");
                    throw myEx;
                }

            }
            objReader = null;

            return objRequisicaoServico;

        }
        #endregion

        #region herdaICChamado
        /// <summary>
        /// Copia os ICs de um chamado para a Requisição de Serviço informada
        /// </summary>
        /// <param name="strCodigoRequisicaoServico">Codigo do Incidente</param>
        /// <param name="strCodigoChamado">Codigo do Chamado</param>
        /// <returns>true se copia ok.</returns>        
        static public void herdaICChamado(String strCodigoChamado, String strCodigoRequisicaoServico)
        {
            String strSql = String.Empty;
            try
            {
                strSql = "INSERT INTO RequisicaoServicoItemConfiguracao ";
                strSql += "SELECT item_configuracao_codigo, '" + strCodigoRequisicaoServico + "' as requisicaoservico_codigo ";
                strSql += "FROM ChamadoItemConfiguracao ";
                strSql += "WHERE chamado_codigo ='" + strCodigoChamado + "' ";
                strSql += "AND item_configuracao_codigo NOT IN ( ";
                strSql += "Select item_configuracao_codigo ";
                strSql += "From RequisicaoServicoItemConfiguracao ";
                strSql += "WHERE requisicaoservico_codigo = '" + strCodigoRequisicaoServico + "' ) ";

                ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
                Banco.executaSQL(strSql);
                Banco = null;
            }
            catch 
            {
                Exception myEx = new Exception("Não foi possivel herdar os itens de configuração do chamado.");
                throw myEx;
            }
        }

        #endregion

        #region AdicionaRelacaoRequisicaoServicoChamado(strCodigoChamado, strCodigoIncidente);
        /// <summary>
        /// Cria o relacionamento entre um chamado e um incidente
        /// </summary>
        /// <param name="strCodigoRequisicaoServico">Codigo da Requisicao Servico</param>
        /// <param name="strCodigoChamado">Codigo do Chamado</param>
        /// <returns>true se ok.</returns>
        static public void AdicionaRelacaoRequisicaoServicoChamado(String strCodigoChamado, String strCodigoRequisicaoServico)
        {
            String strSql = String.Empty;
            try
            {
                strSql = "INSERT INTO RequisicaoServicoChamado ";
                strSql += "(chamado_codigo, requisicaoservico_codigo) ";
                strSql += "VALUES ";
                strSql += "('" + strCodigoChamado + "','" + strCodigoRequisicaoServico + "') ";

                ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
                Banco.executaSQL(strSql);
                Banco = null;
            }
            catch 
            {
                Exception myEx = new Exception("Não foi possivel relacionar o incidente ao chamado.");
                throw myEx;
            }
        }

        #endregion

        #region metodo criaRequisicaoServicoBaseadoChamado
        /// <summary>
        /// Cria uma requisicao de servico vinculado ao chamado e baseado nas informações do chamado.
        /// </summary>
        /// <param name="strCodigoChamado">Codigo do Chamado</param>
        /// <returns>False se houve erro ou TRUE se foi criado com sucesso</returns>
        public static bool criaRequisicaoServicoBaseadoChamado(String strCodigoChamado, out String strCodigoRequisicaoServicoCriado)
        {
            bool bSucesso = false;
            String strMensagem = string.Empty;
            strCodigoRequisicaoServicoCriado = string.Empty;

            try
            {
                ClsRequisicaoServico objRequisicaoServico = ClsRequisicaoServico.herdaInformacoesChamado(strCodigoChamado);
                ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                objIdentificador.Tabela.Valor = objRequisicaoServico.Atributos.NomeTabela;
                objRequisicaoServico.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objRequisicaoServico.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                    strCodigoRequisicaoServicoCriado = objIdentificador.Codigo.Valor.ToString();
                    //Relaciona Requisicao de Servico ao chamado
                    ClsRequisicaoServico.AdicionaRelacaoRequisicaoServicoChamado(strCodigoChamado, strCodigoRequisicaoServicoCriado);

                    SServiceDesk.Negocio.ClsWorkFlow.salvaRepercusao(Convert.ToInt32(objRequisicaoServico.Codigo.Valor), objRequisicaoServico.Atributos.NomeTabela, objRequisicaoServico.Status.Valor);
                  
                    //Copia os CIs do chamado para o incidente criado
                    ClsRequisicaoServico.herdaICChamado(strCodigoChamado, strCodigoRequisicaoServicoCriado);

                    //=================================================================================//
                    // - O que: Atualiza a tabela StatusLog para informar os dados sobre o status que o
                    // Incidente possui.
                    // - Quem: Fernanda Passos
                    // - Quando: 15/03/2006 ás 16:04
                    //=================================================================================//
                    SServiceDesk.Negocio.ClsWorkFlow.gravaLog(Convert.ToInt32(objRequisicaoServico.Codigo.Valor.Trim()), objRequisicaoServico.Atributos.NomeTabela.Trim(), "0", objRequisicaoServico.Status.Valor.Trim());
                    //=================================================================================//


                    bSucesso = true;
                }
                objRequisicaoServico = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return bSucesso;
        }
        #endregion

        #region metodo geraGridViewNotaAtendimento
        /// <summary>
        /// Gera uma nova geraGridView com os registros de nota de atendimento relacionados ao chamado informado.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        /// <param name="strCodigoRequisicaoServico">Codigo da Requisicao de Servico</param>
        public static void geraGridViewNotaAtendimento(System.Web.UI.WebControls.GridView objGridView, String strCodigoRequisicaoServico)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select nota_codigo, data_inclusao, pessoa_codigo, nota ";
                strSql += "from nota ";
                strSql += "where ";
                strSql += " identificador_tabela =  '" + strCodigoRequisicaoServico + "' ";
                strSql += " and tabela =  'RequisicaoServico' ";
                strSql += " order by data_inclusao DESC";
                              
                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;//ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataBind();
                objDataSet.Dispose();              
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo geraDropDownListIncidentePreDefinido
        /// <summary>
        /// Gera um novo DropDownList listando apenas incidentes marcados como modelo, 
        /// de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownListRequisicaoServicoPreDefinido(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico();
                objDropDownList.DataTextField = objRequisicaoServico.objDescricao.Campo;
                objDropDownList.DataValueField = objRequisicaoServico.objCodigo.Campo;

                objRequisicaoServico.objCodigo.CampoIdentificador = false;
                objRequisicaoServico.objCodigo.CampoObrigatorio = false;
                objRequisicaoServico.objRequisicaoServicoModelo.Valor = "S";
                objRequisicaoServico.objRequisicaoServicoModelo.CampoIdentificador = true;
                objRequisicaoServico.objRequisicaoServicoModelo.CampoObrigatorio = true;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objRequisicaoServico.objAtributos, true);
                objRequisicaoServico = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "Selecione o modelo";
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);

                objRequisicaoServico = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Retorna o código do proprietário
        /// <summary>
        /// Retorna o código do proprietário
        /// </summary>
        /// <returns>Retorna o código do proprietário</returns>
        /// <param name="intCodigoRS">Código da requisição de serviço</param> 
        public static string GetCodigoProprietario(int intCodigoRS)
        {
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strValor = objBanco.retornaValorCampo("RequisicaoServico", "pessoa_codigo_proprietario", "requisicaoservico_codigo = " + intCodigoRS + "");
                objBanco = null;
                if (strValor == string.Empty) return string.Empty; else return strValor.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 
      
        #endregion Metodos
    }
}
