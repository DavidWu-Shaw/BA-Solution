using Framework.Data;
using Framework.UoW;
using Framework.Core;
using Framework.Sql;
using System.Configuration;

namespace BA.UnityRegistry
{
    public class DataStoreResolver : IDataStoreResolver
    {
        public const string BaDataStoreKey = "BaDataStore";
        public const string CRMDataStoreKey = "CRMDataStore";

        public DataStoreResolver()
        { }

        public IDataStore Resolve(string dataStoreKey)
        {
            ArgumentValidator.IsNotNullOrEmpty("dataStoreKey", dataStoreKey);

            IConnectionString connectionString = GetConnectionString(dataStoreKey);

            switch (dataStoreKey)
            {
                case DataStoreResolver.BaDataStoreKey:
                    return new BaDataStore(connectionString, true);
                case DataStoreResolver.CRMDataStoreKey:
                    return new CRMDataStore(connectionString, true);
                default:
                    return null;
            }
        }

        public static IConnectionString GetConnectionString(string dataStoreKey)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[dataStoreKey].ConnectionString;
            IConnectionString connection = ConnectionStringFactory.Build(connectionString);
            return connection;
        }
    }

}
