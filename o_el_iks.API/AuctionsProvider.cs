using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class AuctionsProvider : IAuctionsProvider
{
    public static List<AuctionData> _auctions = new List<AuctionData>();

    public void AddAuction(AuctionData data)
    {
        if (data.Price == 0 || string.IsNullOrWhiteSpace(data.Location) || data.DateOfStart == default ||
            data.DateOfEnd == default || data.Condition is not (TypeOfItem.New or TypeOfItem.Old or TypeOfItem.Used) )
        {
            throw new ArgumentException("Incorrect data.");
        }
        AuctionData newAuction = new AuctionData
        {
            Price = data.Price, 
            Location = data.Location, 
            DateOfStart = data.DateOfStart,
            DateOfEnd = data.DateOfEnd, 
            Condition = data.Condition
        };
        _auctions.Add(newAuction);
    }
    
    public List<AuctionData> GetAuctions()
    {
        return _auctions;
    }
}