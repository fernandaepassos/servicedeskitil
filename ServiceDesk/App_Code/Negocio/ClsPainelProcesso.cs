using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsPainelProcesso
/// </summary>

namespace ServiceDesk.Negocio
{
  public class ClsPainelProcesso
  {

    #region metodos

    #region Construtor da classe
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    public ClsPainelProcesso()
    {
    }
    #endregion

    #region enum TipoAgrupamentoTreeView
    /// <summary>
    /// 
    /// </summary>
    public enum TipoAgrupamentoTreeView : int
    {
      STATUS = 0,
      PRIORIDADE = 1,
    }
    #endregion

    #region enum TipoTabelaTreeView
    /// <summary>
    /// 
    /// </summary>
    public enum TipoTabelaTreeView : int
    {
      CHAMADO = 0,
      INCIDENTE = 1,
      PROBLEMA = 2,
      MUDANCA = 3,
      REQUISICAOSERVICO = 4
    }
    #endregion

    #region metodo contadorAgrupamentoTreeView
    /// <summary>
    /// Monta a TreeView Agrupamento (Chamado, Incidente e Problema) por Prioridade e Status
    /// </summary>
    /// <param name="TipoTabela">Chamado, Incidente e Problema</param>
    /// <param name="TipoAgrupamento">Prioridade e Status</param>
    /// <param name="trv">TreeView</param>
    /// <param name="intCodigo">Codigo do Item (Chamado, Incidente ou Problema)</param>
    /// <param name="intCodigoProprietario">Codigo do Proprietario</param>
    /// <param name="strDescricao">Descricao do Item</param>
    public void contadorAgrupamentoTreeView(TipoTabelaTreeView TipoTabela, TipoAgrupamentoTreeView TipoAgrupamento, System.Web.UI.WebControls.TreeView trv, int intCodigo, String strDescricao, int intCodigoProprietario)
    {

      ServiceDesk.FrameWork.ClsTreeView objTreeView = new ServiceDesk.FrameWork.ClsTreeView();
      trv.Nodes.Clear();
      TreeNode objTreeNodePai;
      TreeNode objTreeNodePaiChamado;
      TreeNode objTreeNodeFilho;
      int intTotalContador = 0;

      System.Collections.ArrayList objArrayListPrioridade = SServiceDesk.Negocio.ClsPrioridade.retornaLista();
      System.Collections.ArrayList objArrayListStatus = ServiceDesk.Negocio.ClsStatusTabela.retornaLista(TipoTabela.ToString());
      System.Collections.ArrayList objArrayListNivel = ServiceDesk.Negocio.ClsEquipeNivel.retornaLista();
      System.Collections.ArrayList objArrayListPessoa = SServiceDesk.Negocio.ClsPessoa.retornaListaPorItem(TipoTabela.ToString());
      System.Collections.ArrayList objArrayListEquipe;

      int intI = 0;
      int intI2 = 0;

      objTreeNodePai = objTreeView.insereNoArvore("Agrupar", "0", trv.Nodes);
      objTreeNodePaiChamado = objTreeView.insereNoArvore(TipoTabela.ToString() + "S", "1", objTreeNodePai.ChildNodes);
      objTreeNodePai = objTreeView.insereNoArvore("Por Prioridade", "prioridade_codigo = prioridade_codigo", objTreeNodePaiChamado.ChildNodes);
      for (intI = 0; intI < objArrayListPrioridade.Count; intI++)
      {
        SServiceDesk.Negocio.ClsPrioridade objPrioridade = (SServiceDesk.Negocio.ClsPrioridade)objArrayListPrioridade[intI];
        intTotalContador = retornaTotalPorTabelaPrioridade(TipoTabela.ToString(), Convert.ToInt32(objPrioridade.Codigo.Valor), intCodigo, strDescricao, intCodigoProprietario);
        if (intTotalContador > 0)
            objTreeView.insereNoArvore(objPrioridade.Descricao.Valor + " (" + intTotalContador.ToString() + ")", "prioridade_codigo = 0" + objPrioridade.Codigo.Valor, objTreeNodePai.ChildNodes);
        objPrioridade = null;
      }

      objTreeNodePai = objTreeView.insereNoArvore("Por Status", "status_codigo = status_codigo", objTreeNodePaiChamado.ChildNodes);
      for (intI = 0; intI < objArrayListStatus.Count; intI++)
      {
        ServiceDesk.Negocio.ClsStatus objStatus = (ServiceDesk.Negocio.ClsStatus)objArrayListStatus[intI];
        intTotalContador = retornaTotalPorTabelaStatus(TipoTabela.ToString(), Convert.ToInt32(objStatus.Codigo.Valor), intCodigo, strDescricao, intCodigoProprietario);
        if (intTotalContador > 0)
            objTreeView.insereNoArvore(objStatus.Descricao.Valor + " (" + intTotalContador.ToString() + ")", "status_codigo = 0" + objStatus.Codigo.Valor, objTreeNodePai.ChildNodes);
        objStatus = null;
      }

      intTotalContador = retornaTotalPorTabelaVIP(TipoTabela.ToString(), "S");
      if (intTotalContador > 0)
      {
          objTreeView.insereNoArvore("Vips (" + intTotalContador.ToString() + ")", "vip = 'S'", objTreeNodePaiChamado.ChildNodes);
      }

      objTreeNodePai = objTreeView.insereNoArvore("Escalação Horizontal", "nivel_atendimento_codigo = nivel_atendimento_codigo", objTreeNodePaiChamado.ChildNodes);
      for (intI = 0; intI < objArrayListNivel.Count; intI++)
      {
          ServiceDesk.Negocio.ClsEquipeNivel objNivelHorizontal = (ServiceDesk.Negocio.ClsEquipeNivel)objArrayListNivel[intI];
          intTotalContador = retornaTotalPorTabelaNivel(TipoTabela.ToString(), Convert.ToInt32(objNivelHorizontal.Codigo.Valor), intCodigo, strDescricao, intCodigoProprietario);
          if (intTotalContador > 0)
          {
              objTreeNodeFilho = objTreeView.insereNoArvore(objNivelHorizontal.Descricao.Valor + " (" + intTotalContador.ToString() + ")", "nivel_atendimento_codigo = 0" + objNivelHorizontal.Codigo.Valor, objTreeNodePai.ChildNodes);
              objArrayListEquipe = ServiceDesk.Negocio.ClsEquipe.retornaLista(objNivelHorizontal.Codigo.Valor);
              for (intI2 = 0; intI2 < objArrayListEquipe.Count; intI2++)
              {
                  ServiceDesk.Negocio.ClsEquipe objEquipe = (ServiceDesk.Negocio.ClsEquipe)objArrayListEquipe[intI2];
                  intTotalContador = retornaTotalPorTabelaEquipe(TipoTabela.ToString(), Convert.ToInt32(objEquipe.Codigo.Valor), intCodigo, strDescricao, intCodigoProprietario);
                  if (intTotalContador > 0)
                      objTreeView.insereNoArvore(objEquipe.Descricao.Valor + " (" + intTotalContador.ToString() + ")", "equipe_codigo_alocacao = 0" + objEquipe.Codigo.Valor, objTreeNodeFilho.ChildNodes);
                  objEquipe = null;
              }
          }
          objNivelHorizontal = null;
      }

      intTotalContador = retornaTotalPorTabelaEscalado(TipoTabela.ToString(), intCodigo, strDescricao, intCodigoProprietario);
      if (intTotalContador > 0)
      {
        objTreeView.insereNoArvore("Escalação Vertical (" + intTotalContador.ToString() + ")", "escalado = 'S'", objTreeNodePaiChamado.ChildNodes);
      }

      objTreeNodePai = objTreeView.insereNoArvore("Por Proprietário", "pessoa_codigo_proprietario = pessoa_codigo_proprietario", objTreeNodePaiChamado.ChildNodes);
      intTotalContador = retornaTotalPorTabelaPessoa(TipoTabela.ToString());
      if (intTotalContador > 0)
      {
        objTreeView.insereNoArvore("Sem Proprietário (" + intTotalContador.ToString() + ")", "pessoa_codigo_proprietario is null", objTreeNodePai.ChildNodes);
      }
      for (intI = 0; intI < objArrayListPessoa.Count; intI++)
      {
        SServiceDesk.Negocio.ClsPessoa objPessoa = (SServiceDesk.Negocio.ClsPessoa)objArrayListPessoa[intI];
        intTotalContador = retornaTotalPorTabelaPessoa(TipoTabela.ToString(), Convert.ToInt32(objPessoa.Codigo.Valor));
        objTreeView.insereNoArvore(objPessoa.Nome.Valor + " (" + intTotalContador.ToString() + ")", "pessoa_codigo_proprietario = 0" + objPessoa.Codigo.Valor, objTreeNodePai.ChildNodes);
        objPessoa = null;
      }

      trv.ExpandAll();

      objArrayListPrioridade = null;
      objTreeNodePaiChamado = null;
      objTreeNodePai = null;
      objTreeView = null;
      objArrayListPessoa = null;

    }
    #endregion

