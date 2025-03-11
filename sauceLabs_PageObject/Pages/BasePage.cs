using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace sauceLabs_PageObject.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        public void OpenUrl(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}
