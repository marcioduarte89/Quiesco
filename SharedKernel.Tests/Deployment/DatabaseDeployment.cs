using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Dac;
using NUnit.Framework;
using System;
using System.IO;

namespace SharedKernel.Tests.Deployment
{
    public class DatabaseDeployment
    {
        private Action<string> _log;
        private static string isTestingEnv = Environment.GetEnvironmentVariable("CI");

        public static void DeployDb(string connectionString, string dacpacFileName)
        {
            var dbName = GetDbName(connectionString);

            var dacOptions = new DacDeployOptions
            {
                BlockOnPossibleDataLoss = false,
                CreateNewDatabase = true
            };

            TestContext.Progress.WriteLine($"Deploying {dbName}...");

            var dacServiceInstance = new DacServices(connectionString);

            var basePath = TestContext.CurrentContext.TestDirectory;
            var dacpacFile = Path.Combine(basePath, dacpacFileName);
            using (var dacPac = DacPackage.Load(dacpacFile))
            {
                dacServiceInstance.Deploy(dacPac, dbName, true, dacOptions);
            }
        }

        public static void DropDb(string connectionString)
        {
            var dbName = GetDbName(connectionString);
            TestContext.Progress.WriteLine($"Dropping {dbName}...");

            // use "original" connection string, otherwise DB will be in use
            using (var cnn = new SqlConnection(UseMasterDb(connectionString)))
            {
                cnn.Open();
                using (var cm = cnn.CreateCommand())
                {
                    cm.CommandText = $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; "
                                     + $"DROP DATABASE [{dbName}]";

                    cm.ExecuteNonQuery();
                }
            }
        }

        private static string GetDbName(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            return builder.InitialCatalog;
        }

        private static string UseMasterDb(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = "master"
            };

            return builder.ConnectionString;
        }

        public static string GetConnectionString(string connectionString, string catalog)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);

            // Need to change this so is agnostic of who's running it
            if (!bool.TryParse(isTestingEnv, out var result) || !result)
            {
                if (!builder.InitialCatalog.Equals(catalog))
                {
                    builder.InitialCatalog = catalog;
                }
            }

            return builder.ConnectionString;
        }

        public static void UpdateConfig(string section, string connectionString)
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.Integration.json");
                var json = File.ReadAllText(filePath);
                dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

                void SetSectionPath(string key, string value)
                {
                    var sectionPath = key.Split(':')[0];
                    if (!string.IsNullOrEmpty(sectionPath))
                    {
                        var keyPath = key.Split(':')[1];
                        jsonObj[sectionPath][keyPath] = value;
                    }
                    else
                    {
                        jsonObj[sectionPath] = value; // if no sectionpath just set the value
                    }
                }

                SetSectionPath(section, connectionString);
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, output);
            }
            catch (Exception ex)
            {
                TestContext.Progress.WriteLine("Error writing app settings", ex);
            }
        }
    }
}
