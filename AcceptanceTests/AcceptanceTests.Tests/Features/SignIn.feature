Feature: Login
    As a VH Officer
    I would like to login to the Admin Website
    So that I can book a hearing/view Questionnaire results

@smoketest
Scenario Outline: VH Officers are authorised to sign in to the Admin Website
    Given I am registered as 'VH Officer' in the Video Hearings Azure AD
    When I sign in to the 'Admin Website' using my account details 
    Then '<panel_title>' panel is displayed 
    Examples:
    |panel_title|
    |Book a video hearing|
    |Questionnaire results|

@smoketest
Scenario Outline: Case Admins are authorised to sign in to the Admin Website
    Given I am registered as 'Case Admin' in the Video Hearings Azure AD
    When I sign in to the 'Admin Website' using my account details 
    Then '<panel_title>' panel is displayed 
    Examples:
    |panel_title|
    |Book a video hearing|

@smoketest
Scenario: Non-Admin users are not authorised to sign in to the Admin Website
    Given I am registered as 'Individual' in the Video Hearings Azure AD
    When I sign in to the 'Admin Website' using my account details 
    Then I see a page with the 'unauthorised' message and the content below:
    """
        You are not authorised to use this service
        It looks like you are not registered for this service.

        If you think this is a mistake and you need to speak to someone, please contact us using the options below.

        Contact us for help
    """
