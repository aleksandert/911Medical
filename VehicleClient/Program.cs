// See https://aka.ms/new-console-template for more information
using IdentityModel.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

const string baseUrl = "https://localhost:44387";

const string username = "aleksandert@gmail.com", password = "Test!123";

using var client = new HttpClient();

var token = await GetTokenAsync(client, username, password);

var connection = new HubConnectionBuilder()
    .WithUrl($"{baseUrl}/vehiclehub", options =>
    {
        options.AccessTokenProvider = () => Task.FromResult<string?>(token);
    })
    .WithAutomaticReconnect()
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Debug);
    })
    .Build();

await connection.StartAsync();

Console.WriteLine("VehicleId?");

if (int.TryParse(Console.ReadLine(), out int vehicleId))
{
    await connection.SendAsync("VehicleConnect", vehicleId);
}

Console.ReadLine();

await connection.StopAsync();

await connection.DisposeAsync();



static async Task<string> GetTokenAsync(HttpClient client, string email, string password)
{
    // Retrieve the OpenIddict server configuration document containing the endpoint URLs.
    var configuration = await client.GetDiscoveryDocumentAsync(baseUrl);

    if (configuration.IsError)
    {
        throw new Exception($"An error occurred while retrieving the configuration document: {configuration.Error}");
    }

    var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
    {
        Address = configuration.TokenEndpoint,
        UserName = email,
        Password = password,
        ClientId = "911Medical_Api",
        ClientSecret = "003163EA-BED9-4DA8-9F80-EDB285D3019A"
    });

    if (response.IsError)
    {
        throw new Exception($"An error occurred while retrieving an access token: {response.Error}");
    }

    return response.AccessToken;
}