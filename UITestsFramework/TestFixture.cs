using System;
using System.Diagnostics;
using System.IO;

namespace UITestsFramework
{
    public class TestFixture : IDisposable
    {
        const int iisPort = 60828;
        private Process _iisProcess;

        public TestFixture()
        {
            BuildDatabase();
            StartIIS();
            Browser.Initialize();

            //Hack: but got to clear it out
            Pages.Pages.Register.Goto();
            Pages.Pages.Register.CreateAdminUser("Chad");
            Pages.Pages.Rebuild.Goto();
            Pages.Pages.Rebuild.RebuildAll();
        }

        public void Dispose()
        {
            Browser.Close();
            if (_iisProcess.HasExited == false)
                _iisProcess.Kill();
        }
        private void BuildDatabase()
        {
            Database.Initialization.BuildTables();
        }

        private void StartIIS()
        {
            //See: http://stephenwalther.com/archive/2011/12/22/asp-net-mvc-selenium-iisexpress
            //But PHP isn't running...and it sucks I currently need it to be
            var applicationPath = GetApplicationPath("Web.Admin");

            var tmpFolder = Path.Combine(Path.GetTempPath(), "MBACNationals_Tests");
            if (Directory.Exists(tmpFolder)) Directory.Delete(tmpFolder, true);
            Directory.CreateDirectory(tmpFolder);

            CloneDirectory(applicationPath, tmpFolder);

            File.Delete(Path.Combine(tmpFolder, "ConnectionStrings.config")); //Dev ConnectionStrings.config
            File.Move(Path.Combine(tmpFolder, "ConnectionStrings.config.test"), Path.Combine(tmpFolder, "ConnectionStrings.config")); //Replace with .test

            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            _iisProcess = new Process();
            _iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            _iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", tmpFolder, iisPort);
            _iisProcess.Start();
        }

        protected virtual string GetApplicationPath(string appPath)
        {
            var baseDir = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))));
            return Path.Combine(baseDir, appPath);
        }

        private static void CloneDirectory(string root, string dest)
        {
            foreach (var directory in Directory.GetDirectories(root))
            {
                string dirName = Path.GetFileName(directory);
                if (!Directory.Exists(Path.Combine(dest, dirName)))
                {
                    Directory.CreateDirectory(Path.Combine(dest, dirName));
                }
                CloneDirectory(directory, Path.Combine(dest, dirName));
            }

            foreach (var file in Directory.GetFiles(root))
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            }
        }
    }
}
