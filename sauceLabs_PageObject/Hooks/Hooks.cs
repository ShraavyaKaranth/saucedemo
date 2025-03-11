using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using sauceLabs_PageObject.Utils;

namespace sauceLabs_PageObject.Hooks
{
    [Binding]
    public class Hooks
    {
        private static IWebDriver driver;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private ExtentTest _scenario;
        private static ExtentSparkReporter _sparkReporter;

        private static string reportPath;
        private static string screenshotsDir;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            driver = sauceLabs_PageObject.Utils.WebDriverManager.GetDriver();

            string reportsDir = Path.Combine(Directory.GetCurrentDirectory(), "Reports");
            Directory.CreateDirectory(reportsDir);
            reportPath = Path.Combine(reportsDir, "ExtentReport.html");

            screenshotsDir = Path.Combine(reportsDir, "Screenshots");
            Directory.CreateDirectory(screenshotsDir);

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
            _scenario = _feature.CreateNode(_scenarioContext.ScenarioInfo.Title);
            _scenarioContext["WebDriver"] = driver;
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            string stepText = _scenarioContext.StepContext.StepInfo.Text;
            string screenshotPath = null;

            if (_scenarioContext.TestError != null)
            {
                screenshotPath = CaptureScreenshot(_scenarioContext.ScenarioInfo.Title);
                _scenario.Log(Status.Fail, stepText, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                _scenario.Log(Status.Fail, _scenarioContext.TestError.Message);
            }
            else
            {
                _scenario.Log(Status.Pass, stepText);
            }
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

            _extent.Flush();

            // ✅ Send email with report & screenshots using Gmail SMTP
            SendEmailWithGmail();
        }

        private string CaptureScreenshot(string scenarioName)
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

                string filePath = Path.Combine(screenshotsDir, $"{scenarioName}.png");
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

        private static void SendEmailWithGmail()
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string senderEmail = "shravyakaranth64715@gmail.com"; // ✅ Replace with your Gmail address
                string senderPassword = "gadc mhyr pkpx jljf"; // ✅ Use the App Password (16 characters)
                string recipientEmail = "shravyabahha@gmail.com";

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "SpecFlow Test Report & Screenshots",
                    Body = "Attached are the Extent Report and failure screenshots from the latest test execution.",
                    IsBodyHtml = false
                };

                mail.To.Add(recipientEmail);

                // ✅ Attach Extent Report
                if (File.Exists(reportPath))
                    mail.Attachments.Add(new Attachment(reportPath));

                // ✅ Attach Screenshots (if any)
                foreach (string screenshot in Directory.GetFiles(screenshotsDir, "*.png"))
                {
                    mail.Attachments.Add(new Attachment(screenshot));
                }

                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
                {
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };

                smtp.Send(mail);
                TestContext.Progress.WriteLine("✅ Email sent successfully via Gmail SMTP!");
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine($"❌ Failed to send email via Gmail SMTP: {ex.Message}");
            }
        }
    }
}
