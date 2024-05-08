using o_el_iks.API.Entities;
using o_el_iks.API.Interfaces;

namespace o_el_iks.API.Domain_Services;

public class AuctionsEditor(IAuctionsProvider auctionsProvider) : IAuctionsEditor
{
    public void EditAuction(string location, AuctionData newData)
    {
        var auction = auctionsProvider.GetAuctions().FirstOrDefault(a => a.Location == location);
        if (auction == null)
        {
            throw new ArgumentException("Auction does not exist.");
        }

        auction.Price = newData.Price;
        auction.DateOfEnd = newData.DateOfEnd;
        auction.Condition = newData.Condition;
    }
}