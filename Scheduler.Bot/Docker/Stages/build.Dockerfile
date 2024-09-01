ARG IMAGE_NAME

FROM ${IMAGE_NAME}:restore
ARG CONFIGURATION
COPY . .
RUN dotnet build --no-restore ./Slack.Bot.Api/*.csproj -c ${CONFIGURATION}