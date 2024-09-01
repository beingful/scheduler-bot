ARG IMAGE_NAME

FROM ${IMAGE_NAME}:base
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Slack.Bot.Api.dll"]