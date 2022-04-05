<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCPrioridade.ascx.cs" Inherits="WUCPrioridade" %>

<script language="javascript">
    function verifica() {
	    if (confirm("Deseja mesmo excluir este item?")) {
		    return true;
	    }
	    else {
		    return false;
	    }
    }
</script> 
<script language="javascript">
	function Somente_Numeros(codigo)
	{ 
		var Tecla = event.which;
		if(Tecla == null)
			{
			Tecla = event.keyCode;
			if ( Tecla < 48 ||  Tecla > 57)
			{
				event.returnValue = false;
	    		return false
			}
			event.returnValue = true;
			return true
			}  
	}
</script> 
<tr id="Tr1" runat=server>
    <td height="0" align="center" valign="middle">
		<div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
			<table width="776" border="0" cellspacing="5" cellpadding="0">
                <tr>
                  <td width="60" align="center" valign="middle"><asp:Image ID="imgIcone" runat="server"  /></td>
                    <td align="center" valign="middle"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                </tr>
          </table>    
        </div>
	</td>
</tr>
<tr id="Tr2" runat=server>
    <td align="center" valign="middle">
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
                          <td align="left" valign="middle" class="tituloFont">Prioridade</td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%" border="0" cellpadding="0" cellspacing="0" >
                <tr>
                  <td align="center" valign="top">
                    <table width="95%"  border="0" cellspacing="2" cellpadding="0">
                      <tr>
                        <td width="50" align="left">Descri&ccedil;&atilde;o:</td>
                        <td width="194" align="left">
                          <asp:TextBox ID="txtDescricao" MaxLength="100" CssClass="campo_texto" runat="server" Width="300"></asp:TextBox>                        </td>
                        <td width="120" align="left">In&iacute;cio Atendimento:</td>
                        <td width="40" align="left"><asp:TextBox ID="txtTempoInicioAtendimento" onKeyPress="Somente_Numeros(this)" MaxLength="5" CssClass="campo_texto" runat="server" Width="40"></asp:TextBox>            </td>
                        <td width="50" align="left">Minutos</td>
                        <td align="right">Tempo Solu&ccedil;&atilde;o:</td>
                        <td width="40" align="left"><asp:TextBox ID="txtTempoSolucao"  onKeyPress="Somente_Numeros(this)" MaxLength="5" CssClass="campo_texto" runat="server" Width="40"></asp:TextBox>            </td>
                        <td width="50" align="left">Minutos</td>
                        <td align="center" valign="middle">						  <asp:Button ID="btnLimpar" Width="60px" CssClass="botao" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />              
                          <asp:Button ID="btnSalvar" Width="60px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" /></td>
                      </tr>
                  </table></td>
                </tr>
                <tr align="left" valign="top">
                  <td colspan="7">
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td align="left"><table border="0" align="left"  cellpadding="0" cellspacing="0">
                          <tr>
                            <td class="aba_esquerda_off">&nbsp;</td>
                            <td class="aba_centro_off">Prioridade</td>
                            <td class="aba_direita_off">&nbsp;</td>
                          </tr>
                        </table></td>
                        </tr>
                      <tr align="left">
                        <td valign="top">
                          <asp:Panel ID="pnlGridPrioridade"  runat="server" Height="200px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
						  <asp:GridView ID="gvPrioridade" GridLines="None" Width="98%" HorizontalAlign="Center" AllowPaging="True" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" OnRowCommand="gvPrioridade_OnRowCommand" OnRowDataBound="gvPrioridade_RowDataBound">
                            <FooterStyle CssClass="footerGrid" />              
                            <HeaderStyle CssClass="topoGrid" />              
                            <RowStyle BackColor="#F4FBFA" />
                            <Columns>
                            <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                            <ItemTemplate>
                              <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "prioridade_codigo")%>' runat="server"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField> <asp:TemplateField  HeaderText="Descri&#231;&#227;o">
                            <ItemTemplate>
                              <asp:TextBox CssClass="campo_descricao400" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="400px" />              
                            <ItemStyle HorizontalAlign="Left" Width="400px" />              
              </asp:TemplateField> <asp:TemplateField  HeaderText="In&#237;cio do Atendimento">
              <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "tempo_inicio_atendimento")%> </ItemTemplate>
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              <ItemStyle HorizontalAlign="Center" />
              </asp:TemplateField> <asp:TemplateField  HeaderText="Tempo Solu&#231;&#227;o">
              <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "tempo_solucao")%> </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" />
                  <ItemStyle HorizontalAlign="Center" />
              </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Selecionar" Text="Selecionar" ImageUrl="images/icones/selecionado.gif" >
              <ItemStyle HorizontalAlign="Center" Width="20px" />
              </asp:ButtonField> <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="images/icones/editar.gif" >
              <ItemStyle HorizontalAlign="Center" Width="40px" />
              </asp:ButtonField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif" >
              <ItemStyle HorizontalAlign="Center" Width="20px" />
              </asp:ButtonField>
                            </Columns>
                            <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />              
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#2461BF" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>                        </td>
                      </tr>
                  </table></td>
                </tr>
                <div id="divPrioridadeCriterio" runat="server">
                </div>
                  <tr>
                    <td align="center" valign="middle">
                      <table width="95%"  border="0" cellspacing="2" cellpadding="0">
                        <tr>
                          <td width="50" align="left">Impacto:</td>
                          <td>
                            <asp:DropDownList ID="ddlImpacto" CssClass="campo_texto" runat="server" Width="320px"></asp:DropDownList>                          </td>
                          <td width="50" align="left">Urg&ecirc;ncia:</td>
                          <td width="100px">
                            <asp:DropDownList ID="ddlUrgencia" CssClass="campo_texto" runat="server" Width="320px"></asp:DropDownList>
                          </td>
                          <td align="center" valign="middle">
                            <asp:Button ID="btnLimpa_Criterio" Width="80px" CssClass="botao" runat="server" Text="Limpar" OnClick="btnLimpa_Criterio_Click" />              
            </td>
                          <td align="center" valign="middle">
                            <asp:Button ID="btnSalva_Criterio" Width="80px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalva_Criterio_Click" />              
            </td>
                        </tr>
                    </table></td>
                  </tr>
                  <!-- Prioridade Crit&eacute;rio -->
                  <tr align="left" valign="top">
                    <td colspan="7">
                      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td align="left"><table border="0" align="left"  cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="aba_esquerda_off">&nbsp;</td>
                              <td class="aba_centro_off">Crit&eacute;rio de Prioridade</td>
                              <td class="aba_direita_off">&nbsp;</td>
                            </tr>
                          </table></td>
                          </tr>
                        <tr align="left">
                          <td valign="top">
                            <asp:Panel ID="pnlGridPrioridadeCriterio"  runat="server" Height="150px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
							<asp:GridView ID="gvPrioridadeCriterio" GridLines="None" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server" CellPadding="3" 
									                    OnRowDataBound="gvPrioridadeCriterio_RowDataBound" 
									                    OnRowCommand="gvPrioridadeCriterio_RowCommand" 
									                    OnRowEditing="gvPrioridadeCriterio_RowEditing" 
									                    OnRowCancelingEdit="gvPrioridadeCriterio_RowCancelingEdit" 
									                    OnRowUpdating="gvPrioridadeCriterio_RowUpdating">
                              <FooterStyle CssClass="footerGrid" />              
                              <HeaderStyle CssClass="topoGrid" />              
                              <RowStyle BackColor="#F4FBFA" />
                              <Columns>
                              <asp:CommandField ButtonType="Image" 
                                                            CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" 
                                                            EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True"
                                                            UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" >
                              <ItemStyle Width="50px" />              
                </asp:CommandField> <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                <ItemTemplate>
                  <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "prioridade_codigo")%>' runat="server"></asp:Label>
                </ItemTemplate>
                </asp:TemplateField> <asp:TemplateField HeaderText="Impacto">
                <ItemTemplate>
                  <asp:Label ID="lblImpactoCodigoValor" Text='<%# DataBinder.Eval(Container.DataItem, "impacto_codigo")%>' Visible="false" runat="server"></asp:Label>
                  <asp:Label ID="lblImpactoCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "impacto_codigo")%>' runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                  <asp:Label ID="lblImpactoCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "impacto_codigo")%>' Runat="server" Visible="False"></asp:Label>
                  <asp:DropDownList ID="ddlImpactoCodigo" Width="200" Runat="server"></asp:DropDownList>
                </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField> <asp:TemplateField HeaderText="Urg&#234;ncia">
                <ItemTemplate>
                  <asp:Label ID="lblUrgenciaCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "urgencia_codigo")%>' runat="server"></asp:Label>
                  <asp:Label ID="lblUrgenciaCodigoValor" Text='<%# DataBinder.Eval(Container.DataItem, "urgencia_codigo")%>' Visible="false" runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                  <asp:Label ID="lblUrgenciaCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "urgencia_codigo")%>' Runat="server" Visible="False"></asp:Label>
                  <asp:DropDownList ID="ddlUrgenciaCodigo" Width="200" Runat="server"></asp:DropDownList>
                </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif" >
                <ItemStyle HorizontalAlign="Center" Width="40px" />
                </asp:ButtonField>
                              </Columns>
                              <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />              
                              <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#D9EFF0" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>                          </td>
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
</table>  </td>
</tr>