using API_DOAN_Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper; 

namespace API_DOAN_Infrastructure.DOANDatabaseContext
{
    public class MariaDbContext : IDOANDbContext
    {
        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; set; }

        public MariaDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("Database1")); 
        }
        public int Delete<T>(string code)
        {
            var className = typeof(T).Name;
            var sql = $"DELETE FROM {className} WHERE {className}Code = @id";
            var paramaters = new DynamicParameters();
            paramaters.Add("@id", code);
            var res = Connection.Execute(sql, paramaters, transaction: Transaction);
            return res;
        }

        public IEnumerable<T> Get<T>()
        {
            var className = typeof(T).Name;
            var sql = $"SELECT * FROM {className}";
            var res = Connection.Query<T>(sql);
            return res;
        }

        public T? Get<T>(string code)
        {
            var className = typeof(T).Name;
            var sql = $"SELECT * FROM {className} WHERE {className}Code = @id";
            var paramaters = new DynamicParameters();
            paramaters.Add("@id", code);
            var res = Connection.QueryFirstOrDefault<T>(sql, paramaters);
            return res;
        }

        public int Insert<T>(T entity)
        {
            var className = typeof(T).Name;
            var propListName = "";
            var propListValue = "";

            //Lay ra cac props cua entity 
            var props = entity.GetType().GetProperties();
            var paramaters = new DynamicParameters();

            //Duyet tung prop: 
            foreach (var prop in props)
            {
                //Lay ra ten của prop 
                var propName = prop.Name;

                //Lay ra gia tri cua prop 
                var propValue = prop.GetValue(entity);

                propListName += $"{propName},";
                propListValue += $"@{propName},";

                paramaters.Add($"@{propName}", propValue);
            }
            propListName = propListName.Substring(0, propListName.Length - 1);
            propListValue = propListValue.Substring(0, propListValue.Length - 1);

            //Build cau lenh insert 
            var sqlInsert = $"INSERT INTO {className}({propListName}) VALUES({propListValue})";

            //Thuc thi 
            var res = Connection.Execute(sqlInsert, paramaters, transaction: Transaction);
            return res;
        }

        public int Update<T>(T entity)
        {
            var className = typeof(T).Name;
            var propSet = "";

            //Lay ra cac props cua entity 
            var props = entity.GetType().GetProperties();
            var paramaters = new DynamicParameters();
            var value = props[0].GetValue(entity);
            //Duyet tung prop: 
            foreach (var prop in props)
            {
                //Lay ra ten của prop 
                var propName = prop.Name;

                //Lay ra gia tri cua prop 
                var propValue = prop.GetValue(entity);
                propSet += $"{propName}=@{propName},";

                paramaters.Add($"@{propName}", propValue);
            }
            propSet = propSet.Substring(0, propSet.Length - 1);

            //Build cau lenh update 
            var sqlInsert = $"UPDATE {className} SET {propSet} WHERE {className}Code = @id";
            paramaters.Add("@id", props[1].GetValue(entity));

            //Thuc thi 
            var res = Connection.Execute(sqlInsert, paramaters, transaction: Transaction);
            return res;
        }

        public int DeleteAny<T>(string[] codes)
        {
            var className = typeof(T).Name;
            var res = 0;
            var sql = $"DELETE FROM {className} WHERE {className}Code IN {@codes}"; 
            var paramaters = new DynamicParameters();
            var idsArray = ""; 
            foreach (var id in codes)
            {
                idsArray += $"{id},"; 
            }
            idsArray = idsArray.Substring(0, idsArray.Length - 1);
            paramaters.Add("@codes", idsArray);
            res += Connection.Execute(sql, paramaters);
            return res; 
        }
    }
}
