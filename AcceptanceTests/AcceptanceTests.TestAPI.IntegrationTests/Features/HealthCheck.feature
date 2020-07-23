Feature: HealthCheck
	In order to determine the availability of the test api
	As an api service
	I want to be able to retrieve the test api status

Scenario: HealthCheck - OK
    Given I have a get health request
    When I send the request to the endpoint
    Then the response should have the status ok and success status True
    And the application version should be retrieved