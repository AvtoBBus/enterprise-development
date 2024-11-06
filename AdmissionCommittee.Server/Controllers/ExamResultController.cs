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
        public async Task<ActionResult<IEnumerable<ExamResult>>> Get()
        {
            return Ok(await repository.GetAll());
        }

        /// <summary>
        /// Get exam result by id
        /// </summary>
        /// <param name="id">Applicant`s id</param>
        /// <returns>List of <see cref="ExamResult"/> objects</returns>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<ExamResult>> Get(int id)
        {
            var direction = await repository.GetById(id);

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
        public async Task<ActionResult> Post([FromBody] ExamResultDto item)
        {
            if (item == null) return BadRequest();

            var newItem = mapper.Map<ExamResult>(item);
            await repository.Add(newItem);

            return Ok(newItem);
        }


        /// <summary>
        /// Update exam result by id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="item">Item to insert</param>
        /// <returns><see cref="ExamResult"/> object</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExamResultDto item)
        {
            if (id < 0) return BadRequest();

            var checkItem = await repository.GetById(id);
            if (checkItem == null) return BadRequest();

            var newItem = mapper.Map<ExamResult>(item);
            await repository.Update(newItem, id);

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
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0) return BadRequest();

            await repository.Delete(id);

            return Ok();
        }
    }
}
