# 🛒 Store Management System API

A clean, secure, and scalable **ASP.NET Core Web API** for managing an online store.  
Includes product management, basket/cart, orders, payments, and authentication — built with **Clean Architecture**, **EF Core**, **JWT**, and **Stripe API** integration.

---

## 🚀 Features

- ✅ **JWT Authentication & ASP.NET Identity**
- 🛍️ **Product Management** (CRUD, filtering, searching, pagination)
- 🧺 **Shopping Basket** with session/user management
- 📦 **Order Processing** and checkout flow
- 💳 **Stripe Payment Integration**
- 🔐 **Role-based Access Control**
- 🔁 **Caching** using custom attributes
- 🔍 **Swagger UI** for testing
- 🧰 **AutoMapper** for DTO mapping
- 🧱 **4-Layer Clean Architecture**

---

## 🛠️ Tech Stack

| Layer        | Technology                           |
|--------------|----------------------------------------|
| Framework    | ASP.NET Core 6.0                      |
| ORM          | Entity Framework Core                 |
| Database     | SQL Server                            |
| Auth         | ASP.NET Identity + JWT                |
| Payments     | Stripe API                            |
| Docs         | Swagger                               |
| Mapping      | AutoMapper                            |
| Logging      | Built-in Middleware + Exception Filter|

---

## 🧱 Project Structure (4-Layered Architecture)

### 1️⃣ **Presentation Layer** – (API)
- `Controllers/`: All RESTful endpoints (Products, Orders, Basket, Payment, Auth)
- `Middlewares/`: Global exception handling middleware
- `Extensions/`: Service configuration, Swagger, Identity
- `Helper/`: Caching, Seed data, utilities

---

### 2️⃣ **Business Layer** – (Services)
- (implicitly handled within Controllers + Helper logic)
- Clean separation of logic for:
  - Basket
  - Orders
  - Payments
  - Seeding
  - Token management

---

### 3️⃣ **Data Access Layer** – (Repositories)
- `Store.Data/`
  - `Context/StoreDbContext.cs` → Main app DB context  
  - `Context/StoreIdentityDbContext.cs` → Identity DB context  
  - `Configurations/` → Entity configuration using Fluent API  
  - `Entities/` → Models: `Product`, `Order`, `DeliveryMethod`, etc.

---

### 4️⃣ **Infrastructure Layer** – (Persistence & Identity)
- `Entities/IdentityEntities/` inside Identity Context  
- EF Core `Migrations` folder (not included in ZIP but assumed present)

---

### ⚙️ App Entry & Config

- `Program.cs`: Configures middleware, services, Identity, and routing  
- `appsettings.json`: Connection strings, JWT settings, Stripe keys

---

## 📦 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/USERNAME/REPO_NAME.git
cd Store.API
