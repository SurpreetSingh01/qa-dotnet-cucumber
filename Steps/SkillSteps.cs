using Reqnroll;
using qa_dotnet_cucumber.Pages;
using NUnit.Framework;

[Binding]
public class SkillsSteps
{
    private readonly SkillsPage _skillsPage;
    private readonly LoginPage _loginPage;
    private readonly NavigationHelper _navigationHelper;

    public SkillsSteps(SkillsPage skillsPage, LoginPage loginPage, NavigationHelper navigationHelper)
    {
        _skillsPage = skillsPage;
        _loginPage = loginPage;
        _navigationHelper = navigationHelper;
    }

    [Given(@"I navigate to the Skills tab")]
    public void GivenINavigateToTheSkillsTab()
    {
        _skillsPage.GoToSkillsTab();
    }

    [Given(@"I ensure the skill '([^']*)' does not exist")]
    [Then(@"I delete the skill '([^']*)'")]
    [When(@"I delete the skill '([^']*)'")]
    public void DeleteSkill(string skill)
    {
        _skillsPage.DeleteSkill(skill);
    }

    [When(@"I add a new skill '([^']*)' with level '([^']*)'")]
    [Given(@"I have added the skill '([^']*)' with level '([^']*)'")]
    public void WhenIAddANewSkill(string skill, string level)
    {
        _skillsPage.AddSkill(skill, level);
    }

    [When(@"I update the skill '([^']*)' to '([^']*)' with level '([^']*)'")]
    public void WhenIUpdateTheSkill(string oldSkill, string newSkill, string newLevel)
    {
        _skillsPage.UpdateSkill(oldSkill, newSkill, newLevel);
    }

    [Then(@"the skill '([^']*)' should be displayed")]
    public void ThenTheSkillShouldBeDisplayed(string skill)
    {
        Assert.That(_skillsPage.IsSkillVisible(skill), Is.True, $"Skill '{skill}' should be visible.");
    }

    [Then(@"the skill '([^']*)' should not be displayed")]
    public void ThenTheSkillShouldNotBeDisplayed(string skill)
    {
        Assert.That(_skillsPage.IsSkillVisible(skill), Is.False, $"Skill '{skill}' should NOT be visible.");
    }
}