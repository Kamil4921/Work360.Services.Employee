using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Work360.Services.Employee.Application;
using Work360.Services.Employee.Application.Queries;
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
//builder.Services.AddExceptionHandler<ExceptionHandler>();

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
    app.UseExceptionHandler(_ => {});
}
//[FromQuery] GetEmployee query
app.MapGet("/employee", async (ISender mediator) => await mediator.Send(new GetEmployee(Guid.NewGuid()))).WithOpenApi().WithName("GetEmployee");
app.MapGet("/employees", async (ISender mediator, [FromBody]GetEmployees query) => await mediator.Send(query)).WithOpenApi().WithName("GetEmployees");

app.Run();
