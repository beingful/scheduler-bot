ARG IMAGE_NAME

FROM ${IMAGE_NAME}:build
ARG BUILD_CONFIGURATION
RUN dotnet publish --no-build ./Slack.Bot.Api/*.csproj -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false