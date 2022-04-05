/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela de Projeto.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descrição: Esta classe apresenta várias funcionalidades que permite manipular os dados
    da tabela Projeto.
  
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

/// <summary>
/// Classe de acesso aos dados da tabela Projeto.
/// </summary>
namespace ServiceDesk.Projeto
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsProjetoPessoa
    {
        #region Construtor da classe
        public ClsProjetoPessoa()
        {
            alimentaColecaoCampos();
        }
        #endregion

        #region Declarações
        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigoProjeto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoPessoa = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do projeto.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoProjeto
        {
            get { return objCodigoProjeto; }
            set { this.objCodigoProjeto = value; }
        }

        /// <summary>
        /// Código da pessoa.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoPessoa
        {
            get { return objCodigoPessoa; }
            set { this.objCodigoPessoa = value; }
        }

        #endregion

        #region Metodos

        #region Valida a exclusão
        /// <summary>
        /// Valida exclusão.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação do motivo da não exclusão.</param>
        /// <returns>Retorna true ou false. Se validado ou não.</returns>
        public bool ValidaExclusao(out String strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                return true;
            }
            catch (Exception ex)
            {
                strMensagem = "Processo de validação da exclusão abortado. Exclusão cancelada.";
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
                objAtributos.NomeTabela = "ProjetoPessoa";
                objAtributos.DescricaoTabela = "Tabela que armazena os responsáveis pelas atividades dos projetos.";

                objCodigoProjeto.Campo = "projeto_codigo";
                objCodigoProjeto.Descricao = "Código do projeto.";
                objCodigoProjeto.CampoIdentificador = true;
                objCodigoProjeto.CampoObrigatorio = true;
                objCodigoProjeto.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProjeto);

                objCodigoPessoa.Campo = "pessoa_codigo";
                objCodigoPessoa.Descricao = "Código da pessoa.";
                objCodigoPessoa.CampoIdentificador = true;
                objCodigoPessoa.CampoObrigatorio = true;
                objCodigoPessoa.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoPessoa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Validacao dos dados
        /// <summary>
        /// Validação da integridade dos registros.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou não.</returns>
        public bool ValidacaoDados(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objCodigoProjeto.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o projeto.";
                    return false;
                }
                else if (objCodigoPessoa.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe a pessoa.";
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

        #region Verifica se já existe o registro no banco de dados.
        /// <summary>
        /// Verifica se já existe o registro no banco de dados.
        /// </summary>
        /// <param name="strMsg">Mensagem que o método retorna com status da operação.</param> 
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMsg)
        {
            strMsg = string.Empty;
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSqlCriterio = " projeto_codigo = " + Convert.ToInt32(objCodigoProjeto.Valor.Trim()) + " ";
                strSqlCriterio += " and pessoa_codigo = " + Convert.ToInt32(objCodigoPessoa.Valor.Trim()) + "";

                string strValor = objBanco.retornaValorCampo("ProjetoPessoa", "projeto_codigo", strSqlCriterio);

                //Verifica se o nome foi encontrado.
                if (strValor.Trim() != string.Empty)
                {
                    //strMsg = "A pessoa selecionada já esta como responsável.";
                    return true; //Foi encontrador. Existe no banco.
                }
                else
                    return false; // Não encontrado. Não existe no banco.
            }
            catch (Exception ex)
            {
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

                if (ValidacaoDados(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeJaExisteNoBanco(out strMensagem) == true)
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

                if (ValidacaoDados(out strMensagem) == false) bolRetorno = false;
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeJaExisteNoBanco(out strMensagem) == true) bolRetorno = false;
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

        #region Alterar
        /// <summary>
        /// Alterar
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera()
        {
            try
            {
                string strSql = "UPDATE ProjetoPessoa";
                strSql += " SET pessoa_codigo = " + Convert.ToInt32(CodigoPessoa.Valor.Trim()) + "";
                strSql += " WHERE projeto_codigo = " + Convert.ToInt32(CodigoProjeto.Valor.Trim()) + "";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSql) == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
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

                ////Valida a exclusão.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objobjCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
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

        #region Excluir
        /// <summary>
        /// Excluir
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui()
        {
            try
            {
                string strSql = string.Empty;

                strSql = " Delete from ProjetoPessoa";
                strSql += " where projeto_codigo = " + Convert.ToInt32(CodigoProjeto.Valor) + "";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                if (objBanco.executaSQL(strSql) == true)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsProjetoPessoa objProjetoPessoa, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProjetoPessoa.Atributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera gridview com os projetos da pessoa informada.
        /// </summary>
        /// <param name="objGridView">Nome do GridView que receberá os dados.</param>
        /// <param name="intCodigoPessoa">Código do usuário\pessoa responsável.</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int intCodigoPessoa)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select P.nome as Atividade, P.data_inicio_prevista, ";
                strSql += " P.data_fim_prevista, P.data_inicio_real, P.data_fim_real, PE.nome as Responsavel ";
                strSql += " from Projeto P, ProjetoPessoa PP, Pessoa PE";
                strSql += " where P.projeto_codigo = PP.projeto_codigo";
                strSql += " and PP.pessoa_codigo = PE.pessoa_codigo";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataBind();
                objBanco = null;
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