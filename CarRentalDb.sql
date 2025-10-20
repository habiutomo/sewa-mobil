-- =========================================
-- DATABASE: CarRentalDb
-- =========================================
CREATE DATABASE CarRentalDb;
GO

USE CarRentalDb;
GO

-- =========================================
-- TABLE: ApplicationUser
-- =========================================
CREATE TABLE ApplicationUser (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    [Role] NVARCHAR(20) NOT NULL -- 'Admin' or 'User'
);
GO

-- =========================================
-- TABLE: Car
-- =========================================
CREATE TABLE Car (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL,
    PricePerDay DECIMAL(18,2) NOT NULL,
    [Status] NVARCHAR(20) NOT NULL DEFAULT('Tersedia') -- or 'Disewa'
);
GO

-- =========================================
-- TABLE: Rental
-- =========================================
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

-- =========================================
-- SAMPLE DATA
-- =========================================
INSERT INTO ApplicationUser (Username, [Password], [Role])
VALUES ('admin', 'admin123', 'Admin'),
       ('user1', 'user123', 'User');
GO

INSERT INTO Car ([Name], PricePerDay, [Status])
VALUES 
('Toyota Avanza', 350000, 'Tersedia'),
('Honda Jazz', 400000, 'Tersedia'),
('Mitsubishi Xpander', 500000, 'Tersedia');
GO

PRINT 'Database CarRentalDb created successfully with sample data!';
