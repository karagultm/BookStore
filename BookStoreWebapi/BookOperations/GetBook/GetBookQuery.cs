using System.Linq;
using BookStoreWebapi.Common;
using BookStoreWebapi.DBOperations;

namespace BookStoreWebapi.BookOperations.GetBook
{
    public class GetBookQuery
    {

        private readonly BookStoreDbContext _dbContext;

        public GetBookQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookViewModel Handle(int id)
        {
            // var book = BookList.Where(x=> x.Id == id).FirstOrDefault();
            // var book = BookList.Where(x=> x.Id == id);
            var book = _dbContext.Books.Where(x => x.Id == id).SingleOrDefault();
            BookViewModel vm = new BookViewModel(){
                Title = book.Title,
                PageCount = book.PageCount,
                Genre = ((GenreEnum)book.GenreId).ToString(),
                PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            };
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