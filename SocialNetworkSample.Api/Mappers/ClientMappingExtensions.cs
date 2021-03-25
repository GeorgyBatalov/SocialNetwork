using SocialNetworkSample.Api.Models;
using SocialNetworkSample.Services.Models;

namespace SocialNetworkSample.Api.Mappers
{
    internal static class ClientMappingExtensions
    {
        public static Client ToDomain(this ClientModel source)
        {
            if (source == null)
                return null;

            return new Client
            {
                Name = source.Name
            };
        }

        public static ClientModel ToModel(this Client source)
        {
            if (source == null)
                return null;

            return new ClientModel
            {
                Name = source.Name
            };
        }

        public static PopularClientModel ToPopelarClientModel(this Client source)
        {
            if (source == null)
                return null;

            return new PopularClientModel
            {
                Name = source.Name,
                SubscribersCount = source.SubscribersCount
            };
        }
    }
}