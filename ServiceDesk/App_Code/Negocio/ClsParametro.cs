using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe Parametro
/// </summary>
public class ClsParametro
{
    #region Declaração dos atributos
    //Colecao de atributos de Parametro
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um Parametro
    private ServiceDesk.Banco.ClsAtributo objParametroCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objParametroCodigoSuperior = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAplicacao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objIdentificador = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objValor = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    #endregion

    #region Propriedades

    public static string ChamadoPrefixo;
    public static string CodigoDoSistema;
    public static string Helpdesk;
    public static string DataInclusao;
    public static string DataSimplesBanco;
    public static string DataSimplesExibicao;
    public static string DataCompletaExibicao;
    public static string DataMinimaSistema;
    public static string CodigoStatusSolicitado;
    public static string CodigoStatusIniciado;
    public static string CodigoStatusCancelado;
    public static string CodigoStatusExecutado;
    public static string CodigoStatusAprovado;
    public static string CodigoStatusFinalizado;
    public static string CodigoStatusAceito;
    public static string CodigoStatusNaoAceito;
    public static string CodigoStatusReprovado1Nivel;
    public static string CodigoStatusReprovado2Nivel;
    public static string CodigoStatusReprovado3Nivel;
    public static string CodigoEmpresa;
    public static string CodigoTipoEmpresa;
    public static string CodigoTipoArea;
    public static string CodigoTipoUnidade;
    public static string SMTP;
    public static string SMTPPorta;
    public static string ItemRelacaoTipoPai;
    public static string ItemRelacaoTipoFilho;
    public static string CodigoTipoLogInclusao;
    public static string CodigoTipoLogAlteracao;
    public static string CodigoTipoLogExclusao;
    public static string CodigoTipoLogSeguranca;
    public static string CodigoTipoChamadoIncidente;
    public static string CodigoTipoChamadoRequisicaoMudanca;
    public static string CodigoTipoChamadoServico;
    public static string CodigoTipoChamadoReclamacao;
    public static string CodigoTipoChamadoErro;
    public static string EquipeAtendimentoPadrao;
    public static string TipoItemConfiguracaoServico;
    public static string NivelAtendimentoPadrao;
    public static string QuantidaMaximaSelecaoServicos;
    public static string RedirecionamentoAberturaChamadoOperador;
    public static string RedirecionamentoAberturaChamado;
    public static string CodigoUsuarioAdministrador;
    public static string CodigoUsuarioFinal;
    public static string ADPath;
    public static string ADServer;
    public static string EscalacaoLivre;
    public static string EscalacaoLivreBloqueiaSelecaoNivelInferior;
    public static string URLRelatorios;
    public static string URLIndicadores;
    public static string TempoAtualizacaoSemaforo;
    public static string TempoAtualizacaoAgente;
    public static string BloqueiaEscalacaoHorizontalChamado;
    public static string CodigoNivel1;
    public static string CodigoAnalistaServiceDesk;
    public static string BloqueiaCampoParaNivelMaior1;
    public static string CodigoTipoAprovacao;
    public static string CodigoTipoAceitacao;

    /// <summary>
    /// Coleção de atributos.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributos Atributos
    {
        get { return this.objAtributos; }
    }

    /// <summary>
    /// Código do Parametro.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo ParametroCodigo
    {
        get { return objParametroCodigo; }
        set { this.objParametroCodigo = value; }
    }

    /// <summary>
    /// Código do Parametro superior.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo ParametroCodigoSuperior
    {
        get { return objParametroCodigoSuperior; }
        set { this.objParametroCodigoSuperior = value; }
    }

    /// <summary>
    /// Código da Aplicação
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Aplicacao
    {
        get { return objAplicacao; }
        set { this.objAplicacao = value; }
    }

    /// <summary>
    /// Código do Identificador
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Identificador
    {
        get { return objIdentificador; }
        set { this.objIdentificador = value; }
    }

