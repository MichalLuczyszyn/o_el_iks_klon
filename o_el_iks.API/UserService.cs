using System.Security.Cryptography;
using System.Text;

namespace o_el_iks.API;

public class UserService
{
    static List<Entites.RegistrationData> users = new List<Entites.RegistrationData>();

    public IResult Register(Entites.RegistrationData data)
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
        Entites.RegistrationData newUser = new Entites.RegistrationData { email = data.email, password = hashedPassword };
        users.Add(newUser);
        return Results.Ok("Registration successful.");
    }

    public IResult SignIn(Entites.SignInData data, ITokenProvider tokenProvider, HttpContext httpContext)
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

    public List<Entites.RegistrationData> GetUsers()
    {
        return users;
    }

}