using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkSample.Api.Models
{
    public class ClientModel
    {
        /// <summary>
        ///     Client name
        /// </summary>
        /// <remarks>��� ������ �������� �� ���� � �������� � ���� �� ������� 64 ��������</remarks>
        [Required]
        [StringLength(64, ErrorMessage = "Name length can't be more than 64.")]
        [RegularExpression(@"^[�-��-�a-zA-Z ]*$", ErrorMessage = "Characters are not allowed.")]
        [DisplayName("Client name")]
        public string Name { get; set; }
    }
}