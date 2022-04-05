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
/// Summary description for ClsIncidente
/// </summary>
namespace ServiceDesk.Negocio
{
    public class ClsIncidente
    {
        #region Construtores
        public ClsIncidente()
        {
            this.alimentaColecaoCampos();
        }

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsIncidente(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion

        #endregion

        #region Atributos de um Incidente

        //Colecao de atributos de Incidente
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objMatriculaInclusor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objUrgencia = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPrioridade = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objImpacto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objOrigemIncidente = new ServiceDesk.Banco.ClsAtributo();
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
        private ServiceDesk.Banco.ClsAtributo objTempoDeVida = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempoDeAtendimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEquipe = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTecnico = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEscalado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoIncidente = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objIncidenteModelo = new ServiceDesk.Banco.ClsAtributo();
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
        /// Código da Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descrição da Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Matricula do inclusor da Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo MatriculaInclusor
        {
            get { return objMatriculaInclusor; }
            set { this.objMatriculaInclusor = value; }
        }

        /// <summary>
        ///Tipo do Incidente
        ///</summary>
        public ServiceDesk.Banco.ClsAtributo TipoIncidente
        {
            get { return objTipoIncidente; }
            set { this.objTipoIncidente = value; }
        }

        /// <summary>
        /// Urgência do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Urgencia
        {
            get { return objUrgencia; }
            set { this.objUrgencia = value; }
        }

        /// <summary>
        /// Prioridade do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Prioridade
        {
            get { return objPrioridade; }
            set { this.objPrioridade = value; }
        }

        /// <summary>
        /// Impacto do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Impacto
        {
            get { return objImpacto; }
            set { this.objImpacto = value; }
        }

        /// <summary>
        /// Origem do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo OrigemIncidente
        {
            get { return objOrigemIncidente; }
            set { this.objOrigemIncidente = value; }
        }

        /// <summary>
        /// Status do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Status
        {
            get { return objStatus; }
            set { this.objStatus = value; }
        }

        /// <summary>
        /// Matricula do alterador do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo MatriculaAlterador
        {
            get { return objMatriculaAlterador; }
            set { this.objMatriculaAlterador = value; }
        }

        /// <summary>
        /// Matricula do solicitante do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo MatriculaSolicitante
        {
            get { return objMatriculaSolicitante; }
            set { this.objMatriculaSolicitante = value; }
        }

        /// <summary>
        /// Data de inclusão do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInclusao
        {
            get { return objDataInclusao; }
            set { this.objDataInclusao = value; }
        }

        /// <summary>
        /// Data de alteração do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAlteracao
        {
            get { return objDataAlteracao; }
            set { this.objDataAlteracao = value; }
        }

        /// <summary>
        /// Data de finalização do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataFinalizacao
        {
            get { return objDataFinalizacao; }
            set { this.objDataFinalizacao = value; }
        }

        /// <summary>
        /// Matricula do finalizador do Incidente.
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
        /// Data da avaliação do atendimento do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAvaliacao
        {
            get { return objDataAvaliacao; }
            set { this.objDataAvaliacao = value; }
        }

        /// <summary>
        /// Nota atribuida ao atendimento do Incidente pelo solicitante.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Avaliacao
        {
            get { return objAvaliacao; }
            set { this.objAvaliacao = value; }
        }

        /// <summary>
        /// Observação sobre a avaliação do atendimento do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ObservacaoAvaliacao
        {
            get { return objObservacaoAvaliacao; }
            set { this.objObservacaoAvaliacao = value; }
        }

        /// <summary>
        /// Enviar notificação ao solicitante durante o atendimento do Incidente(S/N)? .
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo EnviarNotificacao
        {
            get { return objEnviarNotificacao; }
            set { this.objEnviarNotificacao = value; }
        }

        /// <summary>
        /// Incidente VIP(S/N)? .
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Vip
        {
            get { return objVip; }
            set { this.objVip = value; }
        }

        /// <summary>
        /// Tempo de vida do Incidente.
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
        /// /// Equipe atribuida ao Incidente
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Equipe
        {
            get { return objEquipe; }
            set { this.objEquipe = value; }
        }

        /// <summary>
        /// Técnico atribuido ao Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tecnico
        {
            get { return objTecnico; }
            set { this.objTecnico = value; }
        }

        /// <summary>
        /// Incidente Escalado?.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Escalado
        {
            get { return objEscalado; }
            set { this.objEscalado = value; }
        }

        /// <summary>
        /// Incidente Modelo?.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Modelo
        {
            get { return objIncidenteModelo; }
            set { this.objIncidenteModelo = value; }
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
        /// Nivel de Atendimento
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
            objAtributos.NomeTabela = "Incidente";
            objAtributos.DescricaoTabela = "Incidente";

            objCodigo.Campo = "incidente_codigo";
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

            objOrigemIncidente.Campo = "incidente_origem_codigo";
            objOrigemIncidente.Descricao = "Origem do Incidente";
            objOrigemIncidente.CampoIdentificador = false;
            objOrigemIncidente.CampoObrigatorio = false;
            objOrigemIncidente.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objOrigemIncidente);

            objTipoIncidente.Campo = "incidente_tipo_codigo";
            objTipoIncidente.Descricao = "Tipo de Incidente";
            objTipoIncidente.CampoIdentificador = false;
            objTipoIncidente.CampoObrigatorio = false;
            objTipoIncidente.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTipoIncidente);

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
            objDataFinalizacao.Tipo = System.Data.DbType.String;
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

