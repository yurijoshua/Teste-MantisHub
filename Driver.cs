using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;

namespace TesteBase2
{

    public class Driver
    {
        public static IWebDriver _driver;

        FirefoxOptions optFireFox = new FirefoxOptions();
        ChromeOptions optChrome = new ChromeOptions();
        InternetExplorerOptions optIE = new InternetExplorerOptions();

        public void SetUp(string Browser, string url)
        {

            optFireFox.AddArguments("--ignore-certificate-errors", "--start-maximized", "--disabl-web-security", "--handless");
            optChrome.AddArguments("--ignore-certificate-errors", "--no-sandbox", "--start-maximized", "--disable-web-security", "--handless");
            optIE.IntroduceInstabilityByIgnoringProtectedModeSettings = true;

            switch (Browser)
            {
                case "Chrome":
                    _driver = new ChromeDriver(PathDriverGet.PathDriver(), optChrome);
                    _driver.Navigate().GoToUrl(url);
                    _driver.Manage().Window.Maximize();
                    break;
                case "Firefox":
                    _driver = new FirefoxDriver(PathDriverGet.PathDriver(), optFireFox);
                    _driver.Navigate().GoToUrl(url);
                    _driver.Manage().Window.Maximize();
                    break;
                case "IE":
                    _driver = new InternetExplorerDriver(PathDriverGet.PathDriver(), optIE);
                    _driver.Navigate().GoToUrl(url);
                    _driver.Manage().Window.Maximize();
                    break;
                default:
                    throw new NotSupportedException("Not supported browser");
            }      
        }

        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }

        public void TearDown()
        {
            _driver.Quit();
            _driver = null;
        }


    }
}
