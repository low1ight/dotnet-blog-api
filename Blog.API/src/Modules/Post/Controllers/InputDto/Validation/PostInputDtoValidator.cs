using FluentValidation;

namespace Blog.API.Modules.Post.Controllers.InputDto.Validation;

public class CreatePostDtoValidator : AbstractValidator<PostInputDto>
{
    public CreatePostDtoValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(2)
            .MaximumLength(50);
        
        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(3)
            .MaximumLength(150);
        
        RuleFor(p => p.Content)
            .NotEmpty().WithMessage("Content is required")
            .MinimumLength(10)
            .MaximumLength(400);
        
    }
}