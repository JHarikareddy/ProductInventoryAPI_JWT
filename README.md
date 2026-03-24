# Product Inventory Management API (JWT Secured)

## Project Description

This project is a secure RESTful API built using ASP.NET Core for managing product inventory. It implements JWT (JSON Web Token) Authentication and Role-Based Authorization to control access to different API endpoints.

---

## Objective

To build a secure Product Inventory Management API using:

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Role-Based Authorization

---

## Authentication & Authorization

This API uses **JWT (JSON Web Token)** for authentication.

###  User Roles

* **Admin** → Full access (GET, POST, PUT, DELETE)
* **Manager** → Can add & update products (GET, POST, PUT)
* **Viewer** → Read-only access (GET only)

---

## Demo Credentials

| Username | Password   | Role    |
| -------- | ---------- | ------- |
| admin    | admin123   | Admin   |
| manager  | manager123 | Manager |
| viewer   | viewer123  | Viewer  |

---

### Core Features

* Add new product
* View all products
* View product by ID
* Update product
* Delete product

### Bonus Features

* Filter products by category
* Sort products by price
* View out-of-stock products

### Security Features

* JWT Authentication
* Role-Based Authorization
* Protected endpoints using `[Authorize]`

---

## Technologies Used

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* Swagger (API Testing)

---

## Setup Instructions

1. Clone the repository
2. Open the project in Visual Studio Code
3. Update connection string in `appsettings.json`
4. Run the application:

   ```
   dotnet run
   ```
5. Open Swagger:

   ```
   https://localhost:xxxx/swagger
   ```

---

## API Endpoints

### Authentication

* `POST /api/auth/login` → Generate JWT Token

###  Product APIs

| Method | Endpoint          | Description       |
| ------ | ----------------- | ----------------- |
| GET    | /api/product      | Get all products  |
| GET    | /api/product/{id} | Get product by ID |
| POST   | /api/product      | Add product       |
| PUT    | /api/product/{id} | Update product    |
| DELETE | /api/product/{id} | Delete product    |

### Bonus APIs

* `GET /api/product/category/{category}` → Filter by category
* `GET /api/product/sort` → Sort by price
* `GET /api/product/outofstock` → Out-of-stock products

---

## 📸 Screenshots

### 1️. GET without Token → 401 Unauthorized

![GET without Token](screenshots/1_GET_without_token.png)

### 2️. Viewer DELETE → 403 Forbidden

![Viewer DELETE](screenshots/2_Viewer_DELETE_403.png)

### 3️. Admin DELETE → 200 OK

![Admin DELETE](screenshots/3_Admin_DELETE_200.png)

### 4️. Manager POST → 201 Created

![Manager POST](screenshots/4_Manager_POST_201.png)

### 5️. Manager DELETE → 403 Forbidden

![Manager DELETE](screenshots/5_Manager_DELETE_403.png)

---

## Error Handling

* **400 Bad Request** → Invalid input
* **401 Unauthorized** → Missing/Invalid token
* **403 Forbidden** → Role not permitted
* **404 Not Found** → Resource not found

---

## Key Concepts Used

* JWT Authentication
* Role-Based Authorization
* Middleware in ASP.NET Core
* REST API Design
* Entity Framework Core

---

---
