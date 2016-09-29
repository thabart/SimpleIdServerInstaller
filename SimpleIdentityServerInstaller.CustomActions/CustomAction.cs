using Microsoft.Deployment.WindowsInstaller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleIdentityServerInstaller.CustomActions
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult UpdateAppSettings(Session session)
        {
            try
            {
                System.Diagnostics.Debugger.Launch();
                session.Log("Start to update the appsettings.json files");
                // TODO = Replace the current path
                const string currentPath = @"c:\Project\SimpleIdServerInstaller\sources";
                var appSettings = new string[] { Path.Combine(currentPath, @"SimpleIdentityServer\SimpleIdentityServer.Startup\appsettings.json") };
                UpdateConnectionStrings(appSettings, session, "connectionstring");
                session.Log("Finish to update the appsettings.json files");
            }
            catch(Exception ex)
            {
                session.Log($"ERROR is custom action UpdateAppSettings {ex}");
                return ActionResult.Failure;
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
