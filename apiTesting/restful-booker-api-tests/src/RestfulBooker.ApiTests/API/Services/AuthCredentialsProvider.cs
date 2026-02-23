using Microsoft.Extensions.Configuration;
using RestfulBooker.ApiTests.Common;
using RestfulBooker.ApiTests.Models;

namespace RestfulBooker.ApiTests.API;

public class AuthCredentialsProvider : IAuthCredentialsProvider
{
      private readonly IConfiguration _configuration;

      public AuthCredentialsProvider(IConfiguration configuration)
      {
            _configuration = configuration;
      }
      public AuthRequestModel GetAdminCredentials()
      {
            var username = _configuration[Constants.AuthUsername];
            var password = _configuration[Constants.AuthPassword];

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                  throw new InvalidOperationException("Missing Auth credentials in configuration file: [appsettings.json]");

            return new AuthRequestModel { Username = username, Password = password };
      }
}