using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreWebapi
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//bu eklemeyle birlikte id değişkenini oto incrementent yapıp bizim dışardan değiştirmemizin önüne geçiyoruz.
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
