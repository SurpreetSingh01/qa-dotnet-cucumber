using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using qa_dotnet_cucumber.Pages;
using System;
using System.IO;

namespace qa_dotnet_cucumber.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private static ExtentReports _extent;
        private static ExtentSparkReporter _sparkReporter;
        private ExtentTest _test;
        private IWebDriver _driver;

        public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string reportPath = Path.Combine(Directory.GetCurrentDirectory(), "TestReport.html");
            _sparkReporter = new ExtentSparkReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(_sparkReporter);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext context)
        {
            _test = _extent.CreateTest(context.ScenarioInfo.Title);

           
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();

           
            options.AddArgument("--start-maximized");

           
            options.AddArgument("--disable-search-engine-choice-screen");

            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080"); 

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            
            _objectContainer.RegisterInstanceAs<IWebDriver>(_driver);
            _objectContainer.RegisterInstanceAs(new NavigationHelper(_driver));
            _objectContainer.RegisterInstanceAs(new LoginPage(_driver));
            
        }

        [AfterStep]
        public void AfterStep(ScenarioContext context)
        {
            var stepType = context.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepName = context.StepContext.StepInfo.Text;

            if (context.TestError == null)
            {
                _test.Log(Status.Pass, $"{stepType}: {stepName}");
            }
            else
            {
              
                var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
                _test.Log(Status.Fail, $"{stepType}: {stepName} - ERROR: {context.TestError.Message}",
                    MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot).Build());
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }
    }
}