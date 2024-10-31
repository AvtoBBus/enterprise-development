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
    public async Task<ActionResult<IEnumerable<Applicant>>> Get()
    {
        var applicationDto = await repository.GetAll();

        if (applicationDto == null) return NotFound();
        return Ok(applicationDto);
    }

    /// <summary>
    /// Get applicant by id
    /// </summary>
    /// <param name="id">Applicant`s id</param>
    /// <returns>List of <see cref="Applicant"/> objects</returns>
    /// <response code="200">Success</response>
    /// <response code="404">Not Found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Applicant>> Get(int id)
    {
        var applicant = await repository.GetById(id);

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
    public async Task<IActionResult> Post([FromBody] ApplicantDto item)
    {
        if (item == null) return BadRequest();

        var newItem = mapper.Map<Applicant>(item);
        await repository.Add(newItem);

        return Ok(newItem);
    }


    /// <summary>
    /// Update applicant by id
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <param name="item">Item to insert</param>
    /// <returns><see cref="Applicant"/> object</returns>
    /// <response code="200">Success</response>
    /// <response code="400">Bad Request</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ApplicantDto item)
    {
        if (item == null || id < 0) return BadRequest();
        
        var newItem = mapper.Map<Applicant>(item);
        await repository.Update(newItem, id);

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
    public async Task<IActionResult> Delete(int id)
    {
        if (id < 0) return BadRequest();

        await repository.Delete(id);

        return Ok();
    }

}
