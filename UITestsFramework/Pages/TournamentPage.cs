using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestsFramework.Pages
{
    public class TournamentPage
    {
        [FindsBy(How = How.CssSelector, Using = "a.tournament")]
        private IList<IWebElement> tournamentLinks;
        
        public void SelectLatestTournament()
        {
            tournamentLinks.Last().Click();
        }
    }
}