using System.Collections.Generic;
using AdminWebsite.Common.Model.Case;
using AdminWebsite.Common.Model.Hearing;

namespace AdminWebsite.Common.Model.Mappings
{
    public class CaseTypesHearingsRoles
    {
        public CaseType CaseType;
        public List<HearingType> HearingTypeList { get; set; }
        public Dictionary<CaseRole,CaseRoleHearingsRoles> CaseRoleHearingsMapping { get; set; }
    }
}

