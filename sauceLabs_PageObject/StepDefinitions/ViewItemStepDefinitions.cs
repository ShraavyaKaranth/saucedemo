using OpenQA.Selenium;
using sauceLabs_PageObject.Pages;
using TechTalk.SpecFlow;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]

    public class ViewItemStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly ViewItem viewItem;

        public ViewItemStepDefinitions()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
            viewItem = new ViewItem(driver);
        }
        [Given(@"User is logged in")]
        public void GivenUserIsLoggedIn()
        {
            Console.WriteLine("User is logged in");
        }

        [When(@"User views an item")]
        public void WhenUserViewsAnItem()
        {
            viewItem.ClickItem();
            Console.WriteLine("Item clicked.");
        }

        [Then(@"Item data is displayed")]
        public void ThenItemDataIsDisplayed()
        {
            Console.WriteLine("Item data is displayed.");
        }
    }
}
