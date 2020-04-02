using System.Collections.Generic;
using AcceptanceTests.Common.Model.Case;
using AcceptanceTests.Common.Model.Hearing;

namespace AcceptanceTests.Common.Model.Mappings
{
    public class CaseRoleHearingsMapping
    {
        internal static Dictionary<CaseRole, CaseRoleHearingsRoles> CreateClaimantDefendantCaseRolesMapping()
        {
            var mapping = new Dictionary<CaseRole, CaseRoleHearingsRoles>();

            var claimantCaseRoleMapping = CreateClaimantMapping();
            mapping.Add(claimantCaseRoleMapping.CaseRole, claimantCaseRoleMapping);

            var defendantCaseRoleMapping = CreateDefendantMapping();
            mapping.Add(defendantCaseRoleMapping.CaseRole, defendantCaseRoleMapping);

            return mapping;
        }

        internal static Dictionary<CaseRole, CaseRoleHearingsRoles> CreateApplicantRespondentCaseRolesMapping()
        {
            var mapping = new Dictionary<CaseRole, CaseRoleHearingsRoles>();

            var applicantCaseRoleMapping = CreateApplicantMapping();
            mapping.Add(applicantCaseRoleMapping.CaseRole, applicantCaseRoleMapping);

            var respondentCaseRoleMapping = CreateRespondentMapping();
            mapping.Add(respondentCaseRoleMapping.CaseRole, respondentCaseRoleMapping);

            return mapping;
        }

        internal static CaseRoleHearingsRoles CreateClaimantMapping()
        {
            var caseRoleMapping = new CaseRoleHearingsRoles
            {
                CaseRole = CaseRole.Claimant,
                HearingRoleList = new List<HearingRole> { HearingRole.ClaimantLip, HearingRole.Representative }
            };

            return caseRoleMapping;
        }

        internal static CaseRoleHearingsRoles CreateDefendantMapping()
        {
            var caseRoleMapping = new CaseRoleHearingsRoles
            {
                CaseRole = CaseRole.Defendant,
                HearingRoleList = new List<HearingRole> { HearingRole.DefendantLip, HearingRole.Representative }
            };

            return caseRoleMapping;
        }

        internal static CaseRoleHearingsRoles CreateApplicantMapping()
        {
            var caseRoleMapping = new CaseRoleHearingsRoles
            {
                CaseRole = CaseRole.Applicant,
                HearingRoleList = new List<HearingRole> { HearingRole.ApplicantLip, HearingRole.Representative }
            };

            return caseRoleMapping;
        }

        internal static CaseRoleHearingsRoles CreateRespondentMapping()
        {
            var caseRoleMapping = new CaseRoleHearingsRoles
            {
                CaseRole = CaseRole.Applicant,
                HearingRoleList = new List<HearingRole> { HearingRole.RespondentLip, HearingRole.Representative }
            };

            return caseRoleMapping;
        }
    }
}
