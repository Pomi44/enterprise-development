var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMySql("mysql-library")
    .AddDatabase("LibraryDb");

var kafka = builder.AddKafka("library-kafka").WithKafkaUI();

builder.AddProject<Projects.Library_Api_Host>("library-api-host")
    .WithReference(db, "DefaultConnection")
    .WithReference(kafka)
    .WithEnvironment("Kafka__BookLoanTopicName", "book-loans")
    .WaitFor(db)
    .WaitFor(kafka);

builder.AddProject<Projects.Library_Generator_Kafka_Host>("library-generator-kafka-host")
    .WithReference(kafka)
    .WithEnvironment("Kafka__BookLoanTopicName", "book-loans")
    .WaitFor(kafka);

builder.Build().Run();