
Payroll System
- Framework: .NET 5.0

Packages Required:
- Microsoft MVVM Toolkit
- Ardalis Guard Clauses
- AutoMapper
- Microsoft Entity Framework Core

Instructions:
> Setting up the connection string
    - Expand "PayrollSystem.UI"
    - Expand "App.xaml"
    - open "App.xaml.cs"
    - Find "services.AddDependency() method call" (at line 70)
    - Replace the Data Source on the servername of your database
    - optional: if initial catalog is different on database name, change it