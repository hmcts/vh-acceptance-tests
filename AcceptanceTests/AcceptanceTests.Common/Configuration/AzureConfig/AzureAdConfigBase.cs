﻿namespace AcceptanceTests.Common.Configuration.AzureConfig
{
    public class AzureAdConfigBase : IAzureAdConfig
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string TenantId { get; set; }
    }
}