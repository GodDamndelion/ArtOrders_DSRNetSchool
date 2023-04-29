namespace ArtOrders.Web;

using FluentValidation;

public class RegisterUserAccountModel
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public int? AvatarId { get; set; }
	public string? Role { get; set; }
	public string? Description { get; set; }
}

public class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountModel>
{
	public RegisterUserAccountModelValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty().WithMessage("User name is required.");

		RuleFor(x => x.Email)
			.EmailAddress().WithMessage("Email is required.");

		RuleFor(x => x.Password)
			.NotEmpty().WithMessage("Password is required.")
			.MaximumLength(50).WithMessage("Password is too long.");
	}

	public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
	{
		var result = await ValidateAsync(ValidationContext<RegisterUserAccountModel>.CreateWithOptions((RegisterUserAccountModel)model, x => x.IncludeProperties(propertyName)));
		if (result.IsValid)
			return Array.Empty<string>();
		return result.Errors.Select(e => e.ErrorMessage);
	};
}