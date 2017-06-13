using System;
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

        public static RegisterPage Register
        {
            get { return GetPage<RegisterPage>(); }
        }

        public static RebuildPage Rebuild
        {
            get { return GetPage<RebuildPage>(); }
        }

        public static LoginPage Login
        {
            get { return GetPage<LoginPage>(); }
        }

        public static ContingentPage Contingent
        {
            get { return GetPage<ContingentPage>(); }
        }

        public static TournamentPage TournamentSelector
        {
            get { return GetPage<TournamentPage>(); }
        }

        public static ProvincePage ProvinceSelector
        {
            get { return GetPage<ProvincePage>(); }
        }
    }
}
