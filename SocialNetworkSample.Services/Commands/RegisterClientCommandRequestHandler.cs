using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SocialNetworkSample.Data.Abstract;
using SocialNetworkSample.Services.Contracts.Commands;
using SocialNetworkSample.Services.Mappers;

namespace SocialNetworkSample.Services.Commands
{
    public sealed class RegisterClientCommandRequestHandler : IRequestHandler<RegisterClientCommandRequest, Guid>
    {
        private readonly IDataContextFactory _dataContextFactory;

        public RegisterClientCommandRequestHandler(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory ?? throw new ArgumentNullException(nameof(dataContextFactory));
        }

        public async Task<Guid> Handle(RegisterClientCommandRequest request, CancellationToken cancellationToken)
        {
            if (request?.Client == null)
                throw new ArgumentNullException(nameof(request.Client));

            using var dataContext = _dataContextFactory.Create();

            var entity = request.Client.ToEntity();

            await dataContext.AddAsync(entity, cancellationToken);

            await dataContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}