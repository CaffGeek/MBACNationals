using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace UITestsFramework
{
    public abstract class TestBase
    {
        private DateTime _lastEvent;
        [TestInitialize]
        public void Initialize()
        {
            PrepareDataTierForTest();
            Browser.Initialize();
        }

        [TestCleanup]
        public void TestFixtureTearDown()
        {
            Browser.Close();
            RestoreDataTierAfterTest();
        }

        private void PrepareDataTierForTest()
        {
            _lastEvent = GetLastEventTime();
            BackupTableStorage();
        }

        private void RestoreDataTierAfterTest()
        {
            DeleteNewEvents(_lastEvent);
            DeleteNewUsers(_lastEvent);
            CleanupOrphanedAggregates();
            DeleteExistingTableStorage();
            SwitchTableStorageToBackups();
        }

        private static void BackupTableStorage()
        {
            var tableClient = GetCloudTableClient();
            var azureTableHelper = new AzureTableHelper.AzureTableHelper(tableClient);
            var tableNames = azureTableHelper.GetTableNames();
            foreach (var tableName in tableNames)
            {
                azureTableHelper.BackupTable(tableName.Key);
            }
        }

        private static void DeleteExistingTableStorage()
        {
            var tableClient = GetCloudTableClient();
            var azureTableHelper = new AzureTableHelper.AzureTableHelper(tableClient);
            var tableNames = azureTableHelper.GetTableNames();
            foreach (var tableName in tableNames)
            {
                azureTableHelper.DeleteTable(tableName.Key);
            }
        }

        private static void SwitchTableStorageToBackups()
        {
            var tableClient = GetCloudTableClient();
            var azureTableHelper = new AzureTableHelper.AzureTableHelper(tableClient);
            var tableNames = azureTableHelper.GetTableNames();
            foreach (var tableName in tableNames)
            {
                azureTableHelper.IterateTableNameFor(tableName.Key);
            }
        }

        private static DateTime GetLastEventTime()
        {
            //TODO: Actually query DB
            return DateTime.Now;
        }

        private static void CleanupOrphanedAggregates()
        {
            var sql = "delete a from aggregates a where not exists (select 1 from events e where a.Id = e.AggregateId";
            //TODO: Execute
        }

        private static void DeleteNewEvents(DateTime since)
        {
            var sql = string.Format("delete from events where timestamp > '{0}'", since);
            //TODO: Execute and change to use SqlParameters
        }

        private static void DeleteNewUsers(DateTime lastEvent)
        {
            //TODO: Execute 
        }

        private static CloudTableClient GetCloudTableClient()
        {
            const string tableStorageConn = "UseDevelopmentStorage=true;";
            var storageAccount = CloudStorageAccount.Parse(tableStorageConn);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient;
        }
    }
}
