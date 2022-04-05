using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsItemConfiguracaoRelacaoPessoa
/// </summary>

namespace ServiceDesk.Negocio
{
    public class ClsItemConfiguracaoRelacaoPessoa
    {

        #region Declara��es
        //Colecao de atributos do Tipo de Estrutura do Item de Configuracao
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos do tipo de estrutura do Item de Configuracao
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        #endregion
        
        #region Propriedades
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get { return this.objAtributos; }
        }

        /// <summary>
        /// C�digo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descri��o.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }
        #endregion

        #region M�todos

        #region Construtor da Classe
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        public ClsItemConfiguracaoRelacaoPessoa()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region Construtor da Classe com passagem de parametro
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        public ClsItemConfiguracaoRelacaoPessoa(int intCodigoPessoaTipo)
        {
            try
            {
                this.alimentaColecaoCampos();
                this.Codigo.Valor = intCodigoPessoaTipo.ToString();
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

        #region metodo alimentaColecaoCampos
        /// <summary>
        /// M�todo que alimenta a cole��o de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "ICRelacaoPessoaTipo";
            objAtributos.DescricaoTabela = "Tipos de Pessoa dos Itens de Configura��o";

            objCodigo.Campo = "ic_pessoa_tipo_codigo";
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

        }
        #endregion

        #region metodo insere
        /// <summary>
        /// M�todo que insere um novo Tipo de Pessoa do Item de Configura��o.
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
                    strMensagem = "Favor informar o Nome do Tipo de Pessoa do Item de Configura��o.";
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

        #region metodo altera
        /// <summary>
        /// M�todo que altera um Tipo de Pessoa do Item de Configura��o
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objDescricao.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Nome do Tipo de Pessoa do Item de Configura��o.";
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
        /// M�todo que exclui um Tipo de Pessoa do Item de Configura��o
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

        #region geraDropDownList
        /// <summary>
        /// Gera o DropDownList dos tipos de pessoa dos itens de configura��o com excess�o do item que tem o c�digo igual ao passado pelo parametro (caso o mesmo seja diferente de 0(zero))
        /// </summary>
        /// <param name="objDropDownList">Objeto DropDownList</param>
        /// <param name="intCodigoItem">C�digo do Tipo da Pessoa do Item que n�o ser� listado</param>
        public static void geraDropDownList(DropDownList objDropDownList, int intCodigoPessoaTipo)
        {
            try
            {
                ServiceDesk.Negocio.ClsItemConfiguracaoRelacaoPessoa objItemConfiguracaoRelacaoPessoa = new ServiceDesk.Negocio.ClsItemConfiguracaoRelacaoPessoa();
                objDropDownList.DataTextField = objItemConfiguracaoRelacaoPessoa.Descricao.Campo;
                objDropDownList.DataValueField = objItemConfiguracaoRelacaoPessoa.Codigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objItemConfiguracaoRelacaoPessoa.Atributos);
                if (intCodigoPessoaTipo > 0)
                {
                    objDropDownList.Items.FindByValue(intCodigoPessoaTipo.ToString()).Enabled = false;
                }
                objItemConfiguracaoRelacaoPessoa = null;
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
