using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using static Spectre.Console.AnsiConsole;
 

namespace nikosnick13.ShiftsLoggerUI;

public class Validation
{
    public static bool isIntValid(string? id) 
    {
        if(string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(id))
        {
            MarkupLine("[red]The input is empty or contains only spaces. Type 0 to return to main menu:[/]");
            return false;
        }

        id = id.Trim();

        if (Int32.TryParse(id, out int result) && result > 0 ) 
        {
            return true;   
        }
        MarkupLine("[red]The input is empty or contains only spaces.Type 0 to return to main menu:[/]");
        return false;
    }

    public static bool isNameValid(string? userInput) {

     

        if (string.IsNullOrEmpty(userInput) ||  string.IsNullOrWhiteSpace(userInput)) 
        {
            MarkupLine("[red]The input is empty or contains only spaces.Type 0 to return to main menu:[/]");
            return false;
        }
        if (userInput.Length < 3 || userInput.Length > 255) 
        {
            if (userInput == "0") return true;
            MarkupLine("[red]The input must be between 3 and 255 characters.Type 0 to return to main menu:[/]");
            return false;
        }
        if (userInput.Any(char.IsDigit)) 
        {
            MarkupLine("[red]The input must not contain numbers.Type 0 to return to main menu:[/]");
            return false;
        }
        return true;
    }

    public static bool isValidDuration(string? timeInput)
    {
        if (string.IsNullOrWhiteSpace(timeInput)
            || !DateTime.TryParseExact(timeInput.Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            MarkupLine("\n[red]Invalid input. Please insert the time in the correct format (HH:mm).Type 0 to return to main menu:[/]");
            return false;
        }

        return true;
    }
}
