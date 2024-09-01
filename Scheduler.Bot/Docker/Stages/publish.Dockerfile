ARG IMAGE_NAME
ARG BUILD_CONFIGURATION

FROM ${IMAGE_NAME}:build
RUN dotnet publish --no-build ./Slack.Bot.Api/*.csproj -c Release -o /app/publish /p:UseAppHost=false