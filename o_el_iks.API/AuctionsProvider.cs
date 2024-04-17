using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class AuctionsProvider : IAuctionsProvider
{
    static List<AuctionData> auctions = new List<AuctionData>();

    public void AddAuction(AuctionData data)
    {
        if (data.price == 0 || string.IsNullOrWhiteSpace(data.location) || data.dateOfStart == default ||
            data.dateOfEnd == default || data.condition is not (TypeOfItem.New or TypeOfItem.Old or TypeOfItem.Used) )
        {
            throw new ArgumentException("Incorrect data.");
        }
        AuctionData newAuction = new AuctionData
        {
            price = data.price, 
            location = data.location, 
            dateOfStart = data.dateOfStart,
            dateOfEnd = data.dateOfEnd, 
            condition = data.condition
        };
        auctions.Add(newAuction);
    }
    
    public List<AuctionData> GetAuctions()
    {
        return auctions;
    }
}