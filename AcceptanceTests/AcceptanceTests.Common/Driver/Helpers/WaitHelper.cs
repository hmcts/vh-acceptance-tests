using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Protractor;

namespace AcceptanceTests.Common.Driver.Helpers
{
    class WaitHelper
    {
        public WebDriverWait newWait(NgWebDriver Driver)
        {
            return newWait(Driver, 60);
        }
        public WebDriverWait newWait(NgWebDriver Driver, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(timeout))
            {
                PollingInterval = TimeSpan.FromSeconds(1),
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait;
        }
    }
}
