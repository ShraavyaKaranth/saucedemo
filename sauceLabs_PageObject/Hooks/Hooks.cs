using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using sauceLabs_PageObject.Utils; // ✅ Ensure correct namespace

namespace sauceLabs_PageObject.Hooks
{
    [Binding]
    public class Hooks
    {
        //private IWebDriver driver;
        private static IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private ExtentTest _scenario;
        private static ExtentSparkReporter _sparkReporter;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();

            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ExtentReport.html");
            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));

            _sparkReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_sparkReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void Setup()
        {
            TestContext.Progress.WriteLine("Initializing WebDriver...");

            // ✅ Ensure fresh WebDriver for each scenario
            //driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();

            // ✅ Store driver in ScenarioContext
            _scenarioContext["WebDriver"] = driver;

            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            string screenshotPath = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title, stepText);

            if (_scenarioContext.TestError == null)
            {
                if (screenshotPath != null)
                {
                    _scenario.Log(Status.Pass, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                else
                {
                    _scenario.Log(Status.Pass, stepText);
                }
            }
            else
            {
                if (screenshotPath != null)
                {
                    _scenario.Log(Status.Fail, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                }
                else
                {
                    _scenario.Log(Status.Fail, stepText);
                }

                _scenario.Log(Status.Fail, _scenarioContext.TestError.Message);
            }
        }

        [AfterScenario]
        public void TearDown()
        {
            TestContext.Progress.WriteLine("Closing WebDriver...");

            //if (driver != null)
            //{
            //    driver.Quit();
            //    driver.Dispose();  // ✅ Ensure cleanup
            //    driver = null;
            //}

            //// ✅ Reset WebDriver in WebDriverManager
            //sauceLabs_PageObject.Utils.WebDriverManager.QuitDriver();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose(); 
                driver = null;
            }

            sauceLabs_PageObject.Utils.WebDriverManager.QuitDriver();

            _extent.Flush();
        }

        private string CaptureScreenshot(string scenarioName, string stepName)
        {
            try
            {
                if (driver == null || driver.WindowHandles.Count == 0)
                {
                    TestContext.Progress.WriteLine("No active browser window. Skipping screenshot.");
                    return null;
                }

                Thread.Sleep(500);

                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                Directory.CreateDirectory(screenshotPath);

                string sanitizedStepName = string.Join("_", stepName.Split(Path.GetInvalidFileNameChars()));
                string filePath = Path.Combine(screenshotPath, $"{scenarioName}_{sanitizedStepName}.png");

                screenshot.SaveAsFile(filePath);
                TestContext.Progress.WriteLine($"Screenshot saved at: {filePath}");

                return filePath;
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"Failed to capture screenshot: {ex.Message}");
                return null;
            }
        }
    }
}




//using System;
//using System.IO;
//using System.Threading;
//using NUnit.Framework;
//using OpenQA.Selenium;
//using TechTalk.SpecFlow;
//using AventStack.ExtentReports;
//using AventStack.ExtentReports.Reporter;
//using WebDriverManager;
//using WebDriverManager.DriverConfigs.Impl;

//namespace sauceLabs_PageObject.Hooks
//{
//    [Binding]
//    public class Hooks
//    {
//        private static IWebDriver driver;
//        private readonly ScenarioContext _scenarioContext;
//        private static ExtentReports _extent;
//        private static ExtentTest _feature;
//        private ExtentTest _scenario;
//        private static ExtentSparkReporter _sparkReporter;

//        public Hooks(ScenarioContext scenarioContext)
//        {
//            _scenarioContext = scenarioContext;
//        }

//        [BeforeTestRun]
//        public static void BeforeTestRun()
//        {
//            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "ExtentReport.html");
//            Directory.CreateDirectory(Path.GetDirectoryName(reportPath));

//            _sparkReporter = new ExtentSparkReporter(reportPath);
//            _extent = new ExtentReports();
//            _extent.AttachReporter(_sparkReporter);
//        }

