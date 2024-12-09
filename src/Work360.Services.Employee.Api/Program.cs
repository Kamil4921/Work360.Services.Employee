using MediatR;
using Microsoft.OpenApi.Models;
using Work360.Services.Employee.Application;
using Work360.Services.Employee.Application.Queries;
using Work360.Services.Employee.Application.Commands;
using Work360.Services.Employee.Core.Entities;
using Work360.Services.Employee.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
});
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
    app.UseExceptionHandler(_ => {});
}

app.MapGet("/employee", async (ISender mediator, Guid id) => await mediator.Send(new GetEmployee(id))).WithOpenApi()
    .WithName("GetEmployee");

app.MapGet("/employees", async (ISender mediator) => 
        await mediator.Send(new GetEmployees()))
                        .WithOpenApi()
                        .WithName("GetEmployees");

app.MapPatch("/employee/changeState", async (ISender mediator, Guid id, string state) => 
        await mediator.Send(new ChangeEmployeeState(id, state)))
                        .WithOpenApi()
                        .WithName("ChangeEmployeeState");

app.MapPatch("/employee/changeContract", async (ISender mediator, Guid id, string contract) =>
        await mediator.Send(new ChangeEmployeeContract(id, contract)))
                        .WithOpenApi()
                        .WithName("ChangeEmployeeContract");

app.MapPost("/employee/add", async (ISender mediator, long pesel, string email, int salary, Contract typeOfContract,
            State state, string fullName, string position, DateTime hiredAt, string address) =>
        await mediator.Send(new EmployeeRegistration(pesel, email, salary, typeOfContract, state, fullName, position, hiredAt, address)))
                        .WithOpenApi()
                        .WithName("AddEmployee");

app.MapDelete("/employee/delete", async (ISender mediator, Guid id) => 
        await mediator.Send(new DeleteEmployee(id)))
                        .WithOpenApi()
                        .WithName("DeleteEmployee");

app.MapPut("/employee/update", async (ISender mediator, Guid id, long pesel, string? email, int salary, Contract typeOfContract,
            State state, string? fullName, string? position, DateTime hiredAt, string? address) =>
        await mediator.Send(new UpdateEmployee(id, pesel, email, salary, typeOfContract, state, fullName, position, hiredAt, address)))
                        .WithOpenApi()
                        .WithName("UpdateEmployee");

app.Run();
