using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text.Json;

namespace Server
{
    public class Data
    {
        public IEnumerable<CatEntity> Cats { get; private set; }

        public Data()
        {
            Cats = new List<CatEntity>();
        }

        public void Load(string route, CancellationToken cancellationToken = default)
        {
            var data = File.ReadAllText(route);
            
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };

            Cats = JsonSerializer.Deserialize<IEnumerable<CatEntity>>(data, jsonOptions);
        }
    }
}