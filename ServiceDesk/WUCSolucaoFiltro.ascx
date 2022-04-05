<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCSolucaoFiltro.ascx.cs" Inherits="WUCSolucaoFiltro" %>
<%@ Register Src="WUCSolucao.ascx" TagName="WUCSolucao" TagPrefix="uc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<table width="100%" border="0" align="left" cellpadding="0" cellspacing="0">
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
    <tr>
        <td align="center" valign="top">
                   <div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
					<table width="776" border="0" cellspacing="5" cellpadding="0">
  <tr>
    <td width="60" align="center" valign="bottom"><asp:Image ID="imgIcone" runat="server"  /></td>
    <td align="center" valign="bottom"> <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
  </tr>
</table>    
</div>
      </td>
    </tr>
    <tr>
        <td align="left">        
        <asp:Panel ID="Panel1" runat="server" Height="240px" ScrollBars="Vertical" Width="98%" CssClass="dataGrid">        
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                                <td>        
		                <asp:GridView ID="gvSolucao" runat="server" AutoGenerateColumns="False" BorderColor="Gray" CellPadding="4" CssClass="dataGrid" ForeColor="Black" GridLines="None" HorizontalAlign="Left" Width="100%" OnRowCommand="gvSolucao_RowCommand" OnRowDataBound="gvSolucao_RowDataBound" OnRowCancelingEdit="gvSolucao_RowCancelingEdit" OnRowEditing="gvSolucao_RowEditing" BackColor="White" OnRowUpdating="gvSolucao_RowUpdating">
                    <FooterStyle CssClass="footerGrid" />
                    <RowStyle BackColor="#F4FBFA" />
                    <HeaderStyle CssClass="topoGrid" />
                <Columns>
                    <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "projeto_codigo")%>'></asp:Label>
                            <asp:Label ID="lblSolucaoProjetoCodigo" runat="server" visible=false Text='<%# DataBinder.Eval(Container.DataItem, "solucao_projeto_codigo")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="1px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descri&#231;&#227;o da solu&#231;&#227;o selecionada">
                        <ItemTemplate>
                            <asp:TextBox CssClass="campo_descricao400" ID="lblNome" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "nome") %>' ></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" Width="400px" />
                        <ItemStyle HorizontalAlign="Left" Width="400px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tipo">
                        <ItemTemplate>
                            <asp:Label ID="lblTipo" Text='<%# DataBinder.Eval(Container.DataItem, "solucao_projeto_tipo_codigo")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="dlTipo" Width="80" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ser&#225; implementada?">
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbSeraImplementado" runat="server" />
                            <asp:Label ID="lblSeraImplementado" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "flag_implementacao")%>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Motivo de n&#227;o implementar">
                        <ItemTemplate>
                            <asp:Label ID="lbMotivoNaoImplemento" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "descricao_nao_implementacao")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMotivoNaoImplemento" ForeColor="Black" Font-Bold=false Text='<%# DataBinder.Eval(Container.DataItem, "descricao_nao_implementacao")%>' Width="100" runat="server"></asp:TextBox>
                        </EditItemTemplate>        
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Wrap="True"  />
                        <HeaderStyle HorizontalAlign="Center" Width="100px" />
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Image" CancelImageUrl="images/icones/voltar.gif" CancelText="Cancelar" EditImageUrl="images/icones/editar.gif" EditText="Editar" ShowEditButton="True" UpdateImageUrl="images/icones/salvar.gif" UpdateText="Salvar" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="40px" />
                    </asp:CommandField>
                    <asp:TemplateField>
                        <ItemTemplate >
                            <a href="Javascript:VisualizaProjeto(<%# DataBinder.Eval(Container.DataItem, "projeto_codigo")%>)">
                                <asp:Image ID="Image1" ImageAlign="Middle" runat=server AlternateText="Detalhe" ImageUrl="~/images/exibir.gif" />
                            </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:TemplateField>        
                    <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="~/images/icones/excluir.gif"
                        Text="Excluir" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20px" />
                    </asp:ButtonField>
                </Columns>
                <PagerStyle BackColor="#F4FBFA" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#C8E4E6" />
            </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="125">
                                <asp:Label ID="lblFiltroPorDescricao" runat="server" Text="Filtro por descrição:"></asp:Label>                          </td>
                            <td>
                                <asp:TextBox ID="txtTabela" runat="server" Visible="false" Width="12px"></asp:TextBox>
                                <asp:TextBox ID="txtTabelaIdentificador" runat="server" Visible="false" Width="13px"></asp:TextBox>
                                <asp:TextBox ID="txtFiltroDescricao" runat="server" Width="450px" CssClass="campo_texto"></asp:TextBox>
                            </td>
                            <td width="200" align="right" valign="middle">
                                <asp:HyperLink ID="btnNovaSolucao" runat="server"><span style="padding:1px 10px 1px 10px; border:1px solid #3C8D8E; text-decoration: none; color: #333333; background-color:  #E9F5F5; font:10px Arial; FONT-WEIGHT:bold;cursor:hand; cursor:pointer;">Novo</span></asp:HyperLink>            
                                <asp:Button ID="btnAssociar" runat="server" Text="Associar" Width="60px" OnClick="btnAssociar_Click" CssClass="botao" Height="16px" />
                          <asp:Button ID="btnFiltrar" runat="server" CssClass="botao" Text="Filtrar" Width="60px" OnClick="btnFiltrar_Click" Height="16px" />                          </td>                    
                        </tr>
                </table>
                </td>
            </tr>
            <tr>
            <td style="height: 154px">
                <asp:GridView ID="gvSolucaoNaoSelecionadas" runat="server" AutoGenerateColumns="False" BorderColor="Gray" CellPadding="4" CssClass="dataGrid" ForeColor="Black" GridLines="None" HorizontalAlign="Left" Width="97%" BackColor="White">
            <FooterStyle CssClass="footerGrid" />
            <RowStyle BackColor="#F4FBFA" />
            <HeaderStyle CssClass="topoGrid" />
                <Columns>
                    <asp:TemplateField HeaderText="C&#243;digo" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "projeto_codigo")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="1px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblEspaco" runat="server" Text=''></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="7px" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="ckbAssociaNao" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="5px" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descri&#231;&#227;o da solu&#231;&#227;o n&#227;o selecionada">
                        <ItemTemplate>
                            <asp:TextBox CssClass="campo_descricao650" ID="lblNome" runat="server" TextMode="MultiLine"  Text='<%# DataBinder.Eval(Container.DataItem, "nome") %>' ></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="Javascript:VisualizaProjeto(<%# DataBinder.Eval(Container.DataItem, "projeto_codigo")%>)">
                                <asp:Image ID="Image1" runat=server AlternateText="Detalhe" ImageUrl="~/images/exibir.gif" />
                            </a>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>                
                </Columns>
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                <EditRowStyle BackColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
              </td>
            </tr>
          </table>
        </asp:Panel>
		</td>
    </tr>
</table>