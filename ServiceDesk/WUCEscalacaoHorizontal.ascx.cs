using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WUCEscalacaoHorizontal : System.Web.UI.UserControl
{

  #region Declarações Publicas

  public String strTabela;
  public int intIdentificadorTabela;

  #endregion

  #region Declarações Privadas
  //Tabela Relacionada
  private String strTabelaRelacionada = string.Empty;
  #endregion

  #region Propriedades
  /// <summary>
  /// Tabela Relacionada
  /// </summary>
  public string TabelaRelacionada
  {
    get
    { return strTabelaRelacionada; }
    set
    { strTabelaRelacionada = value; }
  }
  #endregion

  #region Métodos

  #region método ExibeMensagem
  /// <summary>
  /// Fornece um meio de acesso ao painel de mensagem da tela principal
  /// </summary>
  /// <param name="Mensagem">Mensagem a ser exibida na tela</param>
  /// <param name="Imagem">Nome da imagem do ícone do painel</param>
  /// <param name="Ativo">true para Exibir, false para Ocultar</param>
  /// <example>ExibeMensagem("teste","images/icones/aviso.gif",true)</example>
  private void ExibeMensagem(String Mensagem, String Imagem, bool Ativo)
  {
    try
    {
      Label lblMensagem = (Label)Page.FindControl("lblMensagem");
      HtmlControl divMensagem = (HtmlControl)Page.FindControl("divMensagem");
      Image imgIcone = (Image)Page.FindControl("imgIcone");

      lblMensagem.Text = Mensagem;
      imgIcone.ImageUrl = Imagem;

      if (Ativo == true)
      {
        lblMensagem.Visible = true;
        divMensagem.Visible = true;
      }
      else if (Ativo == false)
      {
        lblMensagem.Visible = false;
        divMensagem.Visible = false;
      }
      else //nao foi especificado, assume true
      {
        lblMensagem.Visible = true;
        divMensagem.Visible = true;
      }
    }
    catch (Exception ex)
    {
      ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    }
  }

  #endregion

  #region Bloqueia Campos
  /// <summary>
  /// Bloqueia campos
  /// </summary>
  /// <param name="bolBloquiaEquipe">Se á para bloquiar a equipe ou não</param>
  /// <param name="bolBloqueiaNivel">Se á para bloquiar o nível ou não</param>
  /// <param name="bolBloqueiaTecnico">Se á para bloquiar o técnico ou não</param>
  public void BloqueiaCampos(bool bolBloquiaEquipe, bool bolBloqueiaNivel, bool bolBloqueiaTecnico)
  {
      //===============================================================================================//
      // - O que: Bloqueia os campos da escalação de acordo com o parametro "BLOQUEARESCALACAOCHAMADO"
      // que indica se para a empresa atual deseja-se bloquear os campos de escalação.
      // - Quem: Fernanda Passos.
      // - Quando: 06/03/2006 ás 10:15hs.
      //===============================================================================================//
      if (bolBloqueiaNivel == true) this.ddlNivel.Enabled = false; else this.ddlNivel.Enabled = true;

      if (bolBloqueiaTecnico == true) this.ddlTecnico.Enabled = false; else this.ddlTecnico.Enabled = true;

      if (bolBloquiaEquipe == true) this.ddlEquipe.Enabled = false; else this.ddlEquipe.Enabled = true;

  }
  #endregion

  public void geraDropDownListNivel(string nivelCodigo)
  {
    try
    {      
      ClsEquipeNivel.geraDropDownList(this.ddlNivel, nivelCodigo);
    }
    catch (Exception ex)
    {
      ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    }
  }

  public void setEquipeNivelTecnico(string strNivelCodigo, string strEquipeCodigo, string strTecnicoCodigo)
  {
    try
    {
      if (strNivelCodigo != string.Empty)
      {
        this.ddlNivel.ClearSelection();
        this.ddlEquipe.ClearSelection();
        this.ddlTecnico.ClearSelection();
        this.ddlNivel.SelectedValue = strNivelCodigo;

        if (strEquipeCodigo != string.Empty)
        {
          ClsEquipe.geraDropDownListNivel(ddlEquipe, Convert.ToInt32(strNivelCodigo));
          ddlEquipe.SelectedValue = strEquipeCodigo;
          ddlTecnico.ClearSelection();
          ddlTecnico.Items.Clear();
          SqlDataReader objReader = ServiceDesk.Negocio.ClsEquipe.getTecnicosEquipe(strEquipeCodigo);
          ddlTecnico.DataTextField = "nome";
          ddlTecnico.DataValueField = "pessoa_codigo";
          ddlTecnico.DataSource = objReader;
          ddlTecnico.DataBind();

          //Adiciona a opção default no dropdownlist.
          ListItem itemDefault = new ListItem();
          itemDefault.Text = "";
          itemDefault.Value = "";
          ddlTecnico.Items.Insert(0, itemDefault);

          if (strTecnicoCodigo != string.Empty)
          { ddlTecnico.SelectedValue = strTecnicoCodigo; }
        }
      } 
    }
    catch (Exception ex)
    {
      ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    } 
  }
  
  public string getNivel()
  {
    if (this.ddlNivel.SelectedIndex > 0)
    {
      return this.ddlNivel.SelectedValue;
    }
    return string.Empty;
  }

  public string getEquipe()
  {
    if (this.ddlEquipe.SelectedIndex > 0)
    {
      return this.ddlEquipe.SelectedValue;
    }
    return string.Empty;
  }

  public string getTecnico()
  {
    if (this.ddlTecnico.SelectedIndex > 0 && this.ddlEquipe.SelectedIndex > 0)
    {
      return this.ddlTecnico.SelectedValue;
    }
    return string.Empty;
  }

  protected void Page_Load(object sender, EventArgs e)
  {

  }
  

  #endregion
  


  protected void ddlNivel_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      ddlTecnico.Items.Clear();
      ddlEquipe.Items.Clear();
      if (ddlNivel.SelectedValue != string.Empty)
      {
          //=======================================================================//
          // - O que: Verifica se pode ser selecionado um nível inferior ao atual
          // - Quem: Fernanda Passos
          // - Quando: 03/2006
          //=======================================================================//
          if (ClsParametro.EscalacaoLivre == "N" && intIdentificadorTabela != 0 && strTabela != string.Empty)
          {
              if (Convert.ToInt32(ddlNivel.SelectedValue.Trim()) < ClsEquipeNivel.GetCodigoNivelAtualProcesso(strTabela.Trim(), intIdentificadorTabela))
                  ddlNivel.SelectedIndex = ClsEquipeNivel.GetCodigoNivelAtualProcesso(strTabela.Trim(), intIdentificadorTabela);
          }
          //=======================================================================//

        ClsEquipe.geraDropDownListNivel(ddlEquipe, Convert.ToInt32(ddlNivel.SelectedValue));
      }
    }
    catch (Exception ex)
    {
      ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);

      ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    }
  }

  protected void ddlEquipe_SelectedIndexChanged(object sender, EventArgs e)
  {
    try
    {
      ddlTecnico.Items.Clear();

      if (ddlEquipe.SelectedValue != string.Empty)
      {
        SqlDataReader objReader = ServiceDesk.Negocio.ClsEquipe.getTecnicosEquipe(ddlEquipe.SelectedValue);
        if (objReader.HasRows)
        {
          ddlTecnico.DataTextField = "nome";
          ddlTecnico.DataValueField = "pessoa_codigo";
          ddlTecnico.DataSource = objReader;
          ddlTecnico.DataBind();
        }
        objReader.Close();
        objReader.Dispose();

        //Adiciona a opção default no dropdownlist.
        ListItem itemDefault = new ListItem();
        itemDefault.Text = "--";
        itemDefault.Value = "";
        itemDefault.Selected = true;
        ddlTecnico.Items.Insert(0, itemDefault);
      }
    }
    catch (Exception ex)
    {
      ExibeMensagem("Ocorreu um erro. Operação não realizada.", "images/icones/aviso.gif", true);
      ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    }
  }

}
