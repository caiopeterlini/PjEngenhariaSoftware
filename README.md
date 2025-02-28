# Projeto Engenharia de Software
 Sistema completo para gerenciamento de pedidos por cliente utilizando técnologias abaixo:
 - Front-End: Angular 18;
 - Back-End: .net core 8;
 - Mensageria: RabbitMQ;
 - Consumer Menssageria: Simple Api .net core 8
 - Containers: Docker;
 - Banco de Dados: MySql.  


Passo necessario para Executar projeto.
- Executar na raiz do projeto : docker-compose up  --build -d (Rabbitmq e mysql)
- Executar na pasta \BackEnd\WebApi : dotnet run   
- Executar na pasta \Microservice\QueueMessage\QueueMessage.Consumer : dotnet run
- Executar \FrontEnd : npm i
- Executar \FrontEnd : ng serve

