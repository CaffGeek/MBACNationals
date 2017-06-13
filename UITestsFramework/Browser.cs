using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace UITestsFramework
{
    public static class Browser
    {
        private static string baseUrl = "http://localhost:60828/setup/";
        private static IWebDriver webDriver;

        public static void Initialize()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\source\MBACNationals\UITestsFramework\bin\Debug\", "geckodriver.exe");
            webDriver = new FirefoxDriver(service);
            
            Goto("");
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static IWebDriver Driver
        {
            get { return webDriver; }
        }

        public static void Goto(string url)
        {
            webDriver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 1));
            webDriver.Url = baseUrl + url;
        }

        public static void Close()
        {
            webDriver.Close();
        }
    }
}
