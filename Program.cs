using FluentValidation;
using LibraryWebAPI.Data;
using LibraryWebAPI.Repository.Implementation;
using LibraryWebAPI.Repository.Interfaces;
using LibraryWebAPI.Services.Implementation;
using LibraryWebAPI.Services.Interfaces;
using LibraryWebAPI.Validation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IBookStackRepository, BookStackRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookReaderRepository, BookReaderRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<BookStackValidator>(ServiceLifetime.Scoped);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDataContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("LibraryDataBase")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
