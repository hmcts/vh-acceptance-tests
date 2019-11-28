using System;
using System.Linq;
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
            var targetPageType = userJourney.Pages.Last().GetType();

            foreach (var page in userJourney.Pages)
            {
                CurrentPage = page;
                var headingToPrint = string.IsNullOrEmpty(CurrentPage.HeadingText) ? CurrentPage.Uri : CurrentPage.HeadingText;

                if (CurrentPage.GetType().IsEquivalentTo(typeof(DashboardPage)))
                {
                    CurrentPage.IsPageLoaded();
                    ChooseDashboardFlow(userJourney.DashboardOption);
                }
                else if (CurrentPage.GetType().IsEquivalentTo(targetPageType))
                {
                    Console.WriteLine($"Reached target page {headingToPrint}");
                    return;
                }
                else
                {
                    //try
                    //{
                        Console.WriteLine($"Current page: {headingToPrint}");
                        CurrentPage.FillDetails(default);
                        CurrentPage.Continue();
                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine($"Exception occurred {ex.Message}");
                    //    throw(ex);
                    //    /*Console.WriteLine("Page has no forms to be filled.");
                    //    Console.WriteLine("Continuing to next page.");
                    //    CurrentPage.Continue();}
                } 
            }
        }

        private static void ChooseDashboardFlow(DashboardOption option)
        {
            switch(option)
            {
                case DashboardOption.BookAvideoHearing:
                    var dashboardPage = (DashboardPage)CurrentPage;
                    dashboardPage.BookHearing();
                    break;
                default:
                    throw new NotImplementedException($"Dashboard option {option} is not implemented.");
            }
        }
    }
}
