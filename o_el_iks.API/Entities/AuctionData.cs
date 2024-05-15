namespace o_el_iks.API.Entities;

public class AuctionData
{
    public Guid Id { get; set; }
    public float Price { get; set; }
    public string Location { get; set; }
    public DateTimeOffset DateOfStart { get; set; }
    public DateTimeOffset DateOfEnd { get; set; }
    public TypeOfItem Condition { get; set; }
    public DateTimeOffset LastModifiedAt { get; private set; }

    public void UpdateAuction(float price, string location, DateTimeOffset dateOfEnd,
        TypeOfItem condition)
    {
        Price = price;
        Location = location;
        DateOfEnd = dateOfEnd;
        Condition = condition;
        LastModifiedAt = DateTimeOffset.UtcNow;
    }
}
