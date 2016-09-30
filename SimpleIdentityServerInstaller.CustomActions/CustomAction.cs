using Microsoft.Deployment.WindowsInstaller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SimpleIdentityServerInstaller.CustomActions
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult UpdateAppSettings(Session session)
        {
            try
            {
                session.Log("Start to update the appsettings.json files");
                var installDirectory = session["INSTALLFOLDER"];
                var connectionString = session["DATABASE_CONNECTIONSTRING"];
                var appSettings = new string[] {
                    Path.Combine(installDirectory, @"SimpleIdentityServer\SimpleIdentityServer.Startup\appsettings.json"),
                    Path.Combine(installDirectory, @"SimpleIdentityServer\SimpleIdentityServer.Uma.Host\appsettings.json"),
                    Path.Combine(installDirectory, @"SimpleIdentityServer\SimpleIdentityServer.Manager.Host.Startup\appsettings.json"),
                    Path.Combine(installDirectory, @"SimpleIdentityServer\SimpleIdentityServer.Configuration.Startup\appsettings.json"),
                    Path.Combine(installDirectory, @"SimpleIdentityServer\IdentityServer4.Startup\appsettings.json"),
                    Path.Combine(installDirectory, @"SimpleIdentityServer\SimpleIdentityServer.IdentityServer.Manager.Startup\appsettings.json")
                };
                UpdateConnectionStrings(appSettings, session, connectionString);
                session.Log("Finish to update the appsettings.json files");
            }
            catch(Exception ex)
            {
                session.Log($"ERROR in custom action UpdateAppSettings {ex}");
                return ActionResult.Failure;
            }

            return ActionResult.Success;
        }
        
        [CustomAction]
        public static ActionResult TestConnection(Session session)
        {
            try
            {
                session.Log("Start to test the connection");
                var databaseServer = session["DATABASE_SERVER"];
                var databaseCatalog = session["DATABASE_CATALOG"];
                var strBuilder = new StringBuilder($"Data Source={databaseServer};Initial Catalog={databaseCatalog};");
                if (string.Equals(session["DATABASE_WINDOWSAUTHENTICATION"], "0", StringComparison.InvariantCultureIgnoreCase))
                {
                    strBuilder.Append("Integrated Security=True;");
                }
                else
                {
                    var databaseLogin = session["DATABASE_LOGIN"];
                    var databasePassword = session["DATABASE_PASSWORD"];                    
                    strBuilder.Append($"User ID={databaseLogin};Password={databasePassword};");
                }

                var connectionString = strBuilder.ToString();
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    session.Log("Finish to test the connection");
                    session["DATABASE_CONNECTION_CORRECT"] = "1";
                    session["DATABASE_CONNECTIONSTRING"] = connectionString;
                    MessageBox.Show("Can connect to the database", "SimpleIdentityServer Installer");
                }

            }
            catch (Exception ex)
            {
                session.Log($"ERROR in custom action TestConnection {ex}");
                session["DATABASE_CONNECTION_CORRECT"] = "0";
                session["DATABASE_CONNECTIONSTRING"] = string.Empty;
                MessageBox.Show("Cannot connect to the database", "SimpleIdentitySever Installer", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
