GetBooking:

Feature: Smoke Tests
@smoke-tests
a. Scenario: Create a GET request GetBookId with valid data
Given I create a "GET" request to „GetBookId”
When I receive a „JSON” response
Then I verify status code is „200”

b. Scenario: Check GetBookId response is not empty
Given I create a "GET" request to „GetBookId”
When I receive a „JSON” response
Then I verify response object is not „null”

NegativeTests – Regression (Package)
c. Scenario: Create GET request GetBookId with invalid data
Given I create a "GET" request to „invalid data”
When I receive a response
Then I verify status code is „404”

    		CreateBooking:

Feature: Smoke Tests
a. Create a POST request CreateBooking with valid data
Given I create a "POST" request to „CreateBooking”
When I receive a „JSON” response
Then I verify status code is „201”

b. Scenario: Verify CreateBooking saves correct data
Given I create a "POST" request to "CreateBooking" with valid booking data
When I receive a JSON response
Then I verify the response contains the right booking details values

Feature: negative scenarios
a. Scenario: CreateBooking with missing mandatory fields
Given I create a "POST" request to "CreateBooking" with incomplete booking data
When I receive a response
Then I verify status code is "400"

b. Scenario: CreateBooking with invalid date format
Given I create a "POST" request to "CreateBooking" with an invalid date format
When I receive a response
Then I verify status code is "422"

    		UpdateBooking:

Positive Scenarios
a. Scenario: UpdateBooking with valid ID and valid data
Given I create a "PUT" request to "UpdateBooking" with a valid booking ID and valid update data
When I receive a JSON response
Then I verify status code is "200"

b. Scenario: Verify updated booking returns proper values
Given I create a "PUT" request to "UpdateBooking" with correct booking modifications
When I receive a JSON response
Then I verify updated fields have new values

Negative Scenarios
a. Scenario: UpdateBooking with invalid booking ID
Given I create a "PUT" request to "UpdateBooking" with an invalid booking ID
When I receive a response
Then I verify status code is "404"

b. Scenario: UpdateBooking with empty request body
Given I create a "PUT" request to "UpdateBooking" with no update data
When I receive a response
Then I verify status code is "400"

    		PartialUpdateBooking:

Positive Scenarios
a. Scenario: PartialUpdateBooking updates only one field
Given I create a "PATCH" request to "PartialUpdateBooking" with a valid booking ID and one valid field to update
When I receive a JSON response
Then I verify status code is "200"

b. Scenario: Verify partial update keeps other fields unchanged
Given I create a "PATCH" request to "PartialUpdateBooking"
When I receive a JSON response
Then I verify unchanged fields remain the same

Negative Scenarios
a. Scenario: PartialUpdateBooking with invalid booking ID
Given I create a "PATCH" request to "PartialUpdateBooking" with an invalid booking ID
When I receive a response
Then I verify status code is "404"

b. Scenario: PartialUpdateBooking with unsupported field
Given I create a "PATCH" request to "PartialUpdateBooking" with an unexisted field
When I receive a response
Then I verify status code is "400"

    		DeleteBooking

Positive Scenarios
a. Scenario: DeleteBooking with valid booking ID
Given I create a "DELETE" request to "DeleteBooking" with a valid booking ID
When I receive a response
Then I verify status code is "204"

b. Scenario: Verify deleted booking no longer exists
Given I create a "DELETE" request to "DeleteBooking" with valid booking ID
When I receive a response
Then I verify a GET request returns "404"

Negative Scenarios
a. Scenario: DeleteBooking with non-existent booking ID
Given I create a "DELETE" request to "DeleteBooking" with a non-existing booking ID
When I receive a response
Then I verify status code is "404"

b. Scenario: DeleteBooking without providing a booking ID
Given I create a "DELETE" request to "DeleteBooking" without specifying a booking ID
When I receive a response
Then I verify status code is "400"
