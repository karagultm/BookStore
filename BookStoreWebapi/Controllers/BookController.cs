using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebapi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book> {
     
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            // var book = BookList.Where(x=> x.Id == id).FirstOrDefault();
            // var book = BookList.Where(x=> x.Id == id);
            var book = BookList.Where(x => x.Id == id).SingleOrDefault();
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
        public IActionResult AddBook([FromBody] Book newBook)
        {

            var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

            if (book is not null)
                return BadRequest();

            BookList.Add(newBook);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {

            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();

            BookList.Remove(book);
            return Ok();
        }

    }
}