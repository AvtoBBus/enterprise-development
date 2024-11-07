using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionCommittee.Domain.Repositories;

public class ApplicantRepository(AdmissionCommitteeDbContext context) : IRepository<Applicant, int>
{
    /// <summary>
    /// Get all applicants
    /// </summary>
    /// <returns>Return list of <see cref="Applicant"/> objects</returns>
    public async Task<List<Applicant>> GetAll()
    {
        return await context.Applicants.ToListAsync();
    }

    /// <summary>
    /// Get applicant by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="Applicant"/> object if can find, else return null</returns>
    public async Task<Applicant?> GetById(int id)
    {
        return await context.Applicants.FindAsync(id);
    }

    /// <summary>
    /// Add new applicant
    /// </summary>
    /// <param name="newItem"><see cref="Applicant"/> item</param>
    public async Task Add(Applicant newItem)
    {
        await context.Applicants.AddAsync(newItem);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public async Task Update(Applicant newItem, int id)
    {
        var item = await GetById(id);
        
        if (item != null)
        {
            item.FullName = newItem.FullName;
            item.BirthdayDate = newItem.BirthdayDate;
            item.City = newItem.City;
            item.Country = newItem.Country;
            context.Applicants.Update(item);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Delete applicant item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public async Task Delete(int id)
    {
        var item = await GetById(id);

        if (item != null)
        {
            context.Applicants.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
