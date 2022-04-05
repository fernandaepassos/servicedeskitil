<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCPessoa.ascx.cs" Inherits="WUCPessoa" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>
<script language="javascript">
<!--
function verifica() {
	if (confirm("Deseja mesmo excluir a pessoa?")) {
		return true;
	}
	else {
		return false;
	}
}
function SomenteNumeros() {
  if ( (event.keyCode >= 48) && (event.keyCode <= 57))
  {
    return true;
  }
  else
  {
    if (event.keyCode != 8)
      {
        event.keyCode = 0
        return false
      }
  }
}
function Limpar(valor, validos) {
// retira caracteres invalidos da string
var result = "";
var aux;
for (var i=0; i < valor.length; i++) {
aux = validos.indexOf(valor.substring(i, i+1));
if (aux>=0) {
result += aux;
}
}
return result;
}
function FormataValor(campo,tammax,teclapres) {
var tecla = teclapres.keyCode;
vr = Limpar(campo.value,"0123456789");
tam = vr.length;
if (tam < tammax && tecla != 8){ tam = vr.length + 1 ; }

if (tecla == 8 ){ tam = tam - 1 ; }

if ( tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105 ){
if ( tam <= 2 ){ 
campo.value = vr ; }
if ( (tam > 2) && (tam <= 5) ){
campo.value = vr.substr( 0, tam - 2 ) + ',' + vr.substr( tam - 2, tam ) ; }
if ( (tam >= 6) && (tam <= 8) ){
campo.value = vr.substr( 0, tam - 5 ) + '.' + vr.substr( tam - 5, 3 ) + ',' +
vr.substr( tam - 2, tam ) ; }
if ( (tam >= 9) && (tam <= 11) ){
campo.value = vr.substr( 0, tam - 8 ) + '.' + vr.substr( tam - 8, 3 ) + '.' +
vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
if ( (tam >= 12) && (tam <= 14) ){
campo.value = vr.substr( 0, tam - 11 ) + '.' + vr.substr( tam - 11, 3 ) + '.' +
vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam
- 2, tam ) ; }
if ( (tam >= 15) && (tam <= 17) ){
campo.value = vr.substr( 0, tam - 14 ) + '.' + vr.substr( tam - 14, 3 ) + '.' +
vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam
- 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ;}
} 

}
-->
</script>
<div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
  <table width="776" border="0" cellspacing="5" cellpadding="0">
    <tr>
      <td width="60" align="center" valign="bottom">
        <asp:Image ID="imgIcone" runat="server" /></td>
      <td align="center" valign="bottom">
        <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
    </tr>
  </table>
