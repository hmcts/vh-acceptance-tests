using System;
using System.Collections.Generic;

namespace AcceptanceTests.Common.Model.Case
{
    public class CaseType
    {
        public int Id { get; }
        public string Name { get; }

        private CaseType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static readonly CaseType CivilMoneyClaims = new CaseType(1, "Civil Money Claims");
        public static readonly CaseType FinancialRemedy = new CaseType(2, "Financial Remedy");
        public static readonly CaseType Generic = new CaseType(3, "Generic");
        public static readonly CaseType ChildrenAct = new CaseType(4, "Children Act");
        public static readonly CaseType Tax = new CaseType(5, "Tax");
        public static readonly CaseType FamilyLawAct = new CaseType(6, "Family Law Act");
        public static readonly CaseType Tribunal = new CaseType(7, "Tribunal");

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
