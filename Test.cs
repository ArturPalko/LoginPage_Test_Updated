using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using WebdriverFactory;
using Webdriver.FinalTestTak;
using ExecuteTestInParallel; 

namespace LoginPageTests
{
    [TestClass]
    public class LoginPageTests
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();

        [TestMethod]
        [DataRow(BrowserType.Chrome)]
        [DataRow(BrowserType.Firefox)]
        [DataRow(BrowserType.Edge)]
        public void LoginFormWithEmptyCredentials(BrowserType browserType)
        {
            ParallelTest.execute(_driver =>
            {
                Logger.Info("Starting the test setup.");
                LoginPage _loginPage = new LoginPage(_driver);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                Logger.Info("Starting LoginFormWithEmptyCredentials test.");

                _loginPage.Open();
                Logger.Info("Opened login page.");

                _loginPage.FillOutTheForm();
                Logger.Info("Filled out the login form with empty credentials.");

                _loginPage.ClearTheForm();
                Logger.Info("Cleared the form.");

                _loginPage.SubmitTheForm();
                Logger.Info("Submitted the form.");

                _loginPage.CheckErrorMessage(LoginPage.ErrorMessageUsernameIsRequired);
                Logger.Info("Checked error message for empty credentials.");
            }, browserType);
        } 

        [TestMethod]
        [DataRow(BrowserType.Chrome)]
        [DataRow(BrowserType.Firefox)]
        [DataRow(BrowserType.Edge)]
        public void LoginFormWithCredentialsByPassingUsername(BrowserType browserType)
        {
            ParallelTest.execute(_driver =>
            {
                Logger.Info("Starting the test setup.");
                LoginPage _loginPage = new LoginPage(_driver);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                Logger.Info("Starting LoginFormWithCredentialsByPassingUsername test.");

                _loginPage.Open();
                Logger.Info("Opened login page.");

                _loginPage.FillOutTheForm(LoginPage.AcceptedUserName, LoginPage.AcceptedPassword);
                Logger.Info("Filled out the form with username but without password.");

                _loginPage.ClearThePasswordField();
                Logger.Info("Cleared the password field.");

                _loginPage.SubmitTheForm();
                Logger.Info("Submitted the form.");

                _loginPage.CheckErrorMessage(LoginPage.ErrorMessagePasswordIsRequired);
                Logger.Info("Checked error message for missing password.");
            }, browserType);
        }

        [TestMethod]
        [DataRow(BrowserType.Chrome)]
        [DataRow(BrowserType.Firefox)]
        [DataRow(BrowserType.Edge)]
        public void LoginFormWithCredentialsByPassingUsernameAndPassword(BrowserType browserType)
        {
            ParallelTest.execute(_driver =>

            {
                LoginPage _loginPage = new LoginPage(_driver);
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                Logger.Info("Starting LoginFormWithCredentialsByPassingUsernameAndPassword test.");

                _loginPage.Open();
                Logger.Info("Opened login page.");

                _loginPage.FillOutTheForm(LoginPage.AcceptedUserName, LoginPage.AcceptedPassword);
                Logger.Info("Filled out the form with valid username and password.");

                _loginPage.SubmitTheForm();
                Logger.Info("Submitted the form.");

                _loginPage.CheckLogin(LoginPage.SwagLabsPageUrl);
                Logger.Info("Checked login success and redirected to the main page.");
            }, browserType);

        }
    }
}



