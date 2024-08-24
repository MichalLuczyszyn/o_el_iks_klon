using o_el_iks.API.Entities;

namespace o_el_iks.API.Interfaces;

public interface IAuctionsProvider
{
    void AddAuction(AuctionCreate data);
    List<AuctionData> GetAuctions();
}