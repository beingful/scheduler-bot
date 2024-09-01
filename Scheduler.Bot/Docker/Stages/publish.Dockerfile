ARG IMAGE_NAME, BUILD_CONFIGURATION

FROM ${IMAGE_NAME}:build
RUN dotnet publish --no-build ./Slack.Bot.Api/*.csproj -c ${BUILD_CONFIGURATION} -o /app/publish /p:UseAppHost=false