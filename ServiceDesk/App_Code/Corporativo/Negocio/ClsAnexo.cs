using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe ClsAnexo.
  /// </summary>
  public class ClsAnexo
  {

    //Colecao de atributos do Anexo
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos do Anexo
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPessoaInclusor = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabelaIdentificador = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCaminho = new ServiceDesk.Banco.ClsAtributo();

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
    /// Código do Inclusor
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo PessoaInclusor
    {
      get { return objPessoaInclusor; }
      set { this.objPessoaInclusor = value; }
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
    /// Identificador da Tabela
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo TabelaIdentificador
    {
      get { return objTabelaIdentificador; }
      set { this.objTabelaIdentificador = value; }
    }

    /// <summary>
    /// Nome do Anexo
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
    /// Caminho físico do Anexo
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Caminho
    {
      get { return objCaminho; }
      set { this.objCaminho = value; }
    }

    #endregion

    #region Construtor
    public ClsAnexo()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsAnexo(int intCodigo)
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
      objAtributos.DescricaoTabela = "Anexo";
      objAtributos.NomeTabela = "Anexo";

      objCodigo.Campo = "anexo_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objPessoaInclusor.Campo = "pessoa_codigo";
      objPessoaInclusor.Descricao = "Código do Inclusor";
      objPessoaInclusor.CampoObrigatorio = false;
      objPessoaInclusor.Tipo = System.Data.DbType.Int32;
      objPessoaInclusor.Tamanho = 50;
      objAtributos.Add(objPessoaInclusor);

      objTabela.Campo = "tabela";
      objTabela.Descricao = "Tabela";
      objTabela.CampoObrigatorio = false;
      objTabela.Tipo = System.Data.DbType.String;
      objTabela.Tamanho = 255;
      objAtributos.Add(objTabela);

      objTabelaIdentificador.Campo = "tabela_identificador";
      objTabelaIdentificador.Descricao = "Identificador da Tabela";
      objTabelaIdentificador.CampoObrigatorio = false;
      objTabelaIdentificador.Tipo = System.Data.DbType.Int32;
      objTabelaIdentificador.Tamanho = 50;
      objAtributos.Add(objTabelaIdentificador);

      objNome.Campo = "anexo_nome";
      objNome.Descricao = "Nome";
      objNome.CampoObrigatorio = false;
      objNome.Tipo = System.Data.DbType.String;
      objNome.Tamanho = 255;
      objAtributos.Add(objNome);

      objDataInclusao.Campo = "data_inclusao";
      objDataInclusao.Descricao = "Data da inclusão";
      objDataInclusao.CampoObrigatorio = false;
      objDataInclusao.Tipo = System.Data.DbType.String;
      objDataInclusao.Tamanho = 255;
      objAtributos.Add(objDataInclusao);

      objCaminho.Campo = "anexo_caminho";
      objCaminho.Descricao = "Caminho";
      objCaminho.CampoObrigatorio = false;
      objCaminho.Tipo = System.Data.DbType.String;
      objCaminho.Tamanho = 255;
      objAtributos.Add(objCaminho);

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
            ClsAnexo objAnexo = new ClsAnexo();
            ServiceDesk.Controle.ClsDataGrid.geraDataGrid(objDataGrid, objAnexo.objAtributos);
            objAnexo = null;
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
            ClsAnexo objAnexo = new ClsAnexo();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objAnexo.objAtributos);
            objAnexo = null;
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
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsAnexo objAnexo, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objAnexo.objAtributos, bolCondicao);
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
            ClsAnexo objAnexo = new ClsAnexo();
            objDropDownList.DataTextField = objAnexo.objNome.Campo;
            objDropDownList.DataValueField = objAnexo.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objAnexo.objAtributos);
            objAnexo = null;
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
            ClsAnexo objAnexo = new ClsAnexo();
            objDropDownList.DataTextField = objAnexo.Nome.Campo;
            objDropDownList.DataValueField = objAnexo.Codigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objAnexo.Atributos);
            objDropDownList.Items.FindByValue(intCodigoItem.ToString()).Enabled = false;
            objAnexo = null;
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

            //if (this.objNome.Valor.Trim() == String.Empty)
            //{
            //    strMensagem = "Favor informar o nome do anexo.<br>";
            //}
            //else
            //{
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Anexo inserido com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            //}

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

            if (this.objNome.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar o nome do anexo.<br>";
            }
            else
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Anexo atualizado com sucesso.";
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
  }
}