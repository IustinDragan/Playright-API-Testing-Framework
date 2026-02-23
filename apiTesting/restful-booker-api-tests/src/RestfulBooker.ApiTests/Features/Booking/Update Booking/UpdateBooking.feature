@api @UpdateBooking @smoke
Feature: Update Booking API

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

      @skip
      Scenario: Update a specific booking by Id
            Given I am authenticated
            And I have a valid booking payload for update
            When I send a PUT request to update the booking with id 28
            Then the response status code should be 200
            And booking values should be updated