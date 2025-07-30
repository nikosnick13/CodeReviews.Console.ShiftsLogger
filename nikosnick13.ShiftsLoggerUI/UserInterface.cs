
using static System.Console;
using static Spectre.Console.AnsiConsole;
using Spectre.Console;
using static nikosnick13.ShiftsLoggerUI.Enums.MenuItems;
using nikosnick13.ShiftsLoggerUI.Services;
using nikosnick13.ShiftsLoggerUI.Models;

namespace nikosnick13.ShiftsLoggerUI;

internal class UserInterface
{
    private readonly ShiftServices _shiftServices;

    public UserInterface(ShiftServices shiftServices)
    {
        _shiftServices = shiftServices;
    }

    public async Task MainMenu()
    {
        bool isAppRunning = true;

        while (isAppRunning)
        {
            var options = AnsiConsole.Prompt(new SelectionPrompt<MenuOptions>()
                      .Title("What you like to do ")
                      .AddChoices(
                           MenuOptions.GetAllShifts,
                           MenuOptions.AddShift,
                           MenuOptions.GetOneShift,
                           MenuOptions.UpdateShift,
                           MenuOptions.DeleteShift,
                           MenuOptions.Exit));

            switch (options)
            {
                case MenuOptions.GetAllShifts:
                    await DisplayAllShifts();
                    Utility.DisplayReturnMessage("[yellow]Prees any key to return to Main Menu.[/]");
                     ReadKey();
                    await Utility.ShowLoadingStatus();
                    break;
                case MenuOptions.AddShift:
                    await CrateAShift();
                    break;
                case MenuOptions.GetOneShift:
                    await DisplayOneShift();
                    break;
                case MenuOptions.UpdateShift:
                    await UpdateAShift();
                    break;
                case MenuOptions.DeleteShift:
                    await DeleteAShift();
                    break;
                case MenuOptions.Exit:
                    await Utility.ShowExitStatus();
                    isAppRunning = false;
                    Environment.Exit(0);
                    return; 
                    
            }
        }
    }

    private async Task CrateAShift()
    {
        string takeFirstName = await GetFirstAndLastName("\nType the first name of employ. Type 0 to return to main menu:");
        string takeLastName = await GetFirstAndLastName("\nType the last name of employ. Type 0 to return to main menu:");
        string takeStartDurtation = await GetDuration("\nInsert shift [green]start time[/] (Format: HH:mm). Type 0 to return to main menu:");
        string takeEndDurtation = await GetDuration("\nInsert shift [green]end time[/] (Format: HH:mm). Type 0 to return to main menu:");

        DateTime startTimetDurtation = DateTime.ParseExact(takeStartDurtation, "HH:mm", null);
        DateTime endTimetDurtation = DateTime.ParseExact(takeEndDurtation, "HH:mm", null);

        var newShift = new Shift
        {
            WorkerName = takeFirstName,
            WorkerLastName = takeLastName,
            StartTime = startTimetDurtation,
            EndTime = endTimetDurtation
        };

        await _shiftServices.AddShiftAsync(newShift);
        Utility.DisplayReturnMessage("[green]Shift successfully created![/]");
        await Utility.ShowLoadingStatus();
    }

    public async Task<string> GetFirstAndLastName(string? msg)
    {
        Utility.DisplayReturnMessage($"[yellow]{msg}[/]");
        string? userInput = ReadLine()?.Trim();


        if (userInput == "0")
        {
            await Utility.ShowLoadingStatus();
            await MainMenu();
            return "";

        }

        while (!Validation.isNameValid(userInput) || string.IsNullOrEmpty(userInput))
        {
            userInput = ReadLine();
            if (userInput == "0") 
            {
                await Utility.ShowLoadingStatus();
                await MainMenu();
                return "";
            }
        }
         
        return userInput;
    }

    public async Task<string> GetDuration(string msg)
    {
        Utility.DisplayReturnMessage($"[yellow]{msg}[/]");
        string? userInput = ReadLine()?.Trim();

        if (userInput == "0") await MainMenu();
        while (!Validation.isValidDuration(userInput) || string.IsNullOrEmpty(userInput))
        {
            userInput = ReadLine();
            if (userInput == "0") await MainMenu();
        }
        return userInput;
    }

