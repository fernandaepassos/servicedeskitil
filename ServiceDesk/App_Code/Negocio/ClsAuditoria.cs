using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsAuditoria
/// </summary>

namespace ServiceDesk.Negocio
{
  public class ClsAuditoria
  {
    
    //Colecao de atributos de Status
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um Atributo do Item de Configuração
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPessoaInclusao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPessoaAlteracao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataInicialReal = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataInicialPrevista = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataFinalReal = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataFinalPrevista = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataAlteracao = new ServiceDesk.Banco.ClsAtributo();
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
    /// Pessoa que realizou a última Alteração
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo PessoaAlteracao
    {
      get { return objPessoaAlteracao; }
      set { this.objPessoaAlteracao = value; }
    }

    /// <summary>
    /// Data Inicial Real
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo DataInicialReal
    {
      get { return objDataInicialReal; }
      set { this.objDataInicialReal = value; }
    }

    /// <summary>
    /// Data Inicial Prevista
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo DataInicialPrevista
    {
      get { return objDataInicialPrevista; }
      set { this.objDataInicialPrevista = value; }
    }

    /// <summary>
    /// Data Final Real
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo DataFinalReal
    {
      get { return objDataFinalReal; }
      set { this.objDataFinalReal = value; }
    }

    /// <summary>
    /// Data Final Prevista
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo DataFinalPrevista
    {
      get { return objDataFinalPrevista; }
      set { this.objDataFinalPrevista = value; }
    }

    /// <summary>
    /// Tabela
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Tabela
    {
      get { return objTabela; }
      set { this.objTabela = value; }
    }

    /// <summary>
    /// Nome da Auditoria
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Nome
    {
      get { return objNome; }
      set { this.objNome = value; }
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
    /// Data da alteração
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo DataAlteracao
    {
      get { return objDataAlteracao; }
      set { this.objDataAlteracao = value; }
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
    public ClsAuditoria()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsAuditoria(int intCodigo)
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
      objAtributos.NomeTabela = "Auditoria";
      objAtributos.DescricaoTabela = "Auditoria";

      objCodigo.Campo = "auditoria_codigo";
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

      objPessoaAlteracao.Campo = "pessoa_alteracao";
      objPessoaAlteracao.Descricao = "Inclusor";
      objPessoaAlteracao.CampoIdentificador = false;
      objPessoaAlteracao.CampoObrigatorio = false;
      objPessoaAlteracao.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objPessoaAlteracao);

      objTabela.Campo = "tabela";
      objTabela.Descricao = "Tabela";
      objTabela.CampoIdentificador = false;
      objTabela.CampoObrigatorio = false;
      objTabela.Tipo = System.Data.DbType.String;
      objAtributos.Add(objTabela);

      objNome.Campo = "nome";
      objNome.Descricao = "Nome";
      objNome.CampoIdentificador = false;
      objNome.CampoObrigatorio = false;
      objNome.Tipo = System.Data.DbType.String;
      objAtributos.Add(objNome);

      objDataInclusao.Campo = "data_inclusao";
      objDataInclusao.Descricao = "Data da Inclusão";
      objDataInclusao.CampoIdentificador = false;
      objDataInclusao.CampoObrigatorio = false;
      objDataInclusao.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataInclusao);

      objDataAlteracao.Campo = "data_alteracao";
      objDataAlteracao.Descricao = "Data da Alteração";
      objDataAlteracao.CampoIdentificador = false;
      objDataAlteracao.CampoObrigatorio = false;
      objDataAlteracao.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataAlteracao);

