using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Contexts;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExecutionStatusTasksController : ControllerBase
    {
        private readonly UserTaskManagerContext _context;

        public ExecutionStatusTasksController(UserTaskManagerContext context)
        {
            _context = context;
        }

        // GET: api/ExecutionStatusTasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExecutionStatusTask>>> GetExecutionStatuses()
        {
            if (_context.ExecutionStatuses == null)
            {
                return NotFound();
            }
            return await _context.ExecutionStatuses.ToListAsync();
        }

        // GET: api/ExecutionStatusTasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExecutionStatusTask>> GetExecutionStatusTask(int id)
        {
            if (_context.ExecutionStatuses == null)
            {
                return NotFound();
            }
            var executionStatusTask = await _context.ExecutionStatuses.FindAsync(id);

            if (executionStatusTask == null)
            {
                return NotFound();
            }

            return executionStatusTask;
        }

        // PUT: api/ExecutionStatusTasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExecutionStatusTask(int id, ExecutionStatusTask executionStatusTask)
        {
            if (id != executionStatusTask.Id)
            {
                return BadRequest();
            }

            _context.Entry(executionStatusTask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExecutionStatusTaskExists(id))
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

        // POST: api/ExecutionStatusTasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExecutionStatusTask>> PostExecutionStatusTask(ExecutionStatusTask executionStatusTask)
        {
            if (_context.ExecutionStatuses == null)
            {
                return Problem("Entity set 'UserTaskManagerContext.ExecutionStatuses'  is null.");
            }
            _context.ExecutionStatuses.Add(executionStatusTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExecutionStatusTask", new { id = executionStatusTask.Id }, executionStatusTask);
        }

        // DELETE: api/ExecutionStatusTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExecutionStatusTask(int id)
        {
            if (_context.ExecutionStatuses == null)
            {
                return NotFound();
            }
            var executionStatusTask = await _context.ExecutionStatuses.FindAsync(id);
            if (executionStatusTask == null)
            {
                return NotFound();
            }

            _context.ExecutionStatuses.Remove(executionStatusTask);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExecutionStatusTaskExists(int id)
        {
            return (_context.ExecutionStatuses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
