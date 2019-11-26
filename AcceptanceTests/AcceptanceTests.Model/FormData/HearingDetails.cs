using System;
using AcceptanceTests.Model.Hearing;

namespace AcceptanceTests.Model.FormData
{
    public class HearingDetails
    {
        public string CaseNumber { get; set; }
        public string CaseName { get; set; }
        public HearingType HearingType { get; set; }
        public bool DontSendQuestionnaire { get; set; }

        public HearingDetails GenerateFakeHearing()
        {
            Console.WriteLine("Generating fake hearing details data:");

            CaseNumber = Faker.RandomNumber.Next().ToString();
            Console.WriteLine($"Generating fake case number {CaseNumber}");

            CaseName = $"{Faker.Name.FullName()} vs. {Faker.Name.FullName()}";
            Console.WriteLine($"Generating fake case name {CaseName}");

            DontSendQuestionnaire = true;
            return this;
        }
    }
}
