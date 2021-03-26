using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SocialNetworkSample.IntegrationTests
{
    internal class FakeLogger<T> : ILogger<T>
    {
        public List<(LogLevel Level, string Message)> Entries { get; } = new List<(LogLevel, string)>();

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            // This is relying on an internal implementation detail, it will break!
            var message = state.ToString();

            Entries.Add((logLevel, message));
        }
    }
}