using System.Collections.Generic;
using AcceptanceTests.Common.Model.Case;
using AcceptanceTests.Common.Model.Hearing;

namespace AcceptanceTests.Common.Model.Mappings
{
    public class CaseMappings
    {
        public CaseMappings(Dictionary<CaseType, CaseTypesHearingsRoles> mappingList)
        {
            MappingList = mappingList;
        }

        public Dictionary<CaseType, CaseTypesHearingsRoles> MappingList { get; }

        public void CreateMappings()
        {
            var genericHearingTypeList = new List<HearingType> { HearingType.Hearing };
            CreateCivilMoneyClaimsMapping();
            CreateCaseTypeApplicantRespondentMapping(CaseType.FinancialRemedy, CaseTypeHearingTypes.GetFinancialRemedyHearingTypes());
            CreateCaseTypeApplicantRespondentMapping(CaseType.ChildrenAct, genericHearingTypeList);
            CreateCaseTypeApplicantRespondentMapping(CaseType.Tax, CaseTypeHearingTypes.GetTaxHearingTypes());
            CreateCaseTypeApplicantRespondentMapping(CaseType.FamilyLawAct, genericHearingTypeList);
            CreateCaseTypeApplicantRespondentMapping(CaseType.Tribunal, genericHearingTypeList);
            CreateCaseTypeApplicantRespondentMapping(CaseType.Generic, genericHearingTypeList);
        } 

        private void CreateCivilMoneyClaimsMapping()
        {
            var caseTypeMapping = new CaseTypesHearingsRoles
            {
                CaseType = CaseType.CivilMoneyClaims,
                HearingTypeList = CaseTypeHearingTypes.GetCivilMoneyClaimsHearingTypes(),
                CaseRoleHearingsMapping = CaseRoleHearingsMapping.CreateClaimantDefendantCaseRolesMapping()
            };

            MappingList.Add(caseTypeMapping.CaseType, caseTypeMapping);
        }

        private void CreateCaseTypeApplicantRespondentMapping(CaseType caseType, List<HearingType> hearingTypeList)
        {
            var caseTypeMapping = new CaseTypesHearingsRoles
            {
                CaseType = caseType,
                HearingTypeList = hearingTypeList,
                CaseRoleHearingsMapping = CaseRoleHearingsMapping.CreateApplicantRespondentCaseRolesMapping()
            };

            MappingList.Add(caseTypeMapping.CaseType, caseTypeMapping);
        }
    }
}
