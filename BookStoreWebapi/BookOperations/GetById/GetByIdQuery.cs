using System;
using System.Linq;
using AutoMapper;
using BookStoreWebapi.Common;
using BookStoreWebapi.DBOperations;

namespace BookStoreWebapi.BookOperations.GetById
{
    public class GetByIdQuery
    {
        public int BookId {get; set;}

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetByIdQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public BookViewModel Handle()
        {
            // var book = BookList.Where(x=> x.Id == id).FirstOrDefault();
            // var book = BookList.Where(x=> x.Id == id);
            var book = _dbContext.Books.Where(x => x.Id == BookId).SingleOrDefault();
            if (book is null)
                throw new Exception("Kitap bulunamadı");
                
            BookViewModel vm = _mapper.Map<BookViewModel>(book);//new BookViewModel(){
            //     Title = book.Title,
            //     PageCount = book.PageCount,
            //     Genre = ((GenreEnum)book.GenreId).ToString(),
            //     PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            // };
          return vm;  
        }
    }
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}