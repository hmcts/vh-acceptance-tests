using System;
using System.Collections.Generic;

namespace AdminWebsite.Common.Model.Case
{
    public class CaseType
    {
        public string Name { get; }

        private CaseType(string name)
        {
            Name = name;
        }

        public static readonly CaseType CivilMoneyClaims = new CaseType("Civil Money Claims");
        public static readonly CaseType FinancialRemedy = new CaseType("Financial Remedy");
        public static readonly CaseType Generic = new CaseType("Generic");
        public static readonly CaseType ChildrenAct = new CaseType("Children Act");
        public static readonly CaseType Tax = new CaseType("Tax");
        public static readonly CaseType FamilyLawAct = new CaseType("Family Law Act");
        public static readonly CaseType Tribunal = new CaseType("Tribunal");

        public static string ToString(CaseType hearingType)
        {
            return hearingType.Name;
        }

        public static CaseType FromString(string name)
        {
            foreach (var type in Values)
            {
                if (type.Name.ToLower().Equals(name.ToLower().Trim()))
                {
                    return type;
                }
            }

            throw new ArgumentOutOfRangeException($"No hearing type found with name '{name}'");
        }

        private static IEnumerable<CaseType> Values
        {
            get
            {
                yield return CivilMoneyClaims;
                yield return FinancialRemedy;
                yield return Generic;
                yield return ChildrenAct;
                yield return Tax;
                yield return FamilyLawAct;
                yield return Tribunal;
            }
        }
    }
}
