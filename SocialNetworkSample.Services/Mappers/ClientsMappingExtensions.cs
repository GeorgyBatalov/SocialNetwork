using SocialNetworkSample.Data.Entities;
using SocialNetworkSample.Services.Models;

namespace SocialNetworkSample.Services.Mappers
{
    internal static class ClientsMappingExtensions
    {
        public static ClientEntity ToEntity(this Client source)
        {
            if (source == null)
                return null;

            return new ClientEntity
            {
                Name = source.Name
            };
        }

        public static Client ToDomain(this ClientEntity source)
        {
            if (source == null)
                return null;

            return new Client
            {
                Name = source.Name,
                SubscribersCount = source.Subscribers?.Count ?? 0
            };
        }
    }
}