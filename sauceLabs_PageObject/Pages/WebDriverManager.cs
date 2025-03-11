using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace sauceLabs_PageObject.Utils
{
    public static class WebDriverManager
    {
        [ThreadStatic]  // ✅ Ensures a separate instance per test thread
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("--headless"); // ✅ Required for GitHub Actions
                chromeOptions.AddArguments("--disable-gpu");
                chromeOptions.AddArguments("--no-sandbox");
                chromeOptions.AddArguments("--disable-dev-shm-usage");

                driver = new ChromeDriver(chromeOptions);
                driver.Manage().Window.Maximize();
            }
            return driver;
        }

        public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}




//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Edge;
//using System;

//namespace SpecFlowSauceDemo.Drivers
//{
//    public class WebDriverManager
//    {
//        private static IWebDriver driver;

//        public static IWebDriver GetDriver()
//        {
//            if (driver == null)
//            {
//                driver = InitializeDriver();
//            }
//            return driver;
//        }

//        private static IWebDriver InitializeDriver()
//        {
//            string browser = "chrome"; // Change this as needed or fetch from config

//            switch (browser.ToLower())
//            {
//                case "firefox":
//                    return new FirefoxDriver();
//                case "edge":
//                    return new EdgeDriver();
//                default:
//                    return new ChromeDriver();
//            }
//        }

//        public static void QuitDriver()
//        {
//            if (driver != null)
//            {
//                driver.Quit();
//                driver = null;
//            }
//        }
//    }
//}




////using OpenQA.Selenium;
////using OpenQA.Selenium.Chrome;

////namespace sauceLabs_PageObject.Utils
////{
////    public static class WebDriverManager
////    {
////        private static IWebDriver driver;

////        public static IWebDriver GetDriver()
////        {
////            if (driver == null)
////            {
////                driver = new ChromeDriver();
////                driver.Manage().Window.Maximize();
////            }
////            return driver;
////        }

////        public static void QuitDriver()
////        {
////            if (driver != null)
////            {
////                driver.Quit();
////                driver = null;
////            }
////        }
////    }
////}
