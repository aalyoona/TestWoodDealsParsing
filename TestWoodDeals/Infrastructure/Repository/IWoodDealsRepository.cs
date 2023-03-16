using System.Collections.Generic;
using TestWoodDeals.Models;

namespace TestWoodDeals
{
    public interface IWoodDealsRepository
    {
        void AddWoodDeal(ContentModel ContentModel);
        List<ContentModel> GetAllWoodDeals();
    }
}