/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Web User Control que possibilidade controle das atividades que uma solução apresenta.
  
  	Data: 06/12/2005
  	Autor: Fernanda Passos
  	Descrição: Tela de pesquisa das soluções.
  
  • Alterações
  	Data:
  	Autor:
  	Descrição: 
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
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

public partial class WUCSolucao : System.Web.UI.UserControl
{
    #region Declarações
    ServiceDesk.Projeto.ClsProjeto objProjeto = new ServiceDesk.Projeto.ClsProjeto();
    ServiceDesk.Negocio.ClsSolucaoProjeto objSolucaoProjeto = new ServiceDesk.Negocio.ClsSolucaoProjeto();
    private string strTabela;
    private int intTabelaIdentificador;
    private int intCodigoProjeto = 0;
    private int intCodigoSolucaoProjeto;
    #endregion

    #region Propriedades

    /// <summary>
    /// Nome da tabela que presenta o processo.
    /// </summary>
    public string Tabela
    {
        get { return strTabela; }
        set { this.strTabela = value; }
    }

    /// <summary>
    /// String identificador na tabela que representa o processo.
    /// </summary>
    public int TabelaIdentificador
    {
        get { return intTabelaIdentificador; }
        set { this.intTabelaIdentificador = value; }
    }

    /// <summary>
    /// Código do projeto
    /// </summary>
    public  int CodigoProjeto
    {
        get { return intCodigoProjeto; }
        set { this.intCodigoProjeto = value; }
    }

    /// <summary>
    /// Código da SoluçãoProjeto
    /// </summary>
    public  int CodigoSolucaoProjeto
    {
        get { return intCodigoSolucaoProjeto; }
        set { this.intCodigoSolucaoProjeto = value; }
    }

    #endregion

    #region Eventos
    protected void btnNovoProjeto_Click(object sender, EventArgs e)
    {
        Novo();
    }

