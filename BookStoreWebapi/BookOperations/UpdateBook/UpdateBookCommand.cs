using System;
using System.Linq;
using BookStoreWebapi.DBOperations;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace BookStoreWebapi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel model; 
            private readonly BookStoreDbContext _dbContext;

        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(int id)
        {

            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                throw new Exception("Böyle bir kitap bulunamadı!");

            book.PageCount = model.PageCount != default ? model.PageCount : book.PageCount;
            book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
            book.Title = model.Title != default ? model.Title : book.Title;
            book.PublishDate = model.PublishDate != default ? model.PublishDate : book.PublishDate;

            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}