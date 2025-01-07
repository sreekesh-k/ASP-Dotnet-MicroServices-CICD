using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class ApiAccessibilityTests
{
    private readonly HttpClient _gatewayClient;
    private readonly HttpClient _foodApiClient;
    private readonly HttpClient _studentsApiClient;

    public ApiAccessibilityTests()
    {
        // Clients pointing to Ocelot Gateway and individual APIs
        _gatewayClient = new HttpClient { BaseAddress = new Uri("http://localhost:8080") };
        _foodApiClient = new HttpClient { BaseAddress = new Uri("http://localhost:5001/api/food") };
        _studentsApiClient = new HttpClient { BaseAddress = new Uri("http://localhost:5002/api/students") };
    }

    [Fact]
    public async Task OcelotGateway_FoodsEndpoint_IsAccessible()
    {
        // Act
        var response = await _gatewayClient.GetAsync("/foods");

        // Assert
        Assert.True(response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task OcelotGateway_StudentsEndpoint_IsAccessible()
    {
        // Act
        var response = await _gatewayClient.GetAsync("/students");

        // Assert
        Assert.True(response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task FoodApi_Endpoint_IsAccessible()
    {
        // Act
        var response = await _foodApiClient.GetAsync("");

        // Assert
        Assert.True(response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task StudentsApi_Endpoint_IsAccessible()
    {
        // Act
        var response = await _studentsApiClient.GetAsync("");

        // Assert
        Assert.True(response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }
}
