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
/// Classe EquipePessoa
/// </summary>
namespace ServiceDesk.Negocio
{
    public class ClsEquipePessoa
    {
        #region Declarações

        //Colecao de atributos de EquipePessoa
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de uma EquipePessoa
        private ServiceDesk.Banco.ClsAtributo objEquipePessoaCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEquipeCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoaCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagLider = new ServiceDesk.Banco.ClsAtributo();

        #endregion
       
        #region Propriedades

        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        /// <summary>
        /// Código da Pessoa da Equipe.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo EquipePessoaCodigo
        {
            get { return objEquipePessoaCodigo; }
            set { this.objEquipePessoaCodigo = value; }
        }

        /// <summary>
        /// Código da Equipe.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo EquipeCodigo
        {
            get { return objEquipeCodigo; }
            set { this.objEquipeCodigo = value; }
        }

        /// <summary>
        /// Código da Pessoa
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PessoaCodigo
        {
            get { return objPessoaCodigo; }
            set { this.objPessoaCodigo = value; }
        }

        /// <summary>
        /// Indica se é lider da equipe ou não.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagLider
        {
            get { return objFlagLider; }
            set { this.objFlagLider = value; }
        }
        #endregion

        #region Métodos

        #region alimentaColecaoCampos

        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "EquipePessoa";
            objAtributos.DescricaoTabela = "EquipePessoa";

            objEquipePessoaCodigo.Campo = "equipe_pessoa_codigo";
            objEquipePessoaCodigo.Descricao = "Código pessoa da equipe";
            objEquipePessoaCodigo.CampoIdentificador = true;
            objEquipePessoaCodigo.CampoObrigatorio = false;
            objEquipePessoaCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objEquipePessoaCodigo);

