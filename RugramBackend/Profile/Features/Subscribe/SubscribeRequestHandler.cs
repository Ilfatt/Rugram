using Infrastructure.MediatR.Contracts;
using Microsoft.EntityFrameworkCore;
using Profile.Data;

namespace Profile.Features.Subscribe;

public class SubscribeRequestHandler(AppDbContext appDbContext) 
	: IGrpcRequestHandler<SubscribeRequest, SubscribeResponse>
{
	public async Task<GrpcResult<SubscribeResponse>> Handle(
		SubscribeRequest request,
		CancellationToken cancellationToken)
	{
		var subscriber = await appDbContext.UserProfiles
			.Include(x => x.SubscribedTo)
			.FirstOrDefaultAsync(x => x.Id == request.SubscriberId, cancellationToken);
		var subscribedTo = await appDbContext.UserProfiles
			.FirstOrDefaultAsync(x => x.Id == request.IdOfProfileSubscribedTo, cancellationToken);

		if (subscriber is null || subscribedTo is null) return StatusCodes.Status404NotFound;
		
		subscriber.SubscribedTo.Add(subscribedTo);

		await appDbContext.SaveChangesAsync(cancellationToken);

		return new SubscribeResponse();
	}
}