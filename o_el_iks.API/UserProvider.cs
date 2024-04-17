using System.Security.Cryptography;
using System.Text;
using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class UserProvider : IUserProvider
{
    static List<RegistrationData> users = new List<RegistrationData>();

    public IResult Register(RegistrationData data)
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
    }

    public IResult SignIn(SignInData data, ITokenProvider tokenProvider, HttpContext httpContext)
    {
        var user = users.Any(u => u.email == data.email && u.password == ShaHash(data.password));
        if (user)
        {
            var token = tokenProvider.GenerateToken();
            httpContext.Response.Cookies.Append("token", token);
            return Results.Ok("Sign in successful.");
        }
        return Results.BadRequest("Incorrect e-mail or password.");
    }
    
    static string ShaHash(string password)
    {
        using (SHA256 hashedPassword = SHA256.Create())
        {
            byte[] hashedBytes = hashedPassword.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder passwordBuilder = new StringBuilder();
            foreach (var t in hashedBytes)
            {
                passwordBuilder.Append(t.ToString("x2"));
            }

            return passwordBuilder.ToString();
        }
    }

    public List<RegistrationData> GetUsers()
    {
        return users;
    }

}