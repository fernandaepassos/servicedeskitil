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

public partial class Chamado : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(2);

        if (!Page.IsPostBack)
        {
            this.mvwAbas.ActiveViewIndex = 0;
        }
    }

    protected void lkbHistorico_Click(object sender, EventArgs e)
    {
        this.mvwAbas.ActiveViewIndex = 0;
    }
    protected void lkbAnexos_Click(object sender, EventArgs e)
    {
        this.mvwAbas.ActiveViewIndex = 1;
    }

    protected void cbkAgendar_CheckedChanged(object sender, EventArgs e)
    {
        if (!this.chkAgendar.Checked)
        {
            //this.dpkDataAgendamento.Enabled = false;
            this.tpHoraAgendamento.Enabled = false;
            //this.dpkDataAgendamento.Visible = false;
            this.tpHoraAgendamento.Visible = false;
            this.lblDataAgendamento.Visible = false;
            this.lblHoraAgendamento.Visible = false;
        }
        else
        {
            //this.dpkDataAgendamento.Enabled = true;
            this.tpHoraAgendamento.Enabled = true;
            //this.dpkDataAgendamento.Visible = true;
            this.tpHoraAgendamento.Visible = true;
            this.lblDataAgendamento.Visible = true;
            this.lblHoraAgendamento.Visible = true;
        }
    }
}
