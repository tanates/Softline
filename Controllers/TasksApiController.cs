using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftlineTest.Models;
using SoftlineTest.Models.Repository;

namespace SoftlineTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksApiController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ModelTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelTasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/ModelTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelTasks>> GetModelTasks(int id)
        {
            var modelTasks = await _context.Tasks.FindAsync(id);

            if (modelTasks == null)
            {
                return NotFound();
            }

            return modelTasks;
        }

        // PUT: api/ModelTasks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelTasks(int id, ModelTasks modelTasks)
        {
            if (id != modelTasks.ID)
            {
                return BadRequest();
            }

            _context.Entry(modelTasks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelTasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ModelTasks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ModelTasks>> PostModelTasks(ModelTasks modelTasks)
        {
            _context.Tasks.Add(modelTasks);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelTasks", new { id = modelTasks.ID }, modelTasks);
        }

        // DELETE: api/ModelTasks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ModelTasks>> DeleteModelTasks(int id)
        {
            var modelTasks = await _context.Tasks.FindAsync(id);
            if (modelTasks == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(modelTasks);
            await _context.SaveChangesAsync();

            return modelTasks;
        }

        private bool ModelTasksExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
