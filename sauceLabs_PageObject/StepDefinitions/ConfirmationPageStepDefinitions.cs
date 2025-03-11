using OpenQA.Selenium;
using sauceLabs_PageObject.Pages;
using TechTalk.SpecFlow;

namespace sauceLabs_PageObject.StepDefinitions
{
    [Binding]
    public class ConfirmationPageStepDefinitions
    {
        private readonly IWebDriver driver;
        private readonly ConfirmationPage cP;

        public ConfirmationPageStepDefinitions()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
            cP = new ConfirmationPage(driver);
        }

        [Given(@"User is in the confirmation page")]
        public void GivenUserIsInTheConfirmationPage()
        {
            Console.WriteLine("User is on the confirmation page.");
        }

        [When(@"User checks for confirmation text and compares it with actual text")]
        public void WhenUserChecksForConfirmationTextAndComparesItWithActualText()
        {
            cP.VerifyConfirmationPage();
            Console.WriteLine("Checked confirmation text.");
        }

        [Then(@"If it matches, the test case passes. Else it fails.")]
        public void ThenIfItMatchesTheTestCasePasses_ElseItFails()
        {
            Console.WriteLine("Test case validation completed.");
        }
    }
}
