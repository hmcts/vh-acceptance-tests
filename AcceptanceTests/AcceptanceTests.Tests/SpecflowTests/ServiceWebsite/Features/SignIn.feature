Feature: Service Website Sign In
    As an Invidual or Representative
    I would like to sign in to VH-Service Web
    So that I can complete suitability Questionnaire

@smoketest
@serviceWebsite
@signIn
@individual
@representative
Scenario Outline: Users with no pending questionnaires to complete when signing in to the Service Website is redirected to Video Website
    Given I am registered as '<role>' in the Video Hearings Azure AD
    And I don't have any pending questionnaires to complete
    When I sign in to the 'Service Website' using my account details
    Then I am redirected to the 'Video Website' automatically
    Examples:
    |role|
    |Individual|
    |Representative|

@pending
@serviceWebsite
@signIn
@individual
@representative
Scenario Outline: User with no pending questionnaires to complete but dropped out when signing in to the Service Website is redirected to Video Website
    Given I am registered as 'Individual' in the Video Hearings Azure AD
    And I don't have any pending questionnaires to complete
    But I answered 'No' to '<question_title>' question
    When I sign in to the 'Service Website' using my account details
    Then I am redirected to the 'Video Website' automatically
    Examples:
    |question_title|
    |Do you have a computer?|
    |Does your computer have a camera and microphone?|
    |Will you have access to the Internet?|
     