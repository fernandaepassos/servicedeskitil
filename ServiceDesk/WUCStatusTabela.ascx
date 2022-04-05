<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCStatusTabela.ascx.cs" Inherits="WUCStatusTabela" %>
<tr id="Tr2" runat=server>
    <td align="center" valign="middle">
	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="20" align="center" valign="top">&nbsp;</td>
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
                          <td align="left" valign="middle" class="tituloFont">Status por Tabela </td>
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
                      <div id="divMensagem" runat="server" class="Mensagem" style="width: 100%; height: 20px;" visible="false">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                            <td width="60" align="center" valign="middle"><asp:Image ID="imgIcone" runat="server"  /></td>
                            <td align="center" valign="middle" style="height: 20px"><asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
                          </tr>
                        </table>
                    </div></td>
                  </tr>
                  <tr align="left" valign="top">
                    <td colspan="7">
                      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td align="left"><table height="19" border="0" align="left"  cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="aba_esquerda_off">&nbsp;</td>
                              <td class="aba_centro_off">Tabelas</td>
                              <td class="aba_direita_off">&nbsp;</td>
                            </tr>
                          </table></td>
                          </tr>
                        <tr>
                          <td align="center" valign="top">
                            <asp:Panel ID="pnlGridTabela"  runat="server" Height="180px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid"> <asp:GridView ID="gvTabela" GridLines="None" Width="98%" HorizontalAlign="Center" PageSize="20" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="False" 
									                OnRowCommand="gvTabela_OnRowCommand">
                              <FooterStyle CssClass="footerGrid" />    
                              <HeaderStyle CssClass="topoGrid"  HorizontalAlign="Left"/>    
                              <RowStyle BackColor="#F4FBFA" />
                              <Columns>
                              <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                              <ItemTemplate>
                                <asp:Label ID="lblStatusTabelaCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "NomeTabela")%>' runat="server"></asp:Label>
                              </ItemTemplate>
                              </asp:TemplateField> <asp:TemplateField HeaderText="Tabela" >
                              <ItemTemplate>
                                <asp:Label ID="lblTabela" Text='<%# DataBinder.Eval(Container.DataItem, "NomeTabela")%>' runat="server"></asp:Label>
                              </ItemTemplate>
                              <FooterStyle HorizontalAlign="Left" />    
                              <HeaderStyle HorizontalAlign="Left" />    
                              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />    
                          </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Selecionar" Text="Selecionar" ImageUrl="images/icones/selecionado.gif" >
                          <ItemStyle HorizontalAlign="Center" Width="20px" />
                          <HeaderStyle Width="20px" />
                          </asp:ButtonField>
                              </Columns>
                              <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />    
                              <SelectedRowStyle BackColor="#C8E4E6" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#C8E4E6" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>
                          </td>
                        </tr>
                    </table></td>
                  </tr>
                  <div id="divStatus" runat="server"> <br />
                  </div>
                  <tr align="left" valign="top">
                    <td colspan="7">
                      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td align="left"><table height="19" border="0" align="left"  cellpadding="0" cellspacing="0">
                            <tr>
                              <td class="aba_esquerda_off">&nbsp;</td>
                              <td class="aba_centro_off">Status</td>
                              <td class="aba_direita_off">&nbsp;</td>
                            </tr>
                          </table></td>
                          </tr>
                        <tr>
                          <td align="center" valign="top">
                            <asp:Panel ID="pnlGridStatusTabela"  runat="server" Height="180px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid"> <asp:GridView ID="gvStatusTabela" GridLines="None" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="False" runat="server" CellPadding="3" ShowFooter="False" OnRowDataBound="gvStatusTabela_RowDataBound" 
									                    >
                              <FooterStyle CssClass="footerGrid" />    
                              <HeaderStyle CssClass="topoGrid" HorizontalAlign="Left" />    
                              <RowStyle BackColor="#F4FBFA" />
                              <Columns>
                              <asp:TemplateField HeaderText="C&#243;digo" Visible = False>
                              <ItemTemplate>
                                <asp:Label ID="lblStatusCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "status_codigo")%>' runat="server"></asp:Label>
                              </ItemTemplate>
                              </asp:TemplateField> <asp:TemplateField HeaderText="Selecionados">
                              <ItemTemplate>
                                <asp:CheckBox ID="ckStatus" runat="server" />    
                          </ItemTemplate>
                              <FooterStyle HorizontalAlign="Left" />    
                              <HeaderStyle HorizontalAlign="Center" Width="100px" />    
                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />    
                              <ControlStyle Width="20px" /> </asp:TemplateField> <asp:TemplateField HeaderText="Sigla">
                              <ItemTemplate>
                                <asp:Label ID="lblSigla" Text='<%# DataBinder.Eval(Container.DataItem, "sigla")%>' runat="server"></asp:Label>
                              </ItemTemplate>
                              <FooterStyle HorizontalAlign="Left" />    
                              <HeaderStyle HorizontalAlign="Center" Width="90px" />    
                              <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />    
                              <ControlStyle Width="20px" /> </asp:TemplateField> <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                              <ItemTemplate>
                                <asp:TextBox CssClass="campo_descricao550" ID="lblDescricao" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "descricao") %>' ></asp:TextBox>
                              </ItemTemplate>
                              <FooterStyle HorizontalAlign="Left" />    
                              <HeaderStyle HorizontalAlign="Left" />    
                              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />    
                          </asp:TemplateField>
                              </Columns>
                              <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />    
                              <SelectedRowStyle BackColor="#C8E4E6" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#C8E4E6" /> <AlternatingRowStyle BackColor="White" /> </asp:GridView> </asp:Panel>
                          </td>
                        </tr>
                        <tr>
                          <td align="right"> <br />
                              <asp:Button ID="btnSalvarStatus" Width="80px" CssClass="botao" runat="server" Text="Salvar" OnClick="btnSalvarStatus_Click" />      
                      </td>
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
                        