Feature: SkillsProfile
    As a user, I want to manage my skills with full coverage (Add, Update, Delete).

Background:
    Given I log into the Mars portal
    And I navigate to the Skills tab

# --- SCENARIO 1: CREATE ---
Scenario Outline: Add multiple valid skills
    Given I ensure the skill '<Skill>' does not exist
    When I add a new skill '<Skill>' with level '<Level>'
    Then the skill '<Skill>' should be displayed
    And I delete the skill '<Skill>'

    Examples:
    | Skill      | Level        |
    | Git        | Expert       |
    | Jenkins    | Intermediate |
    | Leadership | Beginner     |

# --- SCENARIO 2: UPDATE ---
Scenario Outline: Update an existing skill
    Given I ensure the skill '<OldSkill>' does not exist
    And I ensure the skill '<NewSkill>' does not exist
    And I have added the skill '<OldSkill>' with level '<OldLevel>'
    When I update the skill '<OldSkill>' to '<NewSkill>' with level '<NewLevel>'
    Then the skill '<NewSkill>' should be displayed
    And the skill '<OldSkill>' should not be displayed
    And I delete the skill '<NewSkill>'

    Examples:
    | OldSkill | OldLevel | NewSkill | NewLevel |
    | Painting | Beginner | Drawing  | Expert   |
    | Boxing   | Expert   | Yoga     | Beginner |

# --- SCENARIO 3: DELETE ---
Scenario Outline: Delete an existing skill
    Given I ensure the skill '<Skill>' does not exist
    And I have added the skill '<Skill>' with level '<Level>'
    When I delete the skill '<Skill>'
    Then the skill '<Skill>' should not be displayed

    Examples:
    | Skill    | Level        |
    | Cooking  | Intermediate |
    | Swimming | Expert       |