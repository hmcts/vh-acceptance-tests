using System;

namespace AcceptanceTests.Model.FormData
{
    public class HearingSchedule : IFormData
    {
        public IFormData DateTime { get; set; }
        public IFormData HearingVenue { get; set; }
        public string Room { get; set; }

        public IFormData GenerateFake()
        {
            Console.WriteLine("Generating fake address data:");
            var dateTime = new HearingDateTime();
            DateTime = dateTime.GenerateFake();
            HearingVenue = new DropdownListFormData();
            Room = Faker.Name.Last();
            Console.WriteLine($"Generating fake courtroom {Room}");
            return this;
        }
    }
}
