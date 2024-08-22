using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium;
using WebdriverFactory;
using Webdriver.FinalTestTak;
using System;

namespace LoginPageTests
{
    [TestClass]
    public class LoginPageTests
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();
        private IWebDriver _driver;

        [TestCleanup]
        public void TearDown()
        {
            Logger.Info("Tearing down the test environment.");
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
            Logger.Info("Browser closed.");
        }
        public void InitDriver(BrowserType browserType)
        {
            IWebDriverFactory factory = WebDriverFactoryProvider.GetFactory(browserType);
            _driver = factory.CreateDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestMethod]
        [DataRow(BrowserType.Chrome)]
        [DataRow(BrowserType.Firefox)]
        [DataRow(BrowserType.Edge)]
        public void LoginFormWithEmptyCredentials(BrowserType browserType)
        {
            // Arrange (Given)
            Logger.Info($"Starting the test in {browserType}.");
            InitDriver(browserType);
            LoginPage _loginPage = new LoginPage(_driver);

            Logger.Info("Given the login page is open");
            _loginPage.Open();

            // Act (When)
            Logger.Info("When I fill out the form with empty credentials");
            _loginPage.FillOutTheForm();

            Logger.Info("And I clear the form");
            _loginPage.ClearTheForm();

            Logger.Info("And I submit the form");
            _loginPage.SubmitTheForm();

            // Assert (Then)
            Logger.Info("Then I should see an error message indicating that the username is required");
            _loginPage.CheckErrorMessage(LoginPage.ErrorMessageUsernameIsRequired);
        }

        [TestMethod]
        [DataRow(BrowserType.Chrome)]
        [DataRow(BrowserType.Firefox)]
        [DataRow(BrowserType.Edge)]
        public void LoginFormWithCredentialsByPassingUsername(BrowserType browserType)
        {
            // Arrange (Given)
            Logger.Info($"Starting the test in {browserType}.");
            InitDriver(browserType);
            LoginPage _loginPage = new LoginPage(_driver);

            Logger.Info("Given the login page is open");
            _loginPage.Open();

            // Act (When)
            Logger.Info("When I fill out the form with username but without password");
            _loginPage.FillOutTheForm(LoginPage.AcceptedUserName, LoginPage.AcceptedPassword);

            Logger.Info("And I clear the password field");
            _loginPage.ClearThePasswordField();

            Logger.Info("And I submit the form");
            _loginPage.SubmitTheForm();

            // Assert (Then)
            Logger.Info("Then I should see an error message indicating that the password is required");
            _loginPage.CheckErrorMessage(LoginPage.ErrorMessagePasswordIsRequired);
        }

        [TestMethod]
        [DataRow(BrowserType.Chrome)]
        [DataRow(BrowserType.Firefox)]
        [DataRow(BrowserType.Edge)]
        public void LoginFormWithCredentialsByPassingUsernameAndPassword(BrowserType browserType)
        {
            // Arrange (Given)
            Logger.Info($"Starting the test in {browserType}.");
            InitDriver(browserType);
            LoginPage _loginPage = new LoginPage(_driver);

            Logger.Info("Given the login page is open");
            _loginPage.Open();

            // Act (When)
            Logger.Info("When I fill out the form with valid username and password");
            _loginPage.FillOutTheForm(LoginPage.AcceptedUserName, LoginPage.AcceptedPassword);

            Logger.Info("And I submit the form");
            _loginPage.SubmitTheForm();

            // Assert (Then)
            Logger.Info("Then I should be redirected to the main page and logged in successfully");
            _loginPage.CheckLogin(LoginPage.SwagLabsPageUrl);
        }
    }
}




