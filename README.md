
# 🚀 Trainee Management Web API (ASP.NET Core + ADO.NET + JWT)

This project is a **.NET Core Web API** that manages Trainee records with **CRUD operations**, using:

- ✅ ADO.NET + Stored Procedures
- ✅ JWT-based Authentication (no role-based routing)
- ✅ Trainee-level authorization (access to their own data only)
- ✅ Swagger UI with token-based testing

---

## 📦 Technologies Used

- ASP.NET Core Web API (.NET 6+)
- ADO.NET for DB access
- SQL Server (with Stored Procedures)
- JWT (JSON Web Tokens)
- Swagger UI (for API testing)
- C#

---

## 📁 Project Structure

```
Trainee_webAPI/
│
├── Controllers/
│   └── TraineeController.cs
│   └── AuthController.cs
│
├── Models/
│   └── Trainee.cs
│   └── LoginModel.cs
│
├── Services/
│   └── JwtService.cs
│
├── appsettings.json
└── Program.cs
```

---

## 🔐 JWT Authentication

### 🔸 Login

```json
POST /api/auth/login
{
  "email": "example@gmail.com",
  "password": "123456"
}
```

Response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

Use this token in Swagger:

```
Authorization: Bearer <your_token>
```

---

## 🔄 CRUD Endpoints

All endpoints require a **valid JWT token**.

| Method | Endpoint          | Description         |
|--------|-------------------|---------------------|
| GET    | `/api/trainee/getall`      | Get all trainees (only if authenticated) |
| POST   | `/api/trainee/create`      | Create new trainee        |
| PUT    | `/api/trainee/update`      | Update own profile        |
| DELETE | `/api/trainee/delete`      | Delete own profile        |

---

## 🗄️ Database Table: `Trainees`

| Column       | Type         |
|--------------|--------------|
| TraineeID    | int (PK, IDENTITY) |
| Name         | varchar      |
| Email        | varchar      |
| PhoneNumber  | varchar      |
| Department   | varchar      |
| JoiningDate  | datetime     |
| Gender       | varchar      |
| Photo        | varbinary(max) |
| Password     | varchar      |

---

## ⚙️ Stored Procedures

You should use the following stored procedures:

- `sp_CreateTrainee`
- `sp_GetAllTrainees`
- `sp_UpdateTrainee`
- `sp_DeleteTrainee`
- `sp_GetTraineeByEmail`

---

## 🧪 Swagger UI

To test endpoints:
1. Run the app → `https://localhost:5001/swagger`
2. Use `/api/auth/login` to get token.
3. Click **Authorize** 🔒 → paste token:
   ```
   Bearer eyJhbGciOi...
   ```

---

## 👨‍💻 Developed During Internship

> This project was developed while undergoing training at **Claysys Technologies** as part of practical learning on secure API development.

---

## ✅ Features

- 🔒 JWT Auth with Claims
- 📦 ADO.NET + Stored Procs (No EF Core)
- 👤 Trainee-specific update/delete
- 📂 File upload support (Photo as byte[])
- 📘 Clean Swagger UI for testing

---

## Author
ClaySys Training / Anbarasan P / GitHub🖥️: https://github.com/Anbarasan-P

## 📄 License

MIT License - Use freely for learning and development.
