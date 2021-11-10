

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Portal.DevTest.API/Portal.DevTest.API.csproj", "src/Portal.DevTest.API/"]
RUN dotnet restore "./src/Portal.DevTest.API/Portal.DevTest.API.csproj"
COPY . .
WORKDIR "/src/src/Portal.DevTest.API"
RUN dotnet build "Portal.DevTest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Portal.DevTest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Portal.DevTest.API.dll"]