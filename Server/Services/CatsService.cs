using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Grpc.Core;
using System.IO;
using Google.Protobuf;

namespace Server
{
    public class CatsService : Cats.CatsBase
    {
        private readonly ILogger<CatsService> _logger;

        public CatsService(ILogger<CatsService> logger)
        {
            _logger = logger;
        }

        public override async Task GetRandomCat(GetRandomCatRequest request, IServerStreamWriter<CatStreamReply> responseStream, ServerCallContext context)
        {
            using var fs = File.Open("images/1.jpg", FileMode.Open);
            int bytesReaded;

            var buffer = new byte[1024];

            while ((bytesReaded = await fs.ReadAsync(buffer)) > 0)
            {
                var catStreamReply = new CatStreamReply
                {
                    ImageStream = ByteString.CopyFrom(buffer[0..bytesReaded])
                };
                
                await responseStream.WriteAsync(catStreamReply);
            }
        }

    }
}