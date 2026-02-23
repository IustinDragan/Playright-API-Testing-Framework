@api @DeleteBooking @smoke
Feature: Delete Booking API

      Scenario: Delete an existing booking
            Given I am authenticated
            And I have a valid booking payload
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            When I send a DELETE request to delete the created booking
            Then the response status code should be 201

      @skip @flaky
      Scenario: Delete a specific booking by Id
            Given I am authenticated
            When I send a DELETE request to delete the booking with id 1255
            Then the response status code should be 201

      Scenario: Delete booking without authorization
            Given I have a valid booking payload
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            When I send a DELETE request without being authenticated to delete the created booking
            Then the response status code should be 403

      Scenario: Delete booking with invalid authorization
            Given I have an invalid authorization token
            And I have a valid booking payload
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            When I send a DELETE request to delete the created booking
            Then the response status code should be 403

      Scenario: Delete unexisting booking
            Given I am authenticated
            And I have a valid booking payload
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            When I send a DELETE request to delete the created booking
            Then the response status code should be 201
            When I send a DELETE request to delete the created booking
            Then the response status code should be 405