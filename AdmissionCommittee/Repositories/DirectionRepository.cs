using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

public class DirectionRepository : IRepository<Direction, int>
{
    private static List<Direction> _directions = [];
    private static int _directionCount = 0;
    public DirectionRepository(List<Direction> directions)
    {
        _directions = directions;
        _directionCount = _directions.Count;
    }

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
        _directionCount++;
        var newId = _directionCount;
        newItem.Id = newId;
        _directions.Add(newItem);
    }

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newItem">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns>If item not found return false, else true</returns>
    public bool Update(Direction newItem, int id)
    {
        var item = GetById(id);

        if (item == null)
            return false;

        newItem.Id = id;
        _directions[id] = newItem;
        return true;
    }

    /// <summary>
    /// Delete application item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>>If item not found return false, else true</returns>
    public bool Delete(int id)
    {
        var item = GetById(id);

        if (item == null)
            return false;
        return _directions.Remove(item);
    }
}
