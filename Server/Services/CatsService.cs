using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Grpc.Core;

namespace Server
{
    public class CatsService : Cats.CatsBase
    {
        private readonly ILogger<CatsService> _logger;
        private readonly Data _data;

        public CatsService(ILogger<CatsService> logger, Data data)
        {
            _logger = logger;
            _data = data;
        }

        public override async Task<GetRandomCatReply> GetRandomCat(GetRandomCatRequest request, ServerCallContext context)
        {
            if (!_data.Cats.Any())
            {
                throw new Exception("Empty cats, load more cats MTF!!!!");
            }

            _logger.LogInformation("Getting random cat");

            var luckyCat = _data.Cats.ElementAt(new Random().Next(0, _data.Cats.Count()));

            return await Task.FromResult(new GetRandomCatReply
            {
                Cat = new Cat
                {
                    Id = luckyCat.Id.ToString(),
                    ResourceUrl = luckyCat.ResourceUrl
                }
            });
        }

        public override async Task<GetCatsReply> GetCats(GetCatsRequest request, ServerCallContext context)
        {
            if (!_data.Cats.Any())
            {
                throw new Exception("Empty cats, load more cats MTF!!!!");
            }

            _logger.LogInformation("Getting cats");

            var reply = new GetCatsReply();

            foreach (var cat in _data.Cats)
            {
                reply.Cats.Add(
                    new Cat {
                        Id = cat.Id.ToString(),
                        ResourceUrl = cat.ResourceUrl
                    }
                );
            }            

            return await Task.FromResult(reply);
        }
    }
}