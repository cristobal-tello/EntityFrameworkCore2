* EntityFrameworkCore2 project

Code to understand Pluralsight course Entity Framework Core 2: Getting Started

* NuGet EF need it packages 

a) On xxx.Data project, add a microsoft.entityframeworkcore package
b) If you use SQL Server as database engine, add Microsoft.EntityFrameworkCore.SqlServer package
c) In order to made migrations, next package needs to be installed as well: Microsoft.EntityFrameworkCore.Tools
d) In Package Manager Console, you can type 'get-help entityframeworkcore' to see power-shell available functions
e) Then to create a migration, is mandatory do it from an executable project. Also, this project needs to be set a Initial project.
f) Best solution is set a xxx.SomeUI (that's an executable project) as initial project.
g) Add a Reference to xxx.Domain and xxx.Data projects on xxx.SomeUI
h) Add a Microsoft.EntityFrameworkCore.Design nuget package on xxx.SomeUI
j) From Packager Manager, select again xxx.Data project and use 'add-migration initial' to create first migration.