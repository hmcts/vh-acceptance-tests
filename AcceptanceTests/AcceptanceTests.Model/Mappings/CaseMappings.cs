using System.Collections.Generic;
using AcceptanceTests.Model.Type;

namespace AcceptanceTests.Model.Mappings
{
    public class CaseMappings
    {
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
                HearingTypeList = CaseTypeHearingTypes.GetCivilMoneyClaimsHearingTypes()
            };

            caseTypeMapping.CaseRoleHearingsMapping = CaseRoleHearingsMapping.CreateClaimantDefendantCaseRolesMapping();
            MappingList.Add(caseTypeMapping.CaseType, caseTypeMapping);
        }

        private void CreateCaseTypeApplicantRespondentMapping(CaseType caseType, List<HearingType> hearingTypeList)
        {
            var caseTypeMapping = new CaseTypesHearingsRoles
            {
                CaseType = caseType,
                HearingTypeList = hearingTypeList
            };

            caseTypeMapping.CaseRoleHearingsMapping = CaseRoleHearingsMapping.CreateApplicantRespondentCaseRolesMapping();
            MappingList.Add(caseTypeMapping.CaseType, caseTypeMapping);
        }
    }
}