            objIncidenteModelo.Campo = "modelo";
            objIncidenteModelo.Descricao = "Modelo?";
            objIncidenteModelo.CampoIdentificador = false;
            objIncidenteModelo.CampoObrigatorio = false;
            objIncidenteModelo.Tipo = System.Data.DbType.String;
            objAtributos.Add(objIncidenteModelo);

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
        /// Método que insere um novo tipo de urgência.
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
                ClsIncidente objIncidente = new ClsIncidente();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objIncidente.objAtributos);
                objIncidente = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsIncidente objIncidente)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objIncidente.objAtributos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo altera 
        /// <summary>
        /// Método que altera um tipo de Urgência
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)  
        {
          try
          {
              strMensagem = String.Empty;
              bool bolRetorno = false;
              string strMsg = string.Empty;
              
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
        /// Método que exclui
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

        #region getIncidente

        /// <summary>
        /// Pega os dados de um determinado Incidente
        /// </summary>
        /// <param name="strCodigoIncidente">Codigo do Incidente</param>
        static public System.Data.SqlClient.SqlDataReader getIncidente(String strCodigoIncidente)
        {
            String strSql = String.Empty;
            System.Data.SqlClient.SqlDataReader objSqlDataReader = null;

            try
            {
                strSql = "SELECT * FROM Incidente";
                strSql += " WHERE incidente_codigo = '" + strCodigoIncidente + "'";
                objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objSqlDataReader;
        }

        #endregion

        #region metodo geraGridViewIC
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="strCodigoIncidente">Codigo do incidente</param>
        public static void geraGridViewIC(System.Web.UI.WebControls.GridView objGridView, String strCodigoIncidente)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select IC.ic_codigo, IC.Nome ";
                strSql += "from IC IC, IncidenteIC IIC ";
                strSql += "Where IIC.ic_codigo = IC.ic_codigo ";
                strSql += "and IIC.incidente_codigo = '" + strCodigoIncidente + "'";

                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
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

        #region metodo geraGridViewChamadosVinculados
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">GridView</param>
        /// <param name="strCodigoIncidente">Codigo do incidente</param>
        public static void geraGridViewChamadosVinculados(System.Web.UI.WebControls.GridView objGridView, String strCodigoIncidente)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select C.chamado_codigo, C.descricao ";
                strSql += "from chamado C, IncidenteChamado IC ";
                strSql += "Where C.chamado_codigo = IC.chamado_codigo ";
                strSql += "and IC.incidente_codigo = '" + strCodigoIncidente + "' order by IC.chamado_codigo";

                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo PossuiChamadoVinculado
        /// <summary>
        /// Verifica se o chamado possui ao menos 1 chamado vinculado
        /// </summary>
        ///<param name="strCodigoIncidente">Código do Incidente</param>
        public static bool PossuiChamadoVinculado(String strCodigoIncidente)
        {
            bool bPossui = false;
            String strSql = "Select * from incidentechamado where incidente_codigo = " + strCodigoIncidente;
            try
            {
                //Executa a query e preenche a gridview.
                System.Data.SqlClient.SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objSqlDataReader.Read())
                {
                    bPossui = true;
                }
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bPossui;
        }
        #endregion

        #region RemoveRelacaoIncidenteChamado

        /// <summary>
        /// Adiciona um item de configuracao ao Incidente.
        /// </summary>
        /// <param name="strCodigoIncidente">Codigo do Incidente</param>
        /// <param name="strCodigoChamado">Codigo do Chamado a ser desvinculado do Incidente</param>
        static public void RemoveRelacaoIncidenteChamado(String strCodigoIncidente, string strCodigoChamado)
        {
            try
            {
                string strSql = string.Empty;
                String strCodigoIncidenteItemConfiguracao = string.Empty;

                if ((strCodigoIncidente != string.Empty) && (strCodigoChamado != ""))
                {
                    strSql = "DELETE FROM incidentechamado ";
                    strSql += "WHERE incidente_codigo = '" + strCodigoIncidente + "' and chamado_codigo = '" + strCodigoChamado + "' ";
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
            string str = string.Empty;
            try
            {
                ClsIncidente objIncidente = new ClsIncidente();
                objDropDownList.DataTextField = objIncidente.objDescricao.Campo;
                objDropDownList.DataValueField = objIncidente.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objIncidente.objAtributos);
                objIncidente = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "--";
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);
            }
            catch (Exception ex)
            {
                str = ex.Message;
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
        public static void geraDropDownListIncidentePreDefinido(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            string str = string.Empty;
            try
            {
                ClsIncidente objIncidente = new ClsIncidente();
                objDropDownList.DataTextField = objIncidente.objDescricao.Campo;
                objDropDownList.DataValueField = objIncidente.objCodigo.Campo;

                objIncidente.objCodigo.CampoIdentificador = false;
                objIncidente.objCodigo.CampoObrigatorio = false;
                objIncidente.objIncidenteModelo.Valor = "S";
                objIncidente.objIncidenteModelo.CampoIdentificador = true;
                objIncidente.objIncidenteModelo.CampoObrigatorio = true;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objIncidente.objAtributos, true);
                objIncidente = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "Selecione o modelo";
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);

                objIncidente = null;
            }
            catch (Exception ex)
            {
                str = ex.Message;
                throw ex;
            }          
        }
        #endregion

        #region herdaInformacoesChamado(string strCodigoChamado)

        /// <summary>
        /// Herda informações de um determinado chamado e insere no objeto do tipo incidente
        /// </summary>
        /// <param name="strCodigoChamado">Codigo do chamado do qual se deseja herdar os dados</param>
        public static ClsIncidente herdaInformacoesChamado(string strCodigoChamado)
        {
            try
            {
                ClsIncidente objIncidente = new ClsIncidente();

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

                        objIncidente.MatriculaInclusor.Valor = objReader["pessoa_codigo_proprietario"].ToString();
                        objIncidente.MatriculaSolicitante.Valor = objReader["pessoa_codigo_solicitante"].ToString();
                        objIncidente.Descricao.Valor = objReader["descricao"].ToString();

                        if (objReader["data_inclusao"].ToString() != string.Empty)
                        {
                            objIncidente.DataInclusao.Valor = Convert.ToDateTime(objReader["data_inclusao"]).ToString(strFormatoDataInclusao, null);
                        }
                        else
                        {
                            objIncidente.DataInclusao.Valor = strDataMinimaSistema;
                        }


                        objIncidente.Status.Valor = SServiceDesk.Negocio.ClsWorkFlow.primeiroStatus("INCIDENTE").ToString();
                        objIncidente.Urgencia.Valor = objReader["urgencia_codigo"].ToString();
                        objIncidente.Prioridade.Valor = objReader["prioridade_codigo"].ToString();
                        objIncidente.Impacto.Valor = objReader["impacto_codigo"].ToString();
                        objIncidente.TipoIncidente.Valor = objReader["chamado_tipo_codigo"].ToString();
                        if (objReader["origem_chamado_tipo_codigo"].ToString() != string.Empty)
                        {
                            objIncidente.OrigemIncidente.Valor = objReader["origem_chamado_tipo_codigo"].ToString();
                        }
                        objIncidente.Escalado.Valor = objReader["escalado"].ToString();
                        objIncidente.Equipe.Valor = objReader["equipe_codigo_alocacao"].ToString();
                        objIncidente.Tecnico.Valor = objReader["pessoa_codigo_alocacao"].ToString();
                        objIncidente.NivelAtendimento.Valor = objReader["nivel_atendimento_codigo"].ToString();
                    }
                    catch//(Exception ex)
                    {
                        Exception myEx = new Exception("Não foi possivel herdar as informações do chamado.");
                        throw myEx;
                    }

                }
                objReader = null;

                return objIncidente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region herdaICChamado
        /// <summary>
        /// Copia os ICs de um chamado para o incidente informado
        /// </summary>
        /// <param name="strCodigoIncidente">Codigo do Incidente</param>
        /// <param name="strCodigoChamado">Codigo do Chamado</param>
        /// <returns>true se copia ok.</returns>
        //static public bool herdaICChamado(String strCodigoChamado, String strCodigoIncidente)
        static public void herdaICChamado(String strCodigoChamado, String strCodigoIncidente)
        {
            String strSql = String.Empty;
            try
            {
                strSql = "INSERT INTO IncidenteIC ";
                strSql += "SELECT '" + strCodigoIncidente + "' as incidente_codigo, ic_codigo ";
                strSql += "FROM ChamadoIC ";
                strSql += "WHERE chamado_codigo ='" + strCodigoChamado + "' ";
                strSql += "AND ic_codigo NOT IN ( ";
                strSql += "Select ic_codigo ";
                strSql += "From IncidenteIC ";
                strSql += "WHERE incidente_codigo = '" + strCodigoIncidente + "' ) ";

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

        #region AdicionaRelacaoIncidenteChamado(strCodigoChamado, strCodigoIncidente);

        /// <summary>
        /// Cria o relacionamento entre um chamado e um incidente
        /// </summary>
        /// <param name="strCodigoIncidente">Codigo do Incidente</param>
        /// <param name="strCodigoChamado">Codigo do Chamado</param>
        /// <returns>true se ok.</returns>
        //static public bool AdicionaRelacaoIncidenteChamado(String strCodigoChamado, String strCodigoIncidente)
        static public void AdicionaRelacaoIncidenteChamado(String strCodigoChamado, String strCodigoIncidente)
        {
            String strSql = String.Empty;
            try
            {
                strSql = "INSERT INTO IncidenteChamado ";
                strSql += "(chamado_codigo, incidente_codigo) ";
                strSql += "VALUES ";
                strSql += "('" + strCodigoChamado + "','" + strCodigoIncidente + "') ";

                ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
                Banco.executaSQL(strSql);
                Banco = null;
            }
            catch //(Exception ex)
            {
                Exception myEx = new Exception("Não foi possivel relacionar o incidente ao chamado.");
                throw myEx;
            }
        }

        #endregion

        #region metodo criaIncidenteBaseadoChamado
        /// <summary>
        /// Cria im incidente vinculado ao chamado e baseado nas informações do chamado.
        /// </summary>
        /// <param name="strCodigoChamado">Codigo do Chamado</param>
        /// <returns>False se houve erro ou TRUE se foi criado com sucesso</returns>
        public static bool criaIncidenteBaseadoChamado(String strCodigoChamado, out String strCodigoIncidenteCriado)
        {
            bool bSucesso = false;
            String strMensagem = string.Empty;
            strCodigoIncidenteCriado = string.Empty;

            try
            {
                ClsIncidente objIncidente = ClsIncidente.herdaInformacoesChamado(strCodigoChamado);
                ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                objIdentificador.Tabela.Valor = objIncidente.Atributos.NomeTabela;
                objIncidente.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objIncidente.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                    strCodigoIncidenteCriado = objIdentificador.Codigo.Valor.ToString();
                    //Relaciona incidente ao chamado
                    ClsIncidente.AdicionaRelacaoIncidenteChamado(strCodigoChamado, strCodigoIncidenteCriado);

                    SServiceDesk.Negocio.ClsWorkFlow.salvaRepercusao(Convert.ToInt32(objIncidente.Codigo.Valor), objIncidente.Atributos.NomeTabela, objIncidente.Status.Valor);

                    //Copia os CIs do chamado para o incidente criado
                    ClsIncidente.herdaICChamado(strCodigoChamado, strCodigoIncidenteCriado);

                    //=================================================================================//
                    // - O que: Atualiza a tabela StatusLog para informar os dados sobre o status que o
                    // Incidente possui.
                    // - Quem: Fernanda Passos
                    // - Quando: 15/03/2006 ás 16:04
                    //=================================================================================//
                    SServiceDesk.Negocio.ClsWorkFlow.gravaLog(Convert.ToInt32(objIncidente.Codigo.Valor.Trim()), objIncidente.Atributos.NomeTabela.Trim(), "0", objIncidente.Status.Valor.Trim());                    
                    //=================================================================================//

                    bSucesso = true;
                }
                objIncidente = null;
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
        /// <param name="strCodigoIncidente">Codigo do incidente</param>
        public static void geraGridViewNotaAtendimento(System.Web.UI.WebControls.GridView objGridView, String strCodigoIncidente)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select nota_codigo, data_inclusao, pessoa_codigo, nota ";
                strSql += "from nota ";
                strSql += "where ";
                strSql += " identificador_tabela =  '" + strCodigoIncidente + "' ";
                strSql += " and tabela =  'Incidente' ";
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

        #region metodo geraGridViewProblemasVinculados
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">GridView</param>
        /// <param name="strCodigoIncidente">Codigo do incidente</param>
        public static void geraGridViewProblemasVinculados(System.Web.UI.WebControls.GridView objGridView, String strCodigoIncidente)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = "Select P.problema_codigo, P.nome, P.descricao, P.data_inclusao ";
                strSql += "from problema P, ProblemaIncidente PI ";
                strSql += "Where P.problema_codigo = PI.problema_codigo ";
                strSql += "and PI.incidente_codigo = '" + strCodigoIncidente + "' order by PI.problema_codigo";

                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;// ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataBind();
                objDataSet.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region desalocaEquipeTecnico
        /// <summary>
        /// Método desaloca a equipe designada para um chamado. 
        /// É utilizado quando uma notificação é enviada à equipe e a mesma rejeita a designação para o item na tela de notificacao.
        /// </summary>
        /// <returns>Retorna true ou false. Se obteve sucesso não.</returns>
        public static bool desalocaEquipe(int intCodigoIncidente)
        {
            try
            {
                String strMensagem = string.Empty;

                ClsIncidente objIncidente = new ClsIncidente(intCodigoIncidente);
                objIncidente.Equipe.Valor = string.Empty;
                objIncidente.Tecnico.Valor = string.Empty;
                if (objIncidente.altera(out strMensagem))
                {
                    objIncidente = null;
                    return true;
                }
                else
                {
                    objIncidente = null;
                    return false;
                }
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
        /// <param name="intCodigoIncidente">Código do Incidente</param> 
        public static string GetCodigoProprietario(int intCodigoIncidente)
        {
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strValor = objBanco.retornaValorCampo("Incidente", "pessoa_codigo_proprietario", "incidente_codigo = " + intCodigoIncidente + "");
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