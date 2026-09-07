using System.Net.Http.Json;
using System.Text.Json;
using SupportTicketApp.Client.Models;

namespace SupportTicketApp.Client.Services;

public class TicketApiService : ITicketApiService
{
    private readonly HttpClient _httpClient;

    public TicketApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<TicketDto>> GetAllAsync()
    {
        using HttpResponseMessage response = await _httpClient.GetAsync("api/tickets");

        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Request: {response.RequestMessage?.RequestUri}. "
                    + $"Status: {(int)response.StatusCode} {response.StatusCode}. "
                    + $"Response: {content}"
            );
        }

        try
        {
            return JsonSerializer.Deserialize<List<TicketDto>>(
                    content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                ) ?? new List<TicketDto>();
        }
        catch (JsonException)
        {
            string preview = content.Length > 200 ? content[..200] : content;

            throw new InvalidOperationException(
                $"The API did not return JSON. "
                    + $"Request: {response.RequestMessage?.RequestUri}. "
                    + $"Response begins with: {preview}"
            );
        }
    }

    public async Task<TicketDto> CreateAsync(CreateTicketDto ticket)
    {
        using HttpResponseMessage response = await _httpClient.PostAsJsonAsync(
            "api/tickets",
            ticket
        );

        if (!response.IsSuccessStatusCode)
        {
            string error = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException($"The ticket could not be created. {error}");
        }

        return await response.Content.ReadFromJsonAsync<TicketDto>()
            ?? throw new InvalidOperationException("The API returned an empty response.");
    }

    public async Task<List<LookupDto>> GetCustomersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LookupDto>>("api/lookups/customers")
            ?? new();
    }

    public async Task<List<LookupDto>> GetStatusesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LookupDto>>("api/lookups/statuses") ?? new();
    }

    public async Task<List<LookupDto>> GetTechniciansAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<LookupDto>>("api/lookups/technicians")
            ?? new();
    }

    public async Task<TicketDto?> GetByIdAsync(int ticketId)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync($"api/tickets/{ticketId}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TicketDto>();
    }

    public async Task<TicketDto> UpdateAsync(int ticketId, UpdateTicketDto ticket)
    {
        using HttpResponseMessage response = await _httpClient.PutAsJsonAsync(
            $"api/tickets/{ticketId}",
            ticket
        );

        if (!response.IsSuccessStatusCode)
        {
            string error = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException($"The ticket could not be updated. {error}");
        }

        return await response.Content.ReadFromJsonAsync<TicketDto>()
            ?? throw new InvalidOperationException("The API returned an empty response.");
    }

    public async Task<bool> DeleteAsync(int ticketId)
    {
        using HttpResponseMessage response = await _httpClient.DeleteAsync(
            $"api/tickets/{ticketId}"
        );

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        if (!response.IsSuccessStatusCode)
        {
            string error = await response.Content.ReadAsStringAsync();

            throw new InvalidOperationException($"The ticket could not be deleted. {error}");
        }

        return true;
    }
}
