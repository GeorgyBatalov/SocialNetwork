using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetworkSample.Data.Abstract;
using SocialNetworkSample.Services.Contracts.Queries;
using SocialNetworkSample.Services.Mappers;
using SocialNetworkSample.Services.Models;

namespace SocialNetworkSample.Services.Commands
{
    public sealed class GetMostPopularClientsRequestHandler : IRequestHandler<GetMostPopularClientsRequest, ICollection<Client>>
    {
        private readonly IDataContextFactory _dataContextFactory;

        public GetMostPopularClientsRequestHandler(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory ?? throw new ArgumentNullException(nameof(dataContextFactory));
        }

        public async Task<ICollection<Client>> Handle(GetMostPopularClientsRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Top <= 0)
                throw new ArgumentOutOfRangeException(nameof(request.Top));

            using var dataContext = _dataContextFactory.Create();

            var mostPopularClients = await dataContext.Clients
                .Include(x=>x.Subscribers)
                .Where(x=>x.Subscribers.Count > 0)
                .OrderByDescending(x => x.Subscribers.Count).Take(request.Top).ToListAsync(cancellationToken);

            return mostPopularClients.Select(x => x.ToDomain()).ToList();
        }
    }
}