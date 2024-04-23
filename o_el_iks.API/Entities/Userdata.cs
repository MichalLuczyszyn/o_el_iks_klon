namespace o_el_iks.API.Entities;

public class User
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTimeOffset BirthDate { get; set; }
    public DateTimeOffset AccountCreationDate { get; set; }
    public List<AuctionData> Auctions { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
}