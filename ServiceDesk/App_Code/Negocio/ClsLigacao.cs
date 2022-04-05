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
/// Summary description for ClsLigacao
/// </summary>
/// 

namespace ServiceDesk.Negocio
{

    public class ClsLigacao
    {
        //Construtor
        public ClsLigacao()
        {
            this.alimentaColecaoCampos();
        }

        #region Atributos de um registro de ligacao

        //Colecao de atributos de Registro de liga��o
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoChamado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoInclusor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();

        #endregion

        #region Propriedades


        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get { return this.objAtributos; }
        }

        /// <summary>
        /// C�digo da Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descri��o da Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// C�digo do inclusor da Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoInclusor
        {
            get { return objCodigoInclusor; }
            set { this.objCodigoInclusor = value; }
        }

        /// <summary>
        /// Codigo do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoChamado
        {
            get { return objCodigoChamado; }
            set { this.objCodigoChamado = value; }
        }

        /// <summary>
        /// Data de inclus�o do Chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInclusao
        {
            get { return objDataInclusao; }
            set { this.objDataInclusao = value; }
        }


        #endregion

        #region M�todos

        #region alimentaColecaoCampos

        /// <summary>
        /// M�todo que alimenta a cole��o de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "Ligacao";
            objAtributos.DescricaoTabela = "Liga��o";

            objCodigo.Campo = "ligacao_codigo";
            objCodigo.Descricao = "C�digo";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descri��o";
            objDescricao.CampoIdentificador = false;
            objDescricao.CampoObrigatorio = true;
            objDescricao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDescricao);

            objCodigoInclusor.Campo = "pessoa_codigo_proprietario";
            objCodigoInclusor.Descricao = "C�digo do Propriet�rio";
            objCodigoInclusor.CampoIdentificador = false;
            objCodigoInclusor.CampoObrigatorio = true;
            objCodigoInclusor.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoInclusor);

            objDataInclusao.Campo = "data_inclusao";
            objDataInclusao.Descricao = "Data Inclus�o";
            objDataInclusao.CampoIdentificador = false;
            objDataInclusao.CampoObrigatorio = true;
            objDataInclusao.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataInclusao);


            objCodigoChamado.Campo = "chamado_codigo";
            objCodigoChamado.Descricao = "C�digo do Chamado";
            objCodigoChamado.CampoIdentificador = false;
            objCodigoChamado.CampoObrigatorio = true;
            objCodigoChamado.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoChamado);

        }
        #endregion

        #region metodo insere
        /// <summary>
        /// M�todo que insere um novo registro de liga��o.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objDescricao.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar a Descri��o.";
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
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsLigacao objLigacao = new ClsLigacao();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objLigacao.objAtributos);
                objLigacao = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsLigacao objLigacao, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objLigacao.objAtributos, bolCondicao);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo altera
        /// <summary>
        /// M�todo que altera um registro de liga��o.
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
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
        /// M�todo que exclui um registro de liga��o.
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui()
        {
            try
            {
                string strMsg = string.Empty;

                //Valida a exclus�o.
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

        #region getLigacao

        /// <summary>
        /// Pega os dados de um determinado registro de liga��o
        /// </summary>
        /// <param name="strCodigoLigacao">Codigo do registro de ligacao</param>
        static public System.Data.SqlClient.SqlDataReader getLigacao(String strCodigoLigacao)
        {
            try
            {
                String strSql = String.Empty;
                System.Data.SqlClient.SqlDataReader objSqlDataReader = null;

                try
                {
                    strSql = "SELECT * FROM ligacao";
                    strSql += " WHERE ligacao_codigo = '" + strCodigoLigacao + "'";
                    objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                }
                catch
                {

                }
                return objSqlDataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion


        #endregion
    }

}