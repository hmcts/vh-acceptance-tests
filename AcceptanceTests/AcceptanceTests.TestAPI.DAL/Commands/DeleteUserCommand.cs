using System;
using System.Threading.Tasks;
using AcceptanceTests.TestAPI.DAL.Commands.Core;
using AcceptanceTests.TestAPI.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AcceptanceTests.TestAPI.DAL.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public string Username { get; }

        public DeleteUserCommand(string username)
        {
            Username = username;
        }
    }

    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly TestApiDbContext _context;

        public DeleteUserCommandHandler(TestApiDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteUserCommand command)
        {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => string.Equals(x.Username, command.Username, StringComparison.CurrentCultureIgnoreCase));

            if (user == null)
            {
                throw new UserNotFoundException(command.Username);
            }

            _context.Remove(user);

            await _context.SaveChangesAsync();
        }
    }
}
