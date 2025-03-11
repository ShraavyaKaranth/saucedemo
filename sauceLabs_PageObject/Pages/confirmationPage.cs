using OpenQA.Selenium;
using NUnit.Framework;

namespace sauceLabs_PageObject.Pages
{
    public class ConfirmationPage : checkoutOverview
    {
        private readonly By confirmationMessage = By.XPath("//h2[normalize-space()='Thank you for your order!']");

        public ConfirmationPage(IWebDriver driver) : base(driver) { }

        public void VerifyConfirmationPage()
        {
            Thread.Sleep(2000);
            string actualText = driver.FindElement(confirmationMessage).Text;
            Assert.That(actualText, Is.EqualTo("Thank you for your norder!"));
        }
    }
}


//using OpenQA.Selenium;
//using NUnit.Framework;

//namespace sauceLabs_PageObject.Pages
//{
//    public class confirmationPage : BasePage
//    {
//        public confirmationPage(IWebDriver driver) : base(driver) { }

//        private readonly By confirmationMessage = By.XPath("//h2[normalize-space()='Thank you for your order!']");

//        public confirmationPage VerifyConfirmationPage()
//        {
//            string actualText = driver.FindElement(confirmationMessage).Text;
//            Assert.That(actualText, Is.EqualTo("Thank you for your order!"));
//            return this; // Returning this if you want to continue using this object
//        }
//    }
//}
