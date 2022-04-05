using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe Nomenclatura
/// </summary>
public class ClsNomenclatura
{
    //Colecao de atributos de Nomenclatura
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de uma Nomenclatura
    private ServiceDesk.Banco.ClsAtributo objNomenclaturaCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objNomenclaturaCodigoSuperior = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objIdioma = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAplicacao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objIdentificador = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTexto = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objUrl = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objParametro = new ServiceDesk.Banco.ClsAtributo();

    #region Propriedades
    /// <summary>
    /// Cole��o de atributos.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributos Atributos
    {
        get { return this.objAtributos; }
    }

    /// <summary>
    /// C�digo da Nomenclatura.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo NomenclaturaCodigo
    {
        get { return objNomenclaturaCodigo; }
        set { this.objNomenclaturaCodigo = value; }
    }

    /// <summary>
    /// C�digo da Nomenclatura superior.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo NomenclaturaCodigoSuperior
    {
        get { return objNomenclaturaCodigoSuperior; }
        set { this.objNomenclaturaCodigoSuperior = value; }
    }

    /// <summary>
    /// C�digo do Idioma
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Idioma
    {
        get { return objIdioma; }
        set { this.objIdioma = value; }
    }

    /// <summary>
    /// C�digo da Aplica��o
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Aplicacao
    {
        get { return objAplicacao; }
        set { this.objAplicacao = value; }
    }

    /// <summary>
    /// C�digo do Identificador
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Identificador
    {
        get { return objIdentificador; }
        set { this.objIdentificador = value; }
    }

    /// <summary>
    /// Descri��o da Nomenclatura
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Texto
    {
        get { return objTexto; }
        set { this.objTexto = value; }
    }

    /// <summary>
    /// url do Item
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Url
    {
        get { return objUrl; }
        set { this.objUrl = value; }
    }

    /// <summary>
    /// Par�metro
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Parametro
    {
        get { return objParametro; }
        set { this.objParametro = value; }
    }
    #endregion

    #region metodo Construtor da classe
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsNomenclatura()
    {
        this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsNomenclatura(int intCodigo)
    {
        try
        {
            this.alimentaColecaoCampos();
            this.objNomenclaturaCodigo.Valor = intCodigo.ToString();
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
    /// M�todo que alimenta a cole��o de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
        objAtributos.NomeTabela = "Nomenclatura";
        objAtributos.DescricaoTabela = "Nomenclatura";

        objNomenclaturaCodigo.Campo = "nomenclatura_codigo";
        objNomenclaturaCodigo.Descricao = "C�digo";
        objNomenclaturaCodigo.CampoIdentificador = true;
        objNomenclaturaCodigo.CampoObrigatorio = true;
        objNomenclaturaCodigo.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objNomenclaturaCodigo);

        objNomenclaturaCodigoSuperior.Campo = "nomenclatura_codigo_superior";
        objNomenclaturaCodigoSuperior.Descricao = "C�digo Superior da nomenclatura";
        objNomenclaturaCodigoSuperior.CampoIdentificador = false;
        objNomenclaturaCodigoSuperior.CampoObrigatorio = true;
        objNomenclaturaCodigoSuperior.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objNomenclaturaCodigoSuperior);

        objIdioma.Campo = "idioma";
        objIdioma.Descricao = "Idioma";
        objIdioma.CampoIdentificador = false;
        objIdioma.CampoObrigatorio = false;
        objIdioma.Tipo = System.Data.DbType.String;
        objIdioma.Tamanho = 1;
        objAtributos.Add(objIdioma);

        objAplicacao.Campo = "aplicacao";
        objAplicacao.Descricao = "aplicacao";
        objAplicacao.CampoIdentificador = false;
        objAplicacao.CampoObrigatorio = false;
        objAplicacao.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objAplicacao);

        objIdentificador.Campo = "identificador";
        objIdentificador.Descricao = "Identificador";
        objIdentificador.CampoIdentificador = false;
        objIdentificador.CampoObrigatorio = false;
        objIdentificador.Tipo = System.Data.DbType.String;
        objIdentificador.Tamanho = 50;
        objAtributos.Add(objIdentificador);

