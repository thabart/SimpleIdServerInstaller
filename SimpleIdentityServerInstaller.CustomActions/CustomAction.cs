using Microsoft.Deployment.WindowsInstaller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimpleIdentityServerInstaller.CustomActions
{
    public class CustomActions
    {
        #region Custom actions

        [CustomAction]
        public static ActionResult UpdateSimpleIdentityServerConnectionStrings(Session session)
        {
            return UpdateConnectionStringInAppSettings(new string[] 
            {
                @"SimpleIdentityServer\SimpleIdentityServer.Startup\appsettings.json",
                @"SimpleIdentityServer\SimpleIdentityServer.Manager.Host.Startup\appsettings.json"
            }, session, session["DATABASE_SIMPLEIDSERVER_CONNECTIONSTRING"]);
        }

        [CustomAction]
        public static ActionResult UpdateUmaConnectionStrings(Session session)
        {
            return UpdateConnectionStringInAppSettings(new string[]
            {
                @"SimpleIdentityServer\SimpleIdentityServer.Uma.Host\appsettings.json"
            }, session, session["DATABASE_UMA_CONNECTIONSTRING"]);
        }

        [CustomAction]
        public static ActionResult UpdateConfigurationConnectionStrings(Session session)
        {
            return UpdateConnectionStringInAppSettings(new string[]
            {
                @"SimpleIdentityServer\SimpleIdentityServer.Configuration.Startup\appsettings.json"
            }, session, session["DATABASE_CONFIGURATION_CONNECTIONSTRING"]);
        }

        [CustomAction]
        public static ActionResult UpdateIdServerConnectionStrings(Session session)
        {
            return UpdateConnectionStringInAppSettings(new string[]
            {
                @"SimpleIdentityServer\IdentityServer4.Startup\appsettings.json",
                @"SimpleIdentityServer\SimpleIdentityServer.IdentityServer.Manager.Startup\appsettings.json"
            }, session, session["DATABASE_IDSERVER_CONNECTIONSTRING"]);
        }

        [CustomAction]
        public static ActionResult TestSimpleIdentityServerConnection(Session session)
        {
            return TestConnection(session,
                "DATABASE_SIMPLEIDSERVER_SERVER",
                "DATABASE_SIMPLEIDSERVER_CATALOG",
                "DATABASE_SIMPLEIDSERVER_WINDOWSAUTHENTICATION",
                "DATABASE_SIMPLEIDSERVER_LOGIN",
                "DATABASE_SIMPLEIDSERVER_PASSWORD",
                "DATABASE_SIMPLEIDSERVER_CONNECTION_CORRECT",
                "DATABASE_SIMPLEIDSERVER_CONNECTIONSTRING");
        }

        [CustomAction]
        public static ActionResult TestUmaConnection(Session session)
        {
            return TestConnection(session,
                "DATABASE_UMA_SERVER",
                "DATABASE_UMA_CATALOG",
                "DATABASE_UMA_WINDOWSAUTHENTICATION",
                "DATABASE_UMA_LOGIN",
                "DATABASE_UMA_PASSWORD",
                "DATABASE_UMA_CONNECTION_CORRECT",
                "DATABASE_UMA_CONNECTIONSTRING");
        }

        [CustomAction]
        public static ActionResult TestConfigurationConnection(Session session)
        {
            return TestConnection(session,
                "DATABASE_CONFIGURATION_SERVER",
                "DATABASE_CONFIGURATION_CATALOG",
                "DATABASE_CONFIGURATION_WINDOWSAUTHENTICATION",
                "DATABASE_CONFIGURATION_LOGIN",
                "DATABASE_CONFIGURATION_PASSWORD",
                "DATABASE_CONFIGURATION_CONNECTION_CORRECT",
                "DATABASE_CONFIGURATION_CONNECTIONSTRING");
        }

        [CustomAction]
        public static ActionResult TestIdServerConnection(Session session)
        {
            return TestConnection(session,
                "DATABASE_IDSERVER_SERVER",
                "DATABASE_IDSERVER_CATALOG",
                "DATABASE_IDSERVER_WINDOWSAUTHENTICATION",
                "DATABASE_IDSERVER_LOGIN",
                "DATABASE_IDSERVER_PASSWORD",
                "DATABASE_IDSERVER_CONNECTION_CORRECT",
                "DATABASE_IDSERVER_CONNECTIONSTRING");
        }

        #endregion

        #region Private

        private static ActionResult UpdateConnectionStringInAppSettings(IEnumerable<string> appSettings, Session session, string connectionString)
        {
            try
            {
                session.Log("Start to update the appsettings.json files");
                var installDirectory = session["INSTALLFOLDER"];
                appSettings = appSettings.Select(p => Path.Combine(installDirectory, p));
                UpdateConnectionStrings(appSettings, session, connectionString);
                session.Log("Finish to update the appsettings.json files");
            }
            catch (Exception ex)
            {
                session.Log($"ERROR in custom action UpdateAppSettings {ex}");
                return ActionResult.Failure;
            }

            return ActionResult.Success;
        }

        private static ActionResult TestConnection(
            Session session,
            string serverPropertyName,
            string catalogPropertyName,
            string windowsAuthPropertyName,
            string loginPropertyName,
            string passwordPropertyName,
            string correctPropertyName,
            string connectionStringPropertyName)
        {
            const string title = "SimpleIdentityServer Installer";
            try
            {
                session.Log("Start to test the connection");
                var databaseServer = session[serverPropertyName];
                var databaseCatalog = session[catalogPropertyName];
                if (string.IsNullOrWhiteSpace(databaseCatalog))
                {

                    MessageBox.Show("Catalog cannot be empty", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return ActionResult.Success;
                }

                var strBuilder = new StringBuilder($"Data Source={databaseServer};Initial Catalog={databaseCatalog};");
                if (string.Equals(session[windowsAuthPropertyName], "0", StringComparison.InvariantCultureIgnoreCase))
                {
                    strBuilder.Append("Integrated Security=True;");
                }
                else
                {
                    var databaseLogin = session[loginPropertyName];
                    var databasePassword = session[passwordPropertyName];
                    strBuilder.Append($"User ID={databaseLogin};Password={databasePassword};");
                }

                var connectionString = strBuilder.ToString();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    session.Log("Finish to test the connection");
                    session[correctPropertyName] = "1";
                    session[connectionStringPropertyName] = connectionString;
                    MessageBox.Show("Can connect to the database", title);
                }

            }
            catch (Exception ex)
            {
                session.Log($"ERROR in custom action TestConnection {ex}");
                session[correctPropertyName] = "0";
                session[connectionStringPropertyName] = string.Empty;
                MessageBox.Show("Cannot connect to the database", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ActionResult.Success;
        }

        private static void UpdateConnectionStrings(IEnumerable<string> paths, Session session, string connectionString)
        {
            foreach(var path in paths)
            {
                if (!File.Exists(path) && path.EndsWith("appsettings.json"))
                {
                    continue;
                }

                var json = File.ReadAllText(path);
                var jObj = JObject.Parse(json);
                var token = jObj.SelectToken("Data.DefaultConnection.ConnectionString");
                if (token == null)
                {
                    session.Log("the connection string doesn't exist");
                    continue;
                }

                var val = token as JValue;
                if (val == null)
                {
                    session.Log("it's not a JValue");
                    continue;
                }

                val.Value = connectionString;
                var output = jObj.ToString(Formatting.None);
                File.WriteAllText(path, output);
            }
        }

        #endregion
    }
}
