using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            throw new NotImplementedException();
        }

        private static void DeleteExistingTableStorage()
        {
            throw new NotImplementedException();
        }

        private static void SwitchTableStorageToBackups()
        {
            throw new NotImplementedException();
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
    }
}
