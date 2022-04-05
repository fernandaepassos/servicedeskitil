using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsChamado
/// </summary>
/// 

namespace ServiceDesk.Negocio
{

    public class ClsChamado
    {

        #region Construtores

        public ClsChamado()
        {
            this.alimentaColecaoCampos();
        }

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsChamado(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion


        #endregion

        #region Atributos de um Chamado

        //Colecao de atributos de Chamado
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objMatriculaInclusor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objUrgencia = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPrioridade = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objImpacto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objOrigemChamado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objMatriculaAlterador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objMatriculaSolicitante = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAlteracao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataFinalizacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objMatriculaFinalizador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objBaseConhecimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAvaliacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objAvaliacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objObservacaoAvaliacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEnviarNotificacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objVip = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAgendamento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempoDeVida = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempoDeAtendimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEquipe = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTecnico = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEscalado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoChamado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSolucionadoPrimeiroContato = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objChamadoModelo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSLAInicio = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSLASolucao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNivelAtendimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objAcao = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades

        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            { return this.objAtributos; }
        }

        /// <summary>
        /// Código da Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descrição da Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Matricula do inclusor da Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo MatriculaInclusor
        {
            get { return objMatriculaInclusor; }
            set { this.objMatriculaInclusor = value; }
        }

        /// <summary>
        ///Tipo do Chamado
        ///</summary>
        public ServiceDesk.Banco.ClsAtributo TipoChamado
        {
            get { return objTipoChamado; }
            set { this.objTipoChamado = value; }
        }

        /// <summary>
        /// Urgência do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Urgencia
        {
            get { return objUrgencia; }
            set { this.objUrgencia = value; }
        }

        /// <summary>
        /// Prioridade do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Prioridade
        {
            get { return objPrioridade; }
            set { this.objPrioridade = value; }
        }

        /// <summary>
        /// Impacto do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Impacto
        {
            get { return objImpacto; }
            set { this.objImpacto = value; }
        }

        /// <summary>
        /// Origem do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo OrigemChamado
        {
            get { return objOrigemChamado; }
            set { this.objOrigemChamado = value; }
        }

        /// <summary>
        /// Status do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Status
        {
            get { return objStatus; }
            set { this.objStatus = value; }
        }

        /// <summary>
        /// Matricula do alterador do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo MatriculaAlterador
        {
            get { return objMatriculaAlterador; }
            set { this.objMatriculaAlterador = value; }
        }

        /// <summary>
        /// Matricula do solicitante do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo MatriculaSolicitante
        {
            get { return objMatriculaSolicitante; }
            set { this.objMatriculaSolicitante = value; }
        }

        /// <summary>
        /// Data de inclusão do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInclusao
        {
            get { return objDataInclusao; }
            set { this.objDataInclusao = value; }
        }

        /// <summary>
        /// Data de alteração do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAlteracao
        {
            get { return objDataAlteracao; }
            set { this.objDataAlteracao = value; }
        }

        /// <summary>
        /// Data de finalização do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataFinalizacao
        {
            get { return objDataFinalizacao; }
            set { this.objDataFinalizacao = value; }
        }

