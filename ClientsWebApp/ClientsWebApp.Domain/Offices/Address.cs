namespace ClientsWebApp.Domain.Offices
{
    public class Address
    {
        public string City { get; private set; }
        public string Street { get; private set; }
        public int HouseNumber { get; private set; }
        public Address(string city, string street, int houseNumber)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{City} {Street} {HouseNumber}";
        }
    }
}
