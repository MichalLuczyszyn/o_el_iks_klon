using Microsoft.AspNetCore.Mvc;
using o_el_iks.API.DAL;
using o_el_iks.API.Domain_Services;
using o_el_iks.API.Entities;
using o_el_iks.API.Interfaces;
using o_el_iks.API.Providers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IAuctionsProvider, AuctionsProvider>();
builder.Services.AddScoped<IAuctionsService, AuctionsService>();
builder.Services.AddScoped<IAuctionsEditor, AuctionsEditor>();
builder.Services.AddPostgres(builder.Configuration);

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


app.MapPost("/register", ([FromBody] RegistrationData data, IUserProvider userProvider) =>
{
    {
        try
        {
            userProvider.Register(data);
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

app.MapPost("/create-auction", ([FromBody] AuctionCreate data, IAuctionsProvider auctionProvider) =>
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

app.MapPut("/edit-auction", (Guid id, AuctionData data, IAuctionsEditor auctionsEditor) =>
{
        auctionsEditor.EditAuction(id, data);
        return Results.Ok("Auction edited.");
});



app.Run();


record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

