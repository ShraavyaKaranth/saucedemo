using OpenQA.Selenium;

namespace sauceLabs_PageObject.Pages
{
    public class Checkout1 : AddToCart
    {
        private readonly By checkOut = By.XPath("//button[@id='checkout']");

        public Checkout1(IWebDriver driver) : base(driver) { }

        public void Checkout()
        {
            driver.FindElement(checkOut).Click();
        }
    }
}


//using OpenQA.Selenium;

//namespace sauceLabs_PageObject.Pages
//{
//    public class Checkout1 : BasePage
//    {
//        public Checkout1(IWebDriver driver) : base(driver) { }

//        private readonly By checkOut = By.XPath("//button[@id='checkout']");

//        public Checkout2 Checkout()
//        {
//            driver.FindElement(checkOut).Click();
//            return new Checkout2(driver); // Moving to next page
//        }
//    }
//}

