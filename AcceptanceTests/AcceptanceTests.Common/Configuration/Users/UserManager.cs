using System.Collections.Generic;
using System.Linq;

namespace AcceptanceTests.Common.Configuration.Users
{
    public class UserManager
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
    }
}
