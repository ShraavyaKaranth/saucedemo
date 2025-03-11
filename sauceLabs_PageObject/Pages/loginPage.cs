using OpenQA.Selenium;
using sauceLabs_PageObject.Utils;

namespace sauceLabs_PageObject.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver) { }

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(config.BaseUrl);
        }

        public void Login(string username, string password)
        {
            driver.FindElement(By.Id("user-name")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);
            driver.FindElement(By.Id("login-button")).Click();
        }
    }
}


//using OpenQA.Selenium;
//using sauceLabs_PageObject.Utils;

//namespace sauceLabs_PageObject.Pages
//{
//    public class Login : BasePage
//    {
//        public Login(IWebDriver driver) : base(driver) { }

//        private readonly By usernameField = By.Id("user-name");
//        private readonly By passwordField = By.Id("password");
//        private readonly By loginButton = By.Id("login-button");

//        public Login GoToPage()
//        {
//            driver.Navigate().GoToUrl(config.BaseUrl);
//            return this; // Method chaining
//        }

//        public ViewItem login(string username, string password)
//        {
//            driver.FindElement(usernameField).SendKeys(username);
//            driver.FindElement(passwordField).SendKeys(password);
//            driver.FindElement(loginButton).Click();

//            return new ViewItem(driver); // Navigate to ViewItem page
//        }
//    }
//}
