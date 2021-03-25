using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SocialNetworkSample.Data.Entities
{
    [DebuggerDisplay("{Subscriber} -> {Publisher}")]
    public class SubscriptionEntity
    {
        [Required] public Guid SubscriberId { get; set; }

        [Required] public ClientEntity Subscriber { get; set; }
        
        [Required] public ClientEntity Publisher { get; set; }
        
        [Required] public Guid PublisherId { get; set; }
    }
}