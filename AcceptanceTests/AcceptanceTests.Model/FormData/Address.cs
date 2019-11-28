using System;

namespace AcceptanceTests.Model.FormData
{
    public class Address : IFormData
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }

        public IFormData GenerateFake()
        {
            Console.WriteLine("Generating fake address data:");

            HouseNumber = Faker.RandomNumber.Next().ToString();
            Console.WriteLine($"Generating fake house number {HouseNumber}");

            Street = Faker.Address.StreetAddress();
            Console.WriteLine($"Generating fake street {Street}");

            City = Faker.Address.City();
            Console.WriteLine($"Generating fake city {City}");

            County = Faker.Address.UkCounty();
            Console.WriteLine($"Generating fake county {County}");

            Postcode = Faker.Address.UkPostCode().ToUpper();
            Console.WriteLine($"Generating fake UK postcode {Postcode}");
            return this;
        }
    }
}
