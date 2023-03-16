using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TestWoodDeals.Helpers;
using TestWoodDeals.Models;

namespace TestWoodDeals
{
    public class WoodDealsRepository : IWoodDealsRepository
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["MyLocal"].ConnectionString;

        public List<ContentModel> GetAllWoodDeals()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<ContentModel>(Queries.GetAllWoodDeals, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void AddWoodDeal(ContentModel ContentModel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Query(Queries.AddWoodDeal, new
                {
                    ContentModel.SellerName,
                    ContentModel.SellerInn,
                    ContentModel.BuyerName,
                    ContentModel.BuyerInn,
                    ContentModel.WoodVolumeBuyer,
                    ContentModel.WoodVolumeSeller,
                    ContentModel.DealDate,
                    ContentModel.DealNumber
                },
                  commandType: CommandType.StoredProcedure);
            }
        }
    }
}