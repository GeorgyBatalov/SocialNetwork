using System;
using MediatR;
using SocialNetworkSample.Services.Models;

namespace SocialNetworkSample.Services.Contracts.Commands
{
    /// <summary>
    ///     Запрос на подписку одного Клиента на другого
    /// </summary>
    /// <remarks>Делаем класс запечатанным и иммутабельным</remarks>
    public sealed class SubscribeClientCommandRequest : IRequest<bool>
    {
        public SubscribeClientCommandRequest(SubscriptionRequest subscriptionRequest)
        {
            // Запрос с клиентам равным null, рушим всё и сразуы
            SubscriptionRequest = subscriptionRequest ?? throw new ArgumentException(nameof(subscriptionRequest));

            if (subscriptionRequest.PublisherClientId == subscriptionRequest.SubscriberClientId)
                throw new ArgumentException("SubscriberClientId == PublisherClientId!");
        }

        public SubscriptionRequest SubscriptionRequest { get; }
    }
}