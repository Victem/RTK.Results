using Microsoft.AspNetCore.Mvc;

using RTK.Results.Core;
using RTK.Results.SampleApi;
using RTK.Results.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProblemDetails();
builder.Services.AddSingleton<TestService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/handle-number/{number:int}", async (int number, TestService handler) =>
    handler.HandleNumger(number)
        .Match(
            r => Results.Ok(r),
            e => Results.Problem(e.ToProblem())
        )
    );

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
