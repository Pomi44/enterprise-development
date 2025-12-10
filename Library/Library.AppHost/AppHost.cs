var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMySql("mysql-library")
    .AddDatabase("LibraryDb");

builder.AddProject<Projects.Library_Api_Host>("library-api-host")
    .WithReference(db, "DefaultConnection")
    .WaitFor(db);

builder.Build().Run();