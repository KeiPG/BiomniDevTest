using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : Controller
    {
        private readonly BooksContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BooksContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Books
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving books");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Books/Details/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBooksById(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound("Book not found");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving book with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: Books/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Title,Author,PublicationDate,EditionNumber,Isbn")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var maxId = await _context.Books.MaxAsync(b => (int?)b.Id) ?? 0;
                book.Id = maxId + 1;

                _context.Add(book);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetBooksById), new { id = book.Id }, book);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error creating book");
                return StatusCode(500, "An error occurred while saving the book.");
            }
        }

        // PUT: Books/Edit/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,PublicationDate,EditionNumber,Isbn")] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("Book ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BookExists(book.Id))
                {
                    return NotFound("Book not found");
                }
                else
                {
                    _logger.LogError(ex, "Concurrency error updating book");
                    return StatusCode(500, "An error occurred while updating the book.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating book");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: Books/Delete/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound("Book not found");
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting book with ID {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
