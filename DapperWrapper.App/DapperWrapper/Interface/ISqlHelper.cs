using DapperWrapper.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperWrapper.Interface
{
    public interface ISqlHelper
    {
        T GetRecord<T>(string spName, List<ParameterInfo> parameters, CommandType _commandType);
        Task<T> GetRecordAsync<T>(string spName, List<ParameterInfo> parameters, CommandType _commandType);
        Task<T> GetRecordAsync<T>(IDbTransaction _trans, IDbConnection _connection, string spName, List<ParameterInfo> parameters, CommandType _commandType);
        T GetRecord<T>(IDbTransaction _trans, IDbConnection _connection, string spName, List<ParameterInfo> parameters, CommandType _commandType);
        List<T> GetRecords<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        Task<IEnumerable<T>> GetRecordsAsync<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        List<T> GetRecords<T>(IDbTransaction _trans, IDbConnection _connection, string sql, List<ParameterInfo> parameters, CommandType _commandType);
        int GetIntRecord<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        int ExecuteQuery(string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout);
        IEnumerable<IDictionary<string, object>> Query(string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout);
        IEnumerable<IDictionary<int, string>> Query_Dictionary(string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout);
        IEnumerable<dynamic> Query(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        IEnumerable<dynamic> Query(IDbTransaction _transaction, IDbConnection _connection, string sql, List<ParameterInfo> parameters, CommandType _commandType);
        IDictionary<string, object> Query(IDbTransaction _transaction, IDbConnection _connection, string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout);
        int ExecuteQuery(IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout);
        int ExecuteQuery(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        int ExecuteQuery(IDbConnection _connection, IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType);
        int ExecuteScalar(IDbConnection _connection, IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType);
        Task<int> ExecuteScalarAsync(IDbConnection _connection, IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType);
        Task<int> ExecuteScalarAsync(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        int ExecuteScalar(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        T ExecuteScalar<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType);
        int ExecuteQueryWithIntOutputParam(string spName, List<ParameterInfo> parameters, CommandType _commandType);
        int ExecuteScalar(IDbConnection dbConnection, List<ParameterInfo> @params, CommandType storedProcedure);
    }
}
