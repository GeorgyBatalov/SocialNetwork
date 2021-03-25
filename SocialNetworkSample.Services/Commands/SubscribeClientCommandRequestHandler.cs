using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialNetworkSample.Data.Abstract;
using SocialNetworkSample.Data.Entities;
using SocialNetworkSample.Services.Contracts.Commands;

namespace SocialNetworkSample.Services.Commands
{
    /// <summary>
    ///     Обработчик комманд на подписку одного клиента на другого
    /// </summary>
    /// <remarks>
    ///     Можно конечно побогаче ответ предоставить. Типа: подписан, уже подписан, не найден и т.п. Но времени не
    ///     хватает
    /// </remarks>
    public sealed class SubscribeClientCommandRequestHandler : IRequestHandler<SubscribeClientCommandRequest, bool>
    {
        private readonly IDataContextFactory _dataContextFactory;

        public SubscribeClientCommandRequestHandler(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory ?? throw new ArgumentNullException(nameof(dataContextFactory));
        }

        public async Task<bool> Handle(SubscribeClientCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.SubscriptionRequest == null)
                throw new ArgumentNullException(nameof(request.SubscriptionRequest));

            // Сам на себя не должен подписываться
            if (request.SubscriptionRequest.PublisherClientId == request.SubscriptionRequest.SubscriberClientId)
                throw new ArgumentException("SubscriberClientId == PublisherClientId!");

            using var dataContext = _dataContextFactory.Create();
            
            // Может быть уже существует подписка
            var alreadyExists = await dataContext.Subscriptions.AnyAsync(x=> x.PublisherId == request.SubscriptionRequest.PublisherClientId && x.SubscriberId == request.SubscriptionRequest.SubscriberClientId, cancellationToken);

            if (alreadyExists)
                return false;

            // Получаем обоих одним запросом
            var clients = await dataContext.Clients
                .Where(x => x.Id == request.SubscriptionRequest.SubscriberClientId || x.Id == request.SubscriptionRequest.PublisherClientId).Take(2)
                .ToListAsync(cancellationToken);

            // Если их не 2, то таких клиентов не существует (или одного из них)
            if (clients == null || clients.Count != 2)
                return false;

            var subscriber = clients.First(x => x.Id == request.SubscriptionRequest.SubscriberClientId);
            
            var publisher = clients.First(x => x.Id == request.SubscriptionRequest.PublisherClientId);

            var entity = new SubscriptionEntity
            {
                Publisher = publisher,
                Subscriber = subscriber
            };

            await dataContext.AddAsync(entity, cancellationToken);

            await dataContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}