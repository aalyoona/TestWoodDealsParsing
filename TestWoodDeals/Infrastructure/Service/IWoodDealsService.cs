using TestWoodDeals.Models;

namespace TestWoodDeals
{
    public interface IWoodDealsService
    {
        void AddAllExistWoodDealsIntoCache();
        void AddWoodDeals(ContentModel woodDealModels);
    }
}