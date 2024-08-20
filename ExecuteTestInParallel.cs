using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdriver.FinalTestTak;
using WebdriverFactory;
using LoginPageTests;

namespace ExecuteTestInParallel
{
     class ParallelTest
    {
        public static void execute(Action<IWebDriver> testAction, BrowserType browserType)
        {
            IWebDriverFactory factory = WebDriverFactoryProvider.GetFactory(browserType);
            IWebDriver _driver = factory.CreateDriver();
            LoginPageTests.LoginPageTests.Logger.Info("Driver initialized.");

            try
            {
                LoginPageTests.LoginPageTests.Logger.Info("Starting the test setup.");
                testAction(_driver);
            }
            finally
            {
                if (_driver != null)
                {
                    _driver.Quit();
                    _driver.Dispose();
                }
                LoginPageTests.LoginPageTests.Logger.Info("Browser closed.");
            }
        }

    }
}
