using System;
using MediatR;
using SocialNetworkSample.Services.Models;

namespace SocialNetworkSample.Services.Contracts.Commands
{
    /// <summary>
    ///     Запрос на создание Клиента
    /// </summary>
    /// <remarks>Делаем класс запечатанным и иммутабельным</remarks>
    public sealed class RegisterClientCommandRequest : IRequest<Guid>
    {
        public RegisterClientCommandRequest(Client client)
        {
            // Запрос с клиентам равным null, рушим всё и сразуы
            Client = client ?? throw new ArgumentException(nameof(client));
        }

        public Client Client { get; }
    }
}