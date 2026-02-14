# Payroll Management System

A comprehensive ASP.NET MVC application for managing employee information and salary details with reporting capabilities.

## Features

### 1. Employee Master Management
- Add new employees with basic information
- Employee ID auto-generation
- Form validation and error handling
- Real-time client-side validation

### 2. Employee Salary Details
- Add salary components (Basic, HRA, DA, Deduction)
- Real-time salary calculation preview
- Dropdown selection of existing employees
- Input validation for monetary values

### 3. Salary Report System ⭐ **NEW**
- **Single Employee Report**: View salary details for a specific employee
- **All Employees Report**: Comprehensive salary report for all employees
- **Interactive Filters**: Dropdown selection with "Show All" option
- **Summary Dashboard**: Total employees, gross salary, deductions, and net salary
- **Export Options**: 
  - Download PDF report
  - Print HTML report
- **Professional Formatting**: Clean, responsive design with proper styling

## Database Setup

1. **Create Database**: Run the SQL script `Database_Scripts.sql` to set up the database
2. **Connection String**: Update the connection string in `Web.config` if needed:
   ```xml
   <add name="DBCONSTR" 
        connectionString="Data Source=.\SQLEXPRESS;Initial Catalog=Payroll_DB;Integrated Security=True;" 
        providerName="System.Data.SqlClient" />
   ```

## Database Schema

### Tables
- **EmployeeMaster**: Stores employee basic information
- **EmployeeDetails**: Stores salary components for each employee

### Stored Procedures
- `SP_INSERTEMPLOYEE`: Insert new employee
- `SP_SELECTEMPLOYEE`: Get employee list for dropdowns
- `SP_INSERTEMPLOYEEDETAILS`: Insert/Update employee salary details
- `SP_GETEMPLOYEESALARYREPORT`: Get salary report data

## How to Use

### Adding Employees
1. Navigate to Employee Master
2. Fill in employee details (Name, Designation, Mobile)
3. Employee ID is auto-generated
4. Click "Save Employee"

### Adding Salary Details
1. Navigate to Employee Details (Salary)
2. Select an employee from dropdown
3. Enter salary components:
   - Basic Salary
   - HRA (House Rent Allowance)
   - DA (Dearness Allowance)
   - Deduction
4. View real-time calculation preview
5. Click "Save Salary Details"

### Viewing Salary Reports
1. Navigate to Salary Report
2. **For Single Employee**:
   - Select employee from dropdown
   - Click "Show Report"
3. **For All Employees**:
   - Check "Show All Employees"
   - Click "Show Report"
4. **Export Options**:
   - Click "Download PDF" for PDF export
   - Click "Print Report" for HTML printing

## Report Features

### Summary Dashboard
- Total number of employees
- Total gross salary across all employees
- Total deductions
- Total net salary

### Detailed Table
- Employee ID, Name, Designation, Mobile
- Salary components: Basic, HRA, DA, Deduction
- Calculated values: Gross Salary, Net Salary
- Summary totals row

### Export Capabilities
- **PDF Export**: Professional PDF format with company header
- **HTML Print**: Browser-based printing with print-optimized styling
- **Responsive Design**: Works on desktop and mobile devices

## Technical Details

### Architecture
- **Framework**: ASP.NET MVC 5
- **Database**: SQL Server with stored procedures
- **Frontend**: Bootstrap 4, jQuery, Font Awesome
- **Validation**: Client-side and server-side validation

### Key Components
- **Controllers**: EmployeeController with CRUD operations
- **Models**: EmployeeMasterModel, EmployeeDetailsModel, SalaryReportModel
- **DAL**: RegistrationDAL with database operations
- **Views**: Responsive Razor views with modern UI

### Calculations
- **Gross Salary** = Basic Salary + HRA + DA
- **Net Salary** = Gross Salary - Deduction

## Navigation

The application provides seamless navigation between modules:
- Employee Master ↔ Salary Details ↔ Salary Report
- Breadcrumb navigation
- Quick action buttons in headers

## Sample Data

The database script includes sample data for testing:
- 5 sample employees with different designations
- Complete salary details for all sample employees
- Ready-to-use data for report testing

## Browser Compatibility

- Chrome (recommended)
- Firefox
- Edge
- Safari
- Internet Explorer 11+

## Future Enhancements

- Employee photo upload
- Salary history tracking
- Advanced filtering (by designation, salary range)
- Excel export functionality
- Email report distribution
- Dashboard with charts and analytics

---

**Note**: This system maintains all existing functionality while adding comprehensive salary reporting capabilities. No existing code logic has been modified, ensuring backward compatibility.