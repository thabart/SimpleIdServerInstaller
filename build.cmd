echo restore the nuget packages ...
REM dotnet restore repositories/SimpleIdentityServer/SimpleIdentityServer/src && ^
REM dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Startup && ^
REM dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Uma.Host -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Uma.Host && ^
REM dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Manager.Host.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Manager.Host.Startup && ^
REM dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Configuration.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Configuration.Startup && ^
REM dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/IdentityServer4.Startup -o sources/SimpleIdentityServer/IdentityServer4.Startup && ^
REM dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.IdentityServer.Manager.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.IdentityServer.Manager.Startup && ^
REM dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Configuration.IdServer.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Configuration.IdServer.Startup" && ^
npm install repositories/UmaManagerWebSiteApi --prefix repositories/UmaManagerWebSiteApi && ^
echo f | XCOPY repositories\UmaManagerWebSiteApi sources\UmaManagerWebSiteApi\ /D /E /Y