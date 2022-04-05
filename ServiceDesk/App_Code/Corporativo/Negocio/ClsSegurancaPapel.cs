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

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Seguranca Papel
  /// </summary>
  public class ClsSegurancaPapel
  {

    //Colecao de atributos da Seguranca Papel.
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    # region Atributos.
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCampoTabela = new ServiceDesk.Banco.ClsAtributo();
    #endregion

    #region Construtor
    public ClsSegurancaPapel()
    {
      this.alimentaColecaoCampos();
    }

    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsSegurancaPapel(int intCodigo)
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
    /// Adiciona todos os atributos de um Perfil, a coleção de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "SegurancaPapel";
      objAtributos.NomeTabela = "SegurancaPapel";

      objCodigo.Campo = "seguranca_papel_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objAtributos.Add(objDescricao);

      objCampoTabela.Campo = "campo_tabela";
      objCampoTabela.Descricao = "Campo da Tabela";
      objCampoTabela.CampoObrigatorio = true;
      objCampoTabela.Tipo = System.Data.DbType.String;
      objAtributos.Add(objCampoTabela);
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

    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get
      {
        return objDescricao;
      }
      set
      {
        objDescricao = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo CampoTabela
    {
      get
      {
        return objCampoTabela;
      }
      set
      {
        objCampoTabela = value;
      }
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
      strExiste = banco.retornaValorCampo("SegurancaPapel", "seguranca_papel_codigo", "descricao ='" + this.objDescricao.Valor.Trim() + "'");
      if (strExiste != String.Empty)
      {
        bolRetorno = true;
      }
      return bolRetorno;
    }
    #endregion

    #region metodo existeDescricao
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool existeDescricao(int intCodigo)
    {
        //Verifica se já existe um determinado papel pela descrição
        bool bolRetorno = false;
        ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
        string strExiste = string.Empty;
        strExiste = banco.retornaValorCampo("SegurancaPapel", "seguranca_papel_codigo", "descricao ='" + this.objDescricao.Valor.Trim() + "' AND seguranca_papel_codigo <> " + intCodigo);
        if (strExiste != String.Empty)
        {
            bolRetorno = true;
        }
        return bolRetorno;
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
      objGridView.AutoGenerateColumns = false;
      ClsSegurancaPapel objSegurancaPapel = new ClsSegurancaPapel();
      ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objSegurancaPapel.objAtributos);
      objSegurancaPapel = null;
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsSegurancaPapel objSegurancaPapel, bool bolCondicao)
    {
      objGridView.AutoGenerateColumns = false;
      ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objSegurancaPapel.objAtributos, bolCondicao);

    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
      ClsSegurancaPapel objSegurancaPapel = new ClsSegurancaPapel();
      objDropDownList.DataTextField = objSegurancaPapel.objDescricao.Campo;
      objDropDownList.DataValueField = objSegurancaPapel.objCodigo.Campo;
      ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objSegurancaPapel.objAtributos);
      objSegurancaPapel = null;
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um novo cargo.
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
              {
                  strMensagem = strMensagem + "Favor informar a descrição do Papel.<br>";
              }
              else
              {
                  if (existeDescricao(Convert.ToInt32(objCodigo.Valor)) == false)
                  {
                      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                      if (objBanco.insereColecao(this.objAtributos))
                      {
                          strMensagem = "Papel inserido com sucesso.";
                          bolRetorno = true;
                      }
                      else
                      {
                          strMensagem = "Não foi possível inserir o papel.";
                      }
                      objBanco = null;
                  }
                  else
                  {
                      strMensagem = "Já existe um item cadastrado com esta descrição.";
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

    #region metodo altera
    /// <summary>
    /// Método que altera um tipo de Segurança papel
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
      public bool altera(out string strMensagem)
      {
          try
          {
              strMensagem = string.Empty;
              bool bolRetorno = false;

              if (this.objDescricao.Valor.Trim() == String.Empty)
              {
                  strMensagem = strMensagem + "Favor informar a descrição do Papel.<br>";
              }
              else
              {
                  if (existeDescricao(Convert.ToInt32(objCodigo.Valor)) == false)
                  {
                      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                      if (objBanco.alteraColecao(this.objAtributos))
                      {
                          objBanco = null;

                          strMensagem = "Alteração efetuada com sucesso.";
                          bolRetorno = true;
                      }
                      else
                      {
                          objBanco = null;
                          strMensagem = "Não foi possível realizar a operação.";
                      }
                  }
                  else
                  {
                      strMensagem = "Já existe um item cadastrado com esta descrição.";
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

    #region metodo exclui
    /// <summary>
    /// Método que exclui uma pessoa
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui(out String strMensagem)
    {
      try
      {

        //Valida a exclusão.
        if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMensagem, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.apagaColecao(this.objAtributos))
        {
          objBanco = null;
          strMensagem = "Item excluido com sucesso.";
          return true;
        }
        else
        {
          objBanco = null;
          strMensagem = "Não foi possivel excluir o item.";
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