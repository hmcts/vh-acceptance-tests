using System.Collections.Generic;
using AcceptanceTests.Common.Model.Case;
using AcceptanceTests.Common.Model.Hearing;

namespace AcceptanceTests.Common.Model.Mappings
{
    public class CaseTypesHearingsRoles
    {
        public CaseType CaseType;
        public List<HearingType> HearingTypeList { get; set; }
        public Dictionary<CaseRole,CaseRoleHearingsRoles> CaseRoleHearingsMapping { get; set; }
    }
}

