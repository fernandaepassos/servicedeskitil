using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsAuditado
/// </summary>

namespace ServiceDesk.Negocio
{
  public class ClsAuditado
  {
    
    //Colecao de atributos do Auditado
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um Auditado
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPessoaInclusao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAuditoria = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabelaIdentificador = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAuditor = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objComentario = new ServiceDesk.Banco.ClsAtributo();

    #region Propriedades

    public ServiceDesk.Banco.ClsAtributos Atributos
    {
      get { return this.objAtributos; }
    }

    /// <summary>
    /// Código
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Codigo
    {
      get { return objCodigo; }
      set { this.objCodigo = value; }
    }

    /// <summary>
    /// Pessoa que inseriu a Auditoria
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo PessoaInclusao
    {
      get { return objPessoaInclusao; }
      set { this.objPessoaInclusao = value; }
    }

    /// <summary>
    /// Auditoria
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Auditoria
    {
      get { return objAuditoria; }
      set { this.objAuditoria = value; }
    }

    /// <summary>
    /// Identificador da Tabela
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo TabelaIdentificador
    {
      get { return objTabelaIdentificador; }
      set { this.objTabelaIdentificador = value; }
    }

    /// <summary>
    /// Auditor
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Auditor
    {
      get { return objAuditor; }
      set { this.objAuditor = value; }
    }

    /// <summary>
    /// Data da inclusão
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo DataInclusao
    {
      get { return objDataInclusao; }
      set { this.objDataInclusao = value; }
    }

    /// <summary>
    /// Status
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Status
    {
      get { return objStatus; }
      set { this.objStatus = value; }
    }

    /// <summary>
    /// Comentário
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Comentario
    {
      get { return objComentario; }
      set { this.objComentario = value; }
    }

    #endregion

    #region metodos

    #region Construtor da classe
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    public ClsAuditado()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsAuditado(int intCodigo)
    {
      this.alimentaColecaoCampos();
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region alimentaColecaoCampos

    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.NomeTabela = "Auditado";
      objAtributos.DescricaoTabela = "Auditado";

      objCodigo.Campo = "auditado_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = false;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objPessoaInclusao.Campo = "pessoa_inclusao";
      objPessoaInclusao.Descricao = "Inclusor";
      objPessoaInclusao.CampoIdentificador = false;
      objPessoaInclusao.CampoObrigatorio = false;
      objPessoaInclusao.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objPessoaInclusao);

      objAuditoria.Campo = "auditoria_codigo";
      objAuditoria.Descricao = "Nome";
      objAuditoria.CampoIdentificador = false;
      objAuditoria.CampoObrigatorio = false;
      objAuditoria.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objAuditoria);

      objTabelaIdentificador.Campo = "tabela_identificador";
      objTabelaIdentificador.Descricao = "Tabela";
      objTabelaIdentificador.CampoIdentificador = false;
      objTabelaIdentificador.CampoObrigatorio = false;
      objTabelaIdentificador.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objTabelaIdentificador);

      objAuditor.Campo = "auditor_codigo";
      objAuditor.Descricao = "Data da Alteração";
      objAuditor.CampoIdentificador = false;
      objAuditor.CampoObrigatorio = false;
      objAuditor.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objAuditor);

      objDataInclusao.Campo = "data_inclusao";
      objDataInclusao.Descricao = "Data da Inclusão";
      objDataInclusao.CampoIdentificador = false;
      objDataInclusao.CampoObrigatorio = false;
      objDataInclusao.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataInclusao);

      objStatus.Campo = "status_codigo";
      objStatus.Descricao = "Status";
      objStatus.CampoIdentificador = false;
      objStatus.CampoObrigatorio = false;
      objStatus.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatus);

      objComentario.Campo = "comentario";
      objComentario.Descricao = "Comentário";
      objComentario.CampoIdentificador = false;
      objComentario.CampoObrigatorio = false;
      objComentario.Tipo = System.Data.DbType.String;
      objAtributos.Add(objComentario);

    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um Auditado.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objStatus.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a Situação do Auditado.";
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
    /// Método que altera um Auditado
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objStatus.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a Situação do Auditado.";
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
    /// Método que exclui um Auditado
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui()
    {
        try
        {
            string strMsg = string.Empty;

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

    #region metodo geraGridView
    /// <summary>
    /// Método que gera um Grid View de Auditados
    /// </summary>
    /// <param name="objGridView">ObjetoGridView</param>
    /// <param name="intCodigoAuditoria">Código da auditoria</param>
    public static void geraGridView(GridView objGridView, int intCodigoAuditoria)
    {
        try
        {
            String strSql = String.Empty;
            strSql = "SELECT *";
            strSql += " FROM auditado";
            if (intCodigoAuditoria > 0)
            {
                strSql = " WHERE auditoria _codigo = " + intCodigoAuditoria.ToString();
            }

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

    #region metodo geraGridView
    /// <summary>
    /// Método que gera um Grid View de Auditados
    /// </summary>
    /// <param name="objGridView">ObjetoGridView</param>
    public static void geraGridView(GridView objGridView, int intCodigoAuditoria, String strTabela, int intIdentificador)
    {
        try
        {
            String strSql = String.Empty;

            if ((strTabela != String.Empty) && (intIdentificador > 0))
            {
                strSql = "SELECT *";
                strSql += " FROM auditado, auditoria";
                strSql = " WHERE auditado.auditoria _codigo = auditoria.auditoria _codigo";
                strSql = " AND tabela = '" + strTabela + "'";
                strSql = " AND tabela_identificador = '" + intIdentificador.ToString() + "'";

                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);

                objGridView.DataSource = objDataSet;
                objGridView.DataBind();

                objDataSet = null;

            }
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