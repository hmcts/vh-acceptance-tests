using System;

namespace AcceptanceTests.Model.FormData
{
    public class HearingDetails : IFormData
    {
        public string CaseNumber { get; set; }
        public string CaseName { get; set; }
        public IFormData HearingType { get; set; }
        public bool DontSendQuestionnaire { get; set; }

        public IFormData GenerateFake()
        {
            Console.WriteLine("Generating fake hearing details data:");

            CaseNumber = Faker.RandomNumber.Next().ToString();
            Console.WriteLine($"Generating fake case number {CaseNumber}");

            CaseName = $"{Faker.Name.FullName()} vs. {Faker.Name.FullName()}";
            Console.WriteLine($"Generating fake case name {CaseName}");

            HearingType = new DropdownListFormData();

            DontSendQuestionnaire = true;
            return this;
        }
    }
}
