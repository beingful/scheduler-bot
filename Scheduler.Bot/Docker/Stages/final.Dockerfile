ARG IMAGE_NAME

FROM ${IMAGE_NAME}:base
COPY --from=IMAGE_NAME:publish /app/publish .
ENTRYPOINT ["dotnet", "Slack.Bot.Api.dll"]