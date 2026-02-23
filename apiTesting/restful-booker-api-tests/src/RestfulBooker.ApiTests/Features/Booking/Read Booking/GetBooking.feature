@api @GetBooking @smoke
Feature: Get Booking API

      Scenario: Get all booking ids
            When I request all booking IDs
            Then the response status code should be 200

      Scenario: Get booking with specific id
            When I request a specific booking with id 10
            Then the response status code should be 200