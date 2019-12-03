namespace AcceptanceTests.Common.Model.Hearing
{
    public class HearingType
    {

        public string SelectedHearingType { get; set; }

        //blank spaces are intentional - DO NOT CHANGE unless the value in the corresponding dropdown list changes
        public static string ApplicationToSetJudgmentAside = " Application to Set Judgment Aside ";
        public static string FirstDirectionsAppointment = " First Directions Hearing ";
        public static string Hearing = " Hearing ";
        public static string FirstHearing = " First Application ";
        public static string SubstantiveHearing = " Substantive Hearing ";
        public static string CaseManagement = " Case Management Hearing ";
        public static string DirectionsHearing = " Directions Hearing ";
        public static string FinalHearing = " Final Hearing ";
    }
}
