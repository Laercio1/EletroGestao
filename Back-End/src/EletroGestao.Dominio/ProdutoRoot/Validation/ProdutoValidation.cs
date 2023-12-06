using FluentValidation;

namespace EletroGestao.Dominio.ProdutoRoot.Validation
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(c => c.Id)
                   .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");

            RuleFor(c => c.NomeProduto)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}
