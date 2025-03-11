using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace sauceLabs_PageObject.Pages
{
    public class checkoutOverview : Checkout2
    {
        private readonly By finishButton = By.Id("finish");

        public checkoutOverview(IWebDriver driver) : base(driver) { }

        public void FinishClick()
        {
            Thread.Sleep(2000);
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.PageDown).Perform();
            Thread.Sleep(1500);
            driver.FindElement(finishButton).Click();
        }
    }
}


//using OpenQA.Selenium;`
//using OpenQA.Selenium.Interactions;

//namespace sauceLabs_PageObject.Pages
//{
//    public class checkoutOverview : BasePage
//    {
//        public checkoutOverview(IWebDriver driver) : base(driver) { }

//        private readonly By finishButton = By.Id("finish");

//        public confirmationPage FinishClick()
//        {
//            Actions actions = new Actions(driver);
//            actions.SendKeys(Keys.PageDown).Perform();
//            driver.FindElement(finishButton).Click();
//            return new confirmationPage(driver); // Moving to next page
//        }
//    }
//}
