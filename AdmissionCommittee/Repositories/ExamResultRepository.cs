using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

public class ExamResultRepository : IRepository<ExamResult, int>
{
    private static List<ExamResult> _examResults = [];

    public ExamResultRepository(List<ExamResult> examResults)
    {
        _examResults = examResults;
    }

    /// <summary>
    /// Get all exams result
    /// </summary>
    /// <returns>Return list of <see cref="ExamResult"/> objects</returns>
    public List<ExamResult> GetAll() => _examResults;

    /// <summary>
    /// Get exam result by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="ExamResult"/> object if can find, else return null</returns>
    public ExamResult? GetById(int id) => _examResults.FirstOrDefault(e => e.Id == id);

    /// <summary>
    /// Add new exam result
    /// </summary>
    /// <param name="newItem"><see cref="ExamResult"/> item</param>
    public void Add(ExamResult newItem)
    {
        var count = GetAll().Count - 1;
        var newId = _examResults[count].Id + 1;
        newItem.Id = newId;
        _examResults.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool Update(ExamResult newItem, int id)
    {
        var item = GetById(id);

        if (item == null)
            return false;

        newItem.Id = id;
        _examResults[id] = newItem;
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

        if (item == null)
            return false;
        return _examResults.Remove(item);
    }
}
