using System.Collections.Generic;
using AcceptanceTests.Model.Role;
using AcceptanceTests.Model.Type;

namespace AcceptanceTests.Model.Mappings
{
    public class CaseTypesHearingsRoles
    {
        public CaseType CaseType;
        public List<HearingType> HearingTypeList { get; set; }
        public Dictionary<CaseRole,CaseRoleHearingsRoles> CaseRoleHearingsMapping { get; set; }
    }
}

