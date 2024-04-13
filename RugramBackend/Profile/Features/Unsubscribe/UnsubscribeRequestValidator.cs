using FluentValidation;

namespace Profile.Features.Unsubscribe;

public class UnsubscribeRequestValidator : AbstractValidator<UnsubscribeRequest>
{
	public UnsubscribeRequestValidator()
	{
		RuleFor(x => x.NameOfProfileUnsubscribedTo)
			.Must(x => x.Length is >= 5 and <= 25);
		RuleFor(x => x.SubscriberId)
			.NotEqual(Guid.Empty);
	}
}