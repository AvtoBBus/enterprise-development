namespace AdmissionCommittee.Domain.Interfaces;

public interface IRepository<TEntity, TKey>
{
    /// <summary>
    /// Return all items
    /// </summary>
    /// <returns></returns>
    public List<TEntity> GetAll();

    /// <summary>
    /// Return item by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns></returns>
    public TEntity? GetById(TKey id);

    /// <summary>
    /// Add new item
    /// </summary>
    /// <param name="newItem">Item to insert</param>
    /// <returns></returns>
    public void Add(TEntity newItem);

    /// <summary>
    /// Update item by id
    /// </summary>
    /// <param name="newValue">New item state</param>
    /// <param name="id">Id of item</param>
    /// <returns></returns>
    public bool Update(TEntity newValue, TKey id);

    /// <summary>
    /// Delete item by Id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns></returns>
    public bool Delete(TKey id);
}
