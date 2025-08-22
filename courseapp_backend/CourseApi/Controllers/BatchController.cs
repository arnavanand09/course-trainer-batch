using CourseApi.Context;
using CourseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BatchController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var batches = _context.Batches
                                 .Include(b => b.Course)
                                 .Include(b => b.Trainer)
                                 .ToList();

            if (!batches.Any())
                return NotFound(new { message = "No batches found" });

            return Ok(batches);
        }

        // ✅ GET batch by Id
        // Handles: GET https://localhost:7150/api/batch/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var batch = _context.Batches
                                .Include(b => b.Course)
                                .Include(b => b.Trainer)
                                .FirstOrDefault(x => x.BatchId == id);

            if (batch == null)
                return NotFound(new { message = $"Batch with Id {id} not found" });

            return Ok(batch);
        }

        // ✅ POST add new batch
        // Handles: POST https://localhost:7150/api/batch
        [HttpPost]
        public IActionResult Post([FromBody] Batch batch)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            batch.CreatedOn = DateTime.Now;
            batch.IsActive = true;

            _context.Batches.Add(batch);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = batch.BatchId }, batch);
        }

        // ✅ PUT update batch
        // Handles: PUT https://localhost:7150/api/batch/1
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Batch temp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var batch = _context.Batches.FirstOrDefault(x => x.BatchId == id);
            if (batch == null)
                return NotFound(new { message = $"Batch with Id {id} not found" });

            batch.ModifiedOn = DateTime.Now;
            batch.ModifiedBy = 1;
            batch.BatchName = temp.BatchName;
            batch.StartDate = temp.StartDate;
            batch.EndDate = temp.EndDate;
            batch.CourseId = temp.CourseId;
            batch.TrainerId = temp.TrainerId;

            _context.SaveChanges();
            return Ok(batch);
        }

        // ✅ DELETE batch
        // Handles: DELETE https://localhost:7150/api/batch/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var batch = _context.Batches.FirstOrDefault(x => x.BatchId == id);
            if (batch == null)
                return NotFound(new { message = $"Batch with Id {id} not found" });

            try
            {
                _context.Batches.Remove(batch);
                _context.SaveChanges();
                return Ok(new { message = "Batch deleted successfully" });
            }
            catch (DbUpdateException ex)
            {
                // This block handles database-level errors
                if (ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx && sqlEx.Number == 547)
                {
                    return BadRequest(new { message = "Cannot delete this batch as it is linked to other records." });
                }

                // If it's a different type of database error, rethrow or handle accordingly
                return StatusCode(500, new { message = "An unexpected database error occurred.", error = ex.Message });
            }
        }
    }
}