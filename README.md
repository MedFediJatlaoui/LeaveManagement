# 🏖️ Leave Management System API (ASP.NET Core)

Hi, I'm **Mohamed Fedi Jatlaoui**, a passionate Software Engineer, **blockchain enthusiast** and backend developer.  
This project is an **assessment to join Dar Blockchain** as a **Junior .NET Developer**.  
It reflects my interest in clean backend architecture, real-world business logic, and modern deployment practices — and I’d be excited to be part of your team!

---

## 🌐 Live Demo

**Swagger UI:**  
👉 [http://localhost:5006/swagger/index.html](http://localhost:5006/swagger/index.html)  
⚙️ Make sure Docker is running and port **5006** is available.

✅ The API is also **deployed on Render** — proving its readiness for production environments.

---

## 📦 Features

- ✅ **CRUD operations** for leave requests
- 🔍 **Filtering**, **pagination**, **sorting**, and **search**
- ✅ **Admin approval** of pending leave requests
- ❗ **Business rules** implementation (e.g., no overlaps, leave limits)
- 📊 **Summary report** endpoint per year and filters
- 📐 **Clean architecture** with separation of concerns
- 🧱 **Design patterns**: Repository
- 🐳 **Dockerized** app with `docker-compose`
- 🧪 **Seeded** SQLite database with initial data

---

## 🧰 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core + SQLite
- AutoMapper + DTOs
- Swagger (OpenAPI)
- Docker & Docker Compose
- LINQ & Predicate Builder
- Clean Architecture Principles

---

## 🚀 Getting Started

### Prerequisites

- [Docker]
- [Git]

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/leave-management-api.git
cd leave-management-api
```

### 2. Run docker-compose
```bash
docker-compose up 
```
### 3. Access Swagger UI

Once the Docker containers are up and running, you can test the API by accessing the Swagger UI.

1. Open your browser and go to the following URL:  
   👉 [http://localhost:5006/swagger/index.html]

2. This will open the Swagger UI, where you can explore and test all the available API endpoints.

3. Use the interactive interface to make requests directly from the browser and see the responses in real time.

   The Swagger UI provides an easy and visual way to interact with your API, making it ideal for both testing and documentation.
