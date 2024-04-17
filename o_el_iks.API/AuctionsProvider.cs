using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class AuctionsProvider : IAuctionsProvider
{
    static List<AuctionData> auctions = new List<AuctionData>();
    
    public IResult AddAuction(AuctionData data)
    {
        AuctionData newAuction = new AuctionData
        {
            price = data.price, 
            location = data.location, 
            dateOfStart = data.dateOfStart,
            dateOfEnd = data.dateOfEnd, 
            condition = data.condition
        };
        auctions.Add(newAuction);
        return Results.Ok("Auction created.");
    }
    
    public List<AuctionData> GetAuctions()
    {
        return auctions;
    }
}