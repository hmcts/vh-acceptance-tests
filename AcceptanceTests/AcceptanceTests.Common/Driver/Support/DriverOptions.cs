namespace AcceptanceTests.Common.Driver.Support
{
    public class DriverOptions
    {
        public int LocalCommandTimeoutInSeconds { get; set; } = 20;
        public TargetBrowser TargetBrowser { get; set; }
        public TargetDevice TargetDevice { get; set; }
    }
}
