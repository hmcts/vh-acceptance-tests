using System;
using AcceptanceTests.TestAPI.Domain.Ddd;

namespace AcceptanceTests.TestAPI.Domain
{
    public class Allocation : Entity<Guid>
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public User User { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool Allocated { get; set; }

        protected Allocation()
        {
            ExpiresAt = null;
            Allocated = false;
        }

        public Allocation(User user) : this()
        {
            UserId = user.Id;
            Username = user.Username;
        }

        public void Allocate(int minutes)
        {
            Allocated = true;
            ExpiresAt = DateTime.UtcNow.AddMinutes(minutes);
        }

        public bool IsAllocated()
        {
            if (ExpiresAt == null)
            {
                return false;
            }

            return Allocated && DateTime.UtcNow < ExpiresAt;
        }

        public void Unallocate()
        {
            Allocated = false;
            ExpiresAt = null;
        }
    }
}
