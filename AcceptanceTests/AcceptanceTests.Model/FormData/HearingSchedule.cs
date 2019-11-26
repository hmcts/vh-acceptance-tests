using System;
using AcceptanceTests.Model.Hearing;

namespace AcceptanceTests.Model.FormData
{
    public class HearingSchedule
    {
        public HearingDateTime DateTime { get; set; }
        public HearingVenue HearingVenue { get; set; }
        public string Room { get; set; }

        public HearingSchedule GenerateFakeHearingSchedule()
        {
            Console.WriteLine("Generating fake address data:");
            var dateTime = new HearingDateTime();
            DateTime = dateTime.GenerateFakeDateTimeData(dateTime.DefaultFormat);
            Room = Faker.Name.Last();
            Console.WriteLine($"Generating fake courtroom {Room}");
            return this;
        }
    }
}
