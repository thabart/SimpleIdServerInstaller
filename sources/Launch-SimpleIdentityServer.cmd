REM for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"
set ASPNETCORE_ENVIRONMENT=
set DATA_MIGRATED=true
set INTROSPECTION_CLIENT_ID=Anonymous
set INTROSPECTION_CLIENT_SECRET=Anonymous
REM set SERVER=simpleidserver
START cmd /k "cd %IDSERVER%SimpleIdentityServer\SimpleIdentityServer.Startup && SimpleIdentityServer.Startup.exe --server.urls=https://*:5443"
START cmd /k "cd %IDSERVER%SimpleIdentityServer\SimpleIdentityServer.Uma.Host && SimpleIdentityServer.Uma.Host.exe --server.urls=https://*:5445"
START cmd /k "cd %IDSERVER%SimpleIdentityServer\SimpleIdentityServer.Manager.Host.Startup && SimpleIdentityServer.Manager.Host.Startup.exe --server.urls=http://*:5002"
START cmd /k "cd %IDSERVER%SimpleIdentityServer\SimpleIdentityServer.Configuration.Startup && SimpleIdentityServer.Configuration.Startup.exe --server.urls=http://*:5004"
START cmd /k "cd %IDSERVER%UmaManagerWebSiteApi && node server-api.js"
START cmd /k "cd %IDSERVER%UmaManagerWebSite && node server.js"
echo Applications are running ...