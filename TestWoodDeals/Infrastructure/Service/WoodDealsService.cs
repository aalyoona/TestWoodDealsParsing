using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using TestWoodDeals.Models;

namespace TestWoodDeals
{
    public class WoodDealsService : IWoodDealsService
    {
        private readonly IWoodDealsRepository _rep;
        private readonly IMemoryCache _cache;

        public WoodDealsService(IWoodDealsRepository rep, IMemoryCache cache)
        {
            _rep = rep;
            _cache = cache;
        }

        public void AddAllExistWoodDealsIntoCache()
        {
            List<ContentModel> woodDealModels = _rep.GetAllWoodDeals();
            foreach (ContentModel woodDealModel in woodDealModels)
            {
                _cache.Set(woodDealModel.Id, woodDealModel);
            }
        }

        public void AddWoodDeals(ContentModel woodDeal)
        {
            var deal = _cache.Get(woodDeal.Id);
            if (deal == null)
            {
                _rep.AddWoodDeal(woodDeal);
                _cache.Set(woodDeal.Id, woodDeal);
            }
        }
    }
}
