using OpenQA.Selenium;
using sauceLabs_PageObject.Pages;
using sauceLabs_PageObject.Utils;
using TechTalk.SpecFlow;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly LoginPage loginPage;


        public LoginStepDefinitions()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
            loginPage = new LoginPage(driver); // Pass WebDriver to LoginPage
        }

        [Given(@"User is on login page")]
        public void GivenUserIsOnLoginPage()
        {
            loginPage.GoToPage();
        }

        [When(@"User enters username and password and clicks on login button")]
        public void WhenUserEntersUsernameAndPassword()
        {
            loginPage.Login("standard_user", "secret_sauce");
        }

        [Then(@"User is logged in")]
        public void ThenUserIsLoggedIn()
        {
            Console.WriteLine("User successfully logged in.");
        }
    }
}
