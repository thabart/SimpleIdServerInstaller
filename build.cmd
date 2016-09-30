echo restore the nuget packages ...
dotnet restore repositories/SimpleIdentityServer/SimpleIdentityServer/src && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Uma.Host -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Uma.Host && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Manager.Host.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Manager.Host.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Configuration.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Configuration.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/IdentityServer4.Startup -o sources/SimpleIdentityServer/IdentityServer4.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.IdentityServer.Manager.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.IdentityServer.Manager.Startup && ^
dotnet publish repositories/SimpleIdentityServer/SimpleIdentityServer/src/SimpleIdentityServer.Configuration.IdServer.Startup -f net46 -o sources/SimpleIdentityServer/SimpleIdentityServer.Configuration.IdServer.Startup"