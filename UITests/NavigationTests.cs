using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITestsFramework;
using UITestsFramework.Pages;

namespace UITests
{
    [TestClass]
    public class NavigationTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Browser.Initialize();
        }

        [TestCleanup]
        public void TestFixtureTearDown()
        {
            Browser.Close();
        }

        [TestMethod]
        public void CanLoginAsAdmin()
        {
            Pages.Login.Goto();
            Pages.Login.LogInAsAdmin();
            Assert.IsTrue(Pages.TopNavigation.LoggedIn());
        }

        [TestMethod]
        public void CanGoToContingentPage()
        {
            Pages.Login.Goto();
            Pages.Login.LogInAsAdmin();

            Pages.ContingentPage.Goto();
            Assert.IsTrue(Pages.ContingentPage.IsAt());
        }
    }
}
