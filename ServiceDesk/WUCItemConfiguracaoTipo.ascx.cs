using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WUCItemConfiguracaoTipo : System.Web.UI.UserControl
{

  #region metodo Page_Load
  /// <summary>
  /// Metodo de Carregamento da pagina
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void Page_Load(object sender, EventArgs e)
  {
    try
    {
      String strCodigoItem = String.Empty;

      //Esconde a mensagem de erro
      lblMensagem.Visible = false;
      divMensagem.Visible = false;

      if (!Page.IsPostBack)
      {

        ServiceDesk.Negocio.ClsItemConfiguracaoTipo.geraDropDownList(ddlSuperior);
        ServiceDesk.Negocio.ClsStatusTabela.geraDropDownList(ddlStatus, "ICtipo");
        ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraCheckBoxList(cblAtributo);

        //Montando a treeview
        populaNoRaiz();
        
        btnExclui.Attributes.Add("onclick", "return verifica();");

        if (Request.QueryString["codigo"] != null)
        {
          strCodigoItem = Request.QueryString["codigo"].ToString().Trim();

          ServiceDesk.Negocio.ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ServiceDesk.Negocio.ClsItemConfiguracaoTipo(Convert.ToInt32(strCodigoItem));

          if (objItemConfiguracaoTipo.Superior.Valor != String.Empty)
          {
            ddlSuperior.Items[0].Selected = false;
            ddlSuperior.Items.FindByValue(objItemConfiguracaoTipo.Superior.Valor).Selected = true;
          }

          //Desabilitando o item atual na lista dos itens superiores
          ddlSuperior.Items.FindByValue(objItemConfiguracaoTipo.Codigo.Valor).Enabled = false;

          txtDescricao.Text = objItemConfiguracaoTipo.Descricao.Valor;
          txtPrefixo.Text = objItemConfiguracaoTipo.Prefixo.Valor;
          if (objItemConfiguracaoTipo.Status.Valor != String.Empty)
          {
            try
            {
              ddlStatus.Items[0].Selected = false;
              ddlStatus.Items.FindByValue(objItemConfiguracaoTipo.Status.Valor).Selected = true;
            }
            catch(Exception ex)
            {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
            }
          }

          objItemConfiguracaoTipo.marcaTipoAtributo(Convert.ToInt32(strCodigoItem), cblAtributo);

          objItemConfiguracaoTipo = null;

        }

      }

      if (Session["strStatus"] != null)
      {
        //Exibindo mensagem com o status da operação
        lblMensagem.Text = Session["strStatus"].ToString();
        imgIcone.ImageUrl = "images/icones/aviso.gif";
        lblMensagem.Visible = true;
        divMensagem.Visible = true;
      }

      Session["strStatus"] = null;

    }
    catch (Exception ex)
    {
        ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    }
  }
  #endregion

  #region evento btnSalva_Click
  /// <summary>
  /// Método que salva o Tipo do Item de Configuração (inserir/alterar)
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void btnSalva_Click(object sender, EventArgs e)
  {
    try
    {
      if (Request.QueryString["codigo"] == null)
      {
        insereItemConfiguracaoTipo();
      }
      else
      {
        if (Request.QueryString["codigo"] != null)
        {
          alteraItemConfiguracaoTipo(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
        }
      }

      //Atualizando a TreeView
      populaNoRaiz();
      trvTipo.ExpandAll();

    }
    catch (Exception ex)
    {
        ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    }
  }
  #endregion

  #region metodo insereItemConfiguracaoTipo
  /// <summary>
  /// Método que insere um novo Tipo do Item de Configuração
  /// </summary>
  private void insereItemConfiguracaoTipo()
  {
      try
      {
          String strMensagem = String.Empty;
          String strMensagemAltera = String.Empty;

          ServiceDesk.Negocio.ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ServiceDesk.Negocio.ClsItemConfiguracaoTipo();
          ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

          objIdentificador.Tabela.Valor = objItemConfiguracaoTipo.Atributos.NomeTabela;
          objItemConfiguracaoTipo.Codigo.Valor = objIdentificador.getProximoValor().ToString();
          if ((ddlSuperior.SelectedValue != "") && (ddlSuperior.SelectedValue != null))
          {
              objItemConfiguracaoTipo.Superior.Valor = ddlSuperior.SelectedValue;
          }
          objItemConfiguracaoTipo.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
          objItemConfiguracaoTipo.Prefixo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtPrefixo.Text);
          if ((ddlStatus.SelectedValue != "") && (ddlStatus.SelectedValue != null))
          {
              objItemConfiguracaoTipo.Status.Valor = ddlStatus.SelectedValue;
          }

          if (objItemConfiguracaoTipo.insere(out strMensagem))
          {
              //Atualizando o valor na tabela identificador
              objIdentificador.atualizaValor();

              //Atualizando a chave do Tipo do Item de Configuracao
              objItemConfiguracaoTipo.atualizaChave();
              objItemConfiguracaoTipo.altera(out strMensagemAltera);

              //Inserindo os atributos do Tipo do Item de Configuração
              for (int i = 0; i < cblAtributo.Items.Count; i++)
              {
                  if (cblAtributo.Items[i].Selected)
                  {
                      objItemConfiguracaoTipo.AtributoLista = cblAtributo.Items[i].Value;
                  }
              }
              objItemConfiguracaoTipo.insereRelacaoAtributo();

              //Informando a mensagem do status da operação
              Session["strStatus"] = "Tipo do Item de Configuração inserido com sucesso.";

              Response.Redirect("itemconfiguracaotipo.aspx?codigo=" + objItemConfiguracaoTipo.Codigo.Valor);

          }
          else
          {
              //Exibindo mensagem com o status da operação
              lblMensagem.Text = strMensagem;
              imgIcone.ImageUrl = "images/icones/aviso.gif";
              lblMensagem.Visible = true;
              divMensagem.Visible = true;
          }

          objItemConfiguracaoTipo = null;
          objIdentificador = null;

      }
      catch (Exception ex)
      {
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }
  #endregion

  #region metodo alteraItemConfiguracaoTipo
  /// <summary>
  /// Método que altera novo Tipo do Item de Configuração
  /// </summary>
  private void alteraItemConfiguracaoTipo(int intCodigoItem)
  {
      try
      {
          String strMensagem = String.Empty;

          ServiceDesk.Negocio.ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ServiceDesk.Negocio.ClsItemConfiguracaoTipo();

          objItemConfiguracaoTipo.Superior.Valor = ddlSuperior.SelectedValue;
          objItemConfiguracaoTipo.Codigo.Valor = intCodigoItem.ToString();
          objItemConfiguracaoTipo.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
          objItemConfiguracaoTipo.Prefixo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtPrefixo.Text);
          objItemConfiguracaoTipo.Status.Valor = ddlStatus.SelectedValue;

          objItemConfiguracaoTipo.atualizaChave();
          if (objItemConfiguracaoTipo.altera(out strMensagem))
          {
              //Excluindo os atributos do Tipo do Item de Configuração
              objItemConfiguracaoTipo.excluiRelacaoAtributo(intCodigoItem);

              ServiceDesk.Negocio.ClsItemConfiguracaoTipo.atualizaChaveFilhos(intCodigoItem);

              //Inserindo os atributos do Tipo do Item de Configuração
              for (int i = 0; i < cblAtributo.Items.Count; i++)
              {
                  if (cblAtributo.Items[i].Selected)
                  {
                      objItemConfiguracaoTipo.AtributoLista = cblAtributo.Items[i].Value;
                  }
              }
              objItemConfiguracaoTipo.insereRelacaoAtributo();

              strMensagem = "Tipo do Item de Configuração alterado com sucesso.";

          }

          //Exibindo mensagem com o status da operação
          lblMensagem.Text = strMensagem;
          imgIcone.ImageUrl = "images/icones/aviso.gif";
          lblMensagem.Visible = true;
          divMensagem.Visible = true;

          objItemConfiguracaoTipo = null;

      }
      catch (Exception ex)
      {
          ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
      }
  }
  #endregion

  #region metodo populaNoRaiz
  /// <summary>
  /// Método que popula os nós que não possuem pai
  /// </summary>
  public void populaNoRaiz()
  {
    try
    {
      String strSql = String.Empty;

      strSql = "SELECT ic_tipo_codigo, ic_tipo_codigo_superior, descricao";
      strSql += ", (SELECT count(*) FROM ICTipo WHERE ic_tipo_codigo_superior = itemTipo.ic_tipo_codigo) pai";
      strSql += " FROM ICTipo itemTipo";
      strSql += " WHERE ic_tipo_codigo_superior is null";
      strSql += " ORDER BY descricao";

      System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

      trvTipo.Nodes.Clear();

      populaNos(objDataReader, trvTipo.Nodes);

      objDataReader.Dispose();
      objDataReader = null;

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
  public void populaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection)
  {
    try
    {
      while (objDataReader.Read())
      {
        TreeNode objTreeNode = new TreeNode();
        objTreeNode.Text = "<a class=\"menu\" href=\"itemconfiguracaotipo.aspx?codigo=" + objDataReader["ic_tipo_codigo"].ToString() + "\">" + objDataReader["descricao"].ToString() + "</a>";
        objTreeNode.Value = objDataReader["ic_tipo_codigo"].ToString().Trim();
        if (Convert.ToInt32(objDataReader["pai"]) > 0)
        {
          objTreeNode.PopulateOnDemand = true;
        }
        objTreeNodeCollection.Add(objTreeNode);
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
  public void populaSubNivel(int intCodigoPai, TreeNode objTreeNodePai)
  {
    try
    {
      String strSql = String.Empty;

      strSql = "SELECT ic_tipo_codigo, ic_tipo_codigo_superior, descricao";
      strSql += ", (SELECT count(*) FROM ICTipo WHERE ic_tipo_codigo_superior = itemTipo.ic_tipo_codigo) pai";
      strSql += " FROM ICTipo itemTipo";
      strSql += " WHERE ic_tipo_codigo_superior = " + intCodigoPai.ToString();
      strSql += " ORDER BY descricao";

      System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

      populaNos(objDataReader, objTreeNodePai.ChildNodes);

      objDataReader.Dispose();
      objDataReader = null;

    }
    catch (Exception ex)
    {
        ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
    }
  }
  #endregion

  #region evento trvTipo_TreeNodePopulate
  /// <summary>
  /// Evento que ocorre quando um no da tree view é criado
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void trvTipo_TreeNodePopulate(object sender, TreeNodeEventArgs e)
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

  #region evento btnExclui_Click
  /// <summary>
  /// Exlui um tipo do item de configuração
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void btnExclui_Click(object sender, EventArgs e)
  {
    try
    {
      String strCodigoItem = String.Empty;
      String strMensagem = String.Empty;

      if (Request.QueryString["codigo"] != null)
      {
        strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
        ServiceDesk.Negocio.ClsItemConfiguracaoTipo objTipo = new ServiceDesk.Negocio.ClsItemConfiguracaoTipo(Convert.ToInt32(strCodigoItem));
        try
        {
            if (objTipo.exclui(out strMensagem) == true)strMensagem = "Tipo do Item de Configuração excluído com sucesso";
        }
        catch
        {
          strMensagem = "Não foi possível excluir o tipo do Item de Configuração. Existe Item de Configuração associado ao tipo.";
        }
        objTipo = null;

        lblMensagem.Text = strMensagem;
        imgIcone.ImageUrl = "images/icones/aviso.gif";
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

  #region evento btnNovo_Click
  /// <summary>
  /// Permite que o usuário entre com os dados de um novo tipo de Item de Configuração
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void btnNovo_Click(object sender, EventArgs e)
  {
    Response.Redirect(Page.Request.FilePath);
  }
  #endregion

}