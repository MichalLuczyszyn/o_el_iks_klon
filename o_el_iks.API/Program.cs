using Microsoft.AspNetCore.Mvc;
using o_el_iks.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IAuctionsProvider, AuctionProvider>();

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




app.MapPost("/register", ([FromBody] Entites.RegistrationData data, UserService userService) => userService.Register(data));

app.MapPost("/sign-in", (Entites.SignInData data, UserService userService, ITokenProvider tokenProvider, HttpContext httpContext) => userService.SignIn(data, tokenProvider, httpContext));

app.MapGet("/view-users", (UserService userService) => userService.GetUsers());

app.MapPost("/create-auction", ([FromBody] Entites.AuctionData data, IAuctionsProvider auctionProvider) => auctionProvider.AddAuction(data));

app.MapGet("/view-auctions", (IAuctionsProvider auctionProvider) => auctionProvider.GetAuctions());
app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
