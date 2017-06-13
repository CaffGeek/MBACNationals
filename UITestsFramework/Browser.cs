using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITestsFramework
{
    public static class Browser
    {
        private static string baseUrl = "http://localhost:60828/setup/";
        private static IWebDriver webDriver;

        public static void Initialize()
        {
            var chromeDriverDirectory = @"C:\source\MBACNationals\UITestsFramework\bin\Debug\";
            var options = new ChromeOptions();
            options.AddArguments("--disable-extensions");
            webDriver = new ChromeDriver(chromeDriverDirectory, options);
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
