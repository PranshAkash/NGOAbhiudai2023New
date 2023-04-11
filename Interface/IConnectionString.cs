using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOAbhiudai2023.Interface
{
    public interface IConnectionString
    {
        string connectionString { get; set; }
    }

    public class ConnectionString : IConnectionString
    {
        public string connectionString { get; set; }
    }
}
