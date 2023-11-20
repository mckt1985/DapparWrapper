using Dapper;
using DapperWrapper.Config;
using DapperWrapper.Interface;
using DapperWrapper.Models;
using System.Data;

namespace DapperWrapper
{
    public class SqlHelper : Database, ISqlHelper
    {
        private readonly char Prefix;

        public SqlHelper(SqlHelperConfig config)
            : base(config)
        {
            Prefix = config.Provider == "NpgSql" ? ':' : '@';
        }

        /// <summary>
        /// Gets the object from the db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T GetRecord<T>(string spName, List<ParameterInfo> parameters, CommandType _commandType)
        {
            T _record = default(T);
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters p = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        p.Add(Prefix + param.Name, param.Value);
                    }
                }

                _record = SqlMapper.Query<T>(_connection, spName, p, commandType: _commandType).FirstOrDefault();

            }

            return _record;
        }

        public async Task<T> GetRecordAsync<T>(string spName, List<ParameterInfo> parameters, CommandType _commandType)
        {
            T _record = default(T);
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters p = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        p.Add(Prefix + param.Name, param.Value);
                    }
                }

                var result = await SqlMapper.QueryAsync<T>(_connection, spName, p, commandType: _commandType);

                _record = result.FirstOrDefault();
            }

            return _record;
        }

        public async Task<T> GetRecordAsync<T>(IDbTransaction _trans, IDbConnection _connection, string spName, List<ParameterInfo> parameters, CommandType _commandType)
        {
            T _record = default(T);

            DynamicParameters p = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    p.Add(Prefix + param.Name, param.Value);
                }
            }

            var result = await SqlMapper.QueryAsync<T>(_connection, spName, p, transaction: _trans, commandType: _commandType);

            _record = result.FirstOrDefault();


            return _record;
        }

        /// <summary>
        /// Gets the object from the db
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T GetRecord<T>(IDbTransaction _trans, IDbConnection _connection, string spName, List<ParameterInfo> parameters, CommandType _commandType)
        {
            T _record = default(T);
            DynamicParameters p = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    p.Add(Prefix + param.Name, param.Value);
                }
            }

            _record = SqlMapper.Query<T>(_connection, spName, p, transaction: _trans, commandType: _commandType).FirstOrDefault();


            return _record;
        }

        /// <summary>
        /// Gets a list of dynamic objects from the db 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="_commandType"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            List<T> lstValues = new List<T>();
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        _params.Add(Prefix + param.Name, param.Value);
                    }
                }

                lstValues = SqlMapper.Query<T>(_connection, sql, _params, commandType: _commandType, commandTimeout: 180).ToList();

            }

            return lstValues;
        }

        /// <summary>
        /// Gets a list of dynamic objects from the db 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="_commandType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetRecordsAsync<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            IEnumerable<T> lstValues;
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        _params.Add(Prefix + param.Name, param.Value);
                    }
                }

                lstValues = await SqlMapper.QueryAsync<T>(_connection, sql, _params, commandType: _commandType, commandTimeout: 180);
            }

            return lstValues;
        }

        /// <summary>
        /// Gets a list of dynamic objects from the db 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="_commandType"></param>
        /// <returns></returns>
        public List<T> GetRecords<T>(IDbTransaction _trans, IDbConnection _connection, string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            List<T> lstValues = new List<T>();
            DynamicParameters _params = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value);
                }
            }

            lstValues = SqlMapper.Query<T>(_connection, sql, _params, transaction: _trans, commandType: _commandType, commandTimeout: 180).ToList();

            return lstValues;
        }

        public int GetIntRecord<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int iRetVal = 0;
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value);
                }

                iRetVal = SqlMapper.Query<int>(_connection, sql, _params, commandType: _commandType).FirstOrDefault();
            }

            return iRetVal;
        }

        public int ExecuteQuery(string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout)
        {
            int success = 0;
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        _params.Add(Prefix + param.Name, param.Value);
                    }
                }

                success = SqlMapper.Execute(_connection, sql, _params, commandTimeout: CommandTimeout, commandType: _commandType);
            }

            return success;
        }

        public IEnumerable<IDictionary<string, object>> Query(string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout)
        {
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        if (param.Value == null)
                            _params.Add(Prefix + param.Name, null);
                        else
                            _params.Add(Prefix + param.Name, param.Value.ToString() == "" ? null : param.Value);
                    }
                }

                return SqlMapper.Query(_connection, sql, _params, commandTimeout: CommandTimeout, commandType: _commandType) as IEnumerable<IDictionary<string, object>>;
            }
        }

        public IEnumerable<IDictionary<int, string>> Query_Dictionary(string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout)
        {
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        if (param.Value == null)
                            _params.Add(Prefix + param.Name, null);
                        else
                            _params.Add(Prefix + param.Name, param.Value.ToString() == "" ? null : param.Value);
                    }
                }

                return SqlMapper.Query(_connection, sql, _params, commandTimeout: CommandTimeout, commandType: _commandType) as IEnumerable<IDictionary<int, string>>;
            }
        }

        public IEnumerable<dynamic> Query(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value);
                }

                return SqlMapper.Query(_connection, sql, _params, commandType: _commandType);
            }
        }

        public IEnumerable<dynamic> Query(IDbTransaction _transaction, IDbConnection _connection, string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            DynamicParameters _params = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value.ToString());
                }
            }

            return SqlMapper.Query(_connection, sql, _params, transaction: _transaction, commandType: _commandType);
        }

        public IDictionary<string, object> Query(IDbTransaction _transaction, IDbConnection _connection, string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout)
        {
            DynamicParameters _params = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value.ToString());
                }
            }

            return SqlMapper.Query(_connection, sql, _params, commandTimeout: CommandTimeout, transaction: _transaction, commandType: _commandType).ToDictionary(r => (string)r.TokenName, r => (object)r.TokenValue);
        }

        public int ExecuteQuery(IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType, int CommandTimeout)
        {
            int iRetVal = 0;
            using (IDbConnection _connection = _transaction.Connection)
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        _params.Add(Prefix + param.Name, param.Value);
                    }
                }

                iRetVal = SqlMapper.Execute(_connection, sql, _params, transaction: _transaction, commandTimeout: CommandTimeout, commandType: _commandType);
            }

            return iRetVal;
        }

        public int ExecuteQuery(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int iRetVal = 0;
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        _params.Add(Prefix + param.Name, param.Value);
                    }
                }

                iRetVal = SqlMapper.Execute(_connection, sql, _params, commandType: _commandType);
            }

            return iRetVal;
        }

        public int ExecuteQuery(IDbConnection _connection, IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int iRetVal = 0;

            DynamicParameters _params = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value);
                }
            }

            iRetVal = SqlMapper.Execute(_connection, sql, _params, transaction: _transaction, commandType: _commandType);

            return iRetVal;
        }

        public int ExecuteScalar(IDbConnection _connection, IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int iRetVal = 0;

            DynamicParameters _params = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value);
                }
            }

            iRetVal = SqlMapper.ExecuteScalar<int>(_connection, sql, _params, transaction: _transaction, commandType: _commandType);

            return iRetVal;
        }

        public async Task<int> ExecuteScalarAsync(IDbConnection _connection, IDbTransaction _transaction, string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int iRetVal = 0;

            DynamicParameters _params = new DynamicParameters();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value);
                }
            }

            iRetVal = await SqlMapper.ExecuteScalarAsync<int>(_connection, sql, _params, transaction: _transaction, commandType: _commandType);

            return iRetVal;
        }

        public async Task<int> ExecuteScalarAsync(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int iRetVal = 0;

            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        _params.Add(Prefix + param.Name, param.Value);
                    }
                }

                iRetVal = await SqlMapper.ExecuteScalarAsync<int>(_connection, sql, _params, commandType: _commandType);
            }

            return iRetVal;
        }

        public int ExecuteScalar(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int iRetVal = 0;
            using (IDbConnection _connection = CreateConnection())
            {
                using (IDbTransaction trans = _connection.BeginTransaction())
                {
                    try
                    {
                        DynamicParameters _params = new DynamicParameters();

                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                _params.Add(Prefix + param.Name, param.Value);
                            }
                        }

                        iRetVal = SqlMapper.ExecuteScalar<int>(_connection, sql, _params, trans, commandType: _commandType);
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }

            return iRetVal;
        }

        public T ExecuteScalar<T>(string sql, List<ParameterInfo> parameters, CommandType _commandType)
        {
            T iRetVal;
            using (IDbConnection _connection = CreateConnection())
            {
                using (IDbTransaction trans = _connection.BeginTransaction())
                {
                    try
                    {
                        DynamicParameters _params = new DynamicParameters();

                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                _params.Add(Prefix + param.Name, param.Value);
                            }
                        }

                        iRetVal = SqlMapper.ExecuteScalar<T>(_connection, sql, _params, trans, commandType: _commandType);
                        trans.Commit();
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }

            return iRetVal;
        }

        public int ExecuteQueryWithIntOutputParam(string spName, List<ParameterInfo> parameters, CommandType _commandType)
        {
            int success = 0;
            using (IDbConnection _connection = CreateConnection())
            {
                DynamicParameters _params = new DynamicParameters();
                foreach (var param in parameters)
                {
                    _params.Add(Prefix + param.Name, param.Value);
                }

                success = SqlMapper.Execute(_connection, spName, _params, commandType: _commandType);
            }
            return success;
        }

        public int ExecuteScalar(IDbConnection dbConnection, List<ParameterInfo> @params, CommandType storedProcedure)
        {
            throw new NotImplementedException();
        }
    }
}