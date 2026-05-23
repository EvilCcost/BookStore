# BookStore API

RESTful API for managing a bookstore, built with **.NET 8** and **Entity Framework Core**, backed by **PostgreSQL**. Implements a modular architecture with controllers, services, and repositories for clean separation of concerns.

## Features

- **Book Management** — Full CRUD for books with search, authorship, and category assignment
- **Author & Category Catalog** — Manage authors and categories with search capabilities
- **Copy (Ejemplar) Tracking** — Individual copy management with barcode, condition, and status tracking
- **Borrowing (Préstamo) System** — Loan management with check-out, return, cancellation, and overdue tracking
- **Reservations** — Book reservation system with pending/fulfilled/cancelled workflow
- **Sales (Venta)** — Point-of-sale transactions with payment method support
- **Membership Cards (Carnet)** — User identification cards with expiration control
- **Fines (Multa)** — Automated fine management for overdue or damaged items
- **Inspections** — Condition inspection log for copies during loan cycles
- **User & Role Management** — Role-based access control with user administration
- **Secure Authentication** — JWT-based authentication with refresh token support
- **Swagger UI** — Interactive API documentation available in development

## Architecture

```
HTTP Request
    │
    ▼
Controller ──► Service ──► Repository ──► DbContext ──► PostgreSQL
(JWT Auth)    (Business     (Data Access)   (EF Core 8)
               Logic)
```

- **Controllers** — Handle HTTP requests, enforce authorization via `[Authorize]`
- **Services** — Encapsulate business logic, validation, and DTO mapping
- **Repositories** — Abstract data access with generic CRUD + domain-specific queries
- **DbContext** — EF Core `BookStoreContext` with 22 `DbSet` entities

## Tech Stack

| Technology | Purpose |
|---|---|
| .NET 8 | Runtime & framework |
| ASP.NET Core | Web API framework |
| Entity Framework Core 8 | ORM (code-first with migrations) |
| PostgreSQL | Database (via Npgsql) |
| JWT Bearer | Authentication & authorization |
| BCrypt.Net-Next | Password hashing |
| Swashbuckle (Swagger) | API documentation |
| DotNetEnv | Environment variable management |

## Entity Model

The domain model consists of **15 core entities** and **7 catalog entities**:

### Core Entities
`Usuario` · `Rol` · `Sesion` · `Carnet` · `Libro` · `Autor` · `Categoria` · `Ejemplar` · `Prestamo` · `DetallePrestamo` · `Reserva` · `Venta` · `DetalleVenta` · `Inspeccion` · `Multa`

### Catalogs
`EstadoEjemplar` · `CondicionEjemplar` · `EstadoPrestamo` · `EstadoReserva` · `EstadoVenta` · `MetodoPago` · `TipoInspeccion`

## API Endpoints

| Area | Controller | Route | Roles |
|---|---|---|---|
| Users | `UsuarioController` | `/api/usuario` | Admin |
| Books | `LibroController` | `/api/libro` | Admin, Bibliotecario, Cliente |
| Authors | `AutorController` | `/api/autores` | Admin, Bibliotecario |
| Categories | `CategoriaController` | `/api/categorias` | Admin, Bibliotecario |
| Copies | `EjemplarController` | `/api/ejemplares` | Admin, Bibliotecario |
| Loans | `PrestamoController` | `/api/prestamos` | Admin, Bibliotecario |
| Reservations | `ReservaController` | `/api/reservas` | Admin, Bibliotecario |
| Sales | `VentaController` | `/api/ventas` | Admin, Bibliotecario |
| Inspections | `InspeccionController` | `/api/inspecciones` | Admin, Bibliotecario |
| Fines | `MultaController` | `/api/multas` | Admin, Bibliotecario |
| Membership Cards | `CarnetController` | `/api/carnets` | Admin, Bibliotecario |
| Sessions | `SesionController` | `/api/sesiones` | Admin |
| Roles | `RolController` | `/api/roles` | Admin |
| Catalogs | `CatalogoController` | `/api/catalogos` | Admin, Bibliotecario, Cliente |

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/) 14+

### Setup

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd BookBackend
   ```

2. **Configure environment variables**

   Create a `.env` file in the project root (a template is already included):

   ```env
   DB_CONNECTION=Host=localhost;Port=5432;Database=DatabaseName;Username=;Password=
   JWT_SECRET=your-secure-secret-key-at-least-32-characters
   ```

3. **Apply database migrations**

   ```bash
   dotnet ef database update
   ```

4. **Run the application**

   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5214` and Swagger UI at `http://localhost:5214/swagger`.

## Project Structure

```
BookBackend/
├── Controllers/        # API controllers
├── Data/               # DbContext and configuration
├── Migrations/         # EF Core migrations
├── Modelos/            # Domain entities
│   ├── DTOs/           # Data Transfer Objects
│       ├── AutorDto/
│       ├── CarnetDto/
│       ├── CatalogoDto/
│       ├── CategoriaDto/
│       ├── EjemplarDto/
│       ├── InspeccionDto/
│       ├── LibroDto/
│       ├── MultaDto/
│       ├── PrestamoDto/
│       ├── ReservaDto/
│       ├── RolDto/
│       ├── SesionDto/
│       ├── UsuarioDto/
│       └── VentaDto/
├── Repositorio/        # Repository layer
├── Servicios/          # Service layer (business logic)
├── Properties/         # Launch settings
├── Program.cs          # Entry point and DI configuration
└── .env                # Environment variables (not tracked)
```
