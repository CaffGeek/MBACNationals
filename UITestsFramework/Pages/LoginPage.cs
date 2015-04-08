using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestsFramework.Pages
{
    public class LoginPage
    {
        [FindsBy(How = How.Id, Using = "UserName")]
        private IWebElement usernameTextField;

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement passwordTextField;

        [FindsBy(How = How.CssSelector, Using = "input[type='submit']")]
        private IWebElement logInButton;

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
            usernameTextField.SendKeys("");
            passwordTextField.SendKeys("");

            logInButton.Click();
        }
    }
}