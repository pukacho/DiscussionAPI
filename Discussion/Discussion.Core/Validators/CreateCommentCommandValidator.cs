using FluentValidation;
using Discussion.Core.Commands;

namespace Discussion.Core.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");
            
            RuleFor(x => x.ParentId)
                .GreaterThan(0).WithMessage("ParentId must be greater than 0.");
            
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}