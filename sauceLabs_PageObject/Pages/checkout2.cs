using OpenQA.Selenium;

namespace sauceLabs_PageObject.Pages
{
    public class Checkout2 : Checkout1
    {
        private readonly By checkOutFirstName = By.XPath("//input[@id='first-name']");
        private readonly By checkOutLastName = By.XPath("//input[@id='last-name']");
        private readonly By checkOutPostalCode = By.XPath("//input[@id='postal-code']");
        private readonly By continueCheckOut = By.XPath("//input[@id='continue']");
        

        public Checkout2(IWebDriver driver) : base(driver) { }

        


        public void EnterCheckoutDetails(string firstName, string lastName, string postalCode)
        {
            Console.WriteLine("here");
            Thread.Sleep(2000);

            driver.FindElement(checkOutFirstName).SendKeys(firstName);
            driver.FindElement(checkOutLastName).SendKeys(lastName);
            driver.FindElement(checkOutPostalCode).SendKeys(postalCode);
        }

        public void CheckoutContinue()
        {
            driver.FindElement(continueCheckOut).Click();
        }
    }
}


//using OpenQA.Selenium;

//namespace sauceLabs_PageObject.Pages
//{
//    public class Checkout2 : BasePage
//    {
//        public Checkout2(IWebDriver driver) : base(driver) { }

//        private readonly By checkOutFirstName = By.XPath("//input[@id='first-name']");
//        private readonly By checkOutLastName = By.XPath("//input[@id='last-name']");
//        private readonly By checkOutPostalCode = By.XPath("//input[@id='postal-code']");
//        private readonly By continueCheckOut = By.XPath("//input[@id='continue']");


//        public checkoutOverview EnterCheckoutDetails(string firstName, string lastName, string postalCode)
//        {
//            Console.WriteLine("here");

//            driver.FindElement(checkOutFirstName).SendKeys(firstName);
//            driver.FindElement(checkOutLastName).SendKeys(lastName);
//            driver.FindElement(checkOutPostalCode).SendKeys(postalCode);
//            return new checkoutOverview(driver);
//        }

//        public checkoutOverview CheckoutContinue()
//        {
//            driver.FindElement(continueCheckOut).Click();
//            return new checkoutOverview(driver);
//        }
//    }
//}