</div>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <asp:Panel ID="pnGrid" runat="server">
  <tr>
    <td height="20" align="center" valign="top">&nbsp;      </td>
  </tr>
  <tr>
    <td align="center" valign="top">
      <table width="97%"  border="0" cellspacing="0" cellpadding="0">
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
                            <td width="170" align="left" valign="middle" class="tituloFont">Pessoas</td>
                            <td width="20" align="left" valign="middle" class="tituloFont">&nbsp;</td>
                            <td align="right" valign="middle">
                           <asp:ImageButton ID="imgNovaPessoa" ImageUrl="images/icones/inserir.gif" AlternateText="Nova Pessoa" runat="server" OnClick="imgNovaPessoa_Click" />&nbsp;Nova Pessoa </td>
                            <td width="80" align="right" valign="middle"></td>
                          </tr>
                      </table></td>
                      <td width="8" class="dir_top"></td>
                    </tr>
                </table></td>
              </tr>
              <tr>
                <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td align="center" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="0">
                      <tr>
                        <td width="45" align="left">Nome:</td>
                        <td>
                          <asp:TextBox ID="txtNomePesquisa" MaxLength="120" Width="100" runat="server" CssClass="campo_texto"></asp:TextBox>
                        </td>
                        <td width="60" align="left">Matr&iacute;cula:</td>
                        <td align="left">
                          <asp:TextBox ID="txtMatriculaPesquisa" MaxLength="15" Width="60" runat="server" CssClass="campo_texto"></asp:TextBox>
                        </td>
                        <td>
                          <asp:DropDownList ID="ddlEstruturaOrganizacionalPesquisa" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                          <asp:DropDownList ID="ddpTipoUsuarioPesquisa" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                          <asp:Button ID="btnPesquisa" Text="Buscar" runat="server" CssClass="botao" OnClick="pesquisar" />                    
    </td>
                      </tr>
                    </table></td>
                  </tr>
                  <tr>
                    <td align="left" valign="top">
					<asp:Panel ID="pnlGridPessoa"  runat="server" ScrollBars="Vertical" Height="420px" Width="100%" CssClass="dataGrid">
					<asp:GridView ID="gvPessoa" runat="server" Width="98%" CellPadding="4" GridLines="None" OnRowCommand="gvPessoa_RowCommand" AllowPaging="True" AutoGenerateColumns="False" PageSize="12" OnRowDataBound="gvPessoa_RowDataBound"  OnPageIndexChanging="gvPessoa_PageIndexChanging"> <RowStyle BackColor="#F4FBFA" />
            <HeaderStyle CssClass="topoGrid" />    
            <PagerStyle CssClass="menu" HorizontalAlign="Center" />    
            <SelectedRowStyle BackColor="#C8E4E6" Font-Bold="True" ForeColor="#333333" /> <EditRowStyle BackColor="#2461BF" /> <AlternatingRowStyle BackColor="White" />
            <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="images/icones/editar.gif" ShowSelectButton="True" SelectText="Selecionar">
            <ItemStyle Width="30px" />    
                <HeaderStyle Width="30px" />
        </asp:CommandField> <asp:TemplateField HeaderText="C&#243;digo">
        <ItemTemplate>
          <asp:Label ID="lblCodigo" Text='<%# DataBinder.Eval(Container.DataItem, "pessoa_codigo") %>' runat="server"></asp:Label>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Width="30px" />
        <ItemStyle HorizontalAlign="Center" Width="30px" />
        </asp:TemplateField> <asp:TemplateField HeaderText="Matr&#237;cula">
        <ItemTemplate>
          <asp:Label ID="lblMatricula" Text='<%# DataBinder.Eval(Container.DataItem, "matricula")%>' runat="server"></asp:Label>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Center" Width="80px" />
        <ItemStyle HorizontalAlign="Center" Width="80px" />
        </asp:TemplateField> <asp:TemplateField HeaderText="Nome">
        <ItemTemplate>
          <asp:Label ID="lblNome" Text='<%# DataBinder.Eval(Container.DataItem, "nome")%>' runat="server"></asp:Label>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField> <asp:TemplateField HeaderText="Empresa">
        <ItemTemplate>
          <HeaderStyle HorizontalAlign="Left" Width="170px" />
          <ItemStyle HorizontalAlign="Left" Width="170px" />
          <asp:Label ID="lblEstruturaOrganizacional" Text='<%# DataBinder.Eval(Container.DataItem, "estrutura_codigo")%>' runat="server"></asp:Label>
        </ItemTemplate>
        <HeaderStyle HorizontalAlign="Left" />
        <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField> <asp:TemplateField HeaderText="Tipo de Usu&#225;rio">
        <ItemTemplate>
          <asp:Label ID="lblTipoUsuario" Text='<%# DataBinder.Eval(Container.DataItem, "tipo_usuario_codigo")%>' runat="server"></asp:Label>
        </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField> <asp:ButtonField ButtonType="Image" CommandName="Excluir" ImageUrl="images/icones/excluir.gif" Text="Excluir">
        <ItemStyle HorizontalAlign="Center" Width="30px" />
            <HeaderStyle Width="30px" />
        </asp:ButtonField>
            </Columns>
      </asp:GridView> </asp:Panel>
					</td>
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
      </table>
	  </td>
  </tr>
  </asp:Panel>
  <asp:Panel ID="pnPessoa" Visible="false" runat="server"> 
  <tr>
    <td height="20" align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="center" valign="top"><table width="97%"  border="0" cellspacing="0" cellpadding="0" id="Table1">
      <tr>
        <td align="center" valign="top"><table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table2">
            <!--DWLayoutTable-->
            <tr>
              <td height="22" colspan="3">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table3">
                  <!--DWLayoutTable-->
                  <tr>
                    <td width="8" height="22" class="esq_top">&nbsp;</td>
                    <td align="left" valign="top" class="centro_top"><table width="100%" height="22"  border="0" cellpadding="0" cellspacing="0">
                        <tr>
                          <td width="20" align="center" valign="middle">&nbsp;</td>
                          <td width="170" align="left" valign="middle" class="tituloFont">Nova Pessoa</td>
                          <td align="left" valign="middle" class="tituloFont">&nbsp;</td>
                          <td align="right" valign="middle"> </td>
                          <td width="80" align="right" valign="middle"></td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela">
			  <table width="776"  border="0" cellspacing="2" cellpadding="0">
                  <tr align="left" valign="middle">
                    <td width="110">Nome:</td>
                    <td><asp:TextBox ID="txtNome" CssClass="campo_texto" MaxLength="120" Width="266px" runat="server"></asp:TextBox></td>
                    <td width="104">Matr&iacute;cula:</td>
                    <td width="273"><asp:TextBox ID="txtMatricula" CssClass="campo_texto" MaxLength="15" Width="266px" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Empresa:</td>
                    <td><asp:DropDownList ID="ddlEstruturaOrganizacional" Width="269px" runat="server"></asp:DropDownList></td>
                    <td>Cargo:</td>
                    <td><asp:DropDownList ID="ddlCargo" Width="269px" runat="server"></asp:DropDownList></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Usu&aacute;rio:</td>
                    <td><asp:DropDownList ID="ddlTipoUsuario" Width="269px" runat="server"></asp:DropDownList></td>
                    <td>Colaborador:</td>
                    <td><asp:DropDownList ID="ddlTipoColaborador" Width="269px" runat="server"></asp:DropDownList></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Sexo:</td>
                    <td width="279"><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><asp:DropDownList ID="ddlSexo" Width="100px" runat="server"></asp:DropDownList></td>
                          <td width="120" align="right">Nascimento:</td>
                          <td><ew:calendarpopup id="cldDataNascimento" Nullable="true" Width="65" runat="server"> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> </ew:calendarpopup> </td>
                        </tr>
                    </table></td>
                    <td>In&iacute;cio Trabalho: </td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><ew:calendarpopup id="cldDataInicioTrabalho" Nullable="true" Width="65" runat="server"> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> </ew:calendarpopup> </td>
                          <td width="90" align="right">Fim Trabalho :</td>
                          <td align="right"><ew:calendarpopup id="cldDataFimTrabalho" Nullable="true" Width="65" runat="server"> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> </ew:calendarpopup> </td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Logradouro:</td>
                    <td><asp:TextBox ID="txtLogradouro" CssClass="campo_texto" MaxLength="255" Width="266px" runat="server"></asp:TextBox></td>
                    <td>Bairro:</td>
                    <td><asp:TextBox ID="txtBairro" CssClass="campo_texto" Width="266px" MaxLength="50" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Cidade:</td>
                    <td><asp:TextBox ID="txtCidade" CssClass="campo_texto" MaxLength="50" Width="266px" runat="server"></asp:TextBox></td>
                    <td>UF:</td>
                    <td><asp:TextBox ID="txtUf" CssClass="campo_texto" MaxLength="2" Width="266px" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>CPF:</td>
                    <td><asp:TextBox ID="txtCpf" MaxLength="11" CssClass="campo_texto" onKeyPress="SomenteNumeros()" Width="266px" runat="server"></asp:TextBox></td>
                    <td>E-mail:</td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><asp:TextBox ID="txtEmail" MaxLength="300" CssClass="campo_texto" Width="100px" runat="server"></asp:TextBox></td>
                          <td width="90" align="right">CEP:</td>
                          <td><asp:TextBox ID="txtCep" CssClass="campo_texto" MaxLength="10" Width="100px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Telefone</td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><asp:TextBox ID="txtTelefone" CssClass="campo_texto" MaxLength="50" Width="150px" runat="server"></asp:TextBox></td>
                          <td width="50" align="right">Ramal:</td>
                          <td width="50"><asp:TextBox ID="txtRamal" MaxLength="50" CssClass="campo_texto" Width="50px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table></td>
                    <td>Linha de &Ocirc;nibus: </td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><asp:TextBox ID="txtLinhaOnibus" CssClass="campo_texto" onKeyPress="SomenteNumeros()" Width="85px" runat="server"></asp:TextBox></td>
                          <td width="90">Pt. de &Ocirc;nibus: </td>
                          <td><asp:TextBox ID="txtPontoOnibus" CssClass="campo_texto" onKeyPress="SomenteNumeros()" Width="85px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>C&oacute;digo Rede: </td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><asp:TextBox ID="txtCodigoRede" CssClass="campo_texto" MaxLength="30" Width="100px" runat="server"></asp:TextBox></td>
                          <td width="75" align="right">Valor Hora: :</td>
                          <td width="50"><asp:TextBox ID="txtValorHora" CssClass="campo_texto" onKeyPress="SomenteNumeros()" onKeydown="FormataValor(this,10,event)" Width="50px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table></td>
                    <td>Vip:</td>
                    <td>
                      <asp:RadioButtonList ID="rblVip" runat="server" RepeatDirection="Horizontal" Font-Size="8pt" ForeColor="#1D164C" CssClass="TextoValor">
                        <asp:ListItem Value="N" Text="N&atilde;o" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="S" Text="Sim"></asp:ListItem>
                      </asp:RadioButtonList>
                    </td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Localiza&ccedil;&atilde;o F&iacute;sica: </td>
                    <td><asp:TextBox ID="txtLocalizacaoFisica" CssClass="campo_texto" MaxLength="255" Width="266px" runat="server"></asp:TextBox></td>
                    <td>Senha:</td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><asp:TextBox ID="txtSenha" CssClass="campo_texto" TextMode="Password" Width="80px" runat="server"></asp:TextBox></td>
                          <td width="105">Confirmar Senha:</td>
                          <td align="right"><asp:TextBox ID="txtConfirmaSenha" CssClass="campo_texto" TextMode="Password" Width="80px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>Status:</td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><asp:DropDownList ID="ddlStatus" Width="140px" runat="server"></asp:DropDownList></td>
                          <td width="98">Tipo Sanguineo:</td>
                          <td width="30"><asp:TextBox ID="txtTipoSangue" CssClass="campo_texto" MaxLength="3" Width="30px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table></td>
                    <td>Nome de Guerra: </td> 
                    <td><asp:TextBox ID="txtNomeGuerra" CssClass="campo_texto" MaxLength="15" Width="266px" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr align="left" valign="middle">
                    <td>CNH:</td>
                    <td><asp:TextBox ID="txtCnh" CssClass="campo_texto" MaxLength="20" Width="266px" runat="server"></asp:TextBox></td>
                    <td>Expedi&ccedil;&atilde;o:</td>
                    <td><table width="270" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td><ew:calendarpopup id="cldDataExpedicao" Nullable="true" Width="65" runat="server"> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> </ew:calendarpopup> </td>
                          <td width="50">Validade:</td>
                          <td align="right"><ew:calendarpopup id="cldDataValidade" Nullable="true" Width="65" runat="server"> <ButtonStyle CssClass="botao" /> <TextboxLabelStyle CssClass="campo_texto" /> </ew:calendarpopup> </td>
                        </tr>
                    </table></td>
                  </tr>
                  <tr align="center" valign="middle">
                    <td colspan="4">
                      <asp:Button ID="btnSalvar" Text="Salvar" Width="60" runat="server" CssClass="botao" OnClick="salvaPessoa" />    
                      <asp:Button ID="btnLista" Text="Voltar" Width="60" runat="server" CssClass="botao" OnClick="listaPessoa" />    
                </td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="left" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table4">
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
  </asp:Panel>
</table>
