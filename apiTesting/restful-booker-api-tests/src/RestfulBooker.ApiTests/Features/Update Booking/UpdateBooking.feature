@api @UpdateBooking @smoke
Feature: Update Booking API

      Background:
            Given I have a valid authentication token

      Scenario Outline: API should return 200 OK when perform
            And I prepare a valid booking payload
            And I prepare a valid booking payload for <updateType>
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            When I send a <method> request to update the created booking
            Then the response status code should be 200
            And booking values should be updated
            Examples:
                  | caseName                               | method | updateType     |
                  | Update for an existing booking         | PUT    | update         |
                  | Partial update for an existing booking | PATCH  | partial update |

      Scenario Outline: API should return 200 OK when perform an
            And I prepare a valid booking payload for update
            When I send a <method> request to update the booking with id 28
            Then the response status code should be 200
            And booking values should be updated
            Examples:
                  | caseName                                    | method |
                  | Update for a specific booking by Id         | PUT    |
                  | Partial update for a specific booking by Id | PATCH  |

      @api @booking @update @negative
      Scenario Outline: API should return 403 Forbidden when perform an
            Given I prepare a valid booking payload
            And I prepare a valid booking payload for <updateType>
            When I send a POST request to create a booking
            Then the response status code should be 200
            And the booking id should be returned
            When I send a <method> request with <authCase> authorization to update the created booking
            Then the response status code should be 403
            Examples:
                  | caseName                          | method | authCase | updateType     |
                  | Update with missing token         | PUT    | missing  | update         |
                  | Update with invalid token         | PUT    | invalid  | update         |
                  | Partial update with missing token | PATCH  | missing  | partial update |
                  | Partial update with invalid token | PATCH  | invalid  | partial update |

      @api @PartialUpdateBooking @negative
      Scenario Outline: API should return 405 Method Not Allowed when perform an
            And I prepare a valid booking payload for <updateType>
            When I send a <method> request to <updateType> the booking with id 999999
            Then the response status code should be 405
            Examples:
                  | caseName                                 | method | updateType     |
                  | Update for an unexisting booking         | PUT    | update         |
                  | Partial update for an unexisting booking | PATCH  | partial update |