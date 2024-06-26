﻿using o_el_iks.API.Entities;

namespace o_el_iks.API;

public interface IAuctionsProvider
{
    void AddAuction(AuctionData data);
    List<AuctionData> GetAuctions();
}