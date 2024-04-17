namespace o_el_iks.API;

public class TokenProvider : ITokenProvider
{
    public string GenerateToken()
    {
        return "zalogowany";
    }
}