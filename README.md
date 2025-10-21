<img width="1536" height="1024" alt="image" src="https://github.com/user-attachments/assets/94bc15e5-a1dd-4117-8c1f-404b0c64aad4" />
# 🚗 CarRentalApp MVC3 — ASP.NET MVC + SQL Server

A simple **Car Rental Web Application** built with **ASP.NET MVC3**, **Entity Framework**, and **SQL Server**.  
This project implements all the required features from the *Take Home Test*.

---

## 📋 Fitur Utama

### 👥 Autentikasi
- Register dan Login (menggunakan session)
- Dua role:
  - **Admin** → mengelola data mobil & laporan
  - **User** → menyewa & mengembalikan mobil

### 🚗 Manajemen Mobil (Admin only)
- CRUD Mobil (Tambah, Edit, Hapus, Lihat)
- Status mobil: `Tersedia` / `Disewa`

### 🧾 Penyewaan Mobil (User)
- Pilih mobil → pilih tanggal sewa dan kembali  
- Sistem otomatis menghitung total harga:
  ```
  Total = (Jumlah Hari × Harga Sewa per Hari)
  ```
- Setelah disewa → status mobil berubah ke `Disewa`

### 🔁 Pengembalian Mobil
- User dapat mengembalikan mobil yang disewa  
- Setelah dikembalikan → status kembali menjadi `Tersedia`

### 📊 Laporan Penyewaan (Admin only)
- Admin dapat mengekspor laporan ke **CSV**, dan dapat menambahkan fitur PDF/Excel (via NuGet `iTextSharp` dan `EPPlus`)

### 🔒 Keamanan
- Entity Framework sudah melindungi dari **SQL Injection**
- Razor view otomatis melakukan **HTML Encoding (anti-XSS)**

---

## 🧰 Teknologi

| Komponen | Versi / Keterangan |
|-----------|--------------------|
| ASP.NET MVC | v3 (Framework 4.0 disarankan) |
| Entity Framework | 6+ |
| Database | SQL Server |
| Laporan | CSV, iTextSharp (PDF), EPPlus (Excel) |
| Bahasa | C# |

---

## 🗄️ Database: `CarRentalDb`

Gunakan file SQL berikut untuk membuat database:
📁 **CarRentalDb.sql**

Atau salin SQL di bawah ini dan jalankan di **SQL Server Management Studio (SSMS)**:

```sql
CREATE DATABASE CarRentalDb;
GO
USE CarRentalDb;
GO

CREATE TABLE ApplicationUser (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE Car (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    PricePerDay DECIMAL(18,2) NOT NULL,
    [Status] NVARCHAR(20) NOT NULL DEFAULT('Tersedia')
);
GO

CREATE TABLE Rental (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CarId INT NOT NULL,
    UserId INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Rental_Car FOREIGN KEY (CarId) REFERENCES Car(Id),
    CONSTRAINT FK_Rental_User FOREIGN KEY (UserId) REFERENCES ApplicationUser(Id)
);
GO

INSERT INTO ApplicationUser (Username, [Password], [Role])
VALUES ('admin', 'admin123', 'Admin'),
       ('user1', 'user123', 'User');

INSERT INTO Car ([Name], PricePerDay, [Status])
VALUES 
('Toyota Avanza', 350000, 'Tersedia'),
('Honda Jazz', 400000, 'Tersedia'),
('Mitsubishi Xpander', 500000, 'Tersedia');
GO
```

---

## 🔗 Connection String (Web.config)

Pastikan koneksi database kamu diatur seperti ini:

```xml
<connectionStrings>
  <add name="DefaultConnection"
       connectionString="Data Source=localhost;Initial Catalog=CarRentalDb;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Jika menggunakan SQL Server Express:
```xml
connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=CarRentalDb;Integrated Security=True"
```

---

## ⚙️ Langkah Instalasi

### 1. Buat Proyek MVC
- Buka **Visual Studio**
- Pilih: `New Project → ASP.NET Web Application (.NET Framework 4.0)`
- Template: `MVC`

### 2. Salin Folder dari ZIP
Dari file ZIP `CarRentalApp_MVC3_approx.zip`, salin:
```
Controllers/
Models/
Views/
Web.config
```

### 3. Instal Dependensi
Jalankan di **Package Manager Console**:

```powershell
Install-Package EntityFramework
Install-Package iTextSharp
Install-Package EPPlus
```

### 4. Buat Database
Jalankan `CarRentalDb.sql` di SQL Server Management Studio (SSMS).

### 5. Jalankan Aplikasi
Tekan **F5** di Visual Studio.  
Login menggunakan:
```
Username: admin
Password: admin123
```

---

## 📁 Struktur Proyek

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
├── README.md
└── CarRentalDb.sql
```

---

## 🧠 Catatan

- ASP.NET MVC3 memerlukan **.NET Framework 4.0**, bukan 3.5.
- Jika ingin database dibuat otomatis, aktifkan **Code-First Migrations**:
  ```powershell
  Enable-Migrations
  Add-Migration Initial
  Update-Database
  ```
- Ganti password plaintext menjadi hash (gunakan SHA256 atau BCrypt).
- Untuk tampilan lebih baik, tambahkan **Bootstrap 5** atau template CSS.

---

## 🧩 Pengembangan Lanjutan

- Tambahkan filter & pagination di daftar mobil
- Dashboard admin (laporan & statistik penyewaan)
- Validasi tanggal sewa (tidak boleh bentrok)
- Kirim notifikasi email untuk penyewaan/pengembalian
- Upgrade ke ASP.NET Core MVC 8.0 (cross-platform)

---

## 👨‍💻 Author

Dibuat sebagai solusi **Take Home Test - Aplikasi Sewa Mobil (ASP.NET MVC)**  
Cocok dijalankan di Visual Studio 2019 / 2022 dengan SQL Server lokal.
