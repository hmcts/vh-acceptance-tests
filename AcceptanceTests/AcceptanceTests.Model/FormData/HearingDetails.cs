using AcceptanceTests.Model.Type;

namespace AcceptanceTests.Model.FormData
{
    public class HearingDetails
    {
        public string CaseNumber { get; set; }
        public string CaseName { get; set; }
        public HearingType HearingType { get; set; }
        public bool SendQuestionnaire { get; set; }

        public HearingDetails GenerateFakeHearing()
        {
            CaseNumber = Faker.RandomNumber.Next().ToString();
            CaseName = $"{Faker.Name.FullName()} vs. {Faker.Name.FullName()}";
            HearingType = HearingType.Hearing;
            SendQuestionnaire = false;
            return this;
        }
    }
}
