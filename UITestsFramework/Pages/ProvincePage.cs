using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace UITestsFramework.Pages
{
    public class ProvincePage
    {
        [FindsBy(How = How.CssSelector, Using = "a.province")]
        private IList<IWebElement> provinceLinks;
        
        public void Select(string province)
        {
            //TODO: Chad
            provinceLinks.First().Click();
        }
    }
}