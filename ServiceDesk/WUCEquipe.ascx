<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCEquipe.ascx.cs" Inherits="WUCEquipe" %>

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
                              <td align="left" valign="middle" class="tituloFont">Equipe</td>
                            </tr>
                        </table></td>
                        <td width="8" class="dir_top"></td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%" border="0" cellpadding="0" cellspacing="0" >
                    <tr>
                      <td colspan="5" height="1" valign="top">
                        <div id="divMensagem" runat="server" class="Mensagem" style="width: 100%" visible="false">
                          <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                              <td width="60" align="center" valign="middle"><asp:Image ID="imgIcone" runat="server"  /></td>
                              <td align="center" valign="middle"><asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                            </tr>
                          </table>
                      </div></td>
                    </tr>
                    <tr>
                      <td align="center" valign="top">
                        <table width="92%"  border="0" cellspacing="2" cellpadding="0">
                          <tr>
                            <td width="71" align="left">Aplica&ccedil;&atilde;o:</td>
                            <td align="left">
                            <asp:DropDownList ID="ddlAplicacao" runat="server" Width="504px" CssClass="campo_texto"></asp:DropDownList>                            </td>
                            <td width="50" align="left">N&iacute;vel:</td>
                            <td align="right" valign="middle">
                            <asp:DropDownList ID="ddlNivelEquipe" runat="server" CssClass="campo_texto" Width="150"></asp:DropDownList>                            </td>
                          </tr>
                          <tr>
                            <td align="left">Descri&ccedil;&atilde;o:</td>
                            <td align="left"><asp:TextBox ID="txtDescricao" MaxLength="100" CssClass="campo_texto" runat="server" Width="500px"></asp:TextBox></td>
                            <td colspan="2" align="right">
                              <asp:Button ID="btnLimpar" Width="72px" CssClass="botao" runat="server" Text="Limpar" OnClick="btnLimpar_Click" />                  
                              <asp:Button ID="btnSalvar" Width="72px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />                  
          </td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr align="left" valign="top">
                      <td colspan="7">
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td align="left"><table height="19" border="0" align="left"  cellpadding="0" cellspacing="0">
                              <tr>
                                <td class="aba_esquerda_off">&nbsp;</td>
                                <td class="aba_centro_off">Listagem Equipe</td>
                                <td class="aba_direita_off">&nbsp;</td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td colspan="3" align="center" valign="top" width="100%" height="200px">
                              <table height="10" width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                  <td align="left" valign="top">
                                    <asp:Panel ID="pnlGridEquipe" Height="200px" runat="server" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
									<asp:GridView ID="gvEquipe" GridLines="None" Width="98%" HorizontalAlign="Center" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="False" 
									                            OnRowCommand="gvEquipe_OnRowCommand" 
									                            OnRowDataBound="gvEquipe_RowDataBound"
									                            OnRowEditing="gvEquipe_RowEditing" 
									                            OnRowCancelingEdit="gvEquipe_RowCancelingEdit" 
									                            OnRowUpdating="gvEquipe_RowUpdating">
                                      <FooterStyle CssClass="footerGrid" />                  
                                      <HeaderStyle CssClass="topoGrid"  HorizontalAlign="Left"/>                  
                                      <RowStyle BackColor="#F4FBFA" />
                                      <Columns>
                                      <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                                      <ItemTemplate>
                                        <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "equipe_codigo")%>' runat="server"></asp:Label>
                                      </ItemTemplate>
                                      </asp:TemplateField> <asp:TemplateField HeaderText="Aplica&#231;&#227;o" >
                                      <ItemTemplate>
                                        <asp:Label ID="lblAplicacaoCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "aplicacao_codigo")%>' runat="server"></asp:Label>
                                      </ItemTemplate>
                                      <EditItemTemplate  >
                                        <asp:Label ID="lblAplicacaoCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "aplicacao_codigo")%>' Runat="server" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="ddlAplicacaoCodigo" Width="100" Runat="server"></asp:DropDownList>
                                      </EditItemTemplate>
                                      <FooterStyle HorizontalAlign="Left" />                  
                                      <HeaderStyle HorizontalAlign="Left" Width="100px" />                  
                                      <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />                  
                    </asp:TemplateField> <asp:TemplateField HeaderText="Nivel">
                    <ItemTemplate>
                      <asp:Label ID="lblNivelEquipeCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "equipe_nivel_codigo")%>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                      <asp:Label ID="lblNivelEquipeCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "equipe_nivel_codigo")%>' Runat="server" Visible="False"></asp:Label>
                      <asp:DropDownList ID="ddlNivelEquipeCodigo" Width="100" Runat="server"></asp:DropDownList>
                    </EditItemTemplate>
                    <FooterStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                    </asp:TemplateField> <asp:TemplateField  HeaderText="Descri&#231;&#227;o">
                    <ItemTemplate>
                      <asp:TextBox CssClass="campo_descricao400" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                    </ItemTemplate>
                    <EditItemTemplate>
                      <asp:Label ID="lblDescricao" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>' Runat="server" Visible="False"></asp:Label>
                      <asp:TextBox CssClass="campo_texto" ID="txtDescricaoEquipe" runat="server" Width="400px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="400px" />
                    <FooterStyle HorizontalAlign="Left" />
                    <HeaderStyle HorizontalAlign="Left" Width="400px" />
                    </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Selecionar" Text="Selecionar" ImageUrl="images/icones/selecionado.gif" >
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                    <HeaderStyle Width="20px" />
                    </asp:ButtonField> <asp:CommandField ButtonType="Image" 
                                                                        CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" 
                                                                        EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True"
                                                                        UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" >
                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                    <HeaderStyle Width="40px" />
                    </asp:CommandField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" Text="Excluir" ImageUrl="images/icones/excluir.gif" >
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                    <HeaderStyle Width="20px" />
                    </asp:ButtonField>
                                      </Columns>
                                      <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />                  
                                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#C8E4E6" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>                                  </td>
                                </tr>
                            </table></td>
                          </tr>
                      </table></td>
                    </tr>
                    <div id="divEquipePessoa" runat="server">
                      <tr>
                        <td>
                          <table width="100%"  border="0" cellspacing="3" cellpadding="0">
                            <tr>
                              <td width="71" align="left">Empresa:</td>
                              <td>
                                <asp:DropDownList ID="ddlEmpresa" CssClass="campo_texto" runat="server" Width="500px"></asp:DropDownList>
                              </td>
                              <td align="center" valign="middle">
                                <asp:Button ID="btnFiltrar" Width="80px" CssClass="botao" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />                  
                                <asp:Button ID="btnSalvarPessoaEquipe" Width="80px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvarPessoaEquipe_Click" />                  
            </td>
                            </tr>
                        </table></td>
                      </tr>
                      <br />
                      <tr align="left" valign="top">
                        <td colspan="7">
                          <div id="divIntegrantesEquipe" runat="server">
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td align="left"><table height="19" border="0" align="left"  cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="aba_esquerda_off">&nbsp;</td>
                                    <td class="aba_centro_off">Integrantes da Equipe</td>
                                    <td class="aba_direita_off">&nbsp;</td>
                                  </tr>
                                </table></td>
                              </tr>
                              <tr align="left">
                                <td valign="top">
                                  <asp:Panel ID="pnlGridEquipePessoa"  runat="server" Height="150px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid"> <asp:GridView ID="gvEquipePessoa" GridLines="None" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="False" 
									                        OnRowDataBound="gvEquipePessoa_RowDataBound"
									                        OnRowEditing="gvEquipePessoa_RowEditing" 
									                        OnRowCancelingEdit="gvEquipePessoa_RowCancelingEdit" 
									                        OnRowUpdating="gvEquipePessoa_RowUpdating">
                                    <FooterStyle CssClass="footerGrid" />                  
                                    <HeaderStyle CssClass="topoGrid" HorizontalAlign="Left" />                  
                                    <RowStyle BackColor="#F4FBFA" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                                    <ItemTemplate>
                                      <asp:Label ID="lblPessoaCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo")%>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField> <asp:TemplateField HeaderText="Integrante"  HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                      <asp:Label ID="lblIntegranteEquipe" Text='<%# DataBinder.Eval(Container.DataItem, "IntegranteEquipe")%>' runat="server" Visible="false"></asp:Label>
                                      <asp:CheckBox ID="ckEquipePessoa" runat="server" />                  
                  </ItemTemplate>
                                    <EditItemTemplate>
                                      <asp:Label ID="lblIntegranteEquipe2" Text='<%# DataBinder.Eval(Container.DataItem, "IntegranteEquipe")%>' runat="server" Visible="false"></asp:Label>
                                      <asp:CheckBox ID="ckEquipePessoa2" runat="server" />                  
                  </EditItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />                  
                                    <HeaderStyle HorizontalAlign="Left" />                  
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />                  
                  </asp:TemplateField> <asp:TemplateField HeaderText="Equipe"  HeaderStyle-HorizontalAlign="Left">
                  <ItemTemplate>
                    <asp:Label ID="lblPessoaNome" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>' runat="server"></asp:Label>
                  </ItemTemplate>
                  <FooterStyle HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  </asp:TemplateField> <asp:TemplateField HeaderText="Empresa" HeaderStyle-HorizontalAlign="Left">
                  <ItemTemplate>
                    <asp:Label ID="lblEmpresaCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "estrutura_codigo")%>' runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblRazaoSocial" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>' runat="server"></asp:Label>
                  </ItemTemplate>
                  <FooterStyle HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  </asp:TemplateField> <asp:TemplateField HeaderText="Lider" HeaderStyle-HorizontalAlign="Left">
                  <ItemTemplate>
                    <asp:Label ID="lblCodigoFlagLider" Text='<%# DataBinder.Eval(Container.DataItem, "flag_lider")%>' Visible="false" runat="server"></asp:Label>
                    <asp:Label ID="lblFlagLider" Text='<%# DataBinder.Eval(Container.DataItem, "flag_lider")%>' runat="server"></asp:Label>
                  </ItemTemplate>
                  <EditItemTemplate>
                    <asp:Label ID="lblFlagLider" Text='<%# DataBinder.Eval(Container.DataItem, "flag_lider")%>' Runat="server" Visible="False"></asp:Label>
                    <asp:DropDownList ID="ddlFlagLider" runat="server"></asp:DropDownList>
                  </EditItemTemplate>
                  <FooterStyle HorizontalAlign="Left" />
                  <HeaderStyle HorizontalAlign="Left" />
                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  </asp:TemplateField> <asp:CommandField ButtonType="Image" 
                                                                    CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" 
                                                                    EditImageUrl="images/icones/editar.gif" EditText="Editar lider" ShowEditButton="True"
                                                                    UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" >
                  <ItemStyle HorizontalAlign="Center" Width="40px" />
                  </asp:CommandField>
                                    </Columns>
                                    <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />                  
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#C8E4E6" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>                                </td>
                              </tr>
                            </table>
                        </div></td>
                      </tr>
                    </div>
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
    </table>