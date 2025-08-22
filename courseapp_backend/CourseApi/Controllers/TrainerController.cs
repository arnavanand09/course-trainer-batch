using CourseApi.Context;
using CourseApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainerController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET all trainers
        // Handles: GET https://localhost:7150/api/trainer
        [HttpGet]
        public IActionResult Get()
        {
            var trainers = _context.Trainers.ToList();
            if (!trainers.Any())
                return NotFound("No trainers found.");
            return Ok(trainers);
        }

        // ✅ GET trainer by Id
        // Handles: GET https://localhost:7150/api/trainer/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var trainer = _context.Trainers.FirstOrDefault(x => x.TrainerId == id);
            if (trainer == null)
                return NotFound($"Trainer with Id = {id} not found.");
            return Ok(trainer);
        }

        // ✅ POST add new trainer
        // Handles: POST https://localhost:7150/api/trainer
        [HttpPost]
        public IActionResult Post([FromBody] Trainer trainer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            trainer.CreatedOn = DateTime.Now;
            trainer.CreatedBy = 1;  // 👈 set backend default
            trainer.IsActive = true;

            _context.Trainers.Add(trainer);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = trainer.TrainerId }, trainer);
        }

        // ✅ PUT update trainer
        // Handles: PUT https://localhost:7150/api/trainer/1
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Trainer temp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainer = _context.Trainers.FirstOrDefault(x => x.TrainerId == id);
            if (trainer == null)
                return NotFound($"Trainer with Id = {id} not found.");

            trainer.ModifiedOn = DateTime.Now;
            trainer.ModifiedBy = 1; // 👈 backend handles
            trainer.TrainerName = temp.TrainerName;
            trainer.Expertise = temp.Expertise;
            trainer.Email = temp.Email;
            trainer.Phone = temp.Phone;

            _context.SaveChanges();
            return Ok(trainer);
        }

        // ✅ DELETE trainer
        // Handles: DELETE https://localhost:7150/api/trainer/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var trainer = _context.Trainers.FirstOrDefault(x => x.TrainerId == id);
            if (trainer == null)
            {
                return NotFound($"Trainer with Id = {id} not found.");
            }

            // Find all batches associated with this trainer
            var batchesToUpdate = _context.Batches.Where(b => b.TrainerId == id);

            // Set the TrainerId for all associated batches to NULL
            foreach (var batch in batchesToUpdate)
            {
                batch.TrainerId = null;
            }

            // Now, safely delete the trainer record
            _context.Trainers.Remove(trainer);
            _context.SaveChanges();

            return Ok($"Trainer with Id = {id} deleted.");
        }
    }
}