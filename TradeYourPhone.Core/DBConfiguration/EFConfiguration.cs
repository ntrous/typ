using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace TradeYourPhone.Core.DBConfiguration
{
    public class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
