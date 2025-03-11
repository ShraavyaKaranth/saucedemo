using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using sauceLabs_PageObject.Pages;
using TechTalk.SpecFlow;
using sauceLabs_PageObject.Hooks;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]
    public class SauceLabsStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly sauceLabs sl;

        public SauceLabsStepDefinitions(ScenarioContext scenarioContext)
        {
            driver = scenarioContext["WebDriver"] as IWebDriver;
            sl = new sauceLabs(driver);
        }
        [Given(@"User is on the login page")]
        public void GivenUserIsOnTheLoginPage()
        {
            sl.launchbrowser();
            Thread.Sleep(1500);
            Console.WriteLine("User is on login page");
        }

        [When(@"User enters username and password and logs in")]
        public void WhenUserEntersUsernameAndPasswordAndLogsIn()
        {
            sl.login("standard_user", "secret_sauce");
            Thread.Sleep(1500);
        }

        [When(@"Views an item")]
        public void WhenViewsAnItem()
        {
            sl.clickItem();
            Thread.Sleep(1500);
        }

        [When(@"Adds item to cart")]
        public void WhenAddsItemToCart()
        {
            sl.addToCart();
            Thread.Sleep(1500);
        }

        [When(@"Opens cart")]
        public void WhenOpensCart()
        {
            sl.openCart();
            Thread.Sleep(1500);
        }

        [Then(@"Item added to cart shows up")]
        public void ThenItemAddedToCartShowsUp()
        {
            Console.WriteLine("cart opened");
        }

        [Given(@"User is viewing items in the cart")]
        public void GivenUserIsViewingItemsInTheCart()
        {
            Console.WriteLine("Items in cart are shown");
            sl.launchbrowser();
            Thread.Sleep(1500);
            sl.login("standard_user", "secret_sauce");
            Thread.Sleep(1500);
            sl.clickItem();
            Thread.Sleep(1500);
            sl.addToCart();
            Thread.Sleep(1500);
            sl.openCart();
            Thread.Sleep(1500);
        }

        [When(@"User clicks checkout")]
        public void WhenUserClicksCheckout()
        {
            sl.checkout();
            Thread.Sleep(1500);
        }

        [When(@"User enters firstname, lastname and postal code")]
        public void WhenUserEntersFirstnameLastnameAndPostalCode()
        {
            sl.checkOutDetails("Shravya", "Karanth", "575005");
            Thread.Sleep(1500);
        }

        [When(@"User clicks on continue")]
        public void WhenUserClicksOnContinue()
        {
            sl.checkoutContinue();
            Thread.Sleep(1500);
        }

        [When(@"OUser clicks on finish")]
        public void WhenOUserClicksOnFinish()
        {
            sl.finishClick();
            Thread.Sleep(1500);
        }

        [Then(@"Order confirmation page shows up")]
        public void ThenOrderConfirmationPageShowsUp()
        {
            sl.confirmationPage();
        }
    }
}
