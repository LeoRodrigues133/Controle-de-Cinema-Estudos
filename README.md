# Controle de Cinema 

<div align="center">

| <img width="60" src="https://user-images.githubusercontent.com/25181517/121405754-b4f48f80-c95d-11eb-8893-fc325bde617f.png"> | <img width="60" src="https://miro.medium.com/v2/resize:fit:300/0*cdEEkdP1WAuz-Xkb.png"> | <img width="60" src="https://raw.githubusercontent.com/altmann/FluentResults/master/resources/icons/FluentResults-Icon-64.png"> | <img width="60" src="https://rodrigoesilva.wordpress.com/wp-content/uploads/2011/04/sqlserver_sql_server_2008_logo.png"> |
|:---:|:---:|:---:|:---:|
| .NET Core | ASP.NET Core | FluentResults | Microsoft SQL Server |
||
| <img width="60" src="https://www.infoport.es/wp-content/uploads/2023/09/entity-core.png"> | <img width="60" src="https://api.nuget.org/v3-flatcontainer/dapper/2.1.35/icon"> | <img width="60" src="https://www.lambdatest.com/blog/wp-content/uploads/2021/03/MSTest.png"> |
| EF Core | Dapper | MSTest |

</div>

## Projeto

Sistema web para gerenciamento de sessões de cinema com organização de filmes, salas, categorias e controle de exibição.

### Arquitetura
- DDD
- N-Camadas

### Stack:
- .NET 8.0
- ASP.NET MVC
- Microsoft Identity
- Microsoft SQL Server
- Entity Framework Core
- AutoMapper
- Dapper
- FluentResults

### Inclui:
- Testes de Unidade
- Testes de Integração
- Autenticação e Autorização com Microsoft Identity

---

## Detalhes

O **Controle de Cinema** tem como objetivo facilitar o gerenciamento completo das operações de um cinema, abrangendo:

- Cadastro de filmes e categorias;
- Gerenciamento de salas de exibição;
- Agendamento de sessões com controle de data, horário e status;
- Encerramento de sessões;
- Venda de ingressos (simulação);
- Controle de usuários autenticados com permissões.

Interface moderna e responsiva, utilizando Bootstrap, ícones interativos com Bootstrap Icons e estrutura otimizada para boa experiência de uso.

---

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior)
- SQL Server
- Visual Studio ou VS Code

---

## Como Usar

#### Clone o Repositório
```bash
git clone https://github.com/leorodrigues133/controle-de-cinema.git
````
#### Navegue até a pasta raiz da solução

```bash
cd controle-de-cinema
````
#### Restaure as dependências

```bash
dotnet restore
````
#### Navegue até a pasta do projeto Web

```bash
cd ControleDeCinema.WebApp
````
#### Execute o projeto

```bash
dotnet run
````

Licença

Este projeto é distribuído sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

## Demonstração

![Demonstração do sistema](ControleDeCinema.WebApp/wwwroot/img/apresentacao.gif?raw=true)

---
