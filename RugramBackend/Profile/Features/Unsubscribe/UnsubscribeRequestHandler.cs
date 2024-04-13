using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Profile.Data;

namespace Profile.Features.Unsubscribe;

public class UnsubscribeRequestHandler(AppDbContext appDbContext)
	: IGrpcRequestHandler<UnsubscribeRequest, UnsubscribeResponse>
{
	public async Task<GrpcResult<UnsubscribeResponse>> Handle(
		UnsubscribeRequest request,
		CancellationToken cancellationToken)
	{
		var subscriber = await appDbContext.UserProfiles
			.Include(x => x.SubscribedTo)
			.FirstOrDefaultAsync(x => x.Id == request.SubscriberId, cancellationToken);
		var unsubscribedTo = await appDbContext.UserProfiles
			.FirstOrDefaultAsync(x => x.Id == request.IdOfProfileUnsubscribedTo, cancellationToken);

		if (subscriber is null || unsubscribedTo is null) return StatusCodes.Status404NotFound;
		
		if (subscriber.SubscribedTo.Any(x => x.Id == request.IdOfProfileUnsubscribedTo))
		{
			subscriber.SubscribedTo.Remove(unsubscribedTo);
			await appDbContext.SaveChangesAsync(cancellationToken);
		}
		
		return new UnsubscribeResponse();
	}
}