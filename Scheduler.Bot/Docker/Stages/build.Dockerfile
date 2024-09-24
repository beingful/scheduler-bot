ARG IMAGE
ARG BUILD_ID

FROM ${IMAGE}:restore-${BUILD_ID}
ARG BUILD_CONFIGURATION
COPY . .
RUN dotnet build --no-restore ./Slack.Bot.Api/*.csproj -c ${BUILD_CONFIGURATION}