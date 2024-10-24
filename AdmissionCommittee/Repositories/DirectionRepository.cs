using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using System.Xml.Linq;

namespace AdmissionCommittee.Domain.Repositories;

public class DirectionRepository : IRepository<Direction, int>
{
    private static readonly List<Direction> _directions = [];

    /// <summary>
    /// Get all applications
    /// </summary>
    /// <returns>Return list of <see cref="Direction"/> objects</returns>
    public List<Direction> GetAll() => _directions;

    /// <summary>
    /// Get application by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Return <see cref="Direction"/> object if can find, else return null</returns>
    public Direction? GetById(int id) => _directions.FirstOrDefault(d => d.Id == id);

    /// <summary>
    /// Add new application
    /// </summary>
    /// <param name="newItem"><see cref="Direction"/> item</param>
    public void Add(Direction newItem)
    {
        _directions.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool UpdateById(Direction newItem, int id)
    {
        var item_id = _directions.FindIndex(d => d.Id == id);
        if (item_id == -1)
            return false;

        _directions[item_id] = newItem;
        return true;
    }

    /// <summary>
    /// Delete application item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public bool Delete(int id)
    {
        var enterprise = GetById(id);

        if (enterprise == null)
            return false;
        return _directions.Remove(enterprise);
    }
}
