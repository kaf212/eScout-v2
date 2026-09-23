using eScout_v2.Application.Extensions;
using eScout_v2.Application.Infrastructure.Extensions;
using eScout_v2.Application.Persistence.Extensions;
using eScout_v2.Endpoints.Extensions;
using eScout_v2.Middleware.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddInfrastructureExtensions()
    .AddApplicationExtensions();

builder.Services.AddPersistence<DbContext>();

var app = builder.Build();

app.UseGlobalExceptionHandler();

app.RegisterAllEndpoints();

app.Run();