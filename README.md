# RegisterAPI

##  Desafio proposto

1 - Recebimento e Salvamento de Dados Cadastrais: Quando um usuário se cadastra, seus dados devem ser salvos internamente para referências futuras.

2 - Integração com Serviço Externo para Cadastro em Sistema Parceiro: Após salvar os dados internamente, o sistema deve enviar esses dados para um sistema parceiro externo e confirmar o sucesso do cadastro.

3 - Integração com Outro Serviço Externo para o Mesmo Objetivo: Os dados salvos devem ser enviados para um segundo sistema parceiro externo, com confirmação de cadastro bem-sucedido.

4 - Tratamento de Falhas na Integração: Em caso de falhas durante a integração com sistemas parceiros, o sistema deve registrar a falha e notificar os administradores para ação corretiva.

5 - Verificação de Dados no Sistema Parceiro: O sistema deve permitir a verificação dos dados nos sistemas parceiros para garantir a consistência entre os sistemas.

-----------------------------------------------------------------------------------------------------

### Execução do sistema

1 - Na raiz do projeto, possui um arquivo docker-compose.yaml, para executa-lo basta abrir o cmd como admin e executar o comando: docker-compose up -d
Fazendo isso, já temos nosso servidor SQLServer, RabbitMq e MongoDB criados.

2 - Executar "script.sql" no banco de dados SQLServer.
ServerName: 127.0.0.1 Login: sa Password: gs123456!

3 - Executar as soluções através do dotnet run ou através do visual studio diretamente.
