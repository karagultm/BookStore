using System;
using System.Linq;
using BookStoreWebapi.BookOperations.CreateBook;
using BookStoreWebapi.BookOperations.GetById;
using BookStoreWebapi.BookOperations.GetBooks;
using BookStoreWebapi.BookOperations.UpdateBook;
using BookStoreWebapi.DBOperations;
using Microsoft.AspNetCore.Mvc;
using BookStoreWebapi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace BookStoreWebapi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            BookViewModel result;
            try
            {
                GetByIdQuery query = new GetByIdQuery(_context, _mapper);
                query.BookId = id;
                GetByIdQueryValidator validator = new GetByIdQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                // ValidationResult result = validator.Validate(command); 
                validator.ValidateAndThrow(command);
                command.Handle();
                // if (!result.IsValid)
                //     foreach (var item in result.Errors)
                //         Console.WriteLine("Özellik: " + item.PropertyName + " - Mesaj: " + item.ErrorMessage);
                // else
                //     command.Handle();
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
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.BookId = id;
                command.Handle();
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
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}