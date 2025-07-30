using Spectre.Console;
using static System.Console;
using static Spectre.Console.AnsiConsole;
 

namespace nikosnick13.ShiftsLoggerUI;

internal class Utility
{
    public static void DisplayReturnMessage(string msg)
    {
        MarkupLine(msg);
    }
    public static async Task ShowLoadingStatus()
    {
        await Task.Run( () =>  Status().Start("[yellow]Loading Main Menu...[/]", ctx =>
        {
             Thread.Sleep(1000);
        })); 
        AnsiConsole.Clear();
    }
    public static async Task ShowExitStatus()
    {
        await Task.Run(() => Status().Start("[yellow]Good by user....[/]", ctx =>
        {
            Thread.Sleep(1000);
        }));
        AnsiConsole.Clear();
      
    }
}


 
