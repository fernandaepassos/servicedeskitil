using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsuarioLogado
/// </summary>
public class UsuarioLogado
{
    public int IDusuario { get; set; }
    public string Matricula { get; set; }
    public string CodigoRede { get; set; }
    public string Senha { get; set; }
    public string Nome { get; set; }
    public int TipoUsuario { get; set; }
    public string Email { get; set; }
    public int Status { get; set; }

    public UsuarioLogado()
    {

    }

    public UsuarioLogado(DataTable dt)
    {
        IDusuario = Convert.ToInt32(dt.Rows[0]["pessoa_codigo"]);
        CodigoRede = dt.Rows[0]["codigo_rede"].ToString();
        Matricula = dt.Rows[0]["matricula"].ToString();
        Senha = dt.Rows[0]["senha"].ToString();
        Nome = dt.Rows[0]["nome"].ToString();
        TipoUsuario = Convert.ToInt32(dt.Rows[0]["tipo_usuario_codigo"]);
        Email = dt.Rows[0]["email"].ToString();
        Status = Convert.ToInt32(dt.Rows[0]["status_codigo"]);
    }
}