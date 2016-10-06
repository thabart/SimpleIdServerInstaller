set SERVER=idserver
dotnet restore repositories/SimpleIdentityServer/SimpleIdentityServer/src && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/IdentityServer4.Startup -o sources/SimpleIdentityServer/IdentityServer4.Startup -r win7-x64 && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Uma.Host -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Uma.Host && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.IdentityServer.Manager.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.IdentityServer.Manager.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Configuration.IdServer.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Configuration.IdServer.Startup && ^
REM npm install repositories/UmaManagerWebSiteApi --prefix repositories/UmaManagerWebSiteApi && ^
xcopy "repositories\UmaManagerWebSiteApi" "sources\UmaManagerWebSiteApi" /s /e /y /i && ^
cd repositories\UmaManagerWebSite && npm install && bower install && ember build && ^
cd ..\..\ && xcopy "repositories\UmaManagerWebSite\dist" "sources\UmaManagerWebSite\distIdServer" /s /e /y /i && ^
cd sources\UmaManagerWebSite\ && npm install && cd ..\..\