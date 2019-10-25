Feature: Service Website Sign In
    As an Invidual or Representative
    I would like to sign in to VH-Service Web
    So that I can complete suitability Questionnaire

@smoketest
Scenario Outline: Person with no upcoming hearings when signing in to the Service Website is redirected to Video Website
    Given I am registered as '<role>' in the Video Hearings Azure AD
    But I don't have any upcoming video hearings scheduled
    When I sign in to the 'Service Website' using my account details 
    Then I am redirected to 'Video Website' automatically
    Examples:
    |role|
    |Individual|
    |Representative|

# TODO: clarify
@VIH-4577 @Individual
Scenario: Individual without computer submits questionnaire is redirected to Video Web
    Given Individual participant has already submitted questionnaire but drops out
    When 'Individual' with no upcoming hearings logs in with valid credentials
    Then Participant should be redirected to Video Web    