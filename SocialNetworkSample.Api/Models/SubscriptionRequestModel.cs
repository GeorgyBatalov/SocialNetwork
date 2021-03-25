using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkSample.Api.Models
{
    public class SubscriptionRequestModel
    {
        /// <summary>
        ///     Publisher id
        /// </summary>
        [Required(ErrorMessage = "Publisher id is required field")]
        [DisplayName("Publisher id")]
        public Guid PublisherClientId { get; set; }

        /// <summary>
        ///     Subscriber id
        /// </summary>
        [Required(ErrorMessage = "Subscriber id is required field")]
        [DisplayName("Subscriber id")]
        public Guid SubscriberClientId { get; set; }
    }
}