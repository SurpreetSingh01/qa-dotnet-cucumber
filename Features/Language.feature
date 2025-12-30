Feature: LanguageProfile
    As a user, I want to manage my languages with full coverage (Add, Update, Delete).

Background:
    Given I log into the Mars portal
    And I navigate to the Language tab

# --- SCENARIO 1: CREATE (Positive Test with Multiple Data) ---
Scenario Outline: Add multiple valid languages
    # 1. CLEANUP (Pre-test): Start fresh
    Given I ensure the language '<Language>' does not exist
    
    # 2. ACTION: Add language
    When I add a new language '<Language>' with level '<Level>'
    
    # 3. VERIFY: Check it exists
    Then the language '<Language>' should be displayed
    
    # 4. CLEANUP (Post-test): Remove it
    And I delete the language '<Language>'

    Examples:
    | Language | Level            |
    | French   | Fluent           |
    | German   | Conversational   |
    | Spanish  | Native/Bilingual |

# --- SCENARIO 2: UPDATE (State Management Test) ---
Scenario Outline: Update an existing language to a new one
    # 1. CLEANUP: Ensure neither old nor new exists
    Given I ensure the language '<OldLanguage>' does not exist
    And I ensure the language '<NewLanguage>' does not exist
    
    # 2. STATE: Create the data we want to update (Independent Test!)
    And I have added the language '<OldLanguage>' with level '<OldLevel>'
    
    # 3. ACTION: Update it
    When I update the language '<OldLanguage>' to '<NewLanguage>' with level '<NewLevel>'
    
    # 4. VERIFY: New one exists, Old one is gone
    Then the language '<NewLanguage>' should be displayed
    And the language '<OldLanguage>' should not be displayed
    
    # 5. CLEANUP: Delete the new language
    And I delete the language '<NewLanguage>'

    Examples:
    | OldLanguage | OldLevel | NewLanguage | NewLevel |
    | Java        | Basic    | C#          | Fluent   |
    | Python      | Fluent   | Ruby        | Basic    |

# --- SCENARIO 3: DELETE (Functionality Test) ---
Scenario Outline: Delete an existing language
    # 1. CLEANUP: Ensure it doesn't exist
    Given I ensure the language '<Language>' does not exist
    
    # 2. STATE: Create the data we want to delete
    And I have added the language '<Language>' with level '<Level>'
    
    # 3. ACTION: Delete it
    When I delete the language '<Language>'
    
    # 4. VERIFY: It should be gone
    Then the language '<Language>' should not be displayed

    Examples:
    | Language | Level |
    | Hindi    | Basic |
    | Italian  | Fluent|