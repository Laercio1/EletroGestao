using FluentValidation;

namespace EletroGestao.Dominio.PedidoRoot.Validation
{
    public class PedidoValidation : AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");

            RuleFor(c => c.IdCliente)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");

            RuleFor(c => c.NomeCliente)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.IdProduto)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");

            RuleFor(c => c.NomeProduto)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.CEP)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(8, 9).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Regiao)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .MaximumLength(50).WithMessage("O campo {PropertyName} deve ter menos de {MaxLength} caracteres.");

            RuleFor(c => c.NumeroPedido)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .MaximumLength(50).WithMessage("O campo {PropertyName} deve ter menos de {MaxLength} caracteres.");

            RuleFor(c => c.ValorProduto)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

            RuleFor(c => c.ValorFinal)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
        }
    }
}
