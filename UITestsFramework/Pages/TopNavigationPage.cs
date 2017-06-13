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
            homeLink.Click();
        }

        public void Contingent()
        {
            contingentLink.Click();
        }

        public void Reservations()
        {
            reservationsLink.Click();
        }

        public void Travel()
        {
            travelLink.Click();
        }

        public void Practice()
        {
            practiceLink.Click();
        }

        public void Profiles()
        {
            profilesLink.Click();
        }

        public void Register()
        {
            try
            {
                registerLink.Click();
            }
            catch (NoSuchElementException)
            {
                logoutLink.Click();
                registerLink.Click();
            }       
        }

        public void Logout()
        {
            logoutLink.Click();
        }

        public void Login()
        {
            try
            {
                loginLink.Click();
            }
            catch (NoSuchElementException)
            {
                logoutLink.Click();
                loginLink.Click();
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
