using Microsoft.AspNetCore.Mvc.Testing;
using PasswordValidatorApi.Models;
using System.Net;
using System.Net.Http.Json;

namespace PasswordValidatorApi.Tests.Integration.Controllers
{
    public class PasswordControllerIntegrationTests
        : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public PasswordControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Should_ReturnTrue_When_PasswordIsValid()
        {
            var response = await _client.PostAsJsonAsync("/api/password/validate",
                new { password = "AbTp9!fok" });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PasswordResponse>();

            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task Should_ReturnFalse_When_PasswordIsInvalid()
        {
            var response = await _client.PostAsJsonAsync("/api/password/validate",
                new { password = "abc" });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PasswordResponse>();

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Should_ReturnFalse_When_PasswordHasSpace()
        {
            var response = await _client.PostAsJsonAsync("/api/password/validate",
                new { password = "AbTp9!f ok" });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PasswordResponse>();

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Should_ReturnFalse_When_PasswordHasRepeatedCharacters()
        {
            var response = await _client.PostAsJsonAsync("/api/password/validate",
                new { password = "AbTp9!fokA" });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<PasswordResponse>();

            Assert.False(result.IsValid);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_When_RequestBodyIsInvalid()
        {
            var response = await _client.PostAsJsonAsync("/api/password/validate",
                new { });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
