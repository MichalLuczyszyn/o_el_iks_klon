using o_el_iks.API.Entities;

namespace o_el_iks.API;

public interface IUserProvider
{
    IResult Register(RegistrationData data);
    IResult SignIn(SignInData data, ITokenProvider tokenProvider, HttpContext httpContext);
    List<RegistrationData> GetUsers();
}