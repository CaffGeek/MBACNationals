namespace UITestsFramework.Pages
{
    public class ContingentPage
    {
        public void Goto()
        {
            Pages.TopNavigation.Contingent();
        }

        public bool IsAt()
        {
            return Browser.Title.Contains("About");
        }
    }
}