var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Library_Api_Host>("library-api-host");

builder.Build().Run();