//        [BeforeFeature]
//        public static void BeforeFeature(FeatureContext featureContext)
//        {
//            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
//        }

//        [BeforeScenario]
//        public void Setup()
//        {
//            TestContext.Progress.WriteLine("Initializing WebDriver...");

//            if (driver == null)
//            {
//                driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
//            }

//            // ✅ Store WebDriver in ScenarioContext
//            _scenarioContext["WebDriver"] = driver;
//            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
//        }

//        [AfterStep]
//        public void InsertReportingSteps()
//        {
//            string stepText = _scenarioContext.StepContext.StepInfo.Text;
//            string screenshotPath = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title);

//            if (_scenarioContext.TestError == null)
//            {
//                _scenario.Log(Status.Pass, stepText);
//                if (screenshotPath != null)
//                {
//                    _scenario.AddScreenCaptureFromPath(screenshotPath, "Screenshot for Passed Step");
//                }
//            }
//            else
//            {
//                _scenario.Log(Status.Fail, stepText);
//                _scenario.Log(Status.Fail, _scenarioContext.TestError.Message);
//                if (screenshotPath != null)
//                {
//                    _scenario.AddScreenCaptureFromPath(screenshotPath, "Screenshot for Failed Step");
//                }
//            }
//        }

//        [AfterScenario]
//        public void TearDown()
//        {
//            if (driver != null)
//            {
//                driver.Quit();
//                driver = null;
//            }
//        }

//        [AfterTestRun]
//        public static void AfterTestRun()
//        {
//            _extent.Flush();
//        }

//        // ✅ FIX: Unique Filenames for Screenshots
//        private string CaptureScreenshot(string screenshotName)
//        {
//            try
//            {
//                if (driver == null)
//                {
//                    TestContext.Progress.WriteLine("WebDriver is null. Cannot capture screenshot.");
//                    return null;
//                }

//                if (driver.WindowHandles.Count == 0)
//                {
//                    TestContext.Progress.WriteLine("No active browser window. Skipping screenshot.");
//                    return null;
//                }

//                // ✅ Introduce Small Wait Before Capturing Screenshot
//                Thread.Sleep(500);

//                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

//                // ✅ Ensure Screenshots Folder Exists
//                string screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
//                Directory.CreateDirectory(screenshotPath);

//                // ✅ Append Timestamp to Filenames
//                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
//                string filePath = Path.Combine(screenshotPath, $"{screenshotName}_{timestamp}.png");

//                // ✅ Save Screenshot
//                screenshot.SaveAsFile(filePath);
//                TestContext.Progress.WriteLine($"Screenshot saved at: {filePath}");

//                return filePath;
//            }
//            catch (Exception ex)
//            {
//                TestContext.Progress.WriteLine($"Failed to capture screenshot: {ex.Message}");
//                return null;
//            }
//        }
//    }
//}




////using System;
////using NUnit.Framework;
////using OpenQA.Selenium;
////using TechTalk.SpecFlow;
////using AventStack.ExtentReports;
////using AventStack.ExtentReports.Reporter;
////using sauceLabs_PageObject.Utils;

////namespace sauceLabs_PageObject.Hooks
////{
////    [Binding]
////    public class Hooks
////    {
////        private static IWebDriver driver;
////        private readonly ScenarioContext _scenarioContext;
////        private static ExtentReports _extent;
////        private static ExtentTest _feature;
////        private static ExtentTest _scenario;
////        private static ExtentSparkReporter _sparkReporter;

////        public Hooks(ScenarioContext scenarioContext)
////        {
////            _scenarioContext = scenarioContext;
////        }

////        [BeforeTestRun]
////        public static void BeforeTestRun()
////        {
////            TestContext.Progress.WriteLine("Initializing Extent Reports...");
////            _sparkReporter = new ExtentSparkReporter("ExtentReport.html");
////            _extent = new ExtentReports();
////            _extent.AttachReporter(_sparkReporter);
////        }

