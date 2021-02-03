using System;
using System.Collections.Generic;

namespace AcceptanceTests.Common.Model.Participant
{
    public class PartyRole
    {
        public string Name { get; }

        private PartyRole(string name)
        {
            Name = name;
        }

        public static readonly PartyRole LitigantInPerson = new PartyRole("Litigant in person");
        public static readonly PartyRole Representative = new PartyRole("Representative");
        public static readonly PartyRole Judge = new PartyRole("Judge");
        public static readonly PartyRole Interpreter = new PartyRole("Interpreter");

        public static string ToString(PartyRole hearingType)
        {
            return hearingType.Name;
        }

        public static PartyRole FromString(string name)
        {
            foreach (var item in Values)
            {
                if (item.Name.ToLower().Equals(name.ToLower().Trim()))
                {
                    return item;
                }
            }

            throw new ArgumentOutOfRangeException($"No party role type found with name '{name}'");
        }

        private static IEnumerable<PartyRole> Values
        {
            get
            {
                yield return LitigantInPerson;
                yield return Representative;
                yield return Judge;
                yield return Interpreter;
            }
        }
    }
}