        /// <summary>
        /// Matricula do finalizador do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo MatriculaFinalizador
        {
            get { return objMatriculaFinalizador; }
            set { this.objMatriculaFinalizador = value; }
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
        /// Data da avaliação do atendimento do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAvaliacao
        {
            get { return objDataAvaliacao; }
            set { this.objDataAvaliacao = value; }
        }

        /// <summary>
        /// Nota atribuida ao atendimento do Chamado pelo solicitante.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Avaliacao
        {
            get { return objAvaliacao; }
            set { this.objAvaliacao = value; }
        }

        /// <summary>
        /// Observação sobre a avaliação do atendimento do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ObservacaoAvaliacao
        {
            get { return objObservacaoAvaliacao; }
            set { this.objObservacaoAvaliacao = value; }
        }

        /// <summary>
        /// Enviar notificação ao solicitante durante o atendimento do Chamado(S/N)? .
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo EnviarNotificacao
        {
            get { return objEnviarNotificacao; }
            set { this.objEnviarNotificacao = value; }
        }

        /// <summary>
        /// Chamado VIP(S/N)? .
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Vip
        {
            get { return objVip; }
            set { this.objVip = value; }
        }

        /// <summary>
        /// Data para a qual foi agendado o atendimento do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAgendamento
        {
            get { return objDataAgendamento; }
            set { this.objDataAgendamento = value; }
        }

        /// <summary>
        /// Tempo de vida do Chamado.
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
        /// Equipe atribuida ao chamado
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Equipe
        {
            get { return objEquipe; }
            set { this.objEquipe = value; }
        }

        /// <summary>
        /// Técnico atribuido ao chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tecnico
        {
            get { return objTecnico; }
            set { this.objTecnico = value; }
        }

        /// <summary>
        /// Chamado Escalado?.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Escalado
        {
            get { return objEscalado; }
            set { this.objEscalado = value; }
        }

        /// <summary>
        /// Chamado Solucionado no Primeiro Contato?
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo SolucionadoPrimeiroContato
        {
            get { return objSolucionadoPrimeiroContato; }
            set { this.objSolucionadoPrimeiroContato = value; }
        }

        /// <summary>
        /// Chamado Modelo?
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ChamadoModelo
        {
            get { return objChamadoModelo; }
            set { this.objChamadoModelo = value; }
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
        /// Nivel de Atendimento em que o chamado se encontra
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo NivelAtendimento
        {
            get { return objNivelAtendimento; }
            set { this.objNivelAtendimento = value; }
        }


        /// <summary>
        /// Ação sobre chamado
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Acao
        {
            get { return objAcao; }
            set { this.objAcao = value; }
        }

        #endregion

        #region Métodos

        #region Retorna o código do proprietário
        /// <summary>
        /// Retorna o código do proprietário
        /// </summary>
        /// <returns>Retorna o código do proprietário</returns>
        /// <param name="intCodigoChamado">Código do chamado</param> 
        public static string GetCodigoProprietario(int intCodigoChamado)
        {
            try
            {
                Banco.ClsBanco objBanco = new Banco.ClsBanco();
                string strValor = objBanco.retornaValorCampo("Chamado", "pessoa_codigo_proprietario", "chamado_codigo = " + intCodigoChamado + "");
                objBanco = null;

                return string.IsNullOrEmpty(strValor) ? strValor.Trim() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region alimentaColecaoCampos
        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "Chamado";
            objAtributos.DescricaoTabela = "Chamado";

            objCodigo.Campo = "chamado_codigo";
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

            objMatriculaInclusor.Campo = "pessoa_codigo_proprietario";
            objMatriculaInclusor.Descricao = "Matricula Proprietário";
            objMatriculaInclusor.CampoIdentificador = false;
            objMatriculaInclusor.CampoObrigatorio = false;
            objMatriculaInclusor.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objMatriculaInclusor);

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

            objOrigemChamado.Campo = "origem_chamado_tipo_codigo";
            objOrigemChamado.Descricao = "Origem do Chamado";
            objOrigemChamado.CampoIdentificador = false;
            objOrigemChamado.CampoObrigatorio = false;
            objOrigemChamado.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objOrigemChamado);

            objTipoChamado.Campo = "chamado_tipo_codigo";
            objTipoChamado.Descricao = "Tipo de Chamado";
            objTipoChamado.CampoIdentificador = false;
            objTipoChamado.CampoObrigatorio = false;
            objTipoChamado.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTipoChamado);

            objStatus.Campo = "status_codigo";
            objStatus.Descricao = "Status";
            objStatus.CampoIdentificador = false;
            objStatus.CampoObrigatorio = false;
            objStatus.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objStatus);

