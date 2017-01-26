using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestsFramework.Pages
{
    public class LoginPage
    {
        private readonly WebDriverWait _wait;

        private IWebElement usernameTextField 
        { 
            get 
            { 
                _wait.Until(driver => driver.FindElement(By.Id("UserName")));
                return Browser.Driver.FindElement(By.Id("UserName"));
            } 
        }

        private IWebElement passwordTextField { get { return Browser.Driver.FindElement(By.Id("Password")); } }

        private IWebElement logInButton { get { return Browser.Driver.FindElement(By.CssSelector("input[type='submit']")); } }

        public LoginPage()
        {
            _wait = new WebDriverWait(((IWebDriver)Browser.Driver), new TimeSpan(0, 3, 0));
        }

        public void Goto()
        {
            Pages.TopNavigation.Login();
        }

        public bool IsAt()
        {
            return Browser.Title.Contains("Login");
        }

        public void LogInAsAdmin()
        {
            Login();
        }

        private void Login()
        {
            usernameTextField.SendKeys("Chad");
            passwordTextField.SendKeys("9705644");

            logInButton.Click();
        }
    }
}