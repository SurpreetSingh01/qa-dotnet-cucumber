using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace qa_dotnet_cucumber.Pages
{
    public class SkillsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SkillsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

       
        private static string SkillsTabContext = "//div[contains(@class,'tab')][.//div[contains(text(),'Do you have any skills?')]]";
        private By SkillsTab => By.XPath("//a[contains(text(),'Skills')]");

        private By AddNewBtn => By.XPath($"{SkillsTabContext}//div[contains(text(),'Add New')]");
        private By NameInput => By.XPath($"{SkillsTabContext}//input[@name='name']");
        private By LevelDropdown => By.XPath($"{SkillsTabContext}//select[@name='level']");
        private By AddBtn => By.XPath($"{SkillsTabContext}//input[@value='Add']");
        private By UpdateBtn => By.XPath($"{SkillsTabContext}//input[@value='Update']");

        private By GetDeleteButton(string skill) =>
            By.XPath($"{SkillsTabContext}//td[normalize-space(text())='{skill}']/following-sibling::td//i[@class='remove icon']");

        private By GetEditButton(string skill) =>
            By.XPath($"{SkillsTabContext}//td[normalize-space(text())='{skill}']/following-sibling::td//i[@class='outline write icon']");

        private By GetRow(string skill) =>
            By.XPath($"{SkillsTabContext}//td[normalize-space(text())='{skill}']");


        public void GoToSkillsTab()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(SkillsTab)).Click();
            System.Threading.Thread.Sleep(500);
        }

        public void AddSkill(string skill, string level)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewBtn)).Click();

            var nameField = _wait.Until(ExpectedConditions.ElementIsVisible(NameInput));
            nameField.Clear();
            nameField.SendKeys(skill);

            var levelDropdown = _driver.FindElement(LevelDropdown);
            var selectElement = new SelectElement(levelDropdown);
            selectElement.SelectByText(level);

            _driver.FindElement(AddBtn).Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(GetRow(skill)));
        }

        public void UpdateSkill(string oldSkill, string newSkill, string newLevel)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(GetEditButton(oldSkill))).Click();

            var nameField = _wait.Until(ExpectedConditions.ElementIsVisible(NameInput));
            nameField.Clear();
            nameField.SendKeys(newSkill);

            var levelDropdown = _driver.FindElement(LevelDropdown);
            var selectElement = new SelectElement(levelDropdown);
            selectElement.SelectByText(newLevel);

            _driver.FindElement(UpdateBtn).Click();

            _wait.Until(ExpectedConditions.ElementIsVisible(GetRow(newSkill)));
        }

        public void DeleteSkill(string skill)
        {
            try
            {
                var deleteBtn = _driver.FindElement(GetDeleteButton(skill));
                deleteBtn.Click();
                _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(GetRow(skill)));
            }
            catch (NoSuchElementException)
            {
            }
        }

        public bool IsSkillVisible(string skill)
        {
            try { return _driver.FindElements(GetRow(skill)).Count > 0; }
            catch { return false; }
        }
    }
}