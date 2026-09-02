using eScout_v2.Application.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence<DbContext>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();