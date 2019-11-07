
using System;
using AcceptanceTests.Driver.Support;

namespace AcceptanceTests.Driver.Settings
{
    public class SutSettings
    {

        private const string ServiceWebUserSecrets = "CF5CDD5E-FD74-4EDE-8765-2F899C252122";
        private const string AdminWebUserSecrets = "F99A3FE8-CF72-486A-B90F-B65C27DA84EE";
        private const string VideoWebUserSecrets = "C9281025-5048-4313-A2A6-EAB0D3CF5D6C";

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
                case SutSupport.VideoWebsite:
                    appSecret = ServiceWebUserSecrets;
                    break;
                default:
                    throw new Exception($"Couldn't find secrets ID for {targetApp} application.");
            }
            return appSecret;
        }
    }
}
