ARG IMAGE_NAME

FROM ${IMAGE_NAME}:restore
ARG BUILD_CONFIGURATION
COPY . .
RUN dotnet build --no-restore ./Slack.Bot.Api/*.csproj -c ${BUILD_CONFIGURATION}