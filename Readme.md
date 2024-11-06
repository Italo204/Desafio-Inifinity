# Projeto de Desafio

## Descrição do Projeto
Realizada uma API que simula uma empresa de fretes, feito para o desafio proposto pela Infinity-Brasil.
---

## Requisitos

- [.NET 6.0.135](https://dotnet.microsoft.com/pt-br/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

---

## Instalação

### 1. Instalando o .NET

1. Baixe a versão .NET 6.0.135 [aqui](https://dotnet.microsoft.com/download).
2. Siga as instruções para instalar o .NET em seu sistema operacional.
3. Verifique a instalação executando:
    ```bash
    dotnet --version
    ```

### 2. Instalando o SQL Server

1. Baixe o SQL Server [aqui](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads).
2. Siga o guia de instalação do SQL Server para o seu sistema.
3. Após a instalação, verifique se o serviço está ativo.

> **Configuração**: Baixe e instale o Azure Data Studio, para melhor experiência com o SQL Server.

### 3. Instalando Dependências do Projeto

1. No diretório raiz do projeto, execute o comando abaixo para restaurar as dependências:
    ```bash
    dotnet restore
    ```
2. Certifique-se de que todas as dependências foram instaladas corretamente.

---

## Executando o Projeto

1. Primeiro crie o seu banco de dados, importe o arquivo SqlDatabaseCode.sql para a sua máquina.

2. No diretório do projeto, execute:
    ```bash
    dotnet run
    ```

---
