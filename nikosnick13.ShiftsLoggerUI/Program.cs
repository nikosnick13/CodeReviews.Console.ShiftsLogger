using nikosnick13.ShiftsLoggerUI.Services;
using nikosnick13.ShiftsLoggerUI;
namespace nikosnick13.ShiftsLoggerUI;

internal class Program
{
    static async Task Main()
    {   
        var httpClient = new HttpClient();
        var shiftServices = new ShiftServices(httpClient);

        var ui = new UserInterface(shiftServices);

        await ui.MainMenu();
    }
}
