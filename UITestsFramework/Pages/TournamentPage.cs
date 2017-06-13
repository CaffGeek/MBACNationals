using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestsFramework.Pages
{
    public class TournamentPage
    {
        private IList<IWebElement> TournamentLinks => Browser.Driver.FindElements(By.CssSelector("a.tournament"));
        private IWebElement NewTournamentTextField => Browser.Driver.FindElement(By.Id("newTournamentYear"));
        private IWebElement NewTournamentCreate => Browser.Driver.FindElement(By.Id("newTournamentCreate"));

        public void SelectLatestTournament()
        {
            var wait = new WebDriverWait(Browser.Driver, new TimeSpan(0, 3, 0));
            wait.Until(driver => driver.FindElement(By.CssSelector("a.tournament")));
            TournamentLinks.Last().Click();
        }

        public void CreateTournament()
        {
            NewTournamentTextField.SendKeys("2000");
            NewTournamentCreate.SendKeys(Keys.Return);
        }

        public void Goto()
        {
            Pages.TopNavigation.Home();
        }
    }
}
