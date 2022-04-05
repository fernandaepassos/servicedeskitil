<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Auditoria.aspx.cs" Inherits="Auditoria" %>
<%@ Register Assembly="skmMenu" Namespace="skmMenu" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<head id="Head1" runat="server">
    <title>Help Desk</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>

<script language="javascript">
<!--
function verifica(strItem) {
	if (confirm("Deseja mesmo excluir " + strItem + "?")) {
		return true;
	}
	else {
		return false;
	}
}
-->
</script>

<body leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0">
    <div>
        <table width="100%" cellpadding="0" cellspacing="0">           
            <tr>
              <td align="center" valign="top" colspan="2">
                    <div id="divMensagem" style="width: 100%;" runat="server" class="Mensagem" visible="true">
					<table width="776" border="0" cellspacing="5" cellpadding="0">
  <tr>
    <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
    <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
  </tr>
</table>    
</div>
		                
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="20">&nbsp;</td>
                      </tr>
                      <tr>
                        <td align="center" valign="top"><table width="97%"  border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaPadrao">
                                <!--DWLayoutTable-->
                                <tr>
                                  <td height="22" colspan="3">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaCabecalho">
                                      <!--DWLayoutTable-->
                                      <tr>
                                        <td width="8" height="22" class="esq_top">&nbsp;</td>
                                        <td align="left" valign="top" class="centro_top"><table width="100%" height="22"  border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                              <td width="20" align="center" valign="middle">&nbsp;</td>
                                              <td align="left" valign="middle" class="tituloFont">Dados da Auditoria </td>
                                            </tr>
                                        </table></td>
                                        <td width="8" class="dir_top"></td>
                                      </tr>
                                  </table></td>
                                </tr>
                                <tr>
                                  <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td align="center" valign="top"><table width="770" border="0" cellpadding="0" cellspacing="2" >
                                        <tr align="left" valign="middle">
                                          <td width="100">Nome:</td>
                                          <td>
                                            <asp:TextBox ID="txtNome" CssClass="campo_texto" runat="server" Width="274px"></asp:TextBox>
                                          </td>
                                          <td>Situa&ccedil;&atilde;o:</td>
                                          <td>
                                            <asp:DropDownList ID="ddlStatus" Width="270px" CssClass="campo_texto" runat="server"></asp:DropDownList>
                                          </td>
                                        </tr>
                                        <tr align="left" valign="middle">
                                          <td>Data Inicial Prev.: </td>
                                          <td><table width="100%"  border="0" cellpadding="0" cellspacing="0">
                                              <tr align="left" valign="middle">
                                                <td> <ew:CalendarPopup ID="clpDataInicialPrevista" Nullable="true" runat="server" Width="65px"> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> </ew:CalendarPopup> </td>
                                                <td width="90" align="right">Data Final Prev.:</td>
                                                <td> <ew:CalendarPopup ID="clpDataFinalPrevista" Nullable="true" runat="server" Width="65px"> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> </ew:CalendarPopup> </td>
                                              </tr>
                                          </table></td>
                                          <td width="110">Data Inicial Real:</td>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                              <tr align="left" valign="middle">
                                                <td> <ew:CalendarPopup ID="clpDataInicialReal" Nullable="true" runat="server" Width="65px"> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> </ew:CalendarPopup> </td>
                                                <td width="85" align="right">Data Final Real:</td>
                                                <td> <ew:CalendarPopup ID="clpDataFinalReal" Nullable="true" runat="server" Width="65px"> <SelectedDateStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> <MonthHeaderStyle BackColor="Yellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> <WeekdayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <HolidayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <GoToTodayStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <OffMonthStyle BackColor="AntiqueWhite" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Gray" /> <WeekendStyle BackColor="LightGray" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <ClearDateStyle BackColor="White" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <DayHeaderStyle BackColor="Orange" Font-Names="Verdana,Helvetica,Tahoma,Arial" Font-Size="XX-Small"
                                                            ForeColor="Black" /> <TodayDayStyle BackColor="LightGoldenrodYellow" Font-Names="Verdana,Helvetica,Tahoma,Arial"
                                                            Font-Size="XX-Small" ForeColor="Black" /> </ew:CalendarPopup> </td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                        <tr align="left" valign="middle">
                                          <td>Coment&aacute;rio:</td>
                                          <td colspan="3">
                                            <asp:TextBox ID="txtComentario" TextMode="MultiLine" Width="658px" Height="60px" runat="server" CssClass="campo_texto"></asp:TextBox>
                                          </td>
                                        </tr>
                                        <tr align="center" valign="middle">
                                          <td colspan="4">
                                            <asp:Button ID="btnSalvar" CssClass="botao" runat="server" Text="Salvar" OnClick="salvaAuditoria" Width="50px" />                                      
