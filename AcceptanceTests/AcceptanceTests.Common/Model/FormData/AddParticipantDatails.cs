namespace AcceptanceTests.Common.Model.FormData
{
    public class ParticipantDatails : IFormData
    {
        public IFormData Party { get; set; }
        public IFormData Role { get; set; }
        public string Email { get; set; }
        public IFormData Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organisation { get; set; }
        public string Telephone { get; set; }
        public string DisplayName { get; set; }
        public IFormData Address { get; set; }

        public IFormData GenerateFake()
        {
            Party = new DropdownListFormData();
            Role = new DropdownListFormData();
            FirstName = $"Automation_{Faker.Name.First()}";
            LastName = Faker.Name.Last(); 
            Email = $"Automation_{FirstName}.{LastName}@{Faker.Internet.DomainName()}";
            Title = new DropdownListFormData();
            Telephone = Faker.Phone.Number();
            DisplayName = $"{FirstName} {LastName}";
            Address = new Address().GenerateFake();

            return this;
        }
    }
}
