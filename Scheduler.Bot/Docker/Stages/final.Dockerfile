ARG CONTAINER_NAME

FROM ${CONTAINER_NAME}:base
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Slack.Bot.Api.dll"]