&nbsp;&nbsp;
      <input class="botao" name="btnNovo" type="button" value="Novo" title="Novo" onclick="javascript:window.location.href='auditoria.aspx'" style="width: 50px" size="" /></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                    <tr>
                                      <td><table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" valign="top">
                                            <table cellpadding="0" cellspacing="1" border="0">
                                              <tr>
                                                <td>
                                                  <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                      <td id="aba_esq" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                                      <td class="aba_centro_off" id="aba" runat="server"><asp:LinkButton ID="lkbItemConfiguracao" runat="server" OnClick="mudaAba" CommandArgument="0">Itens de Configura&ccedil;&atilde;o</asp:LinkButton></td>
                                                      <td id="aba_dir" runat="server" class="aba_direita_off">&nbsp;</td>
                                                    </tr>
                                                </table></td>
                                                <td>
                                                  <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                      <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                                      <td class="aba_centro_off" id="Td2" runat="server"><asp:LinkButton ID="lkbAuditor" runat="server" OnClick="mudaAba" CommandArgument="1">Auditores</asp:LinkButton></td>
                                                      <td id="Td3" runat="server" class="aba_direita_off">&nbsp;</td>
                                                    </tr>
                                                </table></td>
                                                <td>
                                                  <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                      <td id="Td4" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                                      <td class="aba_centro_off" id="Td5" runat="server"><asp:LinkButton ID="lkbDocumento" runat="server" OnClick="mudaAba" CommandArgument="2">Documentos</asp:LinkButton></td>
                                                      <td id="Td6" runat="server" class="aba_direita_off">&nbsp;</td>
                                                    </tr>
                                                </table></td>
                                              </tr>
                                            </table>
                                            <div> <asp:MultiView ID="mtvAuditoria" ActiveViewIndex="0" runat="server"> <asp:View ID="vwItemConfiguracao" runat="server">
                                              <asp:Panel ID="pnItemConfiguracao" runat="server" Height="150px" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                                                <div class="menu" style="float: left"> <asp:TreeView ID="trvItemConfiguracaoTipo" ExpandDepth="0" ShowLines="true" ShowExpandCollapse="true" runat="server" OnTreeNodePopulate="populaSubNivel" CssClass="menu"> <ParentNodeStyle CssClass="menu" /> <SelectedNodeStyle CssClass="menu" /> <RootNodeStyle CssClass="menu" /> <NodeStyle CssClass="menu" /> <LeafNodeStyle CssClass="menu" /> <HoverNodeStyle CssClass="menu" /> </asp:TreeView> </div>
                                                <div style="text-align: center"> <asp:GridView ID="gvItemConfiguracao" runat="server" Width="98%" BackColor="White"  AllowPaging="True" PageSize="20" BorderStyle="None" Font-Size="8pt"  CellPadding="4" OnRowCommand="gvItemConfiguracao_OnRowCommand" AutoGenerateColumns="False" OnPageIndexChanging="gvItemConfiguracao_PageIndexChanging" OnRowDataBound="gvItemConfiguracao_RowDataBound" OnRowEditing="editarGrid" OnRowCancelingEdit="cancelarEdicaoGrid" OnRowUpdating="atualizaGrid">
                                                  <HeaderStyle CssClass="topoGrid" />                                      
                                                  <EditRowStyle BackColor="#C8E4E6" /> <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                  <FooterStyle BackColor="#FDF9DC" Font-Bold="True" ForeColor="#47451F" />                                      
                                                  <PagerStyle BackColor="#F0F0F0" ForeColor="#47451F" HorizontalAlign="Center" CssClass="menu"/>                                      
                                                  <Columns>
                                                  <asp:CommandField ButtonType="Image" CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True" UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar">
                                                  <ItemStyle Width="40px" />                                      
                                                  <HeaderStyle HorizontalAlign="Center" Width="40px" />                                      
            </asp:CommandField> <asp:TemplateField HeaderText="&#160;C&#243;digo">
            <ItemTemplate>
              <asp:Label ID="lblCodigo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "auditado_codigo") %>' runat="server"></asp:Label>
              <asp:Label ID="lblCodigoItem" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ic_codigo") %>' runat="server"></asp:Label>
              <asp:Label ID="lblCodigoTipo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ic_tipo_codigo") %>' runat="server"></asp:Label>
              <asp:Label ID="lblPrefixoNumero" runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:Label ID="lblCodigo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "auditado_codigo") %>' runat="server"></asp:Label>
              <asp:Label ID="lblCodigoItem" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ic_codigo") %>' runat="server"></asp:Label>
              <asp:Label ID="lblCodigoTipo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ic_tipo_codigo") %>' runat="server"></asp:Label>
              <asp:Label ID="lblPrefixoNumero" runat="server"></asp:Label>
            </EditItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="80px" />
            <HeaderStyle HorizontalAlign="Center" Width="80px" />
            </asp:TemplateField> <asp:TemplateField HeaderText="&#160;Item de Configura&#231;&#227;o">
            <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "nome")%> </ItemTemplate>
            <HeaderStyle HorizontalAlign="Center" Width="100px" />
            <ItemStyle HorizontalAlign="Center" Width="100px" />
            </asp:TemplateField> <asp:TemplateField HeaderText="&#160;Status">
            <ItemTemplate>
              <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo")%>' runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:Label ID="lblStatus" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo")%>' runat="server"></asp:Label>
              <asp:DropDownList ID="ddlStatus" Width="100" runat="server"></asp:DropDownList>
            </EditItemTemplate>
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField> <asp:TemplateField HeaderText="&#160;Auditor">
            <ItemTemplate>
              <asp:Label ID="lblAuditor" Text='<%# DataBinder.Eval(Container.DataItem, "auditor_codigo")%>' runat="server"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:Label ID="lblAuditor" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "auditor_codigo")%>' runat="server"></asp:Label>
              <asp:DropDownList ID="ddlAuditor" Width="100" runat="server"></asp:DropDownList>
            </EditItemTemplate>
            <HeaderStyle HorizontalAlign="Left" Width="250px" />
            <ItemStyle HorizontalAlign="Left" Width="250px" />
            </asp:TemplateField> <asp:TemplateField HeaderText="&#160;Coment&#225;rio">
            <ItemTemplate>
              <asp:TextBox CssClass="campo_descricao200" ID="lblComentario" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "comentario") %>' ></asp:TextBox>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox CssClass="campo_texto" ID="txtComentario" Text='<%# DataBinder.Eval(Container.DataItem, "comentario")%>' Width="190" runat="server"></asp:TextBox>
            </EditItemTemplate>
            <ItemStyle HorizontalAlign="Left" Width="200px" />
            <HeaderStyle HorizontalAlign="Left" Width="200px" />
            </asp:TemplateField>
                                                  </Columns>
                                                </asp:GridView> </div>
                                              </asp:Panel>
                                              </asp:View> <asp:View ID="vwAuditor" runat="server">
                                              <asp:Panel ID="pnAuditor" runat="server" Height="100px" ScrollBars="Vertical" Width="100%" CssClass="dataGrid">
                                                <asp:TextBox ID="txtPessoaNomeBusca" runat="server"></asp:TextBox>
                                                <asp:Button ID="btnPessoaNomeBusca" Text="Buscar por Nome" CssClass="botao" OnClick="pesquisarPessoaPorNome" runat="server" />                                      
                                                <asp:GridView ID="gvAuditor" runat="server" Width="98%" AllowPaging="true" PageSize="20" Font-Size="8" CellPadding="4" OnRowCommand="gvAuditor_OnRowCommand" AutoGenerateColumns="False" OnPageIndexChanging="gvAuditor_PageIndexChanging" OnRowDataBound="gvAuditor_RowDataBound">
                                                <HeaderStyle CssClass="topoGrid" />                                      
                                                <EditRowStyle BackColor="#C8E4E6" /> <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <FooterStyle BackColor="#FDF9DC" Font-Bold="True" ForeColor="#47451F" />                                      
                                                <PagerStyle BackColor="#F0F0F0" ForeColor="#47451F" HorizontalAlign="Center" CssClass="menu"/>                                      
                                                <Columns>
                                                <asp:TemplateField>
                                                <ItemTemplate>
                                                  <asp:CheckBox ID="chbCodigo" runat="server" />                                      
                                                  <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="5%" />                                      
          </asp:TemplateField> <asp:TemplateField HeaderText="Colaborador">
          <ItemTemplate> &nbsp;&nbsp;&nbsp;
              <asp:Label ID="lblNome" Text='<%# DataBinder.Eval(Container.DataItem, "nome") %>' runat="server"></asp:Label>
          </ItemTemplate>
          </asp:TemplateField>
                                                </Columns>
                                              </asp:GridView> </asp:Panel>
                                              <table width="776" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                  <td align="center" valign="middle">
                                                    <asp:Button ID="btSalvaAuditor" Text="Salvar" OnClick="salvaAuditor" CssClass="botao" runat="server" />                                      
            </td>
                                                </tr>
                                              </table>
                                              </asp:View> <asp:View ID="vwDocumento" runat="server">
                                              <div> Arquivo:&nbsp;
                                                 <%-- <input name="file" type="file" id="flDocumento" title="Documento" runat="server" />--%>
                                                  <asp:FileUpload ID="flDocumento" runat="server" style="width: 300px; height: 23px;"/>
