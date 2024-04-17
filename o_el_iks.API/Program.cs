using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using o_el_iks.API;
using o_el_iks.API.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IAuctionsProvider, AuctionsProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


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


app.MapPost("/register", ([FromBody] RegistrationData data, IUserProvider userProvider, IAuctionsProvider auctionsProvider) =>
{
    {
        try
        {
            userProvider.Register(data, auctionsProvider);
            return Results.Ok("Registration successful");
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
});

app.MapPost("/sign-in",
    (SignInData data, IUserProvider userProvider, ITokenProvider tokenProvider, HttpContext httpContext) =>
    {
        try
        {
            userProvider.SignIn(data, tokenProvider, httpContext);
            return Results.Ok("Sing in successful.");
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    });

app.MapGet("/view-users", (IUserProvider userProvider) => userProvider.GetUsers());

app.MapPost("/create-auction", ([FromBody] AuctionData data, IAuctionsProvider auctionProvider) =>
{
    try
    {
        auctionProvider.AddAuction(data);
        return Results.Ok("Auction created.");
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapGet("/view-auctions", (IAuctionsProvider auctionProvider) => auctionProvider.GetAuctions());
app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

