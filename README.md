# 🧑‍💼 Human Resources Management System (HRMS)

> A web-based HR Management System developed by **Pioneers Solutions** to streamline employee management, attendance tracking, and payroll reporting.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [System Modules](#system-modules)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Non-Functional Requirements](#non-functional-requirements)
- [Project Team](#project-team)

---

## 📌 Overview

HRMS is designed to simplify and automate the daily operations of an HR department. The system enables HR managers to manage employee records, configure overtime/deduction policies, track attendance, and generate detailed payroll reports — all from a single, unified interface.

---

## ✨ Features

- 🔐 **Secure Authentication** — Role-based login for HR managers and system users
- 👥 **User Group Management** — Define user groups with granular screen-level permissions
- 🧾 **Employee Registration** — Store personal and work-related data for every employee
- ⚙️ **General Settings** — Configure overtime/deduction rates and weekly holidays
- 📅 **Official Holidays** — Input and manage annual public holidays
- 🕐 **Attendance Tracking** — View, add, edit, and import attendance records via Excel
- 💰 **Payroll Reports** — Monthly salary breakdowns with attendance, overtime, deductions, and net pay
- 🖨️ **Pay Slip Printing** — Print individual pay slips per employee per month

---

## 🛠 Tech Stack

> *(Update this section based on your actual implementation)*

| Layer      | Technology              |
|------------|-------------------------|
| Frontend   | Angular                 |
| Backend    | ASP.NET Core / .NET     |
| Database   | SQL Server              |
| Auth       | JWT (JSON Web Tokens)   |
| Deployment | Azure / IIS             |

---

## 📦 System Modules

### 1. 🔑 Authentication
- Login with username/email and password
- Validation for empty fields and invalid credentials

### 2. 👥 User Groups & Permissions
- Create user groups with specific screen access (Add / Edit / Delete / View)
- Assign permissions per module: Employees, Settings, Attendance, Payroll

### 3. 👤 User Management
- Add, edit, and delete system users
- Assign users to existing permission groups

### 4. 🧾 Employee Registration
- **Personal Data:** Name, address, phone, date of birth, gender, national ID, nationality
- **Work Data:** Contract date, base salary, expected check-in/check-out times
- Full validation: phone format, age (≥ 20 years), national ID (14 digits), contract date (≥ 2008)

### 5. ⚙️ General Settings
- Set overtime rate (per hour equivalent)
- Set deduction rate (per late hour)
- Configure weekly days off (e.g., Friday only, or Friday & Saturday)

### 6. 📅 Official Holidays
- Add public holiday name and date
- Holidays are excluded from absence calculation and attendance reports

### 7. 🕐 Attendance Management
- View attendance records filtered by employee name, department, or date range
- Add attendance records manually or import from Excel
- Edit and delete records with confirmation dialogs

### 8. 💰 Payroll Report
- Filter by employee name, month, and year
- Report includes:
  - Base salary, department
  - Days present / absent
  - Overtime hours & total overtime pay
  - Deduction hours & total deduction
  - **Net salary**
- Print pay slips per employee

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (v8)
- [Node.js](https://nodejs.org/) & Angular CLI
- SQL Server
- Git

### Installation

```bash
# Clone the repository
git clone https://github.com/your-username/hrms.git
cd hrms

# Backend setup
cd HRMSBackend
dotnet restore
dotnet ef database update
dotnet run

# Frontend setup
cd ../HRMSFrontend
npm install
ng serve
```

> The app will be available at `http://localhost:4200`

### Default Credentials

```
Username: admin
Password: ********
```

---

## 💡 Usage

1. Log in as HR Manager
2. Configure **General Settings** (overtime/deduction rates, weekly holidays)
3. Add **Official Holidays** for the year
4. Register **Employees** with personal and work data
5. Import or manually enter **Attendance Records**
6. View and print **Payroll Reports** at the end of each month

---

## 🔒 Non-Functional Requirements

| Requirement        | Details                                                        |
|--------------------|----------------------------------------------------------------|
| **Data Accuracy**  | All inputs are validated before persistence                    |
| **Performance**    | Fast data loading for attendance and payroll queries           |
| **Security**       | Employee data is encrypted to protect company information      |
| **Scalability**    | System supports more employees than current headcount          |
| **Compatibility**  | Works across Chrome, Firefox, Microsoft Edge, and more         |

---

## 👨‍💻 Project Team

| Role               | Name              | Contact                            |
|--------------------|-------------------|------------------------------------|
| CEO                | Ashraf Nouh       | ashraf@pioneers-solutions.com      |
| Project Manager    | Mohamed Alaa      | m.alaa@pioneers-solutions.com      |
| Business Analyst   | Lamiaa Mahmoud    | —                                  |

---

## 📄 License

This project is proprietary software developed by **Pioneers Solutions**.  
All rights reserved © 2021 Pioneers Solutions.
