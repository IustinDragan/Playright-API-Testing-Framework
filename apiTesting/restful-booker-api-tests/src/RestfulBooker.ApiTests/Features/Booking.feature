@api @booking @smoke
Feature: Booking API

  Scenario: Create a booking successfully
    Given I have a valid booking payload
    When I send a POST request to create a booking
    Then the response status code should be 200
    And the booking id should be returned

  Scenario: Get all booking ids
    When I request all booking IDs
    Then the response status code should be 200

  Scenario: Get booking with specific id
    When I request a specific booking with id 10
    Then the response status code should be 200

  Scenario: Validate booking format
    When I request a specific booking with id 9
    Then the response should be in JSON format

  Scenario: Update an existing booking
    Given I am authenticated
    And I have a valid booking payload
    And I have a valid booking payload for update
    When I send a POST request to create a booking
    Then the response status code should be 200
    And the booking id should be returned
    When I send a PUT request to update the created booking
    Then the response status code should be 200
    And booking values should be updated

  #Step implemented for specific booking updates:
  # When I send a PUT request to update the booking with id 33

  Scenario: Delete an existing booking
    Given I am authenticated
    And I have a valid booking payload
    When I send a POST request to create a booking
    Then the response status code should be 200
    And the booking id should be returned
    When I send a DELETE request to delete the created booking
    Then the response status code should be 201

#Step implemented for specific booking delete:
#When I send a DELETE request to delete the booking with id 21