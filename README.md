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


Screen shorts:
<img width="1357" height="680" alt="Favorite_Buyer" src="https://github.com/user-attachments/assets/46fd4836-c259-4360-9e54-80bd3afb65a7" />
<img width="1360" height="669" alt="DetailPage" src="https://github.com/user-attachments/assets/37c1e48c-6d30-4a7e-875c-faae58a1ea46" />
<img width="1362" height="540" alt="Register" src="https://github.com/user-attachments/assets/723987a9-3d1b-4525-adb8-71c516de82e3" />
<img width="1356" height="585" alt="LoginPage" src="https://github.com/user-attachments/assets/dc611cac-cb09-4a77-9790-6c86f177006c" />
<img width="1343" height="682" alt="LandingPage" src="https://github.com/user-attachments/assets/8d2cdb43-8cda-49c9-af48-6dfd0d39a963" />
<img width="1365" height="475" alt="Filter" src="https://github.com/user-attachments/assets/8f93989b-067c-4cfd-a582-6a7d9dbad1ac" />


