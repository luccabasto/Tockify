# Tockify

**Tockify** é um sistema de gerenciamento de tarefas construído com .NET, seguindo princípios de DDD, SOLID e arquitetado em camadas. Ele permite:

- Cadastro de usuários (perfil Client ou Admin).
- Criação de listas de tarefas (“cards”) associadas a usuários Client.
- Cada lista pode conter subtarefas (até 7 por lista).
- Persistência em MongoDB (facilmente trocável por outra tecnologia).
- API REST simples e documentada via Swagger.

---

## Estrutura de Projetos

```
Tockify.sln
│
├── Tockify.Domain            (Entidades, Value Objects, Enums, Interfaces de Repositório)
│
├── Tockify.Application       (DTOs, Use Cases, AutoMapper)
│
├── Tockify.Infrastructure    (MongoContext, Implementações de Repositórios)
│
└── Tockify.WebAPI           (Controllers, DI, Swagger, config)
```

### Camadas

1. **Domain**:  
   - Modelos puros do domínio (`ClientUserModel`, `TaskListModel`, `TaskItemModel`).  
   - Exceções de domínio (`DomainException`).  
   - Interfaces de repositório (`IClientUserRepository`, etc.).

2. **Application**:  
   - Comandos e DTOs para entrada/saída (e.g., `CreateClientUserCommand`, `TaskListDto`).  
   - Use Cases, cada um em sua interface e implementação (`CreateClientUserUseCase`, `GetAllClientUsersUseCase`, etc.).  
   - Mapeamentos de AutoMapper em `MappingProfile`.

3. **Infrastructure**:  
   - `MongoContext` (configura `MongoClient` + coleção de documentos).  
   - Repositórios concretos que implementam as interfaces do Domain, usando MongoDB (`ClientRepository`, `ToDoListRepository`, `TaskItemRepository`).

4. **WebAPI**:  
   - Controllers REST (`ClientUserController`, `CardController` e `TaskItemController`).  
   - Registro de dependências em `Program.cs` (injeção de repositório e use cases).  
   - Swagger habilitado para documentação automática.

---

## Tecnologias

- **.NET 7 / ASP.NET Core Web API**  
- **MongoDB** (driver nativo, `MongoContext` com `GuidRepresentation.Standard`)  
- **AutoMapper** (mapeamento de entidades ↔ DTOs)  
- **Swagger / OpenAPI** (documentação de endpoints)  
- **xUnit + Moq** (sugestão para testes)

---

## Como Executar

1. **Pré-requisitos**:  
   - .NET 7 SDK  
   - MongoDB Community (serviço `mongod` rodando em `localhost:27017`)

2. **Configurar**  
   - Em `Tockify.WebAPI/appsettings.json`, ajuste (se necessário):
     ```jsonc
     {
       "ConnectionStrings": {
         "MongoConnection": "mongodb://localhost:27017"
       },
       "MongoDatabase": "TockifyDb"
     }
     ```

3. **Rodar**  
   - Abra a solução no Visual Studio (ou VS Code) e defina **Tockify.WebAPI** como projeto de inicialização.  
   - Execute (F5 ou `dotnet run`).  
   - Acesse `https://localhost:5001/swagger` para ver os endpoints.

---

## Endpoints Principais

- `POST /api/ClientUser`  
  Cadastra um usuário Client.  
  **Body mínimo**:  
  ```json
  {
    "name": "João Silva",
    "email": "joao.silva@example.com",
    "password": "senha123",
    "gender": "Male"
  }
  ```

- `GET /api/ClientUser`  
  Lista todos os usuários Client.

- `POST /api/Card`  
  Cria uma nova lista de tarefas para um usuário (Client).  
  **Body mínimo**:  
  ```json
  {
    "userId": "<GUID do usuário>",
    "name": "Minha Lista",
    "description": "Descrição breve",
    "dueDate": "2025-06-30T00:00:00Z"
  }
  ```

- `GET /api/Card/user/{userId}`  
  Lista as listas de tarefas de um usuário.

- `POST /api/TaskItem`  
  Cria uma subtarefa em uma TaskList.  
  **Body mínimo**:  
  ```json
  {
    "name": "Comprar leite",
    "description": "Detalhes",
    "taskListId": "<GUID da lista>",
    "dueDate": "2025-06-15T00:00:00Z"
  }
  ```

- `GET /api/TaskItem/task/{taskListId}`  
  Lista todas as subtarefas de uma TaskList.

---

## Principais Conceitos Aplicados

- **DDD (Domain-Driven Design)**:  
  - Entidades ricas em comportamento e validações internas.  
  - Interfaces de repositório desacoplando domínio de persistência.  
  - Agregados: `ClientUser` → `TaskList` → `TaskItem`.

- **SOLID**:  
  - **SRP**: cada classe faz apenas uma coisa.  
  - **OCP**: extensões sem modificar código existente (ex.: trocar repositório).  
  - **LSP**: implementações respeitam contratos de interface.  
  - **ISP**: interfaces pequenas e específicas para cada caso de uso.  
  - **DIP**: dependência de abstrações (injeção de dependência).

- **Injeção de Dependências**:  
  Registrado em `Program.cs` para injetar `MongoContext`, repositórios e use cases.  

- **AutoMapper**:  
  Mapeia automaticamente entre entidades e DTOs.

---

## Contato

Qualquer dúvida, sugestão ou contribuição, abra uma issue ou pull request no repositório oficial.  
