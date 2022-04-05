--Alterações das querys da Classe Usuario.



--getUsuario
SELECT pessoa.matricula, pessoa.nome  
from pessoa, pessoaperfilestrutura, perfilestrutura, perfil, tipousuario, aplicacao 
WHERE  
pessoaperfilestrutura.pessoa_codigo = pessoa.pessoa_codigo 
AND pessoaperfilestrutura.perfil_estrutura_codigo = perfilestrutura.perfil_estrutura_codigo 
AND perfilestrutura.perfil_codigo = perfil.perfil_codigo 
AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo 
AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo 
AND aplicacao.aplicacao_codigo = '2' 
AND tipousuario.sigla in ('ADM','EXE','SOL'

-- for (i = 0; i < strTipUsu.Length; i++)
-- {
--     '" + strTipUsu[i].ToString() + "'
--     if ((i + 1) < strTipUsu.Length)
--     {
--         ,
--     }
-- }
)
AND pessoa.codigo_rede = 'administrador' 


--getUsuarios()
SELECT pessoa.matricula, pessoa.nome  
from pessoa, pessoaperfilestrutura, perfilestrutura, perfil, tipousuario, aplicacao 
WHERE  
pessoaperfilestrutura.pessoa_codigo = pessoa.pessoa_codigo 
AND pessoaperfilestrutura.perfil_estrutura_codigo = perfilestrutura.perfil_estrutura_codigo 
AND perfilestrutura.perfil_codigo = perfil.perfil_codigo 
AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo 
AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo 
AND aplicacao.aplicacao_codigo = '2' 



--getTipoUsuario
SELECT tipousuario.tipo_usuario_codigo, tipousuario.sigla 
FROM pessoa, pessoaperfilestrutura, perfilestrutura, perfil, tipousuario, aplicacao 
WHERE  
pessoaperfilestrutura.pessoa_codigo = pessoa.pessoa_codigo 
AND pessoaperfilestrutura.perfil_estrutura_codigo = perfilestrutura.perfil_estrutura_codigo 
AND perfilestrutura.perfil_codigo = perfil.perfil_codigo 
AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo 
AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo 
AND aplicacao.aplicacao_codigo = '2' 
AND pessoa.codigo_rede = 'administrador' 


