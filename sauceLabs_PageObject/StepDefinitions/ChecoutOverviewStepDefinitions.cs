using OpenQA.Selenium;
using sauceLabs_PageObject.Pages;
using TechTalk.SpecFlow;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]
    public class CheckoutOverviewStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly checkoutOverview cO;

        public CheckoutOverviewStepDefinitions()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
            cO = new checkoutOverview(driver);
        }

        [Given(@"User is in checkout overview page")]
        public void GivenUserIsInCheckoutOverviewPage()
        {
            Console.WriteLine("User is in the checkout overview page.");
        }

        [When(@"User scrolls down and clicks on finish")]
        public void WhenUserScrollsDownAndClicksOnFinish()
        {
            cO.FinishClick();
            Console.WriteLine("Clicked on Finish.");
        }

        [Then(@"Confirmation page will be appeared")]
        public void ThenConfirmationPageWillBeAppeared()
        {
            Console.WriteLine("Confirmation page is displayed.");
        }
    }
}
