set SERVER=simpleidserver
dotnet restore repositories/SimpleIdentityServer/SimpleIdentityServer/src && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Uma.Host -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Uma.Host && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Manager.Host.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Manager.Host.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Configuration.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Configuration.Startup && ^
xcopy "repositories\UmaManagerWebSiteApi" "sources\UmaManagerWebSiteApi" /s /e /y /i && ^
REM npm install sources/UmaManagerWebSiteApi --prefix sources/UmaManagerWebSiteApi && ^
cd repositories\UmaManagerWebSite && npm install && bower install && ember build && ^
cd ..\..\ && xcopy "repositories\UmaManagerWebSite\dist" "sources\UmaManagerWebSite\dist" /s /e /y /i && ^
cd sources\UmaManagerWebSite\ && npm install && cd ..\..\