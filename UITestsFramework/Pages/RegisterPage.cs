using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace UITestsFramework.Pages
{
    public class RegisterPage
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
        private IWebElement confirmTextField { get { return Browser.Driver.FindElement(By.Id("ConfirmPassword")); } }
        private IWebElement registerButton { get { return Browser.Driver.FindElement(By.CssSelector("input[type='submit']")); } }

        public RegisterPage()
        {
            _wait = new WebDriverWait(Browser.Driver, new TimeSpan(0, 3, 0));
        }

        public void Goto()
        {
            Pages.TopNavigation.Register();
        }

        public void CreateUser(string user = null)
        {
            Register(user);
        }

        public void CreateAdminUser(string user = null)
        {
            Database.Accounts.UpgradeUserToAdmin(Register(user));
        }
        
        private string Register(string user = null)
        {
            user = user ?? Guid.NewGuid().ToString();
            var pass = "password";

            usernameTextField.SendKeys(user);
            passwordTextField.SendKeys(pass);
            confirmTextField.SendKeys(pass);

            registerButton.Click();

            return user;
        }
    }
}