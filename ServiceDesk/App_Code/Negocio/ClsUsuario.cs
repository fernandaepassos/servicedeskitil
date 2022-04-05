using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using ServiceDesk.Banco;
using System.Data.Common;
using System.Collections.Generic;
using System.Net;
using System.Web.SessionState;

/// <summary>
/// Classe Usuário. Fornece métodos de autenticação e busca de informações dos 
/// usuários do sistema.
/// </summary>

namespace ServiceDesk.Negocio
{
    public class ClsUsuario
    {

        #region Construtor
        public ClsUsuario()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region Variáveis


        #endregion

        #region Atributos

        //Colecao de atributos de Usuário
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objMatricula = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEmpresa = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objArea = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objUnidade = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCentroDeCusto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCargo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objLinhaOnibus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoUsuario = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInicioTrabalho = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataFimTrabalho = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoColaborador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objRamal = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objEMail = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataNascimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSexo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCPF = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objLogradouro = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objBairro = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCidade = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objUF = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCEP = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTelefone = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPontoOnibus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoRede = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objValorHora = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagVip = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objLocalizacaoFisica = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSenha = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoSangue = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNomeGuerra = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFoto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCnh = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataExpedicaoCnh = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objValidadeCnh = new ServiceDesk.Banco.ClsAtributo();

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
        /// Codigo do Usuario
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
        }

