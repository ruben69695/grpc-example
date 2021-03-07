using System.Threading.Tasks;

using Grpc.Net.Client;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            
            var client = new Cats.CatsClient(channel);
            
            var reply = await client.GetRandomCatAsync(
                new GetRandomCatRequest ()
            );

            var replyTwo = await client.GetCatsAsync(
                new GetCatsRequest()
            );

            System.Console.WriteLine("RANDOM CAT:");
            System.Console.WriteLine(reply.Cat.Id);
            System.Console.WriteLine(reply.Cat.ResourceUrl);
            System.Console.WriteLine();

            System.Console.WriteLine("LIST OF CATS:");
            foreach (var cat in replyTwo.Cats)
            {
                System.Console.WriteLine(cat.Id);
                System.Console.WriteLine(cat.ResourceUrl);
                System.Console.WriteLine();
            }
        }
    }
}
