using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WUCUsuario : System.Web.UI.UserControl
{
    #region Eventos
    #region Page_Load
    /// <summary>
    /// Evento Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    #endregion

    #region Executa pesquisa do usuário
    /// <summary>
    /// Executa pesquisa do usuário
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisa_Click(object sender, EventArgs e)
    {
        if (txtNomeUsuario.Text != string.Empty)
            ServiceDesk.Negocio.ClsUsuario.geraGridView(this.gvUsuarios, txtNomeUsuario.Text);
    }
    #endregion

    /// <summary>
    /// Evento RowCommand do Grid Usuários
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Selecionar")
            {
                GridViewRow objRow = gvUsuarios.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblPessoaCodigo");
                    if (lblCodigo.Text != string.Empty)
                        PreencheDadosPessoa(Convert.ToInt32(lblCodigo.Text));
                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    protected void btnlimpar_Click(object sender, EventArgs e)
    {
        lblPessoaCodigo.Text = string.Empty;

        foreach (Control item in formItens.Controls)
        {
            if (item is TextBox)
            {
                TextBox t = item as TextBox;
                t.Text = string.Empty;
            }
        }
    }
    #endregion

    #region Métodos

    #region Preenche dados do usuário
    /// <summary>
    /// Preenche dados do usuário
    /// </summary>
    /// <param name="CodigoSolicitante"></param>
    public void PreencheDadosPessoa(int CodigoSolicitante)
    {
        try
        {
            lblPessoaCodigo.Text = CodigoSolicitante.ToString();
            SqlDataReader objReaderSolicitante = ServiceDesk.Negocio.ClsUsuario.getInfoUsuario(CodigoSolicitante.ToString());
            if (objReaderSolicitante.Read())
            {
                txtNomeUsuario.Text = objReaderSolicitante["nome"].ToString();
                txtCodigoRede.Text = objReaderSolicitante["codigo_rede"].ToString();
                txtArea.Text = ServiceDesk.Negocio.ClsUsuario.getAreaUsuario(objReaderSolicitante["area_codigo"].ToString());
                txtCargo.Text = ServiceDesk.Negocio.ClsUsuario.getCargoUsuario(objReaderSolicitante["cargo_codigo"].ToString());
                txtEmail.Text = objReaderSolicitante["email"].ToString();
                txtEmpresa.Text = ServiceDesk.Negocio.ClsUsuario.getEmpresaUsuario(objReaderSolicitante["estrutura_codigo"].ToString());
                txtMatricula.Text = objReaderSolicitante["matricula"].ToString();
                txtTelefoneRamal.Text = objReaderSolicitante["telefone"].ToString() + "/" + objReaderSolicitante["ramal"].ToString();
                txtUsuarioVIP.Text = objReaderSolicitante["flag_vip"].ToString();
            }
            objReaderSolicitante.Close();
            objReaderSolicitante.Dispose();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Código da pessoa
    /// <summary>
    /// Código da pessoa
    /// </summary>
    /// <returns></returns>
    public int PessoaCodigo()
    {
        if (lblPessoaCodigo.Text != string.Empty)
            return Convert.ToInt32(lblPessoaCodigo.Text);
        else
            return 0;
    }
    #endregion

    #region Inclui o usuário atual como solicitante.
    /// <summary>
    /// Inclui o usuário atual como solicitante.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProprioSolicitante_Click(object sender, EventArgs e)
    {
        PreencheDadosPessoa(ClsUsuario.getCodigoUsuario());
    }
    #endregion
    #endregion

 
}
