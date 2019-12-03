using System.Collections.Generic;
using AcceptanceTests.Common.Model.Hearing;

namespace AcceptanceTests.Common.Model.Mappings
{
    public class CaseTypeHearingTypes
    {
        public static List<string> GetCivilMoneyClaimsHearingTypes()
        {
            var list = new List<string> {
                    HearingType.ApplicationToSetJudgmentAside,
                    HearingType.FirstHearing,
                    HearingType.DirectionsHearing,
                    HearingType.CaseManagement,
                    HearingType.FinalHearing
                };
            return list;
        }

        public static List<string> GetFinancialRemedyHearingTypes()
        {
            var list = new List<string> {
                    HearingType.FirstDirectionsAppointment,
                    HearingType.FirstHearing,
                    HearingType.DirectionsHearing,
                    HearingType.CaseManagement,
                    HearingType.Hearing,
                    HearingType.FinalHearing
                };
            return list;
        }

        public static List<string> GetTaxHearingTypes()
        {
            var list = new List<string> {
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
