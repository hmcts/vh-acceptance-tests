using AcceptanceTests.TestAPI.Domain;
using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.Common.Builders
{
    public class UserBuilder
    {
        private readonly string _emailStem;
        private readonly int _number;
        private UserType _userType;
        private Application _application;

        public UserBuilder(string emailStem, int number)
        {
            _emailStem = emailStem;
            _number = number;
        }

        public UserBuilder WithUserType(UserType userType)
        {
            _userType = userType;
            return this;
        }

        public UserBuilder ForApplication(Application application)
        {
            _application = application;
            return this;
        }

        public User CreateJudge()
        {
            _userType = UserType.Judge;
            return Build();
        }

        public User CreateIndividual()
        {
            _userType = UserType.Individual;
            return Build();
        }

        public User CreateRepresentative()
        {
            _userType = UserType.Representative;
            return Build();
        }

        public User CreateCaseAdmin()
        {
            _userType = UserType.CaseAdmin;
            return Build();
        }

        public User CreateVideoHearingsOfficer()
        {
            _userType = UserType.VideoHearingsOfficer;
            return Build();
        }

        public User CreatePanelMember()
        {
            _userType = UserType.PanelMember;
            return Build();
        }

        public User CreateObserver()
        {
            _userType = UserType.Observer;
            return Build();
        }

        public User Build()
        {
            var firstname = SetFirstName();
            var lastname = SetLastName();

            return new User()
            {
                Username = $"{RemoveWhitespace(firstname)}{RemoveWhitespace(lastname)}@{_emailStem}",
                ContactEmail = $"{RemoveWhitespace(firstname)}{RemoveWhitespace(lastname)}@{ContactEmailStem(_emailStem)}",
                FirstName = firstname,
                LastName = lastname,
                DisplayName = $"{firstname} {lastname}",
                UserType = _userType,
                Application = _application
            };
        }

        private string SetFirstName()
        {
            return _userType == UserType.Judge ? $"Auto_{_application}_ Courtroom {_number}" : $"Auto_{_application}_";
        }

        private string SetLastName()
        {
            return _userType == UserType.Judge ? $"Auto Building {_number}" : $"{_userType} {_number}";
        }

        private static string RemoveWhitespace(string text)
        {
            return text.Replace(" ", "").Trim();
        }

        private static string ContactEmailStem(string emailStem)
        {
            return emailStem.Substring(16);
        }
    }
}
