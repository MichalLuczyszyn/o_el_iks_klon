using o_el_iks.API.Entities;

namespace o_el_iks.API;

public class AuctionsService(IAuctionsProvider auctionsProvider) : IAuctionsService
{ 
    public List<AuctionData> ViewAuctions()
    {
        return auctionsProvider.GetAuctions();
    }
}