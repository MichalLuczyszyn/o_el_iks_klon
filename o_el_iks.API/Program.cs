using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

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
List<AuctionData> auctions = new List<AuctionData>();

app.MapPost("/register", ([FromBody] RegistrationData data) =>
{
    var userExists = users.Any(u => u.email == data.email);
    if (userExists)
    {
        return Results.BadRequest("User with that e-mail already exists.");
    }
    if (string.IsNullOrWhiteSpace(data.email) || string.IsNullOrWhiteSpace(data.password))
    {
        return Results.BadRequest("Incorrect data.");
    }

    string hashedPassword = ShaHash(data.password);
    RegistrationData newUser = new RegistrationData { email = data.email, password = hashedPassword };
    users.Add(newUser);
    return Results.Ok("Registration successful.");
});

app.MapPost("/sign-in", ([FromBody] LoggingData data) =>
{
    var user = users.Any(u => u.email == data.email && u.password == ShaHash(data.password));
    if (user)
    {
        return Results.Ok("Sign in successful.");
    }
    return Results.BadRequest("Incorrect e-mail or password.");
});

app.MapGet("/view-users", () => users);

app.MapPost("/create-auction", ([FromBody] AuctionData data) =>
{
    AuctionData newAuction = new AuctionData
    {
        price = data.price, location = data.location, dateOfStart = data.dateOfStart,
        dateOfEnd = data.dateOfEnd, condition = data.condition
    };
    auctions.Add(newAuction);
    return Results.Ok("Auction created.");
});

app.MapGet("/view-auctions", () => auctions);

app.Run();


static string ShaHash(string password)
{
    using (SHA256 hashedPassword = SHA256.Create())
    {
        byte[] hashedBytes = hashedPassword.ComputeHash(Encoding.UTF8.GetBytes(password));
        StringBuilder passwordBuilder = new StringBuilder();
        for (int i = 0; i < hashedBytes.Length; i++)
        {
            passwordBuilder.Append(hashedBytes[i].ToString("x2"));
        }

        return passwordBuilder.ToString();
    }
}


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
    public string email { get; }
    public string password { get; }
}

public enum TypeOfItem
{
    New = 1,
    Old = 2,
    Used = 3
}

public class AuctionData
{
    public float price { get; set; }
    public string location { get; set; } 
    public DateOnly dateOfStart { get; set; }
    public DateOnly dateOfEnd { get; set; }
    public TypeOfItem condition { get; set; }
}