<div>
    <p align="center">
      <img src="https://img.shields.io/badge/StudentFlow-API-green" height="130" alt="student-flow-api">
    </p>
</div>

![Status de Desenvolvimento](https://img.shields.io/badge/Status-Concluido-green)

## Tecnologias Utilizadas

- C#

- .NET

- HTML, CSS, JavaScript

- PostgreSQL

## Funcionalidades

- Cadastrar, alterar, deletar e listar estudantes.

- Conversa entre Backend e Frontend

## Requisição

- Buscando Estudante pelo ID
```
GET http://localhost:5278/v1/students/{id}
```
- Retorno

```json
{
  "data": {
    "id": 0,
    "name": "string",
    "registration": "string",
    "course": {
      "courseName": "string",
      "duration": 0
    },
    "email": "string",
    "dateOfBirth": "2025-02-02T16:40:35.159Z"
  },
  "message": "string"
}
```

## Como executar o projeto em sua máquina

1. Instale alguma das IDEs para o desenvolvimento .NET, temos o Rider e o Visual Studio
<br>[link para o Rider](https://www.jetbrains.com/pt-br/rider/)
<br>[link para o Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/)

2. Instale a versão do .NET 8.0.12 no link abaixo

[.NET 8.0.12](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

3. Instale o PostreSQL na sua máquina

[Site oficial do PostgreSQL](https://www.postgresql.org/download/)

4. Clone o repositório, no Git Bash digite o comando:

```
git clone https://github.com/FabricioCarvalho348/Student_Management_Prova-04-WEB-I.git
```

5. Dentro do projeto "StudentFlow.Api" tem dois arquivos appsettings.json e appsettings.Development.json coloque sua conexão do PostgreSQL no campo correspondente

```
  "ConnectionStrings:DefaultConnection": "Host=localhost;Database={NOME_DO_BANCO_DE_DADOS};Username={USERNAME_POSTGRESQL};Password={SUA_SENHA_DO_POSTGRESQL}"
```

6. Instalar o EF Tools

```
dotnet tool install --global dotnet-ef
```

7. Subir as migrações pelo prompt

```
dotnet ef database update
```

8. Executar o backend, clique no botão RUN na sua IDE

9. Executar o Frontend (utilizando o live server no VSCode ou executando separadamente o frontend)


