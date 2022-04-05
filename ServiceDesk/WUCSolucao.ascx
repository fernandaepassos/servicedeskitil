<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCSolucao.ascx.cs" Inherits="WUCSolucao" %>
<%@ Register Assembly="obout_Calendar_Pro_Net" Namespace="OboutInc.Calendar" TagPrefix="obout" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<table width="776" height="520" border="0" cellpadding="0" cellspacing="0" class="tabela_padrao">
  <tr>
    <td align="left" valign="top">
	<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td align="center">
            <div id="divMensagem" runat="server" align="center" class="Mensagem" style="width: 99%; height: 16px;"
                visible="false">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="center" valign="bottom" width="60">
                            <asp:Image ID="imgIcone" runat="server" /></td>
                        <td align="center" valign="bottom">
                            <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top">
<table width="100%">
    <tr>
        <td>
            <table width="100%">
                <tr>
                    <td align="left" width="91">
                    </td>
                    <td colspan="3" align="center">
                        <asp:Label ID="Label3" runat="server" Font-Names="Trebuchet MS" Font-Size="Larger"
                            ForeColor="#1D6E6F" Visible="False"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" width="91">
                        <asp:Label ID="Label1" runat="server" Text="Nome: "></asp:Label></td>
                    <td colspan="3">
<asp:TextBox ID="txtNome" runat="server" Width="680px" CssClass="campo_texto"></asp:TextBox></td>
                </tr>
              <tr>
                <td align="left" style="height: 61px">
                  <asp:Label ID="Label5" runat="server" Text="Descrição: "></asp:Label></td>
                <td colspan="3" style="height: 61px">
                  <asp:TextBox ID="txtObservacao" runat="server" TextMode="MultiLine" Width="680px" CssClass="campo_texto" Height="87px"></asp:TextBox></td>
              </tr>
                <tr>
                    <td align="left">
<asp:Label ID="Label2" runat="server" Text="Responsável: "></asp:Label></td>
                    <td width="165">
                        <asp:DropDownList ID="dlResponsavel" runat="server" Width="165px">
                        </asp:DropDownList>&nbsp;<asp:TextBox ID="txtCodigoProjeto" runat="server" Width="11px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtTabela" runat="server" Width="5px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtCodigoProjetoSuperior" runat="server" Visible="False" Width="5px"></asp:TextBox>
                        <asp:TextBox ID="txtCodigoSolucao" runat="server" Width="5px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtTabelaIdentificador" runat="server" Visible="False" Width="5px"></asp:TextBox><asp:TextBox
                          ID="txtCodigoProjetoPai" runat="server" Width="5px" Visible="False"></asp:TextBox></td>
                    <td width="88" align="left" valign="middle"><asp:Label ID="Label6" runat="server" Text="Início previsto: "></asp:Label></td>
                    <td align="left" valign="middle">
					<asp:TextBox ID="txtInicioPrevisto" runat="server" Width="60px" CssClass="campo_texto"></asp:TextBox>&nbsp;<obout:Calendar ID="CalendarioInicioPrevisto" runat="server" DateFormat="dd/mm/yyyy"
                        DatePickerButtonText='<span style="padding:1px; border:1px solid #3C8D8E; color: #333333; background-color:  #E9F5F5; font:10px Arial; cursor:hand; cursor:pointer;">. . .</span>'
                        DatePickerMode="True" DoubleCalendarMode="False" NamesDays="Dom,  Seg, Ter, Qua, Qui, Sex, Sáb"
                        NamesMonths="Janeiro, Fevereiro,Março, Abril, Maio, Junho, Julho,Agosto, Setembro, Outubro, Novembro, Dezembro"
                        ScriptPath="obout/calendarscript" ShowOtherMonthDays="False" ShowYearSelector="False"
                        StyleFolder="obout/calendarscript" TextBoxId="txtInicioPrevisto" WidthOfMonthContainer="150">
                  </obout:Calendar>
                  &nbsp;
                  <asp:Label ID="Label7" runat="server" Text="Início previsto: "></asp:Label>
                  <asp:TextBox ID="txtFimPrevisto" runat="server" Width="60px" OnTextChanged="txtFimPrevisto_TextChanged" CssClass="campo_texto"></asp:TextBox><obout:Calendar ID="CalendarioFimPrevisto" runat="server" DateFormat="dd/mm/yyyy"
                        DatePickerButtonText='<span style="padding:1px; border:1px solid #3C8D8E; color: #333333; background-color:  #E9F5F5; font:10px Arial; cursor:hand; cursor:pointer;">. . .</span>'
                        DatePickerMode="True" DoubleCalendarMode="False" NamesDays="Dom,  Seg, Ter, Qua, Qui, Sex, Sáb"
                        NamesMonths="Janeiro, Fevereiro,Março, Abril, Maio, Junho, Julho,Agosto, Setembro, Outubro, Novembro, Dezembro"
                        ScriptPath="obout/calendarscript" ShowOtherMonthDays="False" ShowYearSelector="False"
                        StyleFolder="obout/calendarscript" TextBoxId="txtFimPrevisto" WidthOfMonthContainer="150">
                  </obout:Calendar>					</td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="center">
                        &nbsp;<asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="50px" OnClick="btnSalvar_Click" CssClass="botao" />
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" Width="50px" OnClick="btnExcluir_Click" CssClass="botao" />
                        <asp:Button ID="btnNovo" runat="server" Text="Novo item" Width="72px" OnClick="btnNovo_Click" CssClass="botao" />&nbsp;<asp:Button
                            ID="btnFinalizarSolucao" runat="server" Text="Finalizar" CssClass="botao" OnClick="btnFinalizarSolucao_Click" ToolTip="Finalizar solução" /></td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" Height="360px" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                            <asp:TreeView ID="tvProjeto" runat="server" OnSelectedNodeChanged="tvProjeto_SelectedNodeChanged" OnTreeNodePopulate="tvProjeto_TreeNodePopulate" CssClass="menu">
                                <ParentNodeStyle CssClass="menu" />
                                <SelectedNodeStyle CssClass="menu" />
                                <RootNodeStyle CssClass="menu" />
                                <NodeStyle CssClass="menu" />
                                <LeafNodeStyle CssClass="menu" />
                                <HoverNodeStyle CssClass="menu" />
                            </asp:TreeView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
        </td>
    </tr>
</table>	</td>
  </tr>
</table>