using DomainProduct.Interfaces.IRepositorys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class OfferService : BackgroundService
    {
        private readonly ILogger<OfferService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public OfferService(ILogger<OfferService> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Adding iPhone offer at: {time}", DateTimeOffset.Now);

                try
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
                        await AddIphoneOffer(productRepository);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding the iPhone offer.");
                }

                await Task.Delay(TimeSpan.FromSeconds(45), stoppingToken);
            }
        }

        private async Task AddIphoneOffer(IProductRepository productRepository)
        {
            var iphone = await productRepository.GetByCode(1); // simulando que codigo 1 é o iphone

            if (iphone != null)
            { 
                iphone.Situacao = "Ativo";
                iphone.Quantidade += 100;
                await productRepository.Update(iphone);
            }
            Console.WriteLine($"Oferta imperdivel {iphone.Descricao}, por apenas 1 real informe o código do produto {iphone.Codigo}");
        }
    }
}