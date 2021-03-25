using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetworkSample.Api.Mappers;
using SocialNetworkSample.Api.Models;
using SocialNetworkSample.Services.Contracts.Commands;
using SocialNetworkSample.Services.Contracts.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialNetworkSample.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator, ILogger<ClientsController> logger)
        {
            // Без медиатора сервис работать не должен - рушим всё и сразу
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            // Я решил, что логирование - обязательно должно быть (могу и передумать, но пока аргументов против нет - оно будет обязательным)
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        /// <summary>
        ///     Регистрация клиента
        /// </summary>
        /// <remarks>
        ///     Делаю отдельный роут, а не просто POST, так как это всё-таки регистрация, а не создание и
        ///     потом регистрация может превратиться во что-то более сложное и включать в себя какую-то дополнительную логику
        /// </remarks>
        [HttpPost("register")]
        [SwaggerResponse((int) HttpStatusCode.OK, type: typeof(Guid))]
        public async Task<IActionResult> RegisterAsync(ClientModel client)
        {
            // на все случаи жизни, например для тестов
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _logger.LogInformation($"Client with name '{client.Name}' registration requested.");


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Guid id;

            try
            {
                id = await _mediator.Send(new RegisterClientCommandRequest(client.ToDomain()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while trying to register client with name '{client.Name}'.");
                // Именно так, чтобы сохранить трэйс лог
                throw;
            }

            return Ok(id);
        }

        /// <summary>
        ///     Подписать одного клиента на другого
        /// </summary>
        [HttpPost("subscribe")]
        [SwaggerResponse((int) HttpStatusCode.OK, type: typeof(Guid))]
        public async Task<IActionResult> SubscribeAsync(SubscriptionRequestModel request)
        {
            _logger.LogInformation($"Subscription client with id {request.SubscriberClientId} to client with id {request.PublisherClientId} has been requested.");

            // на все случаи жизни, например для тестов
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.PublisherClientId == request.SubscriberClientId)
                throw new ArgumentException("SubscriberClientId == PublisherClientId!");

            bool subscribed;

            try
            {
                subscribed = await _mediator.Send(new SubscribeClientCommandRequest(request.ToDomain()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error while trying to subscribe client with id {request.SubscriberClientId} to client with id {request.PublisherClientId}.");
                throw;
            }

            if (!subscribed)
                return BadRequest();

            return Ok();
        }

        /// <summary>
        ///     Возвращает топ наиболее популярных клиентов
        /// </summary>
        /// <param name="top">Топ</param>
        [HttpGet("most-popular")]
        [SwaggerResponse((int) HttpStatusCode.OK, type: typeof(Guid))]
        public async Task<IActionResult> GetMostPopularAsync(int top = 10)
        {
            _logger.LogInformation($"Top {top} of most popular clients requested.");

            if (top <= 0)
                ModelState.AddModelError(string.Empty, "top value out of range");
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ICollection<PopularClientModel> mostPopularClients;

            try
            {
                mostPopularClients = (await _mediator.Send(new GetMostPopularClientsRequest(top))).Select(x => x.ToPopelarClientModel()).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to get most popular clients.");
                throw;
            }

            return Ok(mostPopularClients);
        }
    }
}