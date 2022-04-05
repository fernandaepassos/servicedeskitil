using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SServiceDesk.Negocio
{
    /// <summary>
    /// Summary description for ClsSegurancaDireitoPapelPapel
    /// </summary>
    public class ClsSegurancaDireitoPapel
    {
        //Colecao de atributos da Seguranca Direito.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        # region Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSegurancaPapelCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSegurancaDireitoCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Construtor
        public ClsSegurancaDireitoPapel()
        {
            this.alimentaColecaoCampos();
        }

        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsSegurancaDireitoPapel(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion

        #region metodo alimentaColecaoCampos
        /// <summary>
        /// Adiciona todos os atributos de um Perfil, a cole��o de atributos.
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.DescricaoTabela = "SegurancaDireitoPapel";
            objAtributos.NomeTabela = "SegurancaDireitoPapel";

            objCodigo.Campo = "seguranca_direito_papel_codigo";
            objCodigo.Descricao = "C�digo";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objSegurancaPapelCodigo.Campo = "seguranca_papel_codigo";
            objSegurancaPapelCodigo.Descricao = "C�digo do Pap�l da seguran�a";
            objSegurancaPapelCodigo.CampoObrigatorio = true;
            objSegurancaPapelCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objSegurancaPapelCodigo);

            objSegurancaDireitoCodigo.Campo = "seguranca_direito_codigo";
            objSegurancaDireitoCodigo.Descricao = "C�digo do Direito da seguran�a";
            objSegurancaDireitoCodigo.CampoObrigatorio = true;
            objSegurancaDireitoCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objSegurancaDireitoCodigo);

            objTabela.Campo = "tabela";
            objTabela.Descricao = "Tabela";
            objTabela.Tipo = System.Data.DbType.String;
            objAtributos.Add(objTabela);
        }
        #endregion

        #region Propriedades
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get
            {
                return objCodigo;
            }
        }

        public ServiceDesk.Banco.ClsAtributo SegurancaPapelCodigo
        {
            get
            {
                return objSegurancaPapelCodigo;
            }
            set
            {
                objSegurancaPapelCodigo = value;
            }
        }

        public ServiceDesk.Banco.ClsAtributo SegurancaDireitoCodigo
        {
            get
            {
                return objSegurancaDireitoCodigo;
            }
            set
            {
                objSegurancaDireitoCodigo = value;
            }
        }

        public ServiceDesk.Banco.ClsAtributo Tabela
        {
            get
            {
                return objTabela;
            }
            set
            {
                objTabela = value;
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
                ClsSegurancaDireitoPapel objSegurancaDireitoPapel = new ClsSegurancaDireitoPapel();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objSegurancaDireitoPapel.objAtributos);
                objSegurancaDireitoPapel = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsSegurancaDireitoPapel objSegurancaDireitoPapel, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objSegurancaDireitoPapel.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownTabelas
        /// <summary>
        /// Gera um DropDownList com a descri��o de todas as tabelas do banco.
        /// </summary>
        /// <param name="objDropDown">Nome do DropDownList</param>
        public static void geraDropDownTabelas(System.Web.UI.WebControls.DropDownList objDropDown)
        {
            try
            {
                String strSQL;
                strSQL = "SELECT name as NomeTabela FROM SysObjects WHERE xtype = 'U' ORDER BY name";
                objDropDown.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
                objDropDown.DataTextField = "NomeTabela";
                objDropDown.DataValueField = "NomeTabela";
                objDropDown.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// M�todo que insere um novo cargo.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objSegurancaPapelCodigo.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar o papel da seguran�a.<br>";

                if (this.objSegurancaDireitoCodigo.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o direito da seguran�a.<br>";

                if (this.objTabela.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o nome da tabela.<br>";

                if (strMensagem == String.Empty)
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Item inserido com sucesso.";
                        bolRetorno = true;
                    }
                    else
                    {
                        strMensagem = "N�o foi poss�vel realizar a opera��o.<br>" + strMensagem;
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

        #region metodo altera
        /// <summary>
        /// M�todo que altera um tipo de Seguran�a papel
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
        public bool altera(out string strMensagem)
        {
            try
            {
                strMensagem = string.Empty;
                bool bolRetorno = false;

                if (this.objSegurancaPapelCodigo.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar o papel da seguran�a.<br>";

                if (this.objSegurancaDireitoCodigo.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o direito da seguran�a.<br>";

                if (this.objTabela.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o nome da tabela.<br>";

                if (strMensagem == String.Empty)
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        objBanco = null;

                        strMensagem = "Altera��o efetuada com sucesso.";
                        bolRetorno = true;
                    }
                    else
                    {
                        objBanco = null;
                        strMensagem = "N�o foi poss�vel realizar a opera��o.";
                    }
                }

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo getDireitoPapel
        public static bool getDireitoPapel(int intCodigoDireito, string strNomeTabela, int intCodigoPapel)
        {
            try
            {
                string strSql = string.Empty;
                strSql = "select * from segurancadireitopapel where seguranca_direito_codigo = "+intCodigoDireito+" and ";
                strSql += "seguranca_papel_codigo = " + intCodigoPapel + " and tabela = '" + strNomeTabela.Trim() + "'";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSql))
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

        #region metodo exclui
        /// <summary>
        /// M�todo que exclui uma pessoa
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui(out String strMensagem)
        {
            try
            {
                //Valida a exclus�o.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMensagem, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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

        #region metodo exclui
        /// <summary>
        /// M�todo que exclui os direitos dos pap�is de acordo com o nome da tabela
        /// </summary>
        /// <param name="strNomeTabela">Nome da Tabela</param>
        /// <param name="intCodigoPapel">C�digo do pap�l</param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public static bool exclui(string strNomeTabela, int intCodigoPapel)
        {
            try
            {
                string strSql = string.Empty;
                strSql = "DELETE FROM segurancadireitopapel WHERE tabela = '" + strNomeTabela.Trim() + "' AND seguranca_papel_codigo = " + intCodigoPapel;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if(objBanco.executaSQL(strSql))
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
    }
}
