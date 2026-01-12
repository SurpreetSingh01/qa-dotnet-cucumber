using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace qa_dotnet_cucumber.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        private readonly By SignInButton = By.XPath("//a[contains(text(),'Sign In')]");
        private readonly By EmailField = By.Name("email");
        private readonly By PasswordField = By.Name("password");
        private readonly By LoginButton = By.XPath("//button[text()='Login']");

        private readonly By SignOutButton = By.XPath("//button[contains(text(),'Sign Out')]");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
        }

        public void ClickSignIn()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SignInButton)).Click();
        }

        public void Login(string email, string password)
        {
            var emailElement = _wait.Until(ExpectedConditions.ElementIsVisible(EmailField));
            emailElement.Clear();
            emailElement.SendKeys(email);

            var passElement = _driver.FindElement(PasswordField);
            passElement.Clear();
            passElement.SendKeys(password);

            _driver.FindElement(LoginButton).Click();
        }

        public bool IsSignOutButtonVisible()
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(SignOutButton)).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }
    }
}