using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace UITestsFramework.Pages
{
    public class TopNavigationPage
    {
        [FindsBy(How = How.LinkText, Using = "Home")]
        private IWebElement homeLink;

        [FindsBy(How = How.LinkText, Using = "Contingent")]
        private IWebElement contingentLink;

        [FindsBy(How = How.LinkText, Using = "Reservations")]
        private IWebElement reservationsLink;

        [FindsBy(How = How.LinkText, Using = "Travel")] 
        private IWebElement travelLink;

        [FindsBy(How = How.LinkText, Using = "Practice")]
        private IWebElement practiceLink;

        [FindsBy(How = How.LinkText, Using = "Profiles")]
        private IWebElement profilesLink;

        [FindsBy(How = How.LinkText, Using = "Log in")]
        private IWebElement loginLink;

        [FindsBy(How = How.LinkText, Using = "Register")]
        private IWebElement registerLink;

        [FindsBy(How = How.LinkText, Using = "Admin")]
        private IWebElement adminMenu;

        [FindsBy(How = How.LinkText, Using = "Rebuild Read Models")]
        private IWebElement rebuildReadModels;

        private IWebElement logoutLink { get { return Browser.Driver.FindElement(By.Id("logoff")); } }

        internal void Home()
        {
            homeLink.SendKeys(Keys.Enter);
        }

        public void Contingent()
        {
            contingentLink.SendKeys(Keys.Enter);
        }

        public void Reservations()
        {
            reservationsLink.SendKeys(Keys.Enter);
        }

        public void Travel()
        {
            travelLink.SendKeys(Keys.Enter);
        }

        public void Practice()
        {
            practiceLink.SendKeys(Keys.Enter);
        }

        public void Profiles()
        {
            profilesLink.SendKeys(Keys.Enter);
        }

        public void Register()
        {
            try
            {
                registerLink.SendKeys(Keys.Enter);
            }
            catch (NoSuchElementException)
            {
                Logout();
                registerLink.SendKeys(Keys.Enter);
            }       
        }

        public void Logout()
        {
            logoutLink.SendKeys(Keys.Enter);
        }

        public void Login()
        {
            try
            {
                loginLink.SendKeys(Keys.Enter);
            }
            catch (NoSuchElementException)
            {
                Logout();
                loginLink.SendKeys(Keys.Enter);
            }
        }

        public void RebuildReadModels()
        {
            adminMenu.SendKeys(Keys.Enter);
            rebuildReadModels.SendKeys(Keys.Enter);
        }

        public bool LoggedIn()
        {
            return logoutLink.GetAttribute("Value").StartsWith("Log off", StringComparison.OrdinalIgnoreCase);
        }
    }
}
