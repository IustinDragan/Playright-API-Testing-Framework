@api @DeleteBooking @smoke
Feature: Delete Booking API

      Background:
            Given I have a valid authentication token
            And I have created a booking to delete

      Scenario: Delete an existing booking
            When I send a DELETE request to delete the created booking
            Then the response status code should be 201
            And the booking should be deleted from the system

      @skip @flaky
      Scenario: Delete a specific booking by Id
            When I send a DELETE request to delete the booking with id 1
            Then the response status code should be 201

      Scenario: Delete booking without authorization
            When I send a DELETE request without being authenticated to delete the created booking
            Then the response status code should be 403

      Scenario: Delete booking with invalid authorization
            Given I have an invalid authorization token
            And I have created a booking to delete
            When I send a DELETE request to delete the created booking
            Then the response status code should be 403

      Scenario: Delete unexisting booking
            When I send a DELETE request to delete the created booking
            Then the response status code should be 201
            When I send a DELETE request to delete the created booking
            Then the response status code should be 405