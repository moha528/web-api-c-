# TodoApi

API web ASP.NET Core avec MongoDB, authentification JWT et autorisation par rôles.

**TP .NET — DIC3 INFO — 2025/2026**

## Prérequis

- .NET 10 SDK
- Docker & Docker Compose (ou MongoDB installé localement)

## Lancer avec Docker

```bash
docker-compose up --build
```

L'API est accessible sur http://localhost:5000/swagger

## Lancer en local (sans Docker)

Assurez-vous que MongoDB tourne sur `localhost:27017`, puis :

```bash
dotnet restore
dotnet run
```

## Tester l'API

1. **Créer un admin** : `POST /api/auth/register-admin`
   ```json
   {
     "email": "admin@test.com",
     "userName": "admin",
     "password": "Admin123!"
   }
   ```

2. **Se connecter** : `POST /api/auth/login`
   ```json
   {
     "email": "admin@test.com",
     "password": "Admin123!"
   }
   ```
   → Copier le token retourné

3. Dans Swagger, cliquer **Authorize**, coller `Bearer {token}`

4. Tester les endpoints CRUD sur `/api/todo`

## Sécurité

| Endpoint | Accès |
|----------|-------|
| `GET /api/todo` | Tout utilisateur authentifié |
| `GET /api/todo/{id}` | Tout utilisateur authentifié |
| `POST /api/todo` | Admin uniquement |
| `PUT /api/todo/{id}` | Admin uniquement |
| `DELETE /api/todo/{id}` | Admin uniquement |

## Structure du projet

```
TodoApi/
├── Controllers/
│   ├── TodoController.cs
│   └── AuthController.cs
├── Models/
│   ├── TodoItem.cs
│   ├── ApplicationUser.cs
│   ├── ApplicationRole.cs
│   └── DTOs/
│       ├── RegisterRequest.cs
│       ├── LoginRequest.cs
│       └── AuthResponse.cs
├── Services/
│   ├── TodoService.cs
│   └── TokenService.cs
├── Settings/
│   ├── MongoDbSettings.cs
│   └── JwtSettings.cs
├── Program.cs
├── appsettings.json
├── Dockerfile
├── docker-compose.yml
└── README.md
```

## Auteur

mtseck — ESP DIC3 INFO — 2025/2026
