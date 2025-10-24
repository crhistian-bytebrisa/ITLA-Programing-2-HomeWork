using LibraryWeb.Application.DTOs;
using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Application.Interfaces;
using LibraryWeb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LanguageController : Controller
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLanguages()
        {
            var languages = await _languageService.GetAllAsync();
            return Ok(languages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetLanguageById(int id)
        {
            var language = await _languageService.GetByIdAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            return Ok(language);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLanguage(CreateLanguageDTO createLanguageDTO)
        {
            var language = await _languageService.AddAsync(createLanguageDTO);
            return CreatedAtAction(nameof(GetLanguageById), new { id = language.Id }, language);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLanguage(int id, CreateLanguageDTO languageDTO)
        {
            var updatedLanguage = await _languageService.UpdateAsync(id, languageDTO);
            return Ok(updatedLanguage);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLanguage(int id)
        {
            await _languageService.DeleteAsync(id);
            return NoContent();
        }
    }
}
