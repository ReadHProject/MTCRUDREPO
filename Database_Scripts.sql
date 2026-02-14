-- =============================================
-- Payroll Management System - Database Scripts
-- =============================================

-- Create Database (if not exists)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'Payroll_DB')
BEGIN
    CREATE DATABASE Payroll_DB;
END
GO

USE Payroll_DB;
GO

-- =============================================
-- Create Tables
-- =============================================

-- Employee Master Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EmployeeMaster' AND xtype='U')
BEGIN
    CREATE TABLE EmployeeMaster (
        EMPID INT IDENTITY(1,1) PRIMARY KEY,
        EMPNAME NVARCHAR(100) NOT NULL,
        EMPDESIGNATION NVARCHAR(50) NOT NULL,
        MOBILE NVARCHAR(10) NOT NULL,
        CREATED_DATE DATETIME DEFAULT GETDATE()
    );
END
GO

-- Employee Details (Salary) Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EmployeeDetails' AND xtype='U')
BEGIN
    CREATE TABLE EmployeeDetails (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        EMPID INT NOT NULL,
        BASICSALARY DECIMAL(18,2) NOT NULL,
        HRA DECIMAL(18,2) NOT NULL,
        DA DECIMAL(18,2) NOT NULL,
        DEDUCTION DECIMAL(18,2) NOT NULL,
        CREATED_DATE DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (EMPID) REFERENCES EmployeeMaster(EMPID)
    );
END
GO

-- =============================================
-- Stored Procedures
-- =============================================

-- Insert Employee Master
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_INSERTEMPLOYEE')
    DROP PROCEDURE SP_INSERTEMPLOYEE;
GO

CREATE PROCEDURE SP_INSERTEMPLOYEE
    @EmpName NVARCHAR(100),
    @EmpDesgnation NVARCHAR(50),
    @EmpMobile NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Validate mobile number
        IF LEN(@EmpMobile) != 10 OR @EmpMobile NOT LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'
        BEGIN
            THROW 50001, 'Mobile number must be exactly 10 digits', 1;
        END
        
        -- Check if mobile already exists
        IF EXISTS (SELECT 1 FROM EmployeeMaster WHERE MOBILE = @EmpMobile)
        BEGIN
            THROW 50002, 'Mobile number already exists', 1;
        END
        
        -- Insert employee
        INSERT INTO EmployeeMaster (EMPNAME, EMPDESIGNATION, MOBILE)
        VALUES (@EmpName, @EmpDesgnation, @EmpMobile);
        
        SELECT SCOPE_IDENTITY() AS NewEmployeeID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- Select Employee Names
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_SELECTEMPLOYEE')
    DROP PROCEDURE SP_SELECTEMPLOYEE;
GO

CREATE PROCEDURE SP_SELECTEMPLOYEE
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT EMPID, EMPNAME 
    FROM EmployeeMaster 
    ORDER BY EMPNAME;
END
GO

-- Insert Employee Details (Salary)
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_INSERTEMPLOYEEDETAILS')
    DROP PROCEDURE SP_INSERTEMPLOYEEDETAILS;
GO

CREATE PROCEDURE SP_INSERTEMPLOYEEDETAILS
    @EmpId INT,
    @BasicSalary DECIMAL(18,2),
    @HRA DECIMAL(18,2),
    @DA DECIMAL(18,2),
    @Deduction DECIMAL(18,2)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        -- Validate employee exists
        IF NOT EXISTS (SELECT 1 FROM EmployeeMaster WHERE EMPID = @EmpId)
        BEGIN
            THROW 50003, 'Employee not found', 1;
        END
        
        -- Check if salary details already exist for this employee
        IF EXISTS (SELECT 1 FROM EmployeeDetails WHERE EMPID = @EmpId)
        BEGIN
            -- Update existing record
            UPDATE EmployeeDetails 
            SET BASICSALARY = @BasicSalary,
                HRA = @HRA,
                DA = @DA,
                DEDUCTION = @Deduction,
                CREATED_DATE = GETDATE()
            WHERE EMPID = @EmpId;
        END
        ELSE
        BEGIN
            -- Insert new record
            INSERT INTO EmployeeDetails (EMPID, BASICSALARY, HRA, DA, DEDUCTION)
            VALUES (@EmpId, @BasicSalary, @HRA, @DA, @Deduction);
        END
        
        SELECT 1 AS Success;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO

-- Get Employee Salary Report
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'SP_GETEMPLOYEESALARYREPORT')
    DROP PROCEDURE SP_GETEMPLOYEESALARYREPORT;
GO

CREATE PROCEDURE SP_GETEMPLOYEESALARYREPORT
    @EmpId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        em.EMPID,
        em.EMPNAME,
        em.EMPDESIGNATION,
        em.MOBILE,
        ed.BASICSALARY,
        ed.HRA,
        ed.DA,
        ed.DEDUCTION
    FROM EmployeeMaster em
    INNER JOIN EmployeeDetails ed ON em.EMPID = ed.EMPID
    WHERE (@EmpId IS NULL OR em.EMPID = @EmpId)
    ORDER BY em.EMPNAME;
END
GO

-- =============================================
-- Sample Data (Optional)
-- =============================================

-- Insert sample employees if table is empty
IF NOT EXISTS (SELECT 1 FROM EmployeeMaster)
BEGIN
    INSERT INTO EmployeeMaster (EMPNAME, EMPDESIGNATION, MOBILE) VALUES
    ('John Doe', 'Software Engineer', '9876543210'),
    ('Jane Smith', 'Senior Developer', '9876543211'),
    ('Mike Johnson', 'Project Manager', '9876543212'),
    ('Sarah Wilson', 'Business Analyst', '9876543213'),
    ('David Brown', 'QA Engineer', '9876543214');
    
    -- Insert sample salary details
    INSERT INTO EmployeeDetails (EMPID, BASICSALARY, HRA, DA, DEDUCTION) VALUES
    (1, 50000.00, 5000.00, 3000.00, 2000.00),
    (2, 75000.00, 7500.00, 4500.00, 3000.00),
    (3, 90000.00, 9000.00, 5400.00, 4000.00),
    (4, 60000.00, 6000.00, 3600.00, 2500.00),
    (5, 45000.00, 4500.00, 2700.00, 1800.00);
END
GO

PRINT 'Database setup completed successfully!';