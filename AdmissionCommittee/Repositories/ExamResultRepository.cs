using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using System.Xml.Linq;

namespace AdmissionCommittee.Domain.Repositories;

public class ExamResultRepository : IRepository<ExamResult, int>
{
    private static readonly List<ExamResult> _eResults = [];

    /// <summary>
    /// Get all exams result
    /// </summary>
    /// <returns>Return list of <see cref="ExamResult"/> objects</returns>
    public List<ExamResult> GetAll() => _eResults;

    /// <summary>
    /// Get exam result by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="ExamResult"/> object if can find, else return null</returns>
    public ExamResult? GetById(int id) => _eResults.FirstOrDefault(e => e.Id == id);

    /// <summary>
    /// Add new exam result
    /// </summary>
    /// <param name="newItem"><see cref="ExamResult"/> item</param>
    public void Add(ExamResult newItem)
    {
        _eResults.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool UpdateById(ExamResult newItem, int id)
    {
        var item_id = _eResults.FindIndex(e => e.Id == id);
        if (item_id == -1)
            return false;

        _eResults[item_id] = newItem;
        return true;
    }

    /// <summary>
    /// Delete exam result item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public bool Delete(int id)
    {
        var item = GetById(id);

        if (item == null) return false;

        return _eResults.Remove(item);
    }
}
