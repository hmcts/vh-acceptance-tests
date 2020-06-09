using System;
using System.Threading;
using AcceptanceTests.Common.Driver;
using AcceptanceTests.Common.Driver.Drivers;
using AcceptanceTests.Common.Driver.Helpers;
using FluentAssertions;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.Test.Helpers
{
    public class VerifyVideoIsPlayingBuilder
    {
        private static int _maxRetries = 5;
        private static int _delayBetweenPolling = 1;
        private static int _delayForVideoToAppear = 30;
        private readonly UserBrowser _browser;

        public VerifyVideoIsPlayingBuilder(UserBrowser browser)
        {
            _browser = browser;
        }

        public VerifyVideoIsPlayingBuilder Retries(int retries)
        {
            _maxRetries = retries;
            return this;
        }

        public VerifyVideoIsPlayingBuilder DelayBetweenPolling(int delay)
        {
            _delayBetweenPolling = delay;
            return this;
        }

        public VerifyVideoIsPlayingBuilder DelayForVideoElementToAppear(int delay)
        {
            _delayForVideoToAppear = delay;
            return this;
        }

        public void Feed(By element)
        {
            _browser.Driver.WaitUntilVisible(element, _delayForVideoToAppear);

            var playing = false;

            for (var i = 1; i <= _maxRetries; i++)
            {
                var currentTime = Convert.ToDouble(_browser.Driver.WaitUntilVisible(element)
                    .GetAttribute("currentTime"));
                if (currentTime > 0)
                {
                    playing = true;
                    break;
                }

                Thread.Sleep(TimeSpan.FromSeconds(_delayBetweenPolling));
            }

            playing.Should().BeTrue("video is playing");
        }
    }
}
