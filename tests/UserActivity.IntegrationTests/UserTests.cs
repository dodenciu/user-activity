using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using UserActivity.Application.CreateUser;

namespace UserActivity.IntegrationTests;

public class UserTests : IClassFixture<IntegrationTestWebAppFactory>
{
    private readonly IntegrationTestWebAppFactory _factory;

    public UserTests(IntegrationTestWebAppFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task CreateUserAsync_InvalidPayload_ReturnsError()
    {
        // Arrange
        const string payload = "<malformed/>";

        using var request = new HttpRequestMessage(HttpMethod.Post, "api/users");
        request.Content = new StringContent(
            payload,
            Encoding.UTF8,
            new MediaTypeHeaderValue(MediaTypeNames.Application.Json)
        );

        HttpClient client = _factory.CreateClient();

        // Act
        using HttpResponseMessage response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task CreateUserAsync_ValidPayload_ReturnsNoContent()
    {
        // Arrange
        var payload = new CreateUserCommand(
            "Patrick",
            "patrick@gmail.com",
            "OneBigPassword",
            new CreateAddressCommand("Germany", "Berlin", "Street 1"));
        string jsonPayload = JsonSerializer.Serialize(payload);

        using var request = new HttpRequestMessage(HttpMethod.Post, "api/users");
        request.Content = new StringContent(
            jsonPayload,
            Encoding.UTF8,
            new MediaTypeHeaderValue(MediaTypeNames.Application.Json)
        );

        HttpClient client = _factory.CreateClient();

        // Act
        using HttpResponseMessage response = await client.SendAsync(request);

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
