using System.Collections.Generic;
using AcceptanceTests.Model.Role;

namespace AcceptanceTests.Model.Mappings
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
                HearingRoleList = new List<HearingRole> { HearingRole.ClaimantLip, HearingRole.Solicitor }
            };

            return caseRoleMapping;
        }

        internal static CaseRoleHearingsRoles CreateDefendantMapping()
        {
            var caseRoleMapping = new CaseRoleHearingsRoles
            {
                CaseRole = CaseRole.Defendant,
                HearingRoleList = new List<HearingRole> { HearingRole.DefendantLip, HearingRole.Solicitor }
            };

            return caseRoleMapping;
        }

        internal static CaseRoleHearingsRoles CreateApplicantMapping()
        {
            var caseRoleMapping = new CaseRoleHearingsRoles
            {
                CaseRole = CaseRole.Applicant,
                HearingRoleList = new List<HearingRole> { HearingRole.ApplicantLip, HearingRole.Solicitor }
            };

            return caseRoleMapping;
        }

        internal static CaseRoleHearingsRoles CreateRespondentMapping()
        {
            var caseRoleMapping = new CaseRoleHearingsRoles
            {
                CaseRole = CaseRole.Applicant,
                HearingRoleList = new List<HearingRole> { HearingRole.RespondentLip, HearingRole.Solicitor }
            };

            return caseRoleMapping;
        }
    }
}
