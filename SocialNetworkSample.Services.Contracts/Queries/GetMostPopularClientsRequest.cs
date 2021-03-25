using System;
using System.Collections.Generic;
using MediatR;
using SocialNetworkSample.Services.Models;

namespace SocialNetworkSample.Services.Contracts.Queries
{
    /// <summary>
    ///     Запрос на получение самых популярных Клиентов соц сети
    /// </summary>
    /// <remarks>Делаем класс запечатанным и иммутабельным</remarks>
    public sealed class GetMostPopularClientsRequest : IRequest<ICollection<Client>>
    {
        public GetMostPopularClientsRequest(int top)
        {
            if (top <= 0)
                throw new ArgumentOutOfRangeException(nameof(top));

            Top = (int) top;
        }

        public int Top { get; }
    }
}