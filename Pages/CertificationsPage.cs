using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using qa_dotnet_cucumber.DataModels;
using System;
using System.Threading;

namespace qa_dotnet_cucumber.Pages
{
    public class CertificationsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CertificationsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        private By CertificationsTab => By.XPath("//a[text()='Certifications']");
        private By AddNewBtn => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//div[text()='Add New']");
        private By ToastMessage => By.XPath("//div[@class='ns-box-inner']");
        private By CertificateInput => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@name='certificationName']");
        private By FromInput => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@name='certificationFrom']");
        private By YearDropdown => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//select[@name='certificationYear']");
        private By AddBtn => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@value='Add']");
        private By UpdateBtn => By.XPath("//div[contains(@class,'active') and contains(@class,'tab')]//input[@value='Update']");

        public void AddCertification(CertificationModel data)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(CertificationsTab));
            Thread.Sleep(2000);
            try { _wait.Until(ExpectedConditions.ElementToBeClickable(AddNewBtn)).Click(); }
            catch (Exception) { ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(AddNewBtn)); }

            if (!string.IsNullOrEmpty(data.Certificate)) _wait.Until(ExpectedConditions.ElementIsVisible(CertificateInput)).SendKeys(data.Certificate);
            if (!string.IsNullOrEmpty(data.From)) _driver.FindElement(FromInput).SendKeys(data.From);
            if (data.Year != "Select Year") new SelectElement(_driver.FindElement(YearDropdown)).SelectByText(data.Year);

            _driver.FindElement(AddBtn).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage));
        }

        public void UpdateCertification(string oldCertName, CertificationModel newData)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(CertificationsTab));
            Thread.Sleep(2000);
            var editIcon = By.XPath($"//td[normalize-space(text())='{oldCertName}']/following-sibling::td//i[@class='outline write icon']");
            _driver.FindElement(editIcon).Click();

            var certInput = _wait.Until(ExpectedConditions.ElementIsVisible(CertificateInput));
            certInput.Clear();
            certInput.SendKeys(newData.Certificate);
            var fromInput = _driver.FindElement(FromInput);
            fromInput.Clear();
            fromInput.SendKeys(newData.From);
            new SelectElement(_driver.FindElement(YearDropdown)).SelectByText(newData.Year);

            _driver.FindElement(UpdateBtn).Click();
            Thread.Sleep(1500);
            _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage));
        }

        public void DeleteCertification(string certName)
        {
            try
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", _driver.FindElement(CertificationsTab));
                Thread.Sleep(2000);
                var deleteIcon = By.XPath($"//td[normalize-space(text())='{certName}']/following-sibling::td//i[@class='remove icon']");
                _driver.FindElement(deleteIcon).Click();
                _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage));
            }
            catch (Exception) { }
        }
        public string GetMessage() => _wait.Until(ExpectedConditions.ElementIsVisible(ToastMessage)).Text;
    }
}