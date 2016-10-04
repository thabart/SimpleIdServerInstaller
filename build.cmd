set SERVER=simpleidserver
dotnet restore repositories/SimpleIdentityServer/SimpleIdentityServer/src && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Uma.Host -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Uma.Host && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Manager.Host.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Manager.Host.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Configuration.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Configuration.Startup && ^
npm install repositories/UmaManagerWebSiteApi --prefix repositories/UmaManagerWebSiteApi && ^
echo f | XCOPY repositories\UmaManagerWebSiteApi sources\UmaManagerWebSiteApi\ /D /E /Y && ^
cd repositories\UmaManagerWebSite && npm install && bower install && ember build && ^
cd ..\..\ && echo f | XCOPY repositories\UmaManagerWebSite\dist sources\UmaManagerWebSite\dist\ /D /E /Y && ^
cd sources\UmaManagerWebSite\ && npm install && cd ..\..\