using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApiNewer.Models;
using BooksApiNewer.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksApiNewer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController: ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if(book == null)
            {
                return NotFound();
            }
            return book;
        }
    }
}
