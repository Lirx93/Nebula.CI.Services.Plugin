FROM dotnetcore/runtime:3.1

EXPOSE 5000

COPY Release /app/Release
WORKDIR /app/Release
ENTRYPOINT dotnet Nebula.CI.Services.Plugin.ApiHost.dll