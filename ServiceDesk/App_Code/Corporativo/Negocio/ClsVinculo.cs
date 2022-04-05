/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe responsável por todas as associações entre tabela no banco de dados.
  
  	Data: 02/03/2006
  	Autor: Fernanda Passos
  	Descrição: Esta classe permite o relacionamento entre todas as tabelas do banco de dados.
  
  • Alterações
  	Data: 
  	Autor: 
  	Descrição: 
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

/// <summary>
/// Classe que permite relacionamento entre tabelas.
/// </summary>
namespace SServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsVinculo
    {
        #region Construtor da classe
        public ClsVinculo()
        {
            alimentaColecaoCampos();
        }
        #endregion

        #region Construtor da classe por parametro
        /// <summary>
        /// Construtor da classe por parametro
        /// </summary>
        /// <param name="intCodigo">Código do Vinculo</param> 
        public ClsVinculo(int intCodigo)
        {
            try
            {
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

        #region Declarações
        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabelaOrigem = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objIdentificadorOrigem = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabelaDestino = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objIdentificadorDestino = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades
        /// <summary>
        /// Coleção de atributos
        /// </summary>
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        /// <summary>
        /// Codigo do registro da tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Nome físico da tabela de origem.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TabelaOrigem 
        {
            get { return objTabelaOrigem ; }
            set { this.objTabelaOrigem = value; }
        }

        /// <summary>
        /// Código identificador na tabela de origem.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo IdentificadorOrigem
        {
            get { return objIdentificadorOrigem; }
            set { this.objIdentificadorOrigem = value; }
        }

        /// <summary>
        /// Nome físico da tabela de destino.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TabelaDestino
        {
            get { return objTabelaDestino; }
            set { this.objTabelaDestino = value; }
        }

        /// <summary>
        /// Código identificador na tabela de destino.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo IdentificadorDestino
        {
            get { return objIdentificadorDestino ; }
            set { this.objIdentificadorDestino = value; }
        }
        #endregion

        #region Metodos

        #region Verifica se o registro já existe na tabela
        /// <summary>
        /// Verifica se o registro já existe na tabela
        /// </summary>
        /// <param name="intIdentificadorDestino">Código do registro na tabela de destino</param> 
        /// <param name="intIdentificadorOrigem">Código do registro na tabela de origem</param> 
        /// <param name="strTabelaDestino">Nome físico na tabela de destino</param> 
        /// <param name="strTabelaOrigem">Nome físico na tabela de origem</param> 
        public static bool VerificaSeRegistroJaExiste(string strTabelaOrigem, int intIdentificadorOrigem, string strTabelaDestino, int intIdentificadorDestino)
        {
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSql = " vinculo_codigo is not null";
                if (strTabelaOrigem.Trim() != string.Empty) strSql += " and tabela_origem = '" + strTabelaOrigem.Trim() + "'";
                if (intIdentificadorOrigem != 0) strSql += " and identificador_origem = " + intIdentificadorOrigem + "";
                if (strTabelaDestino.Trim() != string.Empty) strSql += " and tabela_destino = '" + strTabelaDestino.Trim() + "'";
                if (intIdentificadorDestino != 0) strSql += "and identificador_destino = " + intIdentificadorDestino + ""; 

                if (objBanco.retornaValorCampo("vinculo", "vinculo_codigo", strSql) != string.Empty)
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

        #region Alimenta campos coleção
        /// <summary>
        /// Alimenta campos coleção
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "Vinculo";
                objAtributos.DescricaoTabela = "Tabela que armazena as associacoes de registros entre as tabelas do banco de dados.";

                objCodigo.Campo = "vinculo_codigo";
                objCodigo.Descricao = "Código identificador do registro na tabela.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objTabelaOrigem.Campo = "tabela_origem";
                objTabelaOrigem.Descricao = "Nome fisico da tabela de origem";
                objTabelaOrigem.CampoIdentificador = false;
                objTabelaOrigem.CampoObrigatorio = true;
                objTabelaOrigem.Tipo = System.Data.DbType.String;
                objTabelaOrigem.Tamanho = 100;
                objAtributos.Add(objTabelaOrigem);

                objIdentificadorOrigem.Campo = "identificador_origem";
                objIdentificadorOrigem.Descricao = "Codigo identificador da tabela de origem";
                objIdentificadorOrigem.CampoIdentificador = false;
                objIdentificadorOrigem.CampoObrigatorio = true;
                objIdentificadorOrigem.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objIdentificadorOrigem);

                objTabelaDestino.Campo = "tabela_destino";
                objTabelaDestino.Descricao = "Nome fisico na tabela de destino";
                objTabelaDestino.CampoIdentificador = false;
                objTabelaDestino.CampoObrigatorio = true;
                objTabelaDestino.Tipo = System.Data.DbType.String;
                objTabelaDestino.Tamanho = 100;
                objAtributos.Add(objTabelaDestino);

                objIdentificadorDestino.Campo = "identificador_destino";
                objIdentificadorDestino.Descricao = "Codigo identificador na tabela de destino.";
                objIdentificadorDestino.CampoIdentificador = false;
                objIdentificadorDestino.CampoObrigatorio = true;
                objIdentificadorDestino.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objIdentificadorDestino);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Verifica se todos os campos foram preenchidos
        /// <summary>
        /// Verifica se todos os campos foram preenchidos
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou não.</returns>
        public bool VerificaCampoObrigatorio(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objCodigo.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o código.";
                    return false;
                }
                else if (objTabelaDestino.Valor.Trim() == string.Empty)
                {
                    strMsg = "Por favor, informe a tabela de destino.";
                    return false;
                }
                else if (objIdentificadorDestino.Valor.Trim() == string.Empty)
                {
                    strMsg = "Por favor, informe o identificador na tabela de destino.";
                    return false;
                }
                else if (objTabelaOrigem.Valor.Trim() == string.Empty)
                {
                    strMsg = "Por favor, informe a tabela de origem.";
                    return false;
                }
                else if (objIdentificadorOrigem.Valor.Trim() == string.Empty)
                {
                    strMsg = "Por favor, informe o identificador na tabela de origem.";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }

        }

        #endregion

        #region Inserir
        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="strMensagem">Mensagem com status da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (VerificaCampoObrigatorio(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeRegistroJaExiste(objTabelaOrigem.Valor,Convert.ToInt32(objIdentificadorOrigem.Valor), objTabelaDestino.Valor,Convert.ToInt32(objIdentificadorDestino.Valor) ) == true)
                    {
                        bolRetorno = false;
                    }
                    else if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Registro inserido com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;
            }
            catch (Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Alterar
        /// <summary>
        /// Alterar
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (VerificaCampoObrigatorio(out strMensagem) == false) bolRetorno = false;
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeRegistroJaExiste(objIdentificadorOrigem.Valor.Trim(), Convert.ToInt32(objIdentificadorOrigem.Valor),objTabelaDestino.Valor,Convert.ToInt32(objTabelaDestino.Valor)) == true) bolRetorno = false;
                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Registro alterado com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;
            }
            catch (Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Excluir
        /// <summary>
        /// Excluir
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                //Valida a exclusão.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(objAtributos))
                {
                    strMsg = "Registro excluído com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMsg = "Registro não excluído.";
                    objBanco = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Cria Vinculo entre tabelas
        /// <summary>
        /// Cria Vinculo entre tabelas
        /// </summary>
        /// <param name="strTabelaOrigem">Nome físico da tabela de origem</param>
        /// <param name="strTabelaDestino">Nome físico da tabela de destino</param>
        /// <param name="intIdentificadorTabeleOrigem">Código identificador na tabela de origem</param>
        /// <param name="intIdentificadorTabelaDestino">Código identificador na tabela de destino</param>
        /// <returns>Retorna tru ou false. Se foi vinculado ou não.</returns>
        public static bool CriaVinculoEntreTabela(string strTabelaOrigem, string strTabelaDestino, int intIdentificadorTabeleOrigem, int intIdentificadorTabelaDestino, out string strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                SServiceDesk.Negocio.ClsVinculo objVinculo = new ClsVinculo();

                ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                objIdentificador.Tabela.Valor = objVinculo.Atributos.NomeTabela;
                objVinculo.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                objVinculo.TabelaDestino.Valor = strTabelaDestino.Trim();
                objVinculo.objIdentificadorDestino.Valor = intIdentificadorTabelaDestino.ToString ();
                objVinculo.objTabelaOrigem.Valor = strTabelaOrigem.Trim();
                objVinculo.objIdentificadorOrigem.Valor = intIdentificadorTabeleOrigem.ToString ();

                if(objVinculo.insere(out strMensagem)== true ) 
                {
                    objIdentificador.atualizaValor();
                    objIdentificador = null ;
                    objVinculo = null;
                    return true ; 
                }
                else 
                {
                    objIdentificador = null ;
                    objVinculo = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion
        #endregion
    }
}