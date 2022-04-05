using System;
using System.Collections;

/// <summary>
/// Classe ClsEquipeNivel
/// </summary>
namespace ServiceDesk.Negocio
{
  public class ClsEquipeNivel
  {
    //Colecao de atributos de EquipeNivel
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de EquipeNivel
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCodigoGerente = new ServiceDesk.Banco.ClsAtributo();

    #region Propriedades

    /// <summary>
    /// C�digo do N�vel da Equipe.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Codigo
    {
      get { return objCodigo; }
      set { this.objCodigo = value; }
    }

    /// <summary>
    /// Descri��o do N�vel de Equipe.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get { return objDescricao; }
      set { this.objDescricao = value; }
    }

    /// <summary>
    /// C�digo do Lider do N�vel
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo PessoaCodigoGerente
    {
      get { return objCodigoGerente; }
      set { this.objCodigoGerente = value; }
    }
    #endregion

    #region Contrutor da Classe
    /// <summary>
    /// Construtor da classe EquipeNivel
    /// </summary>
    public ClsEquipeNivel()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsEquipeNivel(int intCodigo)
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

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de EquipeNivel a cole��o de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "EquipeNivel";
      objAtributos.NomeTabela = "EquipeNivel";

      objCodigo.Campo = "equipe_nivel_codigo";
      objCodigo.Descricao = "C�digo do N�vel de Equipe";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "descricao";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 50;
      objAtributos.Add(objDescricao);

