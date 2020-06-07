using System;

namespace AcceptanceTests.Common.Driver.Drivers.Services
{
    internal interface IDriverService
    {
        Uri Start(); 
        void Stop();
    }
}
