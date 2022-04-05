using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe ClsWorkFlowTipo.
  /// </summary>
  public class ClsWorkFlowTipo
  {

    //Colecao de atributos do Tipo do WorkFlow
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos do Tipo do WorkFlow
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagPadrao = new ServiceDesk.Banco.ClsAtributo();

    #region Propriedades
    public ServiceDesk.Banco.ClsAtributos Atributos
    {
      get { return this.objAtributos; }
    }

    /// <summary>
    /// Código do Anexo
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Codigo
    {
      get { return objCodigo; }
    }

    /// <summary>
    /// Descrição
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get { return objDescricao; }
      set { this.objDescricao = value; }
    }

    /// <summary>
    /// Nome da Tabela
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Tabela
    {
      get { return objTabela; }
      set { this.objTabela = value; }
    }

    /// <summary>
    /// Flag Padrão
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo FlagPadrao
    {
      get { return objFlagPadrao; }
      set { this.objFlagPadrao = value; }
    }

    #endregion

    #region Construtor
    public ClsWorkFlowTipo()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsWorkFlowTipo(int intCodigo)
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
    /// Adiciona todos os atributos de um anexo a coleção de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "Tipo de WorkFlow";
      objAtributos.NomeTabela = "WorkFlowTipo";

      objCodigo.Campo = "workflow_tipo_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 50;
      objAtributos.Add(objDescricao);

      objTabela.Campo = "tabela";
      objTabela.Descricao = "Tabela";
      objTabela.Tipo = System.Data.DbType.String;
      objTabela.Tamanho = 50;
      objAtributos.Add(objTabela);

      objFlagPadrao.Campo = "flag_padrao";
      objFlagPadrao.Descricao = "Flag Padrão";
      objFlagPadrao.Tipo = System.Data.DbType.String;
      objFlagPadrao.Tamanho = 1;
      objAtributos.Add(objFlagPadrao);

    }
    #endregion

    #region metodo geraDataGrid
    /// <summary>
    /// Gera um novo DataGrid de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDataGrid">DataGrid</param>
    public static void geraDataGrid(System.Web.UI.WebControls.DataGrid objDataGrid)
    {
        try
        {
            objDataGrid.AutoGenerateColumns = false;
            ClsWorkFlowTipo objWorkFlowTipo = new ClsWorkFlowTipo();
            ServiceDesk.Controle.ClsDataGrid.geraDataGrid(objDataGrid, objWorkFlowTipo.objAtributos);
            objWorkFlowTipo = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova GridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ClsWorkFlowTipo objWorkFlowTipo = new ClsWorkFlowTipo();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objWorkFlowTipo.objAtributos);
            objWorkFlowTipo = null;
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
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsWorkFlowTipo objWorkFlowTipo, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objWorkFlowTipo.objAtributos, bolCondicao);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
        try
        {
            ClsWorkFlowTipo objWorkFlowTipo = new ClsWorkFlowTipo();
            objDropDownList.DataTextField = objWorkFlowTipo.objDescricao.Campo;
            objDropDownList.DataValueField = objWorkFlowTipo.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objWorkFlowTipo.objAtributos);
            objWorkFlowTipo = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraDropDownListItem
    /// <summary>
    /// Gera o DropDownList dos itens de configuração com excessão do item que tem o código igual ao passado pelo parametro
    /// </summary>
    /// <param name="objDropDownList">Objeto DropDownList</param>
    /// <param name="intCodigoItem">Código do Item que não será listado</param>
    public static void geraDropDownListItem(DropDownList objDropDownList, int intCodigoItem)
    {
        try
        {
            ClsWorkFlowTipo objWorkFlowTipo = new ClsWorkFlowTipo();
            objDropDownList.DataTextField = objWorkFlowTipo.objDescricao.Campo;
            objDropDownList.DataValueField = objWorkFlowTipo.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objWorkFlowTipo.Atributos);
            objDropDownList.Items.FindByValue(intCodigoItem.ToString()).Enabled = false;
            objWorkFlowTipo = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraDropDownListTabelaItem
    /// <summary>
    /// Gera o DropDownList dos itens de configuração com excessão do item que tem o código igual ao passado pelo parametro
    /// </summary>
    /// <param name="objDropDownList">Objeto DropDownList</param>
    /// <param name="intCodigoItem">Código do Item que não será listado</param>
    public static void geraDropDownListTabelaItem(DropDownList objDropDownList, String strTabela, String strMensagem)
    {
        try
        {
            ClsWorkFlowTipo objWorkFlowTipo = new ClsWorkFlowTipo();
            objDropDownList.DataTextField = objWorkFlowTipo.objTabela.Campo;
            objDropDownList.DataValueField = objWorkFlowTipo.objTabela.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objWorkFlowTipo.Atributos);
            try
            {
                objDropDownList.Items.FindByValue(strTabela).Enabled = false;
            }
            catch
            {
            }
            if (strMensagem != String.Empty)
            {
                objDropDownList.Items.Insert(0, strMensagem);
                objDropDownList.Items[0].Value = String.Empty;
            }
            objWorkFlowTipo = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a descrição do Tipo de WorkFlow.<br>";
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
    /// Método que altera.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a descrição do tipo do WorkFlow.<br>";
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
    /// Método que exclui.
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

    #region Metodo existeDescricao
    /// <summary>
    /// 
    /// </summary>
    public bool existeDescricao()
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("workflowtipo", "workflow_tipo_codigo", " LOWER(descricao) = '" + this.objDescricao.Valor.ToLower() + "'AND workflow_tipo_codigo <> " + this.objCodigo.Valor);
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;

      return bolRetorno;
    }
    #endregion

    #region Metodo existeDescricao
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strDescricao"></param>
    /// <returns></returns>
    public static bool existeDescricao(String strDescricao)
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("workflowtipo", "workflow_tipo_codigo", " LOWER(descricao) = '" + strDescricao.ToLower() + "'");
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;
      return bolRetorno;
    }
    #endregion

    #region Metodo existeTabela
    /// <summary>
    /// 
    /// </summary>
    public bool existeTabela()
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("workflowtipo", "workflow_tipo_codigo", " LOWER(tabela) = '" + this.objTabela.Valor.ToLower() + "'AND workflow_tipo_codigo <> " + this.objCodigo.Valor);
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;

      return bolRetorno;
    }
    #endregion

    #region Metodo existeTabela
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strTabela"></param>
    /// <returns></returns>
    public static bool existeTabela(String strTabela)
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("workflowtipo", "workflow_tipo_codigo", " LOWER(tabela) = '" + strTabela.ToLower() + "'");
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;
      return bolRetorno;
    }
    #endregion

  }
}