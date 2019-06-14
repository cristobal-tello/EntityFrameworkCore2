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
k) Type 'script-migration' to take a look SQL that EF will use to generate database.
l) Usually, in development time 'update-database' is a valid option to generate the database. But in product mode, maybe the previous script generated in k) step could be useful. It depends.
m) Because we're on developement mode, we use 'update-database -verbose' to create db
n) Go to SQL Server exlorer and look for new created database. You will note, names had been pluralized automatically

* Create a webapp project

a) Add a xxx.Web ASP.Net core Web Application project, type Web Application MVC
b) On previous step we have connection string hardcode in the code. On .net core, we can use Dependecy Injection and set connection string on settings.json file.
c) On xxx.Data project, modify SamuariDbContext and add connection string into appsettings.json file(see changes on github)
d) In this case, we don't use nuGet to get tha packages. Instead, we edit xxx.Web .csproj file and add a package that include all for develop

<ItemGroup>
<PackageReference Include="Microsoft.AspNetCore.All" />
</ItemGroup>

e) Add reference to xxx.Data and xxx.Domain projects

* When you need to add a new migration
a) To avoid problems and be simplest, just comment the cctor in DbContext

 /* 
 public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
{

}
*/

and restore again 

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database=SamuraiAppData; Trusted_Connection=True");
}

b) Be sure you're on xxx.Data project on Packacer Manager Console, run

add-migration Add_ManyToMany_And_One_To_One_RelationShips -verbose

c) update-datbase -verbose
