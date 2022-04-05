select nivel_atendimento_codigo, equipe_codigo_alocacao, pessoa_codigo_alocacao 
from incidente where incidente_codigo = 25

SELECT * FROM notificacao

--chamado - OK
SELECT * from escalacaohorizontal where tabela = 'chamado' 
and tabela_identificador = 133

select nivel_atendimento_codigo, equipe_codigo_alocacao, pessoa_codigo_alocacao 
from chamado WHERE chamado_codigo = '133'

--incidente
SELECT * from escalacaohorizontal where tabela = 'incidente' 
and tabela_identificador = 30

select nivel_atendimento_codigo, equipe_codigo_alocacao, pessoa_codigo_alocacao 
from incidente WHERE incidente_codigo = '30'

--RS
SELECT * from escalacaohorizontal where tabela = 'requisicaoservico' 
and tabela_identificador = 21

select nivel_atendimento_codigo, equipe_codigo_alocacao, pessoa_codigo_alocacao 
from requisicaoservico WHERE requisicaoservico_codigo = '21'




