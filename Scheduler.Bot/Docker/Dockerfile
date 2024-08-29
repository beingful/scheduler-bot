FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src
COPY ./*.sln .
COPY ./Slack.Bot.Api/*.csproj ./Slack.Bot.Api/
RUN dotnet restore

FROM restore AS build
ARG BUILD_CONFIGURATION=Release
COPY . .
RUN dotnet build --no-restore ./Slack.Bot.Api/*.csproj -c $BUILD_CONFIGURATION

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish --no-build ./Slack.Bot.Api/*.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Slack.Bot.Api.dll"]