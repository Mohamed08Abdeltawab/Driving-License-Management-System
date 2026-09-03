# Driving License Management System

A Windows Forms application for managing people, drivers, driving-license applications, tests, and issued licenses for a Driving and Vehicle License Department (DVLD).

## Features

- **Authentication and user administration:** login, inactive-account checks, remember-me credentials, user management, current-user information, password changes, and sign out.
- **People management:** add, edit, find, list, and delete people, including contact information, nationality, date of birth, gender, address, and photo.
- **Driver management:** list drivers and view their personal and license information.
- **Application management:** manage application types and local driving-license applications, including editing, cancellation, deletion, and status tracking.
- **Driving tests:** configure test types and schedule, edit, and take vision, written, and street tests. Test appointments track attempts, attendance, and pass/fail results.
- **License services:** issue a first local license, renew an expired license, replace a lost or damaged license, and view local license information and history.
- **International licenses:** apply for and manage international driving licenses for eligible drivers.
- **Detained licenses:** detain licenses with a fine, list detained licenses, and process their release.

## Architecture

The solution is divided into three projects:

```text
Test_DVLD (WinForms UI)
	↓
Test_DVLD_Buisness (business/domain classes)
	↓
Test_DVLD_DataAccess (ADO.NET and SQL Server access)
```

The main solution is `Test_DVLD/Test_DVLD.sln`. The UI starts with `frmLogin` and opens `frmMain` after successful authentication. Business entities include people, users, drivers, applications, licenses, tests, appointments, countries, and license classes.

## Technology

- C# Windows Forms
- .NET Framework 4.8
- SQL Server
- ADO.NET (`System.Data.SqlClient`)
- Visual Studio 2022 solution format

## Requirements

- Windows
- Visual Studio with .NET desktop development tools
- .NET Framework 4.8 Developer Pack
- A running SQL Server instance
- A database named `Test_DVLD` containing the tables, relationships, views, and initial data expected by the data-access classes
- An active user record for signing in

## Database configuration

The connection string is currently defined in `Test_DVLD_DataAccess/clsDataAccessSettings.cs` rather than in `App.config`:

```text
Server=.;Database=Test_DVLD;User Id=sa;Password=123456
```

Update this value for your SQL Server environment before running the application. The repository does not currently include a database creation script, backup, migration, or seed-data file, so the required database must be provided separately.

For security, do not use a shared `sa` account or commit real credentials in production. Prefer a restricted SQL login and external configuration.

## Build and run

1. Open `Test_DVLD/Test_DVLD.sln` in Visual Studio.
2. Configure SQL Server and create or restore the required `Test_DVLD` database.
3. Update the connection string in `clsDataAccessSettings.cs` if necessary.
4. Set `Test_DVLD` as the startup project.
5. Build the solution in Debug or Release mode.
6. Run the application and sign in with an active database user.

The solution has Debug and Release configurations and contains no separate automated test project; the `Test_DVLD/Tests` folder contains the application's driving-test screens and controls.

## Project layout

```text
Test_DVLD/                 Windows Forms application
Test_DVLD_Buisness/       Business and domain classes
Test_DVLD_DataAccess/     SQL Server data-access classes
LICENSE.txt               Project license
```

## Notes

- The project and namespaces retain the original `Buisness` spelling in several names.
- Remember-me credentials are stored locally by the application; review this behavior before deploying in a shared or production environment.