    /// <summary>
    /// Tipo do Parametro
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Tipo
    {
        get { return objTipo; }
        set { this.objTipo = value; }
    }

    /// <summary>
    /// url do Item
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Valor
    {
        get { return objValor; }
        set { this.objValor = value; }
    }

    /// <summary>
    /// Parâmetro
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
        get { return objDescricao; }
        set { this.objDescricao = value; }
    }


    #endregion

    #region metodo Construtor da classe
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsParametro()
    {
        this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsParametro(int intCodigo)
    {
        try
        {
            this.alimentaColecaoCampos();
            this.objParametroCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
        objAtributos.NomeTabela = "Parametro";
        objAtributos.DescricaoTabela = "Parametro";

        objParametroCodigo.Campo = "parametro_codigo";
        objParametroCodigo.Descricao = "Código";
        objParametroCodigo.CampoIdentificador = true;
        objParametroCodigo.CampoObrigatorio = true;
        objParametroCodigo.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objParametroCodigo);

        objParametroCodigoSuperior.Campo = "parametro_codigo_superior";
        objParametroCodigoSuperior.Descricao = "Código Superior da Parametro";
        objParametroCodigoSuperior.CampoIdentificador = false;
        objParametroCodigoSuperior.CampoObrigatorio = true;
        objParametroCodigoSuperior.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objParametroCodigoSuperior);

        objAplicacao.Campo = "aplicacao";
        objAplicacao.Descricao = "aplicacao";
        objAplicacao.CampoIdentificador = false;
        objAplicacao.CampoObrigatorio = false;
        objAplicacao.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objAplicacao);

        objIdentificador.Campo = "identificador";
        objIdentificador.Descricao = "Identificador";
        objIdentificador.CampoIdentificador = false;
        objIdentificador.CampoObrigatorio = false;
        objIdentificador.Tipo = System.Data.DbType.String;
        objIdentificador.Tamanho = 255;
        objAtributos.Add(objIdentificador);

        objTipo.Campo = "tipo";
        objTipo.Descricao = "tipo";
        objTipo.CampoIdentificador = false;
        objTipo.CampoObrigatorio = false;
        objTipo.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objTipo);

        objValor.Campo = "valor";
        objValor.Descricao = "valor";
        objValor.CampoIdentificador = false;
        objValor.CampoObrigatorio = false;
        objValor.Tipo = System.Data.DbType.String;
        objValor.Tamanho = 255;
        objAtributos.Add(objValor);

        objDescricao.Campo = "Descricao";
        objDescricao.Descricao = "Descricao";
        objDescricao.CampoIdentificador = false;
        objDescricao.CampoObrigatorio = false;
        objDescricao.Tipo = System.Data.DbType.String;
        objDescricao.Tamanho = 255;
        objAtributos.Add(objDescricao);
    }
    #endregion

    #region Popula dropDown aplicacao
    /// <summary>
    /// Popula DropDownList Aplicação
    /// </summary>
    /// <returns>Retorna DataReader</returns>
    public System.Data.SqlClient.SqlDataReader PopulaDropDownAplicacao()
    {
        try
        {
            String strSql = "SELECT aplicacao_codigo, descricao FROM aplicacao ORDER BY aplicacao_codigo";
            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PopulaNivelRaiz
    /// <summary>
    /// Método que popula o nó raiz da TreeView
    /// </summary>
    /// <param name="strCodigoAplicacao">Código da aplicação</param>
    /// <returns>DataReader</returns>
    public System.Data.SqlClient.SqlDataReader PopulaNivelRaiz(String strCodigoAplicacao)
    {
        String strSql;
        try
        {
            strSql = "SELECT parametro_codigo, tipo, valor, identificador,(SELECT COUNT(*) FROM Parametro ";
            strSql += "WHERE parametro_codigo_superior = Paran.parametro_codigo) NumNosFilho FROM ";
            strSql += "Parametro Paran WHERE Paran.aplicacao = " + strCodigoAplicacao;
            strSql += " AND parametro_codigo_superior IS NULL ";
            strSql += " ORDER BY identificador";

            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region PopulaSubNivel
    /// <summary>
    /// Método que popula os sub níveis da treeview
    /// </summary>
    /// <param name="CodigoNivelSuperior">Código do nivel superior</param>
    /// <param name="strCodigoAplicacao">Código da aplicação</param>
    /// <returns>DataReader</returns>
    public System.Data.SqlClient.SqlDataReader PopulaSubNivel(int CodigoNivelSuperior, String strCodigoAplicacao)
    {
        String strSql;
        try
        {
            strSql = "SELECT parametro_codigo, tipo, valor, identificador,(SELECT COUNT(*) FROM Parametro ";
            strSql += "WHERE parametro_codigo_superior = Paran.parametro_codigo) NumNosFilho FROM ";
            strSql += "Parametro Paran WHERE Paran.aplicacao = " + strCodigoAplicacao;
            strSql += " AND Parametro_codigo_superior = " + CodigoNivelSuperior;
            strSql += " ORDER BY identificador";

            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region CarregaParametros
    /// <summary>
    /// Método que retorna o valor do Parâmetro
    /// </summary>
    public static void CarregaParametros()
    {
        try
        {
            ChamadoPrefixo = RetornaValorParametro("CHAMADOPREFIXO");
            CodigoDoSistema = RetornaValorParametro("CODIGODOSISTEMA");
            Helpdesk = RetornaValorParametro("HELPDESK");
            DataInclusao = RetornaValorParametro("DATAINCLUSAO");
            DataSimplesBanco = RetornaValorParametro("DATASIMPLESBANCO");
            DataSimplesExibicao = RetornaValorParametro("DATASIMPLESEXIBICAO");
            DataCompletaExibicao = RetornaValorParametro("DATACOMPLETAEXIBICAO");
            DataMinimaSistema = RetornaValorParametro("CODIGOSTATUSSOLICITADO");
            CodigoStatusSolicitado = RetornaValorParametro("CODIGOSTATUSSOLICITADO");
            CodigoStatusIniciado = RetornaValorParametro("CODIGOSTATUSINICIADO");
            CodigoStatusCancelado = RetornaValorParametro("CODIGOSTATUSCANCELADO");
            CodigoStatusExecutado = RetornaValorParametro("CODIGOSTATUSEXECUTADO");
            CodigoStatusAprovado = RetornaValorParametro("CODIGOSTATUSAPROVADO");
            CodigoStatusFinalizado = RetornaValorParametro("CODIGOSTATUSFINALIZADO");
            CodigoEmpresa = RetornaValorParametro("CODIGOEMPRESA");
            CodigoTipoEmpresa = RetornaValorParametro("CODIGOTIPOEMPRESA");
            CodigoTipoArea = RetornaValorParametro("CODIGOTIPOAREA");
            CodigoTipoUnidade = RetornaValorParametro("CODIGOTIPOUNIDADE");
            SMTP = RetornaValorParametro("SMTP");
            SMTPPorta = RetornaValorParametro("SMTPPORTA");
            ItemRelacaoTipoPai = RetornaValorParametro("ITEMRELACAOTIPOPAI");
            ItemRelacaoTipoFilho = RetornaValorParametro("ITEMRELACAOTIPOFILHO");
            CodigoTipoLogInclusao = RetornaValorParametro("CODIGOTIPOLOGINCLUSAO");
            CodigoTipoLogAlteracao = RetornaValorParametro("CODIGOTIPOLOGALTERACAO");
            CodigoTipoLogExclusao = RetornaValorParametro("CODIGOTIPOLOGEXCLUSAO");
            CodigoTipoLogSeguranca = RetornaValorParametro("CODIGOTIPOLOGSEGURANCA");
            CodigoTipoChamadoIncidente = RetornaValorParametro("CODIGOTIPOCHAMADOINCIDENTE");
            CodigoTipoChamadoRequisicaoMudanca = RetornaValorParametro("CODIGOTIPOCHAMADOMUDANCA");
            CodigoTipoChamadoServico = RetornaValorParametro("CODIGOTIPOCHAMADOSERVICO");
            CodigoTipoChamadoReclamacao = RetornaValorParametro("CODIGOTIPOCHAMADORECLAMACAO");
            CodigoTipoChamadoErro = RetornaValorParametro("CODIGOTIPOCHAMADOERRO");
            EquipeAtendimentoPadrao = RetornaValorParametro("EQUIPEATENDIMENTOPADRAO");
            TipoItemConfiguracaoServico = RetornaValorParametro("TIPOITEMCONFIGURACAOSERVICO");
            NivelAtendimentoPadrao = RetornaValorParametro("NIVELATENDIMENTOPADRAO");
            QuantidaMaximaSelecaoServicos = RetornaValorParametro("QUANTIDAMAXIMASELECAOSERVICOS");
            RedirecionamentoAberturaChamadoOperador = RetornaValorParametro("REDIRECIONAMENTOABERTURACHAMADOOPERADOR");
            RedirecionamentoAberturaChamado = RetornaValorParametro("REDIRECIONAMENTOABERTURACHAMADO");
            CodigoUsuarioAdministrador = RetornaValorParametro("CODIGOUSUARIOADMINISTRADOR");
            CodigoUsuarioFinal = RetornaValorParametro("CODIGOUSUARIOFINAL");
            ADPath = RetornaValorParametro("ADPATH");
            EscalacaoLivre = RetornaValorParametro("ESCALACAOLIVRE");
            URLRelatorios = RetornaValorParametro("URLRELATORIOS");
            URLIndicadores = RetornaValorParametro("URLINDICADORES");
            TempoAtualizacaoSemaforo = RetornaValorParametro("TEMPOATUALIZACAOSEMAFORO");
            TempoAtualizacaoAgente = RetornaValorParametro("TEMPOATUALIZACAOAGENTE");
            BloqueiaEscalacaoHorizontalChamado = RetornaValorParametro("BLOQUEIARESCALACAOCHAMADO");
            CodigoNivel1 = RetornaValorParametro("CODIGONIVEL1");
            CodigoAnalistaServiceDesk = RetornaValorParametro("CODIGOANALISTASERVICEDESK");
            BloqueiaCampoParaNivelMaior1 = RetornaValorParametro("BLOQUEIACAMPOSPARANIVELMAIOR1");
            CodigoStatusAceito = RetornaValorParametro("CODIGOSTATUSACEITO");
            CodigoStatusNaoAceito = RetornaValorParametro("CODIGOSTATUSNAOACEITO");
            CodigoTipoAceitacao = RetornaValorParametro("CODIGOTIPOACEITACAO");
            CodigoTipoAprovacao = RetornaValorParametro("CODIGOTIPOAPROVACAO");
            CodigoStatusReprovado1Nivel = RetornaValorParametro("CODIGOSTATUSREPROVADO1NIVEL");
            CodigoStatusReprovado2Nivel = RetornaValorParametro("CODIGOSTATUSREPROVADO2NIVEL");
            CodigoStatusReprovado3Nivel = RetornaValorParametro("CODIGOSTATUSREPROVADO3NIVEL");
            EscalacaoLivreBloqueiaSelecaoNivelInferior = RetornaValorParametro("BLOQUEIASELECAONIVELINFERIOR");

            return;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region RetornaParametro
    /// <summary>
    /// RetornaParametro - Retorna o Pai de um Parametro ou o própio Parametro
    /// </summary>
    /// <param name="intParametroCodigo">Código do pai ou Código do propio item</param>
    /// <returns> Retorna o codigo, codigo superior e descrição do Parametro</returns>
    public System.Data.SqlClient.SqlDataReader RetornaParametro(int intParametroCodigo)
    {
        String strSql;
        try
        {
            strSql = "SELECT parametro_codigo, parametro_codigo_superior, tipo, ";
            strSql += "identificador, aplicacao, valor, Descricao FROM Parametro ";
            strSql += " WHERE Parametro_codigo = 0" + intParametroCodigo;

            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region RetornaValorParametro
    /// <summary>
    /// Método que retorna o valor do Parâmetro
    /// </summary>
    /// <param name="strIdentificador">Identifica o valor do parâmetro que será retornado</param>
    /// <returns>Retorna uma string</returns>
    private static String RetornaValorParametro(String strIdentificador)
    {
        String strSQL = String.Empty;
        SqlDataReader dr;
        String strRetorno = String.Empty;
        try
        {
            strSQL = "SELECT valor FROM Parametro WHERE identificador = '" + strIdentificador.Trim() + "'";
            dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);

            if (dr.Read())
            {
                strRetorno = dr["valor"].ToString();
            }
            dr.Dispose();
            dr = null;

            return strRetorno;
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region ValidaInsercao
    /// <summary>
    /// Método que verifica se existe algum registro com a conbinação das 3 chaves
    /// </summary>
    /// <param name="strAplicacao">Código da aplicação</param>
    /// <param name="strIdentificador">identificador</param>
    public bool ValidaInsercao(String strCodigoParametro, String strAplicacao, String strIdentificador)
    {
        String strSql;
        bool bolRetorno = false;
        try
        {
            strSql = "SELECT parametro_codigo FROM Parametro WHERE aplicacao = '" + strAplicacao + "' AND identificador = '" + strIdentificador + "' ";

            if (strCodigoParametro != String.Empty && strCodigoParametro != null)
            {
                strSql += "AND parametro_codigo <> " + strCodigoParametro;
            }

            SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            if (objReader.Read())
            {
                bolRetorno = true;
            }

            objReader.Dispose();
            objReader = null;

            return bolRetorno;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um novo Parametro.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objAplicacao.Valor.Trim() == String.Empty)
                strMensagem += "Informe a aplicação a qual o item pertence.<br />";

            if (this.objIdentificador.Valor.Trim() == String.Empty)
                strMensagem += "Informe o identificador do item.<br />";

            if (this.objTipo.Valor.Trim() == String.Empty)
                strMensagem += "Informe o tipo do item.<br />";

            if (this.objValor.Valor.Trim() == String.Empty)
                strMensagem += "Informe o valor do item.<br />";

            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Parametro inserida com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            }
            return bolRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo altera
    /// <summary>
    /// Método que altera um Parametro
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objAplicacao.Valor.Trim() == String.Empty)
                strMensagem += "Informe a aplicação a qual o item pertence.<br />";

            if (this.objIdentificador.Valor.Trim() == String.Empty)
                strMensagem += "Informe o identificador do item.<br />";

            if (this.objTipo.Valor.Trim() == String.Empty)
                strMensagem += "Informe o tipo do item.<br />";

            if (this.objValor.Valor.Trim() == String.Empty)
                strMensagem += "Informe o valor do item.<br />";

            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Parametro atualizada com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            }

            return bolRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo exclui
    /// <summary>
    /// Método que exclui um Parametro
    /// </summary>
    /// <param name="intCodigo">Identifica a Parametro a ser excluido</param>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui(String strIdentificador)
    {
        try
        {
            String strSql;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            strSql = "DELETE FROM Parametro WHERE parametro_codigo IN (SELECT parametro_codigo FROM Parametro ";
            strSql += "WHERE identificador = '" + strIdentificador + "' or identificador like '" + strIdentificador + ".%')";

            objBanco.executaSQL(strSql);
            objBanco = null;
            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}