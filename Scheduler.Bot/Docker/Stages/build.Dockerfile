ARG IMAGE_NAME
ARG CONFIGURATION

FROM ${IMAGE_NAME}:restore
COPY . .
RUN dotnet build --no-restore ./Slack.Bot.Api/*.csproj -c $CONFIGURATION