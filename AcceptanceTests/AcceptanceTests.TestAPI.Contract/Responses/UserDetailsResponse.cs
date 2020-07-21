using System;
using AcceptanceTests.TestAPI.Domain.Enums;

namespace AcceptanceTests.TestAPI.Contract.Responses
{
    /// <summary>
    /// GetUserDetailsResponse
    /// </summary>
    public class UserDetailsResponse
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// ContactEmail
        /// </summary>
        public string ContactEmail { get; set; }
        /// <summary>
        /// First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// UserType
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Application
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
