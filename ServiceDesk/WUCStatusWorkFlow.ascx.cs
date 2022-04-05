using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WUCStatusWorkFlow : System.Web.UI.UserControl
{
    public DropDownList DdlStatusFuturo { get { return ddlStatusFuturo; } }
    public string StatusAtual { get { return txtStatusAtual.Text; } }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Metodo setStatus
    /// <summary>
    /// Preenche o TextBox com o Status atual
    /// </summary>
    /// <param name="strStatus">Status atual</param>
    public void setStatus(String strStatus)
    {
        txtStatusAtual.Text = strStatus.Trim();
    }
    #endregion

    #region Metodo setStatus
    /// <summary>
    /// Preenche o TextBox com o Status atual
    /// </summary>
    /// <param name="strStatus">Código do Status atual</param>
    public void setStatus(int intStatus)
    {
        ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus(intStatus);
        setStatus(objStatus.Descricao.Valor.Trim());
        lblCodigoStatusAtual.Text = intStatus.ToString();
        objStatus = null;
    }
    #endregion

    #region metodo montaDados
    /// <summary>
    /// Pega o codigo do status atual e chama o monta dados
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoStatus"></param>
    public void montaDados(int intCodigoItem, String strTabela)
    {
        String strSql = String.Empty;

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        lblCodigoStatusAtual.Text = objBanco.retornaValorCampo(strTabela.Trim(), "status_codigo", strTabela.Trim() + "_codigo = " + intCodigoItem.ToString());
        objBanco = null;

        if (lblCodigoStatusAtual.Text.Trim() != String.Empty)
        {
            montaDados(intCodigoItem, strTabela, Convert.ToInt32(lblCodigoStatusAtual.Text.Trim()));
        }

    }
    #endregion

    #region metodo montaDados
    /// <summary>
    /// Chama o metodo que monta o DropDownList dos Proximos possiveis status da classe workflow
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intCodigoStatus"></param>
    public void montaDados(int intCodigoItem, String strTabela, int intCodigoStatus)
    {
        lblTabela.Text = strTabela.Trim();
        lblCodigoItem.Text = intCodigoItem.ToString();
        //SServiceDesk.Negocio.ClsWorkFlow.geraDropDownListProximo(ddlStatusFuturo, strTabela, intCodigoStatus);
        SServiceDesk.Negocio.ClsWorkFlow.geraDropDownListProximo(ddlStatusFuturo, strTabela, intCodigoItem, intCodigoStatus);
        if (ddlStatusFuturo.Items.Count > 0)
        {
            ddlStatusFuturo.Items.Insert(0, "Próximo Status");
            ddlStatusFuturo.Items[0].Value = "0";
        }
        if (intCodigoStatus > 0)
        {
            setStatus(intCodigoStatus);
        }
    }
    #endregion

    #region metodo primeiroStatus
    /// <summary>
    /// Retorna o primeiro status de um workflow
    /// </summary>
    /// <param name="strTabela"></param>
    public int primeiroStatus(String strTabela)
    {
        lblCodigoStatusAtual.Text = SServiceDesk.Negocio.ClsWorkFlow.primeiroStatus(strTabela).ToString();
        return Convert.ToInt32(lblCodigoStatusAtual.Text);
    }
    #endregion

    #region Metodo salvaStatus
    /// <summary>
    /// Salva o novo Status
    /// </summary>
    public bool salvaStatus()
    {
        String strSql = String.Empty;
        String strStatusAnterior = lblCodigoStatusAtual.Text.Trim();
        int intProximoStatus = 0;

        if ((lblTabela.Text.Trim() != String.Empty) && (lblCodigoItem.Text.Trim() != String.Empty))
        {
            if (ddlStatusFuturo.SelectedIndex > 0)
            {
                intProximoStatus = Convert.ToInt32(ddlStatusFuturo.SelectedValue);
            }
            else
            {
                if (ddlStatusFuturo.SelectedIndex <= 0)
                {
                    intProximoStatus = SServiceDesk.Negocio.ClsWorkFlow.proximoStatusAutomatico(lblTabela.Text.Trim(), lblCodigoStatusAtual.Text.Trim(), Convert.ToInt32(lblCodigoItem.Text.Trim()));
                }
            }

            if (intProximoStatus > 0)
            {
                strSql = "UPDATE " + lblTabela.Text.Trim();
                strSql += " SET status_codigo = " + intProximoStatus.ToString();
                strSql += " WHERE " + lblTabela.Text.Trim() + "_codigo = " + lblCodigoItem.Text.Trim();
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSql))
                {
                    //=========================================================================================//
                    // - O que: Escalação Horizontal Não Livre - Atualiza o código do nivel para um nível 
                    // inferior ao atual se foi reprovado em algum nível e se a escalação não for livre.
                    // - Quem: Fernanda Passos
                    // - Quando: 03/2006
                    //=========================================================================================//
                    if (ClsParametro.CodigoStatusReprovado1Nivel == intProximoStatus.ToString() || ClsParametro.CodigoStatusReprovado2Nivel == intProximoStatus.ToString() || ClsParametro.CodigoStatusReprovado3Nivel == intProximoStatus.ToString()) ClsEquipeNivel.AtualizaProcessoNivelInferiorAoAtual(Convert.ToInt32(lblCodigoItem.Text.Trim()), lblTabela.Text.Trim());
                    //=========================================================================================//

                    setStatus(intProximoStatus);
                    montaDados(Convert.ToInt32(lblCodigoItem.Text.Trim()), lblTabela.Text.Trim());
                }
                objBanco = null;

                SServiceDesk.Negocio.ClsWorkFlow.salvaRepercusao(Convert.ToInt32(lblCodigoItem.Text.Trim()), lblTabela.Text.Trim().ToLower(), intProximoStatus.ToString());
            }
            if (intProximoStatus > 0) SServiceDesk.Negocio.ClsWorkFlow.gravaLog(Convert.ToInt32(lblCodigoItem.Text.Trim()), lblTabela.Text.Trim().ToLower(), strStatusAnterior, intProximoStatus.ToString());


        }
        return true;

    }
    #endregion

}