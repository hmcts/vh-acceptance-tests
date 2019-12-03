using System;
using System.Linq;
using AcceptanceTests.Common.Model.Page;
using AcceptanceTests.Common.PageObject.Pages.Common;

namespace AcceptanceTests.Common.PageObject.Helpers
{
    public class PageNavigator
    {
        public static UserJourneyPage CurrentPage { get; set; }

        public static void CompleteJourney(UserJourney userJourney)
        {
            var targetPageType = userJourney.Pages.Last().GetType();

            foreach (var page in userJourney.Pages)
            {
                CurrentPage = page;
                var headingToPrint = string.IsNullOrEmpty(CurrentPage.HeadingText) ? CurrentPage.Uri : CurrentPage.HeadingText;

                if (CurrentPage.GetType().IsEquivalentTo(targetPageType))
                {
                    Console.WriteLine($"Reached target page {headingToPrint}");
                    return;
                }
                else
                {
                    try
                    {
                        Console.WriteLine($"Current page: {headingToPrint}");
                        CurrentPage.FillDetails(default);
                        CurrentPage.Continue();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Can't continue to next page.");
                        Console.WriteLine($"Exception occurred -> {ex.InnerException.Message}");
                        throw (ex);
                    }
                } 
            }
        }
    }
}