        /// <summary>
        /// Nome do Usuário
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
        }

        /// <summary>
        /// Empresa do Usuario
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Empresa
        {
            get { return objEmpresa; }
        }

        /// <summary>
        /// Matricula do Usuário.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Matricula
        {
            get { return objMatricula; }
        }

        /// <summary>
        /// Area do Usuário.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Area
        {
            get { return objArea; }
        }

        /// <summary>
        /// Unidade
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Unidade
        {
            get { return objUnidade; }
        }

        /// <summary>
        /// Centro de custo
        /// /// </summary>
        public ServiceDesk.Banco.ClsAtributo CentroDeCusto
        {
            get { return objCentroDeCusto; }
        }

        /// <summary>
        /// Cargo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Cargo
        {
            get { return objCargo; }
        }

        /// <summary>
        /// Status da solicitação.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo LinhaOnibus
        {
            get { return objLinhaOnibus; }
        }

        /// <summary>
        /// Matricula do alterador da solicitação.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TipoUsuario
        {
            get { return objTipoUsuario; }
        }

        /// <summary>
        /// Data Inicio de Trabalho
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInicioTrabalho
        {
            get { return objDataInicioTrabalho; }
        }

        /// <summary>
        /// Data Fim de Trabalho
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataFimTrabalho
        {
            get { return objDataFimTrabalho; }
        }

        /// <summary>
        /// Tipo de Colaborador
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TipoColaborador
        {
            get { return objTipoColaborador; }
        }

        /// <summary>
        /// Data de inclusão da solicitação.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Ramal
        {
            get { return objRamal; }
        }

        /// <summary>
        /// Data de alteração da solicitação.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo EMail
        {
            get { return objEMail; }
        }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataNascimento
        {
            get { return objDataNascimento; }
        }

        /// <summary>
        /// Sexo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Sexo
        {
            get { return objSexo; }
        }

        /// <summary>
        /// CPF
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CPF
        {
            get { return objCPF; }
        }

        /// <summary>
        /// Logradouro
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Logradouro
        {
            get { return objLogradouro; }
        }

        /// <summary>
        ///Bairro
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Bairro
        {
            get { return objBairro; }
        }

        /// <summary>
        /// Cidade
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Cidade
        {
            get { return objCidade; }
        }

        /// <summary>
        /// UF
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo UF
        {
            get { return objUF; }
        }

        /// <summary>
        /// CEP
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CEP
        {
            get { return objCEP; }
        }

        /// <summary>
        /// Telefone
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Telefone
        {
            get { return objTelefone; }
        }


        /// <summary>
        /// Ponto de Onibus
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PontoOnibus
        {
            get { return objPontoOnibus; }
        }

        /// <summary>
        /// Login da Rede
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoRede
        {
            get { return objCodigoRede; }
        }

        /// <summary>
        /// Valor Hora ded trabalho
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ValorHora
        {
            get { return objValorHora; }
        }

        /// <summary>
        /// Usuário Vip?
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Vip
        {
            get { return objFlagVip; }
        }

        /// <summary>
        /// Localização Física
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo LocalizacaoFisica
        {
            get { return objLocalizacaoFisica; }
        }

        /// <summary>
        /// Senha
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Senha
        {
            get { return objSenha; }
        }

        /// <summary>
        /// Status do usuario 
        /// </summ ary>
        public ServiceDesk.Banco.ClsAtributo Status
        {
            get { return objStatus; }
        }

        /// <summary>
        /// Tipo sanguineo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TipoSangue
        {
            get { return objTipoSangue; }
        }


        /// <summary>
        /// Nome de Guerra
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo NomeGuerra
        {
            get { return objNomeGuerra; }
        }


        /// <summary>
        /// Foto
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Foto
        {
            get { return objFoto; }
        }


        /// <summary>
        /// CNH
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Cnh
        {
            get { return objCnh; }
        }


        /// <summary>
        /// Data Expedicao CNH
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataExpedicaoCnh
        {
            get { return objDataExpedicaoCnh; }
        }


        /// <summary>
        /// Data Validade CNH
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataValidadeCnh
        {
            get { return objValidadeCnh; }
        }


        #endregion

        #region Métodos

        #region alimentaColecaoCampos
        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "Pessoa";
            objAtributos.DescricaoTabela = "Pessoas";

            objCodigo.Campo = "pessoa_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objMatricula.Campo = "matricula";
            objMatricula.Descricao = "Matrícula";
            objMatricula.CampoObrigatorio = true;
            objMatricula.Tipo = System.Data.DbType.String;
            objMatricula.Tamanho = 15;
            objAtributos.Add(objMatricula);

            objNome.Campo = "nome";
            objNome.Descricao = "Nome";
            objNome.CampoObrigatorio = true;
            objNome.Tipo = System.Data.DbType.String;
            objNome.Tamanho = 120;
            objAtributos.Add(objNome);

            Empresa.Campo = "estrutura_codigo";
            Empresa.Descricao = "Empresa";
            Empresa.CampoObrigatorio = true;
            Empresa.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(Empresa);

            objArea.Campo = "area_codigo";
            objArea.Descricao = "Área";
            objArea.CampoObrigatorio = true;
            objArea.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objArea);

            objUnidade.Campo = "unidade_codigo";
            objUnidade.Descricao = "Unidade";
            objUnidade.CampoObrigatorio = true;
            objUnidade.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objUnidade);

            objCentroDeCusto.Campo = "centro_custo_codigo";
            objCentroDeCusto.Descricao = "Centro de Custo";
            objCentroDeCusto.CampoObrigatorio = true;
            objCentroDeCusto.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCentroDeCusto);

            objCargo.Campo = "cargo_codigo";
            objCargo.Descricao = "Cargo";
            objCargo.CampoObrigatorio = true;
            objCargo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCargo);

            objLinhaOnibus.Campo = "linha_onibus";
            objLinhaOnibus.Descricao = "Linha Ônibus";
            objLinhaOnibus.CampoObrigatorio = true;
            objLinhaOnibus.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objLinhaOnibus);

            objTipoUsuario.Campo = "tipo_usuario_codigo";
            objTipoUsuario.Descricao = "Tipo de Usuário";
            objTipoUsuario.CampoObrigatorio = true;
            objTipoUsuario.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTipoUsuario);

            objDataInicioTrabalho.Campo = "data_inicio_trabalho";
            objDataInicioTrabalho.Descricao = "Data de Início de trabalho";
            objDataInicioTrabalho.CampoObrigatorio = true;
            objDataInicioTrabalho.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataInicioTrabalho);

            objDataFimTrabalho.Campo = "data_fim_trabalho";
            objDataFimTrabalho.Descricao = "Data fim trabalho";
            objDataFimTrabalho.CampoObrigatorio = true;
            objDataFimTrabalho.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataFimTrabalho);

            objTipoColaborador.Campo = "tipo_colaborador";
            objTipoColaborador.Descricao = "Tipo de Colaborador";
            objTipoColaborador.CampoObrigatorio = true;
            objTipoColaborador.Tipo = System.Data.DbType.Int32;
            objTipoColaborador.Tamanho = 1;
            objAtributos.Add(objTipoColaborador);

            objRamal.Campo = "ramal";
            objRamal.Descricao = "Ramal";
            objRamal.CampoObrigatorio = true;
            objRamal.Tipo = System.Data.DbType.String;
            objRamal.Tamanho = 50;
            objAtributos.Add(objRamal);

            objEMail.Campo = "email";
            objEMail.Descricao = "E-Mail";
            objEMail.CampoObrigatorio = true;
            objEMail.Tipo = System.Data.DbType.String;
            objEMail.Tamanho = 300;
            objAtributos.Add(objEMail);

            objDataNascimento.Campo = "data_nascimento";
            objDataNascimento.Descricao = "Data de Nascimento";
            objDataNascimento.CampoObrigatorio = true;
            objDataNascimento.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataNascimento);

            objSexo.Campo = "sexo";
            objSexo.Descricao = "Sexo";
            objSexo.CampoObrigatorio = true;
            objSexo.Tipo = System.Data.DbType.String;
            objSexo.Tamanho = 1;
            objAtributos.Add(objSexo);

            objCPF.Campo = "cpf";
            objCPF.Descricao = "CPF";
            objCPF.CampoObrigatorio = true;
            objCPF.Tipo = System.Data.DbType.String;
            objCPF.Tamanho = 20;
            objAtributos.Add(objCPF);

            objLogradouro.Campo = "logradouro";
            objLogradouro.Descricao = "Logradouro";
            objLogradouro.CampoObrigatorio = true;
            objLogradouro.Tipo = System.Data.DbType.String;
            objLogradouro.Tamanho = 255;
            objAtributos.Add(objLogradouro);

            objBairro.Campo = "bairro";
            objBairro.Descricao = "Bairro";
            objBairro.CampoObrigatorio = true;
            objBairro.Tipo = System.Data.DbType.String;
            objBairro.Tamanho = 50;
            objAtributos.Add(objBairro);

            objCidade.Campo = "cidade";
            objCidade.Descricao = "Cidade";
            objCidade.CampoObrigatorio = true;
            objCidade.Tipo = System.Data.DbType.String;
            objCidade.Tamanho = 50;
            objAtributos.Add(objCidade);

            objUF.Campo = "uf";
            objUF.Descricao = "UF";
            objUF.CampoObrigatorio = true;
            objUF.Tipo = System.Data.DbType.String;
            objUF.Tamanho = 2;
            objAtributos.Add(objUF);

            objCEP.Campo = "cep";
            objCEP.Descricao = "CEP";
            objCEP.CampoObrigatorio = true;
            objCEP.Tipo = System.Data.DbType.String;
            objCEP.Tamanho = 10;
            objAtributos.Add(objCEP);

            objTelefone.Campo = "telefone";
            objTelefone.Descricao = "Telefone";
            objTelefone.CampoObrigatorio = true;
            objTelefone.Tipo = System.Data.DbType.String;
            objTelefone.Tamanho = 50;
            objAtributos.Add(objTelefone);

            objPontoOnibus.Campo = "ponto_onibus";
            objPontoOnibus.Descricao = "Ponto de ônibus";
            objPontoOnibus.CampoObrigatorio = true;
            objPontoOnibus.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPontoOnibus);

            objCodigoRede.Campo = "codigo_rede";
            objCodigoRede.Descricao = "Codigo de Rede";
            objCodigoRede.CampoObrigatorio = true;
            objCodigoRede.Tipo = System.Data.DbType.String;
            objCodigoRede.Tamanho = 30;
            objAtributos.Add(objCodigoRede);

            objValorHora.Campo = "valor_hora";
            objValorHora.Descricao = "Valor Hora";
            objValorHora.CampoObrigatorio = true;
            objValorHora.Tipo = System.Data.DbType.Decimal;
            objAtributos.Add(objValorHora);

            objFlagVip.Campo = "flag_vip";
            objFlagVip.Descricao = "VIP";
            objFlagVip.CampoObrigatorio = true;
            objFlagVip.Tipo = System.Data.DbType.String;
            objFlagVip.Tamanho = 1;
            objAtributos.Add(objFlagVip);

            objLocalizacaoFisica.Campo = "localizacao_fisica";
            objLocalizacaoFisica.Descricao = "Localização Física";
            objLocalizacaoFisica.CampoObrigatorio = true;
            objLocalizacaoFisica.Tipo = System.Data.DbType.String;
            objLocalizacaoFisica.Tamanho = 255;
            objAtributos.Add(objLocalizacaoFisica);

            objSenha.Campo = "senha";
            objSenha.Descricao = "Senha";
            objSenha.CampoObrigatorio = true;
            objSenha.Tipo = System.Data.DbType.String;
            objSenha.Tamanho = 255;
            objAtributos.Add(objSenha);

            objStatus.Campo = "status_codigo";
            objStatus.Descricao = "Status";
            objStatus.CampoObrigatorio = true;
            objStatus.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objStatus);

            objTipoSangue.Campo = "tipo_sangue";
            objTipoSangue.Descricao = "Tipo Sanguineo";
            objTipoSangue.CampoObrigatorio = true;
            objTipoSangue.Tipo = System.Data.DbType.String;
            objTipoSangue.Tamanho = 3;
            objAtributos.Add(objTipoSangue);

            objNomeGuerra.Campo = "nome_guerra";
            objNomeGuerra.Descricao = "Nome de Guerra";
            objNomeGuerra.CampoObrigatorio = true;
            objNomeGuerra.Tipo = System.Data.DbType.String;
            objNomeGuerra.Tamanho = 15;
            objAtributos.Add(objNomeGuerra);

            objFoto.Campo = "foto";
            objFoto.Descricao = "Foto";
            objFoto.Tipo = System.Data.DbType.String;
            objFoto.Tamanho = 100;
            objAtributos.Add(objFoto);

            objCnh.Campo = "cnh";
            objCnh.Descricao = "CNH";
            objCnh.Tipo = System.Data.DbType.String;
            objCnh.Tamanho = 20;
            objAtributos.Add(objCnh);

            objDataExpedicaoCnh.Campo = "data_expedicao_cnh";
            objDataExpedicaoCnh.Descricao = "Data Expedição CNH";
            objDataExpedicaoCnh.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDataExpedicaoCnh);

            objValidadeCnh.Campo = "data_validade_cnh";
            objValidadeCnh.Descricao = "Data Validade CNH";
            objValidadeCnh.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objValidadeCnh);

        }
        #endregion

        #region getUsuario
        /// <summary>
        /// Verifica se um determinado usuário tem acesso ao sistema
        /// </summary>
        /// <param name="strCodigoRede">Codigo da Rede</param>
        /// <param name="strTipUsu">Tipo de usuário</param>
        /// <returns>bool</returns>
        static public bool getUsuario(String strCodigoRede, String[] strTipUsu)
        {
            bool boolRetorno = false;
            String strSql = String.Empty;
            int i;

            if (!(strCodigoRede.Equals("")) || !(strTipUsu.Equals("")))
            {
                strSql = "SELECT pessoa.matricula, pessoa.nome  ";
                strSql += "from pessoa, pessoaperfilempresa, perfilempresa, perfil, tipousuario, aplicacao ";
                strSql += "WHERE  ";
                strSql += "pessoaperfilempresa.pessoa_codigo = pessoa.pessoa_codigo ";
                strSql += "AND PessoaPerfilEstrutura.perfil_estrutura_codigo = PerfilEstrutura.perfil_estrutura_codigo";
                strSql += "AND perfilempresa.perfil_codigo = perfil.perfil_codigo ";
                strSql += "AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo ";
                strSql += "AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo ";
                strSql += "AND aplicacao.aplicacao_codigo = '" + ClsParametro.CodigoDoSistema + "' ";
                strSql += "AND tipousuario.sigla in (";


                for (i = 0; i < strTipUsu.Length; i++)
                {
                    strSql += "'" + strTipUsu[i].ToString() + "'";
                    if ((i + 1) < strTipUsu.Length)
                    {
                        strSql += ",";
                    }
                }
                strSql += ")";
                strSql += "AND pessoa.codigo_rede = '" + strCodigoRede + "' ";

                try
                {
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                    if (objReader.Read())
                    {
                        boolRetorno = true;
                    }
                    else
                    {
                        boolRetorno = false;
                    }
                    objReader.Close();
                    objReader.Dispose();
                    objReader = null;
                }
                catch
                {
                }
            }

            return boolRetorno;
        }
        #endregion

        #region getNomeUsuario
        /// <summary>
        /// Busca o nome do usuário a partir da sua matrícula.
        /// </summary>
        /// <param name="strCodigoUsuario">Codigo do Usuário.</param>
        /// <returns>O nome do usuário.</returns>
        static public string getNomeUsuario(string strCodigoUsuario)
        {
            String strSql = String.Empty;
            String strNomeUsuario = String.Empty;

            if (strCodigoUsuario == string.Empty) return string.Empty;

            strSql = "SELECT nome FROM pessoa";
            strSql += " WHERE pessoa.pessoa_codigo = " + Convert.ToInt32(strCodigoUsuario) + " ";

            SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            if (objReader.Read())
            {
                strNomeUsuario = objReader["nome"].ToString();
            }
            objReader.Close();
            objReader.Dispose();
            objReader = null;

            return strNomeUsuario;
        }
        #endregion

        #region Retorna nome do usuário pelo código de rede
        /// <summary>
        /// Retorna nome do usuário pelo código de rede.
        /// </summary>
        /// <param name="strCodigoRede">Codigo de rede.</param>
        /// <returns>O nome do usuário.</returns>
        static public String getNomeUsuarioPorCodigoRede(String strCodigoRede)
        {
            String strSql = String.Empty;
            String strNomeUsuario = String.Empty;

            if (strCodigoRede == string.Empty) return string.Empty;

            strSql = "SELECT nome ";
            strSql += " From Pessoa";
            strSql += " WHERE codigo_rede = '" + strCodigoRede.Trim() + "' ";

            SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            if (objReader.Read())
            {
                strNomeUsuario = objReader["nome"].ToString();
            }
            objReader.Close();
            objReader.Dispose();
            objReader = null;

            return strNomeUsuario;
        }
        #endregion

        #region getEmpresaUsuario
        /// <summary>
        /// Busca a empresa do usuario.
        /// </summary>
        /// <param name="strCodigoEmpresa">Codigo da Empresa do usuário.</param>
        /// <returns>O nome da empresa do usuário.</returns>
        static public String getEmpresaUsuario(String strCodigoEmpresa)
        {
            String strSql = String.Empty;
            String strNomeEmpresa = String.Empty;
            String strCodigoTipoEmpresa = ClsParametro.CodigoTipoEmpresa;

            strSql = "select estruturaorganizacional.descricao from estruturaorganizacional ";
            strSql += "Where ";
            strSql += "estruturaorganizacional.tipo_estrutura_codigo = '" + strCodigoTipoEmpresa + "' ";
            strSql += "and estruturaorganizacional.estrutura_codigo =  '" + strCodigoEmpresa + "'";

            try
            {
                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    strNomeEmpresa = objReader["descricao"].ToString();
                }
                else
                {
                    strNomeEmpresa = string.Empty;
                }
                objReader.Close();
                objReader.Dispose();
                objReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strNomeEmpresa;
        }
        #endregion

        #region getUnidadeUsuario
        /// <summary>
        /// Busca o nome a unidade de trabalho a partir do valor informado 
        /// </summary>
        /// <param name="strCodigoUnidade">Unidade da Empresa do usuário.</param>
        /// <returns>O nome da unidade de trabalho do usuário.</returns>
        static public String getUnidadeUsuario(String strCodigoUnidade)
        {
            String strSql = String.Empty;
            String strNomeUnidade = String.Empty;
            String strCodigoTipoUnidade = ClsParametro.CodigoTipoUnidade;

            strSql = "select estruturaorganizacional.descricao from estruturaorganizacional ";
            strSql += "Where ";
            strSql += "estruturaorganizacional.tipo_estrutura_codigo = '" + strCodigoTipoUnidade + "' ";
            strSql += "and estruturaorganizacional.estrutura_codigo =  '" + strCodigoUnidade + "'";

            try
            {
                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    strNomeUnidade = objReader["descricao"].ToString();
                }
                else
                {
                    strNomeUnidade = string.Empty;
                }
                objReader.Close();
                objReader.Dispose();
                objReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strNomeUnidade;
        }
        #endregion

        #region getAreaUsuario
        /// <summary>
        /// Busca o nome a area de trabalho a partir do valor informado 
        /// </summary>
        /// <param name="strCodigoArea">Area da Empresa do usuário.</param>
        /// <returns>O nome da area de trabalho do usuario.</returns>
        static public String getAreaUsuario(String strCodigoArea)
        {
            String strSql = String.Empty;
            String strNomeArea = String.Empty;
            String strCodigoTipoUnidade = ClsParametro.CodigoTipoUnidade;

            strSql = "select estruturaorganizacional.descricao from estruturaorganizacional ";
            strSql += "Where ";
            strSql += "estruturaorganizacional.tipo_estrutura_codigo = '" + strCodigoTipoUnidade + "' ";
            strSql += "and estruturaorganizacional.estrutura_codigo =  '" + strCodigoArea + "'";

            try
            {
                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    strNomeArea = objReader["descricao"].ToString();
                }
                else
                {
                    strNomeArea = string.Empty;
                }
                objReader.Close();
                objReader.Dispose();
                objReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strNomeArea;
        }
        #endregion

        #region getCargoUsuario
        /// <summary>
        /// Busca o nome do cargo de trabalho a partir do valor informado 
        /// </summary>
        /// <param name="strCodigoCargo">Codigo do cargo do usuário.</param>
        /// <returns>O nome do cargo de trabalho do usuario.</returns>
        static public String getCargoUsuario(String strCodigoCargo)
        {
            String strSql = String.Empty;
            String strNomeArea = String.Empty;

            strSql = "select descricao from cargo ";
            strSql += "Where ";
            strSql += "cargo_codigo = '" + strCodigoCargo + "'";
            //strSql += " and status_codigo = 'ATI' ";

            try
            {
                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    strNomeArea = objReader["descricao"].ToString();
                }
                else
                {
                    strNomeArea = string.Empty;
                }
                objReader.Close();
                objReader.Dispose();
                objReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strNomeArea;
        }
        #endregion

        #region metodo getAreaTipo
        /// <summary>
        /// Busca os códigos do tipo de usuário.
        /// </summary>
        /// <returns>Retorna todos os tipos de usuário.</returns>
        static public SqlDataReader getAreaTipo()
        {
            SqlDataReader objReader = null;
            string strSql = "SELECT RTRIM(tipo_usuario_codigo) as tipo_usuario_codigo FROM tipousuario WHERE sigla ('ADM','SOL','ATD')";
            try
            {
                objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objReader;
        }
        #endregion

        #region metodo getUsuarios
        /// <summary>
        /// Pega todos os usuários dos sistema
        /// </summary>
        /// <returns></returns>
        static public System.Data.DataSet getUsuarios()
        {
            String strSql = String.Empty;
            System.Data.DataSet objDataSet = null;

            try
            {
                strSql = "SELECT pessoa.matricula, pessoa.nome  ";
                strSql += "from pessoa, pessoaperfilestrutura, perfilestrutura, perfil, tipousuario, aplicacao ";
                strSql += "WHERE  ";
                strSql += "pessoaperfilestrutura.pessoa_codigo = pessoa.pessoa_codigo ";
                strSql += "AND pessoaperfilestrutura.perfil_estrutura_codigo = perfilestrutura.perfil_estrutura_codigo ";
                strSql += "AND perfilestrutura.perfil_codigo = perfil.perfil_codigo ";
                strSql += "AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo ";
                strSql += "AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo ";
                strSql += "AND aplicacao.aplicacao_codigo = '" + ClsParametro.CodigoDoSistema + "' ";

                objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objDataSet;
        }
        #endregion

        #region getTipoUsuarioCodigo
        /// <summary>
        /// Verifica o tipo de usuario de um usuario.
        /// </summary>
        /// <param name="strCodigoRede">Codigo da rede</param>
        /// <returns>String TipoUsuario</returns>
        static public int getTipoUsuarioCodigo(String strCodigoRede)
        {
            String strSql = String.Empty;
            int intTipoUsuario = 0;

            if (!(strCodigoRede.Equals("")))
            {
                strSql = "SELECT perfil.tipo_usuario_codigo";
                strSql += " FROM perfil, perfilestrutura, pessoaperfilestrutura, pessoa";
                strSql += " WHERE perfil.perfil_codigo = perfilestrutura.perfil_codigo";
                strSql += " AND perfilestrutura.perfil_estrutura_codigo = pessoaperfilestrutura.perfil_estrutura_codigo";
                strSql += " AND pessoaperfilestrutura.pessoa_codigo = pessoa.pessoa_codigo";
                strSql += " AND pessoa.estrutura_codigo = perfilestrutura.estrutura_codigo";
                strSql += " AND perfil.aplicacao_codigo = 0" + ClsParametro.CodigoDoSistema;
                strSql += " AND pessoa.codigo_rede = '" + strCodigoRede + "' ";
                try
                {
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    if (objReader.Read())
                    {
                        intTipoUsuario = Convert.ToInt32(objReader["tipo_usuario_codigo"].ToString());
                    }
                    objReader.Close();
                    objReader.Dispose();
                    objReader = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return intTipoUsuario;
        }
        #endregion

        #region getTipoUsuarioCodigo
        /// <summary>
        /// Verifica o tipo de usuario de um usuario.
        /// </summary>
        /// <param name="intPessoaCodigo">Codigo da rede</param>
        /// <returns>String TipoUsuario</returns>
        static public int getTipoUsuarioCodigo(int intPessoaCodigo)
        {
            String strSql = String.Empty;
            int intTipoUsuario = 0;

            strSql = "SELECT tipousuario.tipo_usuario_codigo, tipousuario.sigla ";
            strSql += "FROM pessoa, pessoaperfilestrutura, perfilestrutura, perfil, tipousuario, aplicacao ";
            strSql += "WHERE  ";
            strSql += "pessoaperfilestrutura.pessoa_codigo = pessoa.pessoa_codigo ";
            strSql += "AND pessoaperfilestrutura.perfil_estrutura_codigo = perfilestrutura.perfil_estrutura_codigo ";
            strSql += "AND perfilestrutura.perfil_codigo = perfil.perfil_codigo ";
            strSql += "AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo ";
            strSql += "AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo ";
            strSql += "AND aplicacao.aplicacao_codigo = '" + ClsParametro.CodigoDoSistema + "' ";
            strSql += "AND pessoa.pessoa_codigo = " + intPessoaCodigo.ToString();
            try
            {
                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    intTipoUsuario = Convert.ToInt32(objReader["tipo_usuario_codigo"].ToString());
                }
                objReader.Close();
                objReader.Dispose();
                objReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return intTipoUsuario;
        }
        #endregion

        #region getTipoUsuario
        /// <summary>
        /// Verifica o tipo de usuario de um usuario.
        /// </summary>
        /// <param name="strCodigoRede">Codigo da rede</param>
        /// <returns>String TipoUsuario</returns>
        static public String getTipoUsuario(String strCodigoRede)
        {
            String strSql = String.Empty;
            String strTipoUsuario = string.Empty;

            if (!(strCodigoRede.Equals("")))
            {


                strSql = "SELECT tipousuario.tipo_usuario_codigo, tipousuario.sigla ";
                strSql += "FROM pessoa, pessoaperfilestrutura, perfilestrutura, perfil, tipousuario, aplicacao ";
                strSql += "WHERE  ";
                strSql += "pessoaperfilestrutura.pessoa_codigo = pessoa.pessoa_codigo ";
                strSql += "AND pessoaperfilestrutura.perfil_estrutura_codigo = perfilestrutura.perfil_estrutura_codigo ";
                strSql += "AND perfilestrutura.perfil_codigo = perfil.perfil_codigo ";
                strSql += "AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo ";
                strSql += "AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo ";
                strSql += "AND aplicacao.aplicacao_codigo = '" + ClsParametro.CodigoDoSistema + "' ";
                strSql += "AND pessoa.codigo_rede = '" + strCodigoRede + "' ";
                try
                {
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    if (objReader.Read())
                    {
                        strTipoUsuario = objReader["sigla"].ToString();
                    }
                    else
                    {
                        strTipoUsuario = string.Empty;
                    }
                    objReader.Close();
                    objReader.Dispose();
                    objReader = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return strTipoUsuario;
        }
        #endregion

        #region getInfoUsuario
        /// <summary>
        /// Busca as informações sobre determinado usuário
        /// </summary>
        /// <param name="strCodigoUsuario"></param>
        /// <returns></returns>
        static public System.Data.SqlClient.SqlDataReader getInfoUsuario(string strCodigoUsuario)
        {
            String strSql = String.Empty;
            System.Data.SqlClient.SqlDataReader objSqlDataReader = null;

            try
            {
                strSql = "Select * from pessoa ";
                strSql += "where pessoa.pessoa_codigo = '" + strCodigoUsuario + "' ";
                objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objSqlDataReader;
        }
        #endregion

        #region getEMailUsuario
        /// <summary>
        /// Busca o nome do cargo de trabalho a partir do valor informado 
        /// </summary>
        /// <param name="strCodigoCargo">Codigo do cargo do usuário.</param>
        /// <returns>O nome do cargo de trabalho do usuario.</returns>
        static public String getEMailUsuario(String strCodigoUsuario)
        {
            String strSql = String.Empty;
            String strEmail = String.Empty;

            strSql = "select email from pessoa ";
            strSql += "Where ";
            strSql += "pessoa_codigo = '" + strCodigoUsuario + "'";

            try
            {
                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    strEmail = objReader["email"].ToString();
                }
                else
                {
                    strEmail = string.Empty;
                }
                objReader.Close();
                objReader.Dispose();
                objReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strEmail;
        }
        #endregion

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">Nome do objeto DropDownList</param>
        /// <param name="strItemDefault">Nome do item que será default ao carregar o DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string strItemDefault)
        {
            try
            {
                String strSql = String.Empty;
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                ClsUsuario objUsuario = new ClsUsuario();

                strSql = objBanco.montaQuery(objUsuario.objAtributos, false);
                strSql += " ORDER BY nome";

                objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objDropDownList.DataTextField = objUsuario.objNome.Campo;
                objDropDownList.DataValueField = objUsuario.Codigo.Campo;
                objDropDownList.DataBind();
                objUsuario = null;
                objBanco = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = strItemDefault;
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);
                itemDefault = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">Nome do objeto DropDownList</param>
        /// <param name="ItemDefault">Objeto do tipo ListItem que possui os valores a serem usados como o item default. Caso seja passado o valor null nao haverá item default</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, ListItem itemDefault)
        {
            try
            {
                String strSql = String.Empty;
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                ClsUsuario objUsuario = new ClsUsuario();

                strSql = objBanco.montaQuery(objUsuario.objAtributos, false);
                strSql += " ORDER BY nome";

                objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objDropDownList.DataTextField = objUsuario.objNome.Campo;
                objDropDownList.DataValueField = objUsuario.Codigo.Campo;
                objDropDownList.DataBind();
                objUsuario = null;
                objBanco = null;

                if (itemDefault != null)
                {
                    itemDefault.Selected = true;
                    objDropDownList.Items.Insert(0, itemDefault);
                    itemDefault = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo ddlProprietario
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDropDownList"></param>
        public static void geraDropDownListProprietario(DropDownList objDropDownList)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "SELECT pessoa_codigo, nome FROM pessoa";
                strSql += " WHERE pessoa_codigo IN (SELECT pessoa_codigo_proprietario FROM chamado)";
                strSql += " OR pessoa_codigo IN (SELECT pessoa_codigo_proprietario FROM incidente)";
                strSql += " OR pessoa_codigo IN (SELECT pessoa_codigo_proprietario FROM problema)";
                strSql += " ORDER BY nome";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objDropDownList.DataValueField = "pessoa_codigo";
                objDropDownList.DataTextField = "nome";
                objDropDownList.DataSource = objDataReader;
                objDropDownList.DataBind();
                objDataReader.Dispose();
                objDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region metodo verificaAcessoUsuarioFuncao
        /// <summary>
        /// Verifica se um determinado perfil está atribuido a um determinado usuário para uma aplicação.
        /// </summary>
        /// <param name="strPessoa">String que representa o código da pessoa.</param>
        /// <param name="strEstrutura">String que representa o código da estrutura.</param>
        /// <param name="strAplicacao">String que representa o código da aplicacao.</param> 
        /// <param name="strTipoUsuario">String que representa o código do tipo de usuario atribuido.</param> 
        /// <returns>Retorna true se a o perfil está atribuido ao usuario. False se não.</returns>
        public static bool verificaAcessoUsuarioFuncao(int pessoa_codigo, int strFuncao, int strTipoUsuario)
        {
            //return true;

            //Usuário é admin ou strFuncao é zero (acesso liberado)
            if (strFuncao == 0 || strTipoUsuario == Convert.ToInt32(ClsParametro.CodigoUsuarioAdministrador))
                return true;

            try
            {
                /*string strSql = "SELECT direito_codigo";
                strSql += " FROM direitoperfil, perfil, perfilestrutura, pessoaperfilestrutura";
                strSql += " WHERE direitoperfil.perfil_codigo = perfil.perfil_codigo";
                strSql += " AND perfil.perfil_codigo = perfilestrutura.perfil_codigo";
                strSql += " AND perfilestrutura.perfil_estrutura_codigo = pessoaperfilestrutura.perfil_estrutura_codigo";
                strSql += " AND direitoperfil.funcao_codigo = " + strFuncao;
                strSql += " AND perfil.tipo_usuario_codigo = " + strTipoUsuario;
                strSql += " AND pessoa_codigo = " + pessoa_codigo;*/

                string sql = string.Format(@"select COUNT(*) AS acesso from DireitoPerfil AS A INNER JOIN Pessoa AS B ON A.perfil_codigo = B.tipo_usuario_codigo
                            WHERE A.funcao_codigo = {0} AND B.pessoa_codigo = {1}",strFuncao,pessoa_codigo);

                var result = ClsBanco.ExecuteScalar(sql);

                return Convert.ToInt32(result) > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region metodo verificaAcessoPapel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intPessoaCodigo"></param>
        /// <param name="strTabelaCampo"></param>
        /// <param name="intItemCodigo"></param>
        /// <param name="intAcaoCodigo"></param>
        /// <param name="strTabela"></param>
        /// <returns></returns>
        public static bool verificaAcessoPapel(int intPessoaCodigo, int intItemCodigo, int intAcaoCodigo, String strTabela)
        {
            String strSql = String.Empty;
            bool bolRetorno = false;
            System.Data.SqlClient.SqlDataReader objDataReader = null;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            try
            {
                if ((ClsUsuario.getTipoUsuarioCodigo(intPessoaCodigo).ToString() == ClsParametro.CodigoUsuarioAdministrador) && (ClsUsuario.getTipoUsuarioCodigo(intPessoaCodigo) > 0))
                {
                    bolRetorno = true;
                }
                else
                {

                    strSql = "SELECT campo_tabela";
                    strSql += " FROM SegurancaDireitoPapel, SegurancaPapel";
                    strSql += " WHERE SegurancaDireitoPapel.seguranca_papel_codigo = SegurancaPapel.seguranca_papel_codigo";
                    strSql += " AND seguranca_direito_codigo = " + intAcaoCodigo.ToString();
                    strSql += " AND UPPER(tabela) = '" + strTabela.ToUpper() + "'";
                    objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                    while (objDataReader.Read() && !bolRetorno)
                    {
                        if (objBanco.retornaValorCampo(strTabela, objDataReader["campo_tabela"].ToString(), objDataReader["campo_tabela"].ToString() + " = " + intPessoaCodigo.ToString() + " AND " + strTabela + "_codigo = " + intItemCodigo.ToString()) != String.Empty)
                        {
                            bolRetorno = true;
                        }
                    }
                }
            }
            catch
            {
                bolRetorno = false;
            }

            finally
            {
                objBanco = null;
                if (objDataReader != null)
                {
                    objDataReader.Dispose();
                }
                objDataReader = null;
            }

            return bolRetorno;

        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Nome da pessoa para pesquisa
        /// </summary>
        /// <param name="strPessoaNome"></param>
        /// <returns></returns>

        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strPessoaNome)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "SELECT pessoa_codigo, matricula, nome";
                strSql += " FROM Pessoa ";
                strSql += " WHERE nome LIKE '%" + strPessoaNome + "%'";
                strSql += " ORDER BY nome ";

                objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Verifica o perfil e o nível
        /// <summary>
        /// Verifica se o usuário é do perfil analista e se esta em grupo superior ao primeiro.
        /// </summary>
        /// <param name="intCodigoPessoa">Código do usuário</param>
        /// <returns>Retorna true ou false. Se esta ou não no perfil analista e em grupo superior ao primeiro</returns>
        public static bool VerificaPerfilNivel(int intCodigoPessoa)
        {
            //=====================================================================//
            // - O que: Verifica se todos os parametros necessário foram informados.
            // - Quem: Fernanda Passos.
            // - Quando: 06/03/2006 ás 12:24hs.
            //=====================================================================//
            if (ClsParametro.CodigoNivel1.Trim() == string.Empty || ClsParametro.CodigoDoSistema.Trim() == string.Empty || ClsParametro.CodigoAnalistaServiceDesk.Trim() == string.Empty || intCodigoPessoa == 0) return false;
            //=====================================================================//

            bool bolValorRetorno = false;

            string strSql = " pessoa_codigo = " + intCodigoPessoa + "";
            strSql += " and equipe_codigo in (select equipe_codigo from equipe where equipe_nivel_codigo <> " + Convert.ToInt32(ClsParametro.CodigoNivel1.Trim()) + ")";
            strSql += " and pessoa_codigo in (select pessoa_codigo from PessoaPerfilEstrutura ";
            strSql += " where perfil_estrutura_codigo in (select perfil_estrutura_codigo from perfilestrutura where perfil_codigo in (select perfil_codigo from perfil where tipo_usuario_codigo = " + Convert.ToInt32(ClsParametro.CodigoAnalistaServiceDesk.Trim()) + " and aplicacao_codigo = " + Convert.ToInt32(ClsParametro.CodigoDoSistema.Trim()) + ")))";

            //=====================================================================//
            // - O que: Executa a verificação.
            // - Quem: Fernanda Passos.
            // - Quando: 06/03/2006 ás 12:24hs.
            //=====================================================================//
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.retornaValorCampo("equipepessoa", "pessoa_codigo", strSql) != string.Empty) bolValorRetorno = true; else bolValorRetorno = false;
            objBanco = null;
            return bolValorRetorno;
            //=====================================================================//
        }
        #endregion

        #region Pega o código do proprietário
        /// <summary>
        /// Pega o código do proprietário
        /// </summary>
        /// <param name="strTabela">Nome da tabela física no banco de dados</param>
        /// <param name="intTabelaIdentificador">Código identificador do registro na tabela</param>
        /// <returns>Retorna o código do usuário proprietário</returns>
        public static string GetCodigoProprietario(string strTabela, int intTabelaIdentificador)
        {
            string strSql = string.Empty;

            if (strTabela.Trim() == "Chamado") strSql = "chamado_codigo = " + intTabelaIdentificador + "";
            if (strTabela.Trim() == "RequisicaoMudanca") strSql = "requisicaomudanca_codigo = " + intTabelaIdentificador + "";
            if (strTabela.Trim() == "RequisicaoServico") strSql = "requisicaoservico_codigo = " + intTabelaIdentificador + "";

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            return objBanco.retornaValorCampo(strTabela.Trim(), "pessoa_codigo_proprietario", strSql);

        }
        #endregion

        #region Retorna nome do usuário pelo código de rede
        public static bool GetLogin(string codigo_rede, out UsuarioLogado user)
        {
            user = null;

            string strSql = @"SELECT pessoa_codigo,codigo_rede,matricula,nome,tipo_usuario_codigo,senha,status_codigo,email 
                     From Pessoa WHERE codigo_rede = @codigo_rede and status_codigo=16";

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@codigo_rede", codigo_rede));

            DataTable dt = ClsBanco.ExecuteDataSet(strSql, parametros);

            if (dt.Rows.Count == 0)
                return false;          //Usuário não encontrado
            else
            {
                //Logon com sucesso
                user = new UsuarioLogado(dt);
                return true;
            }
        }

        /// <summary>
        /// Retorna nome do usuário pelo código de rede.
        /// </summary>
        /// <param name="strCodigoRede">Codigo de rede.</param>
        /// <returns>O nome do usuário.</returns>
        static public string GetLogin(string codigo_rede, string senha, out UsuarioLogado user)
        {
            user = null;

            if (string.IsNullOrEmpty(codigo_rede)) return null;

            string strSql = "SELECT status_codigo From Pessoa WHERE codigo_rede = @codigo_rede";

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@codigo_rede", codigo_rede));

            DataTable dt = ClsBanco.ExecuteDataSet(strSql, parametros);

            if (dt.Rows.Count == 0)
                return "INVALIDO";          //Usuário não encontrado
            else if (Convert.ToInt32(dt.Rows[0]["status_codigo"]) != 16)
                return "DESATIVADO";        //Usuário não está ativo
            else
            {
                //usuário encontrado e ativo, valida a senha
                strSql = @"SELECT pessoa_codigo,codigo_rede,matricula,nome,tipo_usuario_codigo,senha,status_codigo,email
                     From Pessoa WHERE codigo_rede = @codigo_rede and senha = @senha";

                parametros = new List<SqlParameter>();
                parametros.Add(new SqlParameter("@codigo_rede", codigo_rede));
                parametros.Add(new SqlParameter("@senha", senha));

                dt = ClsBanco.ExecuteDataSet(strSql, parametros);

                if (dt.Rows.Count == 0)
                    return "SENHA";
                else
                {
                    //Logon com sucesso
                    user = new UsuarioLogado(dt);
                    return "OK";
                }
            }
        }
        #endregion
        #endregion

        #region acesso au usuário via Sessão
        /// <summary>
        /// Retorna o código do usuário logado
        /// </summary>
        /// <returns>Codigo do Usuario</returns>
        public static int getCodigoUsuario()
        {
            UsuarioLogado user = (UsuarioLogado)HttpContext.Current.Session["USUARIOLOGADO"];
            return user == null ? -1 : user.IDusuario;
        }

        /// <summary>
        /// Pegar codigo de rede do usuário logado
        /// </summary>
        /// <returns></returns>
        public static string getCodigoRede()
        {
            UsuarioLogado user = (UsuarioLogado)HttpContext.Current.Session["USUARIOLOGADO"];
            return user == null ? string.Empty : user.CodigoRede;
        }

        /// <summary>
        /// Busca o nome do usuário logado
        /// </summary>
        /// <returns>O nome do usuário.</returns>
        public static string getNomeUsuario()
        {
            UsuarioLogado user = (UsuarioLogado)HttpContext.Current.Session["USUARIOLOGADO"];
            return user == null ? string.Empty : user.Nome;
        }
        #endregion
    }
}