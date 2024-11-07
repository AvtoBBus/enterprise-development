using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionCommittee.Domain.Repositories;

public class SpecialityRepository(AdmissionCommitteeDbContext context) : IRepository<Speciality, int>
{
    /// <summary>
    /// Get all specialities
    /// </summary>
    /// <returns>Return list of <see cref="Speciality"/> objects</returns>
    public async Task<List<Speciality>> GetAll()
    {
        return await context.Specialities.ToListAsync();
    }

    /// <summary>
    /// Get speciality by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="Speciality"/> object if can find, else return null</returns>
    public async Task<Speciality?> GetById(int id)
    {
        return await context.Specialities.FindAsync(id);
    }

    /// <summary>
    /// Add new speciality
    /// </summary>
    /// <param name="newItem"><see cref="Speciality"/> item</param>
    public async Task Add(Speciality newItem)
    {
        await context.Specialities.AddAsync(newItem);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public async Task Update(Speciality newItem, int id)
    {
        var item = await GetById(id);

        if (item != null)
        {
            item.Name = newItem.Name;
            item.Faculity = newItem.Faculity;
            item.Number = newItem.Number;
            context.Specialities.Update(item);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Delete speciality item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public async Task Delete(int id)
    {
        var item = await GetById(id);

        if (item != null)
        {
            context.Specialities.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