      objDataInicialReal.Campo = "data_inicial_real";
      objDataInicialReal.Descricao = "Data Inicial Real";
      objDataInicialReal.CampoIdentificador = false;
      objDataInicialReal.CampoObrigatorio = false;
      objDataInicialReal.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataInicialReal);

      objDataInicialPrevista.Campo = "data_inicial_prevista";
      objDataInicialPrevista.Descricao = "Data Inicial Prevista";
      objDataInicialPrevista.CampoIdentificador = false;
      objDataInicialPrevista.CampoObrigatorio = false;
      objDataInicialPrevista.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataInicialPrevista);

      objDataFinalReal.Campo = "data_final_real";
      objDataFinalReal.Descricao = "Data Final Real";
      objDataFinalReal.CampoIdentificador = false;
      objDataFinalReal.CampoObrigatorio = false;
      objDataFinalReal.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataFinalReal);

      objDataFinalPrevista.Campo = "data_final_prevista";
      objDataFinalPrevista.Descricao = "Data da Alteração";
      objDataFinalPrevista.CampoIdentificador = false;
      objDataFinalPrevista.CampoObrigatorio = false;
      objDataFinalPrevista.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataFinalPrevista);

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
    /// Método que insere uma Auditoria.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objNome.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar o Nome da Auditoria.";
            }
            else if (this.Status.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar o Status da Auditoria.";
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
    /// Método que altera uma Auditoria
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objNome.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar o Nome da Auditoria.";
            }
            else if (this.Status.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar o Status da Auditoria.";
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
    /// Método que exclui uma Auditoria
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

    #region metodo geraDropDownListStatus
    /// <summary>
    /// Metodo que gera o dropdownlist dos status da Auditoria
    /// </summary>
    /// <param name="objDropDownList"></param>
    public static void geraDropDownListStatus(DropDownList objDropDownList)
    {
        try
        {
            ClsAuditoria objAuditoria = new ClsAuditoria();
            ClsStatusTabela.geraDropDownList(objDropDownList, objAuditoria.Atributos.NomeTabela);
            objAuditoria = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraCheckBoxList
    /// <summary>
    /// Método que gera um CheckBox
    /// </summary>
    /// <param name="objCheckBoxList">Objeto CheckBoxList</param>
    public static void geraCheckBoxList(CheckBoxList objCheckBoxList)
    {
        try
        {
            ClsAuditoria objAuditoria = new ClsAuditoria();
            objCheckBoxList.DataTextField = objAuditoria.Nome.Campo;
            objCheckBoxList.DataValueField = objAuditoria.Codigo.Campo;
            ServiceDesk.Controle.ClsCheckBoxList.geraCheckBoxList(objCheckBoxList, objAuditoria.Atributos);
            objAuditoria = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Método que gera um Grid View de Atributo do Item de Configuração
    /// </summary>
    /// <param name="objGridView">ObjetoGridView</param>
    public static void geraGridView(GridView objGridView)
    {
        try
        {
            String strSql = String.Empty;
            strSql = "SELECT *";
            strSql += " FROM auditoria";


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

    #region metodo geraDropDownListAuditor
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">objjeto Grid View</param>
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraDropDownListAuditor(System.Web.UI.WebControls.DropDownList objDropDownList, int intCodigoAuditoria)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "SELECT pessoa.pessoa_codigo, nome";
            strSql += " FROM pessoa, AuditoriaPessoa";
            strSql += " WHERE pessoa.pessoa_codigo = AuditoriaPessoa.pessoa_codigo";
            strSql += " AND auditoria_codigo = " + intCodigoAuditoria.ToString();

            objDropDownList.DataTextField = "nome";
            objDropDownList.DataValueField = "pessoa_codigo";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            objDropDownList.DataSource = objDataReader;
            objDropDownList.DataBind();
            objDataReader = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo retornaPessoaAuditoria
    /// <summary>
    /// Pega o codigo da Pessoa da Auditoria (Auditor)
    /// </summary>
    /// <param name="intCodigoItem">Código da auditoria</param>
    /// <param name="intCodigoPessoa">Código da Pessoa</param>
    /// <returns>Retorna um inteiro com o código do auditor, se não encontrar retorna 0.</returns>
    public static int retornaItemPessoaTipo(int intCodigoAuditoria, int intCodigoPessoa)
    {
        try
        {
            int intRetorno = 0;
            String strRetorno = String.Empty;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            strRetorno = objBanco.retornaValorCampo("auditoriapessoa", "pessoa_codigo", "auditoria_codigo = " + intCodigoAuditoria.ToString() + " AND pessoa_codigo = " + intCodigoPessoa.ToString());
            if (strRetorno != String.Empty)
            {
                intRetorno = Convert.ToInt32(strRetorno);
            }
            objBanco = null;

            return intRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo excluiAuditor
    /// <summary>
    /// Exclui as relações existentes entre auditoria x pessoa (auditor)
    /// </summary>
    /// <param name="intCodigoAuditoria">Código da auditoria</param>
    /// <returns>Retorna verdadeiro ou falso</returns>
    public static bool excluiAuditor(int intCodigoAuditoria)
    {
        try
        {
            bool bolRetorno = false;
            String strSql = String.Empty;

            strSql = "DELETE FROM AuditoriaPessoa";
            strSql += " WHERE auditoria_codigo = " + intCodigoAuditoria.ToString();

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.executaSQL(strSql))
            {
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

    #region insereAuditor
    /// <summary>
    /// Método que insere um novo auditor, relação entre as tabelas auditoria x pessoa.
    /// </summary>
    /// <param name="intCodigoAuditoria">Código da auditoria</param>
    /// <param name="intCodigoPessoa">Código da pessoa</param>
    /// <returns>Verdadeiro ou falso.</returns>
    public static bool insereAuditor(int intCodigoAuditoria, int intCodigoPessoa)
    {
        try
        {
            bool bolRetorno = false;
            String strSql = String.Empty;

            strSql = "INSERT INTO AuditoriaPessoa";
            strSql += " (auditoria_codigo, pessoa_codigo)";
            strSql += " VALUES";
            strSql += " (" + intCodigoAuditoria.ToString() + ", " + intCodigoPessoa.ToString() + ")";

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.executaSQL(strSql))
            {
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

    #endregion

  }
}