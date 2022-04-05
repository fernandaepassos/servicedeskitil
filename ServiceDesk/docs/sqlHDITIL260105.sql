select * from statuslog 
where tabela = 'chamado'
and tabela_identificador = 26
and status_codigo_origem = 1
and status_codigo_destino = 2


select * from workflow

Select * from status where status_codigo in (
Select status_codigo from statustabela where tabela = 'chamado')


SELECT item_configuracao_codigo, item_configuracao_codigo_superior, nome, 
(SELECT count(*) FROM ItemConfiguracao 
WHERE item_configuracao_codigo_superior = item.item_configuracao_codigo) pai 
FROM ItemConfiguracao item 
WHERE item_configuracao_codigo_superior is null AND item_configuracao_tipo_codigo = 1 ORDER BY nome

select * from itemConfiguracao
WHERE --item_configuracao_codigo_superior is null
--AND 
item_configuracao_tipo_codigo = 1 

update itemConfiguracao set item_configuracao_codigo_superior = null where item_configuracao_codigo =1


SELECT tipousuario.tipo_usuario_codigo, tipousuario.sigla 
FROM pessoa, pessoaperfil, perfilestrutura, perfil, tipousuario, aplicacao 
WHERE  
pessoaperfilempresa.pessoa_codigo = pessoa.pessoa_codigo 
AND pessoaperfilempresa.perfil_empresa_codigo = perfilestrutura.perfil_estrutura_codigo 
AND perfilestrutura.perfil_codigo = perfil.perfil_codigo 
AND perfil.tipo_usuario_codigo = tipousuario.tipo_usuario_codigo 
AND perfil.aplicacao_codigo = aplicacao.aplicacao_codigo 
AND aplicacao.aplicacao_codigo = '2' 
AND pessoa.codigo_rede = 'admministrador' 

select * from pessoaperfilempresa


select * from anexo