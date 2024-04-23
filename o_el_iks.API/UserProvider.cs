using System.Security.Cryptography;
using System.Text;
using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class UserProvider(IAuctionsService auctionsService) : IUserProvider
{
    public static List<User> _users = new List<User>();

    public void Register(RegistrationData data)
    {
        var userExists = _users.Any(u => u.Email == data.Email);
        if (userExists)
        {
            throw new ArgumentException("User with that email already exists");
        }

        if (string.IsNullOrWhiteSpace(data.Email) || string.IsNullOrWhiteSpace(data.Password))
        {
            throw new ArgumentException("Incorrect data.");
        }
        string hashedPassword = ShaHash(data.Password);
        User newUser = new User { Email = data.Email, Password = hashedPassword };
        _users.Add(newUser);
    }

    public void SignIn(SignInData data, ITokenProvider tokenProvider, HttpContext httpContext)
    {
        var user = _users.Any(u => u.Email == data.Email && u.Password == ShaHash(data.Password));
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

    public List<User> GetUsers()
    {
        foreach (var user in _users)
        {
            user.Auctions = auctionsService.ViewAuctions();
        }
        return _users;
    }
}