////        [BeforeFeature]
////        public static void BeforeFeature(FeatureContext featureContext)
////        {
////            TestContext.Progress.WriteLine($"Starting Feature: {featureContext.FeatureInfo.Title}");
////            _feature = _extent.CreateTest(featureContext.FeatureInfo.Title);
////        }

////        [BeforeScenario]
////        public void Setup()
////        {
////            TestContext.Progress.WriteLine("Initializing WebDriver...");
////            if (driver == null)
////            {
////                driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
////            }
////            _scenarioContext["WebDriver"] = driver;
////            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
////        }

////        [AfterStep]
////        public void InsertReportingSteps()
////        {
////            if (_scenarioContext.TestError == null)
////            {
////                _scenario.Log(Status.Pass, _scenarioContext.StepContext.StepInfo.Text);
////            }
////            else
////            {
////                _scenario.Log(Status.Fail, _scenarioContext.StepContext.StepInfo.Text);
////                _scenario.Log(Status.Fail, _scenarioContext.TestError.Message);
////            }
////        }

////        [AfterScenario]
////        public void TearDown()
////        {
////            TestContext.Progress.WriteLine("Scenario Execution Completed.");
////            // Do nothing to keep the browser open
////        }

////        [AfterTestRun]
////        public static void AfterTestRun()
////        {
////            TestContext.Progress.WriteLine("Finalizing Extent Reports...");
////            _extent.Flush();
////            TestContext.Progress.WriteLine("Closing WebDriver...");
////            sauceLabs_PageObject.Utils.WebDriverManager.QuitDriver(); // Close the browser after all tests
////        }
////    }
////}


//////using System;
//////using NUnit.Framework;
//////using OpenQA.Selenium;
//////using TechTalk.SpecFlow;
//////using sauceLabs_PageObject.Utils;

//////namespace sauceLabs_PageObject.Hooks
//////{
//////    [Binding]
//////    public class Hooks
//////    {
//////        private static IWebDriver driver;
//////        private readonly ScenarioContext _scenarioContext;

//////        public Hooks(ScenarioContext scenarioContext)
//////        {
//////            _scenarioContext = scenarioContext;
//////        }

//////        [BeforeScenario]
//////        public void Setup()
//////        {
//////            TestContext.Progress.WriteLine("Initializing WebDriver...");
//////            if (driver != null)
//////            {
//////                driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
//////            }
//////            _scenarioContext["WebDriver"] = driver;
//////        }

//////        [AfterScenario]
//////        public void TearDown()
//////        {
//////            // Do nothing to keep the browser open
//////        }

//////        [BeforeTestRun]
//////        public static void BeforeTestRun()
//////        {
//////            //TestContext.Progress.WriteLine("Initializing WebDriver...");
//////            //if (driver != null)
//////            //{
//////            //    driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();
//////            //}// Start the browser once
//////            //FeatureRunner.RunOrderedTests(); // Run only selected feature tests
//////        }

//////        [AfterTestRun]
//////        public static void AfterTestRun()
//////        {
//////            TestContext.Progress.WriteLine("Closing WebDriver...");
//////            //sauceLabs_PageObject.Utils.WebDriverManager.QuitDriver();  // Close the browser after all tests
//////        }
//////    }
//////}


////////[BeforeScenario(Order = 1)]
////////public void ExecuteLogin()
////////{
////////    var loginSteps = new LoginStepDefinitions();
////////    loginSteps.GivenUserIsOnLoginPage();
////////    loginSteps.WhenUserEntersUsernameAndPassword();
////////    loginSteps.ThenUserIsLoggedIn();


////////}

////////[BeforeScenario(Order = 2)]
////////public void ExecuteViewItem()
////////{
////////    var viewItemSteps = new ViewItemStepDefinitions();
////////    viewItemSteps.GivenUserIsLoggedIn();
////////    viewItemSteps.WhenUserViewsAnItem();
////////    viewItemSteps.ThenItemDataIsDisplayed();
////////}

