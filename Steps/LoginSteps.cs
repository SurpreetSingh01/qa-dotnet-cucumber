using Reqnroll;
using qa_dotnet_cucumber.Pages;
using NUnit.Framework;

[Binding]
public class LoginSteps
{
    private readonly LoginPage _loginPage;
    private readonly NavigationHelper _navigationHelper;

    public LoginSteps(LoginPage loginPage, NavigationHelper navigationHelper)
    {
        _loginPage = loginPage;
        _navigationHelper = navigationHelper;
    }

    [Given(@"I navigate to the Mars home page")]
    public void GivenINavigateToTheMarsHomePage()
    {
        _navigationHelper.NavigateTo("/");
    }

    [When(@"I click on Sign In")]
    public void WhenIClickOnSignIn()
    {
        _loginPage.ClickSignIn();
    }

    [When(@"I enter valid credentials")]
    public void WhenIEnterValidCredentials()
    {
        _loginPage.Login("surpreetsinghnz@gmail.com", "Taransingh46");
    }

    [Then(@"I should be logged in")]
    public void ThenIShouldBeLoggedIn()
    {
        bool isLoggedIn = _loginPage.IsSignOutButtonVisible();
        Assert.That(isLoggedIn, Is.True, "Login failed: 'Sign Out' button was not visible.");
    }
}