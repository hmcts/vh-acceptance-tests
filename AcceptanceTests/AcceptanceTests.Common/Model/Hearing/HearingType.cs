using System;
using System.Collections.Generic;

namespace AcceptanceTests.Common.Model.Hearing
{
    public class HearingType
    {
        public string Name { get; }

        private HearingType(string name)
        {
            Name = name;
        }

        public static readonly HearingType ApplicationToSetJudgmentAside = new HearingType("Application to Set Judgment Aside");
        public static readonly HearingType AutomatedTest = new HearingType("Automated Test");
        public static readonly HearingType Hearing = new HearingType("Hearing");
        public static readonly HearingType FirstDirectionsAppointment = new HearingType("First Directions Hearing");
        public static readonly HearingType FirstApplication = new HearingType("First Application");
        public static readonly HearingType SubstantiveHearing = new HearingType("Substantive Hearing");
        public static readonly HearingType CaseManagement = new HearingType("Case Management");
        public static readonly HearingType DirectionsHearing = new HearingType("Directions Hearing");
        public static readonly HearingType FinalHearing = new HearingType("Final Hearing");

        public static string ToString(HearingType hearingType)
        {
            return hearingType.Name;
        }

        public static HearingType FromString(string name)
        {
            foreach (var hearingType in Values)
            {
                if (hearingType.Name.ToLower().Equals(name.ToLower().Trim()))
                {
                    return hearingType;
                }
            }

            throw new ArgumentOutOfRangeException($"No hearing type found with name '{name}'");
        }

        private static IEnumerable<HearingType> Values
        {
            get
            {
                yield return ApplicationToSetJudgmentAside;
                yield return AutomatedTest;
                yield return Hearing;
                yield return FirstDirectionsAppointment; 
                yield return FirstApplication;
                yield return SubstantiveHearing;
                yield return CaseManagement;
                yield return DirectionsHearing; 
                yield return FinalHearing;
            }
        }
    }
}