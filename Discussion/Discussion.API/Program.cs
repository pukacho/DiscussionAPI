using Discussion.DB;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Discussion.Common.Interfaces;
using Discussion.Core.Pipeline;
using Discussion.DB.Repositories;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Register controllers and Swagger for API documentation.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure EF Core – using an in-memory database for simplicity.
builder.Services.AddDbContext<DiscussionDbContext>(options =>
    options.UseInMemoryDatabase("DiscussionDb"));

// Register MediatR using the new configuration approach for v11+.
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(Assembly.Load("Discussion.Core"));
});

// Register repository dependency.
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Register FluentValidation validators from the Discussion.Core assembly.
builder.Services.AddValidatorsFromAssembly(Assembly.Load("Discussion.Core"));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.Run();