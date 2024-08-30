ARG CONTAINER_NAME
ARG BUILD_CONFIGURATION

FROM restore AS build
COPY . .
RUN dotnet build --no-restore ./Slack.Bot.Api/*.csproj -c $BUILD_CONFIGURATION