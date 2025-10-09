using LibraryWeb.Application.DTOs;
using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Application.Interfaces;
using LibraryWeb.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetAllWithDetails()
        {
            var books = await _bookService.GetAllWithDetailsAsync();
            return Ok(books);
        }


        [HttpGet("Details/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookWithDetailsById(int id)
        {
            var book = await _bookService.GetWithDetailsByIdAsync(id);
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookDTO createBookDTO)
        {
            var book = await _bookService.AddAsync(createBookDTO);
            return CreatedAtAction(nameof(GetBookWithDetailsById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, CreateBookDTO bookDTO)
        {
            var updatedBook = await _bookService.UpdateAsync(id,bookDTO);
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }


    }
}
