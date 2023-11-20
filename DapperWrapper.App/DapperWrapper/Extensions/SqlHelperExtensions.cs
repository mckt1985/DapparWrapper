using DapperWrapper.Config;
using DapperWrapper.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWrapper.Extensions
{
    public static class SqlHelperExtensions
    {
        public static IServiceCollection AddSqlHelper(this IServiceCollection services, SqlHelperConfig config)
        {
            services.AddSingleton<ISqlHelper>(x => new SqlHelper(config));
            return services;
        }
    }
}
