using OpenQA.Selenium;
using sauceLabs_PageObject.Pages;
using TechTalk.SpecFlow;
using sauceLabs_PageObject.Utils;
using NUnit.Framework;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]
    public class AddToCartStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly AddToCart addToCart;
        private readonly sauceLabs sl;


        public AddToCartStepDefinitions()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
            addToCart = new AddToCart(driver);
            sl = new sauceLabs(driver);

        }
        [Given(@"User is viewing an item")]
        public void GivenUserIsViewingAnItem()
        {
            //sl.launchbrowser();
            //Thread.Sleep(1500);
            //sl.login("standard_user", "secret_sauce");
            //sl.clickItem();
            Console.WriteLine("User is viewing item");
        }

        [When(@"User adds item to cart and clicks on cart button")]
        public void WhenUserAddsItemToCartAndClicksOnCartButton()
        {
            addToCart.AddItemToCart();
            Console.WriteLine("Item added to cart.");
            addToCart.OpenCart();
            Console.WriteLine("Cart opened.");
        }


        [Then(@"Cart will be opened")]
        public void ThenCartWillBeOpened()
        {
            Console.WriteLine("Cart page is displayed.");
        }
    }
}
