using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using AdmissionCommittee.Domain.Models;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Application;

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
        public ActionResult<IEnumerable<DirectionDto>> Get()
        {
            var direction = repository.GetAll();

            if (direction == null) return NotFound();
            return Ok(direction);
        }

        /// <summary>
        /// Get direction by id
        /// </summary>
        /// <param name="id">Applicant`s id</param>
        /// <returns>Список объектов <see cref="Direction"/></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public ActionResult<DirectionDto> Get(int id)
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

            var newId = repository.GetAll().Count;
            Application.Mapper servise = new(mapper);
            var newDirection = servise.GetDirection(item);

            newDirection.Id = newId;
            repository.Add(newDirection);

            return Ok(newDirection);
        }


        /// <summary>
        /// Update direction by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="item">Item to insert</param>
        /// <returns>Созданный объект <see cref="Direction"/></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] DirectionDto item)
        {

            if (item == null || id == null) return BadRequest();


            if (id > repository.GetAll().Count - 1 || id < 0) return BadRequest();

            Application.Mapper servise = new(mapper);
            item.Id = id;

            var newDirection = servise.GetDirection(item);

            var itemId = newDirection.Id;

            repository.UpdateById(newDirection, itemId);

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

            if (id == null) return BadRequest();

            if (id > repository.GetAll().Count - 1 || id < 0) return BadRequest();

            repository.Delete(id);

            return Ok();
        }
    }
}
