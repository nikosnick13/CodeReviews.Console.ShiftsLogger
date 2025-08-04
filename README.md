# Shifts Logger - Fullstack Console App

A simple console-based UI + Web API project for managing shifts.

## ✅ Console UI (ShiftsLoggerUI)

- View all shifts
- View one shift by ID
- Add, update, and delete shifts

## 🛠️ Web API (ShiftsLoggerAPI)

- `GET /api/Shift` – Get all shifts
- `GET /api/Shift/{id}` – Get a shift by id
- `POST /api/Shift` – Create a shift
- `PUT /api/Shift/{id}` – Update a shift
- `DELETE /api/Shift/{id}` – Delete a shift

```bash
dotnet run


---

## 🧱 Technologies Used

- **C# / .NET 6+**
- **Spectre.Console** (for rich terminal UI)
- **HttpClient** (for REST communication)
- **Entity Framework Core** (for API persistence)
- **ASP.NET Core Web API**

---

## 🗂️ Project Structure

```bash
ShiftsLoggerSolution/
│
├── ShiftsLoggerUI/           # Terminal UI (client)
│   ├── Services/             # Handles API communication
│   ├── Models/               # Shift model
│   ├── Enums/                # Menu enum
│   ├── UserInterface.cs      # Console UI logic
│   ├── Utility.cs            # Utility helpers
│   └── Program.cs            # Entry point
│
└── ShiftsLoggerAPI/          # ASP.NET Core Web API
    ├── Controllers/          # ShiftController.cs
    ├── Models/               # Shift.cs
    ├── Data/                 # DbContext, Migrations
    ├── Program.cs            # API entry point
    └── appsettings.json      # API configuration
```

---

## 📦 How to Run

### 🔹 Run the API

```bash
cd ShiftsLoggerAPI
dotnet run
```

Make sure it runs on:
```
https://localhost:7290/api/Shift/
```

### 🔹 Run the Console UI

In a new terminal:

```bash
cd ShiftsLoggerUI
dotnet run
```

---

## 🔍 Example Shift JSON

```json
{
  "id": 1,
  "workerName": "John",
  "workerLastName": "Doe",
  "startTime": "2025-07-30T09:00:00",
  "endTime": "2025-07-30T17:00:00"
}
```

---

## ✅ Future Improvements

- Authentication/Authorization
- Better exception handling & validation
- Unit & Integration tests
- UI pagination support

---

## 👤 Author

Created by [nikosnick13](https://github.com/nikosnick13) — feel free to fork, suggest or contribute!

---

🎯 *This is a great project for learning fullstack development using .NET with clean separation of concerns and modern tools.*