using AcceptanceTests.Common.Api.Helpers;
using AcceptanceTests.Common.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;
using System.Web;
using System.Xml;

namespace AcceptanceTests.Common.Api.Clients
{
    public class ZapApi
    {
        private RestClient client;
        private readonly string HtmlReportUrl = "/OTHER/core/other/htmlreport/";
        private readonly string XmlReportUrl = "/OTHER/core/other/xmlreport/";
        private readonly string FetchRulesUrl = "/JSON/replacer/view/rules/";
        private readonly string RemoveRuleUrl = "/JSON/replacer/action/removeRule/";
        private readonly string AddRuleUrl = "/JSON/replacer/action/addRule/";
        private readonly string RecordsToScanUrl = "/JSON/pscan/view/recordsToScan/";
        private readonly string SpiderScanStatusUrl = "/JSON/spider/view/status/";
        private readonly string ActiveScanStatusUrl = "/JSON/ascan/view/status/";
        private readonly string ActiveScanUrl = "/JSON/ascan/action/scan/";
        private readonly string SpiderScanUrl = "/JSON/spider/action/scan/";
        private readonly string EnablePassiveScanUrl = "/JSON/pscan/action/setEnabled/";
        private readonly string EnableAllScannerPassiveUrl = "/JSON/pscan/action/enableAllScanners/";
        private readonly string EnableAllScannerActiveUrl = "/JSON/ascan/action/enableAllScanners/";
        private readonly string SetModeUrl = "/JSON/core/action/setMode/";
        private readonly string ResultOk = "{\"Result\":\"OK\"}";
        private string _apiKey;
        public enum ScanType
        {
            Spider,
            Active,
            AjaxSpider
        }

        public ZapApi(ZapConfiguration zapConfiguration)
        {
            client = new RestClient($"https://{zapConfiguration.ApiAddress}:{zapConfiguration.ApiPort}/");
            client.AddDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml");
            client.RemoteCertificateValidationCallback = new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });

            _apiKey = $"?apikey={GetApiKey(zapConfiguration.ApiConfigPath)}";
        }

        public string Scan(ScanType scan, string url)
        {
            var tempUrl = "";
            var parameters = $"&url={HttpUtility.UrlEncode(url)}&recurse=true";
            switch (scan)
            {
                case ScanType.Spider:
                    tempUrl = SpiderScanUrl ;
                    parameters += "&maxChildren=&contextName=&subtreeOnly=";
                    break;
                case ScanType.Active:
                    tempUrl = ActiveScanUrl ;
                    parameters += "&inScopeOnly=&scanPolicyName=&method=&postData=&contextId=";
                    break;
            }
            return GetDictionaryValue(tempUrl, parameters, "scan");
        }

        public string ScanStatus(ScanType scan, string scanid)
        {
            var url = "";
            var parameters = $"&scanId={scanid}";
            switch (scan)
            {
                case ScanType.Spider: 
                    url = SpiderScanStatusUrl;
                    break;
                case ScanType.Active:
                    url = ActiveScanStatusUrl;
                    break;
            }
            return GetDictionaryValue(url, parameters, "status");
        }


        public string RecordsToScan()
        {
            return GetDictionaryValue(RecordsToScanUrl,"","recordsToScan");
        }

        private string GetDictionaryValue(string url, string parameters, string key)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(GetResponse(url,parameters))[key];
        }

        public ZapReplacerRuleResponse GetRules()
        {
            return JsonConvert.DeserializeObject<ZapReplacerRuleResponse>(GetResponse(FetchRulesUrl));
        }

        public bool SetMode(string mode)
        {
            var parameters = $"&mode={mode}";
            return GetResponse(EnableAllScannerActiveUrl, string.Empty).Contains(ResultOk);
        }

        public bool EnableAllActiveScanners(string scanPolicyName)
        {
            var parameters = $"&scanPolicyName={scanPolicyName}";
            return GetResponse(EnableAllScannerActiveUrl, string.Empty).Contains(ResultOk);
        }

        public bool EnableAllPassiveScanners()
        {
            return GetResponse(EnableAllScannerPassiveUrl, string.Empty).Contains(ResultOk);
        }

        public bool EnablePassiveScan(bool flag)
        {
            var parameters = $"&enabled={flag}";
            return GetResponse(EnablePassiveScanUrl, parameters).Contains(ResultOk);
        }

        public bool RemoveRule(string description)
        { 
           var parameters = $"&description={description}";
           return  GetResponse(RemoveRuleUrl, parameters).Contains(ResultOk);
        }

        public bool AddRule(string description,string ruleName,string bearerToken)
        {
            var parameters = $"&description={description}&enabled=true&matchType=REQ_HEADER&matchRegex=false&matchString={ruleName}&replacement=Bearer {bearerToken}&initiators=";
            return GetResponse(AddRuleUrl, parameters).Contains(ResultOk);
        }

        public byte[] HtmlReport()
        {
            return Encoding.ASCII.GetBytes(GetResponse(HtmlReportUrl));
        }

        public byte[] XmlReport()
        {            
            return Encoding.ASCII.GetBytes(GetResponse(XmlReportUrl));
        }

        private string GetResponse(string url, string parameters = "")
        {
            url += _apiKey + parameters;
            var response = client.Execute(GetRequest(url));
            return response.Content;
        }

        private RestRequest GetRequest(string url)
        {
            return new RestRequest(url, Method.GET);
        }

        private string GetApiKey(string configFile)
        {
            var doc = new XmlDocument();
            doc.Load(configFile);

            var node = doc.GetElementsByTagName("key");

            if (node.Count > 0 && node[0] != null)
                return node[0].InnerText;

            throw new Exception($"Unable to resolve api key from {configFile}");
        }

    }
}
