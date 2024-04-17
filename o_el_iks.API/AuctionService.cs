namespace o_el_iks.API;

public class AuctionService
{
    static List<Entites.AuctionData> auctions = new List<Entites.AuctionData>();
    
    public IResult CreateAuction(Entites.AuctionData data)
    {
        Entites.AuctionData newAuction = new Entites.AuctionData
        {
            price = data.price, location = data.location, dateOfStart = data.dateOfStart,
            dateOfEnd = data.dateOfEnd, condition = data.condition
        };
        auctions.Add(newAuction);
        return Results.Ok("Auction created.");
    }
    
    public List<Entites.AuctionData> GetAuctions()
    {
        return auctions;
    }
}