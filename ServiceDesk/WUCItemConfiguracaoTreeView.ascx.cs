using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WUCItemConfiguracaoTreeView : System.Web.UI.UserControl
{

  #region Declarações Publicas

  public String strTabela;

  #endregion

  #region Declarações Privadas

  //Legenda do painel de agrupamento
  private String strLegendaGrupo = string.Empty;
  //Tabela Relacionada
  private String strTabelaRelacionada = string.Empty;

  #endregion

  #region Propriedades

  /// <summary>
  /// Legenda a ser aplicada na groupbox
  /// </summary>
  public string Legenda
  {
    get
    { return strLegendaGrupo; }
    set
    { strLegendaGrupo = value; }
  }

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

  #region evento trvTipo_TreeNodePopulate
  /// <summary>
  /// Evento que ocorre quando um no da tree view é criado
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void tvwCI_TreeNodePopulate(object sender, TreeNodeEventArgs e)
  {
      try
      {
          populaSubNivel(Convert.ToInt32(e.Node.Value), e.Node);

      }
      catch (Exception ex)
      {
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }

  #endregion

  #region Evento Salvar
  /// <summary>
  /// Evento Salvar
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void btnSalvar_Click(object sender, EventArgs e)
  {
      try
      {
          TreeNodeCollection objTreeNodeCollection = tvw_CI.Nodes;
          for (int intI = 0; intI < objTreeNodeCollection.Count; intI++)
          {
              VerificaNos(objTreeNodeCollection[intI]);
          }
          populaNoRaiz();
          tvw_CI.ExpandAll();

          Mensagem(true, false, "Operação realizada com sucesso.");
      }
      catch (Exception ex)
      {
          Mensagem(false, false, string.Empty);
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }
  #endregion 
  
  #region Evento Page Load
  /// <summary>
  /// Page Load
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void Page_Load(object sender, EventArgs e)
  {
      Mensagem(false, false, string.Empty);
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
          else //nao foi especidifcao, assume true
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

  #region método CarregaIC

  public void CarregaIC(int CodigoIdentificador)
  {

    if (CodigoIdentificador != 0)
    {
        try
        {
            lblCodigoIdentificador.Text = CodigoIdentificador.ToString();
            pnlCIs.GroupingText = this.strLegendaGrupo;
            populaNoRaiz();
            tvw_CI.ExpandAll();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
  }
  #endregion

  #region metodo populaNoRaiz
  /// <summary>
  /// Método que popula os nós que não possuem pai
  /// </summary>
  //private void populaNoRaiz(int CodigoIdentificador, string Tabela)
  private void populaNoRaiz()
  {
      try
      {
          String strSql = String.Empty;
          strSql = "(SELECT ic_codigo, nome, ";
          strSql += " '0' as ordem ";
          strSql += "FROM IC  ";
          strSql += "WHERE ic_codigo IN ( ";
          strSql += "Select  ";
          strSql += "case Charindex(',',IC.Chave) ";
          strSql += "when 0 then IC.Chave  ";
          strSql += "else substring(IC.Chave, 1, Charindex(',',IC.Chave)- 1)  ";
          strSql += "end  as ic_codigo ";
          strSql += "from " + strTabelaRelacionada + "IC ,  IC  ";
          strSql += "where " + strTabelaRelacionada + "IC." + strTabelaRelacionada + "_codigo =  '" + lblCodigoIdentificador.Text.Trim() + "' ";
          strSql += "AND  IC.ic_codigo = " + strTabelaRelacionada + "IC.ic_codigo ";
          strSql += ")) ";
          strSql += "UNION ";
          strSql += "( ";
          strSql += "  SELECT DISTINCT ";
          strSql += "  IC.ic_codigo,  nome, ";
          strSql += "  '1' as ordem ";
          strSql += "  FROM IC";
          strSql += "  WHERE ";
          strSql += "  ic_codigo_superior is null ";
          strSql += "  AND IC.ic_codigo NOT IN ";
          strSql += "  ( ";
          strSql += "    Select ";
          strSql += "    case Charindex(',',IC.Chave) ";
          strSql += "    when 0 then IC.Chave ";
          strSql += "    else substring(IC.Chave, 1, Charindex(',',IC.Chave)- 1) ";
          strSql += "    end  as ic_codigo ";
          strSql += "    from " + strTabelaRelacionada + "IC CIC,  IC IC ";
          strSql += "    where CIC." + strTabelaRelacionada + "_codigo = '" + lblCodigoIdentificador.Text.Trim() + "' ";
          strSql += "    AND CIC.ic_codigo = IC.ic_codigo ";
          strSql += "  ) ";
          strSql += ") ";
          strSql += "order by ordem ";

          System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
          tvw_CI.Nodes.Clear();
          populaNos(objDataReader, tvw_CI.Nodes);
          objDataReader.Dispose();

      }
      catch (Exception ex)
      {
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }
  #endregion

  #region metodo populaNos
  /// <summary>
  /// Método que popula os nós
  /// </summary>
  private void populaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection)
  {
      try
      {
          while (objDataReader.Read())
          {
              TreeNode objTreeNode = new TreeNode();
              objTreeNode.Text = objDataReader["nome"].ToString();
              objTreeNode.Value = objDataReader["ic_codigo"].ToString().Trim();
              if (Convert.ToInt32(objDataReader["ic_codigo"]) > 0)
              {
                  objTreeNode.PopulateOnDemand = true;
              }
              if (ClsItemConfiguracao.VerificaExistenciaItemConfiguracao(this.lblCodigoIdentificador.Text.Trim(), this.TabelaRelacionada, objDataReader["ic_codigo"].ToString()))
              {
                  objTreeNode.Checked = true;
              }
              else
              { objTreeNode.Checked = false; }
              objTreeNode.ShowCheckBox = true;
              objTreeNode.NavigateUrl = "Javascript:VisualizarItemConfiguracao(" + objTreeNode.Value + ");";
              objTreeNodeCollection.Add(objTreeNode);
              objTreeNode = null;
          }

      }
      catch (Exception ex)
      {
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }
  #endregion

  #region metodo populaSubNivel
  /// <summary>
  /// Método que popula os nós filhos da TreeView
  /// </summary>
  /// <param name="intCodigoPai"></param>
  /// <param name="objTreeNode"></param>
  private void populaSubNivel(int intCodigoPai, TreeNode objTreeNodePai)
  {
      try
      {
          String strSql = String.Empty;
          strSql = "SELECT DISTINCT ";
          strSql += "IC.ic_codigo, ic_codigo_superior, nome,  ";
          strSql += "ic_tipo_codigo, " + strTabelaRelacionada + "IC.ic_codigo as marcado ";
          strSql += "FROM IC, " + strTabelaRelacionada + "IC ";
          strSql += "WHERE  ";
          strSql += "IC.ic_codigo *= " + strTabelaRelacionada + "IC.ic_codigo ";
          strSql += "AND " + strTabelaRelacionada + "_codigo = " + lblCodigoIdentificador.Text.Trim();
          strSql += "AND ic_codigo_superior =" + intCodigoPai.ToString();
          strSql += " ORDER BY ic_tipo_codigo , nome ";

          System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
          populaNos(objDataReader, objTreeNodePai.ChildNodes);
          objDataReader.Dispose();

      }
      catch (Exception ex)
      {
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }
  #endregion

  #region metodo VerificaNos
  /// <summary>
  /// Verificar os nós
  /// </summary>
  /// <param name="objTreeNode"></param>
  private void VerificaNos(TreeNode objTreeNode)
  {
      try
      {
          if (objTreeNode.Checked)
          {
              try
              {
                  ServiceDesk.Negocio.ClsItemConfiguracao.AdicionaItemConfiguracao(lblCodigoIdentificador.Text.Trim(), this.TabelaRelacionada, objTreeNode.Value.ToString());
              }
              catch
              {
              }
          }
          else
          {
              if (ServiceDesk.Negocio.ClsItemConfiguracao.VerificaExistenciaItemConfiguracao(lblCodigoIdentificador.Text.Trim(), this.TabelaRelacionada, objTreeNode.Value.ToString()))
              {
                  ServiceDesk.Negocio.ClsItemConfiguracao.RemoveItemConfiguracao(lblCodigoIdentificador.Text.Trim(), this.TabelaRelacionada, objTreeNode.Value.ToString());
              }
          }

          foreach (TreeNode objNode in objTreeNode.ChildNodes)
          {
              VerificaNos(objNode);
          }

      }
      catch (Exception ex)
      {
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }
  #endregion

  #region Mensagem
  /// <summary>
    /// Exibe Mensagem na tela
    /// </summary>
    /// <param name="bolExibe">Recebe true ou false. Se é para exibir ou não.</param>
    /// <param name="bolMsgCritica">Recebe true ou false. Se é para exibir ícone critico ou não.</param>
    /// <param name="strMensagem">Conteúdo da mensagem</param>
    public void Mensagem(bool bolExibe, bool bolMsgCritica, string strMensagem)
    {
        try
        {
            Label lblMensagem = (Label)Parent.FindControl("lblMensagem");
            Image imgIcone = (Image)Parent.FindControl("imgIcone");
            HtmlControl divMensagem = (HtmlControl)Parent.FindControl("divMensagem");

            if (bolExibe == true && strMensagem != string.Empty)
            {
                divMensagem.Visible = true;
                lblMensagem.Text = strMensagem.Trim();
                lblMensagem.Visible = true;
                if (bolMsgCritica == true) imgIcone.ImageUrl = "images/icones/aviso.gif"; else imgIcone.ImageUrl = "images/icones/info.gif";
            }
            else
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
  #endregion
  
  #endregion
}
