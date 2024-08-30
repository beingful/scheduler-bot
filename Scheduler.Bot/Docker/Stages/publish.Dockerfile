ARG CONTAINER_NAME
ARG BUILD_CONFIGURATION

FROM build AS publish
RUN dotnet publish --no-build ./Slack.Bot.Api/*.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false