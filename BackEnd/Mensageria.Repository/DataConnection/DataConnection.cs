
using System.Configuration;
using Dapper;
using Menssageria.Model.Entities;
using Menssageria.Model.Interfaces;
using Model.Entities;
using Model.Interfaces;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;



namespace Mensageria.Repository.DataConnection
{
    public class DataConnection : IDataConnection
    {
        private string _ConnectionString { get; set; }
        private MySqlConnection _mySqlConnection { get; set; }
        public DataConnection()
        {
            _ConnectionString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            this._mySqlConnection = new MySqlConnection(_ConnectionString);
        }

     

       

        public void OpenConection()
        {
            _mySqlConnection.Open();
        }

        public void CloseConection()
        {
            _mySqlConnection.Close();
        }

        public void ExecuteQuery(string query)
        {
            _mySqlConnection.Execute(query);

        }


        public T GetExecute<T>(string query) where T : class
        {
         return _mySqlConnection.QueryFirstOrDefault<T>(query);
        }

      
    }
}
