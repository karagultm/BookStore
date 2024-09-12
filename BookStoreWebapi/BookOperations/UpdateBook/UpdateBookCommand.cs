using System;
using System.Linq;
using BookStoreWebapi.DBOperations;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace BookStoreWebapi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId {get; set;}
        public UpdateBookModel model; 
            private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {

            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
                throw new Exception("Böyle bir kitap bulunamadı!");

            book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
            book.Title = model.Title != default ? model.Title : book.Title;

            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
   

    }
}