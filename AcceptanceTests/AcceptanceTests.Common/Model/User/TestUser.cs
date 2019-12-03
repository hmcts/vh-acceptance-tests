using System;
using System.Collections.Generic;
using AcceptanceTests.Common.Model.Case;
using AcceptanceTests.Common.Model.Hearing;

namespace AcceptanceTests.Common.Model.User
{
    public class TestUser
    {
        public Guid Id { get; set; }
        public List<Guid> Hearings { get; set; }
        public UserRole Role { get; set; }
        public CaseRole CaseRole { get; set; }
        public HearingRole HearingRole { get; set; }
        public string AlternativeEmail { get; set; }
        public string Firstname { get; set; } 
        public string Lastname { get; set; }
        public string Displayname { get; set; }
        public string Username { get; set; }
        public string Representee { get; set; }
        public string SolicitorsReference { get; set; }
        public bool DefaultParticipant { get; set; }
        public List<CaseType> CaseTypes { get; set; }
    }
}
