using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;

namespace AdmissionCommittee.Domain.Repositories;

public class ApplicantRepository : IRepository<Applicant, int>
{
    private static List<Applicant> _applicants = [];

    public ApplicantRepository(List<Applicant> applicants)
    {
        _applicants = applicants;
        //    _applicants.Add(new() { Id = 0, BirthdayDate = new DateTime(2005, 1, 18), City = "Samara", Country = "Russia", FullName = "Vladimir Vladimirovich" });
        //    _applicants.Add(new() { Id = 1, BirthdayDate = new DateTime(2002, 2, 9), City = "Samara", Country = "Russia", FullName = "Andrew Viktorovich" });
        //    _applicants.Add(new() { Id = 2, BirthdayDate = new DateTime(2005, 7, 8), City = "Vladivostok", Country = "Russia", FullName = "Vitaliy Vitalivich" });
        //    _applicants.Add(new() { Id = 3, BirthdayDate = new DateTime(2004, 1, 13), City = "Samara", Country = "Russia", FullName = "Michail Michailovich" });
        //    _applicants.Add(new() { Id = 4, BirthdayDate = new DateTime(2004, 6, 2), City = "Saints-Petersburg", Country = "Russia", FullName = "Veronika Igorevna" });
        //    _applicants.Add(new() { Id = 5, BirthdayDate = new DateTime(2004, 2, 12), City = "Samara", Country = "Russia", FullName = "Ivan Ivanov" });
        //    _applicants.Add(new() { Id = 6, BirthdayDate = new DateTime(2005, 2, 22), City = "Vladivostok", Country = "Russia", FullName = "Danila Danilovich" });
        //    _applicants.Add(new() { Id = 7, BirthdayDate = new DateTime(2001, 2, 2), City = "Samara", Country = "Russia", FullName = "Maria Olegovna" });
        //    _applicants.Add(new() { Id = 8, BirthdayDate = new DateTime(2002, 1, 4), City = "Moscow", Country = "Russia", FullName = "Sergey Sergeevich" });
        //    _applicants.Add(new() { Id = 9, BirthdayDate = new DateTime(2004, 12, 1), City = "Vladivostok", Country = "Russia", FullName = "Vladimir Vladimirov" });
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
