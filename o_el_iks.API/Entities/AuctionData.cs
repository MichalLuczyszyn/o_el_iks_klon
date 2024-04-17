namespace o_el_iks.API.Entities;

public class AuctionData
{
    public float price { get; set; }
    public string location { get; set; } 
    public DateTimeOffset dateOfStart { get; set; }
    public DateTimeOffset dateOfEnd { get; set; }
    public TypeOfItem condition { get; set; }
}