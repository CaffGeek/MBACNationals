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
        public void CanRegisterUser()
        {
            Pages.Register.Goto();
            Pages.Register.CreateUser();
            Assert.IsTrue(Pages.TopNavigation.LoggedIn());
        }

        [Test]
        public void CanRegisterAsAdmin()
        {
            Pages.Register.Goto();
            Pages.Register.CreateAdminUser();
            Assert.IsTrue(Pages.TopNavigation.LoggedIn());
        }

        [Test]
        public void CanCreateTournament()
        {
            Pages.Register.Goto();
            Pages.Register.CreateAdminUser();

            Pages.TournamentSelector.CreateTournament();            
            Pages.TournamentSelector.SelectLatestTournament();
            Pages.ProvinceSelector.Select("MB");

            Assert.IsTrue(Pages.Contingent.IsAt());
        }
    }
}
