namespace o_el_iks.API;

public interface IAuctionsProvider
{
    IResult AddAuction(Entites.AuctionData data);
    List<Entites.AuctionData> GetAuctions();
}