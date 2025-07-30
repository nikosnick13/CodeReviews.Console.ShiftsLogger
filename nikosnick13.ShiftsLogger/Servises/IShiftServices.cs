using Microsoft.EntityFrameworkCore;
using nikosnick13.ShiftsLoggerAPI.Model;

namespace nikosnick13.ShiftsLoggerAPI.Services;

public interface IShiftServices
{
    IEnumerable<Shift> GetAllShifts();
    Shift? GetShiftById(int id);
    Shift CreateShift(Shift shift);
    Shift? UpdateShift(Shift updatedShift);
    void DeleteShift(int id);
}
