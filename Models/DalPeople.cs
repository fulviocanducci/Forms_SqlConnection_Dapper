using System.Collections.Generic;
using System.Data.SqlClient;
namespace Models
{
    public class DalPeople
    {
        public Connection<SqlConnection> Connection { get; }

        public DalPeople(Connection<SqlConnection> connection)
        {
            Connection = connection;
        }

        public People Insert(People people)
        {
            people.Id = (int)Connection.Insert(people);
            return people;
        }       

        public bool Edit(People people)
        {
            return Connection.Edit(people);
        }

        public bool Delete(int id)
        {
            return Connection.Delete(Find(id));
        }

        public IEnumerable<People> Get()
        {
            return Connection.Get<People>();
        }

        public IEnumerable<TModel> Get<TModel>(string filter, params string[] select) where TModel: class
        {
            string sql = $"SELECT {"[" + string.Join("],[", select) + "]"} FROM [People] ";
            sql += " WHERE [People].[Name] LIKE @Filter ORDER BY [People].[Name] ASC";
            return Connection.Get<TModel>(sql, filter, select);
        }

        public People Find(int id)
        {
            return Connection.Find<People>(id);
        }        
    }
}
