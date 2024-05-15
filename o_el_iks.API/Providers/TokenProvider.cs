using o_el_iks.API.Interfaces;

namespace o_el_iks.API.Providers;

public class TokenProvider : ITokenProvider
{
    public string GenerateToken()
    {
        return "zalogowany";
    }
}