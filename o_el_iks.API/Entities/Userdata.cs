namespace o_el_iks.API.Entities;

public class User
{
    public string name { get; set; }
    public string surname { get; set; }
    public DateTimeOffset birthDate { get; set; }
    public DateTimeOffset accountCreationDate { get; set; }
    public List<AuctionData> auctions { get; set; }
    public string email { get; set; }
    public string password { get; set; }

    public User(IAuctionsProvider auctionsProvider)
    {
        auctions = auctionsProvider.GetAuctions();
    }
}