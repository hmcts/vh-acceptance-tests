Feature: Users
	In order to manage test users
	As an api service
	I want to be able to create, retrieve and delete users

Scenario: Get user details by id - OK
	Given I have a user
	And I have a valid get user details by id request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And the user details should be retrieved

Scenario: Get user details by id - Not Found
	Given I have a get user details by id request with a nonexistent user id
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False

Scenario: Get All Users by UserType and Application - OK
	Given I have a user with user type Judge
	And I have another user with user type Judge
	And I have a user with user type Individual
	And I have a valid get all users by user type Judge and application request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And a list of user details for the given user type and application should be retrieved

Scenario: Get All Users by UserType and Application - NotFound
	Given I have a valid get all users by user type None and application request
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False

Scenario Outline: Create user - Created
	Given I have a valid create user request for a <UserType>
	When I send the request to the endpoint
	Then the response should have the status Created and success status True
	And the response contains the new user details
	Examples: 
	| UserType             |
	| CaseAdmin            |
	| VideoHearingsOfficer |
	| Judge                |
	| Individual           |
	| Representative       |
	| Observer             |
	| PanelMember          |

Scenario: Create user numbers iterate - Created
	Given I have a valid create user request for a Judge
	When I send the create user request twice
	Then the user numbers should be incremented

Scenario: Get user number iterated - OK
	Given I have a valid get user number iterated request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And the iterated number should be retrieved

Scenario: Delete User - NoContent
	Given I have a user
	And I have a valid delete user by user id request
	When I send the request to the endpoint
	Then the response should have the status NoContent and success status True

Scenario: Delete User - NotFound
	Given I have a valid delete user by user id request for a nonexistent user
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False