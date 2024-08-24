using o_el_iks.API.Entities;
using o_el_iks.API.Interfaces;

namespace o_el_iks.API.Providers;

public class AuctionsProvider : IAuctionsProvider
{
    public static List<AuctionData> Auctions = new List<AuctionData>();

    public void AddAuction(AuctionCreate data)
    {
        if (data.Price == 0 || string.IsNullOrWhiteSpace(data.Location) || data.DateOfEnd == default 
            || data.Condition is not (TypeOfItem.New or TypeOfItem.Old or TypeOfItem.Used) )
        {
            throw new ArgumentException("Incorrect data.");
        }
        AuctionData newAuction = new AuctionData
        {
            Id = Guid.NewGuid(),
            Price = data.Price, 
            Location = data.Location, 
            DateOfStart = DateTimeOffset.UtcNow,
            DateOfEnd = data.DateOfEnd, 
            Condition = data.Condition
        };
        Auctions.Add(newAuction);
    }
    
    public List<AuctionData> GetAuctions()
    {
        return Auctions;
    }
}