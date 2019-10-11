using System;
using System.Collections.Generic;
using AcceptanceTests.Model.Role;
using AcceptanceTests.Model.Type;

namespace AcceptanceTests.Model.User
{
    public class TestUser
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public string AlternativeEmail { get; set; }
        public string Firstname { get; set; } 
        public string Lastname { get; set; }
        public string Displayname { get; set; }
        public string Username { get; set; }
        public string CaseRoleName { get; set; }
        public string HearingRoleName { get; set; }
        public string Representee { get; set; }
        public string SolicitorsReference { get; set; }
        public bool DefaultParticipant { get; set; }
        public List<CaseType> CaseTypes { get; set; }
    }
}