    private async Task DeleteAShift()
    {
        await DisplayAllShifts();
        Utility.DisplayReturnMessage("\n[yellow]Type the Id you want to delete.Type 0 to return to Main menu:[/]");

        string? userInput = ReadLine()?.Trim();
        if (userInput == "0") 
            {
                await Utility.ShowLoadingStatus();
                await MainMenu();
                return;
            
            }

        while (!Validation.isIntValid(userInput) || string.IsNullOrEmpty(userInput))
        {
            userInput = ReadLine();
            if (userInput == "0")
            {
                await Utility.ShowLoadingStatus();
                await MainMenu();
                return;
            }

        }

        int id = Int32.Parse(userInput);

        var shift = await _shiftServices.GetByIdAsync(id);

        if (shift == null)
        {
            Utility.DisplayReturnMessage("[yellow]\nReturning to main menu...[/]");
            await Utility.ShowLoadingStatus();
            return;
        }
        if (Confirm($"Are you sure you want to delete the shift for [yellow] {shift.WorkerName} {shift.WorkerLastName}[/]?"))
        {
            await _shiftServices.DeleteShiftAsync(id);
            Utility.DisplayReturnMessage("[green]Shift deleted successfully.[/]");
            await Utility.ShowLoadingStatus();
        }
        else
        {
            Utility.DisplayReturnMessage("[green]Shift not deleted.[/]");
            await Utility.ShowLoadingStatus();
        }
    }

    private async Task UpdateAShift()
    {
        await DisplayAllShifts();
        Utility.DisplayReturnMessage("[yellow]Type the Id who want to edit. Type 0 to return to Main menu: [/] ");
        string? userInput = ReadLine()?.Trim();
        if (userInput == "0") await MainMenu();

        while (!Validation.isIntValid(userInput) || string.IsNullOrEmpty(userInput))
        {
            Utility.DisplayReturnMessage($"[red]The Id: {userInput} dosn't exist please type again the Id or type 0 to return to Main Menu: [/]");
            userInput = ReadLine();
            if (userInput == "0") await MainMenu();
        }

        int id = Int32.Parse(userInput);
        var existingShift = await _shiftServices.GetByIdAsync(id);

        if (existingShift == null)
        {
            Utility.DisplayReturnMessage($"[red]No shift found with Id {id}.[/]");
            return;
        }

        string? updatedFirstName = existingShift.WorkerName;
        string? updatedLastName = existingShift.WorkerLastName;
        DateTime updatedStartTime = existingShift.StartTime;
        DateTime updatedEndTime = existingShift.EndTime;

        if (Confirm($"Are you sure you want to change the first name of the employ? [yellow] [/]?"))
        {
            updatedFirstName = await GetFirstAndLastName("\nType the first name of employ.Type 0 to return to main menu:");
        }
        if (Confirm($"Are you sure you want to change the last name of the employ? [yellow] [/]?"))
        {
            updatedLastName = await GetFirstAndLastName("\nType the last name of employ.Type 0 to return to main menu:");
        }
        if (Confirm($"Are you sure you want to change start hour ? [yellow] [/]?"))
        {
            string takeStartDurtation = await GetDuration("\nInsert shift [green]start time[/] (Format: HH:mm). Type 0 to return to main menu:");
            updatedStartTime = DateTime.ParseExact(takeStartDurtation, "HH:mm", null);
        }
        if (Confirm($"Are you sure you want to change end hour ? [yellow] [/]?"))
        {
            string takeEndDurtation = await GetDuration("\nInsert shift [green]end time[/] (Format: HH:mm). Type 0 to return to main menu:");
            updatedEndTime = DateTime.ParseExact(takeEndDurtation, "HH:mm", null);
        }

        var updateShift = new Shift
        {
            Id = existingShift.Id,
            WorkerName = updatedFirstName,
            WorkerLastName = updatedLastName,
            StartTime = updatedStartTime,
            EndTime = updatedEndTime
        };

        await _shiftServices.UpdateShiftAsync(updateShift);

        Utility.DisplayReturnMessage("[green]Shift successfully updated![/]");
        await Utility.ShowLoadingStatus();

    }

    private async Task DisplayOneShift()
    {
        await DisplayAllShifts();
        Utility.DisplayReturnMessage("[yellow]Type the Id who want to see. Type 0 to return to Main menu.[/] ");
        string? userInput = ReadLine()?.Trim();
        if (userInput == "0") await MainMenu();

        while (!Validation.isIntValid(userInput) || string.IsNullOrEmpty(userInput))
        {
            Utility.DisplayReturnMessage($"[red]The Id: {userInput} dosn't exist please type again the Id or type 0 to return to Main Menu: [/]");
            userInput = ReadLine();
            if (userInput == "0") await MainMenu();
        }

        int id = Int32.Parse(userInput);
        var existingShift = await _shiftServices.GetByIdAsync(id);

        if (existingShift == null)
        {
            Utility.DisplayReturnMessage($"[red]No shift found with Id {id}.[/]");
            return;
        }
        TableVisualisation.ShowOneTable(existingShift);
        Utility.DisplayReturnMessage("[yellow]Prees any key to return to Main Menu.[/]");
        await Utility.ShowLoadingStatus();
    }

    private async Task DisplayAllShifts()
    {
        var shifts = await _shiftServices.GetAllShiftsAsync();
        TableVisualisation.ShowTable(shifts);
    }
}
