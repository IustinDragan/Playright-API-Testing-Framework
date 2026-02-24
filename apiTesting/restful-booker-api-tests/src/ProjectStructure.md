apiTesting/
│
├── ProjectSetupDocumentation.md
├── TestScenarios.md
│
└── restful-booker-api-tests/
│
├── global.json
├── tests.sln
│
├── .vscode/
│ ├── launch.json
│ └── tasks.json
│
└── src/
└── RestfulBooker.ApiTests/
│
├── RestfulBooker.ApiTests.csproj
├── appsettings.json
├── playwright.config.json
├── specFlow.json
├── Startup.cs
│
├── API/
│ ├── Builders/
│ │ ├── BookingBuilder.cs
│ │ ├── BookingFactoryFromBuilder.cs
│ │ └── BookingFactoryFromJson.cs
│ │
│ ├── Entities/
│ │ ├── Booking.cs
│ │ └── BookingDates.cs
│ │
│ ├── Models/
│ │ ├── AuthenticationModels/
│ │ │ ├── AuthRequestModel.cs
│ │ │ └── AuthResponseModel.cs
│ │ │
│ │ ├── CreateBookingModels/
│ │ │ ├── CreateBookingRequestModel.cs
│ │ │ └── CreateBookingResponseModel.cs
│ │ │
│ │ └── UpdateBookingModels/
│ │ ├── UpdateBookingRequestModel.cs
│ │ └── UpdateBookingResponseModel.cs
│ │
│ └── Services/
│ ├── AuthCredentialsProvider.cs
│ ├── AuthenticationService.cs
│ ├── BookingService.cs
│ │
│ └── Interfaces/
│ ├── IAuthCredentialsProvider.cs
│ ├── IAuthentificationService.cs
│ └── IBookingService.cs
│
├── Common/
│ ├── Assertion/
│ │ └── BookingAssertions.cs
│ │
│ └── Helpers/
│ ├── Constants.cs
│ ├── SchemaValidator.cs
│ │
│ ├── JsonHelpers/
│ │ ├── JsonDeserializerHelper.cs
│ │ ├── JsonSerializerHelper.cs
│ │ └── JsonTestDataReader.cs
│ │
│ ├── JsonTestData/
│ │ ├── createValidBooking.json
│ │ └── updateValidBooking.json
│ │
│ ├── ReportsGenerator/
│ │ ├── PlaywrightReportHelper.cs
│ │ └── SpecFlowReportGenerator.cs
│ │
│ ├── ScenarioContext/
│ │ └── BookingScenarioContext.cs
│ │
│ └── TestAttributes/
│ └── SlowTestAttribute.cs
│ ── Constants.cs
│
├── Core/
│ ├── Base/
│ │ ├── TestBase.cs
│ │ └── Playwright/
│ │ └── ApiTestBase.cs
│ │
│ └── Configuration/
│ └── ConfigManager.cs
│
├── Data/
│ ├── Database/
│ │ └── SqliteService.cs
│ │
│ └── TestData/
│ └── CreateBookingSchema.json
│
├── Features/
│ └── Booking/
│ ├── CreateBooking/
│ │ ├── CreateBooking.feature
│ │ └── CreateBooking.feature.cs
│ │
│ ├── DeleteBooking/
│ │ ├── DeleteBooking.feature
│ │ └── DeleteBooking.feature.cs
│ │
│ ├── ReadBooking/
│ │ ├── GetBooking.feature
│ │ └── GetBooking.feature.cs
│ │
│ ├── UpdateBooking/
│ │ ├── UpdateBooking.feature
│ │ └── UpdateBooking.feature.cs
│ │
│ └── UpdateBookingPartial/
│ ├── UpdateBookingPartial.feature
│ └── UpdateBookingPartial.feature.cs
│
├── PlaywrightTests/
│ └── BookingTests/
│ ├── CreateBookingTests.cs
│ ├── DeleteBookingTests.cs
│ ├── GetBookingTests.cs
│ └── UpdateBookingTests.cs
├── Hooks/
│ └── SpecFlowHooks.cs
│
└── StepDefinitions/
└── Booking/
├── CreateBookingSteps.cs
├── DeleteBookingSteps.cs
├── ReadBookingSteps.cs
└── UpdateBookingSteps.cs
└── Auth/
└── AuthSteps.cs
