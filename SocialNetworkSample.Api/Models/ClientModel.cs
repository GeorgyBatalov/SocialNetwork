using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkSample.Api.Models
{
    public class ClientModel
    {
        /// <summary>
        ///     Client name
        /// </summary>
        /// <remarks>»м€ должно состо€ть из букв и пробелов и быть не длиннее 64 символов</remarks>
        [Required]
        [StringLength(64, ErrorMessage = "Name length can't be more than 64.")]
        [RegularExpression(@"^[а-€ј-яa-zA-Z ]*$", ErrorMessage = "Characters are not allowed.")]
        [DisplayName("Client name")]
        public string Name { get; set; }
    }
}