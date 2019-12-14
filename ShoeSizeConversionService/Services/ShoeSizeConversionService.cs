using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ShoeSizeConversionService.Models;

namespace ShoeSizeConversionService
{
    public class ShoeSizeConversionService : ShoeSizeConversion.ShoeSizeConversionBase
    {
        private readonly ILogger<ShoeSizeConversionService> _logger;
        private static readonly Dictionary<double, double> womenSizes = new Dictionary<double, double>()
        {
            { 4, 35 },
            { 4.5, 35 },
            { 5, 36 },
            { 5.5, 36 },
            { 6, 37 },
            { 6.5, 37 },
            { 7, 38 },
            { 7.5, 39 },
            { 8, 39 },
            { 8.5, 40 },
            { 9, 40 },
            { 9.5, 41 },
            { 10, 41.5 },
            { 10.5, 42 },
            { 11, 43 },
            { 11.5, 43 },
            { 12, 44 }
        };
        private static readonly Dictionary<double, double> menSizes = new Dictionary<double, double>()
        {
            { 6 , 39 },
            { 6.5, 39 },
            { 7, 40 },
            { 7.5, 40 },
            { 8, 41 },
            { 8.5, 41.5 },
            { 9, 42 },
            { 9.5, 43 },
            { 10, 43.5 },
            { 10.5, 44 },
            { 11, 44 },
            { 11.5, 45 },
            { 12, 46 },
            { 13 , 47 },
            { 14 , 48 },
            { 15 , 49 },
            { 16 , 50}
        };

        public ShoeSizeConversionService(ILogger<ShoeSizeConversionService> logger)
        {
            _logger = logger;
        }

        public override Task<ConversionReply> ConvertShoeSize(ConversionRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ConversionReply
            {
                Size = (Gender)request.Gender == Gender.Women ? ConvertWomenShoeSize(request.Size) : ConvertMenShoeSize(request.Size)
            });
        }

        private double ConvertWomenShoeSize(double size)
        {
            double result = 0;
            womenSizes.TryGetValue(size, out result);
            return result;
        }

        private double ConvertMenShoeSize(double size)
        {
            double result = 0;
            menSizes.TryGetValue(size, out result);
            return result;
        }
    }
}
