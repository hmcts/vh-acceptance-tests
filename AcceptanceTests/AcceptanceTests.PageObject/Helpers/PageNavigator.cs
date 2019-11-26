using System;
using AcceptanceTests.Model.Page;
using AcceptanceTests.PageObject.Pages.AdminWebsite;
using AcceptanceTests.PageObject.Pages.Common;

namespace AcceptanceTests.PageObject.Helpers
{
    public class PageNavigator
    {
        public static UserJourneyPage CurrentPage { get; set; }

        public static void CompleteJourney(UserJourney userJourney)
        {
            foreach (var page in userJourney.Pages)
            {
                CurrentPage = page;

                if (CurrentPage.GetType().IsEquivalentTo(typeof(DashboardPage)))
                {
                    switch (userJourney.DashboardOption)
                    {
                        case DashboardOption.BookAvideoHearing:
                            var dashboardPage = (DashboardPage)CurrentPage;
                            dashboardPage.BookHearing();
                            break;
                        default:
                            throw new NotImplementedException($"Dashboard option {userJourney.DashboardOption} is not implemented.");
                    }
                } else
                {
                    try
                    {
                        var headingToPrint = string.IsNullOrEmpty(CurrentPage.HeadingText) ? CurrentPage.Uri : CurrentPage.HeadingText;
                        Console.WriteLine($"Current page: {headingToPrint}");
                        CurrentPage.FillDetails(default);
                        CurrentPage.Continue();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception occurred {ex.Message}");
                        Console.WriteLine("Page has no forms to be filled.");
                        Console.WriteLine("Continuing to next page.");
                        CurrentPage.Continue();
                    }
                } 
            }
        }
    }
}
