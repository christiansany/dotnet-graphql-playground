using Blog.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Blog") ?? "Data Source=Blog.db";

builder.Services.AddSqlite<BlogPageDb>(connectionString);
builder.Services
    .AddGraphQLServer()
    .AddGlobalObjectIdentification()
    .RegisterDbContext<BlogPageDb>()
    .AddQueryType<Query>();

var app = builder.Build();

app.MapGraphQL();

app.Run();
