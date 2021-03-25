using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SocialNetworkSample.Data.Entities
{
    [DebuggerDisplay("{Name} ({Id})")]
    public class ClientEntity : EntityBase
    {
        [StringLength(64)] [Required] public string Name { get; set; }

        public ICollection<SubscriptionEntity> Subscribers { get; set; }

        public ICollection<SubscriptionEntity> Publishers { get; set; }
    }
}