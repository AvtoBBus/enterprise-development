using AdmissionCommittee.Application.DTO;
using AdmissionCommittee.Domain.Interfaces;
using AdmissionCommittee.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AdmissionCommittee.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamResultController(IRepository<ExamResult, int> repository, IMapper mapper) : ControllerBase

    {
        /// <summary>
        /// Get all exam results
        /// </summary>
        /// <returns><see cref="ExamResult"/> list</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        public ActionResult<IEnumerable<ExamResultDto>> Get()
        {
            var direction = repository.GetAll();

            if (direction == null) return NotFound();
            return Ok(direction);
        }

        /// <summary>
        /// Get exam result by id
        /// </summary>
        /// <param name="id">Applicant`s id</param>
        /// <returns>Список объектов <see cref="ExamResult"/></returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public ActionResult<ExamResultDto> Get(int id)
        {
            var direction = repository.GetById(id);

            if (direction == null)
                return NotFound();

            return Ok(direction);
        }

        /// <summary>
        /// Add new exam result
        /// </summary>
        /// <param name="item">Item to insert</param>
        /// <returns>New <see cref="ExamResult"/> object</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        public ActionResult Post([FromBody] ExamResultDto item)
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
        /// Update exam result by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="item">Item to insert</param>
        /// <returns>Созданный объект <see cref="ExamResult"/></returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ExamResultDto item)
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
        /// Delete exam result by id
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
