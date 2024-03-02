using MediatR;
using SimpleResults;

namespace Movie.PoC.Api.Features.Auth;

public record LoginQuery : IRequest<Result>
{
    
}

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result>
{
    public async Task<Result> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}