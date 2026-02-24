@api @UpdateBooking @smoke
Feature: Update Booking API

      Background:
            Given I have a valid authentication token

      Scenario: Update an existing booking
            And I prepare a valid booking payload
            And I prepare a valid booking payload for update
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            When I send a PUT request to update the created booking
            Then the response status code should be 200
            And booking values should be updated

      @skip
      Scenario: Update a specific booking by Id
            And I prepare a valid booking payload for update
            When I send a PUT request to update the booking with id 28
            Then the response status code should be 200
            And booking values should be updated