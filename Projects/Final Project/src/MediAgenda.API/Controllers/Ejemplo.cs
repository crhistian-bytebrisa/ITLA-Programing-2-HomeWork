using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Models;

namespace MediAgenda.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ejemplo : ControllerBase
    {
        private readonly MediContext _context;

        public Ejemplo(MediContext context)
        {
            _context = context;
        }

        // GET: api/Ejemplo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnalysisModel>>> GetAnalyses()
        {
            return await _context.Analyses.ToListAsync();
        }

        // GET: api/Ejemplo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalysisModel>> GetAnalysisModel(int id)
        {
            var analysisModel = await _context.Analyses.FindAsync(id);

            if (analysisModel == null)
            {
                return NotFound();
            }

            return analysisModel;
        }

        // PUT: api/Ejemplo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnalysisModel(int id, AnalysisModel analysisModel)
        {
            if (id != analysisModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(analysisModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnalysisModelExists(id))
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

        // POST: api/Ejemplo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AnalysisModel>> PostAnalysisModel(AnalysisModel analysisModel)
        {
            _context.Analyses.Add(analysisModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnalysisModel", new { id = analysisModel.Id }, analysisModel);
        }

        // DELETE: api/Ejemplo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysisModel(int id)
        {
            var analysisModel = await _context.Analyses.FindAsync(id);
            if (analysisModel == null)
            {
                return NotFound();
            }

            _context.Analyses.Remove(analysisModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnalysisModelExists(int id)
        {
            return _context.Analyses.Any(e => e.Id == id);
        }
    }
}
