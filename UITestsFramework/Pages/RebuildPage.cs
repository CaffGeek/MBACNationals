using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace UITestsFramework.Pages
{
    public class RebuildPage
    {
        private IWebElement All => Browser.Driver.FindElement(By.LinkText("All"));

        public void Goto()
        {
            Pages.TopNavigation.RebuildReadModels();
        }

        public void RebuildAll()
        {
            Actions actions = new Actions(Browser.Driver);
            actions.MoveToElement(All);
            actions.Click();
            actions.Build().Perform();
        }
    }
}