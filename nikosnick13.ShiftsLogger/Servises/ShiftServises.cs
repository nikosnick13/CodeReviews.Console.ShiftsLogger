using Microsoft.EntityFrameworkCore;
using nikosnick13.ShiftsLoggerAPI.Data;
using nikosnick13.ShiftsLoggerAPI.Model;
using nikosnick13.ShiftsLoggerAPI.Services;

namespace nikosnick13.ShiftsLoggerAPI.Servises;

public class ShiftServices : IShiftServices
{
    private readonly ApplicationDbContext _context;

    public ShiftServices(ApplicationDbContext context)
    {
        _context = context;
    }
    public Shift CreateShift(Shift shift)
    {
        try 
        {
            _context.Add(shift);
            _context.SaveChanges();
            return shift;
        }
        catch(Exception ex)
        {
            throw new ApplicationException("Something went wrong while creating the shift.", ex);
        }
    }
    public void DeleteShift(int id)
    {
        try 
        {
            var shift = _context.Shift.FirstOrDefault(s => s.Id == id);
            if (shift == null) throw new ApplicationException($"Shift with ID {id} not found.");
               

            _context.Shift.Remove(shift);
            _context.SaveChanges();
        }   
        catch(Exception ex)
        {
            throw new ApplicationException("Something went wrong while creating the shift.", ex);
        }
    }

    public IEnumerable<Shift> GetAllShifts()
    {
        return _context.Shift.ToList();
    }
 
    public Shift? GetShiftById(int id)
    {
        try 
        {
            return _context.Shift.FirstOrDefault( x => x.Id == id);
        }
        catch(Exception ex)
        {
            throw new ApplicationException("Something went wrong while creating the shift.", ex);
        }

    }

    public Shift? UpdateShift(Shift updatedShift)
    {
        try
        {
            var existingShift = _context.Shift.Find(updatedShift.Id);

            if (existingShift == null)
                return null;

          
            existingShift.WorkerName = updatedShift.WorkerName;
            existingShift.WorkerLastName = updatedShift.WorkerLastName;
            existingShift.StartTime = updatedShift.StartTime;
            existingShift.EndTime = updatedShift.EndTime;

            _context.SaveChanges();

            return existingShift;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Something went wrong while updating the shift.", ex);
        }
    }
}
