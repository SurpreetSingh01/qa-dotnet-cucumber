using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace qa_dotnet_cucumber.Pages
{
    public class LanguagePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public LanguagePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        private By LanguageTab => By.XPath("//a[contains(text(),'Languages')]");
        private By AddNewBtn => By.XPath("//div[contains(text(),'Add New')]");
        private By NameInput => By.Name("name");
        private By LevelDropdown => By.Name("level");
        private By AddBtn => By.XPath("//input[@type='button' and @value='Add']");
        private By UpdateBtn => By.XPath("//input[@type='button' and @value='Update']");

        private By GetDeleteButton(string language) =>
            By.XPath($"//td[normalize-space(text())='{language}']/following-sibling::td//i[@class='remove icon']");

        private By GetEditButton(string language) =>
            By.XPath($"//td[normalize-space(text())='{language}']/following-sibling::td//i[@class='outline write icon']");

        private By GetRow(string language) =>
            By.XPath($"//td[normalize-space(text())='{language}']");


        public void GoToLanguageTab()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(LanguageTab)).Click();
        }

        public void AddLanguage(string language, string level)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewBtn)).Click();

            var nameField = _wait.Until(ExpectedConditions.ElementIsVisible(NameInput));
            nameField.Clear();
            nameField.SendKeys(language);

            var levelDropdown = _driver.FindElement(LevelDropdown);
            var selectElement = new SelectElement(levelDropdown);
            selectElement.SelectByText(level);

            _driver.FindElement(AddBtn).Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(GetRow(language)));
        }

        public void UpdateLanguage(string oldLanguage, string newLanguage, string newLevel)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(GetEditButton(oldLanguage))).Click();

            var nameField = _wait.Until(ExpectedConditions.ElementIsVisible(NameInput));
            nameField.Clear();
            nameField.SendKeys(newLanguage);

            var levelDropdown = _driver.FindElement(LevelDropdown);
            var selectElement = new SelectElement(levelDropdown);
            selectElement.SelectByText(newLevel);

            _driver.FindElement(UpdateBtn).Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(GetRow(newLanguage)));
        }

        public void DeleteLanguage(string language)
        {
            try
            {
                var deleteBtn = _driver.FindElement(GetDeleteButton(language));
                deleteBtn.Click();
                _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(GetRow(language)));
            }
            catch (NoSuchElementException)
            {
            }
        }

        public bool IsLanguageVisible(string language)
        {
            try { return _driver.FindElements(GetRow(language)).Count > 0; }
            catch { return false; }
        }
    }
}