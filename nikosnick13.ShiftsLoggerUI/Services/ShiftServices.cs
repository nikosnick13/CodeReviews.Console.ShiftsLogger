using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Spectre.Console.AnsiConsole;
using System.Net.Http.Json;
using nikosnick13.ShiftsLoggerUI.Models;
using Spectre.Console;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace nikosnick13.ShiftsLoggerUI.Services;

internal class ShiftServices
{
    private readonly HttpClient _client;

    public ShiftServices(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7290/api/Shift/");
    }

    public async Task<IEnumerable<Shift>> GetAllShiftsAsync()
    {

        try
        {
            var shifts = await _client.GetFromJsonAsync<IEnumerable<Shift>>("");
            return shifts ?? Enumerable.Empty<Shift>();
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"API Error: {ex.Message}");
            return Enumerable.Empty<Shift>();
        }
    }

    public async Task<Shift?> GetOneShiftAsync(int id)
    {
        try
        {
            var response = await _client.GetAsync($"{id}");

            if (!response.IsSuccessStatusCode)
            {

                MarkupLine($"[red]Faild to get the id {response.StatusCode} - {response.ReasonPhrase}[/]");
            }

            var shift = await response.Content.ReadFromJsonAsync<Shift>();

            if (shift == null)
            {
                MarkupLine($"[yellow]No shift found with Id {id}.[/]");
                return null;
            }
            MarkupLine($"[green]Found shift for {shift.WorkerName} {shift.WorkerLastName}[/]");
            MarkupLine($"Start: {shift.StartTime}, End: {shift.EndTime}");

            return shift;
        }
        catch (HttpRequestException ex)
        {
            WriteLine($" API Error: {ex.Message}");
            return null;
        }
    }

    public async Task AddShiftAsync(Shift shift)
    {
        try
        {
            await _client.PostAsJsonAsync("", shift);
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"API Error: {ex.Message}");
        }
    }

    public async Task<Shift?> GetByIdAsync(int id)
    {
        try
        {
            var response = await _client.GetAsync($"{id}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MarkupLine($"[yellow]Shift with Id {id} was not found.[/]");
                    return null;
                }
                MarkupLine($"[red]API Error: {response.StatusCode} - {response.ReasonPhrase}[/]");
                return null;
            }

            var shift = await response.Content.ReadFromJsonAsync<Shift>();
            return shift;
        }
        catch (HttpRequestException ex)
        {
            MarkupLine($"[red]API Error: {ex.Message}[/]");
            return null;
        }
    }

    public async Task DeleteShiftAsync(int id)
    {
        try
        {
            var response = await _client.DeleteAsync($"{id}");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MarkupLine($"[yellow]Shift with Id {id} was not found.[/]");

                }
                MarkupLine($"[red]API Error: {response.StatusCode} - {response.ReasonPhrase}[/]");
            }
        }
        catch (HttpRequestException ex)
        {
            WriteLine($"API Error: {ex.Message}");
        }
    }

    public async Task<bool> UpdateShiftAsync(Shift shift)
    {
        try
        {
            var response = await _client.PutAsJsonAsync($"{shift.Id}", shift);

            if (!response.IsSuccessStatusCode)
            {
                MarkupLine($"[red]Failed to update shift: {response.StatusCode} - {response.ReasonPhrase}[/]");
                return false;
            }

            MarkupLine($"[green]Shift updated successfully on API![/]");
            return true;
        }
        catch (HttpRequestException ex)
        {
            MarkupLine($"[red]API Error: {ex.Message}[/]");
            return false;
        }
    }
}
