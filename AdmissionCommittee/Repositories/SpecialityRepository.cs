using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

public class SpecialityRepository : IRepository<Speciality, int>
{
    private static List<Speciality> _specialyties = [];

    public SpecialityRepository(List<Speciality> specialyties)
    {
        _specialyties = specialyties;
    }

    /// <summary>
    /// Get all specialities
    /// </summary>
    /// <returns>Return list of <see cref="Speciality"/> objects</returns>
    public List<Speciality> GetAll() => _specialyties;

    /// <summary>
    /// Get speciality by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="Speciality"/> object if can find, else return null</returns>
    public Speciality? GetById(int id) => _specialyties.FirstOrDefault(s => s.Id == id);

    /// <summary>
    /// Add new speciality
    /// </summary>
    /// <param name="newItem"><see cref="Speciality"/> item</param>
    public void Add(Speciality newItem)
    {
        _specialyties.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool UpdateById(Speciality newItem, int id)
    {
        var item_id = _specialyties.FindIndex(s => s.Id == id);
        if (item_id == -1)
            return false;

        _specialyties[item_id] = newItem;
        return true;
    }


    /// <summary>
    /// Delete speciality item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public bool Delete(int id)
    {
        var enterprise = GetById(id);

        if (enterprise == null)
            return false;
        return _specialyties.Remove(enterprise);
    }
}
