using System.Collections.Generic;
using AcceptanceTests.Common.Model.Case;
using AcceptanceTests.Common.Model.Hearing;

namespace AcceptanceTests.Common.Model.Mappings
{
    public class CaseRoleHearingsRoles
    {
        public CaseRole CaseRole { get; set; }
        public List<HearingRole> HearingRoleList { get; set; }
    }
}
