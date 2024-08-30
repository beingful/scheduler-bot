ARG CONTAINER_NAME

FROM base AS final
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Slack.Bot.Api.dll"]