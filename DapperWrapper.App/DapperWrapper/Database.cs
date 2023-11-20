using DapperWrapper.Config;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWrapper
{
    public class Database
    {
        private readonly SqlHelperConfig configuration;

        public Database() { }

        public Database(SqlHelperConfig configuration) =>
            this.configuration = configuration;

        public string ConnectionString()
        {
            return this.configuration.ConnectionString;
        }

        public DbConnection CreateConnection()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(this.configuration.Provider);

            var connection = factory.CreateConnection();
            connection.ConnectionString = ConnectionString();
            connection.Open();
            return connection;
        }
    }
}
