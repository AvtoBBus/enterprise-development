using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using System.Xml.Linq;

namespace AdmissionCommittee.Domain.Repositories;

public class ApplicantRepository : IRepository<Applicant, int>
{
    private static readonly List<Applicant> _applicants = [];

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
        _applicants.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool UpdateById(Applicant newItem, int id)
    {
        var item_id = _applicants.FindIndex(a => a.Id == id);
        if (item_id == -1)
            return false;

        _applicants[item_id] = newItem;
        return true;
    }

    /// <summary>
    /// Delete applicant item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public bool Delete(int id)
    {
        var enterprise = GetById(id);

        if (enterprise == null)
            return false;
        return _applicants.Remove(enterprise);
    }
}
