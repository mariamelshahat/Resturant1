# Restaurantopia 🍽️

A comprehensive restaurant management system built using ASP.NET Core MVC. The system provides an interface for managing products, orders, and users with full authentication and authorization support.

## Prerequisites 📋

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended) or [VS Code](https://code.visualstudio.com/)

## Installation and Setup 🚀

### 1. Database Setup

```sql
-- Create Database
CREATE DATABASE Restaurantopia;
GO

-- Create User
CREATE LOGIN NewUser WITH PASSWORD = '123';
GO

USE Restaurantopia;
GO

CREATE USER NewUser FOR LOGIN NewUser;
GO

-- Grant Permissions
ALTER ROLE db_owner ADD MEMBER NewUser;
GO
```

### 2. Run Project Locally

```bash
# Clone the project
git clone [project-url]
cd Restaurantopia

# Restore packages
dotnet restore

# Run migrations
dotnet ef database update

# Run the project
dotnet run
```

### 3. Run with Docker

```bash
# Build and run containers
docker-compose up -d --build

# View logs
docker-compose logs -f

# Stop containers
docker-compose down
```

## Project Structure 📁

```
Restaurantopia/
├── Controllers/         # Controllers
├── Models/             # Data Models
├── Views/              # User Interfaces
├── wwwroot/           # Static Files (CSS, JS, Images)
├── Interfaces/        # Interfaces
├── Repositories/      # Data Repositories
└── Program.cs         # Entry Point
```

## Key Features ✨

- 🔐 Integrated Authentication and Authorization
- 👥 User and Role Management
- 🍕 Product and Category Management
- 🛒 Complete Order System
- 📱 Responsive User Interface
- 🖼️ Image Upload and Display Support
- 🔍 Search and Filtering

## Available Roles 👥

1. **Admin**
   - Manage products and categories
   - Manage users and roles
   - View and manage orders

2. **Customer**
   - Browse products
   - Create and track orders
   - Add reviews

## Configuration 🔧

### Connection String

```json
{
  "ConnectionStrings": {
    "Default": "Server=host.docker.internal;Database=Restaurantopia;User Id=NewUser;Password=123;TrustServerCertificate=true;MultipleActiveResultSets=true;"
  }
}
```

### Ports

- Application: `http://localhost:8080`
- Database: `1433`

## Docker 🐳

### 📦 Pull the Image from Docker Hub

You can pull the prebuilt Docker image directly from Docker Hub:

https://hub.docker.com/r/hajermabrouk/restaurantopia

```bash
docker pull hajermabrouk/restaurantopia:latest


### Key Files

- `Dockerfile`: Image build settings
- `docker-compose.yml`: Service configuration
- `.dockerignore`: Ignore unnecessary files

### Useful Commands

```bash
# Build image
docker build -t restaurantopia .

# Run container
docker run -d -p 8080:80 restaurantopia

# View logs
docker logs restaurantopia-app

# Enter container
docker exec -it restaurantopia-app sh
```

## Troubleshooting 🔍

1. **Database Connection Issues**
   - Ensure SQL Server is running
   - Verify Connection String
   - Check user permissions

2. **Image Display Issues**
   - Ensure images exist in `/wwwroot/Images`
   - Check folder permissions
   - Verify paths in code

3. **Docker Issues**
   - Ensure Docker Desktop is running
   - Try restarting Docker
   - Check logs for errors

## Contributing 🤝

1. Fork the project
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License 📄

This project is licensed under the [MIT License](LICENSE).

