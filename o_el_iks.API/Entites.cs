namespace o_el_iks.API;

public class Entites
{
    public class RegistrationData
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public class SignInData
    {
        public string email { get; set; }
        public string password { get; set; }
    }

    public enum TypeOfItem
    {
        New = 1,
        Old = 2,
        Used = 3
    }

    public class AuctionData
    {
        public float price { get; set; }
        public string location { get; set; } 
        public DateTimeOffset dateOfStart { get; set; }
        public DateTimeOffset dateOfEnd { get; set; }
        public TypeOfItem condition { get; set; }
    }
}