    #region metodo retornaTotalPorTabelaStatus
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela com um determinado Status 
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoStatus"></param>
    /// <param name="intCodigo"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <param name="strDescricao"></param>
    /// <returns></returns>
    public int retornaTotalPorTabelaStatus(String strTabela, int intCodigoStatus, int intCodigo, String strDescricao, int intCodigoProprietario)
    {
      int intRetorno = 0;
      String strResultado = String.Empty;
      String strCondicao = String.Empty;

      strCondicao = " status_codigo = " + intCodigoStatus.ToString();
      if (intCodigo > 0)
      {
        strCondicao += " AND " + strTabela + "_codigo = " + intCodigo.ToString();
      }
      if (strDescricao.Trim() != String.Empty)
      {
        strCondicao += " AND descricao LIKE '%" + strDescricao.Trim() + "%'";
      }
      if (intCodigoProprietario > 0)
      {
        strCondicao += " AND pessoa_codigo_proprietario = " + intCodigoProprietario.ToString();
      }

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
      if (strResultado != String.Empty)
      {
        intRetorno = Convert.ToInt32(strResultado);
      }
      objBanco = null;

      return intRetorno;

    }
    #endregion

    #region metodo retornaTotalPorTabelaPrioridade
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela com uma determinada Prioridade 
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoPrioridade"></param>
    /// <param name="intCodigo"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <param name="strDescricao"></param>
    /// <returns></returns>
    public int retornaTotalPorTabelaPrioridade(String strTabela, int intCodigoPrioridade, int intCodigo, String strDescricao, int intCodigoProprietario)
    {
      int intRetorno = 0;
      String strResultado = String.Empty;
      String strCondicao = String.Empty;

      strCondicao = " prioridade_codigo = " + intCodigoPrioridade.ToString();
      if (intCodigo > 0)
      {
        strCondicao += " AND " + strTabela + "_codigo = " + intCodigo.ToString();
      }
      if (strDescricao.Trim() != String.Empty)
      {
        strCondicao += " AND descricao LIKE '%" + strDescricao.Trim() + "%'";
      }
      if (intCodigoProprietario > 0)
      {
        strCondicao += " AND pessoa_codigo_proprietario = " + intCodigoProprietario.ToString();
      }

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
      if (strResultado != String.Empty)
      {
        intRetorno = Convert.ToInt32(strResultado);
      }
      objBanco = null;

      return intRetorno;

    }
    #endregion

