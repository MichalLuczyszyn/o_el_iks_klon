using o_el_iks.API.Entities;

namespace o_el_iks.API.Interfaces;

public interface IAuctionsService
{
    List<AuctionData> ViewAuctions();
}