using Library.Application;
using Library.Application.Contracts;
using Library.Application.Contracts.BookLoans;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.EditionTypes;
using Library.Application.Contracts.Publishers;
using Library.Application.Contracts.Readers;
using Library.Application.Services;
using Library.Domain;
using Library.Domain.Models;
using Library.Infrastructure.EfCore;
using Library.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new LibraryMapper());
});

builder.AddServiceDefaults();

builder.Services.AddTransient<IRepository<BookLoan, int>, BookLoanRepository>();
builder.Services.AddTransient<IRepository<Book, int>, BookRepository>();
builder.Services.AddTransient<IRepository<EditionType, int>, EditionTypeRepository>();
builder.Services.AddTransient<IRepository<Publisher, int>, PublisherRepository>();
builder.Services.AddTransient<IRepository<Reader, int>, ReaderRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IApplicationService<BookLoanDto, BookLoanCreateUpdateDto, int>, BookLoanService>();
builder.Services.AddScoped<IApplicationService<EditionTypeDto, EditionTypeCreateUpdateDto, int>, EditionTypeService>();
builder.Services.AddScoped<IApplicationService<PublisherDto, PublisherCreateUpdateDto, int>, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReaderService, ReaderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseInlineDefinitionsForEnums();

    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("Library"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }
});

builder.AddMySqlDbContext<LibraryDbContext>(connectionName: "DefaultConnection");

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
db.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