    #region metodo retornaTotalPorTabelaNivel
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela com um determinado Status 
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoStatus"></param>
    /// <param name="intCodigo"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <param name="strDescricao"></param>
    /// <returns></returns>
      public int retornaTotalPorTabelaNivel(String strTabela, int intCodigoNivel, int intCodigo, String strDescricao, int intCodigoProprietario)
    {
        int intRetorno = 0;
        String strResultado = String.Empty;
        String strCondicao = String.Empty;

        strCondicao = " nivel_atendimento_codigo = " + intCodigoNivel.ToString();
        if (intCodigo > 0)
        {
            strCondicao += " AND " + strTabela + "_codigo = " + intCodigo.ToString();
        }
        if (strDescricao.Trim() != String.Empty)
        {
            strCondicao += " AND descricao LIKE '%" + strDescricao.Trim() + "%'";
        }
        if (intCodigoProprietario > 0)
        {
            strCondicao += " AND pessoa_codigo_proprietario = " + intCodigoProprietario.ToString();
        }

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
        if (strResultado != String.Empty)
        {
            intRetorno = Convert.ToInt32(strResultado);
        }
        objBanco = null;

        return intRetorno;

    }
    #endregion

    #region metodo retornaTotalPorTabelaEquipe
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela com um determinada Equipe 
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoEquipe"></param>
    /// <param name="intCodigo"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <param name="strDescricao"></param>
    /// <returns></returns>
      public int retornaTotalPorTabelaEquipe(String strTabela, int intCodigoEquipe, int intCodigo, String strDescricao, int intCodigoProprietario)
    {
        int intRetorno = 0;
        String strResultado = String.Empty;
        String strCondicao = String.Empty;

        strCondicao = " equipe_codigo_alocacao = " + intCodigoEquipe.ToString();
        if (intCodigo > 0)
        {
            strCondicao += " AND " + strTabela + "_codigo = " + intCodigo.ToString();
        }
        if (strDescricao.Trim() != String.Empty)
        {
            strCondicao += " AND descricao LIKE '%" + strDescricao.Trim() + "%'";
        }
        if (intCodigoProprietario > 0)
        {
            strCondicao += " AND pessoa_codigo_proprietario = " + intCodigoProprietario.ToString();
        }

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
        if (strResultado != String.Empty)
        {
            intRetorno = Convert.ToInt32(strResultado);
        }
        objBanco = null;

        return intRetorno;

    }
    #endregion