&nbsp;&nbsp; Descri&ccedil;&atilde;o:&nbsp;
          <asp:TextBox ID="txtDocumentoNome" runat="server" Width="300" CssClass="campo_texto"></asp:TextBox>
&nbsp;&nbsp;
          <asp:Button id="btnSalvaDocumento" Text="Salvar" runat="server" OnClick="salvaDocumento" CssClass="botao" />
                                              </div>
                                              <asp:Panel ID="pnDocumento" runat="server" Height="130px" ScrollBars="Both" Width="100%" BorderStyle="None" CssClass="dataGrid"> <asp:GridView ID="gvDocumento" runat="server" Width="98%" BackColor="White" AllowPaging="True" PageSize="20" BorderStyle="None" CellPadding="4" OnRowCommand="gvDocumento_OnRowCommand" AutoGenerateColumns="False" OnPageIndexChanging="gvDocumento_PageIndexChanging" OnRowDataBound="gvDocumento_RowDataBound">
                                                <HeaderStyle CssClass="topoGrid" />                                      
                                                <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                <FooterStyle BackColor="#FDF9DC" Font-Bold="True" ForeColor="#47451F" />                                      
                                                <PagerStyle BackColor="#F0F0F0" ForeColor="#47451F" HorizontalAlign="Center" CssClass="menu"/>                                      
                                                <Columns>
                                                <asp:TemplateField>
                                                <ItemTemplate>
                                                  <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_codigo") %>' Visible="false" runat="server"></asp:Label>
                                                  <asp:Label ID="lblCaminho" Text='<%# DataBinder.Eval(Container.DataItem, "anexo_caminho") %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />                                      
                                                <HeaderStyle HorizontalAlign="Center" Width="20px" />                                      
          </asp:TemplateField> <asp:TemplateField HeaderText="Arquivo">
          <ItemTemplate>
            <asp:Label ID="lblArquivo" runat="server"></asp:Label>
          </ItemTemplate>
          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px" />
          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px" />
          </asp:TemplateField> <asp:TemplateField HeaderText="Descri&#231;&#227;o">
          <ItemTemplate>
            <asp:TextBox CssClass="campo_descricao350" ID="txtNome" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "anexo_nome") %>' ></asp:TextBox>
          </ItemTemplate>
          <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px" />
          <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="350px" />
          </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif">
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
          </asp:ButtonField>
                                                </Columns>
                                              </asp:GridView> </asp:Panel>
                                          </asp:View> </asp:MultiView> </div></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                  </table></td>
                                </tr>
                                <tr>
                                  <td colspan="3" align="left" valign="top">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" id="tabelaRodape">
                                      <!--DWLayoutTable-->
                                      <tr>
                                        <td width="8" height="7" class="esq_down"></td>
                                        <td valign="top" class="centro_down"></td>
                                        <td width="8" class="dir_down"></td>
                                      </tr>
                                  </table></td>
                                </tr>
                            </table></td>
                          </tr>
                        </table></td>
                      </tr>
                    </table></td>
            </tr>
      </table>
    </div>
    </form>
</body>
</html>