# 📌 Projeto MarkRent

## 🚀 Visão Geral
Este projeto é uma aplicação de aluguel de veículos que utiliza uma arquitetura moderna e tecnologias avançadas para garantir escalabilidade, confiabilidade e desempenho.

## 🏗 Tecnologias Utilizadas
O projeto faz uso das seguintes tecnologias:

- **.NET 8** - Framework principal para desenvolvimento da API.
- **Entity Framework Core** - ORM para manipulação do banco de dados.
- **PostgreSQL** - Banco de dados relacional utilizado.
- **Docker** - Containerização do ambiente para fácil deploy e configuração.
- **RabbitMQ** - Mensageria para comunicação assíncrona entre serviços.

## 🔧 Configuração do Ambiente

### 1️⃣ Pré-requisitos
Antes de rodar o projeto, certifique-se de ter instalado:

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (ou superior) com suporte a .NET 8
- [SDK .NET 8](https://dotnet.microsoft.com/)

### 2️⃣ Configuração e Execução

1. **Abrir o Visual Studio** e carregar a solução do projeto.
2. Certifique-se de que o projeto principal está definido como **docker-compose**.

## 📝 Banco de Dados
A aplicação utiliza **PostgreSQL** e pode ser acessada com as credenciais padrão definidas no `docker-compose.yml`.
- Para acessar o banco de dados **URL:** `http://localhost:15432`

- **Host:** `MarkRentdb`
- **Porta:** `5432`
- **Usuário:** `postgres`
- **Senha:** `postgres`
- **Banco de Dados:** `markrentdb`
- **container_name**: `MarkRent-db`

## 📩 Mensageria com RabbitMQ
O RabbitMQ é utilizado para comunicação entre serviços. Após rodar o `docker-compose`, o painel de administração do RabbitMQ estará disponível em:

- **URL:** `http://localhost:15672`
- **Usuário:** `guest`
- **Senha:** `guest`

##  Conclusão
Seguindo essas orientações, você conseguirá configurar e rodar o projeto sem dificuldades. Se houver dúvidas, consulte a documentação oficial das tecnologias utilizadas.


