using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UITestsFramework
{
    public static class Browser
    {
        private static string baseUrl = "http://localhost:54055/Setup/";
        private static readonly IWebDriver webDriver = new ChromeDriver();
        public static void Initialize()
        {
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
            webDriver.Url = baseUrl + url;
        }

        public static void Close()
        {
            webDriver.Close();
        }
    }
}
