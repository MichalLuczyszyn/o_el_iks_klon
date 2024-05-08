using o_el_iks.API.Entities;
using o_el_iks.API.Interfaces;

namespace o_el_iks.API.Domain_Services;

public class AuctionsService(IAuctionsProvider auctionsProvider) : IAuctionsService
{ 
    public List<AuctionData> ViewAuctions()
    {
        return auctionsProvider.GetAuctions();
    }
}