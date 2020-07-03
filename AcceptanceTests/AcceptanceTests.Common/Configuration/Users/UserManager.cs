using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTests.Common.Model.UserRole;

namespace AcceptanceTests.Common.Configuration.Users
{
    public static class UserManager
    {
        public static UserAccount GetDefaultParticipantUser(List<UserAccount> userAccounts)
        {
            return userAccounts.First(x => x.DefaultParticipant.Equals(true));
        }

        public static UserAccount GetUserFromDisplayName(List<UserAccount> userAccounts, string displayName)
        {
            return userAccounts.First(x => x.DisplayName.ToLower().Contains(displayName.ToLower().Replace(" ", "")));
        }

        public static UserAccount GetJudgeUser(List<UserAccount> userAccounts)
        {
            return userAccounts.First(x => x.Role.StartsWith("Judge"));
        }

        public static UserAccount GetClerkUser(List<UserAccount> userAccounts)
        {
            return userAccounts.First(x => x.Role.StartsWith("Clerk"));
        }

        public static UserAccount GetCaseAdminUser(List<UserAccount> userAccounts)
        {
            return userAccounts.First(x => x.Role.StartsWith("Case admin"));
        }

        public static UserAccount GetVideoHearingOfficerUser(List<UserAccount> userAccounts)
        {
            return userAccounts.First(x => x.Role.StartsWith("Video Hearings Officer"));
        }

        public static List<UserAccount> GetIndividualUsers(List<UserAccount> userAccounts)
        {
            return userAccounts.Where(x => x.Role.StartsWith("Individual")).ToList();
        }

        public static List<UserAccount> GetRepresentativeUsers(List<UserAccount> userAccounts)
        {
            return userAccounts.Where(x => x.Role.StartsWith("Representative")).ToList();
        }

        public static List<UserAccount> GetNonClerkParticipantUsers(List<UserAccount> userAccounts)
        {
            return userAccounts.Where(x => x.Role.StartsWith("Individual") || x.Role.StartsWith("Representative")).ToList();
        }

        public static UserAccount GetIndividualNotInHearing(List<UserAccount> userAccounts, List<string> userLastNamesInTheHearing)
        {
            var individualUsers = GetIndividualUsers(userAccounts);
            foreach (var user in individualUsers.Where(user => !userLastNamesInTheHearing.Any(x => x.ToLower().Equals(user.Lastname.ToLower()))))
            {
                return user;
            }
            throw new DataMisalignedException("All individual users are assigned in the hearing.");
        }

        public static UserAccount GetObserverUser(List<UserAccount> userAccounts)
        {
            return userAccounts.First(x => x.Lastname.StartsWith("Observer"));
        }

        public static UserAccount GetPanelMemberUser(List<UserAccount> userAccounts)
        {
            return userAccounts.First(x => x.Lastname.StartsWith("PanelMember"));
        }

        public static UserAccount GetUserFromRole(List<UserAccount> userAccounts, UserRole role)
        {
            return userAccounts.First(x => x.Role.ToLower().Equals(role.ToString().ToLower()));
        }
    }
}
