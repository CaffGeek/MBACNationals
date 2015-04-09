using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Storage.Table;

namespace MBACNationals.ReadModels
{
    public class AzureTableLocator
    {
        private readonly Dictionary<string, string> _cachedTableNames = new Dictionary<string, string>();
        private readonly CloudTableClient _tableClient;

        public AzureTableLocator(CloudTableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public CloudTable GetTableFor(Type type)
        {
            return GetTableFor(type.Name);
        }

        public CloudTable CreateNewTableFor(Type type)
        {
            return CreateNewTableFor(type.Name);
        }

        public CloudTable GetTableFor(string typeName)
        {
            typeName = typeName.ToLower();
            string currentTableName;
            if (_cachedTableNames.ContainsKey(typeName))
            {
                currentTableName = _cachedTableNames[typeName];
            }
            else 
            {
                var nameTable = GetNameTable();

                var tableResults = nameTable.Execute(TableOperation.Retrieve<TableName>(typeName, typeName));
                var entity = (TableName) tableResults.Result;

                if (entity != null)
                {
                    currentTableName = entity.CurrentTableName;
                }
                else
                {
                    currentTableName = typeName;
                    var newTable = new TableName(typeName)
                    {
                        CurrentTableName = currentTableName
                    };
                    nameTable.Execute(TableOperation.Insert(newTable));
                }
                _cachedTableNames.Add(typeName, currentTableName);
            }

            var currentTable = _tableClient.GetTableReference(currentTableName);
            currentTable.CreateIfNotExists();

            return currentTable;
        }

        public CloudTable CreateNewTableFor(string typeName)
        {
            typeName = typeName.ToLower();

            var nameTable = GetNameTable();
            var tableResults = nameTable.Execute(TableOperation.Retrieve<TableName>(typeName, typeName));
            var entity = (TableName)tableResults.Result
                ?? new TableName(typeName)
                {
                    CurrentTableName = typeName
                };

            var regex = new Regex("\\d+$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            var currentTableName = entity.CurrentTableName;
            var iteration = 1;
            var matches = regex.Match(currentTableName);
            if (matches.Success)
            {
                iteration = int.Parse(matches.Value) + 1;
            }

            var currentTable = _tableClient.GetTableReference(currentTableName);
            currentTable.DeleteIfExistsAsync();

            entity.CurrentTableName = typeName + iteration;
            nameTable.Execute(TableOperation.InsertOrReplace(entity));

            return GetTableFor(typeName);
        }

        private CloudTable GetNameTable()
        {
            var nameTable = _tableClient.GetTableReference("zzzTableNames");
            nameTable.CreateIfNotExists();
            return nameTable;
        }

        public class TableName : TableEntity
        {
            public TableName() { }
            public TableName(string originalTableName)
            {
                RowKey = originalTableName;
                PartitionKey = originalTableName;
            }
            public string CurrentTableName { get; set; }
        }
    }
}