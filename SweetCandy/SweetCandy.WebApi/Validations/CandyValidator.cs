using FluentValidation;
using SweetCandy.WebApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SweetCandy.WebApi.Validations
{
    public class CandyValidator : AbstractValidator<CandyEditModel>
    {
        public CandyValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Tên chủ đề không được để trống");
            
            RuleFor(a => a.Price)
                .GreaterThan(0)
                .WithMessage("Giá tiền phải lớn hơn 0");
            
            RuleFor(a => a.ExpirationDate)
                .Must((date) => !date.Equals(default(DateTime)))
                .WithMessage("Ngày hết hạn không hợp lệ");
            
            RuleFor(a => a.CategoryId)
                .GreaterThan(0)
                .WithMessage("ID danh mục không hợp lệ");
        }
    }
}
