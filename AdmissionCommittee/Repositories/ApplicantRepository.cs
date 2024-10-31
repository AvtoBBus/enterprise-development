using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

public class ApplicantRepository : IRepository<Applicant, int>
{
    private static List<Applicant> _applicants = [];

    public ApplicantRepository(List<Applicant> applicants)
    {
        _applicants = applicants;
    }


    /// <summary>
    /// Get all applicants
    /// </summary>
    /// <returns>Return list of <see cref="Applicant"/> objects</returns>
    public List<Applicant> GetAll() => _applicants;

    /// <summary>
    /// Get applicant by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="Applicant"/> object if can find, else return null</returns>
    public Applicant? GetById(int id) => _applicants.FirstOrDefault(a => a.Id == id);

    /// <summary>
    /// Add new applicant
    /// </summary>
    /// <param name="newItem"><see cref="Applicant"/> item</param>
    public void Add(Applicant newItem)
    {
        var count = GetAll().Count - 1;
        var newId = _applicants[count].Id + 1;
        newItem.Id = newId;
        _applicants.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool Update(Applicant newItem, int id)
    {
        var item = GetById(id);

        if (item == null)
            return false;

        newItem.Id = id;
        _applicants[id] = newItem;
        return true;
    }

    /// <summary>
    /// Delete applicant item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public bool Delete(int id)
    {
        var item = GetById(id);

        if (item == null)
            return false;
        return _applicants.Remove(item);
    }
}
