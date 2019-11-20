using System.Collections.Generic;
using AcceptanceTests.Model.Type;

namespace AcceptanceTests.Model.Mappings
{
    public class CaseTypeHearingTypes
    {
        public static List<HearingType> GetCivilMoneyClaimsHearingTypes()
        {
            var list = new List<HearingType> {
                    HearingType.ApplicationToSetJudgmentAside,
                    HearingType.FirstHearing,
                    HearingType.DirectionsHearing,
                    HearingType.CaseManagement,
                    HearingType.FinalHearing
                };
            return list;
        }

        public static List<HearingType> GetFinancialRemedyHearingTypes()
        {
            var list = new List<HearingType> {
                    HearingType.FirstDirectionsAppointment,
                    HearingType.FirstHearing,
                    HearingType.DirectionsHearing,
                    HearingType.CaseManagement,
                    HearingType.Hearing,
                    HearingType.FinalHearing
                };
            return list;
        }

        public static List<HearingType> GetTaxHearingTypes()
        {
            var list = new List<HearingType> {
                    HearingType.FirstHearing,
                    HearingType.SubstantiveHearing,
                    HearingType.CaseManagement,
                    HearingType.DirectionsHearing,
                    HearingType.Hearing,
                    HearingType.FinalHearing
                };
            return list;
        }
    }
}
