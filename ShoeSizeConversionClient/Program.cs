using System;
using ShoeSizeConversionService;
using Grpc.Net.Client;
using System.Threading.Tasks;
using System.Net.Http;

namespace ShoeSizeConversionClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);

            var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var client = new ShoeSizeConversion.ShoeSizeConversionClient(channel);
            var conversionRequest = new ConversionRequest { Size = 11.5, Gender = ConversionRequest.Types.Gender.Women };
            var conversionReply = await client.ConvertShoeSizeAsync(conversionRequest);

            Console.WriteLine($"{conversionRequest.Size} {conversionRequest.Gender} US → {conversionReply.Size} EU");
            Console.ReadKey();
        }
    }
}
