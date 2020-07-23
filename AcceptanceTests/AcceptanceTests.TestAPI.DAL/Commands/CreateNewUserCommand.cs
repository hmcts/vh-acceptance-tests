using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class CreateNewUserCommand : ICommand
    {
        public string Username { get; set; }
        public string ContactEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public int Number { get; set; }
        public UserType UserType { get; set; }
        public Application Application { get; set; }
        public Guid NewUserId { get; set; }

        public CreateNewUserCommand(string username, string contactEmail, string firstName, string lastName, 
            string displayName, int number, UserType userType, Application application)
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

    public class CreateNewUserCommandHandler : ICommandHandler<CreateNewUserCommand>
    {
        private readonly TestApiDbContext _context;

        public CreateNewUserCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateNewUserCommand command)
        {
            var user = new Domain.User(
                command.Username, 
                command.ContactEmail, 
                command.FirstName,
                command.LastName,
                command.DisplayName,
                command.Number,
                command.UserType,
                command.Application
                );
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            command.NewUserId = user.Id;
        }
    }
}