    #region metodo retornaTotalPorTabelaEscalado
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela com um determinada Equipe 
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigo"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <param name="strDescricao"></param>
    /// <returns></returns>
      public int retornaTotalPorTabelaEscalado(String strTabela, int intCodigo, String strDescricao, int intCodigoProprietario)
    {
        int intRetorno = 0;
        String strResultado = String.Empty;
        String strCondicao = String.Empty;

        strCondicao = " escalado = 'S'";
        if (intCodigo > 0)
        {
            strCondicao += " AND " + strTabela + "_codigo = " + intCodigo.ToString();
        }
        if (strDescricao.Trim() != String.Empty)
        {
            strCondicao += " AND descricao LIKE '%" + strDescricao.Trim() + "%'";
        }
        if (intCodigoProprietario > 0)
        {
            strCondicao += " AND pessoa_codigo_proprietario = " + intCodigoProprietario.ToString();
        }

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
        if (strResultado != String.Empty)
        {
            intRetorno = Convert.ToInt32(strResultado);
        }
        objBanco = null;

        return intRetorno;

    }
    #endregion

    #region metodo retornaTotalPorTabelaPessoa
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela de uma determinada Pessoa
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <returns></returns>
    public int retornaTotalPorTabelaPessoa(String strTabela, int intCodigoProprietario)
    {
      int intRetorno = 0;
      String strResultado = String.Empty;
      String strCondicao = String.Empty;

      if (intCodigoProprietario > 0)
      {
        strCondicao += " pessoa_codigo_proprietario = " + intCodigoProprietario.ToString();

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
        if (strResultado != String.Empty)
        {
          intRetorno = Convert.ToInt32(strResultado);
        }
        objBanco = null;
      }

      return intRetorno;

    }
    #endregion

