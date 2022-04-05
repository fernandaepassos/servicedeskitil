// JScript File

var opcao = "toolbar=no,width=" + (screen.availWidth * 0.8) + ",height=" + (screen.availHeight * 0.7);
				
function NoveJanela(strLink) 
{ 
    var retorno = window.open(strLink, "", "width=784px,height=562px") 
    
}

function NovaJanela(strLink, strTamanhoJanela) 
{ 
    var retorno = window.open(strLink, "", "toolbar=no," + strTamanhoJanela) 
    retorno.moveTo(1,1);
}

function NovaJanelaDashBoard(strLink, strTamanhoJanela) 
{ 
    var retorno = window.open(strLink, "", "scrollbars=yes,toolbar=no," + strTamanhoJanela) 
    retorno.moveTo(1,1);
}

function VisualizaSobre(sobre) 
{ 
    var strLink = "sobre.htm?sobre="+sobre;
    var retorno = window.open(strLink, "", "width=271px,height=280px")
    retorno.moveTo(1,1);
}


function VisualizaChamado(chamado) 
{ 
    var strLink = "ChamadoOperador.aspx?chamado="+chamado;
    var retorno = window.open(strLink, '_blank') 
}

function VisualizaIncidente(incidente) 
{ 
    var strLink = "incidente.aspx?incidente="+incidente;
    var retorno = window.open(strLink, "", "width=1000px,height=700px")
    retorno.moveTo(1,1);
}

function VisualizaConhecimento(conhecimento) 
{ 
    var strLink = "conhecimento.aspx?conhecimento="+conhecimento;
    var retorno = window.open(strLink, "", "width=1000px,height=700px")
    retorno.moveTo(1,1);
}

function VisualizaRequisicaoServico(RequisicaoServico) 
{ 
    var strLink = "RequisicaoServico.aspx?RequisicaoServico="+RequisicaoServico;
    var retorno = window.open(strLink, "", "width=1000px,height=700px")
    retorno.moveTo(1,1);
}

function VisualizaRequisicaoMudanca(RequisicaoMudanca) 
{ 
    var strLink = "RequisicaoMudanca.aspx?RequisicaoMudanca="+RequisicaoMudanca;
    var retorno = window.open(strLink, "", "width=1000px,height=700px")
    retorno.moveTo(1,1);
}

function VisualizaRequisicao(RequisicaoServico) 
{ 
    var strLink = "RequisicaoServico.aspx?RequisicaoServico="+RequisicaoServico;
    var retorno = window.open(strLink, "", "width=1000px,height=700px")
    retorno.moveTo(1,1);
}

function VisualizarItemConfiguracao(ItemConfiguracao) 
{ 
    var strLink = "itemconfiguracao.aspx?codigo="+ItemConfiguracao;
    var retorno = window.open(strLink, "", "toolbar=no,width=784,height=562");
    retorno.moveTo(1,1);
}

function VisualizaEstruturaOrganizacional(codigo) 
{ 
    var strLink = "EstruturaOrganizacional.aspx?codestrutura="+codigo;
    var retorno = window.open(strLink, "", "width=784px,height=562px") 
    retorno.moveTo(1,1);
}

function VisualizaAplicacao(codigo) 
{ 
    var strLink = "Aplicacao.aspx?codaplicacao="+codigo;
    var retorno = window.open(strLink, "", "width=784px,height=562px") 
    retorno.moveTo(1,1);
}

function VisualizaProjeto(projeto_codigo) 
{ 
    var strLink = "Projeto.aspx?projeto_codigo="+projeto_codigo;
    var retorno = window.open(strLink, "", "width=784px,height=562px")
    retorno.moveTo(1,1);
}

function VisualizaProjetoNovo(tabela, tabela_identificador) 
{ 
    var strLink = "Projeto.aspx?tabela="+tabela+"&tabela_identificador="+tabela_identificador;
    var retorno = window.open(strLink, "", "width=784px,height=562px") 
    retorno.moveTo(1,1);
}

function AbrirIndicadores(strLink) 
{ 
    var retorno = window.open(strLink) 
    retorno.moveTo(1,1);
}
