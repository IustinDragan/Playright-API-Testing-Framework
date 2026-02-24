@api @CreateBooking @smoke
Feature: Create Booking API

      Background:
            Given the booking API endpoint is available

      Scenario: Create a booking successfully
            Given I prepare a valid booking payload
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            And I save the response to database


      @slow
      Scenario: Validate booking format
            When I request a specific booking with id 9
            Then the response should be in JSON format


      @regression @slow(120000)
      Scenario: Create booking with invalid payload
            Given I prepare an invalid booking payload
            When I send a POST request to create a booking
            Then the response status code should be 500