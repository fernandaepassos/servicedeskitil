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

public partial class inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            OboutInc.SlideMenu.SlideMenu menu = new OboutInc.SlideMenu.SlideMenu();

            menu.ScriptPath = "obout/smscript";
            menu.StyleFolder = "obout/smscript";

            menu.AllExpanded = true;

            menu.AddParent("1", "Funções mais acessadas");
            menu.AddChild("1_1", "Cockpit", "Cockpit2.aspx");
            menu.AddChild("1_2", "Semáforo", "semaforo.aspx");
            menu.AddChild("1_3", "Meus chamados", "meus_chamados.aspx");
            menu.AddChild("1_4", "Meus Incidentes", "meus_incidentes.aspx");
            menu.AddChild("1_5", "Minhas Requisições de Serviço", "MinhasRequisicoesServico.aspx");
            menu.AddChild("1_6", "Minhas Requisições de Mudança", "MinhasRequisicoesMudanca.aspx");

            menu.AddParent("2", "Meu menu");
            menu.AddChild("2_1", "Notificações", "notificacao.aspx");
            menu.AddChild("2_2", "Base Conhecimento", "BaseConhecimento.aspx");
            menu.AddChild("2_3", "Segurança", "PessoaPerfilEstrutura.aspx");
            menu.AddChild("2_4", "Parametro", "Parametro.aspx");

            menu.AddParent("3", "Configurações pessoais");
            menu.AddChild("3_1", "Notificações", "Notificacao.aspx");
            menu.AddChild("3_2", "Pessoas", "Pessoa.aspx");
            menu.AddChild("3_3", "Segurança", "PessoaPerfilEstrutura.aspx");

            phMenu.Controls.Add(menu);

            menu = null;

            //Sylvio - se possui Notificação pendente de resposta redireciona para
            //a pagina de notificações.
            if (ClsNotificacao.existeNotificacaoPendente(ClsUsuario.getCodigoUsuario().ToString()))
            {
                Response.Redirect("Notificacao.aspx", false);
            }
            else
            {
                if (ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString() == ClsParametro.CodigoUsuarioFinal)
                {
                    if (ClsParametro.RedirecionamentoAberturaChamado != string.Empty)
                        Response.Redirect(ClsParametro.RedirecionamentoAberturaChamado);
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}
