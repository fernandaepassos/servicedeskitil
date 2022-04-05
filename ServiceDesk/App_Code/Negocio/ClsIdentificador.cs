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
/// Classe Identificador - Gera os ID de registro baseado na tabela Identificador do corporativo.
/// </summary>
/// 
namespace ServiceDesk.Negocio
{
    public class ClsIdentificador
    {
        #region Declarações
        //Colecao de atributos do identificador
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de uma empresa
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades

        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get { return this.objAtributos; }
        }

        public ServiceDesk.Banco.ClsAtributo Tabela
        {
            get { return objTabela; }
        }

        public ServiceDesk.Banco.ClsAtributo Codigo
        {
          get { return objCodigo; }
        }
        #endregion

        #region Contrutor da Classe
        /// <summary>
        /// Construtor da classe Identificador
        /// </summary>
        public ClsIdentificador()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region Métodos

        #region Alimenta campos coleção
        /// <summary>
        /// Adiciona todos os atributos de uma empresa a coleção de atributos.
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.DescricaoTabela = "Identificador";
            objAtributos.NomeTabela = "identificador";

            objTabela.Campo = "tabela";
            objTabela.Descricao = "Tabela";
            objTabela.CampoIdentificador = true;
            objTabela.CampoObrigatorio = true;
            objTabela.Tipo = System.Data.DbType.String;
            objTabela.Tamanho = 50;
            objAtributos.Add(objTabela);

            objCodigo.Campo = "codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);
        }
        #endregion

        #region getProximoValor
        /// <summary>
        /// Pega o proximo valor a ser inserido.
        /// </summary>
        /// <returns>Retorna o valor inserido</returns>
        public int getProximoValor()
        {
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                this.objCodigo.Valor = objBanco.retornaValorCampo(this.objAtributos.NomeTabela, this.objCodigo.Campo, "LOWER(" + this.objTabela.Campo + ") = '" + this.objTabela.Valor + "'");
                if (this.objCodigo.Valor == String.Empty || this.objCodigo.Valor == null)
                {
                    this.objCodigo.Valor = "1";
                }
                else
                {
                    this.objCodigo.Valor = Convert.ToString(Convert.ToInt32(this.objCodigo.Valor) + 1);
                }
                //return intProximo;

                return Convert.ToInt32(this.objCodigo.Valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region atualizaValor
        /// <summary>
        /// Atualiza o valor do campo codigo para codigo + 1
        /// </summary>
        /// <returns>Retorna [true] ou [false]</returns>
        public bool atualizaValor()
        {
            bool bolRetorno = false;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            try
            {
                if (this.objCodigo.Valor == "1")
                {
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        bolRetorno = true;
                    }
                }
                else
                {
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        bolRetorno = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return bolRetorno;
        }
        #endregion

        #region Valida exclusão
        /// <summary>
        /// Valida exclusão
        /// </summary>
        /// <param name="strNomeCampo">Nome do campo</param>
        /// <param name="strMensagem">Mensagem com descricao de como resultou o processo</param> 
        /// <param name="strValor">Valor a ser excluído</param> 
        /// <param name="bolCampoInteiro">Indica seo campo é do tipo inteiro</param> 
        /// <param name="bolCampoString">Indica se o campo é do tipo string</param> 
        /// <returns>Retorna true ou false. Se é valido ou não.</returns> 
        /// <param name="strTabelaOrigem">Tabela de origem</param> 
        public static bool ValidaExclusao(string strNomeCampo, string strValor, out String strMensagem, bool bolCampoInteiro, bool bolCampoString, string strTabelaOrigem)
        {
            strMensagem = string.Empty;
            if (strNomeCampo == string.Empty || strValor == string.Empty || strTabelaOrigem == string.Empty)
            {
                strMensagem = "Informe os parâmetros corretamente.";
                return false;
            }

            string strSql = "select sysobjects.name as tabela, syscolumns.name as campo";
            strSql += " from syscolumns, sysobjects";
            strSql += " where syscolumns.id = sysobjects.id";
            strSql += " and syscolumns.name = '" + strNomeCampo.Trim() + "'";
            strSql += " and sysobjects.name <> '"+ strTabelaOrigem.Trim() +"'";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            while (objDataReader.Read())
            {
                if (VerificaSeEstaAssociado(objDataReader["tabela"].ToString().Trim(), objDataReader["campo"].ToString().Trim(), strValor, bolCampoInteiro, bolCampoString) == true)
                {
                    strMensagem = "O registro que deseja excluir esta sendo utilizado em outro processo. Exclusão abortada.";
                    objDataReader.Dispose();
                    return false;
                }
            }
            objDataReader.Dispose();
            return true;
        }
        #endregion

        #region Verifica se o registro esta associado
        /// <summary>
        /// Verifica se registro esta associa na tabela
        /// </summary>
        /// <param name="strTabela">Nome da tabela</param>
        /// <param name="strCampo">Nome do campo</param>
        /// <param name="strValor">Valor do campo</param>
        /// <returns>Retorna true ou false. Se esta associado ou não</returns>
        /// <param name="bolCampoInteiro">Indica se o campo é do tipo inteiro</param> 
        /// <param name="bolCampoString">Indica se o campo é do tipo string</param> 
        public static bool VerificaSeEstaAssociado(string strTabela, string strCampo, string strValor, bool bolCampoInteiro, bool bolCampoString)
        {
            string strSql = string.Empty;
            if (bolCampoInteiro == true) strSql = " " + strCampo + " = " + Convert.ToInt32(strValor) + "";
            if (bolCampoString == true) strSql = " " + strCampo + " = '" + strValor.Trim() + "' ";


            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            string strValorRetornado = objBanco.retornaValorCampo(strTabela, strCampo, strSql);
            objBanco = null;

            if (strValorRetornado == string.Empty)
                return false;
            else if (strValorRetornado != string.Empty)
                return true;

            return false;
        }
        #endregion

        #endregion
    }
}