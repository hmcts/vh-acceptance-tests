using System;

namespace AcceptanceTests.Common.Model.FormData
{
    public class HearingDateTime : IFormData
    {
        public string Date { get; set; }
        public string[] StartTime { get; set; }
        public string Duration { get; set; }

        public readonly string DefaultFormat = "dd-MM-yyyy";
        private const string ChromeEuHearingDateFormat = "MM-dd-yyyy";
        private const string FirefoxHearingDateFormat = "yyyy-MM-dd";

        private static string[] CurrentTime() => DateTime.Now.AddMinutes(30).ToString("HH:mm").Split(':');

        public string GetHearingScheduledDateFormat(string browser, bool runningWithSaucelabs)
        {
            if (browser.Contains("Chrome") && runningWithSaucelabs)
                return ChromeEuHearingDateFormat;
            return browser.Contains("Firefox") ? FirefoxHearingDateFormat : DefaultFormat;
        }

        public IFormData GenerateFake()
        {
            Console.WriteLine("Generating fake hearing schedule:");
            Date = DateTime.Now.ToString(DefaultFormat);
            Console.WriteLine($"Generated date {Date}");

            StartTime = CurrentTime();
            Console.WriteLine($"Generated start time {StartTime[0]}:{StartTime[1]}");

            Duration = $"{Faker.RandomNumber.Next(0, 23)}:{Faker.RandomNumber.Next(30, 59)}";
            Console.WriteLine($"Generated duration {Duration}");
            return this;
        }

        public IFormData GenerateFake(string browser, bool runningWithSaucelabs)
        {
            Console.WriteLine("Generating fake hearing schedule:");
            Date = DateTime.Now.ToString(GetHearingScheduledDateFormat(browser, runningWithSaucelabs));
            Console.WriteLine($"Generated date {Date}");

            StartTime = CurrentTime();
            Console.WriteLine($"Generated start time {StartTime[0]}:{StartTime[1]}");

            Duration = $"{Faker.RandomNumber.Next(0, 23)}:{Faker.RandomNumber.Next(30, 59)}";
            Console.WriteLine($"Generated duration {Duration}");
            return this;
        }
    }
}
