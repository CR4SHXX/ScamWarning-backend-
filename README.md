# ScamWarning Backend API (School Project Demo)

A simplified .NET 9 Web API backend for a scam warning platform where users can submit, view, and comment on scam warnings. **This version is specifically simplified for a school project demo** - no external dependencies, no authentication complexity.

## Features

- **Simple User Authentication**: No JWT tokens - login returns user info directly
- **Warning Management**: Create, approve/reject, and search scam warnings
- **Auto-Approved Warnings**: All warnings are automatically approved for easy testing
- **Comment System**: Add and retrieve comments on warnings
- **Category Management**: Pre-seeded categories (Phishing, Phone Scam, Investment Scam, Romance Scam, Other)
- **No External Dependencies**: Uses InMemory database - runs without SQL Server installation

## Technology Stack

- .NET 9.0
- Entity Framework Core 9.0 (InMemory Database)
- Plain text password comparison (for demo only)
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
- Simple plain text password comparison (demo only - not for production)
- Basic password validation (min 3 characters)

### UserService
- User registration with email uniqueness validation
- Login returns user info (id, username, email, isAdmin)
- User retrieval by ID
- Email existence checking

### WarningService
- Create warnings (automatically Approved for demo)
- Approve/reject warnings (public for demo)
- Get all approved warnings
- Get pending warnings (public for demo)
- Search and filter warnings by term and category

### CommentService
- Add comments to warnings (no authentication required)
- Get all comments for a warning

### CategoryService
- Get all available categories
- Validate category existence

## Setup Instructions

### Prerequisites
- .NET 9 SDK (download from https://dotnet.microsoft.com/download)

### Installation

1. Clone the repository
```bash
git clone <repository-url>
cd ScamWarning-backend-
```

2. Run the application
```bash
dotnet run
```

That's it! The API will be available at `http://localhost:5000`

**No database setup required!** The app uses an InMemory database and automatically seeds sample data on startup.

## API Documentation

Swagger UI is available at:
- `http://localhost:5000/swagger`

## Demo Credentials

A demo user is automatically created on startup:
- **Email**: demo@test.com
- **Password**: demo123
- **Is Admin**: true

## Quick Test

```bash
# Get all categories
curl http://localhost:5000/api/categories

# Get all warnings
curl http://localhost:5000/api/warnings

# Login (returns user info, not token)
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email": "demo@test.com", "password": "demo123"}'

# Create a new warning (auto-approved)
curl -X POST http://localhost:5000/api/warnings \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Test Scam",
    "description": "This is a test scam warning",
    "warningSigns": "Watch out for these signs",
    "categoryId": 1,
    "userId": 1
  }'

# Add a comment
curl -X POST http://localhost:5000/api/warnings/1/comments \
  -H "Content-Type: application/json" \
  -d '{"text": "Great warning!", "userId": 1}'
```

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

## Pre-Seeded Data

On startup, the application automatically seeds:

### Categories
1. Phishing
2. Phone Scam
3. Investment Scam
4. Romance Scam
5. Other

### Demo User
- Username: demo
- Email: demo@test.com
- Password: demo123
- Is Admin: true

### Sample Warnings
1. "Fake Bank Email" - Phishing category
2. "IRS Phone Call Scam" - Phone Scam category

## Key Simplifications for Demo

⚠️ **This version is for school project demos only. DO NOT use in production!**

1. **No JWT Authentication**: Login returns user info directly, no tokens needed
2. **Plain Text Passwords**: Passwords stored as plain text (demo only!)
3. **InMemory Database**: No SQL Server required, data resets on restart
4. **Auto-Approved Warnings**: All warnings automatically approved
5. **No Authorization**: All endpoints are public for easy testing
6. **Simplified Validation**: Minimal password requirements (3+ chars)

## What's Different from Production Version

This simplified version removes:
- ❌ JWT Bearer Authentication
- ❌ BCrypt password hashing
- ❌ SQL Server dependency
- ❌ Database migrations
- ❌ [Authorize] attributes on controllers
- ❌ Approval workflow for warnings
- ❌ Admin-only endpoints

## For Production Use

To make this production-ready, you would need to:
1. Add JWT authentication back
2. Use BCrypt for password hashing
3. Connect to a real database (SQL Server, PostgreSQL, etc.)
4. Add proper authorization with [Authorize] attributes
5. Implement approval workflow for warnings
6. Add rate limiting and security headers
7. Use strong password requirements
8. Add input sanitization and validation
9. Implement proper error handling and logging
10. Add HTTPS enforcement

## License

[Specify your license here]
