using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
namespace Models
{
    public abstract class Connection<T>: IDisposable where T: IDbConnection, new()
    {
        protected IDbConnection Connect { get; private set; }
        protected string ConnectionString { get; private set; }

        public Connection(string connectionString)
        {
            ConnectionString = connectionString;
            Connect = Activator.CreateInstance<T>();
            Connect.ConnectionString = connectionString;
        }

        public long Insert<TModel>(TModel model) where TModel: class
        {
            return Connect.Insert(model);
        }

        public bool Edit<TModel>(TModel model) where TModel : class
        {
            return Connect.Update(model);
        }

        public bool Delete<TModel>(TModel model) where TModel : class
        {
            return Connect.Delete(model);
        }

        public TModel Find<TModel>(long id) where TModel : class
        {
            return Connect.Get<TModel>(id);
        }

        public TModel Find<TModel>(int id) where TModel : class
        {
            return Connect.Get<TModel>(id);
        }

        public IEnumerable<TModel> Get<TModel>() where TModel : class
        {
            return Connect.GetAll<TModel>(); 
        }

        public IEnumerable<TModel> Get<TModel>(string sql, string filter, params string[] select)
        {                      
            return (!string.IsNullOrEmpty(filter))
                ? Connect.Query<TModel>(sql, new { Filter = $"%{filter}%" })
                : Connect.Query<TModel>(sql);
        }

        public void Dispose()
        {
            Connect = null;
            GC.SuppressFinalize(this);
        }
    }
}
