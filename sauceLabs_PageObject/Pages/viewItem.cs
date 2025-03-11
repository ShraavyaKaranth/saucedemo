using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace sauceLabs_PageObject.Pages
{
    public class ViewItem : LoginPage
    {
        private readonly By item = By.XPath("//img[@alt='Sauce Labs Backpack']");

        public ViewItem(IWebDriver driver) : base(driver) { }

        public void ClickItem()
        {
            wait.Until(d => d.FindElement(item).Displayed);
            driver.FindElement(item).Click();
        }
    }
}



//using OpenQA.Selenium;

//namespace sauceLabs_PageObject.Pages
//{
//    public class ViewItem : BasePage
//    {
//        public ViewItem(IWebDriver driver) : base(driver) { }

//        private readonly By item = By.XPath("//img[@alt='Sauce Labs Backpack']");

//        public AddToCart ClickItem()
//        {
//            wait.Until(d => d.FindElement(item).Displayed);
//            driver.FindElement(item).Click();

//            return new AddToCart(driver); // Navigate to AddToCart page
//        }
//    }
//}
