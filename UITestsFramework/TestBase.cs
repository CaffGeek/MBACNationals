using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;

namespace UITestsFramework
{
    public abstract class TestBase
    {
        const int iisPort = 60828;
        private string _applicationName;
        private Process _iisProcess;

        public TestBase(string applicationName)
        {
            _applicationName = applicationName;
        }

        [TestInitialize]
        public void Initialize()
        {
            StartIIS();
            Browser.Initialize();
        }

        [TestCleanup]
        public void TestFixtureTearDown()
        {
            Browser.Close();
            if (_iisProcess.HasExited == false)
                _iisProcess.Kill();
        }
                
        private void StartIIS() {
            //See: http://stephenwalther.com/archive/2011/12/22/asp-net-mvc-selenium-iisexpress
            //But PHP isn't running...and it sucks I currently need it to be
            var applicationPath = GetApplicationPath(_applicationName);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
 
            _iisProcess = new Process();
            _iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, iisPort);
            _iisProcess.Start();
        }
  
        protected virtual string GetApplicationPath(string applicationName) {
            var solutionFolder = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)));
            return Path.Combine(solutionFolder, applicationName);
        }
    }
}
