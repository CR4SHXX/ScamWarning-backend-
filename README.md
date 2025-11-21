# ScamWarning Backend API

A .NET 9 Web API backend for a scam warning platform where users can submit, view, and comment on scam warnings.

## Features

- **User Authentication**: JWT-based authentication with secure password hashing (BCrypt)
- **Warning Management**: Create, approve/reject, and search scam warnings
- **Comment System**: Add and retrieve comments on warnings
- **Category Management**: Pre-defined scam categories (Phishing, Phone Scam, Investment Scam, Romance Scam, Other)
- **Role-Based Access**: Admin users can approve/reject pending warnings

## Technology Stack

- .NET 9.0
- Entity Framework Core 9.0
- SQL Server (LocalDB for development)
- JWT Bearer Authentication
- BCrypt.Net for password hashing
- Swagger/OpenAPI for API documentation

## Project Structure

```
ScamWarning/
├── Controllers/          # API Controllers (to be added)
├── Data/                 # Database context
├── DTOs/                 # Data Transfer Objects
├── Interfaces/           # Repository and Service interfaces
├── Models/               # Entity models
├── Repositories/         # Repository implementations
├── Services/             # Business logic services
│   ├── Interfaces/      # Service interfaces
│   ├── AuthService.cs
│   ├── CategoryService.cs
│   ├── CommentService.cs
│   ├── UserService.cs
│   └── WarningService.cs
└── Program.cs           # Application entry point
```

## Services

### AuthService
- JWT token generation with 7-day expiration
- BCrypt password hashing
- Password strength validation (min 8 chars, uppercase, lowercase, digit)

### UserService
- User registration with email uniqueness validation
- Login with JWT token generation
- User retrieval by ID
- Email existence checking

### WarningService
- Create warnings (default status: Pending)
- Approve/reject warnings (admin only)
- Get all approved warnings
- Get pending warnings (admin only)
- Search and filter warnings by term and category

### CommentService
- Add comments to warnings
- Get all comments for a warning

### CategoryService
- Get all available categories
- Validate category existence

## Setup Instructions

### Prerequisites
- .NET 9 SDK
- SQL Server or SQL Server LocalDB

### Installation

1. Clone the repository
```bash
git clone <repository-url>
cd ScamWarning-backend-
```

2. Update database connection string in `appsettings.json` if needed
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ScamWarningDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Apply database migrations
```bash
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

The API will be available at `https://localhost:5001` (or the port specified in launchSettings.json)

## Configuration

### JWT Settings (appsettings.json)

**Important**: For production deployments, do NOT hardcode the JWT secret key in configuration files. Use environment variables or secure key management services (Azure Key Vault, AWS Secrets Manager, etc.).

```json
"JwtSettings": {
  "SecretKey": "YourSuperSecretKeyForJWTTokenGeneration123456789",
  "Issuer": "ScamWarningAPI",
  "Audience": "ScamWarningClient"
}
```

### Environment Variables (Production)

Set these environment variables in production:
- `JwtSettings__SecretKey`: Your secure JWT signing key
- `JwtSettings__Issuer`: Your API issuer name
- `JwtSettings__Audience`: Your client audience name
- `ConnectionStrings__DefaultConnection`: Your production database connection string

## API Documentation

When running in development mode, Swagger UI is available at:
- `https://localhost:5001/swagger`

## Database Schema

### Users
- Id, Username, Email, Password (hashed), IsAdmin, CreatedAt

### Categories
- Id, Name, Description
- Pre-seeded with 5 categories

### Warnings
- Id, Title, Description, WarningSigns, ImageUrl, AuthorId, CategoryId, Status, CreatedAt
- Status: "Pending", "Approved", or "Rejected"

### Comments
- Id, Text, WarningId, UserId, CreatedAt

## Security Features

- **Password Hashing**: BCrypt with automatic salt generation
- **Password Validation**: Minimum 8 characters, requires uppercase, lowercase, and digit
- **JWT Authentication**: Secure token-based authentication with 7-day expiration
- **Input Validation**: DTOs with data annotations for request validation
- **SQL Injection Protection**: Entity Framework parameterized queries

## Performance Considerations

The current implementation loads data into memory for filtering. For production deployments with large datasets, consider:

1. Implementing database-level filtering in repositories
2. Adding pagination support
3. Implementing caching for frequently accessed data
4. Adding indexes on frequently queried columns

## Next Steps

- Add API Controllers to expose the services
- Implement authorization policies for admin-only endpoints
- Add input validation attributes to DTOs
- Implement unit tests for services
- Add integration tests for API endpoints
- Configure CORS for frontend integration
- Set up logging and error handling middleware

## License

[Specify your license here]
