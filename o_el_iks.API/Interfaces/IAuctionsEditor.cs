using o_el_iks.API.Entities;

namespace o_el_iks.API.Interfaces;

public interface IAuctionsEditor
{
    void EditAuction(string location, AuctionData newData);
}