using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe Aplicacao
  /// </summary>
  public class ClsAplicacao
  {

    //Colecao de atributos de uma aplicação.
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    #region Atributos
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAplicacaoSuperior = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPrecoReal = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPrecoDolar = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagIstalacaoPadrao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipoLicensa = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objQuantidadeLicenca = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objChave = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricaoUtilizacao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagPermissaoAcesso = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagAgendar = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagVisibilidade = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagAprovacao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objSigla = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objLocalizacaoInterna = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objLocalizacaoExterna = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objVersao = new ServiceDesk.Banco.ClsAtributo();
    #endregion

    #region Contrutor
    /// <summary>
    /// Contrutor da Classe
    /// </summary>
    public ClsAplicacao()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region ClsAplicacao(int intCodigoAplicacao)
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    /// <param name="intCodigoAplicacao"></param>
    public ClsAplicacao(int intCodigoAplicacao)
    {
      if (intCodigoAplicacao > 0)
      {
        String strSql = "SELECT *";
        strSql += " FROM aplicacao";
        strSql += " WHERE aplicacao_codigo = " + intCodigoAplicacao.ToString();
        System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        if (objDataReader.Read())
        {
          this.alimentaColecaoCampos();
          this.Codigo.Valor = intCodigoAplicacao.ToString();
          this.Descricao.Valor = objDataReader["descricao"].ToString();
          this.AplicacaoSuperior.Valor = objDataReader["aplicacao_superior_codigo"].ToString();
          this.PrecoReal.Valor = objDataReader["preco_br"].ToString();
          this.PrecoDolar.Valor = objDataReader["preco_us"].ToString();
          this.FlagIstalacaoPadrao.Valor = objDataReader["flag_instalacao_padrao"].ToString();
          this.TipoLicensa.Valor = objDataReader["tipo_licenca"].ToString();
          this.QuantidadeLicenca.Valor = objDataReader["quantidade_licenca"].ToString();
          this.Status.Valor = objDataReader["status_codigo"].ToString();
          this.Chave.Valor = objDataReader["chave_tratamento"].ToString();
          this.DescricaoUtilizacao.Valor = objDataReader["descricao_utilizacao"].ToString();
          this.FlagPermissaoAcesso.Valor = objDataReader["flag_permissao_acesso"].ToString();
          this.FlagAgendar.Valor = objDataReader["flag_agendar"].ToString();
          this.FlagVisibilidade.Valor = objDataReader["flag_visibilidade"].ToString();
          this.FlagAprovacao.Valor = objDataReader["flag_aprovacao"].ToString();
          this.Sigla.Valor = objDataReader["sigla"].ToString();
          this.LocalizacaoInterna.Valor = objDataReader["localizacao_interna"].ToString();
          this.LocalizacaoExterna.Valor = objDataReader["localizacao_externa"].ToString();
          this.Versao.Valor = objDataReader["versao"].ToString();
        }
        objDataReader.Dispose();
        objDataReader = null;
      }
    }
    #endregion

    #region alimentaColecaoCampos
    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.NomeTabela = "Aplicacao";
      objAtributos.DescricaoTabela = "Aplicacao";

      objCodigo.Campo = "aplicacao_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 255;
      objAtributos.Add(objDescricao);

      objAplicacaoSuperior.Campo = "aplicacao_superior_codigo";
      objAplicacaoSuperior.Descricao = "Aplicação Superior";
      objAplicacaoSuperior.CampoObrigatorio = true;
      objAplicacaoSuperior.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objAplicacaoSuperior);

      objPrecoReal.Campo = "preco_br";
      objPrecoReal.Descricao = "Preço R$";
      objPrecoReal.CampoObrigatorio = true;
      objPrecoReal.Tipo = System.Data.DbType.Decimal;
      objAtributos.Add(objPrecoReal);

      objPrecoDolar.Campo = "preco_us";
      objPrecoDolar.Descricao = "Preco U$";
      objPrecoDolar.CampoObrigatorio = true;
      objPrecoDolar.Tipo = System.Data.DbType.Decimal;
      objAtributos.Add(objPrecoDolar);

      objFlagIstalacaoPadrao.Campo = "flag_instalacao_padrao";
      objFlagIstalacaoPadrao.Descricao = "Instalação padrão?";
      objFlagIstalacaoPadrao.CampoObrigatorio = true;
      objFlagIstalacaoPadrao.Tipo = System.Data.DbType.String;
      objFlagIstalacaoPadrao.Tamanho = 1;
      objAtributos.Add(objFlagIstalacaoPadrao);

      objTipoLicensa.Campo = "tipo_licenca";
      objTipoLicensa.Descricao = "Tipo de Licença";
      objTipoLicensa.CampoObrigatorio = true;
      objTipoLicensa.Tipo = System.Data.DbType.String;
      objTipoLicensa.Tamanho = 3;
      objAtributos.Add(objTipoLicensa);

      objQuantidadeLicenca.Campo = "quantidade_licenca";
      objQuantidadeLicenca.Descricao = "Tipo de Licença";
      objQuantidadeLicenca.CampoObrigatorio = true;
      objQuantidadeLicenca.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objQuantidadeLicenca);

      objChave.Campo = "chave_tratamento";
      objChave.Descricao = "Chave de Tratamento";
      objChave.CampoObrigatorio = true;
      objChave.Tipo = System.Data.DbType.String;
      objChave.Tamanho = 100;
      objAtributos.Add(objChave);

      objStatus.Campo = "status_codigo";
      objStatus.Descricao = "Status";
      objStatus.CampoObrigatorio = true;
      objStatus.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatus);

      objDescricaoUtilizacao.Campo = "descricao_utilizacao";
      objDescricaoUtilizacao.Descricao = "Descrição da Utilização";
      objDescricaoUtilizacao.CampoObrigatorio = true;
      objDescricaoUtilizacao.Tipo = System.Data.DbType.String;
      objDescricaoUtilizacao.Tamanho = 1000;
      objAtributos.Add(objDescricaoUtilizacao);

      objFlagPermissaoAcesso.Campo = "flag_permissao_acesso";
      objFlagPermissaoAcesso.Descricao = "Flag de Permissão de Acesso";
      objFlagPermissaoAcesso.CampoObrigatorio = true;
      objFlagPermissaoAcesso.Tipo = System.Data.DbType.String;
      objFlagPermissaoAcesso.Tamanho = 1;
      objAtributos.Add(objFlagPermissaoAcesso);

      objFlagAgendar.Campo = "flag_agendar";
      objFlagAgendar.Descricao = "Flag de Agendamento";
      objFlagAgendar.CampoObrigatorio = true;
      objFlagAgendar.Tipo = System.Data.DbType.String;
      objFlagAgendar.Tamanho = 1;
      objAtributos.Add(objFlagAgendar);

      objFlagVisibilidade.Campo = "flag_visibilidade";
      objFlagVisibilidade.Descricao = "Flag de Visibilidade";
      objFlagVisibilidade.CampoObrigatorio = true;
      objFlagVisibilidade.Tipo = System.Data.DbType.String;
      objFlagVisibilidade.Tamanho = 1;
      objAtributos.Add(objFlagVisibilidade);

      objFlagAprovacao.Campo = "flag_aprovacao";
      objFlagAprovacao.Descricao = "Flag de Aprovação";
      objFlagAprovacao.CampoObrigatorio = true;
      objFlagAprovacao.Tipo = System.Data.DbType.String;
      objFlagAprovacao.Tamanho = 1;
      objAtributos.Add(objFlagAprovacao);

      objSigla.Campo = "sigla";
      objSigla.Descricao = "Sigla";
      objSigla.CampoObrigatorio = true;
      objSigla.Tipo = System.Data.DbType.String;
      objSigla.Tamanho = 100;
      objAtributos.Add(objSigla);

      objLocalizacaoInterna.Campo = "localizacao_interna";
      objLocalizacaoInterna.Descricao = "Localização Interna";
      objLocalizacaoInterna.CampoObrigatorio = true;
      objLocalizacaoInterna.Tipo = System.Data.DbType.String;
      objLocalizacaoInterna.Tamanho = 1000;
      objAtributos.Add(objLocalizacaoInterna);

      objLocalizacaoExterna.Campo = "localizacao_externa";
      objLocalizacaoExterna.Descricao = "Localização Externa";
      objLocalizacaoExterna.CampoObrigatorio = true;
      objLocalizacaoExterna.Tipo = System.Data.DbType.String;
      objLocalizacaoExterna.Tamanho = 1000;
      objAtributos.Add(objLocalizacaoExterna);

      objVersao.Campo = "versao";
      objVersao.Descricao = "Versão";
      objVersao.CampoObrigatorio = true;
      objVersao.Tipo = System.Data.DbType.String;
      objVersao.Tamanho = 255;
      objAtributos.Add(objVersao);

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
      set
      {
        this.objCodigo = value;
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
        this.objDescricao = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo AplicacaoSuperior
    {
      get
      {
        return objAplicacaoSuperior;
      }
      set
      {
        this.objAplicacaoSuperior = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo PrecoReal
    {
      get
      {
        return objPrecoReal;
      }
      set
      {
        this.objPrecoReal = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo PrecoDolar
    {
      get
      {
        return objPrecoDolar;
      }
      set
      {
        this.objPrecoDolar = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo FlagIstalacaoPadrao
    {
      get
      {
        return objFlagIstalacaoPadrao;
      }
      set
      {
        this.objFlagIstalacaoPadrao = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo TipoLicensa
    {
      get
      {
        return objTipoLicensa;
      }
      set
      {
        this.objTipoLicensa = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo QuantidadeLicenca
    {
      get
      {
        return objQuantidadeLicenca;
      }
      set
      {
        this.objQuantidadeLicenca = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Chave
    {
      get
      {
        return objChave;
      }
      set
      {
        this.objChave = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Status
    {
      get
      {
        return objStatus;
      }
      set
      {
        this.objStatus = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo DescricaoUtilizacao
    {
      get
      {
        return objDescricaoUtilizacao;
      }
      set
      {
        this.objDescricaoUtilizacao = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo FlagPermissaoAcesso
    {
      get
      {
        return objFlagPermissaoAcesso;
      }
      set
      {
        this.objFlagPermissaoAcesso = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo FlagAgendar
    {
      get
      {
        return objFlagAgendar;
      }
      set
      {
        this.objFlagAgendar = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo FlagVisibilidade
    {
      get
      {
        return objFlagVisibilidade;
      }
      set
      {
        this.objFlagVisibilidade = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo FlagAprovacao
    {
      get
      {
        return objFlagAprovacao;
      }
      set
      {
        this.objFlagAprovacao = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Sigla
    {
      get
      {
        return objSigla;
      }
      set
      {
        this.objSigla = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo LocalizacaoInterna
    {
      get
      {
        return objLocalizacaoInterna;
      }
      set
      {
        this.objLocalizacaoInterna = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo LocalizacaoExterna
    {
      get
      {
        return objLocalizacaoExterna;
      }
      set
      {
        this.objLocalizacaoExterna = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Versao
    {
      get
      {
        return objVersao;
      }
      set
      {
        this.objVersao = value;
      }
    }

    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere uma nova aplicação.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
      strMensagem = String.Empty;
      bool bolRetorno = false;

      if (this.objDescricao.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a Descrição da Aplicação.";
      }
      else
      {
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.insereColecao(this.objAtributos))
        {
          strMensagem = "Aplicação inserida com sucesso.";
          bolRetorno = true;
        }
        objBanco = null;
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
      String strSql = String.Empty;
      objGridView.AutoGenerateColumns = false;
      ClsAplicacao objAplicacao = new ClsAplicacao();
      //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objAplicacao.objAtributos);
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strSql = objBanco.montaQuery(objAplicacao.objAtributos, false);
      strSql += " ORDER BY descricao";
      System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;
      objBanco = null;
      objAplicacao = null;
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsAplicacao objAplicacao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objAplicacao.objAtributos);
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
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsAplicacao objAplicacao, bool bolCondicao)
    {
      objGridView.AutoGenerateColumns = false;
      ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objAplicacao.objAtributos, bolCondicao);
    }
    #endregion

    #region metodo geraGridViewQuery
    /// <summary>
    /// Gera o GridView de acordo com a Query passada
    /// </summary>
    /// <param name="objGridView">nome do GridView</param>
    /// <param name="strSQL">Sript SQL</param>
    public static bool geraGridViewQuery(System.Web.UI.WebControls.GridView objGridView, String strSQL)
    {
        try
        {
            System.Data.SqlClient.SqlDataReader dr;

            objGridView.AutoGenerateColumns = false;

            dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
            if (dr.Read())
            {
                objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
                objGridView.DataBind();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraDetailsView
    /// <summary>
    /// Gera uma novo DetailsView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraDetailsView</param>
    public static void geraDetailsView(System.Web.UI.WebControls.DetailsView objDetailsView, int intCodigo)
    {
      objDetailsView.AutoGenerateRows = false;
      ClsAplicacao objAplicacao = new ClsAplicacao();
      objAplicacao.Codigo.Valor = intCodigo.ToString();
      ServiceDesk.Controle.ClsDetailsView.geraDetailsView(objDetailsView, objAplicacao.objAtributos, true);
      objAplicacao = null;
    }
    #endregion

    #region metodo altera
    /// <summary>
    /// Método que altera uma empresa
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {

      strMensagem = String.Empty;
      bool bolRetorno = false;

      if (this.objDescricao.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a Descrição da Aplicação.";
      }
      else
      {
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.alteraColecao(this.objAtributos))
        {
          strMensagem = "Aplicação atualizada com sucesso.";
          bolRetorno = true;
        }
        objBanco = null;
      }

      return bolRetorno;
    }
    #endregion

    #region metodo exclui
    /// <summary>
    /// Método que exclui uma aplicação
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui()
    {
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
    #endregion

    #region geraArvore
    /// <summary>
    /// Método que gera a árvore de aplicações.
    /// </summary>
    public static void geraArvore(System.Web.UI.WebControls.TreeView objArvore)
    {
      criaRaizArvore(objArvore);
    }

    private static void criaRaizArvore(System.Web.UI.WebControls.TreeView objTreeView)
    {
      //Cria arvore e adiciona o nó raiz
      objTreeView.Nodes.Clear();  // Limpando Treeview 

      //Nó raiz
      System.Web.UI.WebControls.TreeNode rootNode = new System.Web.UI.WebControls.TreeNode();
      System.Web.UI.WebControls.TreeNode Node = new System.Web.UI.WebControls.TreeNode();
      rootNode.Value = "-1";
      rootNode.NavigateUrl = "";
      rootNode.Target = "";
      rootNode.Text = "Aplicações";
      rootNode.ImageUrl = "images/icones/info.gif";
      objTreeView.Nodes.Add(rootNode);

      //restante da árvore
      criaArvore(objTreeView);

      objTreeView.ExpandAll();
    }
    #endregion

    #region criaArvore
    /// <summary>
    /// Metodo que cria a arvore
    /// </summary>
    /// <param name="objTreeView"></param>
    private static void criaArvore(System.Web.UI.WebControls.TreeView objTreeView)
    {
      //Cria a árvore
      try
      {
        System.Data.SqlClient.SqlDataReader objReader;
        string strSql = "SELECT aplicacao_codigo,descricao, status from aplicacao where aplicacao_superior_codigo is NULL order by descricao";
        objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        if (objReader.HasRows)
        {
          while (objReader.Read())
          {
            System.Web.UI.WebControls.TreeNode Node = new System.Web.UI.WebControls.TreeNode();
            Node.Value = objReader["aplicacao_codigo"].ToString();
            Node.Text = objReader["descricao"].ToString();
            montaArvore(Node);

            //adiciona nós na arvore
            objTreeView.Nodes[0].ChildNodes.Add(Node);
          }
        }
        objReader.Close();
      }
      catch
      {
      }
      //Fim da Criação da árvore.
    }
    #endregion

    #region montaArvore
    /// <summary>
    /// Método que monta a arvore
    /// </summary>
    /// <param name="Node"></param>
    private static void montaArvore(System.Web.UI.WebControls.TreeNode Node)
    {
      System.Data.SqlClient.SqlDataReader objReaderArvore;
      string strSql;
      strSql = "SELECT aplicacao_codigo, descricao, status, chave_tratamento";
      strSql += " FROM aplicacao";
      strSql += " WHERE aplicacao_superior_codigo ='" + Node.Value.ToString() + "'";
      strSql += " ORDER BY chave_tratamento";

      try
      {
        objReaderArvore = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        System.Web.UI.WebControls.TreeNode newNode;
        if (objReaderArvore.HasRows)
        {
          while (objReaderArvore.Read())
          {
            newNode = new System.Web.UI.WebControls.TreeNode(); // Inicializando Node 
            newNode.Value = objReaderArvore["aplicacao_codigo"].ToString();
            newNode.Text = objReaderArvore["descricao"].ToString();
            newNode.ShowCheckBox = true;
            Node.ChildNodes.Add(newNode);

            montaArvore(newNode);
          }
        }
        objReaderArvore.Close();
      }
      catch
      {
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
            String strSql = String.Empty;
            ClsAplicacao objAplicacao = new ClsAplicacao();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            strSql = objBanco.montaQuery(objAplicacao.objAtributos,false);
            strSql += " ORDER BY descricao";

            objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            objDropDownList.DataTextField = objAplicacao.objDescricao.Campo;
            objDropDownList.DataValueField = objAplicacao.objCodigo.Campo;
            objDropDownList.DataBind();

            objAplicacao = null;
            objBanco = null;
        }
        #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Método que gera a dropdownlist de aplicações pai do formulario.
    /// </summary>
    /// <returns>.</returns>
    /// 
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string campoChave, string campoExibicao)
    {
      ClsAplicacao app = new ClsAplicacao();
      app.alimentaColecaoCampos();
      objDropDownList.DataTextField = campoExibicao;
      objDropDownList.DataValueField = campoChave;
      ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, app.objAtributos);

      //Adiciona a opção default no dropdownlist.
      ListItem itemDefault = new ListItem();
      itemDefault.Text = "- Nenhum item selecionado -";
      itemDefault.Value = "";
      objDropDownList.Items.Insert(0, itemDefault);

    }
    #endregion

    #region metodo geraDropDownListExcecao
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos exceto a aplicacao passada no parametro.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    /// <param name="intCodigoAplicacao">Código da Aplicação</param>
    public static void geraDropDownListExcecao(System.Web.UI.WebControls.DropDownList objDropDownList, int intCodigoAplicacao)
    {
      string strSql = string.Empty;
      strSql = "SELECT aplicacao_codigo, descricao ";
      strSql = strSql + " FROM aplicacao";
      strSql = strSql + " WHERE aplicacao_codigo <> 0" + intCodigoAplicacao.ToString() + " ORDER BY descricao";

      ClsAplicacao objAplicacao = new ClsAplicacao();

      objDropDownList.DataTextField = objAplicacao.objDescricao.Campo;
      objDropDownList.DataValueField = objAplicacao.objCodigo.Campo;
      objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
      objDropDownList.DataBind();

      objAplicacao = null;
    }
    #endregion

    #region geraDropDownListPorEmpresa
    /// <summary>
    /// Método que gera a dropdownlist de aplicações de acordo com a empresa especificada.
    /// </summary>
    /// <param name="objDropDownList">Nome do objeto do tipo DropDownList que será manipulado pelo método.</param>
    /// <param name="strEstrutura">Código da estrutura que será usado para filtrar as aplicações a serem exibidas.</param>
    /// <param name="campoTextoExibicao">Nome do campo cujo valor será exibido no texto do item da combo.</param>
    /// <param name="campoValor">Nome do campo cujo valor será atribuido ao item da combo.</param>
    /// <returns>.</returns>
    /// 
    public static void geraDropDownListPorEmpresa(System.Web.UI.WebControls.DropDownList objDropDownList, String strEstrutura, String campoTextoExibicao, String campoValor)
    {
      //busca as aplicações para a empresa especificada		
      string strSql = string.Empty;
      strSql = "SELECT distinct app.aplicacao_codigo, app.descricao ";
      strSql = strSql + " from aplicacao app, perfil p, perfilestrutura pe ";
      strSql = strSql + " where pe.estrutura_codigo = " + strEstrutura;
      strSql = strSql + " and pe.perfil_codigo = p.perfil_codigo ";
      strSql = strSql + " and p.aplicacao_codigo = app.aplicacao_codigo ";

      objDropDownList.DataTextField = campoTextoExibicao;
      objDropDownList.DataValueField = campoValor;
      System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
      objDropDownList.DataSource = objDataReader;
      objDropDownList.DataBind();
      objDataReader.Dispose();
      objDataReader = null;

      //Adiciona a opção default no dropdownlist.
      ListItem itemDefault = new ListItem();
      itemDefault.Text = "- Nenhum item selecionado -";
      itemDefault.Value = "";
      objDropDownList.Items.Insert(0, itemDefault);
    }
    #endregion

    #region atualizaChave
    /// <summary>
    /// <para>Função que atualiza as chaves dos subitens para um determinado item.</para>
    /// <para>Data: 12/05/2005.</para>
    /// <para>Autor: Sylvio Neto (Adaptação de rotina existente no projetos).</para>
    /// </summary>
    /// <param name="numitm"><para>(int) Número identificador do item.</para></param>
    /// <param name="chave"><para>(string) Chave do item pai.</para></param>

    private static void atualizaChave(int numitm, string chave)
    {
      //Atualizando a chave do item
      string strSql = "UPDATE aplicacao SET ";
      if (chave != string.Empty)
      {
        strSql = strSql + " chave_tratamento = '" + chave + "," + numitm.ToString() + "'";
      }
      else
      {
        strSql = strSql + " chave_tratamento ='" + numitm.ToString() + "'";
      }
      strSql = strSql + " WHERE aplicacao_codigo = " + numitm.ToString();


      try
      {
        ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
        banco.executaSQL(strSql);

      }
      catch (Exception ex)
      {
        throw ex;
      }

      if (chave == string.Empty)
      {
        chave = numitm.ToString();
      }
      else
      {
        chave = chave + "," + numitm;
      }

      //Verificando se o item possui filhos
      strSql = "SELECT aplicacao_codigo FROM aplicacao";
      strSql = strSql + " WHERE aplicacao_superior_codigo = 0" + numitm.ToString();
      System.Data.SqlClient.SqlDataReader objReaderAtualizaChave = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql); //SqlHelper.ExecuteReader(strConn, CommandType.Text, strSql);
      while (objReaderAtualizaChave.Read())
      {
        atualizaChave(Convert.ToInt32(objReaderAtualizaChave["aplicacao_codigo"]), chave);
      }
      objReaderAtualizaChave.Close();

    }
    #endregion

    #region alimentaColecao
    /// <summary>
    /// Alimenta a coleção de atributos de uma Aplicacao
    /// </summary>
    /// <param name="intCodigo">Código da aplicacao a ser alimentada</param>
    public void alimentaAplicacao(int intCodigo)
    {
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region getDescricaoTipoLicenca
    /// <summary>
    /// Retorna a descrição do Tipo de Licença
    /// </summary>
    /// <param name="strAbreviacaoTipoLicenca"></param>
    /// <returns></returns>
    public static String getDescricaoTipoLicenca(String strAbreviacaoTipoLicenca)
    {
      String strRetorno = String.Empty;

      switch (strAbreviacaoTipoLicenca.Trim())
      {
        case "USR":
          {
            strRetorno = "Por Usuário";
            break;
          }
        case "INS":
          {
            strRetorno = "Por Instalação";
            break;
          }
      }
      return strRetorno;
    }
    #endregion

    #region getDescricaoSituacao
    /// <summary>
    /// Retorna a descrição de uma Situacao
    /// </summary>
    /// <param name="strAbreviacaoSituacao"></param>
    /// <returns></returns>
    public static String getDescricaoSituacao(String strAbreviacaoSituacao)
    {
      String strRetorno = String.Empty;

      switch (strAbreviacaoSituacao.Trim())
      {
        case "S":
          {
            strRetorno = "Sim";
            break;
          }
        default:
          {
            strRetorno = "Não";
            break;
          }
      }
      return strRetorno;
    }
    #endregion

  }
}