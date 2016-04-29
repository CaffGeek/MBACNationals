using Microsoft.VisualStudio.TestTools.UnitTesting;
using UITestsFramework;
using UITestsFramework.Pages;

namespace UITests
{
    [TestClass]
    public class NavigationTests : TestBase
    {        
        public NavigationTests ()
            : base("Web.Admin") 
	    { }             

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

            Pages.Contingent.Goto();
            Pages.TournamentSelector.SelectLatestTournament();
            Pages.ProvinceSelector.Select("MB");
            Assert.IsTrue(Pages.Contingent.IsAt());
        }
    }
}