            objMatriculaAlterador.Campo = "pessoa_codigo_alterador";
            objMatriculaAlterador.Descricao = "Código Alterador";
            objMatriculaAlterador.CampoIdentificador = false;
            objMatriculaAlterador.CampoObrigatorio = false;
            objMatriculaAlterador.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objMatriculaAlterador);

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
            objDataFinalizacao.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataFinalizacao);

            objMatriculaFinalizador.Campo = "pessoa_codigo_finalizador";
            objMatriculaFinalizador.Descricao = "Código Finalizador";
            objMatriculaFinalizador.CampoIdentificador = false;
            objMatriculaFinalizador.CampoObrigatorio = false;
            objMatriculaFinalizador.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objMatriculaFinalizador);

            objMatriculaSolicitante.Campo = "pessoa_codigo_solicitante";
            objMatriculaSolicitante.Descricao = "Código Solicitante";
            objMatriculaSolicitante.CampoIdentificador = false;
            objMatriculaSolicitante.CampoObrigatorio = false;
            objMatriculaSolicitante.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objMatriculaSolicitante);

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

            objDataAgendamento.Campo = "data_agendamento";
            objDataAgendamento.Descricao = "Data Agendamento";
            objDataAgendamento.CampoIdentificador = false;
            objDataAgendamento.CampoObrigatorio = false;
            objDataAgendamento.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataAgendamento);

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

            objSolucionadoPrimeiroContato.Campo = "solucionado_primeiro_contato";
            objSolucionadoPrimeiroContato.Descricao = "Flag solucionado no primeiro contato";
            objSolucionadoPrimeiroContato.CampoIdentificador = false;
            objSolucionadoPrimeiroContato.CampoObrigatorio = false;
            objSolucionadoPrimeiroContato.Tipo = System.Data.DbType.String;
            objAtributos.Add(objSolucionadoPrimeiroContato);

            objChamadoModelo.Campo = "modelo";
            objChamadoModelo.Descricao = "Modelo";
            objChamadoModelo.CampoIdentificador = false;
            objChamadoModelo.CampoObrigatorio = false;
            objChamadoModelo.Tipo = System.Data.DbType.String;
            objAtributos.Add(objChamadoModelo);

            objNivelAtendimento.Campo = "nivel_atendimento_codigo";
            objNivelAtendimento.Descricao = "Nível de Atendimento";
            objNivelAtendimento.CampoIdentificador = false;
            objNivelAtendimento.CampoObrigatorio = false;
            objNivelAtendimento.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objNivelAtendimento);

            objAcao.Campo = "acao_codigo";
            objAcao.Descricao = "Ação";
            objAcao.CampoIdentificador = false;
            objAcao.CampoObrigatorio = false;
            objAcao.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objAcao);
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere um novo chamado
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

                        // Grava Log de Operação
                        ClsLog.insereLog(ClsLog.enumTipoLog.INSERT, this.MatriculaAlterador.Valor, "Chamado", this.objCodigo.Valor, String.Empty);

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
                ClsChamado objChamado = new ClsChamado();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objChamado.objAtributos);
                objChamado = null;
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
        /// <param name="objChamado">objeto da classe chamado</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsChamado objChamado)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objChamado.objAtributos);
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
        /// <param name="objChamado">objeto da classe chamado</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsChamado objChamado, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objChamado.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewNaoFinalizadosPorPrioridade
        /// <summary>
        /// Gera Grid View dos chamados nao finalizados por Prioridade
        /// </summary>
        /// <param name="objGridView"></param>
        public static void geraGridViewNaoFinalizadosPorPrioridade(GridView objGridView, int intCodigoPrioridade)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = @"SELECT * FROM chamado WHERE status_codigo <> {0}
                                  AND prioridade_codigo = {1}
                                  ORDER BY data_inclusao asc";

                strSql = string.Format(strSql, ClsStatus.getCodigoStatusFinalizado(), intCodigoPrioridade);

                DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo altera
        /// <summary>
        /// Método que altera
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                String strMensagemLog = string.Empty;
                bool bolRetorno = false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                if (objDescricao.Valor.Trim() == string.Empty)
                {
                    strMensagem = "Por favor, informe a descrição do chamado.";
                    bolRetorno = false;
                }
                else if (objBanco.alteraColecao(this.objAtributos))
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

        #region metodo alteraAtributo
        /// <summary>
        /// Método que altera
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool alteraAtributo(string strChamadoCodigo, string strAtributoNome, string strValor)
        {
            string strSql = string.Empty;
            bool bolRetorno = false;


            if (strAtributoNome != string.Empty && strChamadoCodigo != string.Empty)
            {
                strSql = "UPDATE chamado SET ";
                strSql = strSql + strAtributoNome + " = '" + strValor + "'";
                strSql = strSql + " WHERE chamado_codigo = " + strChamadoCodigo;
            }

            try
            {
                ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
                banco.executaSQL(strSql);
                bolRetorno = true;
                banco = null;

            }
            catch (Exception ex)
            {
                bolRetorno = false;
                throw ex;
            }

            return bolRetorno;

        }
        #endregion

        #region metodo exclui
        /// <summary>
        /// Método que exclui um tipo de Urgência
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui()
        {
            try
            {
                string strMsg = string.Empty;

                //Valida a exclusão.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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

        #region getChamado
        /// <summary>
        /// Pega os dados de um determinado chamado
        /// </summary>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        static public System.Data.SqlClient.SqlDataReader getChamado(String strCodigoChamado)
        {
            try
            {
                String strSql = String.Empty;
                System.Data.SqlClient.SqlDataReader objSqlDataReader = null;

                try
                {
                    strSql = "SELECT * FROM chamado";
                    strSql += " WHERE chamado_codigo = '" + strCodigoChamado + "'";
                    objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                }
                catch
                { }
                return objSqlDataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AdicionaItemConfiguracao
        /// <summary>
        /// Adiciona um item de configuracao ao chamado.
        /// </summary>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        static public void AdicionaItemConfiguracao(String strCodigoChamado, string strCodigoItem)
        {
            try
            {
                string strSql = string.Empty;
                if (strCodigoChamado != string.Empty)
                {
                    strSql = "INSERT INTO ChamadoIC ";
                    strSql += " (chamado_codigo, ic_codigo) ";
                    strSql += " SELECT " + strCodigoChamado + ", ic_codigo ";
                    strSql += " FROM IC WHERE ic_codigo IN (" + strCodigoItem + ")";

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

        #region RemoveItemConfiguracao

        /// <summary>
        /// Adiciona um item de configuracao ao chamado.
        /// </summary>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        static public void RemoveItemConfiguracao(String strCodigoChamado, string strCodigoItem)
        {
            try
            {
                string strSql = string.Empty;
                String strCodigoChamadoItemConfiguracao = string.Empty;

                if ((strCodigoChamado != string.Empty) && (strCodigoItem != ""))
                {
                    strSql = "DELETE FROM ChamadoIC ";
                    strSql += "WHERE chamado_codigo = '" + strCodigoChamado + "' and ic_codigo = '" + strCodigoItem + "' ";
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

        #region metodo geraGridViewIC
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        public static void geraGridViewIC(System.Web.UI.WebControls.GridView objGridView, String strCodigoChamado)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select IC.ic_codigo, IC.Nome ";
                strSql += "from IC IC, ChamadoIC CIC ";
                strSql += "Where CIC.ic_codigo = IC.ic_codigo ";
                strSql += "and CIC.chamado_codigo = '" + strCodigoChamado + "'";

                DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewIncidentes
        /// <summary>
        /// Gera uma nova geraGridView com os incidentes relacionados ao chamado informado.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        public static void geraGridViewIncidentes(System.Web.UI.WebControls.GridView objGridView, String strCodigoChamado)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select I.incidente_codigo, I.descricao, I.status_codigo as status  ";
                strSql += "from incidenteChamado IC, incidente I ";
                strSql += "where  ";
                strSql += "IC.incidente_codigo = I.incidente_codigo ";
                strSql += "AND IC.chamado_codigo =  '" + strCodigoChamado + "' ";
                strSql += " order by I.data_inclusao DESC";

                DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewLigacoes
        /// <summary>
        /// Gera uma nova geraGridView com os registros de ligacao relacionados ao chamado informado.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        public static void geraGridViewLigacoes(System.Web.UI.WebControls.GridView objGridView, String strCodigoChamado)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select ligacao_codigo, data_inclusao, pessoa_codigo_proprietario, descricao ";
                strSql += "from ligacao ";
                strSql += "where ";
                strSql += " chamado_codigo =  '" + strCodigoChamado + "' ";
                strSql += " order by data_inclusao DESC";

                DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewNotaAtendimento
        /// <summary>
        /// Gera uma nova geraGridView com os registros de nota de atendimento relacionados ao chamado informado.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        /// <param name="strCodigoChamado">Codigo do incidente</param>
        public static void geraGridViewNotaAtendimento(System.Web.UI.WebControls.GridView objGridView, String strCodigoChamado)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select nota_codigo, data_inclusao, pessoa_codigo, nota ";
                strSql += "from nota ";
                strSql += "where ";
                strSql += " identificador_tabela =  '" + strCodigoChamado + "' ";
                strSql += " and tabela =  'Chamado' ";
                strSql += " order by data_inclusao DESC";

                DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewRequisicaoServico
        /// <summary>
        /// Gera uma nova geraGridView com as requisicoes de servico relacionadas ao chamado
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        public static void geraGridViewRequisicaoServico(System.Web.UI.WebControls.GridView objGridView, String strCodigoChamado)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select I.requisicaoservico_codigo, I.descricao, I.status_codigo as status  ";
                strSql += "from RequisicaoServicoChamado IC, requisicaoservico I ";
                strSql += "where  ";
                strSql += "IC.requisicaoservico_codigo = I.requisicaoservico_codigo ";
                strSql += "AND IC.chamado_codigo =  '" + strCodigoChamado + "' ";
                strSql += " order by I.data_inclusao DESC";

                DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewRequisicaoMudanca
        /// <summary>
        /// Gera uma nova geraGridView com as requisicoes de Mudanca relacionadas ao chamado
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="strCodigoChamado">Codigo do chamado</param>
        public static void geraGridViewRequisicaoMudanca(System.Web.UI.WebControls.GridView objGridView, String strCodigoChamado)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select I.requisicaomudanca_codigo, I.descricao, I.status_codigo as status  ";
                strSql += "from RequisicaoMudancaChamado IC, requisicaomudanca I ";
                strSql += "where  ";
                strSql += "IC.requisicaomudanca_codigo = I.requisicaomudanca_codigo ";
                strSql += "AND IC.chamado_codigo =  '" + strCodigoChamado + "' ";
                strSql += " order by I.data_inclusao DESC";

                DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewQuery
        /// <summary>
        /// Gera uma nova geraGridView utilizado uma query informada.
        /// </summary>
        /// <param name="objGridView">Grid View</param>
        /// <param name="strSql">Query que retornará os resultados da gridview.</param>
        public static void geraGridViewQuery(System.Web.UI.WebControls.GridView objGridView, String strSql)
        {
            objGridView.AutoGenerateColumns = false;

            try
            {
                //Executa a query e preenche a gridview.
                DataSet ds = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = ds;
                objGridView.DataBind();
                ds.Dispose();
                ds = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo geraGridViewQuery
        /// <summary>
        /// Gera uma nova geraGridView utilizado uma query informada.
        /// </summary>
        /// <param name="objGridView">Grid View</param>
        /// <param name="strSql">Query que retornará os resultados da gridview.</param>
        /// <param name="strMensagem">Mensagem de confirmação de sucesso na operação ou falha</param>
        public static bool geraGridViewQuery(System.Web.UI.WebControls.GridView objGridView, String strSql, out String strMensagem)
        {
            try
            {
                bool bOk;
                strMensagem = string.Empty;
                objGridView.AutoGenerateColumns = false;

                try
                {
                    SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    if (!objDataReader.Read())
                    {
                        strMensagem = "A pesquisa não retornou resultados.";
                        bOk = false;
                    }
                    else
                    { bOk = true; }
                    objDataReader.Close();
                    objDataReader.Dispose();
                    //Executa a query e preenche a gridview.
                    DataSet ds = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                    objGridView.DataSource = ds;
                    objGridView.DataBind();
                    ds.Dispose();
                    ds = null;

                }
                catch
                {

                    strMensagem = "Ocorreu um erro ao realizar a pesquisa.";
                    bOk = false;
                }

                return bOk;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo PossuiIncidenteVinculado
        /// <summary>
        /// Verifica se o chamado possui ao menos 1 incidente vinculado
        /// </summary>
        ///<param name="strCodigoChamado">Código do Chamado</param>
        public static bool PossuiIncidenteVinculado(String strCodigoChamado)
        {
            bool bPossuiIncidente = false;
            String strSql = "Select * from incidentechamado where chamado_codigo = " + strCodigoChamado;
            try
            {
                //Executa a query e preenche a gridview.
                System.Data.SqlClient.SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objSqlDataReader.Read())
                {
                    bPossuiIncidente = true;
                }
                objSqlDataReader.Dispose();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bPossuiIncidente;
        }
        #endregion

        #region metodo PossuiMudancaVinculado
        /// <summary>
        /// Verifica se o chamado possui ao menos 1 incidente vinculado
        /// </summary>
        ///<param name="strCodigoChamado">Código do Chamado</param>
        public static bool PossuiMudancaVinculado(String strCodigoChamado)
        {
            bool bPossuiIncidente = false;
            String strSql = "Select * from SolucaoProjeto where tabela = 'Chamado' AND tabela_identificador = " + strCodigoChamado;
            try
            {
                //Executa a query e preenche a gridview.
                System.Data.SqlClient.SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objSqlDataReader.Read())
                {
                    bPossuiIncidente = true;
                }
                objSqlDataReader.Dispose();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bPossuiIncidente;
        }
        #endregion

        #region metodo PossuiRequisicaoServicoVinculado
        /// <summary>
        /// Verifica se o chamado possui ao menos 1 requisicao de serviço vinculado
        /// </summary>
        ///<param name="strCodigoChamado">Código do Chamado</param>
        public static bool PossuiRequisicaoServicoVinculado(String strCodigoChamado)
        {
            bool bPossuiRequisicao = false;
            String strSql = "Select * from requisicaoservicochamado where chamado_codigo = " + strCodigoChamado;
            try
            {
                //Executa a query e preenche a gridview.
                System.Data.SqlClient.SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objSqlDataReader.Read())
                {
                    bPossuiRequisicao = true;
                }
                objSqlDataReader.Dispose();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bPossuiRequisicao;
        }
        #endregion

        #region metodo PossuiRequisicaoMudancaVinculado
        /// <summary>
        /// Verifica se o chamado possui ao menos 1 requisicao de Mudanca vinculado
        /// </summary>
        ///<param name="strCodigoChamado">Código do Chamado</param>
        public static bool PossuiRequisicaoMudancaVinculado(String strCodigoChamado)
        {
            bool bPossuiRequisicao = false;
            String strSql = "Select * from requisicaomudancachamado where chamado_codigo = " + strCodigoChamado;
            try
            {
                //Executa a query e preenche a gridview.
                System.Data.SqlClient.SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objSqlDataReader.Read())
                {
                    bPossuiRequisicao = true;
                }
                objSqlDataReader.Dispose();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bPossuiRequisicao;
        }
        #endregion

        #region metodo retornaTotalPorPrioridade
        public static string retornaTotalPorPrioridade(String strTabela, string intCodigoPrioridade, String strComparacao, string intCodigoStatus, string campo_data)
        {
            string retorno = "0";

            string strSql = "SELECT COUNT(" + strTabela + "_codigo) FROM " + strTabela;
            strSql += " WHERE prioridade_codigo = " + intCodigoPrioridade.ToString();
            strSql += " AND status_codigo " + strComparacao + " " + intCodigoStatus.ToString();
            strSql += " AND MONTH(" + campo_data + ") = " + DateTime.Now.Month.ToString();
            strSql += " AND YEAR(" + campo_data + ") = " + DateTime.Now.Year.ToString();

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            if (objDataReader.Read())
            {
                if (objDataReader[0].ToString() != String.Empty)
                {
                    retorno = objDataReader[0].ToString();
                }
            }

            objDataReader.Dispose();
            objDataReader = null;

            return retorno;

        }
        #endregion

        #region metodo retornaTotalHorasAtendimentoPorPrioridade
        public static string retornaTotalHorasAtendimentoPorPrioridade(String strTabela, string intCodigoPrioridade, string intCodigoStatus)
        {
            try
            {
                string dblRetorno = "0";
                String strCondicao = String.Empty;

                strCondicao = " prioridade_codigo = " + intCodigoPrioridade;
                strCondicao += " AND status_codigo = " + intCodigoStatus;
                strCondicao += " AND MONTH(data_finalizacao) = " + DateTime.Now.Month.ToString();
                strCondicao += " AND YEAR(data_finalizacao) = " + DateTime.Now.Year.ToString();

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                try
                {
                    dblRetorno = objBanco.retornaValorCampo(strTabela, "ISNULL(SUM(tempo_atendimento), 0)", strCondicao);
                }
                catch
                {
                }
                objBanco = null;

                return dblRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaTotalHorasFilaPorPrioridade
        public static string retornaTotalHorasFilaPorPrioridade(String strTabela, string intCodigoPrioridade, string intCodigoStatus)
        {
            try
            {
                string dblRetorno = "0";

                string strCondicao = " prioridade_codigo = " + intCodigoPrioridade;
                strCondicao += " AND status_codigo <> " + intCodigoStatus;
                strCondicao += " AND MONTH(data_inclusao) = " + DateTime.Now.Month.ToString();
                strCondicao += " AND YEAR(data_inclusao) = " + DateTime.Now.Year.ToString();

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                try
                {
                    dblRetorno = objBanco.retornaValorCampo(strTabela, "ISNULL(SUM(tempo_vida), 0)", strCondicao);
                }
                catch
                {
                }
                objBanco = null;

                return dblRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownListChamadoPreDefinido
        /// <summary>
        /// Gera um novo DropDownList listando apenas incidentes marcados como modelo, 
        /// de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownListChamadoPreDefinido(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                string str = string.Empty;
                try
                {
                    ClsChamado objChamado = new ClsChamado();
                    objDropDownList.DataTextField = objChamado.objDescricao.Campo;
                    objDropDownList.DataValueField = objChamado.objCodigo.Campo;

                    objChamado.objCodigo.CampoIdentificador = false;
                    objChamado.objCodigo.CampoObrigatorio = false;
                    objChamado.objChamadoModelo.Valor = "S";
                    objChamado.objChamadoModelo.CampoIdentificador = true;
                    objChamado.objChamadoModelo.CampoObrigatorio = true;
                    ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objChamado.objAtributos, true);
                    objChamado = null;

                    //Adiciona a opção default no dropdownlist.
                    ListItem itemDefault = new ListItem();
                    itemDefault.Text = "Selecione o modelo";
                    itemDefault.Value = "";
                    itemDefault.Selected = true;
                    objDropDownList.Items.Insert(0, itemDefault);

                    objChamado = null;
                }
                catch (Exception ex)
                {
                    str = ex.Message;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo AlteraSLA

        public static void AlteraSLAItensRelacionados(int CodigoChamado, int CodigoPrioridade, int CodigoImpacto, int CodigoUrgencia)
        {
            string strMensagem = string.Empty;
            string strSql = string.Empty;
            try
            {
                ClsPrioridade objPrioridade = new ClsPrioridade(CodigoPrioridade);

                #region incidentes
                if (PossuiIncidenteVinculado(CodigoChamado.ToString()))
                {
                    //altera o sla nos incidentes relacionados
                    strSql = "SELECT incidente_codigo FROM incidenteChamado ";
                    strSql += "WHERE chamado_codigo = '" + CodigoChamado + "'";
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    while (objReader.Read())
                    {
                        ClsIncidente objIncidente = new ClsIncidente(Convert.ToInt32(objReader["incidente_codigo"].ToString()));
                        objIncidente.SLAInicio.Valor = objPrioridade.TempoInicioAtendimento.Valor;
                        objIncidente.SLASolucao.Valor = objPrioridade.TempoSolucao.Valor;
                        objIncidente.Impacto.Valor = CodigoImpacto.ToString();
                        objIncidente.Urgencia.Valor = CodigoUrgencia.ToString();
                        objIncidente.Prioridade.Valor = CodigoPrioridade.ToString();
                        objIncidente.DataAlteracao.VerificaAlteracao = false;
                        objIncidente.DataAvaliacao.VerificaAlteracao = false;
                        objIncidente.DataInclusao.VerificaAlteracao = false;
                        objIncidente.DataFinalizacao.VerificaAlteracao = false;

                        if (objIncidente.DataAlteracao.Valor != string.Empty)
                        {
                            objIncidente.DataAlteracao.Valor = Convert.ToDateTime(objIncidente.DataAlteracao.Valor).ToString(ClsParametro.DataInclusao);
                        }
                        if (objIncidente.DataAvaliacao.Valor != string.Empty)
                        {
                            objIncidente.DataAvaliacao.Valor = Convert.ToDateTime(objIncidente.DataAvaliacao.Valor).ToString(ClsParametro.DataInclusao);
                        }
                        if (objIncidente.DataFinalizacao.Valor != string.Empty)
                        {
                            objIncidente.DataFinalizacao.Valor = Convert.ToDateTime(objIncidente.DataFinalizacao.Valor).ToString(ClsParametro.DataInclusao);
                        }
                        if (objIncidente.DataInclusao.Valor != string.Empty)
                        {
                            objIncidente.DataInclusao.Valor = Convert.ToDateTime(objIncidente.DataInclusao.Valor).ToString(ClsParametro.DataInclusao);
                        }

                        objIncidente.altera(out strMensagem);
                        objIncidente = null;
                    }
                    objReader.Dispose();
                    objReader = null;
                }
                #endregion incidentes       

                #region altera o sla nas requisições de serviço relacionadas.
                if (PossuiRequisicaoServicoVinculado(CodigoChamado.ToString()))
                {
                    //altera o sla nos incidentes relacionados
                    strSql = "SELECT requisicaoservico_codigo FROM RequisicaoServicoChamado ";
                    strSql += "WHERE chamado_codigo = '" + CodigoChamado + "'";
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    while (objReader.Read())
                    {
                        ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico(Convert.ToInt32(objReader["requisicaoservico_codigo"].ToString()));
                        objRequisicaoServico.SLAInicio.Valor = objPrioridade.TempoInicioAtendimento.Valor;
                        objRequisicaoServico.SLASolucao.Valor = objPrioridade.TempoSolucao.Valor;
                        objRequisicaoServico.Impacto.Valor = CodigoImpacto.ToString();
                        objRequisicaoServico.Urgencia.Valor = CodigoUrgencia.ToString();
                        objRequisicaoServico.Prioridade.Valor = CodigoPrioridade.ToString();
                        objRequisicaoServico.DataAlteracao.VerificaAlteracao = false;
                        objRequisicaoServico.DataAvaliacao.VerificaAlteracao = false;
                        objRequisicaoServico.DataInclusao.VerificaAlteracao = false;
                        objRequisicaoServico.DataFinalizacao.VerificaAlteracao = false;

                        if (objRequisicaoServico.DataAlteracao.Valor != string.Empty)
                        {
                            objRequisicaoServico.DataAlteracao.Valor = Convert.ToDateTime(objRequisicaoServico.DataAlteracao.Valor).ToString(ClsParametro.DataInclusao);
                        }
                        if (objRequisicaoServico.DataAvaliacao.Valor != string.Empty)
                        {
                            objRequisicaoServico.DataAvaliacao.Valor = Convert.ToDateTime(objRequisicaoServico.DataAvaliacao.Valor).ToString(ClsParametro.DataInclusao);
                        }
                        if (objRequisicaoServico.DataFinalizacao.Valor != string.Empty)
                        {
                            objRequisicaoServico.DataFinalizacao.Valor = Convert.ToDateTime(objRequisicaoServico.DataFinalizacao.Valor).ToString(ClsParametro.DataInclusao);
                        }
                        if (objRequisicaoServico.DataInclusao.Valor != string.Empty)
                        {
                            objRequisicaoServico.DataInclusao.Valor = Convert.ToDateTime(objRequisicaoServico.DataInclusao.Valor).ToString(ClsParametro.DataInclusao);
                        }


                        objRequisicaoServico.altera(out strMensagem);
                        objRequisicaoServico = null;
                    }
                    objReader.Dispose();
                    objReader = null;
                }
                #endregion altera o sla nas requisições de serviço relacionadas.

                objPrioridade = null;

            }
            catch (Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;

            }
        }
        #endregion

        #region metodo apagaItensVinculados(string CodigoChamado)
        /// <summary>
        /// Apaga os itens relacionados(vinculados) ao chamado.
        /// </summary>
        /// <param name="CodigoChamado">Codigo do Chamado</param>
        public static void apagaItensVinculados(string TipoItemPreservado, string CodigoChamado)
        {
            try
            {
                string strSql = string.Empty;
                string strCodigos = string.Empty;

                if ((TipoItemPreservado.Trim() != string.Empty) && (CodigoChamado.Trim() != string.Empty))
                {
                    strSql = "";
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                    if (TipoItemPreservado.Trim().ToLower() != "incidente")
                    {
                        strCodigos = string.Empty;
                        strSql = "Select incidente_codigo from IncidenteChamado where chamado_codigo = '" + CodigoChamado + "' ";
                        SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        while (objReader.Read())
                        {
                            if (strCodigos == string.Empty)
                            { strCodigos += "'" + objReader["incidente_codigo"].ToString() + "'"; }
                            else
                            { strCodigos += ",'" + objReader["incidente_codigo"].ToString() + "'"; }
                        }
                        objReader.Dispose();
                        objReader = null;

                        if (strCodigos != string.Empty)
                        {
                            //apaga relacionamentos
                            strSql = " DELETE FROM IncidenteChamado ";
                            strSql += " WHERE chamado_codigo =  '" + CodigoChamado + "'; ";
                            objBanco.executaSQL(strSql);

                            //apaga os itens
                            strSql = " DELETE FROM Incidente  ";
                            strSql += " WHERE incidente_codigo IN (" + strCodigos + ") ";
                            objBanco.executaSQL(strSql);
                        }
                    }

                    if (TipoItemPreservado.Trim().ToLower() != "requisicaoservico")
                    {
                        strCodigos = string.Empty;
                        strSql = "Select requisicaoservico_codigo from RequisicaoServicoChamado where chamado_codigo = '" + CodigoChamado + "' ";
                        SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        while (objReader.Read())
                        {
                            if (strCodigos == string.Empty)
                            { strCodigos += "'" + objReader["requisicaoservico_codigo"].ToString() + "'"; }
                            else
                            { strCodigos += ",'" + objReader["requisicaoservico_codigo"].ToString() + "'"; }
                        }
                        objReader.Dispose();
                        objReader = null;

                        if (strCodigos != string.Empty)
                        {
                            //apaga relacionamentos
                            strSql = " DELETE FROM RequisicaoServicoChamado ";
                            strSql += " WHERE chamado_codigo =  '" + CodigoChamado + "'; ";
                            objBanco.executaSQL(strSql);

                            //apaga os itens
                            strSql = " DELETE FROM RequisicaoServico  ";
                            strSql += " WHERE requisicaoservico_codigo IN (" + strCodigos + ") ";
                            objBanco.executaSQL(strSql);
                        }
                    }

                    if (TipoItemPreservado.Trim().ToLower() != "mudanca")
                    {

                    }

                    objBanco = null;
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion

        #region Verifica se o chamado foi classifica como um tipo que gera processo
        /// <summary>
        /// Verifica se o chamado informado esta classificado como um tipo que gera processo.
        /// </summary>
        /// <param name="intCodigoChamado">Código do chamado.</param>
        /// <returns>Retorna true ou false. Se é do tipo que gera processo ou não.</returns>
        public static bool VerificaSeClassificadoComoTipoGeraProcesso(int intCodigoChamado)
        {
            //================================================================================//
            // - O que: Verifica se o chamado passado por parametro esta classificado com um
            // tipo de chamado que gera processo. Se estiver retorna true, senão retorna false.
            // - Quem: Fernanda Passos.
            // - Quando: 05/03/2006 ás 22:05hs
            //================================================================================//
            bool bolValorRetorno = false;
            string strSql = " Chamado.chamado_tipo_codigo = ChamadoTipo.chamado_tipo_codigo ";
            strSql += " and chamado.chamado_codigo = " + intCodigoChamado + " ";
            strSql += " and ChamadoTipo.flag_gera_processo = 'S'";

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.retornaValorCampo("Chamado, ChamadoTipo", "chamado_codigo", strSql) != string.Empty) bolValorRetorno = true; else bolValorRetorno = false;
            objBanco = null;
            return bolValorRetorno;
        }
        #endregion
        #endregion

    }
}