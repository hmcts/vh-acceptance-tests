namespace AcceptanceTests.Model.Forms
{
    public class Address
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }

        public Address GenerateFakeAddress()
        {
            Street = Faker.Address.StreetAddress();
            City = Faker.Address.City();
            County = Faker.Address.UkCounty();
            Postcode = Faker.Address.UkPostCode().ToUpper();
            return this;
        }
    }
}
