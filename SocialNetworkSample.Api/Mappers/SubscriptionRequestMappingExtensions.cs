using SocialNetworkSample.Api.Models;
using SocialNetworkSample.Services.Models;

namespace SocialNetworkSample.Api.Mappers
{
    internal static class SubscriptionRequestMappingExtensions
    {
        public static SubscriptionRequest ToDomain(this SubscriptionRequestModel source)
        {
            if (source == null)
                return null;

            return new SubscriptionRequest
            {
                PublisherClientId = source.PublisherClientId,
                SubscriberClientId = source.SubscriberClientId
            };
        }
    }
}