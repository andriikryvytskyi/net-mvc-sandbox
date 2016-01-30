using System.Data.Entity;
using WebMVCAppSandbox.DataAccessLayer;

namespace DataAccessLayer
{
    public class DatabaseSettings
    {
        public static void SetDatabase()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SalesERPDAL>());
        }
    }
}
