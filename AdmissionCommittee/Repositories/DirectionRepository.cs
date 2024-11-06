using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionCommittee.Domain.Repositories;

public class DirectionRepository(AdmissionCommitteeDbContext context) : IRepository<Direction, int>
{
    /// <summary>
    /// Get all applications
    /// </summary>
    /// <returns>Return list of <see cref="Direction"/> objects</returns>
    public async Task<List<Direction>> GetAll()
    {
        return await context.Directions.ToListAsync();
    }

    /// <summary>
    /// Get application by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="Direction"/> object if can find, else return null</returns>
    public async Task<Direction> GetById(int id)
    {
        return await (ValueTask<Direction>)context.Directions.FindAsync(id)!;
    }

    /// <summary>
    /// Add new application
    /// </summary>
    /// <param name="newItem"><see cref="Direction"/> item</param>
    public async Task Add(Direction newItem)
    {
        await context.Directions.AddAsync(newItem);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public async Task Update(Direction newItem, int id)
    {
        var item = await GetById(id);

        if (item != null)
        {
            item.ApplicantId = newItem.ApplicantId;
            item.Priority = newItem.Priority;
            item.SpecialityId = newItem.SpecialityId;
            context.Directions.Update(item);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Delete application item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public async Task Delete(int id)
    {
        var item = await GetById(id);

        if (item != null)
        {
            context.Directions.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
