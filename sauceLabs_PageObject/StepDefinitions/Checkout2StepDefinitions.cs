using OpenQA.Selenium;
using sauceLabs_PageObject.Pages;
using TechTalk.SpecFlow;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]
    public class Checkout2StepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly Checkout2 c2;

        public Checkout2StepDefinitions()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
            c2 = new Checkout2(driver);
        }

        [Given(@"user is in check out page")]
        public void GivenUserIsInCheckOutPage()
        {
            Console.WriteLine("user is in checkout page");
            Thread.Sleep(4000);
        }

        
        [When(@"User enters firstname, lastname and postalcode and clicks on continue button")]
        public void WhenUserEntersFirstnameLastnameAndPostalcodeAndClicksOnContinueButton()
        {
            c2.EnterCheckoutDetails("Shravya", "Karanth", "575005");
            Console.WriteLine("Entered checkout details.");
            c2.CheckoutContinue();
            Console.WriteLine("Clicked on Continue.");
        }


        [Then(@"User is navigated to checkout overview page")]
        public void ThenUserIsNavigatedToCheckoutOverviewPage()
        {
            Console.WriteLine("Navigated to checkout overview page.");
        }
    }
}
