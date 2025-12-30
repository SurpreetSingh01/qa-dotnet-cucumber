Feature: LoginFunctionality

Scenario: Perform a successful login
    Given I navigate to the Mars home page
    When I click on Sign In
    And I enter valid credentials
    Then I should be logged in