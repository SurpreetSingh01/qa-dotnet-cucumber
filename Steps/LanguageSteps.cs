using Reqnroll;
using qa_dotnet_cucumber.Pages;
using NUnit.Framework;

[Binding]
public class LanguageSteps
{
    private readonly LanguagePage _languagePage;
    private readonly LoginPage _loginPage;
    private readonly NavigationHelper _navigationHelper;

    public LanguageSteps(LanguagePage languagePage, LoginPage loginPage, NavigationHelper navigationHelper)
    {
        _languagePage = languagePage;
        _loginPage = loginPage;
        _navigationHelper = navigationHelper;
    }

    [Given(@"I log into the Mars portal")]
    public void GivenILogIntoTheMarsPortal()
    {
        _navigationHelper.NavigateTo("/");
        _loginPage.ClickSignIn();
        _loginPage.Login("surpreetsinghnz@gmail.com", "Taransingh46");
    }

    [Given(@"I navigate to the Language tab")]
    public void GivenINavigateToTheLanguageTab()
    {
        _languagePage.GoToLanguageTab();
    }

    [Given(@"I ensure the language '([^']*)' does not exist")]
    [Then(@"I delete the language '([^']*)'")]
    [When(@"I delete the language '([^']*)'")]
    public void DeleteLanguage(string language)
    {
        _languagePage.DeleteLanguage(language);
    }

    [When(@"I add a new language '([^']*)' with level '([^']*)'")]
    [Given(@"I have added the language '([^']*)' with level '([^']*)'")]
    public void WhenIAddANewLanguage(string language, string level)
    {
        _languagePage.AddLanguage(language, level);
    }

    [When(@"I update the language '([^']*)' to '([^']*)' with level '([^']*)'")]
    public void WhenIUpdateTheLanguage(string oldLang, string newLang, string newLevel)
    {
        _languagePage.UpdateLanguage(oldLang, newLang, newLevel);
    }

    [Then(@"the language '([^']*)' should be displayed")]
    public void ThenTheLanguageShouldBeDisplayed(string language)
    {
        Assert.That(_languagePage.IsLanguageVisible(language), Is.True, $"Language '{language}' should be visible but is not.");
    }

    [Then(@"the language '([^']*)' should not be displayed")]
    public void ThenTheLanguageShouldNotBeDisplayed(string language)
    {
        Assert.That(_languagePage.IsLanguageVisible(language), Is.False, $"Language '{language}' should NOT be visible but it is.");
    }
}