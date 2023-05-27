using FluentValidation;
using SweetCandy.WebApi.Models;

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
                .GreaterThan(DateTime.MinValue)
                .WithMessage("Ngày hết hạn không hợp lệ");
            
            RuleFor(a => a.CategoryId)
                .GreaterThan(0)
                .WithMessage("ID danh mục không hợp lệ");
        }
    }
}
