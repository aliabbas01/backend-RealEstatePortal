# Real Estate Portal - Backend API

![.NET Core](https://img.shields.io/badge/.NET-6.0-purple)
![Entity Framework](https://img.shields.io/badge/EF%20Core-7.0-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-orange)
![JWT Auth](https://img.shields.io/badge/JWT-Auth-green)

A RESTful API for property management with JWT authentication, built with .NET 6.

## Features

- **User Authentication**
  - JWT-based registration/login
  - Role-based authorization (Buyer/Agent/Admin)
  - Password hashing with salt

- **Property Management**
  - CRUD operations for properties
  - Advanced filtering (price, bedrooms, location)
  - Image URL handling

- **Favorites System**
  - Save/unsave properties
  - Get user's favorite listings

## API Documentation

### Base URL
`https://api.realestateportal.com/v1`

### Authentication
Requires JWT in `Authorization` header:



### Endpoints

#### Auth
| Endpoint | Method | Description |
|----------|--------|-------------|
| `/auth/register` | POST | Register new user |
| `/auth/login` | POST | Authenticate user |

#### Properties
| Endpoint | Method | Description |
|----------|--------|-------------|
| `/properties` | GET | Get filtered properties |
| `/properties/{id}` | GET | Get property details |
| `/properties/favorites` | GET | Get user favorites (Auth) |
| `/properties/favorites/{id}` | POST | Toggle favorite (Auth) |

## Request/Response Examples

**Register User**
```http
POST /auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "SecurePass123!"
}

appsetting.json

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=RealEstateDB;Trusted_Connection=True;"
  },
  "AppSettings": {
    "Token": "your-64-character-secret-key",
    "TokenExpirationDays": 7
  },
  "AzureStorage": {
    "ConnectionString": "",
    "ContainerName": "property-images"
  }
}
