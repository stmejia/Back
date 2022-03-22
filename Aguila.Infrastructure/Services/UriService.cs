using Aguila.Core.Interfaces;
using System;

namespace Aguila.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetUriServer()
        {
            return new Uri(_baseUri);
        }

    }
}
