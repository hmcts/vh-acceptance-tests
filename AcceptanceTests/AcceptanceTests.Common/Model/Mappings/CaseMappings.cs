using System.Collections.Generic;
using AcceptanceTests.Common.Model.Case;
using AcceptanceTests.Common.Model.Hearing;
using HearingType = AcceptanceTests.Common.Model.Case.HearingType;

namespace AcceptanceTests.Common.Model.Mappings
{
    public class CaseMappings
    {
        public Dictionary<HearingType, CaseTypesHearingsRoles> MappingList { get; }

        public void CreateMappings()
        {
            var genericHearingTypeList = new List<string> { Hearing.HearingType.Hearing };
            CreateCivilMoneyClaimsMapping();
            CreateCaseTypeApplicantRespondentMapping(HearingType.FinancialRemedy, CaseTypeHearingTypes.GetFinancialRemedyHearingTypes());
            CreateCaseTypeApplicantRespondentMapping(HearingType.ChildrenAct, genericHearingTypeList);
            CreateCaseTypeApplicantRespondentMapping(HearingType.Tax, CaseTypeHearingTypes.GetTaxHearingTypes());
            CreateCaseTypeApplicantRespondentMapping(HearingType.FamilyLawAct, genericHearingTypeList);
            CreateCaseTypeApplicantRespondentMapping(HearingType.Tribunal, genericHearingTypeList);
            CreateCaseTypeApplicantRespondentMapping(HearingType.Generic, genericHearingTypeList);
        } 

        private void CreateCivilMoneyClaimsMapping()
        {
            var caseTypeMapping = new CaseTypesHearingsRoles
            {
                _hearingType = HearingType.CivilMoneyClaims,
                HearingTypeList = CaseTypeHearingTypes.GetCivilMoneyClaimsHearingTypes()
            };

            caseTypeMapping.CaseRoleHearingsMapping = CaseRoleHearingsMapping.CreateClaimantDefendantCaseRolesMapping();
            MappingList.Add(caseTypeMapping._hearingType, caseTypeMapping);
        }

        private void CreateCaseTypeApplicantRespondentMapping(HearingType hearingType, List<string> hearingTypeList)
        {
            var caseTypeMapping = new CaseTypesHearingsRoles
            {
                _hearingType = hearingType,
                HearingTypeList = hearingTypeList
            };

            caseTypeMapping.CaseRoleHearingsMapping = CaseRoleHearingsMapping.CreateApplicantRespondentCaseRolesMapping();
            MappingList.Add(caseTypeMapping._hearingType, caseTypeMapping);
        }
    }
}