        objTexto.Campo = "texto";
        objTexto.Descricao = "texto";
        objTexto.CampoIdentificador = false;
        objTexto.CampoObrigatorio = false;
        objTexto.Tipo = System.Data.DbType.String;
        objTexto.Tamanho = 200;
        objAtributos.Add(objTexto);

        objUrl.Campo = "url";
        objUrl.Descricao = "url";
        objUrl.CampoIdentificador = false;
        objUrl.CampoObrigatorio = false;
        objUrl.Tipo = System.Data.DbType.String;
        objUrl.Tamanho = 100;
        objAtributos.Add(objUrl);

        objParametro.Campo = "parametro";
        objParametro.Descricao = "parametro";
        objParametro.CampoIdentificador = false;
        objParametro.CampoObrigatorio = false;
        objParametro.Tipo = System.Data.DbType.String;
        objParametro.Tamanho = 100;
        objAtributos.Add(objParametro);
    }
    #endregion

    #region Popula dropDown aplicacao
    /// <summary>
    /// Popula DropDownList Aplica��o
    /// </summary>
    /// <returns>Retorna DataReader</returns>
    public System.Data.SqlClient.SqlDataReader PopulaDropDownAplicacao()
    {
        try
        {
            String strSql = "SELECT aplicacao_codigo, descricao FROM aplicacao ORDER BY aplicacao_codigo";
            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PopulaNivelRaiz
    /// <summary>
    /// M�todo que popula o n� raiz da TreeView
    /// </summary>
    /// <param name="strCodigoAplicacao">C�digo da aplica��o</param>
    /// <param name="strCodigoIdioma">C�digo do Idioma</param>
    /// <returns>DataReader</returns>
    public System.Data.SqlClient.SqlDataReader PopulaNivelRaiz(String strCodigoAplicacao, String strCodigoIdioma)
    {
        String strSql;
        try
        {
            strSql = "SELECT nomenclatura_codigo, texto,identificador,(SELECT COUNT(*) FROM Nomenclatura ";
            strSql += "WHERE nomenclatura_codigo_superior = Nom.nomenclatura_codigo) NumNosFilho FROM ";
            strSql += "Nomenclatura Nom WHERE Nom.aplicacao = " + strCodigoAplicacao + " AND ";
            strSql += "Nom.Idioma = '" + strCodigoIdioma + "' AND nomenclatura_codigo_superior IS NULL ";

            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PopulaSubNivel
    /// <summary>
    /// M�todo que popula os sub n�veis da treeview
    /// </summary>
    /// <param name="CodigoNivelSuperior">C�digo do nivel superior</param>
    /// <param name="strCodigoAplicacao">C�digo da aplica��o</param>
    /// <param name="strCodigoIdioma">C�digo do Idioma</param>
    /// <returns>DataReader</returns>
    public System.Data.SqlClient.SqlDataReader PopulaSubNivel(int CodigoNivelSuperior, String strCodigoAplicacao, String strCodigoIdioma)
    {
        String strSql;
        try
        {
            strSql = "SELECT nomenclatura_codigo, texto,identificador,(SELECT COUNT(*) FROM Nomenclatura ";
            strSql += "WHERE nomenclatura_codigo_superior = Nom.nomenclatura_codigo) NumNosFilho FROM ";
            strSql += "Nomenclatura Nom WHERE Nom.aplicacao = " + strCodigoAplicacao + " AND ";
            strSql += "Nom.Idioma = '" + strCodigoIdioma + "' AND nomenclatura_codigo_superior = " + CodigoNivelSuperior;

            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region CriaNomenclatura
    /// <summary>
    /// M�todo que crNomenclatura
    /// </summary>
    /// <param name="strIdentificador">C�digo do componente a ser alterado</param>
    /// <returns>Retorna uma string</returns>
    public static String CriaNomenclatura(String strIdentificador)
    {
        String strSQL = String.Empty;
        SqlDataReader dr;
        try
        {
            strSQL = "SELECT texto FROM nomenclatura WHERE identificador = '" + strIdentificador.Trim() + "'";
            dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);

            if (dr.Read())
                return dr["texto"].ToString();
            else
                return String.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ValidaInsercao
    /// <summary>
    /// M�todo que verifica se existe algum registro com a conbina��o das 3 chaves
    /// </summary>
    /// <param name="strAplicacao">C�digo da aplica��o</param>
    /// <param name="strIdentificador">identificador</param>
    /// <param name="strIdioma">C�digo do idioma</param>
    public bool ValidaInsercao(String strCodigoNomenclatura, String strAplicacao, String strIdentificador, String strIdioma)
    {
        String strSql;
        try
        {
            strSql = "SELECT nomenclatura_codigo FROM Nomenclatura WHERE aplicacao = '" + strAplicacao + "'";
            strSql += "AND identificador = '" + strIdentificador + "' AND idioma = '" + strIdioma + "' ";

            if (strCodigoNomenclatura != String.Empty && strCodigoNomenclatura != null)
                strSql += "AND nomenclatura_codigo <> " + strCodigoNomenclatura;

            if (ServiceDesk.Banco.ClsBanco.geraDataReader(strSql).Read())
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region RetornaNomenclatura
    /// <summary>
    /// RetornaNomenclatura - Retorna o Pai de uma nomenclatura ou a pr�pia nomenclatura
    /// </summary>
    /// <param name="intNomenclaturaCodigo">C�digo do pai ou C�digo do propio item</param>
    /// <returns> Retorna o codigo, codigo superior e descri��o da Nomenclatura</returns>
    public System.Data.SqlClient.SqlDataReader RetornaNomenclatura(int intNomenclaturaCodigo)
    {
        String strSql;
        try
        {
            strSql = "SELECT nomenclatura_codigo, nomenclatura_codigo_superior, texto, ";
            strSql += "identificador, idioma, aplicacao, url, parametro FROM Nomenclatura ";
            strSql += " WHERE nomenclatura_codigo = 0" + intNomenclaturaCodigo;

            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// M�todo que insere uma nova nomenclatura.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objIdioma.Valor.Trim() == String.Empty)
                strMensagem = "Informe o idioma do item.<br />";

            if (this.objAplicacao.Valor.Trim() == String.Empty)
                strMensagem += "Informe a aplica��o a qual o item pertence.<br />";

            if (this.objIdentificador.Valor.Trim() == String.Empty)
                strMensagem += "Informe o identificador do item.<br />";

            if (this.objTexto.Valor.Trim() == String.Empty)
                strMensagem += "Informe o nome do item.<br />";

            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Nomenclatura inserida com sucesso.";
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
    /// M�todo que altera uma nomenclatura
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
    public bool altera(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objIdioma.Valor.Trim() == String.Empty)
                strMensagem = "Informe o idioma do item.<br />";

            if (this.objAplicacao.Valor.Trim() == String.Empty)
                strMensagem += "Informe a aplica��o a qual o item pertence.<br />";

            if (this.objIdentificador.Valor.Trim() == String.Empty)
                strMensagem += "Informe o identificador do item.<br />";

            if (this.objTexto.Valor.Trim() == String.Empty)
                strMensagem += "Informe o nome do item.<br />";

            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Nomenclatura atualizada com sucesso.";
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
    /// M�todo que exclui uma nomenclatura
    /// </summary>
    /// <param name="intIdentificador">Identifica a nomenclatura a ser excluida</param>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
    public bool exclui(string strIdentificador)
    {
        try
        {
            String strSql;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            strSql = "DELETE FROM Nomenclatura WHERE nomenclatura_codigo IN (SELECT nomenclatura_codigo FROM Nomenclatura ";
            strSql += "WHERE identificador = '" + strIdentificador + "' or identificador like '" + strIdentificador + ".%') ";

            objBanco.executaSQL(strSql);
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

}
