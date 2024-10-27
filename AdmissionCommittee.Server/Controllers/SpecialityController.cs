using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController(IRepository<Speciality, int> repository, IMapper mapper) : ControllerBase

    {
        /// <summary>
        /// Get all specialities
        /// </summary>
        /// <returns><see cref="Speciality"/> list</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        public ActionResult<IEnumerable<SpecialityDto>> Get()
        {
            var direction = repository.GetAll();

            if (direction == null) return NotFound();
            return Ok(direction);
        }

        /// <summary>
        /// Get speciality by id
        /// </summary>
        /// <param name="id">Applicant`s id</param>
        /// <returns>Список объектов <see cref="Speciality"/></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public ActionResult<SpecialityDto> Get(int id)
        {
            var direction = repository.GetById(id);

            if (direction == null)
                return NotFound();

            return Ok(direction);
        }

        /// <summary>
        /// Add new speciality
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <returns>New <see cref="Speciality"/> object</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public ActionResult Post([FromBody] SpecialityDto item)
        {

            if (item == null) return BadRequest();

            var newId = repository.GetAll().Count;
            Application.Mapper servise = new(mapper);
            var newEResult = servise.GetExamResult(item);

            newEResult.Id = newId;
            repository.Add(newEResult);

            return Ok(newEResult);
        }


        /// <summary>
        /// Update speciality by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="item">Item to insert</param>
        /// <returns>Созданный объект <see cref="Speciality"/></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] SpecialityDto item)
        {

            if (item == null || id == null) return BadRequest();


            if (id > repository.GetAll().Count - 1 || id < 0) return BadRequest();

            Application.Mapper servise = new(mapper);
            item.Id = id;

            var newEResult = servise.GetExamResult(item);

            var itemId = newEResult.Id;

            repository.UpdateById(newEResult, itemId);

            return Ok();
        }

        /// <summary>
        /// Delete speciality by id
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
}
