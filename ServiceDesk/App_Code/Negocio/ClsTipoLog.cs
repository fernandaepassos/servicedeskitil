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
/// Summary description for ClsTipoLog
/// </summary>
public class ClsTipoLog
{
    public ClsTipoLog()
    {
        this.alimentaColecaoCampos();
    }

    //Colecao de atributos de Tipo de Impacto
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de uma Tipo de Impacto
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();


    #region Propriedades

    public ServiceDesk.Banco.ClsAtributos Atributos
    {
        get
        {
            return this.objAtributos;
        }
    }

    /// <summary>
    /// Código do Tipo.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Codigo
    {
        get { return objCodigo; }
        set { this.objCodigo = value; }
    }

    /// <summary>
    /// Descrição do Tipo.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
        get { return objDescricao; }
        set { this.objDescricao = value; }
    }



    #endregion

    #region métodos


    #region alimentaColecaoCampos

    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
        objAtributos.NomeTabela = "LogEventoTipo";
        objAtributos.DescricaoTabela = "Tipo de Log de Evento";

        objCodigo.Campo = "log_evento_tipo_codigo";
        objCodigo.Descricao = "Código";
        objCodigo.CampoIdentificador = true;
        objCodigo.CampoObrigatorio = true;
        objCodigo.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objCodigo);

        objDescricao.Campo = "descricao";
        objDescricao.Descricao = "Descrição";
        objDescricao.CampoIdentificador = false;
        objDescricao.CampoObrigatorio = true;
        objDescricao.Tipo = System.Data.DbType.String;
        objAtributos.Add(objDescricao);
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um novo tipo de log
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
                strMensagem = "Favor informar a Descrição.";
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

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ClsTipoLog objTipoLog = new ClsTipoLog();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoLog.objAtributos);
            objTipoLog = null;
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
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsTipoLog objTipoLog, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoLog.objAtributos, bolCondicao);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo altera
    /// <summary>
    /// Método que altera um tipo de Log
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
                strMensagem = "Favor informar a Descrição do Tipo.";
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
    /// Método que exclui um tipo de Log
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

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
        try
        {
            ClsTipoLog objTipoLog = new ClsTipoLog();
            objDropDownList.DataTextField = objTipoLog.objDescricao.Campo;
            objDropDownList.DataValueField = objTipoLog.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objTipoLog.objAtributos);
            objTipoLog = null;

            //Adiciona a opção default no dropdownlist.
            ListItem itemDefault = new ListItem();
            itemDefault.Text = "--";
            itemDefault.Value = "";
            itemDefault.Selected = true;
            objDropDownList.Items.Insert(0, itemDefault);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region getCodigoTipoLogInsercao
    /// <summary>
    /// Retorna o codigo do tipo de log
    /// </summary>
    /// <returns></returns>
    static public string getCodigoTipoLogInsercao()
    {
        String strCodigoTipoLogInsercao = String.Empty;
        try
        {
            strCodigoTipoLogInsercao = ClsParametro.CodigoTipoLogInclusao;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strCodigoTipoLogInsercao;
    }
    #endregion

    #region getCodigoTipoLogAlteracao
    /// <summary>
    /// Retorna o codigo do tipo de log
    /// </summary>
    /// <returns></returns>
    static public string getCodigoTipoLogAlteracao()
    {
        String strCodigoTipoLogAlteracao = String.Empty;
        try
        {
            strCodigoTipoLogAlteracao = ClsParametro.CodigoTipoLogAlteracao;
        }
        catch (Exception ex)
        {
            throw ex;
        }


        return strCodigoTipoLogAlteracao;
    }
    #endregion

    #region getCodigoTipoLogExclusao
    /// <summary>
    /// Retorna o codigo do tipo de log
    /// </summary>
    /// <returns></returns>
    static public string getCodigoTipoLogExclusao()
    {
        String strCodigoTipoLogExclusao = String.Empty;
        try
        {
            strCodigoTipoLogExclusao = ClsParametro.CodigoTipoLogExclusao;
        }
        catch (Exception ex)
        {
            throw ex;
        }


        return strCodigoTipoLogExclusao;
    }
    #endregion

    #region getCodigoTipoLogSeguranca
    /// <summary>
    /// Retorna o codigo do tipo de log
    /// </summary>
    /// <returns></returns>
    static public string getCodigoTipoLogSeguranca()
    {
        String strCodigoTipoLogSeguranca = String.Empty;
        try
        {
            strCodigoTipoLogSeguranca = ClsParametro.CodigoTipoLogSeguranca;
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return strCodigoTipoLogSeguranca;
    }
    #endregion
    #endregion
}