            objEquipeCodigo.Campo = "equipe_codigo";
            objEquipeCodigo.Descricao = "Código da Equipe";
            objEquipeCodigo.CampoIdentificador = false;
            objEquipeCodigo.CampoObrigatorio = true;
            objEquipeCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objEquipeCodigo);

            objPessoaCodigo.Campo = "pessoa_codigo";
            objPessoaCodigo.Descricao = "Código da pessoa";
            objPessoaCodigo.CampoIdentificador = false;
            objPessoaCodigo.CampoObrigatorio = true;
            objPessoaCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPessoaCodigo);

            objFlagLider.Campo = "flag_lider";
            objFlagLider.Descricao = "Indica o Lider da Equipe";
            objFlagLider.CampoIdentificador = false;
            objFlagLider.CampoObrigatorio = false;
            objFlagLider.Tipo = System.Data.DbType.String;
            objFlagLider.Tamanho = 1;
            objAtributos.Add(objFlagLider);
        }
        #endregion

        #region Contrutor da Classe
        /// <summary>
        /// Construtor da classe EquipePessoa
        /// </summary>
        public ClsEquipePessoa()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsEquipePessoa(int intCodigo)
        {
            try
            {
                this.alimentaColecaoCampos();
                this.objEquipePessoaCodigo.Valor = intCodigo.ToString();
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

        #region metodo insere
        /// <summary>
        /// Método que insere
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Equipe inserida com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
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
        /// Método que altera 
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Equipe altera com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
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
        /// Método que altera um registro definindo um lider para equipe.
        /// </summary>
        /// <param name="intCodigoEquipe">Código da Equipe.</param>
        /// <param name="intCodigoPessoa">Código da Pessoa.</param>
        /// <param name="CodigoLider">S para sim e N para não - Estes valores definem se a pessoa é lider ou não.</param>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem, int intCodigoEquipe, int intCodigoPessoa, String strCodigoLider)
        {
            try
            {
                String strSQL = String.Empty;
                strMensagem = String.Empty;
                bool bolRetorno = false;

                strSQL = "UPDATE EquipePessoa SET flag_lider = '" + strCodigoLider + "' WHERE ";
                strSQL += "equipe_codigo = " + intCodigoEquipe + " AND pessoa_codigo = " + intCodigoPessoa;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSQL))
                {
                    strMensagem = "Equipe alterada com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
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
        /// Método que exclui
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui()
        {
            try
            {
                //Valida a exclusão.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;
                
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    objBanco = null;
                    return true;
                }
                else
                {
                    objBanco = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo excluiEquipe
        /// <summary>
        /// Método que exclui
        /// </summary>
        public static bool excluiEquipe(int intCodigoEquipe)
        {
            try
            {
                //Valida a exclusão.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;
                
                string strSql = "DELETE FROM equipePessoa WHERE equipe_codigo = " + intCodigoEquipe;
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSql))
                {
                    objBanco = null;
                    return true;
                }
                else
                {
                    objBanco = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo excluiPessoaEquipe
        /// <summary>
        /// Método que exclui
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool excluiPessoaEquipe(int intCodigoPessoa, int intCodigoEquipe)
        {
            try
            {
                try
                {
                    String strSQL = "DELETE FROM EquipePessoa WHERE equipe_codigo = " + intCodigoEquipe + " AND pessoa_codigo = " + intCodigoPessoa;
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    objBanco.executaSQL(strSQL);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownEmpresa
        public SqlDataReader geraDropDownEmpresa()
        {
            try
            {
                String strSQL = "SELECT EstruturaOrganizacional.descricao, EstruturaOrganizacional.estrutura_codigo ";
                strSQL += "FROM EstruturaOrganizacional, TipoEstruturaOrganizacional ";
                strSQL += "WHERE TipoEstruturaOrganizacional.descricao = 'empresa' AND ";
                strSQL += "EstruturaOrganizacional.tipo_estrutura_codigo = TipoEstruturaOrganizacional.tipo_estrutura_codigo ";
                strSQL += "ORDER BY EstruturaOrganizacional.descricao";
                
                return ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Retorna as Pessoas de acordo com a equipe selecionada.
        /// </summary>
        ///<param name="intCodigoEquipe">Código da equipe</param>
        ///<returns>DataReader</returns>
        public SqlDataReader geraGridViewEquipe(int intCodigoEquipe)
        {
            try
            {

                String strSQL = " SELECT EstruturaOrganizacional.descricao,Pessoa.nome,EquiPepessoa.flag_lider";
            strSQL += " FROM EquipePessoa, EstruturaOrganizacional, Pessoa WHERE EquipePessoa.equipe_codigo = " + intCodigoEquipe +" ";
            strSQL += " AND Pessoa.pessoa_codigo = EquipePessoa.pessoa_codigo AND";
            strSQL += " EstruturaOrganizacional.estrutura_codigo = Pessoa.estrutura_codigo";
                
            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
                                            
        #region metodo GetEquipe
        /// <summary>
        /// Método que pega os integrantes de uma equipe e ao mesmo tempo pega
        /// os integrantes de uma empresa. Este método foi implementado desta forma para 
        /// Carregar de uma única vez o GridView.
        /// </summary>
        /// <param name="intCodigoEquipe">Código da equipe</param>
        /// <param name="intCodigoEmpresa">Código da empresa</param>
        /// <returns>Retorna um DataSet</returns>
        public DataSet GetEquipe(String strCodigoEquipe, String strCodigoEmpresa)
        {
            System.Data.DataSet objDataSet;
            System.Data.DataRow objDataRow;
            System.Data.SqlClient.SqlDataReader objDataReader;

            try
            {

                String strSQL = "SELECT EstruturaOrganizacional.estrutura_codigo,EstruturaOrganizacional.descricao,Pessoa.pessoa_codigo,Pessoa.nome, EquipePessoa.flag_lider,";
                strSQL += " 'S' as IntegranteEquipe FROM EstruturaOrganizacional, Pessoa, EquipePessoa WHERE EquipePessoa.equipe_codigo = " + strCodigoEquipe.Trim() +" ";
                strSQL += " AND Pessoa.pessoa_codigo = EquipePessoa.pessoa_codigo AND";
                strSQL += " EstruturaOrganizacional.estrutura_codigo = Pessoa.estrutura_codigo order by Pessoa.nome";
    
                objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSQL);

                if (strCodigoEmpresa != String.Empty && strCodigoEmpresa != "null")
                {
                    strSQL = " SELECT EstruturaOrganizacional.estrutura_codigo,EstruturaOrganizacional.descricao,Pessoa.pessoa_codigo,Pessoa.nome, 'N' as IntegranteEquipe ";
                    strSQL += " FROM EstruturaOrganizacional, Pessoa WHERE EstruturaOrganizacional.estrutura_codigo = " + strCodigoEmpresa.Trim() + " AND ";
                    strSQL += " Pessoa.estrutura_codigo = EstruturaOrganizacional.estrutura_codigo AND Pessoa.pessoa_codigo not in (select pessoa_codigo from EquipePessoa ";
                    strSQL += " where equipe_codigo = " + strCodigoEquipe.Trim() + ") group by EstruturaOrganizacional.estrutura_codigo,EstruturaOrganizacional.descricao,Pessoa.pessoa_codigo,Pessoa.nome ";
                    strSQL += " order by Pessoa.nome";

                    objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
                    while (objDataReader.Read())
                    {
                        // Popula o DataRow dsProdutos.Tables(0).NewRow
                        objDataRow = objDataSet.Tables[0].NewRow();
                        objDataRow["estrutura_codigo"] = objDataReader["estrutura_codigo"].ToString();
                        objDataRow["descricao"] = objDataReader["descricao"].ToString();
                        objDataRow["pessoa_codigo"] = objDataReader["pessoa_codigo"].ToString();
                        objDataRow["nome"] = objDataReader["nome"].ToString();
                        objDataRow["flag_lider"] = "N";
                        objDataRow["IntegranteEquipe"] = objDataReader["IntegranteEquipe"].ToString();  

                        // Adiciona um registro no DataSet
                        objDataSet.Tables[0].Rows.Add(objDataRow);
                    }
                    objDataReader = null;
                    objDataRow = null;
                }
                return objDataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetLiderEquipe
        /// <summary>
        /// Método que Verifica se a equipe possui um lider.
        /// </summary>
        /// <param name="intCodigoEquipe">Código da equipe</param>
        /// <param name="intCodigoPessoa">Código da pessoa</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public bool GetLiderEquipe(int intCodigoEquipe, int intCodigoPessoa)
        {
            String strSQL;
            try
            {
                strSQL = "SELECT * FROM EquipePessoa WHERE flag_lider = 'S' ";
                strSQL += "AND equipe_codigo = " + intCodigoEquipe + " AND pessoa_codigo <> " + intCodigoPessoa;

                if (ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL).Read())
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

        #region metodo VerificaExisteEquipePessoa
        /// <summary>
        /// Verifica se já existe uma pessoa já cadastrada na equipe
        /// </summary>
        /// <param name="intCodigoPessoa">Código da Pessoa</param>
        /// <param name="intCodigoEquipe">Código da Equipe</param>
        /// <returns>True ou False</returns>
        public bool VerificaExisteEquipePessoa(int intCodigoPessoa, int intCodigoEquipe)
        {
            try
            {
                String strSQL = "SELECT equipe_pessoa_codigo FROM EquipePessoa WHERE equipe_codigo = " + intCodigoEquipe + " AND pessoa_codigo = " + intCodigoPessoa;
                if (ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL).Read())
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
        #endregion
    }
}
