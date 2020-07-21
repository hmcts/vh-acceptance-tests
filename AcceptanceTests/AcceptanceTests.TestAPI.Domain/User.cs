using System;
using AcceptanceTests.TestAPI.Domain.Ddd;
using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.Domain
{
    public class User : AggregateRoot<Guid>
    {
        public string Username { get; set; }
        public string ContactEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public int Number { get; set; }
        public UserType UserType { get; set; }
        public Application Application { get; set; }
        public DateTime CreatedDate { get; set; }

        public User()
        {
            CreatedDate = DateTime.UtcNow;
        }

        public User(string username, string contactEmail, string firstName, string lastName, 
            string displayName, int number, UserType userType, Application application) : this()
        {
            Username = username;
            ContactEmail = contactEmail;
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Number = number;
            UserType = userType;
            Application = application;
        }
    }
}
