<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCAprovador.ascx.cs" Inherits="WUCAprovador" %>
<table border="0" cellpadding="0" cellspacing="0" style="width: 762px">
    <tr>
        <td>
            <div id="divMensagem" runat="server" align="center" class="Mensagem" style="width: 100%" visible="false">
                <table width="650px" border="0" cellpadding="0" cellspacing="5">
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
        <td align="center" valign="middle">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr align="left" valign="middle">
                    <td width="50">
          <asp:Label ID="Label1" runat="server" Text="Empresa:"></asp:Label></td>
                    <td>
            <asp:DropDownList ID="ddlEmpresa" runat="server" Width="269px" OnSelectedIndexChanged="ddlEmpresa_SelectedIndexChanged">
          </asp:DropDownList></td>
                    <td width="80">
                  <asp:Label ID="Label3" runat="server" Text="Pessoa:"></asp:Label>                  </td>
                    <td>
                  <asp:TextBox ID="txtPessoa" runat="server" CssClass="campo_texto" Width="200px"></asp:TextBox>                  </td>
                    <td align="center"><asp:Button ID="btnFiltrar" runat="server" CssClass="botao" OnClick="btnFiltrar_Click" Width="60px" Text="Filtrar" />          </td>
              </tr>
                <tr align="left" valign="middle">
                  <td colspan="2"><table width="60%" border="0" cellpadding="0" cellspacing="1">
                    <tr align="center" valign="middle">
                      <td width="100">
                        <asp:LinkButton ID="lkbMarcarTodos" CssClass="menu" runat="server" OnClick="MarcarTodas" Width="96px">Marcar Todos</asp:LinkButton></td>
                      <td width="120">
                        <asp:LinkButton ID="lkbDesmarcarTodos" CssClass="menu" runat="server" OnClick="DesmarcarTodas"
                Width="107px">Desmarcar Todos</asp:LinkButton></td>
                    </tr>
                  </table></td>
                  <td><asp:Label ID="Label4" runat="server" Text="Tipo Notificação:"></asp:Label></td>
                  <td><asp:DropDownList ID="ddlTipoNotificacao" runat="server" Width="204px">
                        </asp:DropDownList><asp:TextBox ID="txtTabela" runat="server" Visible="False" Width="14px"></asp:TextBox>
            <asp:TextBox ID="txtTabelaIdentificador" runat="server" Visible="False" Width="14px"></asp:TextBox></td>
                  <td align="center"><asp:Button ID="btnSalvar" runat="server" CssClass="botao" Text="Salvar" Width="60px" OnClick="btnSalvar_Click" /></td>
                </tr>
          </table>
      </td>
    </tr>
    <tr>
        <td align="left" valign="top">
</td>
    </tr>
    <tr>
        <td>
		<asp:Panel ID="pnlPessoa" runat="server" Height="110px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
            <asp:GridView ID="gvPessoa" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="Gray" CellPadding="4" CssClass="dataGrid" ForeColor="Black" GridLines="None"
                HorizontalAlign="Left" Width="98%">
                <FooterStyle CssClass="footerGrid" />
                <RowStyle BackColor="#F4FBFA" />
                <Columns>
                    <asp:TemplateField HeaderText="CodigoPessoa" Visible=False>
                        <ItemTemplate>
                            <asp:Label ID="lblCodigoPessoa" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox runat="server" ID="cbxPessoa"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nome do colaborador">
                        <FooterStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="lblPessoa" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                <HeaderStyle CssClass="topoGrid" />
                <EditRowStyle BackColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
          </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="left" valign="middle">
		<table border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td id="aba_esq1" runat="server" class="aba_esquerda_off">&nbsp;
                  </td>
                  <td id="aba1" runat="server" class="aba_centro_off">
                    <asp:Label ID="Label2" runat="server" Text="Histórico de Aprovadores \ Reprovadores" Width="267px"></asp:Label></td>
                  <td id="aba_dir1" runat="server" class="aba_direita_off">&nbsp;
                  </td>
                </tr>
          </table>
      </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlHistorico" runat="server" Height="110px" Width="100%" ScrollBars="Vertical" CssClass="dataGrid">
            <asp:GridView ID="gvHistorico" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="Gray" CellPadding="4" ForeColor="Black" GridLines="None"
                HorizontalAlign="Left" Width="98%" Height="1px" OnRowDataBound="gvHistorico_RowDataBound">
                <FooterStyle CssClass="footerGrid" />
                <RowStyle BackColor="#F4FBFA" />
                <Columns>
                    <asp:TemplateField HeaderText="NotificacaoCodigo" Visible =False >
                        <ItemTemplate>
                            <asp:Label ID="lblNotificacaoCodigo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "notificacao_codigo")%>'></asp:Label>
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Para quem">
                        <ItemTemplate>
                            <asp:Label ID="lblPessoa" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "nome_receptor")%>'></asp:Label>  
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descri&#231;&#227;o">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDescricaoNotificacao" CssClass="campo_descricao400" TextMode="MultiLine" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "descricao")%>'></asp:TextBox> 
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Data solicita&#231;&#227;o" >
                        <ItemTemplate>
                            <asp:Label ID="lblDataSolicitacao" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_inclusao")%>'></asp:Label>  
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Data Resposta">
                        <ItemTemplate>
                            <asp:Label ID="lblDataResposta" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "data_resposta")%>'></asp:Label>  
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "flag_aprovado")%>'></asp:Label>  
                        </ItemTemplate> 
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" />
                <HeaderStyle CssClass="topoGrid" />
                <EditRowStyle BackColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
