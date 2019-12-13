using System.Collections.Generic;
using AdminWebsite.Common.Model.Case;
using AdminWebsite.Common.Model.Hearing;

namespace AdminWebsite.Common.Model.Mappings
{
    public class CaseRoleHearingsRoles
    {
        public CaseRole CaseRole { get; set; }
        public List<HearingRole> HearingRoleList { get; set; }
    }
}
