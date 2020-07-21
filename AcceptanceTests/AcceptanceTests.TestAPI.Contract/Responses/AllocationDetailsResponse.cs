using System;

namespace AcceptanceTests.TestAPI.Contract.Responses
{
    /// <summary>
    /// Allocate a user request model
    /// </summary>
    public class AllocationDetailsResponse
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
        /// EnvironmentName
        /// </summary>
        public string EnvironmentName { get; set; }

        /// <summary>
        /// ExpiresAt
        /// </summary>
        public DateTime? ExpiresAt { get; set; }

        /// <summary>
        /// Allocated
        /// </summary>
        public bool Allocated { get; set; }
    }
}
