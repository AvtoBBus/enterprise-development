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
        var count = GetAll().Count - 1;
        var newId = _specialyties[count].Id + 1;
        newItem.Id = newId;
        _specialyties.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool Update(Speciality newItem, int id)
    {
        var item = GetById(id);

        if (item == null)
            return false;

        newItem.Id = id;
        _specialyties[id] = newItem;
        return true;
    }


    /// <summary>
    /// Delete speciality item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public bool Delete(int id)
    {
        var item = GetById(id);

        if (item == null)
            return false;
        return _specialyties.Remove(item);
    }
}
