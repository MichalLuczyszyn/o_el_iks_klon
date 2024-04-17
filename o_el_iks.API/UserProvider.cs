using System.Security.Cryptography;
using System.Text;
using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class UserProvider : IUserProvider
{
    static List<RegistrationData> users = new List<RegistrationData>();

    public void Register(RegistrationData data)
    {
        var userExists = users.Any(u => u.email == data.email);
        if (userExists)
        {
            throw new ArgumentException("User with that email already exists");
        }

        if (string.IsNullOrWhiteSpace(data.email) || string.IsNullOrWhiteSpace(data.password))
        {
            throw new ArgumentException("Incorrect data.");
        }
        string hashedPassword = ShaHash(data.password);
        RegistrationData newUser = new RegistrationData { email = data.email, password = hashedPassword };
        users.Add(newUser);
    }

    public void SignIn(SignInData data, ITokenProvider tokenProvider, HttpContext httpContext)
    {
        var user = users.Any(u => u.email == data.email && u.password == ShaHash(data.password));
        if (!user)
        {
            throw new ArgumentException("Incorrect email or password.");
        }
        var token = tokenProvider.GenerateToken();
        httpContext.Response.Cookies.Append("token", token);
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