using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using qa_dotnet_cucumber.DataModels;
using System;
using System.Threading;

namespace qa_dotnet_cucumber.Pages
{
    public class EducationPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public EducationPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        private By EducationTab => By.XPath("//a[text()='Education']");
        private By AddNewBtn => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//div[text()='Add New']");
        private By ToastMessage => By.XPath("//div[@class='ns-box-inner']");
        private By UniversityInput => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@name='instituteName']");
        private By CountryDropdown => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//select[@name='country']");
        private By TitleDropdown => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//select[@name='title']");
        private By DegreeInput => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@name='degree']");
        private By YearDropdown => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//select[@name='yearOfGraduation']");
        private By AddBtn => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@value='Add']");
        private By UpdateBtn => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@value='Update']");

        public void AddEducation(EducationModel data)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(EducationTab));
            Thread.Sleep(2000);
            try { _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewBtn)).Click(); }
            catch (Exception) { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(AddNewBtn)); }

            if (!string.IsNullOrEmpty(data.University)) _wait.Until(ExpectedConditions.ElementIsVisible(UniversityInput)).SendKeys(data.University);
            if (data.Country != "Select Country") new SelectElement(_driver.FindElement(CountryDropdown)).SelectByText(data.Country);
            if (data.Title != "Select Title") new SelectElement(_driver.FindElement(TitleDropdown)).SelectByText(data.Title);
            if (!string.IsNullOrEmpty(data.Degree)) _driver.FindElement(DegreeInput).SendKeys(data.Degree);
            if (data.Year != "Select Year") new SelectElement(_driver.FindElement(YearDropdown)).SelectByText(data.Year);

            _driver.FindElement(AddBtn).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage));
        }

        public void UpdateEducation(string oldUniversityName, EducationModel newData)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(EducationTab));
            Thread.Sleep(2000);
            var editIcon = By.XPath($"//td[normalize-space(text())='{oldUniversityName}']/following-sibling::td//i[@class='outline write icon']");
            _driver.FindElement(editIcon).Click();

            var uniInput = _wait.Until(ExpectedConditions.ElementIsVisible(UniversityInput));
            uniInput.Clear();
            uniInput.SendKeys(newData.University);
            new SelectElement(_driver.FindElement(CountryDropdown)).SelectByText(newData.Country);
            new SelectElement(_driver.FindElement(TitleDropdown)).SelectByText(newData.Title);
            var degreeInput = _driver.FindElement(DegreeInput);
            degreeInput.Clear();
            degreeInput.SendKeys(newData.Degree);
            new SelectElement(_driver.FindElement(YearDropdown)).SelectByText(newData.Year);

            _driver.FindElement(UpdateBtn).Click();
            Thread.Sleep(1500);
            _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage));
        }

        public void DeleteEducation(string universityName)
        {
            try
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(EducationTab));
                Thread.Sleep(2000);
                var deleteIcon = By.XPath($"//td[normalize-space(text())='{universityName}']/following-sibling::td//i[@class='remove icon']");
                _driver.FindElement(deleteIcon).Click();
                _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage));
            }
            catch (Exception) { }
        }
        public string GetMessage() => _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage)).Text;
    }
}