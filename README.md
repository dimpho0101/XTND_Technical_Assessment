# XTND Task Management API

## Overview

XTND Task Management API is a full-featured REST API for managing users and their tasks. It implements industry-standard patterns including:

- **CQRS (Command Query Responsibility Segregation)** - Separates read and write operations
- **MediatR** - Mediator pattern for loose coupling between handlers
- **Clean Architecture** - Clear separation of concerns with organized layers
- **Comprehensive Logging** - Every operation tracked with full context

- ### Key Capabilities

✅ Create, read, update, and delete users and tasks  
✅ Pagination support for large datasets  
✅ Constraint validation (prevent deleting users with assigned tasks)  
✅ Complete HTTP request/response logging  
✅ CRUD operation logging with business context  
✅ Professional Swagger/OpenAPI documentation  
✅ PostgreSQL database with EF Core migrations  

### System Architecture Overview
<img width="2204" height="4484" alt="image" src="https://github.com/user-attachments/assets/72636b7f-e7f4-4946-8aee-94b31a3b4669" />



## Prerequisites

### Required
- **.NET 10 SDK**
- **PostgreSQL 14+**
  - Ensure PostgreSQL is running
  - Note your username and password

 ##  Installation
### Step 1: Clone the Repository

git clone https://github.com/dimpho0101/XTND_Technical_Assessment.git
cd XTND_Technical_Assessment

### Step 2: Configure Database Connection

Open `appsettings.json` and update the PostgreSQL connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=xtnd_db;Username=postgres;Password=YourPassword"
  }
}

### Step 3: Restore NuGet Packages

```cmd
dotnet restore

### Step 4: Apply Database Migrations

The migrations run automatically on application startup, but you can also run them manually:

```cmd
dotnet ef database update
```

This creates the database schema with:
- `task_users` table (users)
- `tasks` table (tasks)
- `task_item_statuses` table (status lookup)

- ## 🚀 Running the Application

### Visual Studio or command line
Visual Studio
1. Open `XTND_Technical_Assessment.sln` in Visual Studio 2022
2. Press **F5** or click **Run**
3. The app will launch at `https://localhost:5149`
4. Swagger UI will open automatically at the root path

Command line
1. cd XTND_Technical_Assessment 
2. dotnet run

### Step 3: Access the Application

- **Swagger UI:** `http://localhost:5149` (or `https://localhost:5149`)
- **API Base:** `http://localhost:5149/api`

## Testing the API

### Using Swagger UI

1. Navigate to `http://localhost:5149`
2. Click on any endpoint to expand it
3. Click **Try it out**
4. Fill in parameters and click **Execute**
5. View the response

### Using the .http File

Open `XTND_Technical_Assessment.http` in Visual Studio and click **Send Request** on any request.

##  Features

### User Management
- ✅ Create users with display name
- ✅ Retrieve user by ID
- ✅ Delete user (with constraint validation)
- ✅ Prevent deletion of users with assigned tasks

### Task Management
- ✅ Create tasks and assign to users
- ✅ Retrieve task by ID (with user and status names)
- ✅ Update task title and status
- ✅ Delete task by ID
- ✅ List all tasks with pagination
- ✅ List user's tasks with pagination

### Data Management
- ✅ Automatic timestamp tracking (created_at, updated_at)
- ✅ Status lookup (Backlog, In Progress, Completed)
- ✅ Constraint validation (foreign keys, restrictions)

### API Features
- ✅ Pagination (offset/limit, max 100 per page)
- ✅ Proper HTTP status codes (200, 201, 204, 400, 404)
- ✅ JSON responses with camelCase property names
- ✅ Swagger/OpenAPI documentation
- ✅ Request/response examples

### Logging & Monitoring
- ✅ HTTP request/response logging with
- ✅ CRUD operation logging
- ✅ Query execution logging
- ✅ Error and exception logging
- ✅ Constraint violation logging
- ✅ Performance tracking

## API Endpoints

### Tasks

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/tasks` | Create a new task |
| GET | `/api/tasks/{id}` | Get task by ID |
| PUT | `/api/tasks/{id}` | Update task |
| DELETE | `/api/tasks/{id}` | Delete task |
| GET | `/api/tasks` | Get all tasks (paginated) |
| GET | `/api/tasks/user/{userId}` | Get user's tasks (paginated) |

### Users

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/users` | Create a new user |
| GET | `/api/users/{id}` | Get user by ID |
| DELETE | `/api/users/{id}` | Delete user |

### Query Parameters

**Pagination (for list endpoints):**
- `offset` - Number of records to skip (default: 0)
- `limit` - Number of records to return (default: 10, max: 100)





