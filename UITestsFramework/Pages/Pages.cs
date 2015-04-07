using OpenQA.Selenium.Support.PageObjects;

namespace UITestsFramework.Pages
{
    public static class Pages
    {
        private static T GetPage<T>() where T : new()
        {
            var page = new T();
            PageFactory.InitElements(Browser.Driver, page);
            return page;
        }

        public static TopNavigationPage TopNavigation
        {
            get { return GetPage<TopNavigationPage>(); }
        }

        public static LoginPage Login
        {
            get { return GetPage<LoginPage>(); }
        }

        public static ContingentPage ContingentPage
        {
            get { return GetPage<ContingentPage>(); }
        }
    }
}
