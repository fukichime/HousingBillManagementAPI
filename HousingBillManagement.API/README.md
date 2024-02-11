# Housing Bill Management System

## Introduction

The Housing Bill Management System is a web application designed to manage bills and apartment information in a housing complex. The system allows administrators to create, update, and delete bills, manage apartment details, and assign bills to apartments.

## Features

- User registration and authentication with JWT-based token generation.
- Apartment management for creating, updating, and deleting apartments.
- Bill management for creating, updating, and deleting bills.
- Monthly bill total calculation for each building.
- User-specific bill view.

## Technologies Used

- **ASP.NET Core**: Backend framework for building the web application.
- **Entity Framework Core**: ORM for database operations.
- **Identity Framework**: Handles user registration, authentication, and authorization.
- **JWT (JSON Web Token)**: Used for secure user authentication.
- **SQL Server**: Database to store application data.

## Libraries Used

- **Microsoft.AspNetCore.Identity.EntityFrameworkCore**: Provides Entity Framework Core support for the ASP.NET Identity system.
- **Microsoft.EntityFrameworkCore.SqlServer**: Entity Framework Core SQL Server database provider.
- **Microsoft.Extensions.Identity.Stores**: Extended functionalities for Identity stores.
- **Microsoft.IdentityModel.Tokens**: Library for handling JWT authentication tokens.
- **Swashbuckle.AspNetCore**: Adds Swagger/OpenAPI functionality to the project for API documentation.
- **AutoMapper.Extensions.Microsoft.DependencyInjection**: Simplifies object-to-object mapping.
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson**: Allows the use of Newtonsoft.Json as the JSON serializer for ASP.NET Core.

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/) (optional but recommended)

### Installation

1. Clone the repository:

```bash
git clone https://github.com/yourusername/housing-bill-management.git
