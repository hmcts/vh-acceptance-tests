using System.Collections.Generic;
using AdminWebsite.Common.Model.Hearing;

namespace AdminWebsite.Common.Model.Mappings
{
    public class CaseTypeHearingTypes
    {
        public static List<HearingType> GetCivilMoneyClaimsHearingTypes()
        {
            var list = new List<HearingType> {
                    HearingType.ApplicationToSetJudgmentAside,
                    HearingType.FirstApplication,
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
                    HearingType.FirstApplication,
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
                    HearingType.FirstApplication,
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
