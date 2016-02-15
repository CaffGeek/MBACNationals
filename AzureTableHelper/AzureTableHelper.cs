using Microsoft.WindowsAzure.Storage.Table;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AzureTableHelper
{
    public class AzureTableHelper
    {
        private static Dictionary<string, string> _cachedTableNames = new Dictionary<string, string>();

        private readonly CloudTableClient _tableClient;

        public AzureTableHelper(CloudTableClient tableClient)
        {
            _tableClient = tableClient;

            GetTableNames();
        }

        public Dictionary<string, string> GetTableNames()
        {
            var nameTable = GetNameTable();
            var tableResults = nameTable.ExecuteQuery(new TableQuery<TableName>());

            _cachedTableNames = new Dictionary<string, string>();
            foreach (var tableName in tableResults)
            {
                _cachedTableNames.Add(tableName.RowKey, tableName.CurrentTableName);
            }

            return _cachedTableNames;
        } 

        public CloudTable GetTableFor(Type type)
        {
            return GetTableFor(type.Name);
        }

        public string GetTableNameFor(Type type)
        {
            return GetTableNameFor(type.Name);
        }

        public string GetNextTableNameFor(Type type)
        {
            return GetNextTableNameFor(type.Name);
        }

        public string IterateTableNameFor(Type type)
        {
            return IterateTableNameFor(type.Name);
        }

        public void DeleteTable(Type type)
        {
            DeleteTable(type.Name);
        }

        public string BackupTable(Type type)
        {
            return BackupTable(type.Name);
        }

        public CloudTable GetTableFor(string typeName)
        {
            var currentTableName = GetTableNameFor(typeName);
            return _tableClient.GetTableReference(currentTableName);
        }

        public string GetTableNameFor(string typeName)
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
                var entity = (TableName)tableResults.Result;

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
            }

            return currentTableName;
        }

        public string GetNextTableNameFor(string typeName)
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

            return typeName + iteration;
        }
        
        public string IterateTableNameFor(string typeName)
        {
            typeName = typeName.ToLower();

            var nameTable = GetNameTable();
            var tableResults = nameTable.Execute(TableOperation.Retrieve<TableName>(typeName, typeName));
            var entity = (TableName)tableResults.Result
                ?? new TableName(typeName)
                {
                    CurrentTableName = typeName
                };

            entity.CurrentTableName = GetNextTableNameFor(typeName);
            nameTable.Execute(TableOperation.InsertOrReplace(entity));

            _cachedTableNames[typeName] = entity.CurrentTableName;
            var table = GetTableFor(typeName);
            table.CreateIfNotExists();
            
            return entity.CurrentTableName;
        }

        public void DeleteTable(string typeName)
        {
            var table = _tableClient.GetTableReference(typeName);
            table.DeleteIfExistsAsync();
        }

        public string BackupTable(string typeName)
        {
            var originalTable = GetTableFor(typeName);

            TableContinuationToken token = null;
            var originalEntities = new List<DynamicTableEntity>();
            do
            {
                var queryResult = originalTable.ExecuteQuerySegmented(new TableQuery(), token);
                originalEntities.AddRange(queryResult.Results);
                token = queryResult.ContinuationToken;
            } while (token != null);

            var newTableName = GetNextTableNameFor(typeName);
            var newTable = _tableClient.GetTableReference(newTableName);
            newTable.CreateIfNotExists();

            //BatchOperations can only be done on a single partition at a time
            foreach (var partition in originalEntities.GroupBy(x => x.PartitionKey))
            {
                //BatchOperations are limited to 1000 actions per batch
                foreach (var batch in partition.Batch(1000))
                {
                    var batchOperation = new TableBatchOperation();
                    foreach (var entity in batch)
                    {
                        batchOperation.Insert(entity);
                    }
                    newTable.ExecuteBatch(batchOperation);
                }
            }

            return newTableName;
        }

        private CloudTable GetNameTable()
        {
            //You need to install Azure Storage Emulator from: https://azure.microsoft.com/en-us/downloads/
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