using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MimeMapping;
using TaskManager.Contexts;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFilesController : ControllerBase
    {
        private readonly UserTaskManagerContext _context;

        public UserFilesController(UserTaskManagerContext context)
        {
            _context = context;
        }

        // GET: api/UserFiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFile>>> GetUserFiles()
        {
            if (_context.UserFiles == null)
            {
                return NotFound();
            }
            return await _context.UserFiles.ToListAsync();
        }

        // GET: api/UserFiles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFile>> GetUserFile(Guid id)
        {
            if (_context.UserFiles == null)
            {
                return NotFound();
            }
            var userFile = await _context.UserFiles.FindAsync(id);

            if (userFile == null)
            {
                return NotFound();
            }

            return userFile;
        }

        // GET: api/UserFiles/download/5
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadUserFile(Guid id)
        {
            if (_context.UserFiles == null)
            {
                return NotFound();
            }

            var userFile = await _context.UserFiles.FindAsync(id);

            if (userFile == null)
            {
                return NotFound();
            }
            string contentType = MimeUtility.GetMimeMapping(userFile.FileName);

            return File(userFile.FileData, contentType, fileDownloadName: userFile.FileName);
        }

        // PUT: api/UserFiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFile(Guid id, UserFile userFile)
        {
            if (id != userFile.Id)
            {
                return BadRequest();
            }

            _context.Entry(userFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserFileExists(id))
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

        // POST: api/UserFiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserFile>> PostUserFile(UserFile userFile)
        {
            if (_context.UserFiles == null)
            {
                return Problem("Entity set 'UserTaskManagerContext.UserFiles'  is null.");
            }

            if (userFile.FileData == null)
            {
                byte[] fileData;
                using (var stream = new FileStream(userFile.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        fileData = reader.ReadBytes((int)stream.Length);
                    }
                }
                userFile.FileName = Path.GetFileName(userFile.FileName);
                userFile.FileData = fileData;
            }

            _context.UserFiles.Add(userFile);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserFile", new { id = userFile.Id }, userFile);
        }

        // DELETE: api/UserFiles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserFile(Guid id)
        {
            if (_context.UserFiles == null)
            {
                return NotFound();
            }
            var userFile = await _context.UserFiles.FindAsync(id);
            if (userFile == null)
            {
                return NotFound();
            }

            _context.UserFiles.Remove(userFile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserFileExists(Guid id)
        {
            return (_context.UserFiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
