ARG IMAGE_NAME
ARG BUILD_ID

FROM ${IMAGE_NAME}:publish-${BUILD_ID} AS publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER app
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Slack.Bot.Api.dll"]