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

app.UseHttpsRedirection();

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

app.MapGet("/employee/changeState", async (ISender mediator, Guid id, string state) => 
        await mediator.Send(new ChangeEmployeeState(id, state)))
                        .WithOpenApi()
                        .WithName("ChangeEmployeeState");

app.MapGet("/employee/changeContract", async (ISender mediator, Guid id, string contract) =>
        await mediator.Send(new ChangeEmployeeContract(id, contract)))
                        .WithOpenApi()
                        .WithName("ChangeEmployeeContract");

app.MapGet("/employee/add", async (ISender mediator, Employee employee) => 
        await mediator.Send(new EmployeeRegistration(employee.Pesel,
        employee.Email, employee.Salary, employee.TypeOfContract, employee.State, employee.FullName, employee.Position,
        employee.HiredAt, employee.Address)))
                        .WithOpenApi()
                        .WithName("AddEmployee");

app.MapGet("/employee/delete", async (ISender mediator, Guid id) => 
        await mediator.Send(new DeleteEmployee(id)))
                        .WithOpenApi()
                        .WithName("DeleteEmployee");

app.MapGet("/employee/update", async (ISender mediator, Employee employee) => 
        await mediator.Send(new UpdateEmployee(employee)))
                        .WithOpenApi()
                        .WithName("UpdateEmployee");

app.Run();
