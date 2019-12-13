using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AdminWebsite.Common.Model.Participant
{
    [JsonObject]
    public class Party
    {
        public string Name { get; }

        [JsonConstructor]
        private Party(string name)
        {
            Name = name;
        }

        public static Party Claimant = new Party("Claimant");
        public static Party Defendant = new Party("Defendant");
        public static Party Judge = new Party("Judge");


        public static string ToString(Party hearingType)
        {
            return hearingType.Name;
        }

        public static Party FromString(string name)
        {
            foreach (var item in Values)
            {
                if (item.Name.ToLower().Equals(name.ToLower().Trim()))
                {
                    return item;
                }
            }

            throw new ArgumentOutOfRangeException($"No party type found with name '{name}'");
        }

        private static IEnumerable<Party> Values
        {
            get
            {
                yield return Defendant;
                yield return Claimant;
                yield return Judge;
            }
        }
    }
}
