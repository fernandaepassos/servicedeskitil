/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela Problema.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descrição: Esta classe dispõe de vários métodos e atributos que permitem várias ações quanto 
    aos registros da tabela Problema. Esta voltado para o gerenciamento de problemas.
  
  
  • Alterações
  	Data: 14/12/2005
  	Autor: Fernanda Passos
  	Descrição: Inclusão do atributo pessoa_codigo_alocacao na declaração, coleção de atributos e
    inclusão alteração do nome equipe_codigo para equipe_codigo_alocacao.
 
   • Alterações
  	Data: 14/12/2005
  	Autor: Sylvio Neto
  	Descrição: Adicionados os métodos herdaICIncidente e AdicionaRelacaoIncidenteProblema que
    são utilizados para relacionar um incidente ao problema e copiar os itens de configuracao de
    um incidente para um problema.
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe de acesso a dados da tabela Problema.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsProblema
    {
        #region Construtor da classe
        public ClsProblema()
        {
            alimentaColecaoCampos();
        }

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsProblema(int intCodigo)
        {
          this.alimentaColecaoCampos();
          this.objCodigo.Valor = intCodigo.ToString();
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          objBanco.alimentaColecao(this.objAtributos);
          objBanco = null;
        }
        #endregion

        #endregion

        #region Declarações

        

        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de um problema.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDtInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDtAlteracao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoProprietario = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoUsuarioAlterou = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoEquipeAlocacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoPessoaAlocacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoStatus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoProblemaTipo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagFechado = new ServiceDesk.Banco.ClsAtributo();

        #endregion
       
        #region Propriedades
        /// <summary>
        /// Coleção de atributos
        /// </summary>
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        /// <summary>
        /// Codigo do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo ; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Nome do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
            set { this.objNome = value; }
        }

        /// <summary>
        /// Descrição do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Data e hora da inclusão do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DtInclusao
        {
            get { return objDtInclusao; }
            set { this.objDtInclusao = value; }
        }

        /// <summary>
        /// Data e hora da alteração do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DtAlteracao
        {
            get { return objDtAlteracao; }
            set { this.objDtAlteracao = value; }
        }

        /// <summary>
        /// Usuário que cadastrou o problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo UsuarioQueCadastrou
        {
            get { return objCodigoProprietario; }
            set { this.objCodigoProprietario = value; }
        }

        /// <summary>
        /// Usuário fez a ultima alteração no problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo UsuarioQueAlterou
        {
            get { return objCodigoUsuarioAlterou; }
            set { this.objCodigoUsuarioAlterou = value; }
        }

        /// <summary>
        /// Equipe para solução do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoEquipeAlocacao 
        {
            get { return objCodigoEquipeAlocacao; }
            set { this.objCodigoEquipeAlocacao = value; }
        }

        /// <summary>
        /// Pessoa da equipe alocada para solução do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoPessoaAlocacao
        {
            get { return objCodigoPessoaAlocacao; }
            set { this.objCodigoPessoaAlocacao = value; }
        }
        
        /// <summary>
        /// Código do status do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Status
        {
            get { return objCodigoStatus; }
            set { this.objCodigoStatus = value; }
        }

        /// <summary>
        /// Código do tipo do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoProblemaTipo
        {
            get { return objCodigoProblemaTipo; }
            set { this.objCodigoProblemaTipo = value; }
        }

        /// <summary>
        /// Flag para indicar se o problema esta aberto ou fechado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagFechado
        {
            get { return objFlagFechado; }
            set { this.objFlagFechado = value; }
        }
        
    #endregion

        #region Metodos

        #region Validacao dos dados
        /// <summary>
        /// Validação da integridade dos registros.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou não.</returns>
        public bool ValidacaoDadosProblema(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objNome.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o nome do problema.";
                    return false;
                }
                else if (objDescricao.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe a descrição.";
                    return false;
                }
                else if (objCodigoProblemaTipo.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o tipo do problema.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
    
        }
        #endregion

        #region Validacao exclusão
        /// <summary>
        /// Valida exclusão
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou não.</returns>
        public bool ValidaExclusao(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                //Valida a exclusão.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Negocio.ClsMudanca objMudanca = new ClsMudanca();
                //Verifica se pode dessassociar as mudanças do problema
                if (objMudanca.exclui(out strMsg) == false)
                {
                    objMudanca = null;
                    return false;
                }
                objMudanca = null;
                return true;            
            }
            catch(System .Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Inserir
        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                //Verifica se todos os campos foram preenchidos.
                if (ValidacaoDadosProblema(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Insere no banco de dados.
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Problema inserido com sucesso";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;            
            }
            catch(System .Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Alterar
        /// <summary>
        /// Alterar
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {

            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                //Verifica se todos os campos foram informados.
                if (ValidacaoDadosProblema(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                //Verifica se o nome do problema já existe no banco para outro registro.
                else if (VerificaSeJaExisteNoBanco() == true)
                {
                    strMensagem = "Já existe problema com o nome informado.";
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Altera no banco de dados.
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Problema alterado com sucesso";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;            
            }
            catch(System .Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Excluir
        /// <summary>
        /// Excluir
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                //Valida exclusão do problema
                if (ValidaExclusao(out strMsg) == false)
                    return false;

                //>>> Exclui todas as associações de incidentes
                ServiceDesk.Negocio.ClsProblemaIncidente objProblemaIncidente = new ClsProblemaIncidente();
                objProblemaIncidente.excluiAssociacaoIncidenteDoProblema(Convert.ToInt32(objCodigo.Valor));
                objProblemaIncidente = null;

                //>>> Exclui todas as associações de itens de configuração
                ServiceDesk.Negocio.ClsProblemaItemConfig objProblemaItemConfig = new ClsProblemaItemConfig();
                objProblemaItemConfig.excluiAssociacaoItemConfigDoProblema(Convert.ToInt32(objCodigo.Valor));
                objProblemaItemConfig = null;

                //>>> Exclui todas as associações de mudanças
                ServiceDesk.Negocio.ClsProblemaMudanca objMudanca = new ClsProblemaMudanca();
                objMudanca.excluiAssociacaoMudanca(Convert.ToInt32(objCodigo.Valor));
                objMudanca = null;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    strMsg = "Registro excluído com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMsg = "Registro não excluído.";
                    objBanco = null;
                    return false;
                }            
            }
            catch(System .Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion
        
        #region Alimenta campos coleção
        /// <summary>
        /// Alimenta campos coleção
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "Problema";
                objAtributos.DescricaoTabela = "Tabela dos problema";

                objCodigo.Campo = "problema_codigo";
                objCodigo.Descricao = "Código do problema";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objNome.Campo = "nome";
                objNome.Descricao = "Nome do problema";
                objNome.CampoIdentificador = false;
                objNome.CampoObrigatorio = true;
                objNome.Tipo = System.Data.DbType.String;
                objAtributos.Add(objNome);

                objDescricao.Campo = "descricao";
                objDescricao.Descricao = "Descrição do problema";
                objDescricao.CampoIdentificador = false;
                objDescricao.CampoObrigatorio = true;
                objDescricao.Tamanho = 255;
                objDescricao.Tipo = System.Data.DbType.String;
                objAtributos.Add(objDescricao);

                objDtInclusao.Campo = "data_inclusao";
                objDtInclusao.Descricao = "Data e hora cadastro";
                objDtInclusao.CampoIdentificador = false;
                objDtInclusao.CampoObrigatorio = true;
                objDtInclusao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDtInclusao);

                objDtAlteracao.Campo = "data_alteracao";
                objDtAlteracao.Descricao = "Data e hora alteração";
                objDtAlteracao.CampoIdentificador = false;
                objDtAlteracao.CampoObrigatorio = false;
                objDtAlteracao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDtAlteracao);

                objCodigoProprietario.Campo = "pessoa_codigo_proprietario";
                objCodigoProprietario.Descricao = "Pessoa que cadastrou proprietário do problema";
                objCodigoProprietario.CampoIdentificador = false;
                objCodigoProprietario.CampoObrigatorio = true;
                objCodigoProprietario.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProprietario);

                objCodigoUsuarioAlterou.Campo = "pessoa_codigo_alteracao";
                objCodigoUsuarioAlterou.Descricao = "Pessoa que alterou";
                objCodigoUsuarioAlterou.CampoIdentificador = false;
                objCodigoUsuarioAlterou.CampoObrigatorio = false;
                objCodigoUsuarioAlterou.Tipo = System.Data.DbType.String;
                objAtributos.Add(objCodigoUsuarioAlterou);

                objCodigoEquipeAlocacao.Campo = "equipe_codigo_alocacao";
                objCodigoEquipeAlocacao.Descricao = "Equipe de solução";
                objCodigoEquipeAlocacao.CampoIdentificador = false;
                objCodigoEquipeAlocacao.CampoObrigatorio = false;
                objCodigoEquipeAlocacao.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoEquipeAlocacao);

                objCodigoPessoaAlocacao.Campo = "pessoa_codigo_alocacao";
                objCodigoPessoaAlocacao.Descricao = "Pessoa da equipe alocada para solução";
                objCodigoPessoaAlocacao.CampoIdentificador = false;
                objCodigoPessoaAlocacao.CampoObrigatorio = false;
                objCodigoPessoaAlocacao.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoPessoaAlocacao);

                objCodigoStatus.Campo = "status_codigo";
                objCodigoStatus.Descricao = "Status";
                objCodigoStatus.CampoIdentificador = false;
                objCodigoStatus.CampoObrigatorio = false;
                objCodigoStatus.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoStatus);

                objCodigoProblemaTipo.Campo = "problema_tipo_codigo";
                objCodigoProblemaTipo.Descricao = "Código do tipo do problema.";
                objCodigoProblemaTipo.CampoIdentificador = false;
                objCodigoProblemaTipo.CampoObrigatorio = true;
                objCodigoProblemaTipo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProblemaTipo);

                objFlagFechado.Campo = "flag_fechado";
                objFlagFechado.Descricao = "Flag que indica se o problema esta fechado ou não.";
                objFlagFechado.CampoIdentificador = false;
                objFlagFechado.CampoObrigatorio = true;
                objFlagFechado.Tipo = System.Data.DbType.String;
                objAtributos.Add(objFlagFechado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string strItemDefault)
        {
            try
            {
                ClsProblema objProblema = new ClsProblema();
                objDropDownList.DataTextField = objProblema.objNome.Campo;
                objDropDownList.DataValueField = objProblema.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objProblema.objAtributos);
                objProblema = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = strItemDefault.Trim();
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsProblema objProblema, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblema.objAtributos, bolCondicao);
                objProblema = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera gridview com campos especificos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select Problema.problema_codigo, Problema.nome,ProblemaTipo.nome as tipo ,";
                strSql += " Pessoa.nome as proprietario, Problema.data_inclusao";
                strSql += " from Problema, Pessoa, ProblemaTipo";
                strSql += " where Problema.problema_tipo_codigo = ProblemaTipo.problema_tipo_codigo";
                strSql += " and Pessoa.pessoa_codigo = Problema.pessoa_codigo_proprietario";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataBind();
                objBanco = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Pega o próximo código identificador da tabela
        /// <summary>
        /// Pega o próximo registro identificador da tabela.
        /// </summary>
        /// <returns>Retorna número inteiro do próximo identificador ou zero se houver erro.</returns>
        public int GetMaxId()
        {
            try
            {
                string strSql = "select max(problema_codigo) as maximo from Problema";

                System.Data.SqlClient.SqlDataReader objDtReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objDtReader.Read())
                {
                    string strMax = objDtReader["maximo"].ToString();
                    if (strMax == string.Empty)
                    {
                        objDtReader.Dispose();
                        return 1;
                    }
                    else
                    {
                        objDtReader.Dispose();
                        return Convert.ToInt32(strMax) + 1;
                    }
                }
                else
                {
                    objDtReader.Dispose();
                    return 1;
                }
            
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region Verifica se dados já existem
        /// <summary>
        /// Verifica se já existe com o mesmo nome para registro diferente.
        /// </summary>
        /// <param name="strNome"></param>
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
        public bool VerificaSeJaExisteNoBanco()
        {
            try
            {   //Busca no banco de dados pelo nome.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSqlCriterio = "nome = '"+ this.objNome.Valor .Trim () +"' and problema_codigo <> "+ this .objCodigo.Valor.Trim () +"";
                
                string strValor = objBanco.retornaValorCampo("Problema", "nome", strSqlCriterio);
                
                objBanco = null;

                //Verifica se o nome foi encontrado.
                if (strValor.Trim() != string.Empty)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
      
        #region herdaICIncidente
        /// <summary>
        /// Copia os ICs de um incidente para o problema informado
        /// </summary>
        /// <param name="strCodigoIncidente">Codigo do Incidente</param>
        /// <param name="strCodigoProblema">Codigo do Problema</param>
        /// <returns>true se copia ok.</returns>          
        static public void herdaICIncidente(String strCodigoIncidente, String strCodigoProblema)
        {
          String strSql = String.Empty;
          try
          {
            strSql = "INSERT INTO ProblemaItemConfiguracao ";
            strSql += "SELECT '" + strCodigoProblema + "' as problema_codigo, item_configuracao_codigo ";
            strSql += "FROM IncidenteItemConfiguracao ";
            strSql += "WHERE incidente_codigo ='" + strCodigoIncidente + "' ";
            strSql += "AND item_configuracao_codigo NOT IN ( ";
            strSql += "Select item_configuracao_codigo ";
            strSql += "From ProblemaItemConfiguracao ";
            strSql += "WHERE problema_codigo = '" + strCodigoProblema + "' ) ";

            ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
            Banco.executaSQL(strSql);
            Banco = null;
          }
          catch //(Exception ex)
          {
            Exception myEx = new Exception("Não foi possivel copiar os itens de configuração.");
            throw myEx;
          }
        }

        #endregion

        #region AdicionaRelacaoIncidenteProblema

        /// <summary>
        /// Cria o relacionamento entre um chamado e um incidente
        /// </summary>
        /// <param name="strCodigoIncidente">Codigo do Incidente</param>
        /// <param name="strCodigoChamado">Codigo do Chamado</param>
        /// <returns>true se ok.</returns>
        static public void AdicionaRelacaoIncidenteProblema(String strCodigoIncidente, String strCodigoProblema)
        {
          String strSql = String.Empty;
          try
          {
            strSql = "INSERT INTO ProblemaIncidente ";
            strSql += "(problema_codigo, incidente_codigo) ";
            strSql += "VALUES ";
            strSql += "('" + strCodigoProblema + "','" + strCodigoIncidente + "') ";

            ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
            Banco.executaSQL(strSql);
            Banco = null;
          }
          catch //(Exception ex)
          {
            Exception myEx = new Exception("Não foi possivel relacionar o incidente ao problema.");
            throw myEx;
          }
        }

        #endregion

        #endregion
    }
}