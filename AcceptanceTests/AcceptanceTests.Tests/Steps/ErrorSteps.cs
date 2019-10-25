using System;
using AcceptanceTests.Model;
using AcceptanceTests.Model.Type;
using AcceptanceTests.PageObject.Page;
using Coypu;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Tests.Steps
{
    [Binding]
    public class ErrorSteps
    {
        BrowserSession _driver;

        public ErrorSteps(BrowserSession driver)
        {
            _driver = driver;
        }

        [Then(@"I see a page with the '(.*)' message and the content below:")]
        public void ThenErrorMessageIsDisplayedAsYouAreNotAuthorisedToUseThisService(string messageType, string pageContent)
        {
            var parsedMessageType = EnumParser.ParseText<MessageType>(messageType);

            switch (parsedMessageType)
            {
                case MessageType.Unauthorised:
                    new UnauthorisedErrorPage(_driver).UnauthorisedText().Should().Be(pageContent);
                    break;
                default:
                    throw new NotSupportedException($"Message {messageType} is not currently supported");
            } 
        }

        [Then(@"I can follow the '(.*)' CTA on the screen")]
        public void ThenICanFollowTheCTAOnTheScreen(string ctaText)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
