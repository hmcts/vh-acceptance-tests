using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;

namespace AcceptanceTests.Driver.Settings
{
    public class SutSettings
    {

        private const string ServiceWebUserSecrets = "CF5CDD5E-FD74-4EDE-8765-2F899C252122";
        private const string AdminWebUserSecrets = "F99A3FE8-CF72-486A-B90F-B65C27DA84EE";

        public static string GetTargetAppSecret(SutSupport targetApp)
        {
            string appSecret = "";
            switch (targetApp)
            {
                case SutSupport.AdminWebsite:
                    appSecret = AdminWebUserSecrets;
                    break;
                case SutSupport.ServiceWebsite:
                    appSecret = ServiceWebUserSecrets;
                    break;
            }
            return appSecret;
        }
    }
}
