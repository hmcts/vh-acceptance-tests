﻿using System;
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

        public static readonly PartyRole ClaimantLip = new PartyRole("Claimant LIP");
        public static readonly PartyRole DefendantLip = new PartyRole("Defendant LIP");
        public static readonly PartyRole Solicitor = new PartyRole("Solicitor");
        public static readonly PartyRole Judge = new PartyRole("Judge");

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
                yield return DefendantLip;
                yield return ClaimantLip;
                yield return Solicitor;
                yield return Judge;
            }
        }
    }
}