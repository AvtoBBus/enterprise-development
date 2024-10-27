using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicantController(IRepository<Applicant, int> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get all applicants
    /// </summary>
    /// <returns><see cref="Applicant"/> list</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet]
    public ActionResult<IEnumerable<ApplicantDto>> Get()
    {
        var applicationDto = repository.GetAll();

        if (applicationDto == null) return NotFound();
        return Ok(applicationDto);
    }

    /// <summary>
    /// Get applicant by id
    /// </summary>
    /// <param name="id">Applicant`s id</param>
    /// <returns>Список объектов <see cref="Applicant"/></returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id}")]
    public ActionResult<ApplicantDto> Get(int id)
    {
        var applicant = repository.GetById(id);

        if (applicant == null)
            return NotFound();

        return Ok(applicant);
    }

    /// <summary>
    /// Add new applicant
    /// </summary>
    /// <param name="item">Item to insert</param>
    /// <returns>New <see cref="Applicant"/> object</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Bad Request</response>
    [HttpPost]
    public IActionResult Post([FromBody] ApplicantDto item)
    {

        if (item == null) return BadRequest();

        var newId = repository.GetAll().Count;
        Application.Mapper servise = new(mapper);
        var newApplicant = servise.GetApplicant(item);

        newApplicant.Id = newId;
        repository.Add(newApplicant);

        return Ok(newApplicant);
    }


    /// <summary>
    /// Update applicant by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <param name="item">Item to insert</param>
    /// <returns>Созданный объект <see cref="Applicant"/></returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ApplicantDto item)
    {

        if (item == null || id == null) return BadRequest();


        if (id > repository.GetAll().Count - 1 || id < 0) return BadRequest();

        Application.Mapper servise = new(mapper);
        item.Id = id;

        var newApplicant = servise.GetApplicant(item);

        var itemId = newApplicant.Id;

        repository.UpdateById(newApplicant, itemId);

        return Ok();
    }

    /// <summary>
    /// Delete applicant by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {

        if (id == null) return BadRequest();

        if (id > repository.GetAll().Count - 1 || id < 0) return BadRequest();

        repository.Delete(id);

        return Ok();
    }

}
