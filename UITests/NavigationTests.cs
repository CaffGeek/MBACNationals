using NUnit.Framework;
using UITestsFramework;
using UITestsFramework.Pages;

namespace UITests
{
    [TestFixture]
    public class NavigationTests : TestBase
    {        
        public NavigationTests ()
            : base("Web.Admin") 
	    { }             

        [Test]
        public void CanLoginAsAdmin()
        {
            Pages.Login.Goto();
            Pages.Login.LogInAsAdmin();
            Assert.IsTrue(Pages.TopNavigation.LoggedIn());
        }

        [Test]
        public void CanGoToContingentPage()
        {
            Pages.Login.Goto();
            Pages.Login.LogInAsAdmin();

            Pages.TournamentSelector.SelectLatestTournament();
            Pages.ProvinceSelector.Select("MB");
            Assert.IsTrue(Pages.Contingent.IsAt());
        }
    }
}
