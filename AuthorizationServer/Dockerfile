FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["AuthorizationServer.sln", "./"]
COPY ["AuthorizationServer/AuthorizationServer.csproj", "AuthorizationServer/"]

RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR "/src/AuthorizationServer"
RUN dotnet build "AuthorizationServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AuthorizationServer.csproj" -c Release -o /app

FROM base AS final
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthorizationServer.dll"]
