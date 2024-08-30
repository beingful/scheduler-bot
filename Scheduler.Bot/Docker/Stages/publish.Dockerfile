ARG CONTAINER_NAME
ARG BUILD_CONFIGURATION

FROM ${CONTAINER_NAME}:build
RUN dotnet publish --no-build ./Slack.Bot.Api/*.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false