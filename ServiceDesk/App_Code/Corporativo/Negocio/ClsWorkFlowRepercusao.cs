using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe ClsWorkFlowRepercusao.
  /// </summary>
  public class ClsWorkFlowRepercusao
  {

    //Colecao de atributos do WorkFlowRepercusao
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos do WorkFlowRepercusao
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objWorkFlow = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabelaOrigem = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabelaDestino = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatusOrigem = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatusDestino = new ServiceDesk.Banco.ClsAtributo();

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
    /// Codigo do WorkFlow
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo WorkFlow
    {
      get { return objWorkFlow; }
      set { this.objWorkFlow = value; }
    }

    /// <summary>
    /// Tabela Origem
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo TabelaOrigem
    {
      get { return objTabelaOrigem; }
      set { this.objTabelaOrigem = value; }
    }

    /// <summary>
    /// Tabela Destino
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo TabelaDestino
    {
      get { return objTabelaDestino; }
      set { this.objTabelaDestino = value; }
    }

    /// <summary>
    /// Status de Origem
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo StatusOrigem
    {
      get { return objStatusOrigem; }
      set { this.objStatusOrigem = value; }
    }

    /// <summary>
    /// Status de Destino
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo StatusDestino
    {
      get { return objStatusDestino; }
      set { this.objStatusDestino = value; }
    }

    #endregion

    #region Construtor
    public ClsWorkFlowRepercusao()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsWorkFlowRepercusao(int intCodigo)
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
      objAtributos.DescricaoTabela = "WorkFlowRepercusao";
      objAtributos.NomeTabela = "WorkFlowRepercusao";

      objCodigo.Campo = "workflow_repercusao_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objWorkFlow.Campo = "workflow_codigo";
      objWorkFlow.Descricao = "Código do WorkFlow";
      objWorkFlow.CampoObrigatorio = true;
      objWorkFlow.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objWorkFlow);

      objTabelaOrigem.Campo = "tabela_origem";
      objTabelaOrigem.Descricao = "Tabela de Origem";
      objTabelaOrigem.Tipo = System.Data.DbType.String;
      objAtributos.Add(objTabelaOrigem);

      objTabelaDestino.Campo = "tabela_destino";
      objTabelaDestino.Descricao = "Tabela de Origem";
      objTabelaDestino.Tipo = System.Data.DbType.String;
      objAtributos.Add(objTabelaDestino);

      objStatusOrigem.Campo = "status_codigo_origem";
      objStatusOrigem.Descricao = "Código do Status";
      objStatusOrigem.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatusOrigem);

      objStatusDestino.Campo = "status_codigo_destino";
      objStatusDestino.Descricao = "Código do Status";
      objStatusDestino.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatusDestino);

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
            ClsWorkFlowRepercusao objWorkFlowRepercusao = new ClsWorkFlowRepercusao();
            ServiceDesk.Controle.ClsDataGrid.geraDataGrid(objDataGrid, objWorkFlowRepercusao.objAtributos);
            objWorkFlowRepercusao = null;
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
            ClsWorkFlowRepercusao objWorkFlowRepercusao = new ClsWorkFlowRepercusao();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objWorkFlowRepercusao.objAtributos);
            objWorkFlowRepercusao = null;
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
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsWorkFlowRepercusao objWorkFlowRepercusao, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objWorkFlowRepercusao.objAtributos, bolCondicao);
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

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.insereColecao(this.objAtributos))
            {
                strMensagem = "Item inserido com sucesso.";
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

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.alteraColecao(this.objAtributos))
            {
                strMensagem = "Item atualizado com sucesso.";
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

  }
}