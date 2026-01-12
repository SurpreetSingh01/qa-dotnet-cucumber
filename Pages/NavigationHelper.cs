using OpenQA.Selenium;

namespace qa_dotnet_cucumber.Pages
{
    public class NavigationHelper
    {
        private readonly IWebDriver _driver;

        public NavigationHelper(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateTo(string urlPath)
        {
            string baseUrl = "http://10.211.55.2:5003";

            _driver.Navigate().GoToUrl(baseUrl + urlPath);
        }
    }
}