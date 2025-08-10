# Order Management System

Sistema de gerenciamento de pedidos desenvolvido com .NET 8 (Backend) e React + TypeScript (Frontend).

## Tecnologias Utilizadas

### Backend

- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM
- **PostgreSQL** - Banco de dados
- **Azure Service Bus** - Processamento de filas
- **SignalR** - Comunicação em tempo real
- **AutoMapper** - Mapeamento de objetos
- **FluentValidation** - Validação de dados

### Frontend

- **React 19** - Framework JavaScript
- **TypeScript** - Linguagem tipada
- **Tailwind CSS** - Framework CSS
- **React Query** - Gerenciamento de estado
- **SignalR** - Comunicação em tempo real
- **React Hook Form** - Formulários
- **Zod** - Validação de schemas

## 🚀 Execução com Docker

### 1. Configuração das Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto com as seguintes variáveis:

```env
# Portas dos serviços
API_PORT=5197
FRONTEND_PORT=5173
POSTGRES_PORT=5432

# Configurações do PostgreSQL
POSTGRES_USER=root
POSTGRES_PASSWORD=password
POSTGRES_DB=order-management-db

# Configurações da API
API_DOTNET_ENV=Development
VITE_API_URL=http://localhost:${API_PORT}
FRONTEND_URL=http://localhost:${FRONTEND_PORT}

# Azure Service Bus
SERVICEBUS_UPDATE_ORDER_QUEUE= Sua chave de acesso para o serviço de mensageria
```

### 2. Executar com Docker Compose

```bash
docker-compose up --build

docker-compose down
```

### 3. Acessar as aplicações

- **Frontend**: http://localhost:5173
- **Backend API**: http://localhost:5197
- **Swagger**: http://localhost:5197/swagger
- **PostgreSQL**: localhost:5432

## 💻 Execução Local

### Variáveis de Ambiente para Desenvolvimento Local

#### Backend (.NET)

As configurações estão em `api/src/OrderManagement.Api/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "Connection": "Host=localhost;Port=5432;Database=order-management-db;Username=root;Password=password"
  },
  "ServiceBus": {
    "UpdateOrderQueue": "Sua chave de acesso ao serviço de mensageria"
  },
  "AllowedOrigins": "http://localhost:5173"
}
```

#### Frontend (React)

Crie um arquivo `.env` em `web/` com:

```env
VITE_API_URL=http://localhost:5197
```

### Configuração do Banco de Dados

#### Postgres

```bash
docker run --name postgres-order-management \
  -e POSTGRES_USER=root \
  -e POSTGRES_PASSWORD=password \
  -e POSTGRES_DB=order-management-db \
  -p 5432:5432 \
  -d postgres:15
```

### Executar o Backend (.NET)

```bash
cd api

dotnet restore

dotnet ef database update --project src/OrderManagement.Infra --startup-project src/OrderManagement.Api

dotnet run --project src/OrderManagement.Api

```

A API estará disponível em:

- **HTTP**: http://localhost:5197
- **Swagger**: http://localhost:5197/swagger

### 3. Executar o Frontend (React)

```bash
cd web

npm install

npm run dev
```

O frontend estará disponível em: http://localhost:5173
