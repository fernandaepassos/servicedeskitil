using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe TipoPrioridade
/// </summary>
/// 
namespace ServiceDesk.Negocio
{
    public class ClsPrioridade
    {
        //Colecao de atributos de Tipo de Prioridade
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de uma Tipo de Prioridade
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objValorMinimo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objValorMaximo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempoInicioAtendimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempoSolucao = new ServiceDesk.Banco.ClsAtributo();

        #region Propriedades
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        /// <summary>
        /// Código do Tipo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descrição do Tipo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Valor Minimo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ValorMinimo
        {
          get { return objValorMinimo; }
          set { this.objValorMinimo = value; }
        }

        /// <summary>
        /// Valor Maximo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ValorMaximo
        {
          get { return objValorMaximo; }
          set { this.objValorMaximo = value; }
        }

        /// <summary>
        /// Tipo Inicio Atendimento
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TempoInicioAtendimento
        {
            get { return objTempoInicioAtendimento; }
            set { this.objTempoInicioAtendimento = value; }
        }

        /// <summary>
        /// Tempo Solução
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TempoSolucao
        {
            get { return objTempoSolucao; }
            set { this.objTempoSolucao = value; }
        }
        #endregion

        #region Métodos
        #region Construtor da classe
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        public ClsPrioridade()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsPrioridade(int intCodigo)
        {
            try{
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
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
            objAtributos.NomeTabela = "Prioridade";
            objAtributos.DescricaoTabela = "Tipo de Prioridade";

            objCodigo.Campo = "prioridade_codigo";
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

            objValorMinimo.Campo = "valor_minimo";
            objValorMinimo.Descricao = "Valor Mínimo";
            objValorMinimo.CampoIdentificador = false;
            objValorMinimo.CampoObrigatorio = true;
            objValorMinimo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objValorMinimo);

            objValorMaximo.Campo = "valor_maximo";
            objValorMaximo.Descricao = "Valor Maximo";
            objValorMaximo.CampoIdentificador = false;
            objValorMaximo.CampoObrigatorio = true;
            objValorMaximo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objValorMaximo);

            objTempoInicioAtendimento.Campo = "tempo_inicio_atendimento";
            objTempoInicioAtendimento.Descricao = "Inicio do atendimento";
            objTempoInicioAtendimento.CampoIdentificador = false;
            objTempoInicioAtendimento.CampoObrigatorio = false;
            objTempoInicioAtendimento.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTempoInicioAtendimento);

            objTempoSolucao.Campo = "tempo_solucao";
            objTempoSolucao.Descricao = "Tempo da Solução";
            objTempoSolucao.CampoIdentificador = false;
            objTempoSolucao.CampoObrigatorio = false;
            objTempoSolucao.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTempoSolucao);
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
            strMensagem = String.Empty;
            bool bolRetorno = false;
            try
            {
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
                strMensagem = "Não foi possivel realizar a transação.";
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
              String strSql = String.Empty;
              objGridView.AutoGenerateColumns = false;
              ClsPrioridade objPrioridade = new ClsPrioridade();
              //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objPrioridade.objAtributos);
              ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
              strSql = objBanco.montaQuery(objPrioridade.objAtributos, false);
              strSql += " ORDER BY descricao";
              System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
              objGridView.DataSource = objDataSet;
              objGridView.DataBind();
              objDataSet.Dispose();
              objDataSet = null;
              objBanco = null;
              objPrioridade = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsPrioridade objPrioridade, bool bolCondicao)
        {try{
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objPrioridade.objAtributos, bolCondicao);
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
        {try{

            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a Descrição do Tipo.";
            }
            else
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Item atualizado com sucesso.";
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

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {try{
            ClsPrioridade objTipoPrioridade = new ClsPrioridade();
            objDropDownList.DataTextField = objTipoPrioridade.objDescricao.Campo;
            objDropDownList.DataValueField = objTipoPrioridade.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objTipoPrioridade.objAtributos);
            objTipoPrioridade = null;

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

        #region metodo getPrioridade(string strImpactoCodigo, string strUrgenciaCodigo)
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static string getPrioridade(string strImpactoCodigo, string strUrgenciaCodigo)
        {
          string strPrioridadeCodigo = string.Empty;
          String strCondicao = string.Empty;

          strCondicao = " impacto_codigo = 0" + strImpactoCodigo;
          strCondicao = strCondicao + " AND ";
          strCondicao = strCondicao + " urgencia_codigo = 0" + strUrgenciaCodigo;

          try
          {
            ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
            strPrioridadeCodigo = Banco.retornaValorCampo("PrioridadeCriterio", "prioridade_codigo", strCondicao);
        }
        catch (Exception ex)
        {
            throw ex;
        }
          return strPrioridadeCodigo;
        }
        #endregion

        #region metodo getPrioridadeDescricao(string strPrioridadeCodigo)
        /// <summary>
        /// Pega descrição da prioridade
        /// </summary>
        public static string getPrioridadeDescricao(string strPrioridadeCodigo)
        {
            String strDescricaoPrioridade = string.Empty;
            String strCondicao = string.Empty;

            strCondicao = " prioridade_codigo = 0" + strPrioridadeCodigo;

            try
            {
                ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
                strDescricaoPrioridade = Banco.retornaValorCampo("Prioridade", "descricao", strCondicao);
                Banco = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strDescricaoPrioridade;
        }
        #endregion

        #endregion

    }

}