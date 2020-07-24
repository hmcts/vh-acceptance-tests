Feature: Allocations
	In order to manage test users
	As an api service
	I want to be able to create, retrieve and delete allocations

	Scenario: Get allocation by user id - OK
	Given I have a user with an allocation
	And I have a get allocation by user id request with a valid user id
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And the response contains the allocation details

	Scenario: Get allocation by user id - NotFound
	Given I have a user with an allocation
	And I have a get allocation by user id request with a nonexistent user id
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False

	Scenario: Create allocation - Created
	Given I have a user
	And I have a valid create allocation request for a valid user
	When I send the request to the endpoint
	Then the response should have the status Created and success status True
	And the response contains the allocation details

	Scenario: Create allocation - NotFound
	Given I have a valid create allocation request for a nonexistent user
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False

	Scenario: Create allocation for an existing allocation - Conflict
	Given I have a user
	And I have a valid create allocation request for a valid user
	When I send the request to the endpoint
	Then the response should have the status Created and success status True
	When I send the same request twice
	Then the response should have the status Conflict and success status False

	Scenario: Delete allocation - NoContent
	Given I have a user with an allocation
	And I have a delete allocation request with a valid user id
	When I send the request to the endpoint
	Then the response should have the status NoContent and success status True

	Scenario: Delete allocation - NotFound
	Given I have a delete allocation request with a nonexistent user id
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False

	Scenario: Allocate user by user id - OK
	Given I have a user with an allocation who is unallocated
	And I have an allocate by user id request for a valid user
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And the user details should be retrieved
	And the user should be allocated
	
	Scenario: Allocate user by user id - NotFound
	Given I have a user with an allocation who is unallocated
	And I have an allocate by user id request for a nonexistent user
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False

	Scenario Outline: Allocate user by user type and application - No Users Exist - OK
	Given I have a Allocate user by user type <UserType> and application request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And the user details for the newly created <UserType> user during allocation should be retrieved
	And the user should be allocated
	Examples: 
	| UserType             |
	| CaseAdmin            |
	| VideoHearingsOfficer |
	| Judge                |
	| Individual           |
	| Representative       |
	| Observer             |
	| PanelMember          |

	Scenario Outline: Allocate user by user type and application - No Users Available - OK
	Given I have a <UserType> user with an allocation who is allocated
	And I have a Allocate user by user type <UserType> and application request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And the user details for the newly created <UserType> user during allocation should be retrieved
	And the user should be allocated
	Examples: 
	| UserType             |
	| CaseAdmin            |
	| VideoHearingsOfficer |
	| Judge                |
	| Individual           |
	| Representative       |
	| Observer             |
	| PanelMember          |

	Scenario Outline: Allocate user by user type and application - Unallocated users available - OK
	Given I have a <UserType> user with an allocation who is unallocated
	And I have a Allocate user by user type <UserType> and application request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And the user details should be retrieved
	And the user should be allocated
	Examples: 
	| UserType             |
	| CaseAdmin            |
	| VideoHearingsOfficer |
	| Judge                |
	| Individual           |
	| Representative       |
	| Observer             |
	| PanelMember          |

	Scenario: Unallocate allocated user by username - OK
	Given I have a user with an allocation who is allocated
	And I have another user with an allocation who is allocated
	And I have a valid unallocate users by username request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And a list of user allocation details should be retrieved for the unallocated users

	Scenario: Unallocate unallocated user by username - OK
	Given I have a user with an allocation who is unallocated
	And I have another user with an allocation who is unallocated
	And I have a valid unallocate users by username request
	When I send the request to the endpoint
	Then the response should have the status OK and success status True
	And a list of user allocation details should be retrieved for the unallocated users

	Scenario: Unallocate user - NotFound
	Given I have a valid unallocate users by username request for a nonexistent user
	When I send the request to the endpoint
	Then the response should have the status NotFound and success status False