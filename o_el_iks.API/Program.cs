using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

List<RegistrationData> users = new List<RegistrationData>();

app.MapPost("/register", ([FromBody] RegistrationData data) =>
{
    if (string.IsNullOrWhiteSpace(data.email) || string.IsNullOrWhiteSpace(data.password))
    {
        return Results.BadRequest("Incorrect data.");
    }
    RegistrationData newUser = new RegistrationData { email = data.email, password = data.password };
    users.Add(newUser);
    return Results.Ok("Registration successful.");
});

app.MapPost("/sign-in", ([FromBody] LoggingData data) =>
{
    var user = users.Any(u => u.email == data.email && u.password == data.password);
    if (user)
    {
        return Results.Ok("Sign in successful.");
    }
    return Results.BadRequest("Incorrect e-mail or password.");
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class RegistrationData
{
    public string email { get; set; }
    public string password { get; set; }
}

public class LoggingData
{
    public string email { get; set; }
    public string password { get; set; }
}