    #region metodo retornaTotalPorTabelaPessoa
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela sem proprietario
    /// </summary>
    /// <param name="strTabela"></param>
    /// <returns></returns>
    public int retornaTotalPorTabelaPessoa(String strTabela)
    {
      int intRetorno = 0;
      String strResultado = String.Empty;
      String strCondicao = String.Empty;

      strCondicao += " pessoa_codigo_proprietario is null";
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
      if (strResultado != String.Empty)
      {
        intRetorno = Convert.ToInt32(strResultado);
      }
      objBanco = null;

      return intRetorno;

    }
    #endregion

    #region metodo retornaTotalPorTabelaVIP
    /// <summary>
    /// Retorna o total de ocorrencias em uma tabela Vip
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <returns></returns>
    public int retornaTotalPorTabelaVIP(String strTabela, String strVip)
    {
      int intRetorno = 0;
      String strResultado = String.Empty;
      String strCondicao = String.Empty;

      if (strVip != String.Empty)
      {
        strCondicao += " vip = '" + strVip + "'";

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strResultado = objBanco.retornaValorCampo(strTabela, "COUNT(" + strTabela + "_codigo)", strCondicao);
        if (strResultado != String.Empty)
        {
          intRetorno = Convert.ToInt32(strResultado);
        }
        objBanco = null;
      }

      return intRetorno;

    }
    #endregion

    #region metodo geraDataSet
    /// <summary>
    /// Gera o DataSet do Item(Chamado, Incidente ou Problema) de acordo com os parametros de pesquisa
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigo"></param>
    /// <param name="strBusca"></param>
    /// <param name="intCodigoItem"></param>
    /// <param name="strDescricao"></param>
    /// <param name="intCodigoProprietario"></param>
    /// <returns></returns>
      public static System.Data.DataSet geraDataSet(String strTabela, string strCondicao, int intCodigoItem, String strDescricao, int intCodigoProprietario)
    {
      String strSql = String.Empty;

      strSql = "SELECT * ";
      strSql += " FROM " + strTabela;
      strSql += " WHERE " + strCondicao;
      if (intCodigoItem > 0)
      {
        strSql += " AND " + strTabela + "_codigo = " + intCodigoItem.ToString();
      }
      if (strDescricao.Trim() != String.Empty)
      {
        strSql += " AND descricao LIKE '%" + strDescricao.Trim() + "%'";
      }
      if (intCodigoProprietario > 0)
      {
        strSql += " AND pessoa_codigo_proprietario = " + intCodigoProprietario.ToString();
      }

      strSql += " ORDER BY prioridade_codigo asc, data_inclusao asc";

      System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);

      return objDataSet;

    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Monta a Grid(Chamado, Incidente ou Problema) de acordo com os parametros de pesquisa
    /// </summary>
    /// <param name="objGridView">Grid View</param>
    /// <param name="strTabela">Tabela a ser realizada a pesquisa</param>
    /// <param name="intCodigo">Codigo do Status ou Prioridade</param>
    /// <param name="strBusca">Status/Prioridade</param>
    /// <param name="intCodigoItem">Codigo do Item a ser pesquisado</param>
    /// <param name="strDescricao">Descrição a ser pesquisada</param>
    /// <param name="intCodigoProprietario">Código do Proprietario</param>
    public static void geraGridView(GridView objGridView, String strTabela, string strCondicao, int intCodigoItem, String strDescricao, int intCodigoProprietario)
    {

        System.Data.DataSet objDataSet = geraDataSet(strTabela, strCondicao, intCodigoItem, strDescricao, intCodigoProprietario);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;

    }
    #endregion

    #endregion

  }
}