using FluentValidation;

namespace EletroGestao.Dominio.ClienteRoot.Validation
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Id)
                   .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.");

            RuleFor(c => c.NomeRazaoSocial)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório.")
                .Length(11, 18).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.");
        }
    }
}
