using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace sauceLabs_PageObject.Pages
{
    public class AddToCart : ViewItem
    {
        private readonly By addItemToCart = By.Id("add-to-cart");
        private readonly By cartLink = By.ClassName("shopping_cart_link");

        public AddToCart(IWebDriver driver) : base(driver) { }

        public void AddItemToCart()
        {
            wait.Until(d => d.FindElement(addItemToCart).Displayed);
            driver.FindElement(addItemToCart).Click();
        }

        public void OpenCart()
        {
            wait.Until(d => d.FindElement(cartLink).Displayed);
            driver.FindElement(cartLink).Click();
        }
    }
}



//using OpenQA.Selenium;

//namespace sauceLabs_PageObject.Pages
//{
//    public class AddToCart : BasePage
//    {
//        public AddToCart(IWebDriver driver) : base(driver) { }

//        private readonly By addItemToCart = By.Id("add-to-cart");
//        private readonly By cartLink = By.ClassName("shopping_cart_link");

//        public AddToCart AddItemToCart()
//        {
//            wait.Until(d => d.FindElement(addItemToCart).Displayed);
//            driver.FindElement(addItemToCart).Click();
//            return this; // Staying on same page
//        }

//        public Checkout1 OpenCart()
//        {
//            wait.Until(d => d.FindElement(cartLink).Displayed);
//            driver.FindElement(cartLink).Click();
//            return new Checkout1(driver); // Moving to next page
//        }
//    }
//}
