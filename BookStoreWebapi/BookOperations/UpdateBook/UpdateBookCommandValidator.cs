using System.Data;
using FluentValidation;

namespace BookStoreWebapi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(x => x.model.GenreId).GreaterThan(0);
            RuleFor(x => x.model.Title).MinimumLength(3).NotEmpty();
            RuleFor(x=>x.BookId).GreaterThan(0);
        }
    }
}