using DirectoryStructureApp.BLL.DTOs;
using FluentValidation;

namespace DirectoryStructureApp.BLL.Validators;

public class DirectoryDtoValidator : AbstractValidator<DirectoryDto>
{
    public DirectoryDtoValidator()
    {
        RuleFor(d => d.Id).NotNull();
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(1, 255).WithMessage("Name length should be between 1 and 255 characters.");
    }
}