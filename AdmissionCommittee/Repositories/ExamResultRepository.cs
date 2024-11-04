using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AdmissionCommittee.Domain.Repositories;

public class ExamResultRepository(AdmissionCommitteeDbContext context) : IRepository<ExamResult, int>
{
    /// <summary>
    /// Get all exams result
    /// </summary>
    /// <returns>Return list of <see cref="ExamResult"/> objects</returns>
    public async Task<List<ExamResult>> GetAll()
    {
        return await context.ExamResults.ToListAsync();
    }

    /// <summary>
    /// Get exam result by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="ExamResult"/> object if can find, else return null</returns>
    public async Task<ExamResult> GetById(int id)
    {
        return await context.ExamResults.FindAsync(id);
    }

    /// <summary>
    /// Add new exam result
    /// </summary>
    /// <param name="newItem"><see cref="ExamResult"/> item</param>
    public async Task Add(ExamResult newItem)
    {
        await context.ExamResults.AddAsync(newItem);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public async Task Update(ExamResult newItem, int id)
    {
        var item = await GetById(id);

        if (item != null)
        {
            item.ApplicantId = newItem.ApplicantId;
            item.Result = newItem.Result;
            item.ExamName = newItem.ExamName;
            context.ExamResults.Update(item);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Delete exam result item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public async Task Delete(int id)
    {
        var item = await GetById(id);

        if (item != null)
        {
            context.ExamResults.Remove(item);
            await context.SaveChangesAsync();
        }
    }
}
