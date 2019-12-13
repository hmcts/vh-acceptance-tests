﻿using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace AcceptanceTests.Common.Driver.Strategies
{
    public abstract class Drivers
    {
        internal TimeSpan IdleTimeout { get; set; }
        internal Dictionary<string, object> SauceOptions { get; set; }
        internal Uri Uri { get; set; }
        internal TimeSpan SauceLabsTimeout { get; set; }
        internal TimeSpan LocalTimeout { get; set; }
        internal string MacPlatform { get; set; }
        internal bool UseVideoFiles { get; set; }
        internal string BuildPath { get; set; }
        internal string Filename { get; set; }

        public abstract RemoteWebDriver InitialiseForSauceLabs();
        public abstract IWebDriver InitialiseForLocal();
    }
}
