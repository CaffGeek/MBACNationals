using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace UITestsFramework.Pages
{
    public class ProvincePage
    {
        [FindsBy(How = How.CssSelector, Using = "a.province")]
        private IList<IWebElement> provinceLinks;
        
        public void Select(string province)
        {
            provinceLinks.First().Click();
        }
    }
}