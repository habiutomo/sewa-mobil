# CarRentalApp MVC3 (Take Home Test)

A simple **Car Rental Web Application** built with **ASP.NET MVC pattern** using **Entity Framework** and **SQL Server**.  
This project follows the *Take Home Test* specification.

---

## 📋 Features

### 👥 Authentication
- Register & Login (Forms-based session)
- Roles: `Admin`, `User`

### 🚗 Car Management (Admin only)
- CRUD: Add, Edit, Delete, List Cars
- Car Status: `Tersedia` / `Disewa`

### 🧾 Rentals (User only)
- Select Car → Choose rental & return dates
- System calculates total price:  
  **Total = (Days × Price per day)**
- Updates car status to `Disewa`

### 🔁 Return Car
- User can return rented cars → car becomes `Tersedia`

### 📊 Reports (Admin only)
- Export rental data to **CSV**
- PDF/Excel export possible (add `iTextSharp` / `EPPlus` NuGet packages)

### 🔒 Security
- Entity Framework ORM prevents SQL Injection
- Razor auto-encodes HTML → safe from XSS

---

## 🏗️ Setup Guide

1. **Create new ASP.NET MVC Project**
   - Visual Studio → *New Project* → ASP.NET Web Application
   - Choose `MVC`
   - Target framework: `.NET Framework 4.0` or higher (recommended)

2. **Copy Source Files**
   - Copy folders from this ZIP:  
     `Controllers`, `Models`, `Views`, and `Web.config`

3. **Install Dependencies**
   In Visual Studio Package Manager Console:
   ```powershell
   Install-Package EntityFramework
   Install-Package iTextSharp
   Install-Package EPPlus
   ```

4. **Configure Database**
   - Edit `Web.config`:
     ```xml
     <connectionStrings>
       <add name="DefaultConnection" 
            connectionString="Data Source=.;Initial Catalog=CarRentalDb;Integrated Security=True"
            providerName="System.Data.SqlClient" />
     </connectionStrings>
     ```
   - Run EF migrations or create tables manually.

5. **Run Project**
   - Press **F5** to start the app.
   - Register a new user → change `Role` field in DB to `Admin` for admin access.

---

## 📁 Project Structure

```
CarRentalApp/
├── Controllers/
│   ├── AccountController.cs
│   ├── CarsController.cs
│   ├── RentalsController.cs
│   ├── ReportsController.cs
│   └── HomeController.cs
│
├── Models/
│   ├── ApplicationUser.cs
│   ├── Car.cs
│   ├── Rental.cs
│   └── ApplicationDbContext.cs
│
├── Views/
│   ├── Account/
│   ├── Cars/
│   ├── Rentals/
│   ├── Home/
│   └── Shared/
│
├── Web.config
└── README.md
```

---


## 📦 Future Improvements
- Real authentication with ASP.NET Identity
- UI styling (Bootstrap)
- Pagination & filtering
- Rental history by date
- Dashboard for admins

---

Created for **Take Home Test: Car Rental Application**
