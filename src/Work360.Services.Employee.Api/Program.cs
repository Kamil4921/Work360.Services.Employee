using MediatR;
using Work360.Services.Employee.Application;
using Work360.Services.Employee.Application.Queries;
using Work360.Services.Employee.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler();
}

app.MapGet("/employee", async (ISender mediator, GetEmployee query) => await mediator.Send(query));
app.MapGet("/employees", async (ISender mediator, GetEmployees query) => await mediator.Send(query));

app.UseHttpsRedirection();
app.Run();
