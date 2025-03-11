using OpenQA.Selenium;
using sauceLabs_PageObject.Pages;
using TechTalk.SpecFlow;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]
    public class CheckoutStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly Checkout1 c1;
        private readonly sauceLabs sl;

        public CheckoutStepDefinitions()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
            c1 = new Checkout1(driver);
            sl = new sauceLabs(driver);

        }

        [Given(@"The cart is opened")]
        public void GivenTheCartIsOpened()
        {
            //sl.launchbrowser();
            //Thread.Sleep(1500);
            //sl.login("standard_user", "secret_sauce");
            //sl.clickItem();
            //sl.addToCart();
            //Thread.Sleep(1500);
            //sl.openCart();

            Console.WriteLine("Cart is opened.");
        }

        [When(@"User clicks on checkout button")]
        public void WhenUserClicksOnCheckoutButton()
        {
            c1.Checkout();
            Console.WriteLine("Proceeding to checkout.");
        }

        [Then(@"checkout page will be opened")]
        public void ThenCheckoutPageWillBeOpened()
        {
            Console.WriteLine("Checkout page is displayed.");
        }

    }
}
