<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCItemConfiguracao.ascx.cs" Inherits="WUCItemConfiguracao" %>
<div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
  <table width="776" border="0" cellspacing="5" cellpadding="0" align="center">
    <tr>
      <td width="60" align="center" valign="bottom">
        <asp:Image ID="imgIcone" runat="server" /></td>
      <td align="center" valign="bottom">
        <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
    </tr>
  </table>
</div>
<table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
  <tr>
    <td height="20" align="center" valign="top">&nbsp;    </td>
  </tr>
  <tr>
    <td align="center" valign="top"><table width="98%"  border="0" cellspacing="0" cellpadding="0">
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
                          <td align="left" valign="middle" class="tituloFont">Item de Configura&ccedil;&atilde;o </td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela">
                <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="200" align="left" valign="top">
					<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>
                        <td>
                          <table width="100%" height="100%" cellpadding="0" cellspacing="0">
                            <tr>
                              <td>
                                <table border="0" align="left" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="aba_esquerda_off">&nbsp;</td>
                                    <td class="aba_centro_off">Tipo</td>
                                    <td class="aba_direita_off">&nbsp;</td>
                                  </tr>
                              </table></td>
                            </tr>
                            <tr>
                              <td align="left" valign="top" class="dataTree">
                                <asp:Panel ID="pnTreeView" CssClass="dataGrid" Width="200px" Height="210" ScrollBars="Both" runat="server">
								<asp:TreeView ID="trvTipo" Height="190" PopulateNodesFromClient="true" ShowLines="true" ShowExpandCollapse="true" runat="server" OnTreeNodePopulate="trvTipo_TreeNodePopulate"></asp:TreeView> </asp:Panel>                              </td>
                            </tr>
                            <tr>
                              <td>
                                <table border="0" align="left" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td class="aba_esquerda_off">&nbsp;</td>
                                    <td class="aba_centro_off">Estrutura</td>
                                    <td class="aba_direita_off">&nbsp;</td>
                                  </tr>
                              </table></td>
                            </tr>
                            <tr>
                              <td align="left" valign="top" class="dataTree">
                                <asp:Panel ID="pnTreeViewItem" CssClass="dataGrid" Width="200px" Height="210" ScrollBars="Both" runat="server">
								<asp:TreeView ID="trvItemConfiguracao" Height="190" PopulateNodesFromClient="true" ShowLines="true" ShowExpandCollapse="true" runat="server" OnTreeNodePopulate="trvItemConfiguracao_TreeNodePopulate"></asp:TreeView> </asp:Panel>                              </td>
                            </tr>
                        </table></td>
                      </tr>
                    </table></td>
                    <td align="left" valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td align="center" valign="top"><asp:Panel ID="pnItem" runat="server" CssClass="dataGrid">
                          <table border="0" cellspacing="3" cellpadding="0">
                            <tr>
                              <td align="left" valign="middle" width="99"> Nome:</td>
                              <td colspan="3" align="left" valign="middle">
                                <asp:TextBox ID="txtNome" runat="server" Width="480px" CssClass="campo_texto"></asp:TextBox></td>
                            </tr>
                            <tr>
                              <td align="left" valign="middle"> Item superior:</td>
                              <td colspan="2" align="left" valign="middle">
                                <asp:DropDownList ID="ddlSuperior" runat="server" Width="100%" CssClass="campo_texto"> </asp:DropDownList>
                              </td>
                              <td align="left" valign="middle">
                                <asp:CheckBox ID="ckbInternoTI" runat="server" />                        
                                <asp:Label ID="lblInternoTI" Text='Interno TI' runat="server"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td align="left" valign="middle">Tipo:</td>
                              <td width="188" align="left" valign="middle">
                                <asp:DropDownList ID="ddlTipoItem" AutoPostBack="true" OnSelectedIndexChanged="buscaPrefixoTipo"
                            runat="server" Width="208px" CssClass="campo_texto"> </asp:DropDownList></td>
                              <td width="65" align="right" valign="middle">Situa&ccedil;&atilde;o: </td>
                              <td width="209" align="left" valign="middle">
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="205px" CssClass="campo_texto"> </asp:DropDownList></td>
                            </tr>
                            <tr>
                              <td align="left" valign="middle">Prefixo:</td>
                              <td align="left" valign="middle">
                                <asp:TextBox ID="txtPrefixoTipo" Width="204px" runat="server" CssClass="campo_texto"></asp:TextBox></td>
                              <td align="right" valign="middle">N&uacute;mero:</td>
                              <td align="left" valign="middle">
                                <asp:TextBox ID="txtNumero" runat="server" Width="201px" CssClass="campo_texto"></asp:TextBox></td>
                            </tr>
                            <tr>
                              <td align="left" valign="middle">Janela Serv.:</td>
                              <td align="left" valign="middle">
                                <asp:DropDownList ID="ddlServicoJanela" runat="server" Width="100%" CssClass="campo_texto"> </asp:DropDownList></td>
                              <td align="right"> Suporte:</td>
                              <td align="left" valign="middle">
                                <asp:DropDownList ID="ddlSuporte" runat="server" Width="205px" CssClass="campo_texto"> </asp:DropDownList></td>
                            </tr>
                            <tr>
                              <td align="left" valign="top">Descri&ccedil;&atilde;o:</td>
                              <td colspan="3" align="left" valign="middle">
                                <asp:TextBox ID="txtDescricao" runat="server" TextMode="MultiLine" Width="480px" Height="50" CssClass="campo_texto"></asp:TextBox>
                              </td>
                            </tr>
                            <tr align="center" valign="middle">
                              <td colspan="4" align="center" valign="middle">
                                <asp:Button ID="btnSalva" CssClass="botao" Text="Salvar" runat="server" OnClick="btnSalva_Click" />                        
                                <asp:Button ID="btnCopiaItem" CssClass="botao" Text="Copiar Item" runat="server" OnClick="btnCopiaItem_Click" />                        
                                <asp:Button ID="btnNovo" CssClass="botao" Text="Novo" runat="server" OnClick="novoItemConfiguracao" />                        
      </td>
                            </tr>
                          </table>
                        </asp:Panel></td>
                      </tr>
                      <tr>
                        <td align="left" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0">
                          <tr>
                            <td align="center" valign="top">
                              <asp:Panel ID="pnTab" Visible="false" Width="100%" runat="server" CssClass="dataGrid">
                                <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td align="left" valign="top" class="linha">
                                      <table border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td>
                                            <table border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td id="aba_esq" runat="server" class="aba_esquerda_off">&nbsp; </td>
                                                <td class="aba_centro_off" id="aba" runat="server">
                                                  <asp:LinkButton ID="lkbAtributo" runat="server" OnClick="mudaAba" CommandArgument="0">Rel. Atributo</asp:LinkButton></td>
                                                <td id="aba_dir" runat="server" class="aba_direita_off">&nbsp; </td>
                                              </tr>
                                          </table></td>
                                          <td>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                              <tr>
                                                <td id="aba_esq1" runat="server" class="aba_esquerda_off">&nbsp; </td>
                                                <td class="aba_centro_off" id="aba1" runat="server">
                                                  <asp:LinkButton ID="btnRelacionamento" runat="server" OnClick="mudaAba" CommandArgument="1">Rel. IC</asp:LinkButton></td>
                                                <td id="aba_dir1" runat="server" class="aba_direita_off">&nbsp; </td>
                                              </tr>
                                          </table></td>
                                          <td>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                              <tr>
                                                <td id="aba_esq2" runat="server" class="aba_esquerda_off">&nbsp; </td>
                                                <td class="aba_centro_off" id="aba2" runat="server">
                                                  <asp:LinkButton ID="btnEstrutura" runat="server" OnClick="mudaAba" CommandArgument="2">Rel. Estrutura</asp:LinkButton></td>
                                                <td id="aba_dir2" runat="server" class="aba_direita_off">&nbsp; </td>
                                              </tr>
                                          </table></td>
                                          <td>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                              <tr>
                                                <td id="aba_esq3" runat="server" class="aba_esquerda_off">&nbsp; </td>
                                                <td class="aba_centro_off" id="aba3" runat="server">
                                                  <asp:LinkButton ID="btnPessoa" runat="server" CommandArgument="3" OnClick="mudaAba">Rel. Pessoa</asp:LinkButton></td>
                                                <td id="aba_dir3" runat="server" class="aba_direita_off">&nbsp; </td>
                                              </tr>
                                          </table></td>
                                          <td>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                              <tr>
                                                <td id="Td1" runat="server" class="aba_esquerda_off">&nbsp; </td>
                                                <td class="aba_centro_off" id="Td2" runat="server">
                                                  <asp:LinkButton ID="btnStatusLog" runat="server" CommandArgument="4" OnClick="mudaAba">Log de Status</asp:LinkButton></td>
                                                <td id="Td3" runat="server" class="aba_direita_off">&nbsp; </td>
                                              </tr>
                                          </table></td>
                                          <td>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                              <tr>
                                                <td id="Td4" runat="server" class="aba_esquerda_off">&nbsp;</td>
                                                <td class="aba_centro_off" id="Td5" runat="server">
                                                  <asp:LinkButton ID="lkbAtividade" runat="server" CommandArgument="5" OnClick="mudaAba">Atividades</asp:LinkButton>
                                                </td>
                                                <td id="Td6" runat="server" class="aba_direita_off">&nbsp;</td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td align="left" valign="top">
									<asp:MultiView ID="mtvItem" ActiveViewIndex="0" runat="server"> <asp:View ID="vwAtributo" runat="server">
                                        <asp:Panel ID="pnlGridAtributo" runat="server" ScrollBars="Vertical" Height="170px" Width="100%" CssClass="dataGrid">
										<asp:GridView ID="gvAtributo" GridLines="None" runat="server" Width="97%" BackColor="White"
                                    BorderColor="#3366CC" AllowPaging="True" PageSize="20" BorderStyle="None" Font-Size="8pt"
                                    CellPadding="4" OnRowCommand="gvAtributo_OnRowCommand" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvAtributo_PageIndexChanging" OnRowDataBound="gvAtributo_RowDataBound">
                                          <HeaderStyle CssClass="topoGrid" />                        
                                          <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                          <FooterStyle CssClass="footerGrid" />                        
                                          <PagerStyle HorizontalAlign="center" CssClass="menu" />                        
                                          <Columns>
                                          <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                          <ItemTemplate>
                                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "ic_atributo_codigo") %>'
                                            runat="server"></asp:Label>
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Right" Width="50px" />                        
                  </asp:TemplateField> <asp:TemplateField HeaderText="Atributo">
                  <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "descricao")%> </ItemTemplate>
                  <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                  </asp:TemplateField> <asp:TemplateField HeaderText="Valor">
                  <ItemTemplate>
                    <asp:TextBox CssClass="campo_texto" Width="350px" ID="txtValor" runat="server"></asp:TextBox>
                    <asp:Label ID="lblMedida" Text='<%# DataBinder.Eval(Container.DataItem, "medida_codigo") %>'
                                            runat="server"></asp:Label>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  </asp:TemplateField>
                                          </Columns>
                                        </asp:GridView> </asp:Panel>
                                        <div align="center">
                                          <asp:Button Text="Salvar Todos" CssClass="botao" ID="btnSalvaAtributos" runat="server"
                                    OnClick="salvaAtributos" />                        
                </div>
                                        </asp:View> <asp:View ID="vwRelacionamento" runat="server">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text="Tipo de relac.:"></asp:Label></td>
                                                    <td>
                                        <asp:DropDownList ID="ddlRelacaoTipo" runat="server" Width="70px"> </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" Text="Item de config.:"></asp:Label></td>
                                                    <td>
                <asp:DropDownList ID="ddlItemConfiguracaoFilho" Width="280px" runat="server"> </asp:DropDownList></td>
                                                    <td>
                <asp:Button ID="btnSalvarRelacionamento" Text="Salvar" Width="40px" CssClass="botao" runat="server" OnClick="btnSalvarRelacionamento_Click" /></td>
                                                </tr>
                                            </table>
                <asp:Panel ID="pnlGridRelacionamento" runat="server" ScrollBars="Vertical" Height="170px" Width="100%" CssClass="dataGrid">
				<asp:GridView ID="gvRelacionamento" GridLines="None" runat="server" Width="97%"
                                    BackColor="White" BorderColor="#3366CC" AllowPaging="True" PageSize="20" BorderStyle="None"
                                    Font-Size="8pt" CellPadding="4" OnRowCommand="gvRelacionamento_OnRowCommand" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvRelacionamento_PageIndexChanging" OnRowDataBound="gvRelacionamento_RowDataBound">
                  <HeaderStyle CssClass="topoGrid" />
                  <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                  <FooterStyle CssClass="footerGrid" />
                  <PagerStyle HorizontalAlign="Center" CssClass="menu" />
                  <Columns>
                  <asp:TemplateField HeaderText="Rela&#231;&#227;o">
                    <ItemTemplate>
                        <asp:TextBox CssClass="campo_descricao300" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" />
                    <ItemStyle HorizontalAlign="Left" Width="50%" />
                  </asp:TemplateField> 
                  <asp:TemplateField HeaderText="Item de Configura&#231;&#227;o">
                    <ItemTemplate> 
                        <asp:Label runat="server" ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "ic_codigo_destino") %>'> </asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50%" Wrap="False" />
                  </asp:TemplateField>
                  <asp:TemplateField Visible="False">
                   <ItemTemplate>
                        <asp:Label runat="server" ID="lblCodigoIcDestino" Text='<%# DataBinder.Eval(Container.DataItem, "ic_codigo_destino") %>'></asp:Label>                   
                   </ItemTemplate> 
                  </asp:TemplateField>
                  <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCodigoTipoRelacao" Text='<%# DataBinder.Eval(Container.DataItem, "ic_relacao_tipo_codigo") %>' ></asp:Label>                     
                    </ItemTemplate> 
                  </asp:TemplateField>
                  <asp:TemplateField visible="false">
                    <ItemTemplate> 
                        <asp:Label runat="server" ID="lblCodigoOrigem" Text='<%# DataBinder.Eval(Container.DataItem, "ic_codigo_origem") %>'> </asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%" />
                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50%" Wrap="False" />
                  </asp:TemplateField>
                  <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif" Text="Button" />
                  </Columns>
                </asp:GridView> </asp:Panel>
                                        </asp:View> <asp:View ID="vwEstrutura" runat="server">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" Text="Filtro Tipo Estrutura Organizacional:" ></asp:Label></td>
                                                    <td>
                                        <asp:DropDownList ID="ddlEstruturaTipo" AutoPostBack="true" OnSelectedIndexChanged="montaEstruturaPorTipo"
                                  runat="server" Width="90px"> </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" Text="Filtro Tipo Relacionamento:" Width="208px"></asp:Label></td>
                                                    <td>
                                        <asp:DropDownList ID="ddlItemEstruturaTipo" AutoPostBack="true" OnSelectedIndexChanged="montaEstruturaPorTipo"
                                  runat="server" Width="100px"> </asp:DropDownList></td>
                                                </tr>
                                            </table>
                                        <asp:Panel ID="pnlGridEstrutura" runat="server" ScrollBars="Vertical" Height="170px" Width="100%" CssClass="dataGrid">
										<asp:GridView ID="gvEstrutura" GridLines="None" runat="server" Width="97%" BackColor="White"
                                    BorderColor="#3366CC" AllowPaging="True" PageSize="20" BorderStyle="None" Font-Size="8pt"
                                    CellPadding="4" OnRowCommand="gvEstrutura_OnRowCommand" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvEstrutura_PageIndexChanging" OnRowDataBound="gvEstrutura_RowDataBound">
                                          <HeaderStyle CssClass="topoGrid" />                        
                                          <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                          <FooterStyle CssClass="footerGrid" />                        
                                          <PagerStyle HorizontalAlign="center" CssClass="menu" />                        
                                          <Columns>
                                          <asp:TemplateField>
                                          <ItemTemplate>
                                            <asp:CheckBox ID="chbCodigo" runat="server" />                        
                                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "estrutura_codigo") %>'
                                            Visible="false" runat="server"></asp:Label>
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Center" Width="5%" />                        
                  </asp:TemplateField> <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                  <ItemTemplate>
                    <asp:TextBox CssClass="campo_descricao300" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  </asp:TemplateField>
                                          </Columns>
                                        </asp:GridView> </asp:Panel>
                                        <div style="text-align: center">
                                          <asp:Button ID="btnSalvaRelacaoEstrutura" Text="Salvar" OnClick="insereItemEstrutura"
                                    runat="server" CssClass="botao" />                        
                </div>
                                        </asp:View> <asp:View ID="vwPessoa" runat="server">
                                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" Text="Tipo:"></asp:Label></td>
                                                    <td>
                                        <asp:DropDownList ID="ddlItemPessoaTipo" AutoPostBack="true" OnSelectedIndexChanged="atualizaItemPessoaTipo" width="100px" runat="server"></asp:DropDownList></td>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" Text="Estr. Organiz.:"></asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEstruturaOrganizacional" runat="server" Width="181px">
                                                        </asp:DropDownList></td>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" Text="Pessoa:"></asp:Label></td>
                                                    <td>
                                        <asp:TextBox ID="txtPessoaNomeBusca" runat="server" CssClass="campo_texto" Width="70px"></asp:TextBox></td>
                                                    <td><asp:Button ID="btnPessoaNomeBusca" Text="Filtrar" Width="40px" CssClass="botao" OnClick="pesquisarPessoaPorNome" runat="server" /></td>
                                                </tr>
                                            </table>
                                        <asp:Panel ID="pnlGridPessoa" runat="server" ScrollBars="Vertical" Height="170px" Width="100%" CssClass="dataGrid">
										<asp:GridView GridLines="None" ID="gvPessoa" runat="server" Width="97%" BackColor="White"
                                    BorderColor="#3366CC" AllowPaging="True" PageSize="20" BorderStyle="None" Font-Size="8pt"
                                    CellPadding="4" OnRowCommand="gvPessoa_OnRowCommand" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvPessoa_PageIndexChanging" OnRowDataBound="gvPessoa_RowDataBound">
                                          <HeaderStyle CssClass="topoGrid" />                        
                                          <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                          <FooterStyle CssClass="footerGrid" />                        
                                          <PagerStyle HorizontalAlign="center" CssClass="menu" />                        
                                          <Columns>
                                          <asp:TemplateField>
                                          <ItemTemplate>
                                            <asp:CheckBox ID="chbCodigo" runat="server" />                        
                                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>'
                                            Visible="false" runat="server"></asp:Label>
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Center" Width="5%" />                        
                  </asp:TemplateField> <asp:TemplateField HeaderText="Colaborador">
                  <ItemTemplate> &nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblNome" Text='<%# DataBinder.Eval(Container.DataItem, "nome") %>'
                                            runat="server"></asp:Label>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  </asp:TemplateField>
                                          </Columns>
                                        </asp:GridView> </asp:Panel>
                                        <div style="text-align: center">
                                          <asp:Button ID="btSalvaItemRelacaoPessoa" Text="Salvar" OnClick="insereItemRelacaoPessoa"
                                    runat="server" CssClass="botao" />                        
                </div>
                                        </asp:View> <asp:View ID="vwStatusLog" runat="server">
                                        <asp:Panel ID="pnlGridStatusLog" runat="server" ScrollBars="Vertical" Height="170px" Width="100%" CssClass="dataGrid">
										<asp:GridView ID="gvStatusLog" GridLines="None" runat="server" Width="97%" BackColor="White"
                                    BorderColor="#3366CC" AllowPaging="True" PageSize="20" BorderStyle="None" Font-Size="8pt"
                                    CellPadding="4" OnRowCommand="gvStatusLog_OnRowCommand" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvStatusLog_PageIndexChanging" OnRowDataBound="gvStatusLog_RowDataBound">
                                          <HeaderStyle CssClass="topoGrid" />                        
                                          <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                          <FooterStyle CssClass="footerGrid" />                        
                                          <PagerStyle HorizontalAlign="center" CssClass="menu" />                        
                                          <Columns>
                                          <asp:TemplateField HeaderText="Colaborador">
                                          <ItemTemplate>
                                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>'
                                            Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblPessoa" runat="server"></asp:Label>
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Left" Width="100px" />                        
                                          <HeaderStyle HorizontalAlign="Left" Width="100px" />                        
                  </asp:TemplateField> <asp:TemplateField HeaderText="Status Antigo">
                  <ItemTemplate>
                    <asp:Label ID="lblStatusOrigem" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo_origem") %>'
                                            runat="server"></asp:Label>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                  </asp:TemplateField> <asp:TemplateField HeaderText="Status Novo">
                  <ItemTemplate> &nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblStatusDestino" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo_destino") %>'
                                            runat="server"></asp:Label>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                  </asp:TemplateField> <asp:TemplateField HeaderText="Data da Altera&#231;&#227;o">
                  <ItemTemplate> &nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblData" Text='<%# DataBinder.Eval(Container.DataItem, "data") %>'
                                            runat="server"></asp:Label>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:TemplateField>
                                          </Columns>
                                        </asp:GridView> </asp:Panel>
                                        </asp:View> <asp:View ID="vwAcao" runat="server">
                                        <asp:Panel ID="pnlGridAcao" runat="server" ScrollBars="Vertical" Height="170px" Width="100%" CssClass="dataGrid">
										<asp:GridView ID="gvAcao" GridLines="None" runat="server" Width="97%" BackColor="White"
                                    BorderColor="#3366CC" AllowPaging="True" PageSize="20" BorderStyle="None" Font-Size="8pt"
                                    CellPadding="4" OnRowCommand="gvAcao_OnRowCommand" AutoGenerateColumns="False"
                                    OnPageIndexChanging="gvAcao_PageIndexChanging" OnRowDataBound="gvAcao_RowDataBound">
                                          <HeaderStyle CssClass="topoGrid" />                        
                                          <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                          <FooterStyle CssClass="footerGrid" />                        
                                          <PagerStyle HorizontalAlign="Center" CssClass="menu" />                        
                                          <Columns>
                                          <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                                          <ItemTemplate>
                                            <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "acao_codigo") %>' runat="server"></asp:Label>
                                          </ItemTemplate>
                                          <ItemStyle HorizontalAlign="Right" Width="50px" />                        
                  </asp:TemplateField> <asp:TemplateField HeaderText="Associar">
                  <ItemTemplate>
                    <asp:CheckBox ID="cbxAssociaAcao" runat="server"/>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                  </asp:TemplateField> <asp:TemplateField HeaderText="Atributo">
                  <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "descricao")%> </ItemTemplate>
                  <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Middle" />
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" />
                  </asp:TemplateField> <asp:TemplateField HeaderText="Valor">
                  <ItemTemplate>
                    <asp:TextBox CssClass="campo_texto" Width="350px" ID="txtValor" runat="server"></asp:TextBox>
                  </ItemTemplate>
                  <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                  </asp:TemplateField>
                                          </Columns>
                                        </asp:GridView> </asp:Panel>
                                        <div align="center">
                                          <asp:Button Text="Salvar" CssClass="botao" ID="btnSalvaValorAcao" runat="server" OnClick="salvaAcao" />                        
                </div>
                                    </asp:View> </asp:MultiView></td>
                                  </tr>
                                </table>
                              </asp:Panel>
                            </td>
                          </tr>
                          <tr>
                            <td align="center" valign="top">
                              <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td align="left" valign="top">
                                    <asp:Panel ID="pnLista" Visible="false" runat="server" ScrollBars="Vertical" Height="170" Width="100%" CssClass="dataGrid">
                                      <asp:Label ID="lblMensagemGrid" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F" Visible="false"></asp:Label>
                                      <asp:GridView GridLines="None" ID="gvItemConfiguracao" runat="server" Width="98%"
                            BackColor="White" AllowPaging="True" PageSize="20" BorderStyle="None" Font-Size="8pt"
                            CellPadding="4" OnRowCommand="gvItemConfiguracao_OnRowCommand" AutoGenerateColumns="False"
                            OnPageIndexChanging="gvItemConfiguracao_PageIndexChanging" OnRowDataBound="gvItemConfiguracao_RowDataBound" OnLoad="gvItemConfiguracao_Load">
                                      <HeaderStyle CssClass="topoGrid" />                        
                                      <RowStyle BackColor="White" ForeColor="#1D164C" /> <AlternatingRowStyle BackColor="#F4FBFA" ForeColor="#1D164C" /> <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                      <FooterStyle CssClass="footerGrid" />                        
                                      <PagerStyle HorizontalAlign="center" CssClass="menu" />                        
                                      <Columns>
                                      <asp:TemplateField HeaderText="&#160;C&#243;digo">
                                      <ItemTemplate>
                                        <asp:Label ID="lblCodigo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ic_codigo") %>'
                                    runat="server"></asp:Label>
                                        <asp:Label ID="lblCodigoTipo" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ic_tipo_codigo") %>'
                                    runat="server"></asp:Label>
                                        <asp:Label ID="lblPrefixoNumero" runat="server"></asp:Label>
                                      </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" />                        
                                      <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />                        
              </asp:TemplateField> <asp:TemplateField HeaderText="&#160;Item de Configura&#231;&#227;o">
              <ItemTemplate> <%# DataBinder.Eval(Container.DataItem, "nome")%> </ItemTemplate>
              <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
              </asp:TemplateField> <asp:TemplateField HeaderText="&#160;Status">
              <ItemTemplate>
                <asp:Label ID="lblStatus" Text='<%# DataBinder.Eval(Container.DataItem, "status_atual")%>'
                                    runat="server"></asp:Label>
              </ItemTemplate>
              <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
              </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Editar" Text="Editar" ImageUrl="images/icones/editar.gif">
              <ItemStyle HorizontalAlign="Center" Width="40px" />
              </asp:ButtonField>
                                      </Columns>
                                      <EditRowStyle BackColor="#C8E4E6" /> </asp:GridView>
                                      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="center">
                                            <asp:Button ID="btnListaNovo" CssClass="botao" Text="Novo" runat="server" OnClick="novoItemConfiguracao" />                        
                  </td>
                                        </tr>
                                      </table>
                                    </asp:Panel>
                                  </td>
                                </tr>
                            </table></td>
                          </tr>
                        </table></td>
                      </tr>
                    </table></td>
                  </tr>
                </table>
				</td>
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