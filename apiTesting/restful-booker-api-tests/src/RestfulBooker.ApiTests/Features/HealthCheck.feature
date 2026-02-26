Feature: Ping - Health Check

      @smoke @ping
      Scenario: API is up and ready - HealthCheck endpoint
            When I send a GET request to healthCheck endpoint
            Then the response status code should be 201
            And the response header "content-type" should contain "text/plain"
            And the response time should be less than 2000 ms

      @negative @ping
      Scenario: Wrong method on HealthCheck endpoint
            When I send a POST request to healthCheck endpoint
            Then the response status code should be 404