using System;
using System.Linq;
using BookStoreWebapi.BookOperations.CreateBook;
using BookStoreWebapi.BookOperations.GetById;
using BookStoreWebapi.BookOperations.GetBooks;
using BookStoreWebapi.BookOperations.UpdateBook;
using BookStoreWebapi.DBOperations;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebapi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetByIdQuery query = new GetByIdQuery(_context);
            var result = query.Handle(id);

            return Ok(result);
        }

        // [HttpGet] // aynı anda 2 tane http get i yazamıyorsun eğer yazarsan conflict yersin haberin olsun.
        // public Book GetByIdQuerry([FromQuery]int  id){
        //     // var book = BookList.Where(x=> x.Id == id).FirstOrDefault();
        //     // var book = BookList.Where(x=> x.Id == id);
        //     var book = BookList.Where(x=> x.Id == id).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        //complex tiplerde yani classlar objetlerde from body kullanama gerek yok ama basit tiplerde
        //yani int string bool gibi ifadelerde from body kullanmak zorundasın 
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.model = updatedBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

    }
}