using System;
using System.Collections.Generic;
using System.Text;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;

namespace AcceptanceTests.Common.Driver.Helpers
{
    class WaitHelper
    {
        public WebDriverWait newWait(NgWebDriver Driver)
        {
            NUnit.Framework.TestContext.WriteLine($"New Wait driver run.");
            WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(30))
            {
                PollingInterval = TimeSpan.FromSeconds(1),
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait;
        }
    }
}
