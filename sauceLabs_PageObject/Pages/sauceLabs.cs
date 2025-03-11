using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using sauceLabs_PageObject.Utils;

namespace sauceLabs_PageObject.Pages
{
    [Binding]
    public class sauceLabs
    {
        private IWebDriver driver;

        public sauceLabs(IWebDriver driver)
        {

            this.driver = driver;
        }

        // locators on thye login page

        By usernameField = By.XPath("//input[@id='user-name']");
        By passwordField = By.XPath("//input[@id='password']");
        By loginFormLocator = By.XPath("//input[@id='login-button']");
        By item = By.XPath("//img[@alt='Sauce Labs Backpack']");
        By addItemToCart = By.XPath("//button[@id='add-to-cart']");
        By cartLink = By.XPath("//a[@class='shopping_cart_link']");
        By checkOut = By.XPath("//button[@id='checkout']");
        By checkOutFirstName = By.XPath("//input[@id='first-name']");
        By checkOutLastName = By.XPath("//input[@id='last-name']");
        By checkOutPostalCode = By.XPath("//input[@id='postal-code']");
        By continueCheckOut = By.XPath("//input[@id='continue']");
        By finishButton = By.XPath("//button[@id='finish']");
        By confirmationMessage = By.XPath("//h2[normalize-space()='Thank you for your order!']");
        // laucnh browser

        public void launchbrowser()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(config.BaseUrl);
            Thread.Sleep(3000);  // Increase wait time to ensure page loads

        }

        // enter username and password and submit

        public void login(String username, String password)
        {

            driver.FindElement(usernameField).SendKeys(username);
            driver.FindElement(passwordField).SendKeys(password);
            driver.FindElement(loginFormLocator).Click();
        }

        

        // home page is displayed
        public void clickItem()
        {
            driver.FindElement(item).Click();
        }
        public void addToCart()
        {
            driver.FindElement(addItemToCart).Click();
        }
        public void openCart()
        {
            driver.FindElement(cartLink).Click();
        }
        public void homepagedisplay()
        {

            Console.WriteLine("Home Page");
            Thread.Sleep(3000);
            driver.Close();
        }

        public void checkout()
        {
            driver.FindElement(checkOut).Click();
        }
        public void checkOutDetails(String firstName, String lastName, String postalCode)
        {

            driver.FindElement(checkOutFirstName).SendKeys(firstName);
            driver.FindElement(checkOutLastName).SendKeys(lastName);
            driver.FindElement(checkOutPostalCode).SendKeys(postalCode);
        }
        public void checkoutContinue()
        {
            driver.FindElement(continueCheckOut).Click();
        }
        public void finishClick()
        {
            Actions actions = new Actions(driver);
            actions.SendKeys(Keys.PageDown).Perform();
            actions.SendKeys(Keys.PageDown).Perform();
            driver.FindElement(finishButton).Click();
        }
        public void confirmationPage()
        {
            string actualText = driver.FindElement(confirmationMessage).Text;
            Assert.That(actualText, Is.EqualTo("Thank you for your order!"));
            //Assert.AreEqual("Thank you for your order!", confirmationMessage.Text, "Order confirmation message is incorrect!");
        }
    }
}
