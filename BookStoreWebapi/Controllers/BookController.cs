using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using BookStoreWebapi.BookOperations.CreateBook;
using BookStoreWebapi.BookOperations.GetBooks;
using BookStoreWebapi.DBOperations;
using Microsoft.AspNetCore.Mvc;
using static BookStoreWebapi.BookOperations.CreateBook.CreateBookCommand;

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
        public Book GetById(int id)
        {
            // var book = BookList.Where(x=> x.Id == id).FirstOrDefault();
            // var book = BookList.Where(x=> x.Id == id);
            var book = _context.Books.Where(x => x.Id == id).SingleOrDefault();
            return book;
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
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {

            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            _context.SaveChanges();
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