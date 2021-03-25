using System;

namespace SocialNetworkSample.Services.Models
{
    public class SubscriptionRequest
    {
        public Guid SubscriberClientId { get; set; }
        public Guid PublisherClientId { get; set; }
    }
}