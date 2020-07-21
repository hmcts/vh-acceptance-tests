using AcceptanceTests.TestAPI.Contract.Requests;
using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.Common.Builders
{
    public class UserRequestBuilder
    {
        private readonly string _emailStem;
        private readonly int _number;
        private UserType _userType;
        private Application _application;

        public UserRequestBuilder(string emailStem, int number)
        {
            _emailStem = emailStem;
            _number = number;
        }

        public UserRequestBuilder WithUserType(UserType userType)
        {
            _userType = userType;
            return this;
        }

        public UserRequestBuilder ForApplication(Application application)
        {
            _application = application;
            return this;
        }

        public CreateNewUserRequest Build()
        {
            var firstname = SetFirstName();
            var lastname = SetLastName();

            return new CreateNewUserRequest()
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
