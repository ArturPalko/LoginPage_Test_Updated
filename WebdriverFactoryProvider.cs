using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webdriver.FinalTestTak;

namespace WebdriverFactory
{
    public enum BrowserType
    {
        Chrome,
        Firefox,
        Edge
    }

    public interface IWebDriverFactory
    {
        IWebDriver CreateDriver();
    }

    public class ChromeDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver() => new ChromeDriver();
    }

    public class FirefoxDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver() => new FirefoxDriver();
    }

    public class EdgeDriverFactory : IWebDriverFactory
    {
        public IWebDriver CreateDriver() => new EdgeDriver();
    }

    public static class WebDriverFactoryProvider
    {
        public static IWebDriverFactory GetFactory(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriverFactory();
                case BrowserType.Firefox:
                    return new FirefoxDriverFactory();
                case BrowserType.Edge:
                    return new EdgeDriverFactory();
                default:
                    throw new NotSupportedException($"Browser type {browserType} is not supported");
            }
        }

    }
}
