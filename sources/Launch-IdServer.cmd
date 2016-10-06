set ASPNETCORE_ENVIRONMENT=idserver
set DATA_MIGRATED=true
set INTROSPECTION_CLIENT_ID=website_api
set INTROSPECTION_CLIENT_SECRET=website_api
START cmd /k "cd %IDSERVER%SimpleIdentityServer\IdentityServer4.Startup && IdentityServer4.Startup.exe --server.urls=https://*:5443"
START cmd /k "cd %IDSERVER%SimpleIdentityServer\SimpleIdentityServer.Uma.Host && SimpleIdentityServer.Uma.Host.exe --server.urls=https://*:5445"
START cmd /k "cd %IDSERVER%SimpleIdentityServer\SimpleIdentityServer.IdentityServer.Manager.Startup && SimpleIdentityServer.IdentityServer.Manager.Startup.exe --server.urls=http://*:5002"
START cmd /k "cd %IDSERVER%SimpleIdentityServer\SimpleIdentityServer.Configuration.IdServer.Startup && SimpleIdentityServer.Configuration.IdServer.Startup.exe --server.urls=http://*:5004"
START cmd /k "cd %IDSERVER%UmaManagerWebSiteApi && npm install && node server-api.js"
START cmd /k "cd %IDSERVER%UmaManagerWebSite && node server-idserver.js"
echo Applications are running ...