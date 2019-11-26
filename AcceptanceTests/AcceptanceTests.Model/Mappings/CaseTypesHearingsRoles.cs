using System.Collections.Generic;
using AcceptanceTests.Model.Case;
using AcceptanceTests.Model.Hearing;

namespace AcceptanceTests.Model.Mappings
{
    public class CaseTypesHearingsRoles
    {
        public CaseType CaseType;
        public List<string> HearingTypeList { get; set; }
        public Dictionary<CaseRole,CaseRoleHearingsRoles> CaseRoleHearingsMapping { get; set; }
    }
}

