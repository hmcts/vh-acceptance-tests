using System;

namespace AcceptanceTests.TestAPI.Contract.Responses
{
    /// <summary>
    /// Details of a users current allocation
    /// </summary>
    public class UserAllocationResponse
    {
        /// <summary>
        /// Id of the allocation
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// At what time the user can be reused
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Has the user been allocated to an environment
        /// </summary>
        public bool Allocated { get; set; }
    }
}
