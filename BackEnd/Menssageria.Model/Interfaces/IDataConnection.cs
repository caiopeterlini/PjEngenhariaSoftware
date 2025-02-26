using Menssageria.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menssageria.Model.Interfaces
{
    public interface IDataConnection
    {

        public T GetExecute<T>(string query) where T : class;
        public void OpenConection();
        public void CloseConection();
        public void ExecuteQuery(string query);
    }
}
