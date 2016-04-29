using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITestsFramework
{
    public static class Browser
    {
        private static string baseUrl = "http://localhost:2020/";
        private static IWebDriver webDriver;

        public static void Initialize()
        {
            webDriver = new ChromeDriver();
            Goto("");
        }

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static ISearchContext Driver
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
