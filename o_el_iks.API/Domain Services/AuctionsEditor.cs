using o_el_iks.API.Entities;
using o_el_iks.API.Interfaces;

namespace o_el_iks.API.Domain_Services;

public class AuctionsEditor(IAuctionsProvider auctionsProvider) : IAuctionsEditor
{
    public AuctionData GetAuction(Guid id)
    {
        var auction = auctionsProvider.GetAuctions().FirstOrDefault(a => a.Id == id);
        return auction;
    }
    public void EditAuction(Guid id, AuctionData newData)
    {
        var auction = GetAuction(id);
        if (auction != null)
        {
            auction.UpdateAuction(newData.Price, newData.Location, newData.DateOfEnd, newData.Condition);
        }
    }
}