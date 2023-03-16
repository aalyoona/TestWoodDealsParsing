using Microsoft.Extensions.DependencyInjection;
using System;
using System.Timers;

namespace TestWoodDeals
{
    public class StartUp
    {
        private readonly IServiceProvider _serviceProvider;

        public StartUp()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IWoodDealsService, WoodDealsService>()
            .AddSingleton<IWoodDealsRepository, WoodDealsRepository>()
            .AddSingleton<IRequestSendler, RequestSendler>()
            .AddMemoryCache()
            .BuildServiceProvider();
        }

        public void Start()
        {
            _serviceProvider.GetService<IWoodDealsService>().AddAllExistWoodDealsIntoCache();
            _serviceProvider.GetService<IRequestSendler>().SendRequest();

            Timer timer = new Timer(600000);
            timer.Elapsed += SendRequest;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void SendRequest(object source, ElapsedEventArgs a)
        {
            _serviceProvider.GetService<IRequestSendler>().SendRequest();
        }

    }
}