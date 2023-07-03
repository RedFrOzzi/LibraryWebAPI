using FluentValidation;
using LibraryWebAPI.Models;

namespace LibraryWebAPI.Validation;

public class BookStackValidator : AbstractValidator<BookStack>
{
    public BookStackValidator()
    {
        RuleFor(bs => bs.Title).NotEmpty().WithMessage("Title can not be empty");
    }
}
