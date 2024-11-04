using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectionController(IRepository<Direction, int> repository, IMapper mapper) : ControllerBase

    {
        /// <summary>
        /// Get all directions
        /// </summary>
        /// <returns><see cref="Direction"/> list</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        public ActionResult<IEnumerable<Direction>> Get()
        {
            return Ok(repository.GetAll());
        }

        /// <summary>
        /// Get direction by id
        /// </summary>
        /// <param name="id">Applicant`s id</param>
        /// <returns>List of <see cref="Direction"/> objects</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public ActionResult<Direction> Get(int id)
        {
            var direction = repository.GetById(id);

            if (direction == null)
                return NotFound();

            return Ok(direction);
        }

        /// <summary>
        /// Add new direction
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <returns>New <see cref="Direction"/> object</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public IActionResult Post([FromBody] DirectionDto item)
        {
            if (item == null) return BadRequest();

            var newItem = mapper.Map<Direction>(item);
            repository.Add(newItem);

            return Ok(newItem);
        }


        /// <summary>
        /// Update direction by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="item">Item to insert</param>
        /// <returns><see cref="Direction"/> object</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DirectionDto item)
        {
            if (id < 0) return BadRequest();

            var checkItem = repository.GetById(id);
            if (checkItem == null) return BadRequest();

            var newItem = mapper.Map<Direction>(item);
            repository.Update(newItem, id);

            return Ok();
        }

        /// <summary>
        /// Delete direction by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest();

            repository.Delete(id);

            return Ok();
        }
    }
}
