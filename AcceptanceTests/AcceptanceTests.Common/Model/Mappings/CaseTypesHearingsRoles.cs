using System.Collections.Generic;
using AcceptanceTests.Common.Model.Case;

namespace AcceptanceTests.Common.Model.Mappings
{
    public class CaseTypesHearingsRoles
    {
        public CaseType CaseType;
        public List<string> HearingTypeList { get; set; }
        public Dictionary<CaseRole,CaseRoleHearingsRoles> CaseRoleHearingsMapping { get; set; }
    }
}

