REM for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"
set ASPNETCORE_ENVIRONMENT=
set DATA_MIGRATED=true
START cmd /k "cd SimpleIdentityServer/SimpleIdentityServer.Startup && SimpleIdentityServer.Startup.exe --server.urls=https://*:5443"
START cmd /k "bash ./wait-for-it.sh localhost:5443 -t 300 && cd SimpleIdentityServer/SimpleIdentityServer.Uma.Host && SimpleIdentityServer.Uma.Host.exe -f net46 --server.urls=https://*:5445"
START cmd /k "bash ./wait-for-it.sh localhost:5445 -t 300 && cd SimpleIdentityServer/SimpleIdentityServer.Manager.Host.Startup && SimpleIdentityServer.Manager.Host.Startup.exe -f net46 --server.urls=http://*:5002"
START cmd /k "bash ./wait-for-it.sh localhost:5002 -t 300 && cd SimpleIdentityServer/SimpleIdentityServer.Configuration.Startup && SimpleIdentityServer.Configuration.Startup.exe -f net46 --server.urls=http://*:5004"
echo Applications are running ...