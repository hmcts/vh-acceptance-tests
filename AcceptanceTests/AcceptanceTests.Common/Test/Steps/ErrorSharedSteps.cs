using Protractor;

namespace AcceptanceTests.Common.Test.Steps
{
    public class ErrorSharedSteps
    {
        public void WhenTheUserAttemptsToNavigateToANonexistentPage(NgWebDriver driver, string baseUrl)
        {
            driver.Navigate().GoToUrl(AddSlashToUrlIfRequired(baseUrl));
        }

        private static string AddSlashToUrlIfRequired(string baseUrl)
        {
            return baseUrl[^1].Equals(char.Parse("/")) ? $"{baseUrl}non-existent-page" : $"{baseUrl}/non-existent-page";
        }
    }
}
