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
/// Classe Chamado Origem
/// </summary>
namespace ServiceDesk.Negocio
{
  public class ClsChamadoOrigem
  {
    //Colecao de atributos de ChamadoOrigem
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de ChamadoOrigem
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();

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
    /// Descrição
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get { return objDescricao; }
      set { this.objDescricao = value; }
    }
    #endregion

    #region metodos
    #region Construtor da classe
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    public ClsChamadoOrigem()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsChamadoOrigem(int intCodigo)
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
      objAtributos.NomeTabela = "ChamadoOrigem";
      objAtributos.DescricaoTabela = "Origem do Chamado";

      objCodigo.Campo = "chamado_origem_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = false;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoIdentificador = false;
      objDescricao.CampoObrigatorio = false;
      objDescricao.Tipo = System.Data.DbType.String;
      objAtributos.Add(objDescricao);
    }
    #endregion

    #region metodo existeDescricao
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool existeDescricao()
    {
      //Verifica se já existe um determinado papel pela descrição
      bool bolRetorno = false;
      ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
      string strExiste = string.Empty;
      strExiste = banco.retornaValorCampo("ChamadoOrigem", "chamado_origem_codigo", "descricao ='" + this.objDescricao.Valor.Trim() + "' AND chamado_origem_codigo <> " + this.objCodigo.Valor.Trim());
      if (strExiste != String.Empty)
      {
        bolRetorno = true;
      }
      return bolRetorno;
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere uma nova Origem do Chamado
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
          strMensagem = "Favor informar a descrição da Origem do Chamado.";

        if (strMensagem == String.Empty)
        {
          if (existeDescricao() == false)
          {
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.insereColecao(this.objAtributos))
            {
              strMensagem = "Origem inserida com sucesso.";
              bolRetorno = true;
            }
            objBanco = null;
          }
          else
            strMensagem = "Já existe uma origem cadastrada com esta descrição.";
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
    /// Método que altera uma Origem do chamado
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objDescricao.Valor.Trim() == String.Empty)
          strMensagem = "Favor informar a descrição da Origem do Chamado.";

        if (strMensagem == String.Empty)
        {
          if (existeDescricao() == false)
          {
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.alteraColecao(this.objAtributos))
            {
              strMensagem = "Origem atualizada com sucesso.";
              bolRetorno = true;
            }
            objBanco = null;
          }
          else
            strMensagem = "Já existe uma origem cadastrada com esta descrição.";
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
    /// Método que exclui uma Origem do chamado
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
        ServiceDesk.Negocio.ClsChamadoOrigem objChamadoOrigem = new ServiceDesk.Negocio.ClsChamadoOrigem();
        //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objChamadoOrigem.objAtributos);
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strSql = objBanco.montaQuery(objChamadoOrigem.objAtributos, false);
        strSql += " ORDER BY descricao";
        System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
        objGridView.DataSource = objDataSet;
        objGridView.DataBind();
        objDataSet.Dispose();
        objDataSet = null;
        objBanco = null;
        objChamadoOrigem = null;
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