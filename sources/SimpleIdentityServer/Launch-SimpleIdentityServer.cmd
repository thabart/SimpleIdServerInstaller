REM for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"
set ASPNETCORE_ENVIRONMENT=
set DATA_MIGRATED=true
START cmd /k "cd SimpleIdentityServer.Startup && SimpleIdentityServer.Startup.exe --server.urls=https://*:5443"
START cmd /k "cd SimpleIdentityServer.Uma.Host && SimpleIdentityServer.Uma.Host.exe --server.urls=https://*:5445"
START cmd /k "cd SimpleIdentityServer.Manager.Host.Startup && SimpleIdentityServer.Manager.Host.Startup.exe --server.urls=http://*:5002"
START cmd /k "cd SimpleIdentityServer.Configuration.Startup && SimpleIdentityServer.Configuration.Startup.exe --server.urls=http://*:5004"
echo Applications are running ...