namespace o_el_iks.API.Entities;

public class AuctionData
{
    private float _price;
    public float Price
    {
        get => _price;
        set
        {
            _price = value;
            LastModifiedAt = DateTimeOffset.Now;
        }
    }
    public string Location{ get; set;}

    public DateTimeOffset DateOfStart{ get; set;}

    private DateTimeOffset _dateOfEnd;
    public DateTimeOffset DateOfEnd
    {
        get => _dateOfEnd;
        set
        {
            _dateOfEnd = value;
            LastModifiedAt = DateTimeOffset.Now;
        }
    }

    private TypeOfItem _condition;
    public TypeOfItem Condition
    {
        get => _condition;
        set
        {
            _condition = value;
            LastModifiedAt = DateTimeOffset.Now;
        }
    }

    public DateTimeOffset LastModifiedAt { get; private set; }
}
