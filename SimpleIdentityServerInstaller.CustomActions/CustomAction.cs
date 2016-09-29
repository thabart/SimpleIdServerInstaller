using Microsoft.Deployment.WindowsInstaller;
using System;

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

                session.Log("Finish to update the appsettings.json files");
            }
            catch(Exception ex)
            {
                session.Log($"ERROR is custom action UpdateAppSettings {ex}");
                return ActionResult.Failure;
            }

            return ActionResult.Success;
        }
    }
}
