using FluentValidation;
using SweetCandy.WebApi.Models;

namespace SweetCandy.WebApi.Validations
{
    public class CategoryValidator : AbstractValidator<CategoryEditModel>
    {
        public CategoryValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Tên chủ đề không được để trống");
        }
    }
}
