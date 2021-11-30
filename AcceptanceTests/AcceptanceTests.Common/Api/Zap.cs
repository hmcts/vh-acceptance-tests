using AcceptanceTests.Common.Api.Clients;
using AcceptanceTests.Common.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static AcceptanceTests.Common.Api.Clients.ZapApi;

namespace AcceptanceTests.Common.Api
{
    public static class Zap
    {

        private static string Configuration => string.IsNullOrEmpty(ZapConfiguration.BuildConfigMode) ? "Release" : ZapConfiguration.BuildConfigMode;

        private const string DockerCompose = "docker-compose";

        private static readonly string DockerArguments = $"-f docker-compose.yml -f docker-compose.test.yml -p {ZapConfiguration.ServiceName}";

        private static readonly TimeSpan TestTimeout = TimeSpan.FromSeconds(60);

        private static HttpClientHandler HttpClientHandler => new HttpClientHandler { Proxy = WebProxy };

        private static ZapConfiguration ZapConfiguration => new ConfigurationBuilder()
                                                            .AddJsonFile("appsettings.json")
                                                            .Build()
                                                            .GetSection("ZapConfiguration")
                                                            .Get<ZapConfiguration>();

        private static readonly ZapApi ZapApi = new ZapApi(ZapConfiguration);

        public static IWebProxy WebProxy => (ZapConfiguration.ZapScan || ZapConfiguration.SetUpProxy) ? new WebProxy($"http://{ZapConfiguration.ApiAddress}:{ZapConfiguration.ApiPort}", false) : null;

        public static bool SetupProxy => (ZapConfiguration.ZapScan || ZapConfiguration.SetUpProxy);

        private static string WorkingDirectory => Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()))));

        private static bool setToken = false;

        public static void Start()
        {
            if (!ZapConfiguration.ZapScan) return;

            if(!ZapConfiguration.SkipPublish)
            {
                Build();
            }

            StartContainers();

            var zapStarted = WaitForZap().Result;

            if (!zapStarted)
            {
                throw new Exception($"Zap startup failed after trying for '{TestTimeout}' ");
            }
            else
            {
                ZapApi.EnablePassiveScan(true);
                ZapApi.EnableAllPassiveScanners();
                ZapApi.SetMode();
                ZapApi.EnableAllActiveScanners();
            }

            var started = WaitForService().Result;

            if (!started)
            {
                throw new Exception($"Application service startup failed after trying for '{TestTimeout}'");
            }
        }

        public static void SetAuthToken(string bearerToken)
        {
            if (!ZapConfiguration.ZapScan) return;

            if (!setToken)
            {
                var ruleDescription = "Auth";
                var replacers = ZapApi.GetRules();
                if (replacers != null && replacers.Rules.Any(r => r.Description == ruleDescription))
                {
                    ZapApi.RemoveRule(ruleDescription);                     
                }
                ZapApi.AddRule(ruleDescription, "Authorization", bearerToken);

                setToken = true;
            }
        }

        public static void ReportAndShutDown(string reportFileName, string scanUrl)
        {
            if (!ZapConfiguration.ZapScan) return;

            try
            {
                PollPassiveScanCompletion();

                if (!string.IsNullOrEmpty(scanUrl) && ZapConfiguration.ActiveScan)
                {
                    Scan(scanUrl);
                }

                if (!string.IsNullOrEmpty(reportFileName))
                {
                    reportFileName = $"{reportFileName}-Tests-Security-{DateTime.Now:dd-MMM-yyyy-hh-mm-ss}";
                    WriteHtmlReport(reportFileName);
                    WriteXmlReport(reportFileName);
                }
            }
            finally
            {
                StopContainers();
            }
        }

        public static void Scan(string target)
        {
            StartSpidering(target);
            ActiveScan(target);
        }

        private static void StartSpidering(string target)
        {
            try
            {
                PollScanStatus(target, ScanType.Spider);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on running spider scan at '{target}' : {e.Message}");
            }
        }

        private static void ActiveScan(string url)
        {
            try
            { 
                PollScanStatus(url, ScanType.Active);
            }
            catch (Exception e)
            {
                throw new Exception($"Error on running active scan at '{url}' : {e.Message}");
            }
        }

        /* This method can be removed since the up most method is not being called from anywhere */

        private static void PollScanStatus(string url, ScanType scan)
        {
            var scanid = ZapApi.Scan(scan, url);

            int progress;
            while (true)
            {
                //Thread.Sleep(2000);   /* unable to see why waiting for 2 seconds on each iteration is required or significant */
                var value = ZapApi.ScanStatus(scan,scanid);
                progress = Convert.ToInt32(value);
                if (progress >= 100)
                {
                    break;
                }
            }
        }

        private static void PollPassiveScanCompletion()
        {
            while (true)
            {
                //Thread.Sleep(1000);   /* unable to see why waiting for 2 seconds on each iteration is required or significant */
                var value = ZapApi.RecordsToScan();
                if (!string.IsNullOrEmpty(value) && value == "0")
                    break;
            }
        }

        private static void Build()
        {
            var processStartInfo = CreateProcess("dotnet", $"publish --configuration {Configuration}", $"{WorkingDirectory}\\{ZapConfiguration.SolutionFolderName}");

            RunProcess(processStartInfo);
        }

        private static void StartContainers()
        {
            var processStartInfo = CreateProcess(DockerCompose, $"{DockerArguments} up -d --build");

            RunProcess(processStartInfo);
        }

        private static ProcessStartInfo CreateProcess(string fileName, string arguments, string workingDirectory = null)
        {
            return new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                WorkingDirectory = !string.IsNullOrEmpty(workingDirectory) ? workingDirectory : WorkingDirectory
            };
        }

        private static void StopContainers()
        {
            var processStartInfo = CreateProcess(DockerCompose, $"{DockerArguments} down --rmi local");

            RunProcess(processStartInfo);
        }

        private static void RunProcess(ProcessStartInfo processStartInfo)
        {
            processStartInfo.Environment["CONFIGURATION"] = Configuration;

            var process = Process.Start(processStartInfo);
            
            process.WaitForExit();

            if(process.ExitCode != 0)
            {
                throw new Exception($"Error running cmd {processStartInfo.FileName} {processStartInfo.Arguments} {processStartInfo.WorkingDirectory}");
            }
        }

        private static async Task<bool> WaitForService()
        {
            var testServiceUrl = $"https://{ZapConfiguration.ServiceName}/swagger/index.html";

            using var client = new HttpClient(HttpClientHandler);
            return await PollApi(client, testServiceUrl);
        }

        private static async Task<bool> WaitForZap()
        {
            var zapUrl = $"http://{ZapConfiguration.ApiAddress}:{ZapConfiguration.ApiPort}";

            using var client = new HttpClient();
            return await PollApi(client, zapUrl);
        }

        private static async Task<bool> PollApi(HttpClient client, string url)
        {
            client.Timeout = TimeSpan.FromSeconds(1);

            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < TestTimeout)
            {
                try
                {
                    var response = await client.GetAsync(new Uri(url)).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                catch
                {

                }

                await Task.Delay(1000).ConfigureAwait(false);
            }

            return false;
        }

        private static void WriteHtmlReport(string reportFileName)
        {
            File.WriteAllBytes(reportFileName + ".html", ZapApi.HtmlReport());
        }

        private static void WriteXmlReport(string reportFileName)
        {
            File.WriteAllBytes(reportFileName + ".xml", ZapApi.XmlReport());
        }

    }
}

