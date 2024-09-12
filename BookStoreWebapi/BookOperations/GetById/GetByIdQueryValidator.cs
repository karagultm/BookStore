using System.IO.Compression;
using FluentValidation;

namespace BookStoreWebapi.BookOperations.GetById
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
        }

    }
}