////////[BeforeScenario(Order = 3)]
////////public void ExecuteAddToCart()
////////{
////////    var addToCartSteps = new AddToCartStepDefinitions();
////////    addToCartSteps.GivenUserIsViewingAnItem();
////////    addToCartSteps.WhenUserAddsItemToCartAndClicksOnCartButton();
////////    addToCartSteps.ThenCartWillBeOpened();
////////}

////////[BeforeScenario(Order = 4)]
////////public void ExecuteCheckout1()
////////{
////////    var checkout1Steps = new CheckoutStepDefinitions();
////////    checkout1Steps.GivenTheCartIsOpened();
////////    checkout1Steps.WhenUserClicksOnCheckoutButton();
////////    checkout1Steps.ThenCheckoutPageWillBeOpened();
////////}

////////[BeforeScenario(Order = 5)]
////////public void ExecuteCheckout2()
////////{
////////    var checkout2Steps = new Checkout2StepDefinitions();
////////    checkout2Steps.GivenUserIsInCheckOutPage();
////////    checkout2Steps.WhenUserEntersFirstnameLastnameAndPostalcodeAndClicksOnContinueButton();
////////    checkout2Steps.ThenUserIsNavigatedToCheckoutOverviewPage();
////////}

////////[BeforeScenario(Order = 6)]
////////public void ExecuteCheckoutOverview()
////////{
////////    var checkoutOverviewSteps = new CheckoutOverviewStepDefinitions();
////////    checkoutOverviewSteps.GivenUserIsInCheckoutOverviewPage();
////////    checkoutOverviewSteps.WhenUserScrollsDownAndClicksOnFinish();
////////    checkoutOverviewSteps.ThenConfirmationPageWillBeAppeared();
////////}

////////[BeforeScenario(Order = 7)]
////////public void ExecuteConfirmation()
////////{
////////    var confirmationSteps = new ConfirmationPageStepDefinitions();
////////    confirmationSteps.GivenUserIsInTheConfirmationPage();
////////    confirmationSteps.WhenUserChecksForConfirmationTextAndComparesItWithActualText();
////////    confirmationSteps.ThenIfItMatchesTheTestCasePasses_ElseItFails();
////////}



////////using System;
////////using NUnit.Framework;
////////using OpenQA.Selenium;
////////using TechTalk.SpecFlow;
////////using sauceLabs_PageObject.Utils; // Ensure this is correctly referenced

////////namespace sauceLabs_PageObject.Hooks
////////{
////////    [Binding]
////////    public class Hooks
////////    {
////////        private readonly ScenarioContext _scenarioContext;
////////        private static IWebDriver driver; // Store driver instance

////////        public Hooks(ScenarioContext scenarioContext)
////////        {
////////            _scenarioContext = scenarioContext;
////////        }

////////        [BeforeTestRun]
////////        public static void BeforeTestRun()
////////        {
////////            TestContext.Progress.WriteLine("Initializing WebDriver...");
////////            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();// Fix: Ensure WebDriverManager is properly referenced
////////        }

////////        [AfterTestRun]
////////        public static void AfterTestRun()
////////        {
////////            TestContext.Progress.WriteLine("Closing WebDriver...");
////////            sauceLabs_PageObject.Utils.WebDriverManager.QuitDriver(); // Fix: Ensure WebDriverManager is properly referenced
////////        }

////////        [BeforeScenario]
////////        public void Setup()
////////        {
////////            _scenarioContext["WebDriver"] = driver; // Fix: Store driver in ScenarioContext correctly
////////        }

////////        [AfterScenario]
////////        public void TearDown()
////////        {
////////            Console.WriteLine("After Scenario Execution");
////////            // Do NOT quit driver here; it should persist across all test runs
////////        }
////////    }
////////}
