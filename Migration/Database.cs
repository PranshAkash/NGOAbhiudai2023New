﻿using Dapper;
using NGOAbhiudai2023.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGOAbhiudai2023.Migration
{
    public class Database
    {
        private readonly IDapperRepository _context;
        public Database(IDapperRepository context) => _context = context;
        public void CreateDatabase(string dbName)
        {
            var query = "SELECT * FROM sys.databases WHERE name = @name";
            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);
            using (var connection = _context.GetMasterConnection())
            {
                var records = connection.Query(query, parameters);
                if (!records.Any())
                    connection.Execute($"CREATE DATABASE {dbName}");
            }
        }
    }
}
