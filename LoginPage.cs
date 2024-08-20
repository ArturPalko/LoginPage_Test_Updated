using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Xml.Linq;
using OpenQA.Selenium.Interactions;
using NLog;
using Microsoft.VisualStudio.TestTools.UnitTesting;




namespace Webdriver.FinalTestTak
{
    public class LoginPage 
    {
        private const string TestUrl = "https://www.saucedemo.com/";
        private const string TestLoginFormWithEptyCredentialUsername = "Artur Palko";
        private const string TestLoginFormWithEptyCredentialPassword = "12345";
        public const string ErrorMessageUsernameIsRequired = "Epic sadface: Username is required";
        public const string AcceptedUserName = "standard_user";
        public const string AcceptedPassword = "secret_sauce";
        public const string ErrorMessagePasswordIsRequired = "Epic sadface: Password is required";
        public const string SwagLabsPageUrl = "https://www.saucedemo.com/inventory.html";
        private readonly IWebDriver _driver;

        public IWebElement UsernameField => _driver.FindElement(By.XPath("//input[@data-test='username']"));
        public IWebElement PasswordField => _driver.FindElement(By.XPath("//input[@data-test='password']"));
        public IWebElement SubmitBtn => _driver.FindElement(By.Id("login-button"));
        public IWebElement ErrorMessageOfEmptyForm => _driver.FindElement(By.XPath("//div[@class='error-message-container error']/h3[@data-test='error']"));

        public LoginPage(IWebDriver driver)
        {
           
            _driver = driver;
            LogManager.Setup().LoadConfigurationFromFile("NLog.config");
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl(TestUrl);
        }

        public void FillOutTheForm(string userName = TestLoginFormWithEptyCredentialUsername, string password = TestLoginFormWithEptyCredentialPassword)
        {
            UsernameField.SendKeys(userName);
            PasswordField.SendKeys(password);
        }

        public void ClearTheForm()
        {
            UsernameField.SendKeys(Keys.Control + "a");
            UsernameField.SendKeys(Keys.Delete);
            PasswordField.SendKeys(Keys.Control + "a");
            PasswordField.SendKeys(Keys.Delete);
        }
        public void ClearThePasswordField()
        {
             PasswordField.SendKeys(Keys.Control + "a");
             PasswordField.SendKeys(Keys.Delete);
        }
        public void SubmitTheForm()
        {
            SubmitBtn.Click();
        }
        public void CheckErrorMessage(string errromessage)
        {
            Assert.AreEqual(errromessage, ErrorMessageOfEmptyForm.Text);
        }
        public void CheckLogin(string expectedUrl)
        {
            string currentUrl = _driver.Url;
            Assert.AreEqual(expectedUrl, currentUrl);
        }
    }
}