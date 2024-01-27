using lib_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lib_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RackController: ControllerBase
    {
        private readonly LibraryDbContext _context;

        public RackController(LibraryDbContext context)
        {
            _context = context;
        }

        //Get all racks - GET:api/rack
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rack>>> GetRacks()
        {
            return await _context.Racks.ToListAsync();
        }

        //Get rack by id - GET:api/rack/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Rack>> GetRackById(int id)
        {
            var rack = await _context.Racks.FindAsync(id);
            
            if(rack == null)
            {
                return NotFound();
            }
            return rack;
        }

        //Create rack - POST:api/rack
        [HttpPost]
        public async Task<ActionResult<Rack>> CreateRack(Rack rack)
        {
            _context.Racks.Add(rack);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRack", new {id =  rack.Id}, rack);
        }

        //Edit rack - PUT:api/rack/id
        [HttpPut("{id}")]
        public async Task<ActionResult<Rack>> EditRack(int id, Rack rack)
        {
            if(id != rack.Id)
            {
                return BadRequest();
            }
            _context.Entry(rack).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete rack - DELETE:api/rack/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rack>> DeleteRack(int id)
        {
            var rack = await _context.Racks.FindAsync(id);

            if(rack == null)
            {
                return NotFound();
            }
            _context.Racks.Remove(rack);
            await _context.SaveChangesAsync();

            return NoContent();
              
        }

    }
}