    #region Page_Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (txtCodigoProjetoPai.Text != string.Empty)
        {
            if (ServiceDesk.Projeto.ClsProjeto.VerificaSeFinalizado(txtCodigoProjetoPai.Text.Trim()) == true)
            {
                Label3.Text = "Solução Finalizada";
                Label3.Visible = true;
            }
            else
                Label3.Visible = false;
        }
    }
    #endregion

    #region Salvar
    /// <summary>
    /// Salvar solução
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            Salvar();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Excluir
    /// <summary>
    /// Excluir
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            Excluir();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Novo
    /// <summary>
    /// Limpa campos para inserção de novo registro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            Novo();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
         }
    }
    #endregion

    #region Treeview Projeto - SelectedNodeChanged
    /// <summary>
    /// Treeview Projeto - SelectedNodeChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvProjeto_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            PreecheCampos(objProjeto.GetDadosProjetoPorParamatro(Convert.ToInt32(tvProjeto.SelectedValue)), false,string .Empty ,string .Empty );
            tvProjeto.SelectedNodeStyle.BackColor = System.Drawing.Color.LightGray;
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region TreeView Projeto - TreeNodePopulate
    /// <summary>
    /// TreeView Projeto - TreeNodePopulate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvProjeto_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            ServiceDesk.Projeto.ClsProjeto.PopulaNoz(Convert.ToInt32(e.Node.Value), null, e.Node, false);
            tvProjeto.ExpandAll();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Projeto novo - NovaSolucao_Click
    /// <summary>
    /// Projeto novo - NovaSolucao_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovaSolucao_Click(object sender, EventArgs e)
    {
        try
        {
            Novo();
            tvProjeto.Nodes.Clear();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    protected void txtFimPrevisto_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnFinalizarSolucao_Click(object sender, EventArgs e)
    {
        if (ServiceDesk.Projeto.ClsProjeto.FinalizaProjeto(txtCodigoProjetoPai.Text.Trim()) == true)
        {
            Label3.Visible = true;
            Label3.Text = "Solução Finalizada";
        }
        else
            Label3.Visible = false;
    }
    #endregion

    #region Métodos

    #region Salvar projeto
    /// <summary>
    /// Salvar projeto
    /// </summary>
    public void Salvar()
    {
        try
        {
            string strMensagem = string.Empty;
            divMensagem.Visible = false;
            
                #region Valida as datas
            if (txtInicioPrevisto.Text.Trim () != string.Empty  && txtFimPrevisto.Text.Trim () != string.Empty)
            {
              try
              {
                if (Convert.ToDateTime(txtInicioPrevisto.Text.Trim()) > Convert.ToDateTime(txtFimPrevisto.Text.Trim()))
                {
                  lblMensagem.Text = "A data de inicio prevista não pode ser maior que a data fim prevista.";
                  imgIcone.ImageUrl = "images/icones/info.gif";
                  divMensagem.Visible = true;
                  return;
                }
              }
              catch 
              {
                lblMensagem.Text = "Por favor, preencha as datas corretamente.";
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                return;
              }
            }
            #endregion

            int intCodigoProjeto = 0;
            if (txtTabela.Text != string.Empty && txtTabelaIdentificador.Text != string.Empty && txtCodigoSolucao.Text == string.Empty)
            {
                #region Insere Novo Projeto
                if (txtCodigoProjetoSuperior.Text.Trim() == string.Empty && txtCodigoProjeto.Text.Trim() == string.Empty)
                {
                    objProjeto.Codigo.Valor = objProjeto.GetMaxId().ToString();
                    objProjeto.CodigoSuperior.Valor = "0";
                    objProjeto.Nome.Valor = txtNome.Text.Trim();
                    objProjeto.Observacao.Valor = txtObservacao.Text.Trim();
                    objProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                    objProjeto.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();

                    if (txtInicioPrevisto.Text.Trim() != string.Empty)
                      objProjeto.DataInicioPrevista.Valor = Convert.ToDateTime(txtInicioPrevisto.Text.Trim()).ToString(ClsParametro.DataInclusao);

                    if (txtFimPrevisto.Text.Trim() != string.Empty)
                      objProjeto.DataFimPrevista.Valor = Convert.ToDateTime(txtFimPrevisto.Text.Trim ()).ToString(ClsParametro.DataInclusao);

                    if (objProjeto.insere(out strMensagem) == false)
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                    else
                    {
                      txtCodigoProjetoPai.Text = objProjeto.Codigo.Valor; 
                      //Insere solução
                        objSolucaoProjeto.Codigo.Valor  = objSolucaoProjeto.GetMaxId().ToString();
                        objSolucaoProjeto.Tabela.Valor = txtTabela.Text.Trim();
                        objSolucaoProjeto.CodigoIdentificadorTabela.Valor = txtTabelaIdentificador.Text.Trim();
                        objSolucaoProjeto.CodigoProjeto.Valor = objProjeto.Codigo.Valor;
                        if (objSolucaoProjeto.insere(out strMensagem) == true) 
                          txtCodigoSolucao.Text = objSolucaoProjeto.Codigo.Valor;

                         //Insere o responsável
                        if (dlResponsavel.SelectedValue.ToString() != string.Empty)
                        {
                            ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ServiceDesk.Projeto.ClsProjetoPessoa();

                            objProjetoPessoa.CodigoPessoa.Valor = dlResponsavel.SelectedValue.ToString();
                            objProjetoPessoa.CodigoProjeto.Valor = objProjeto.Codigo.Valor;
                            objProjetoPessoa.insere(out strMensagem);
                            objProjetoPessoa = null ;
                        }
                        //<<<< Fim insere responsavel
                    }
                }
                #endregion
            }
            else if (txtCodigoSolucao.Text.Trim() != string.Empty && objSolucaoProjeto.VerificaSeExisteProjetoParaSolucao(Convert.ToInt32(txtCodigoSolucao.Text.Trim()), out intCodigoProjeto) == false)
            {
                #region Insere novo projeto
                if (txtCodigoProjetoSuperior.Text.Trim() == string.Empty && txtCodigoProjeto.Text.Trim ()  == string.Empty)
                {
                    objProjeto.Codigo.Valor = objProjeto.GetMaxId().ToString();
                    objProjeto.CodigoSuperior.Valor = "0";
                    objProjeto.Nome.Valor = txtNome.Text.Trim();
                    objProjeto.Observacao.Valor = txtObservacao.Text.Trim();
                    objProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                    objProjeto.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();
                    if (txtInicioPrevisto.Text != string.Empty)
                        objProjeto.DataInicioPrevista.Valor = Convert.ToDateTime(txtInicioPrevisto.Text.Trim()).ToString(ClsParametro.DataInclusao);

                    if (txtFimPrevisto.Text != string.Empty)
                        objProjeto.DataFimPrevista.Valor = Convert.ToDateTime(txtFimPrevisto.Text.Trim()).ToString(ClsParametro.DataInclusao);

                    if (objProjeto.insere(out strMensagem) == false)
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                    else
                    {
                        txtCodigoProjetoPai.Text = objProjeto.Codigo.Valor;

                        //Atualiza tabela SolucaoProjeto com o código do projeto.
                        objProjeto.UpdateCodigoProjetoSolucao(Convert.ToInt32(objProjeto.Codigo.Valor), Convert.ToInt32(txtCodigoSolucao.Text .Trim () ));

                        //Se foi informado o responsável atualiza responsável.
                        if (dlResponsavel.SelectedValue.ToString() != string.Empty)
                        {
                            ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ServiceDesk.Projeto.ClsProjetoPessoa();

                            objProjetoPessoa.CodigoPessoa.Valor = dlResponsavel.SelectedValue.ToString();
                            objProjetoPessoa.CodigoProjeto.Valor = objProjeto.Codigo.Valor;

                            if (objProjetoPessoa.insere(out strMensagem) == false)
                            {
                                lblMensagem.Text = strMensagem;
                                imgIcone.ImageUrl = "images/icones/aviso.gif";
                                divMensagem.Visible = true;
                            }
                            else
                            {
                                divMensagem.Visible = false;
                                int intCodPai = 0;
                                if (objProjeto.VerificaSeFilho(Convert.ToInt32(objProjeto.Codigo.Valor), out intCodPai) == true)
                                    txtCodigoProjetoSuperior.Text = intCodPai.ToString();
                                txtCodigoProjeto.Text = objProjeto.Codigo.Valor;
                            }
                        }
                        //<<<< Fim insere responsavel
                    }
                }
                #endregion
            }
            else if (txtCodigoProjetoSuperior.Text.Trim() == string.Empty && txtCodigoProjeto.Text.Trim() == string .Empty )
            {
                #region Insere atividade filha
                if (tvProjeto.SelectedNode == null && txtCodigoProjetoSuperior.Text == string.Empty)
                {
                    lblMensagem.Text = "Por favor, selecione o item no qual deseja inserir uma atividade.";
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                    return;
                }

                objProjeto.Codigo.Valor = objProjeto.GetMaxId().ToString();
                if (tvProjeto.SelectedNode != null) { objProjeto.CodigoSuperior.Valor = tvProjeto.SelectedNode.Value; }
                else if (txtCodigoProjetoSuperior.Text != string.Empty) { objProjeto.CodigoSuperior.Valor = txtCodigoProjetoSuperior.Text.Trim(); }

                objProjeto.Observacao.Valor = txtObservacao.Text.Trim();
                objProjeto.Nome.Valor = txtNome.Text.Trim();
                objProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objProjeto.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();

                if (txtInicioPrevisto.Text.Trim() != string.Empty)
                    objProjeto.DataInicioPrevista.Valor = Convert.ToDateTime(txtInicioPrevisto.Text.Trim()).ToString(ClsParametro.DataInclusao);

                if (txtFimPrevisto.Text.Trim() != string.Empty)
                    objProjeto.DataFimPrevista.Valor = Convert.ToDateTime(txtFimPrevisto.Text.Trim()).ToString(ClsParametro.DataInclusao);

                if (objProjeto.insere(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
                else
                {
                    //Se foi informado o responsável atualiza responsável.
                    if (dlResponsavel.SelectedValue.ToString() != string.Empty)
                    {
                        ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ServiceDesk.Projeto.ClsProjetoPessoa();

                        objProjetoPessoa.CodigoPessoa.Valor = dlResponsavel.SelectedValue.ToString();
                        objProjetoPessoa.CodigoProjeto.Valor = objProjeto.Codigo.Valor;

                        if (objProjetoPessoa.insere(out strMensagem) == false)
                        {
                            lblMensagem.Text = strMensagem;
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                        else
                        {
                            divMensagem.Visible = false;
                            int intCodPai = 0;
                            if (objProjeto.VerificaSeFilho(Convert.ToInt32(objProjeto.Codigo.Valor), out intCodPai) == true)
                                txtCodigoProjetoSuperior.Text = intCodPai.ToString();
                            txtCodigoProjeto.Text = objProjeto.Codigo.Valor;
                        }
                    }
                    //<<<< Fim insere responsavel
                }
                #endregion
            }
            else if (txtCodigoProjeto.Text .Trim ()  != string .Empty )
            {
                #region Altera uma atividade
                objProjeto.Codigo.Valor = txtCodigoProjeto.Text;
                objProjeto.Observacao.Valor = txtObservacao.Text.Trim();
                if (txtCodigoProjetoSuperior.Text.Trim() == string.Empty)
                    objProjeto.CodigoSuperior.Valor = "0";
                else objProjeto.CodigoSuperior.Valor = txtCodigoProjetoSuperior.Text.Trim();
                objProjeto.Nome.Valor = txtNome.Text.Trim();
                objProjeto.DataAlteracao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objProjeto.CodigoPessoaAlterador.Valor = ClsUsuario.getCodigoUsuario().ToString();

                if (txtInicioPrevisto.Text.Trim() != string.Empty)
                    objProjeto.DataInicioPrevista.Valor = Convert.ToDateTime(txtInicioPrevisto.Text.Trim()).ToString(ClsParametro.DataInclusao);

                if (txtFimPrevisto.Text.Trim() != string.Empty)
                    objProjeto.DataFimPrevista.Valor = Convert.ToDateTime(txtFimPrevisto.Text.Trim()).ToString(ClsParametro.DataInclusao);

                if (objProjeto.altera(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
                else
                {
                    //Altera o responsável.
                    ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ServiceDesk.Projeto.ClsProjetoPessoa();
                    objProjetoPessoa.CodigoPessoa.Valor = dlResponsavel.SelectedValue.ToString();
                    objProjetoPessoa.CodigoProjeto.Valor = objProjeto.Codigo.Valor;
                    //Verifica se já existe resp.
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.retornaValorCampo("ProjetoPessoa", "projeto_codigo", "projeto_codigo = " + Convert.ToInt32(objProjeto.Codigo.Valor.Trim()) + "") != string.Empty)
                        objProjetoPessoa.altera();
                    else
                        objProjetoPessoa.insere(out strMensagem);

                    divMensagem.Visible = false;
                    int intCodPai = 0;
                    if (objProjeto.VerificaSeFilho(Convert.ToInt32(objProjeto.Codigo.Valor), out intCodPai) == true)
                        txtCodigoProjetoSuperior.Text = intCodPai.ToString();
                    txtCodigoProjeto.Text = objProjeto.Codigo.Valor;
                }
                #endregion
            }

            if (txtCodigoProjetoPai.Text.Trim() != string.Empty) 
            {
              ServiceDesk.Projeto.ClsProjeto.populaNoRaiz(Convert.ToInt32(txtCodigoProjetoPai.Text.Trim()),tvProjeto);
              tvProjeto.ExpandAll();
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion 

    #region Excluir
    /// <summary>
    /// Excluir projeto
    /// </summary>
    public void Excluir()
    {
        try
        { 
            ///Verifica se foi informado um projeto ou atividade.
            if (tvProjeto .SelectedValue.ToString() == string .Empty )
            {
                lblMensagem.Text = "Por favor, selecione um item para excluir.";
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                return;
            }

            string strMensagem = string.Empty;

            objProjeto.Codigo.Valor = tvProjeto.SelectedValue.ToString();
            if (objProjeto.exclui(out strMensagem) == false)
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else
            {
                ServiceDesk.Negocio.ClsSolucaoProjeto objSolucaoProjeto = new ServiceDesk.Negocio.ClsSolucaoProjeto();
                objSolucaoProjeto.Codigo.Valor = tvProjeto.SelectedValue.ToString().Trim();
                objSolucaoProjeto.exclui(out strMensagem);

                if (txtCodigoProjetoPai.Text.Trim() != string.Empty)
                {
                  ServiceDesk.Projeto.ClsProjeto.populaNoRaiz(Convert.ToInt32(txtCodigoProjetoPai.Text.Trim()), tvProjeto);
                  tvProjeto.ExpandAll();
                }
                Novo();
                divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Novo
    /// <summary>
    /// Excluir projeto
    /// </summary>
    public void Novo()
    {
        try
        {
            txtNome.Text = string.Empty;
            dlResponsavel.SelectedIndex = -1;
            txtFimPrevisto.Text = string.Empty;
            txtInicioPrevisto.Text = string.Empty; 
            txtObservacao.Text = string.Empty;
            txtCodigoProjeto.Text = string.Empty;
            txtCodigoProjetoSuperior.Text = string.Empty;
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Preenche campos
    /// <summary>
    /// Preenche campos
    /// </summary>
    /// <param name="bolPai">Indica se os dados que seram preenchidos é do pai</param> 
    /// <param name="objReader">Data Reader com os dados a serem preenchidos.</param> 
    public void PreecheCampos(System.Data.SqlClient.SqlDataReader objReader, bool bolPai, string strTabela, string strTabelaIdentificador)
    {
        try
        {
          if (strTabela != string.Empty && strTabelaIdentificador != string.Empty)
            {
                txtTabela.Text = strTabela.Trim();
                txtTabelaIdentificador.Text = strTabelaIdentificador.Trim();
                return;
            }

            if (objReader == null) return;

            if (objReader.Read())
            {
                if (strTabela != string .Empty && strTabelaIdentificador != string.Empty)
                {
                    txtTabela.Text = strTabela.Trim();
                    txtTabelaIdentificador.Text = strTabelaIdentificador.Trim();
                    return;
                }

                txtCodigoProjetoSuperior.Text = objReader["projeto_codigo_superior"].ToString();
                txtCodigoProjeto.Text = objReader["projeto_codigo"].ToString();
                txtNome.Text = objReader["nome"].ToString();
                dlResponsavel.SelectedValue = objReader["pessoa_codigo"].ToString();

                if (objReader["data_inicio_prevista"].ToString() != string.Empty)
                    txtInicioPrevisto.Text = Convert.ToDateTime(objReader["data_inicio_prevista"].ToString()).ToString(ClsParametro.DataInclusao);
                else
                  txtInicioPrevisto.Text = string.Empty;

                if (objReader["data_fim_prevista"].ToString() != string.Empty)
                    txtFimPrevisto.Text = Convert.ToDateTime(objReader["data_fim_prevista"].ToString()).ToString(ClsParametro.DataInclusao);
                else
                  txtFimPrevisto.Text = string.Empty; 

                   txtObservacao.Text = objReader["observacao"].ToString();
            }
            objReader.Dispose();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Preenche TreeView
    /// <summary>
    /// Preenche TreeView
    /// </summary>
    /// <param name="intCodigoProjeto">Código do projeto</param>
    public void PreencheTreeView(int intCodigoProjeto)
    {
        try
        {
            txtCodigoProjetoPai.Text = intCodigoProjeto.ToString().Trim();
            ServiceDesk.Projeto.ClsProjeto.populaNoRaiz(intCodigoProjeto, tvProjeto);
            tvProjeto.ExpandAll();
        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Preenche Drop Dowm List
    /// <summary>
    /// Preenche Drop Dowm List
    /// </summary>
    public void PreencheDropDownList()
    {
        try
        {
            dlResponsavel.Items.Clear();
            ClsUsuario.geraDropDownList(dlResponsavel, "Selecione o responsável");
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion
    #endregion

}
