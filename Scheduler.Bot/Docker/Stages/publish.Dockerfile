ARG IMAGE_NAME
ARG BUILD_ID

FROM ${IMAGE_NAME}:build-${BUILD_ID}
ARG BUILD_CONFIGURATION
RUN dotnet publish --no-build ./Slack.Bot.Api/*.csproj -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false