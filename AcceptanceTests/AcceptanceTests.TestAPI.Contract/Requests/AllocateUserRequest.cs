using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.Contract.Requests
{
    /// <summary>
    /// Allocate a user request model
    /// </summary>
    public class AllocateUserRequest
    {
        /// <summary>
        /// User type of the user (e.g. Judge, Individual etc...)
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// The Application to assign the user too (e.g. VideoWeb, AdminWeb etc...)
        /// </summary>
        public Application Application { get; set; }
    }
}