      objCodigoGerente.Campo = "pessoa_codigo_gerente";
      objCodigoGerente.Descricao = "C�digo do Gerente do Nivel";
      objCodigoGerente.CampoIdentificador = false;
      objCodigoGerente.CampoObrigatorio = true;
      objCodigoGerente.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigoGerente);
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// M�todo que insere
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
          strMensagem = "Favor informar a descri��o do N�vel da Equipe.<br />";

        if (this.objCodigoGerente.Valor.Trim() == String.Empty)
          strMensagem += "Favor selecionar o lider do N�vel.";

        if (strMensagem == String.Empty)
        {
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          if (objBanco.insereColecao(this.objAtributos))
          {
            strMensagem = "N�vel da Equipe inserido com sucesso.";
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
    /// M�todo que altera
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
    public bool altera(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objDescricao.Valor.Trim() == String.Empty)
          strMensagem = "Favor informar a descri��o do N�vel da Equipe.<br />";

        if (this.objCodigoGerente.Valor.Trim() == String.Empty)
          strMensagem += "Favor selecionar o lider do N�vel.";

        if (strMensagem == String.Empty)
        {
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          if (objBanco.alteraColecao(this.objAtributos))
          {
            strMensagem = "N�vel da Equipe atualizado com sucesso.";
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
    /// M�todo que exclui
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

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
      try
      {
        String strSql = String.Empty;
        objGridView.AutoGenerateColumns = false;
        ClsEquipeNivel objEquipeNivel = new ClsEquipeNivel();
        //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objEquipeNivel.objAtributos);
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strSql = objBanco.montaQuery(objEquipeNivel.objAtributos, false);
        strSql += " ORDER BY descricao";
        System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
        objGridView.DataSource = objDataSet;
        objGridView.DataBind();
        objDataSet.Dispose();
        objDataSet = null;
        objBanco = null;
        objEquipeNivel = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsEquipeNivel objEquipeNivel, bool bolCondicao)
    {
      try
      {
        objGridView.AutoGenerateColumns = false;
        ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objEquipeNivel.objAtributos, bolCondicao);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
        try
        {
            ClsEquipeNivel objEquipeNivel = new ClsEquipeNivel();
            
            objDropDownList.DataTextField = objEquipeNivel.objDescricao.Campo;
            objDropDownList.DataValueField = objEquipeNivel.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objEquipeNivel.objAtributos);
            objDropDownList.Items.Insert(0, "selecione");
            objDropDownList.Items[0].Value = String.Empty;
            
            objEquipeNivel = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string nivelCodigo)
    {
      try
      {
          String strSql = String.Empty;
          ClsEquipeNivel objEquipeNivel = new ClsEquipeNivel();
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

          if (nivelCodigo.Trim() == string.Empty)
          { nivelCodigo = "0"; }
          if (ClsParametro.EscalacaoLivre.Trim() == "N")
          {
            //=======================================================================================//
            // -  O que: Quando a escala��o horizontal n�o for livre e quando for bloqueiada
            // a sele��o de um n�vel inferior ao atual o sistema dever� exibir
            // somente o n�vel atual e o pr�ximo para evitar selecionar um n�vel inferior ao atual.
            // - Quem: Fernanda Passos
            // - Quando: 27/03/2006
            //=======================================================================================//
            if (nivelCodigo != "0" && ClsParametro.EscalacaoLivreBloqueiaSelecaoNivelInferior == "S")
            { 
              strSql = "(SELECT * FROM ";
              strSql += "(SELECT TOP 1 * FROM equipenivel";
              strSql += " WHERE equipe_nivel_codigo = " + nivelCodigo + ""; // Pega o n�vel atual.
              strSql += " ORDER BY equipe_nivel_codigo desc) as A)";
              strSql += " UNION ";
              strSql += " SELECT * FROM (SELECT TOP 1 * FROM equipenivel ";
              strSql += " where equipe_nivel_codigo > " + nivelCodigo + " "; // Pega o primeiro n�vel depois do atual.
              strSql += " ORDER BY equipe_nivel_codigo ) as B";
              strSql += " ORDER BY equipe_nivel_codigo ";

            }
            //=======================================================================================//
            else
            {
              //=======================================================================================//
              // -  O que: Permite a selecao de do primeiro n�vel antes do atual, do n�vel atual e do
              //  primeiro pr�ximo depois do nivel atual.
              // - Quem: Flavio da Purifica��o.
              //=======================================================================================//              strSql = "(SELECT * FROM ";
              strSql += " (SELECT TOP 1 * FROM equipenivel  ";
              strSql += " WHERE equipe_nivel_codigo < " + nivelCodigo + "  ";//Pega o primeiro n�vel antes do atual.
              strSql += " ORDER BY equipe_nivel_codigo desc) as A) ";
              strSql += " UNION ";
              strSql += " (SELECT * FROM equipenivel  ";
              strSql += " where equipe_nivel_codigo = " + nivelCodigo + ") ";// Pega o n�vel atual.
              strSql += " UNION  ";
              strSql += " (SELECT * FROM ( ";
              strSql += " SELECT TOP 1 * FROM equipenivel  ";
              strSql += " where equipe_nivel_codigo > " + nivelCodigo + "  "; // Pega o primeiro n�vel depois do atual.
              strSql += " ORDER BY equipe_nivel_codigo ) as B) ";
              strSql += " ORDER BY equipe_nivel_codigo ";
              //=======================================================================================//
            }
          }

          else //exibe todos
          {
            strSql = objBanco.montaQuery(objEquipeNivel.objAtributos, false);
            strSql += " ORDER BY descricao";
          }

          objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
          objDropDownList.DataTextField = objEquipeNivel.objDescricao.Campo;
          objDropDownList.DataValueField = objEquipeNivel.objCodigo.Campo;
          objDropDownList.DataBind();
          objDropDownList.Items.Insert(0, "0");
          objDropDownList.Items[0].Text = "";
          if ((ClsParametro.EscalacaoLivre.Trim() == "N") && (nivelCodigo != "0"))
          {
            objDropDownList.SelectedValue = nivelCodigo.Trim();
          }
          else
          { objDropDownList.SelectedIndex = 0; }
          objEquipeNivel = null;
          objBanco = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo retornaLista
    /// <summary>
    /// Retorna um Array de objetos Prioridade
    /// </summary>
    /// <returns></returns>
    public static ArrayList retornaLista()
    {
      String strSql = String.Empty;
      ArrayList arlNivel = new ArrayList();

      strSql = "SELECT equipe_nivel_codigo FROM EquipeNivel ";
      strSql += " ORDER BY descricao";

      System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
      while (objReader.Read())
      {
        ClsEquipeNivel objNivel = new ClsEquipeNivel(Convert.ToInt32(objReader["equipe_nivel_codigo"].ToString()));
        arlNivel.Add(objNivel);
        objNivel = null;
      }
      objReader.Dispose();
      objReader = null;

      return arlNivel;

    }
    #endregion

    #region Atualiza processo para n�vel inferior ao atual
    /// <summary>
    /// Muda o c�digo do n�vel do processo (INC, RM ou RS) para um n�vel inferior ao n�vel atual
    /// </summary>
    /// <param name="intIdentificadorTabela">C�digo identificador da tabela que representa o processo</param>
    /// <param name="strTabela">Nome da tabela que representa o processo</param>
    /// <returns>Retorna true ou false. Se foi atualizado ou n�o</returns>
    public static bool AtualizaProcessoNivelInferiorAoAtual(int intIdentificadorTabela, string strTabela)
    {
        try
        {   
            string strSql = string.Empty;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            bool bolRetorno;

            string strNivelAtual = objBanco.retornaValorCampo(strTabela.Trim().ToUpper(), "nivel_atendimento_codigo", strTabela.Trim() +"_codigo = "+ intIdentificadorTabela +"");
            string strNivelInferior = GetCodigoNivelInferiorAtual(Convert.ToInt32(strNivelAtual.Trim())).Trim();

            if (strNivelAtual != string.Empty && strNivelInferior != string.Empty)
            {
                strSql = "update  " + strTabela.Trim().ToUpper() + " set nivel_atendimento_codigo = " + Convert.ToInt32(strNivelInferior) + " where " + strTabela.Trim() + "_codigo = " + intIdentificadorTabela + "";
                if (objBanco.executaSQL(strSql.Trim()) == true) bolRetorno = true; else bolRetorno = false;
            }
            else bolRetorno = false;

            objBanco = null;
            return bolRetorno;

        }
        catch (Exception ex)
        { 
            throw ex; 
        }
    }
    #endregion

    #region Pega n�mero do n�vel inferior ao atual
    /// <summary>
    /// Pega c�digo do n�vel inferior ao atual
    /// </summary>
    /// <param name="intCodigoNivelAtual"></param>
    /// <returns>Retorno um string com c�digo o n�vel</returns>
      public static string GetCodigoNivelInferiorAtual(int intCodigoNivelAtual)
      {
          try
          {
              //=====================================================================//
              // - O que: Pega o c�digo do n�vel inferior ao n�vel atual e retorna. 
              // Caso n�o encontre o sistema retorna string.empty
              // - Quem: Fernanda Passos
              // - Quando: 22/03/2006 �s 16:35hs
              //=====================================================================//
              if (intCodigoNivelAtual > 0)
              {
                  ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                  string strCodigoNivelAnterior = objBanco.retornaValorCampo("equipenivel", "equipe_nivel_codigo", "equipe_nivel_codigo = " + (intCodigoNivelAtual -1) + "");
                  if (strCodigoNivelAnterior != string.Empty) return strCodigoNivelAnterior.Trim(); else return string.Empty;
              }
              else return string.Empty;
              //=====================================================================//
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
    #endregion

    #region Pega n�vel atual do processo
    /// <summary>
    /// Pega c�digo do n�vel atual do processo.
    /// </summary>
    /// <param name="strTabela">Nome f�sico da tabela no banco de dados</param>
    /// <param name="intIdentificadorTabela">C�digo identificador</param>
    /// <returns>Retorna string do n�vel do processo</returns>
    public static int GetCodigoNivelAtualProcesso(string strTabela, int intIdentificadorTabela)
    {
        try
        {
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            string strValor = objBanco.retornaValorCampo(strTabela.Trim().ToUpper(), "nivel_atendimento_codigo", strTabela +"_codigo ="+ intIdentificadorTabela +"");
            if(strValor != string.Empty) return Convert.ToInt32(strValor.Trim()); else return 0;  
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
  }
}