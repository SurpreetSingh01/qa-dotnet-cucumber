- Mars Portal Automation
Hi, this is my test automation project for the Mars Portal. I built this framework to automatically test the Login, Language, and Skills features.

- Tools I Used
Language: C# (.NET 8.0)

Testing Framework: ReqnRoll (SpecFlow) for BDD

Browser Automation: Selenium WebDriver (Chrome)

Reporting: ExtentReports (creates HTML reports)

- How I Structured the Code (Page Object Model)
I used the Page Object Model (POM) design pattern to keep the code clean and easy to maintain. Here is how I organized it:

Pages folder: This is where I keep the locators (XPath) and methods. For example, SkillsPage.cs knows how to find the "Add" button and type into the text box.

Steps folder: This is where the actual test steps live. The steps call the Page methods to do the work.

Features folder: These are the Gherkin files (Given, When, Then) that describe the test scenarios in plain English.

Hooks folder: This handles the setup and teardown logic (like opening the browser before a test and closing it after).

- Key Features

Data Cleanup: I made sure the tests are independent. Every test creates its own data and deletes it when it's done. This stops the tests from failing because of old data.

Scenario Outlines: I used these to test multiple examples at once (like adding French, Spanish, and German in a single test).

Smart Waits: Instead of using slow Thread.Sleep, I used explicit waits (WebDriverWait) so the tests are fast and stable.

- How to Run the Tests
Clone this repository to your machine.

Open the solution file in Visual Studio 2022.

Build the Solution (Ctrl+Shift+B) to restore packages.

Open the Test Explorer (Go to Test > Test Explorer).

Click Run All Tests.

- Reports
After the tests finish, you can find a TestReport.html file in the project folder. It shows which tests passed or failed, and takes a screenshot if something goes wrong.