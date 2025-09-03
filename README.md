# Sistema de Vendas - Microserviços

Este projeto é uma solução de microserviços para um sistema de vendas, desenvolvido em .NET. Ele é composto por duas principais APIs: Vendas e Estoque, além de uma biblioteca compartilhada (Shared). O objetivo é demonstrar uma arquitetura moderna, escalável e desacoplada para operações de vendas e controle de estoque.

## Sumário

- [Visão Geral](#visão-geral)
- [Arquitetura](#arquitetura)
- [Serviços](#serviços)
- [Como Executar](#como-executar)
- [Configuração](#configuração)
- [Migrações e Banco de Dados](#migrações-e-banco-de-dados)
- [Testes](#testes)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Estrutura de Pastas](#estrutura-de-pastas)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Visão Geral

O sistema foi projetado para simular operações de vendas e controle de estoque. Cada serviço é independente, facilitando manutenção, escalabilidade e deploy.

## Arquitetura

- **Vendas.API**: Gerencia pedidos de venda, integração com estoque e regras de negócio.
- **Estoque.API**: Controla produtos e quantidade em estoque.
- **Shared**: Biblioteca de classes e modelos compartilhados entre os serviços.

A comunicação entre os serviços pode ser feita via HTTP REST, e pode ser expandida para mensageria (RabbitMQ, Kafka) conforme necessidade.

## Serviços

### Vendas.API

- CRUD de vendas
- Integração com Estoque
- Controllers: `VendasController`, `VendasPedidoController`

### Estoque.API

- CRUD de produtos
- Controle de quantidade
- Controller: `ProdutosController`

## Como Executar

1. **Pré-requisitos:**
   - .NET 8.0 ou superior instalado
   - SQL Server ou outro banco compatível
2. **Restaurar pacotes:**
   ```powershell
   dotnet restore
   ```
3. **Executar migrações:**
   ```powershell
   dotnet ef database update --project Vendas.API
   dotnet ef database update --project Estoque.API
   ```
4. **Executar os serviços:**
   ```powershell
   dotnet run --project Vendas.API
   dotnet run --project Estoque.API
   ```
5. **Acessar endpoints:**
   - Vendas: `https://localhost:xxxx/swagger`
   - Estoque: `https://localhost:xxxx/swagger`

## Configuração

- Os arquivos `appsettings.json` e `appsettings.Development.json` de cada serviço contêm as configurações de conexão com banco, portas, etc.
- O arquivo `launchSettings.json` define perfis de execução para facilitar o desenvolvimento.

## Migrações e Banco de Dados

- As migrações estão nas pastas `Migrations` de cada API.
- Para criar novas migrações:
  ```powershell
  dotnet ef migrations add <NomeMigracao> --project Vendas.API
  dotnet ef migrations add <NomeMigracao> --project Estoque.API
  ```

## Testes

- Os testes podem ser adicionados em projetos separados ou nas próprias APIs.
- Para executar testes:
  ```powershell
  dotnet test
  ```

## Tecnologias Utilizadas

- .NET 8.0/9.0
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)

## Estrutura de Pastas

```
EcommerceMicroservices.sln
Estoque.API/
Shared/
Vendas.API/
```

Cada pasta contém seu próprio projeto, controllers, modelos, migrações e configurações.

## Contribuição

1. Fork este repositório
2. Crie uma branch (`git checkout -b feature/nova-feature`)
3. Commit suas alterações (`git commit -am 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/nova-feature`)
5. Abra um Pull Request

## Licença

Este projeto está sob a licença MIT.
