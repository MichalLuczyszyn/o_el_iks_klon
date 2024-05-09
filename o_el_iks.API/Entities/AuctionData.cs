namespace o_el_iks.API.Entities;

public class AuctionData
{
    public float Price { get; set; }
    public string Location { get; set; } 
    public DateTimeOffset DateOfStart { get; set; }
    public DateTimeOffset DateOfEnd { get; set; }
    public TypeOfItem Condition { get; set; }
}