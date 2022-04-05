<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCParametro.ascx.cs" Inherits="WUCParametro2" %>
<script language="javascript">
    function verifica() {
	    if (confirm("Deseja mesmo excluir este item?")) {
		    return true;
	    }
	    else {
		    return false;
	    }
    }
    
    function LimitaCampo(texto,capacidade) 
	{ 
		var Tecla = event.which;
		if(Tecla == null)
		{
			Tecla = event.keyCode;
			if ( Tecla != 46 &&  Tecla != 8 && texto.length > capacidade)
			{
				event.returnValue = false;
				return false
			} 
		}
	}
</script>  
<tr>
    <td>
	    <div id="divMensagem" align="center" style="width: 100%;" runat="server" class="Mensagem" visible="false">
		    <table width="776" border="0" cellspacing="5" cellpadding="0">
                <tr>
                    <td width="60" align="center" valign="bottom">
                        <asp:Image ID="imgIcone" runat="server"  />
                    </td>
                    <td align="center" valign="bottom"> 
                        <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label>
                    </td>
                </tr>
            </table>    
        </div>
	</td>
</tr>
<tr id="Tr1" runat=server>
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
                          <td align="left" valign="middle" class="tituloFont">Par&acirc;metro</td>
                        </tr>
                    </table></td>
                    <td width="8" class="dir_top"></td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top" class="fundo_tabela"><table width="770" border="0" cellpadding="0" cellspacing="2" >
                  <tr>
                    <td width="89" align="left"> Aplica&ccedil;&atilde;o: </td>
                    <td align="left" colspan="2">
                      <asp:DropDownList ID="ddlAplicacao" Width="305px" runat="server"> </asp:DropDownList>
                    </td>
                    <td align="left">Grupo: </td>
                    <td align="left"><asp:DropDownList ID="ddlGrupo" runat="server" Font-Names="Trebuchet MS" Font-Size="10pt" Width="311px"> </asp:DropDownList></td>
                  </tr>
                  <tr>
                    <td align="left" width="89"> Identificador: </td>
                    <td colspan="4" align="left" valign="top">
                      <asp:TextBox ID="txtIdentificador" Width="685px" MaxLength="255" runat="server" CssClass="campo_texto"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td align="left" width="89"> Tipo: </td>
                    <td colspan="2" align="left" valign="top">
                      <asp:DropDownList ID="ddlTipo" runat="server" Width="305px" CssClass="campo_texto">
                        <asp:ListItem Value="0">Selecione o tipo</asp:ListItem>
                        <asp:ListItem Value="1">Texto</asp:ListItem>
                        <asp:ListItem Value="2">N&#250;mero</asp:ListItem>
                        <asp:ListItem Value="3">Data</asp:ListItem>
                      </asp:DropDownList>
                    </td>
                    <td align="left" valign="middle" width="89">Valor: </td>
                    <td width="310" align="left" valign="top">
                      <asp:TextBox ID="txtValor" Width="307px" MaxLength="255" runat="server" CssClass="campo_texto"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td align="left" width="89"> Descri&ccedil;&atilde;o: </td>
                    <td colspan="4" align="left" valign="top">
                      <asp:TextBox ID="txtDescricao" TextMode="MultiLine" onKeyDown="javascript:LimitaCampo(this.value,255)" Width="685px" Height="100px" runat="server" CssClass="campo_texto"></asp:TextBox>
                    </td>
                  </tr>
                  <tr>
                    <td height="19" colspan="7" align="center" valign="middle">
                      <asp:Button ID="btnSalva" CssClass="botao" Width="80px" Text="Salvar" runat="server" OnClick="btnSalva_Click" />    
&nbsp;
                  <asp:Button ID="btnNovo" CssClass="botao" Width="80px" Text="Novo subitem" runat="server" OnClick="btnNovo_Click" />
&nbsp;
                  <asp:Button ID="btnExcluir" CssClass="botao" Width="80px" Text="Excluir" runat="server" OnClick="btnExcluir_Click" />
                  <asp:Button ID="btnProcurar" CssClass="botao" Width="80px" Text="Procurar" runat="server" OnClick="btnProcurar_Click" />
                    </td>
                  </tr>
                  <tr>
                    <td align="left" class="menu" valign="top" colspan="5">
                      <asp:Panel ID="pnlTreeview" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" runat="server" ScrollBars="Vertical" Height="200px" CssClass="dataGrid"> <asp:TreeView ID="tvParametro" PopulateNodesFromClient="true" ShowLines="true" ShowExpandCollapse="true" runat="server" OnTreeNodePopulate="tvParametro_TreeNodePopulate" OnSelectedNodeChanged="tvParametro_SelectedNodeChanged"> </asp:TreeView> </asp:Panel>
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
    </table></td>
  </tr>
</table>	</td>
</tr>
<tr>
    <td>&nbsp;  </td>
</tr>