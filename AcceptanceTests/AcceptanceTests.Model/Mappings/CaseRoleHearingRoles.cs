using System.Collections.Generic;
using AcceptanceTests.Model.Case;
using AcceptanceTests.Model.Hearing;

namespace AcceptanceTests.Model.Mappings
{
    public class CaseRoleHearingsRoles
    {
        public CaseRole CaseRole { get; set; }
        public List<HearingRole> HearingRoleList { get; set; }

    }
}
