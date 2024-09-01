using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using o_el_iks.API.Entities;

namespace o_el_iks.API.DAL.Configurations;

internal sealed class AuctionConfiguration : IEntityTypeConfiguration<AuctionData>
{
    public void Configure(EntityTypeBuilder<AuctionData> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
    }
}