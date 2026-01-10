using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using qa_dotnet_cucumber.Pages; 
using System;
using System.IO;

namespace qa_dotnet_cucumber.Utilities
{
    public class BaseTest
    {
        public IWebDriver driver;

        public static ExtentReports extent;
        public static ExtentTest test;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Reports", "index.html");
            var sparkReporter = new ExtentSparkReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);

            var loginPage = new LoginPage(driver);
            driver.Navigate().GoToUrl("http://10.211.55.2:5003");
            loginPage.ClickSignIn();
            loginPage.Login("surpreetsinghnz@gmail.com", "Taransingh46");
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
                test.Fail("Test Failed: " + message);
            }
            else if (status == TestStatus.Passed)
            {
                test.Pass("Test Passed Successfully");
            }

            driver?.Quit();
            driver?.Dispose();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            extent.Flush();
        }
    }
}